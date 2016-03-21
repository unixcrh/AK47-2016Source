using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 为Asp.net的ObjectDataSource对应的分页查询类所做的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class ObjectDataSourceQueryAdapterBase<T, TCollection>
        where T : new()
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public TCollection Query(int startRowIndex, int maximumRows, ref int totalCount)
        {
            return Query(startRowIndex, maximumRows, string.Empty, string.Empty, ref totalCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public TCollection Query(int startRowIndex, int maximumRows, string orderBy, ref int totalCount)
        {
            return this.Query(startRowIndex, maximumRows, string.Empty, orderBy, ref totalCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public TCollection Query(int startRowIndex, int maximumRows, string where, string orderBy, ref int totalCount)
        {
            PagedQueryResult<T, TCollection> result = this.InnerQuery(startRowIndex, maximumRows, string.Empty, string.Empty, where, orderBy, totalCount);

            this.OnAfterQuery(result.PagedData);

            totalCount = result.TotalCount;

            return result.PagedData;
        }

        /// <summary>
        /// 根据页面参数，条件对象，排序条件构造分页查询。select默认使用*。from部分使用返回对象的ORTableMapping中TableName属性
        /// </summary>
        /// <param name="prp"></param>
        /// <param name="conditionObject"></param>
        /// <param name="orderByBuilder"></param>
        /// <returns></returns>
        public PagedQueryResult<T, TCollection> Query(IPageRequestParams prp, object conditionObject, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            return this.Query(prp, string.Empty, string.Empty, conditionObject, orderByBuilder);
        }

        /// <summary>
        /// 根据页面参数，select字句，from子句，条件对象，排序条件构造分页查询
        /// </summary>
        /// <param name="prp"></param>
        /// <param name="select">SQL语句的select部分。如果为空，则使用*</param>
        /// <param name="from">SQL语句的from部分。如果为空，则使用返回对象的ORTableMapping中TableName属性</param>
        /// <param name="conditionObject"></param>
        /// <param name="orderByBuilder"></param>
        /// <returns></returns>
        public PagedQueryResult<T, TCollection> Query(IPageRequestParams prp, string select, string from, object conditionObject, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            prp.NullCheck("prp");

            int totalCount = 0;
            string where = string.Empty;

            if (conditionObject != null)
            {
                IConnectiveSqlClause connectiveBuilder = ConditionMapping.GetConnectiveClauseBuilder(conditionObject);
                where = connectiveBuilder.ToSqlString(TSqlBuilder.Instance);
            }

            string orderBy = string.Empty;

            if (orderByBuilder != null)
            {
                OrderBySqlClauseBuilder wrappedBuilder = OrderBySqlClauseBuilder.CreateWrapperBuilder(orderByBuilder);
                orderBy = wrappedBuilder.ToSqlString(TSqlBuilder.Instance);
            }

            return this.InnerQuery(prp.ToRowIndex(), prp.PageSize, select, from, where, orderBy, totalCount);
        }

        /// <summary>
        /// 根据PageRequest来查询，返回PagedQueryResult
        /// </summary>
        /// <param name="prp"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public PagedQueryResult<T, TCollection> Query(IPageRequestParams prp, string where, string orderBy)
        {
            prp.NullCheck("prp");

            int totalCount = 0;

            TCollection list = Query(prp.ToRowIndex(), prp.PageSize, where, orderBy, ref totalCount);

            return this.InnerQuery(prp.ToRowIndex(), prp.PageSize, string.Empty, string.Empty, where, orderBy, totalCount);
        }

        private PagedQueryResult<T, TCollection> InnerQuery(int startRowIndex, int maximumRows, string select, string from, string where, string orderBy, int totalCount)
        {
            if (select.IsNullOrEmpty())
                select = "*";

            if (from.IsNullOrEmpty())
                from = GetMappingInfo().TableName;

            QueryCondition qc = new QueryCondition(startRowIndex,
                maximumRows, select, from, orderBy, where);

            OnBuildQueryCondition(qc);

            TSqlCommonAdapter adapter = new TSqlCommonAdapter(GetConnectionName());

            TCollection list = adapter.SplitPageQuery<T, TCollection>(qc, this.OnDataRowToObject, ref totalCount);

            ObjectContextCache.Instance[ContextCacheKey] = totalCount;

            PagedQueryResult<T, TCollection> result = new PagedQueryResult<T, TCollection>();

            result.PageIndex = (qc.RowIndex / maximumRows) + 1;
            result.PageSize = maximumRows;
            result.TotalCount = totalCount;
            result.PagedData = list;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataCollection"></param>
        /// <param name="row"></param>
        protected virtual void OnDataRowToObject(TCollection dataCollection, DataRow row)
        {
            T data = new T();

            ORMappingItemCollection mapping = GetMappingInfo();
            ORMapping.DataRowToObject(row, mapping, data);

            dataCollection.Add(data);
        }

        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <param name="qc">默认构造的查询条件</param>
        protected virtual void OnBuildQueryCondition(QueryCondition qc)
        {
        }

        /// <summary>
        /// 查询结束后，查询出的集合数据
        /// </summary>
        /// <param name="result"></param>
        protected virtual void OnAfterQuery(TCollection result)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public int GetQueryCount(ref int totalCount)
        {
            return (int)ObjectContextCache.Instance[ContextCacheKey];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public int GetQueryCount(string where, ref int totalCount)
        {
            return (int)ObjectContextCache.Instance[ContextCacheKey];
        }

        /// <summary>
        /// 缓存QueryCount的Cache Key。默认是typeof(TCollection).Name + ".Query"
        /// </summary>
        protected virtual string ContextCacheKey
        {
            get
            {
                return typeof(TCollection).Name + ".Query";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetConnectionName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual ORMappingItemCollection GetMappingInfo()
        {
            return ORMapping.GetMappingInfo<T>();
        }
    }
}
