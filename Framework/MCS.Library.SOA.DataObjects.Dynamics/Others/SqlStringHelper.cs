using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Security;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics
{
    public static class SqlStringHelper
    {
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> list, params string[] igrozieColumns)
        {
            var mpp = ORMapping.GetMappingInfo<T>();
            DataTable dt = new DataTable(mpp.TableName);
            Type classType = typeof(T);
            mpp.ForEach(p =>
                            {
                                if (igrozieColumns.Contains(p.DataFieldName) == false)
                                {
                                    Type t = classType.GetProperty(p.PropertyName).PropertyType;
                                    DataColumn dc = new DataColumn(p.DataFieldName);

                                    if (p.EncryptProperty)
                                        dc.DataType = typeof(string);
                                    else if (p.SubClassPropertyName.IsNullOrWhiteSpace() == false)
                                    {
                                        dc.DataType = typeof(string);
                                    }
                                    else if (t.IsEnum)
                                    {
                                        dc.DataType = typeof(string);
                                    }
                                    else
                                        dc.DataType = t;

                                    if (p.PrimaryKey)
                                    {
                                        dc.AllowDBNull = false;
                                    }
                                    dt.Columns.Add(dc);
                                }
                            });
            list.ForEach(l =>
                             {
                                 DataRow dr = dt.NewRow();
                                 mpp.ForEach(p =>
                                                        {
                                                            DataColumn dc = dt.Columns[p.DataFieldName];
                                                            var data = GetValueFromObject(p, l);
                                                            if (data is DateTime)
                                                            {
                                                                if ((DateTime)data > DateTime.MinValue)
                                                                    dr[dc] = data;
                                                            }
                                                            else
                                                            {
                                                                dr[dc] = data;
                                                            }
                                                        });
                                 dt.Rows.Add(dr);
                             });

            return dt;
        }

        private static object GetValueFromObject(ORMappingItem item, object graph)
        {
            object data = null;

            if (string.IsNullOrEmpty(item.SubClassPropertyName))
            {
                data = GetValueFromObjectDirectly(item, graph);

                if (item.EncryptProperty)
                    data = EncryptPropertyValue(data);
            }
            else
            {
                if (graph != null)
                {
                    System.Reflection.MemberInfo mi = TypePropertiesWithNonPublicCacheQueue.Instance.GetPropertyInfoDirectly(graph.GetType(), item.PropertyName);

                    if (mi == null)
                        mi = graph.GetType().GetField(item.PropertyName,
                            BindingFlags.Instance | BindingFlags.Public);

                    if (mi != null)
                    {
                        object subGraph = GetMemberValueFromObject(mi, graph);

                        if (subGraph != null)
                            data = GetValueFromObjectDirectly(item, subGraph);
                    }
                }
            }

            return data;
        }

        private static object GetValueFromObjectDirectly(ORMappingItem item, object graph)
        {
            object data = GetMemberValueFromObject(item.MemberInfo, graph);

            if (data != null)
            {
                Type dataType = data.GetType();
                if (dataType.IsEnum)
                {
                    if (item.EnumUsage == EnumUsageTypes.UseEnumValue)
                        data = (int)data;
                    else
                        data = data.ToString();
                }
                else
                    if (dataType == typeof(TimeSpan))
                        data = ((TimeSpan)data).TotalSeconds;
            }

            return data;
        }

        private static object GetMemberValueFromObject(System.Reflection.MemberInfo mi, object graph)
        {
            try
            {
                object data = null;

                switch (mi.MemberType)
                {
                    case MemberTypes.Property:
                        PropertyInfo pi = (PropertyInfo)mi;
                        if (pi.CanRead)
                            data = pi.GetValue(graph, null);
                        break;
                    case MemberTypes.Field:
                        FieldInfo fi = (FieldInfo)mi;
                        data = fi.GetValue(graph);
                        break;
                }
                return data;
            }
            catch (Exception ex)
            {
                Exception realEx = ex.GetRealException();
                throw new ApplicationException(string.Format("读取属性{0}值的时候出错，{1}", mi.Name, realEx.Message));
            }
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="originalData"></param>
        /// <returns></returns>
        private static object EncryptPropertyValue(object originalData)
        {
            object result = originalData;

            if (originalData != null && originalData != DBNull.Value)
            {
                if (originalData is string == false || (string)originalData != string.Empty)
                {
                    result = originalData.ToEncryptorValue();
                }
            }

            return result;
        }

        /// <summary>
        /// 得到加密后的值
        /// </summary>
        /// <returns></returns>
        public static string ToEncryptorValue(this object originalData)
        {
            ISymmetricEncryption encryptor = new ORMappintItemEncryption("DefaultPropertyEncryptor");
            var result = encryptor.EncryptString(originalData.ToString()).ToBase16String();
            return result;
        }

        public static void SaveBulkForList<T>(this IEnumerable<T> list, string connectionNameConfig, int batchSize, params string[] igrozieColumns)
        {
            DataTable dt = ConvertToDataTable(list, igrozieColumns);
            dt.SaveBulkForList(connectionNameConfig, batchSize, igrozieColumns);
        }

        public static void SaveBulkForList(this DataTable dt, string connectionNameConfig, int batchSize, params string[] igrozieColumns)
        {
            string connectionname = DbConnectionManager.GetConnectionString(connectionNameConfig);
            if (batchSize < 1)
                batchSize = dt.Rows.Count;
            SqlBulkCopy cp = new SqlBulkCopy(connectionname, SqlBulkCopyOptions.UseInternalTransaction) { BatchSize = batchSize, DestinationTableName = dt.TableName };
            dt.Columns.ForEach<DataColumn>(c =>
                                               {
                                                   if (igrozieColumns.Contains(c.ColumnName) == false)
                                                       cp.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                                               });

            cp.WriteToServer(dt, DataRowState.Added);

            cp.Close();
        }

        public static void BatchUpdate(this DataTable dt, string connectionName, Action<List<string>> whereColunmAction, Action<List<string>> updateColumnAction, int batchSize = 1000)
        {
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

            string connectionnString = DbConnectionManager.GetConnectionString(connectionName);
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = null;
                adapter.UpdateCommand = ReadUpdateSql(dt.TableName, whereColunmAction, updateColumnAction);
                adapter.UpdateBatchSize = batchSize;
                adapter.AcceptChangesDuringUpdate = true;
                adapter.ContinueUpdateOnError = true;
                using (SqlConnection conon = new SqlConnection(connectionnString))
                {
                    adapter.UpdateCommand.Connection = conon;
                    SqlTransaction tran = null;
                    try
                    {
                        conon.Open();
                        tran = conon.BeginTransaction();
                        adapter.UpdateCommand.Transaction = tran;
                        adapter.Update(dt);
                        tran.Commit();
                    }
                    catch
                    {
                        if (tran != null)
                            tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conon.Close();
                    }
                }
            }
        }

        private static SqlCommand ReadUpdateSql(string table, Action<List<string>> whereColunmAction, Action<List<string>> updateColumnAction)
        {
            SqlCommand cmd = new SqlCommand();
            List<string> whereColunms = new List<string>();
            whereColunmAction(whereColunms);
            List<string> updateColumns = new List<string>();
            updateColumnAction(updateColumns);
            WhereSqlClauseBuilder where = new WhereSqlClauseBuilder();
            whereColunms.ForEach(w =>
                                     {
                                         var p = AddParameter(cmd, w);
                                         where.AppendItem(w, p.ParameterName, "=", true);
                                     });
            UpdateSqlClauseBuilder update = new UpdateSqlClauseBuilder();
            updateColumns.ForEach(u =>
                                      {
                                          var p = AddParameter(cmd, u);
                                          update.AppendItem(u, p.ParameterName, "=", true);
                                      }
                );
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", table,
                                       update.ToSqlString(TSqlBuilder.Instance),
                                       where.ToSqlString(TSqlBuilder.Instance));
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.UpdatedRowSource = UpdateRowSource.None;

            return cmd;
        }

        private static SqlParameter AddParameter(SqlCommand cmd, string u)
        {
            var p = new SqlParameter
                        {
                            Direction = ParameterDirection.Input,
                            ParameterName = "@" + u,
                            SourceColumn = u
                        };
            cmd.Parameters.Add(p);
            return p;
        }

        /// <summary>
        /// 获取SQL字串委托
        /// </summary>
        /// <param name="data">传入实体对象</param>
        /// <param name="mappingType">映射类型</param>
        /// <param name="isFilterByT"></param>
        /// <returns>SQL字串</returns>
        public delegate string GetSqlStrAction<in T>(T data, Type mappingType, bool isFilterByT);

        /// <summary>
        /// 创建一个实体对象的Insert语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <param name="mappingType">映射类型</param>
        /// <param name="isFilterByT">是否按类型进行映射过滤</param>
        /// <returns>Insert语句</returns>
        public static string GetInsertSql<T>(T data, Type mappingType, bool isFilterByT)
        {
            //获取映射关系集合
            ORMappingItemCollection mappings = ORMapping.GetMappingInfo(mappingType);
            //过滤
            if (isFilterByT)
                mappings = mappings.FilterMappingInfoByDeclaringType(mappingType);

            return String.Format("{0} {1} ",
                                  ORMapping.GetInsertSql(data, mappings, TSqlBuilder.Instance),
                                  TSqlBuilder.Instance.DBStatementSeperator);
        }

        /// <summary>
        /// 创建一个对象的Insert语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <returns>Insert语句</returns>
        public static string GetInsertSql<T>(T data)
        {
            //默认进行类型过滤
            return GetInsertSql(data, typeof(T), false);
        }

        public static string GetDeleteSql<T>(T data, Type mappingType, bool isFilterByT)
        {
            //获取映射关系集合
            ORMappingItemCollection mappings = ORMapping.GetMappingInfo(mappingType);
            //过滤
            if (isFilterByT)
                mappings = mappings.FilterMappingInfoByDeclaringType(mappingType);

            //创建Where子句表达式
            WhereSqlClauseBuilder builder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(data, mappings);
            ExceptionHelper.FalseThrow(builder.Count > 0, "必须为对象{0}指定关键字", typeof(T));

            return String.Format(" DELETE FROM {0} WHERE {1} {2} ",
                                  mappings.TableName,
                                  builder.ToSqlString(TSqlBuilder.Instance),
                                  TSqlBuilder.Instance.DBStatementSeperator);
        }

        /// <summary>
        /// 创建一个对象的Delete语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <returns>Delete语句</returns>
        public static string GetDeleteSql<T>(T data)
        {
            return GetDeleteSql(data, typeof(T), false);
        }

        /// <summary>
        /// 创建实体对象的Update语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">实体对象</param>
        /// <param name="mappingType">映射类型</param>
        /// <param name="isFilterByT">是否按实体类型进行过滤</param>
        /// <returns>Update语句</returns>
        public static string GetUpdateSql<T>(T data, Type mappingType, bool isFilterByT)
        {
            //获取映射关系集合
            ORMappingItemCollection mappings = ORMapping.GetMappingInfo(mappingType);
            //过滤
            if (isFilterByT)
                mappings = mappings.FilterMappingInfoByDeclaringType(mappingType);

            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append(ORMapping.GetUpdateSql(data, mappings, TSqlBuilder.Instance));
            sqlStr.Append(TSqlBuilder.Instance.DBStatementSeperator);
            sqlStr.Append(String.Format(" IF (@@ROWCOUNT = 0) BEGIN {0} END ",
                          ORMapping.GetInsertSql(data, mappings, TSqlBuilder.Instance)));
            sqlStr.Append(TSqlBuilder.Instance.DBStatementSeperator);

            return sqlStr.ToString();
        }

        /// <summary>
        /// 创建一个对象的Update语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <returns>Update语句</returns>
        public static string GetUpdateSql<T>(T data)
        {
            if (data == null)
                return String.Empty;

            return GetUpdateSql(data, typeof(T), false);
        }

        /// <summary>
        /// 创建逻辑删除语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <returns>逻辑删除语句</returns>
        public static string GetLogicDeleteSql<T>(T data)
        {
            ORMappingItemCollection mappings = ORMapping.GetMappingInfo<T>();

            //创建Where子句表达式
            WhereSqlClauseBuilder builder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(data, mappings);
            ExceptionHelper.FalseThrow(builder.Count > 0, "必须为对象{0}指定关键字", typeof(T));

            return String.Format(" UPDATE {0} SET ValidStatus=0 WHERE {1} {2}",
                                  mappings.TableName,
                                  builder.ToSqlString(TSqlBuilder.Instance),
                                  TSqlBuilder.Instance.DBStatementSeperator);
        }

        /// <summary>
        /// 创建删除子表记录的语句
        /// </summary>
        /// <param name="childTblName">子表名称</param>
        /// <param name="parentKeyField">父键字段</param>
        /// <param name="parentKeyValue">父键值</param>
        /// <returns>删除语句</returns>
        public static string GetChildDeleteSql(string childTblName, string parentKeyField, string parentKeyValue)
        {
            return String.Format(" DELETE FROM {0} WHERE {1}=N'{2}' {3}",
                                  childTblName,
                                  parentKeyField,
                                  parentKeyValue,
                                  TSqlBuilder.Instance.DBStatementSeperator);
        }

        /// <summary>
        /// 创建逻辑删除子表记录的语句
        /// </summary>
        /// <param name="childTblName">子表名称</param>
        /// <param name="parentKeyField">父键字段</param>
        /// <param name="parentKeyValue">父键值</param>
        /// <returns>删除语句</returns>
        public static string GetChildLogicDeleteSql(string childTblName, string parentKeyField, string parentKeyValue)
        {
            return String.Format(" UPDATE {0} SET ValidStatus=0 WHERE {1}=N'{2}' {3}",
                                  childTblName,
                                  parentKeyField,
                                  parentKeyValue,
                                  TSqlBuilder.Instance.DBStatementSeperator);
        }

        /// <summary>
        /// 获取子类的SQL语句
        /// 注意：仅能正确获取基类继承连续带TableMapping特性的子类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <param name="action">生成SQL字串的方法</param>
        /// <returns>返回SQL字串</returns>
        public static string GetSubClassSql<T>(T data, GetSqlStrAction<T> action)
        {
            StringBuilder sqlStr = new StringBuilder();

            //遍历类层次，通过TableName特性来判决是否要创建类的SQL语句
            Type baseType = typeof(T);
            List<SubClassSqlStrHelper> subClassList = new List<SubClassSqlStrHelper>();

            while (baseType != typeof(object))
            {
                //如果不存在TableMapping特性则中断遍历，并将上一个基类的过滤属性设为false
                if (baseType != null)
                {
                    var tableMapping = (ORTableMappingAttribute)Attribute.GetCustomAttribute(baseType, typeof(ORTableMappingAttribute), true);
                    if (tableMapping == null)
                    {
                        if (subClassList.Count > 0)
                            subClassList[subClassList.Count - 1].IsFilterByType = false;
                        break;
                    }
                }

                //保留本次遍历信息到List中
                subClassList.Add(new SubClassSqlStrHelper { ClassType = baseType, IsFilterByType = true });

                if (baseType != null) baseType = baseType.BaseType;
            }

            //添加SQL语句
            foreach (SubClassSqlStrHelper sqlHelper in subClassList)
            {
                sqlStr.Append(action(data, sqlHelper.ClassType, sqlHelper.IsFilterByType));
            }

            return sqlStr.ToString();
        }

        /// <summary>
        /// 获取子类的Insert语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <returns>Update语句</returns>
        public static string GetSubClassInsertSql<T>(T data)
        {
            return GetSubClassSql(data, GetInsertSql);
        }

        /// <summary>
        /// 获取子类的Update语句
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">传入的对象</param>
        /// <returns>Update语句</returns>
        public static string GetSubClassUpdateSql<T>(T data)
        {
            return GetSubClassSql(data, GetUpdateSql);
        }

        /// <summary>
        /// 获取子类的删除语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">实体对象</param>
        /// <returns>SQL语句</returns>
        public static string GetSubClassDeleteSql<T>(T data)
        {
            return GetSubClassSql(data, GetDeleteSql);
        }

        /// <summary>
        /// 生成子表与父表联接查询语句
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="whereAction">Where条件</param>
        /// <param name="orderByAction">OrderBy条件</param>
        /// <returns>SQL语句</returns>
        public static string GetSubClassSelectSql(Type type, Action<WhereSqlClauseBuilder> whereAction = null, Action<OrderBySqlClauseBuilder> orderByAction = null)
        {
            StringBuilder sqlStr = new StringBuilder();

            //生成连接查询语句
            ORMappingItemCollection mappings = ORMapping.GetMappingInfo(type);
            ORMappingItem primaryItem = mappings.Find(p => p.PrimaryKey);
            sqlStr.Append(String.Format(" SELECT * FROM {0} ", mappings.TableName));

            //遍历父类，找出父表，生成SELECT语句
            Type baseType = type.BaseType;
            while (baseType != typeof(object))
            {
                //如果不存在TableMapping特性则跳过
                if (baseType != null)
                {
                    var tableMapping = (ORTableMappingAttribute)Attribute.GetCustomAttribute(baseType, typeof(ORTableMappingAttribute), true);
                    if (tableMapping == null)
                    {
                        baseType = baseType.BaseType;
                        continue;
                    }

                    //创建内联接查询语句
                    sqlStr.Append(String.Format(" INNER JOIN {0} ON {1}.{2} = {0}.{2} ", tableMapping.TableName, mappings.TableName, primaryItem.DataFieldName));
                }
                if (baseType != null) baseType = baseType.BaseType;
            }

            //Where子句处理
            if (whereAction != null)
            {
                //生成Where子句
                WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
                whereAction(builder);
                //将Where子句添加到SQL字串
                if (builder.Count > 0)
                    sqlStr.Append(String.Format(" WHERE {0} ", builder.ToSqlString(TSqlBuilder.Instance)));
            }

            //OrderBy子句处理
            if (orderByAction != null)
            {
                //生成OrderBy子句
                OrderBySqlClauseBuilder orderByBuilder = new OrderBySqlClauseBuilder();
                orderByAction(orderByBuilder);
                //将OrderBy子句添加到SQL字串
                if (orderByBuilder.Count > 0)
                    sqlStr.Append(String.Format(" ORDER BY {0} ", orderByBuilder.ToSqlString(TSqlBuilder.Instance)));
            }

            return sqlStr.ToString();
        }

        #region--sql相关

        /// <summary>
        /// 获得相关查询语句
        /// </summary>
        /// <param name="qc"></param>
        /// <returns></returns>
        public static string GetSelectStr(QueryCondition qc)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("SELECT {0} FROM  {1}", String.IsNullOrWhiteSpace(qc.SelectFields) ? "*" : qc.SelectFields,
                                 qc.FromClause);
            if (String.IsNullOrWhiteSpace(qc.WhereClause) == false)
            {
                builder.AppendFormat(" WHERE {0}", qc.WhereClause);
            }
            if (String.IsNullOrWhiteSpace(qc.GroupBy) == false)
            {
                builder.AppendFormat(" Group By {0}", qc.GroupBy);
            }
            if (String.IsNullOrWhiteSpace(qc.OrderByClause) == false)
            {
                builder.AppendFormat(" Order By {0}", qc.OrderByClause);
            }
            builder.Append(TSqlBuilder.Instance.DBStatementSeperator);
            return builder.ToString();
        }

        /// <summary>
        /// 从实体获取必须属性之外的属性 为了使调用平台ORMapping相关生成Sql的方便
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requriedpropertites"></param>
        /// <returns></returns>
        public static string[] GetExceptProperties<T>(string[] requriedpropertites)
        {
            requriedpropertites.NullCheck("需要的属性不能为空");
            if (requriedpropertites.Length < 1)
                throw new Exception("需要的属性不能为空");
            //获得类型的所有属性名称
            var properties = typeof(T).GetProperties().Select(p => p.Name);
            //获取要忽略的属性
            return properties.Except(requriedpropertites).ToArray();
        }

        /// <summary>
        /// 更新实体上部分字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="requriedpropertites"></param>
        /// <returns></returns>
        public static string GetUpdateSql<T>(T graph, params string[] requriedpropertites)
        {
            return ORMapping.GetUpdateSql(graph, TSqlBuilder.Instance, GetExceptProperties<T>(requriedpropertites));
        }

        /// <summary>
        /// 插入实体上部分必要的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="requriedpropertites"></param>
        /// <returns></returns>
        public static string GetInsertSql<T>(T graph, params string[] requriedpropertites)
        {
            return ORMapping.GetInsertSql(graph, TSqlBuilder.Instance, GetExceptProperties<T>(requriedpropertites));
        }

        /// <summary>
        /// 获取被删除实体的产生的Sql，条件是所有主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string GetDeleteByPK<T>(T graph)
        {
            return GetDeleteByWhere<T>(ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(graph).ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 由实体必须的非主键字段产生AND连接Where子句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="requriedpropertites"></param>
        /// <returns></returns>
        public static string GetWhereSql<T>(T graph, params string[] requriedpropertites)
        {
            return GetWhereSqlClauseBuilder(graph, requriedpropertites).ToSqlString(TSqlBuilder.Instance);
        }

        public static WhereSqlClauseBuilder GetWhereSqlClauseBuilder<T>(T graph, params string[] requriedpropertites)
        {
            return ORMapping.GetWhereSqlClauseBuilder(graph, GetExceptProperties<T>(requriedpropertites));
        }

        /// <summary>
        /// 实体必须的非主键字段产生AND连条件的删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="requriedpropertites"></param>
        /// <returns></returns>
        public static string GetDeleteByWhereNoPK<T>(T graph, params string[] requriedpropertites)
        {
            return GetDeleteByWhere<T>(GetWhereSql(graph, requriedpropertites));
        }

        /// <summary>
        /// 按某条件产生的Sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string GetDeleteByWhere<T>(string where)
        {
            var sql = new StringBuilder();
            sql.AppendFormat("DELETE FROM {0} WHERE {1}", ORMapping.GetMappingInfo<T>().TableName, @where);
            return sql.ToString();
        }

        /// <summary>
        /// 按某条件产生的Sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereAction"></param>
        /// <returns></returns>
        public static string GetDeleteByWhere<T>(Action<WhereSqlClauseBuilder> whereAction)
        {
            var builder = new WhereSqlClauseBuilder();
            whereAction(builder);
            return GetDeleteByWhere<T>(builder.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 批量更新某些字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="where"></param>
        /// <param name="requriedpropertites"></param>
        /// <returns></returns>
        public static string BathUpdate<T>(T graph, string where, params string[] requriedpropertites)
        {
            var mapping = ORMapping.GetMappingInfo<T>();
            UpdateSqlClauseBuilder updateBuilder = ORMapping.GetUpdateSqlClauseBuilder(graph, mapping, GetExceptProperties<T>(requriedpropertites));
            return String.Format("UPDATE {0} SET {1} WHERE {2}",
                                 mapping.TableName,
                                 updateBuilder.ToSqlString(TSqlBuilder.Instance),
                                 @where);
        }

        /// <summary>
        /// 批量更新某些字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string BathUpdate<T>(string where, Action<UpdateSqlClauseBuilder> action)
        {
            var mapping = ORMapping.GetMappingInfo<T>();
            var updateBuilder = new UpdateSqlClauseBuilder();
            action(updateBuilder);

            return String.Format("UPDATE {0} SET {1} WHERE {2}",
                                 mapping.TableName,
                                 updateBuilder.ToSqlString(TSqlBuilder.Instance),
                                 @where);
        }

        /// <summary>
        /// 批量更新某些字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string BathUpdate<T>(IConnectiveSqlClause where, Action<UpdateSqlClauseBuilder> action)
        {
            var updateBuilder = new UpdateSqlClauseBuilder();
            action(updateBuilder);
            return String.Format("UPDATE {0} SET {1} WHERE {2}",
                                 ORMapping.GetMappingInfo<T>().TableName,
                                 updateBuilder.ToSqlString(TSqlBuilder.Instance),
                                 where.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="property"></param>
        /// <param name="alsName">别名</param>
        /// <returns></returns>
        public static string GetDataField(this ORMappingItemCollection coll, string property, string alsName = null)
        {
            var attr = coll.Find(m => m.PropertyName == property);
            if (attr == null)
                return string.Empty;
            if (string.IsNullOrWhiteSpace(alsName))
                alsName = coll.TableName;
            return string.Format("{0}.{1}", alsName, attr.DataFieldName);
        }

        public static string GetORMMapinngField(this ORMappingItemCollection coll, string property)
        {
            var attr = coll.Find(m => m.PropertyName == property);
            if (attr == null)
                return string.Empty;
            return attr.DataFieldName;
        }

        public static string GetDataField<T>(string property)
        {
            return ORMapping.GetMappingInfo<T>().GetDataField(property);
        }

        ///// <summary>
        ///// 获取关系条件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="con"></param>
        ///// <param name="p"></param>
        ///// <param name="l2R"></param>
        //public static void GetRelationWhere<T>(this ConnectiveSqlClauseCollection con, string p, bool l2R = false)
        //{
        //    var lmpp = ORMapping.GetMappingInfo<T>();
        //    var cpp = ConditionMapping.GetMappingInfo(typeof(T));
        //    var lattr = cpp.Find(c => c.PropertyName == p);
        //    if (lattr == null)
        //        return;
        //    var rmpp = ORMapping.GetMappingInfo(lattr.MemberInfo.ReflectedType);
        //    string rightf = rmpp.GetDataField(lattr.SubClassPropertyName);
        //    string leftf = string.Format("{0}.{1}", lmpp.TableName, lattr.DataFieldName);
        //    var builder = new WhereSqlClauseBuilder();
        //    if (l2R == false)
        //        builder.AppendItem(leftf, rightf, "=", true);
        //    else
        //        builder.AppendItem(rightf, leftf, "=", true);
        //    con.Add(builder);
        //}

        #endregion

        /// <summary>
        /// 执行查询语句返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="qc"></param>
        /// <param name="databaseConnect"></param>
        /// <returns></returns>
        public static TCollection GetData<T, TCollection>(this QueryCondition qc, string databaseConnect)
            where TCollection : EditableDataObjectCollectionBase<T>, new()
            where T : new()
        {
            string sql = GetSelectStr(qc);
            var dt = DbHelper.RunSqlReturnDS(sql, databaseConnect).Tables[0];
            var result = new TCollection();
            ORMapping.DataViewToCollection(result, dt.DefaultView);
            return result;
        }
    }

    public enum JoinMode
    {
        [EnumItemDescription("INNER JOIN {0} ON {1}.{2} = {3}.{4}")]
        InnerJoin = 0,
        [EnumItemDescription("LEFT OUTER JOIN {0} ON {1}.{2} = {3}.{4}")]
        LeftOutJoin = 1
    }

    /// <summary>
    /// 子类SQL帮助
    /// </summary>
    internal class SubClassSqlStrHelper
    {
        public Type ClassType { get; set; }

        public bool IsFilterByType { get; set; }
    }

    public class JoinBuilder
    {
        /// <summary>
        /// 左边第一个出现的表
        /// </summary>
        private string LeftFirstTable { get; set; }

        /// <summary>
        /// 左边第一个出现的表的别名
        /// </summary>
        private string LeftFirstTableAlsa { get; set; }

        private Dictionary<int, string> JoinItems { get; set; }

        public JoinBuilder(Type t, string leftFirstTableAlsa = null)
        {
            LeftFirstTableAlsa = leftFirstTableAlsa;
            LeftFirstTable = ORMapping.GetMappingInfo(t).TableName;
            JoinItems = new Dictionary<int, string>();
        }

        /// <summary>
        /// 通过ORMFieldMaping属性
        /// </summary>
        /// <typeparam name="TLt"></typeparam>
        /// <typeparam name="TRt"></typeparam>
        /// <param name="lp"></param>
        /// <param name="rp"></param>
        /// <param name="lals"></param>
        /// <param name="rals"></param>
        /// <returns></returns>
        public JoinBuilder InnerJoin<TLt, TRt>(string lp, string rp, string lals = null, string rals = null)
        {
            string sql = GetJoinCamal<TLt, TRt>(lp, rp, lals, rals);
            JoinItems.Add(JoinItems.Count + 1, sql);
            return this;
        }

        public JoinBuilder LeftOuterJoin<TLt, TRt>(string lp, string rp, string lals = null, string rals = null, Action<ConnectiveSqlClauseCollection> addCondtion = null)
        {
            string sql = GetJoinCamal<TLt, TRt>(lp, rp, lals, rals, JoinMode.LeftOutJoin);
            var connective = new ConnectiveSqlClauseCollection();
            if (addCondtion != null)
            {
                addCondtion(connective);
            }
            if (connective.Count > 0)
            {
                sql = string.Format(" {0} AND {1} ", sql, connective.ToSqlString(TSqlBuilder.Instance));
            }
            JoinItems.Add(JoinItems.Count + 1, sql);
            return this;
        }

        /// <summary>
        ///  通过JoinMappingCondtionAttribute属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="l2R"></param>
        /// <param name="lals"></param>
        /// <param name="rals"></param>
        /// <param name="joinMode"></param>
        /// <returns></returns>
        public JoinBuilder InnerJoin<T>(string property, bool l2R = false, string lals = null, string rals = null, JoinMode joinMode = JoinMode.InnerJoin)
        {
            Type t = typeof(T);
            var lomapp = ORMapping.GetMappingInfo(t);
            var p = t.GetProperty(property);
            if (p == null)
                return this;
            var lattr = p.GetCustomAttributes(typeof(JoinMappingCondtionAttribute), true).FirstOrDefault() as JoinMappingCondtionAttribute;
            if (lattr == null)
                return this;
            string sql = l2R == false ? JoinCamal(lals, rals, joinMode, lattr.SubTableField, lomapp.TableName, lattr.DataFieldName, lattr.SubClassMappingTable)
                : JoinCamal(rals, lals, joinMode, lattr.DataFieldName, lattr.SubClassMappingTable, lattr.SubTableField, lomapp.TableName);
            JoinItems.Add(JoinItems.Count + 1, sql);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lals"></param>
        /// <param name="rals"></param>
        /// <param name="mode"></param>
        /// <param name="rfield"></param>
        ///<param name="ltable"></param>
        ///<param name="lfield"></param>
        ///<param name="rtable"></param>
        ///<returns></returns>
        private string JoinCamal(string lals, string rals, JoinMode mode, string rfield, string ltable,
                                 string lfield, string rtable)
        {
            if (lals.IsNullOrWhiteSpace())
            {
                lals = ltable;
            }

            if (rals.IsNullOrWhiteSpace())
            {
                rals = rtable;
            }
            else
            {
                rtable += " AS " + rals;
            }
            string sqlformat = EnumItemDescriptionAttribute.GetDescription(mode);
            return string.Format(sqlformat, rtable, rals, rfield, lals, lfield);
        }

        /// <summary>
        /// T1 是子类
        /// </summary>
        /// <typeparam name="TLt"></typeparam>
        /// <typeparam name="TRt"></typeparam>
        /// <param name="lp"></param>
        /// <param name="rp"></param>
        /// <param name="lals"></param>
        /// <param name="rals"></param>
        /// <param name="mode"></param>
        /// <returns>获得连接sql</returns>
        private string GetJoinCamal<TLt, TRt>(string lp,
            string rp,
            string lals = null,
            string rals = null,
            JoinMode mode = JoinMode.InnerJoin)
        {
            Type lt = typeof(TLt);
            Type rt = typeof(TRt);
            var lmapp = ORMapping.GetMappingInfo(lt);
            var lattr = lmapp.Find(m => m.PropertyName == lp);
            if (lattr == null)
                return string.Empty;
            var rmapp = ORMapping.GetMappingInfo(rt);
            var rattr = rmapp.Find(m => m.PropertyName == rp);
            if (rattr == null)
                return string.Empty;
            string lfield = lattr.DataFieldName;
            string rfield = rattr.DataFieldName;
            return JoinCamal(lals, rals, mode, rfield, lmapp.TableName, lfield, rmapp.TableName);
        }

        public string ToSqlString()
        {
            var builder = new StringBuilder();
            builder.Append(LeftFirstTableAlsa.IsNullOrWhiteSpace() ? LeftFirstTable : string.Format(
                " {0} AS {1} ", LeftFirstTable, LeftFirstTableAlsa));
            foreach (int k in JoinItems.Keys)
            {
                builder.Append("  ");
                builder.Append(JoinItems[k]);
            }
            return builder.ToString();
        }
    }

    /// <summary>
    /// 执行join连接的条件
    /// </summary>
    public class JoinMappingCondtionAttribute : Attribute
    {
        private string _subClassMappingTable;
        private string _subTableField;
        private string _dataFieldName;

        public JoinMappingCondtionAttribute(string subClassMappingTable, string subTableField, string fieldName)
        {
            _subClassMappingTable = subClassMappingTable;
            _subTableField = subTableField;
            _dataFieldName = fieldName;
        }

        public string SubClassMappingTable
        {
            get { return _subClassMappingTable; }
        }

        public string SubTableField
        {
            get { return _subTableField; }
        }

        public string DataFieldName
        {
            get { return _dataFieldName; }
        }

        public static WhereSqlClauseBuilder GetWhereBuilder<T>(string property, bool l2R = false, string las = null, string ras = null)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();
            Type t = typeof(T);
            var lomapp = ORMapping.GetMappingInfo(t);
            var p = t.GetProperty(property);
            if (p == null)
                return builder;
            var lattr = p.GetCustomAttributes(typeof(JoinMappingCondtionAttribute), true).FirstOrDefault() as JoinMappingCondtionAttribute;
            if (lattr == null)
                return builder;
            string lf = string.Format("{0}.{1}", las.IsNullOrWhiteSpace() ? lomapp.TableName : las, lattr.DataFieldName);
            string rf = string.Format("{0}.{1}", ras.IsNullOrWhiteSpace() ? lattr._subClassMappingTable : ras, lattr._subTableField);
            if (l2R)
            {
                builder.AppendItem(rf, lf, "=", true);
            }
            else
            {
                builder.AppendItem(lf, rf, "=", true);
            }

            return builder;
        }
    }

}