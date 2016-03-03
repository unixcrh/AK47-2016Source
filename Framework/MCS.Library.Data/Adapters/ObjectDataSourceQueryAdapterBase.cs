using MCS.Library.Caching;
using MCS.Library.Data.Adapters;
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
            return Query(startRowIndex, maximumRows, string.Empty, orderBy, ref totalCount);
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
            TCollection result = InnerQuery(startRowIndex, maximumRows, where, orderBy, ref totalCount);

            OnAfterQuery(result);

            return result;
        }

        private TCollection InnerQuery(int startRowIndex, int maximumRows, string where, string orderBy, ref int totalCount)
        {
            QueryCondition qc = new QueryCondition(startRowIndex,
                maximumRows, "*", GetMappingInfo().TableName, orderBy, where);

            OnBuildQueryCondition(qc);

            TSqlCommonAdapter adapter = new TSqlCommonAdapter(GetConnectionName());

            TCollection result = adapter.SplitPageQuery<T, TCollection>(qc, this.OnDataRowToObject, ref totalCount);

            ObjectContextCache.Instance[ContextCacheKey] = totalCount;

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

        //protected virtual string GetConnectionName()
        //{
        //    return ConnectionDefine.SearchConnectionName;
        //}

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
