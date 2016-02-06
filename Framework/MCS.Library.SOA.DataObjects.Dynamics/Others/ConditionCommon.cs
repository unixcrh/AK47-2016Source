using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;

namespace MCS.Library.SOA.DataObjects.Dynamics
{
    public static class CondtionCommon
    {
        /// <summary>
        /// 角色矩阵中别名
        /// </summary>
        public const string UnitAlias = "UnitAlias";

        public const string Colon = ":";

        public const string Underline = "_";

       
    

        /// <summary>
        /// 生成版本时间段控制的条件够早期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectiveSqlClauses"></param>
        /// <param name="timstamp"></param>
        /// <param name="tableAls"></param>
        public static void GetVersionTimeCondion<T>(this ConnectiveSqlClauseCollection connectiveSqlClauses, DateTime timstamp, string tableAls = null, Action<ConnectiveSqlClauseCollection> action = null)
        {
            connectiveSqlClauses.GetVersionTimeCondion<T>(timstamp, timstamp, tableAls, action);
        }

        /// <summary>
        /// 生成版本时间段控制的条件够早期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectiveSqlClauses"></param>
        /// <param name="timstamp"></param>
        /// <param name="tableAls"></param>
        public static void GetVersionTimeCondion<T>(this ConnectiveSqlClauseCollection connectiveSqlClauses, DateTime start, DateTime end, string tableAls = null, Action<ConnectiveSqlClauseCollection> action = null)
        {
            QueryCondtionEntityBase condtion = new QueryCondtionEntityBase
                                                   {
                                                       VersionEndTime = end,
                                                       VersionStartTime = start,
                                                       ValidStatus = true
                                                   };
            if (tableAls.IsNullOrWhiteSpace())
                tableAls = ORMapping.GetMappingInfo<T>().TableName;
            connectiveSqlClauses.GetCondition(condtion, tableAls);
            if (action != null)
                action(connectiveSqlClauses);
        }

        

        public static void GetVersionTimeAndEffectionSECondion<T>(this ConnectiveSqlClauseCollection connectiveSqlClauses, string dateTimeField, string tableAls = null)
        {
            var table = tableAls;
            if (string.IsNullOrWhiteSpace(table))
                table = ORMapping.GetMappingInfo<T>().TableName;
            WhereSqlClauseBuilder where = new WhereSqlClauseBuilder();
            where.AppendItem(string.Format("{0}.VersionStartTime", table), dateTimeField, "<=", true);
            where.AppendItem(string.Format("{0}.VersionEndTime", table), dateTimeField, ">", "ISNULL(${DataField}$,'99990909 00:00:00.000') ${Operation}$ ${Data}$", true);
            where.AppendItem(string.Format("{0}.EffectStartTime", table), dateTimeField, "<=", true);
            where.AppendItem(string.Format("{0}.EffectEndTime", table), dateTimeField, ">", "ISNULL(${DataField}$,'99990909 00:00:00.000') ${Operation}$ ${Data}$", true);
            connectiveSqlClauses.Add(where);
        }

        public static void GetVersionTimeCondion<T>(this ConnectiveSqlClauseCollection connectiveSqlClauses, string dateTimeField, string tableAls = null)
        {
            var table = tableAls;
            if (string.IsNullOrWhiteSpace(table))
                table = ORMapping.GetMappingInfo<T>().TableName;
            WhereSqlClauseBuilder where = new WhereSqlClauseBuilder();
            where.AppendItem(string.Format("{0}.VersionStartTime", table), dateTimeField, "<=", true);
            where.AppendItem(string.Format("{0}.VersionEndTime", table), dateTimeField, ">", "ISNULL(${DataField}$,'99990909 00:00:00.000') ${Operation}$ ${Data}$", true);
            connectiveSqlClauses.Add(where);
        }

        
        /// <summary>
        /// 产生时间类型的
        /// </summary>
        /// <param name="connectiveSqlClauses"></param>
        /// <param name="condtion"></param>
        /// <param name="table"></param>
        public static WhereSqlClauseBuilder GetCondition(this ConnectiveSqlClauseCollection connectiveSqlClauses, object condtion, string table = null)
        {
            Type t = condtion.GetType();
            var builder = new WhereSqlClauseBuilder();
            var items = ConditionMapping.GetMappingInfo(t);

            if (table == null)
                table = ORMapping.GetMappingInfo(t).TableName;
            string tablename = string.Format("{0}.", table);
            foreach (ConditionMappingItem item in items)
            {
                if (string.IsNullOrWhiteSpace(item.SubClassPropertyName) == false)
                    continue;
                object data;
                bool b = condtion.IsTypeDefaultValue(item, out data);
                if (b)
                    continue;
                string field = string.Format(item.DataFieldName, tablename);
                if (field.Contains(tablename) == false)
                    field = string.Format("{0} {1}", tablename, item.DataFieldName);

                builder.AppendItem(field, data, item.Operation, item.Template, item.IsExpression);
            }
            connectiveSqlClauses.Add(builder);
            return builder;
        }

        /// <summary>
        /// 判断该映射字段对应的属性 的值是否是类型默认值
        /// </summary>
        /// <param name="condtion"></param>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsTypeDefaultValue(this object condtion, ConditionMappingItem item, out  object data)
        {
            data = null;
            var pi = item.MemberInfo as PropertyInfo;
            if (pi == null)
                return true;
            if (pi.CanRead == false)
                return true;
            data = pi.GetValue(condtion, null);
            if (data == null)
                return true;
            Type type = pi.PropertyType;
            if (type == typeof(object))
                type = data.GetType();
            else if (type.IsEnum)
            {
                int d = (int)data;
                data = d.ToString();
                if (d > 0)
                    return false;
                return true;
            }
            else if (type == typeof(DateTime))
            {
                DateTime dt = (DateTime)data;
                if (dt == DateTime.MinValue)
                    return true;
                return false;
            }
            else if (type == typeof(decimal))
            {
                decimal dci = (decimal)data;
                if (dci == decimal.MinValue)
                    return true;
                return false;
            }
            else if (type == typeof(int))
            {
                int intd = (int)data;
                if (intd == int.MinValue)
                    return true;
                return false;
            }
            else
                if (type == typeof(string))
                    return string.IsNullOrEmpty((string)data);
            return data.Equals(TypeCreator.GetTypeDefaultValue(type));
        }
    }
}