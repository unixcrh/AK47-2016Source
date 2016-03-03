using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 为Asp.net的ObjectDataSource对应的分页查询类所做的基类，返回的是通用的DataView类型，不需要通过ORMapping
    /// </summary>
    public abstract class DataViewDataSourceQueryAdapterBase
    {
        private string _DefaultTableName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DataViewDataSourceQueryAdapterBase()
        {
        }

        /// <summary>
        /// 构造方法，提供默认的数据表名。如果不提供，请重载OnBuildQueryCondition方法
        /// </summary>
        /// <param name="tableName"></param>
        public DataViewDataSourceQueryAdapterBase(string tableName)
        {
            tableName.CheckStringIsNullOrEmpty("tableName");

            this._DefaultTableName = tableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataView Query(int startRowIndex, int maximumRows, ref int totalCount)
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
        public DataView Query(int startRowIndex, int maximumRows, string orderBy, ref int totalCount)
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
        public DataView Query(int startRowIndex, int maximumRows, string where, string orderBy, ref int totalCount)
        {
            DataView result = InnerQuery(startRowIndex, maximumRows, where, orderBy, ref totalCount);

            OnAfterQuery(result);

            return result;
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

        private DataView InnerQuery(int startRowIndex, int maximumRows, string where, string orderBy, ref int totalCount)
        {
            QueryCondition qc = new QueryCondition(startRowIndex,
                maximumRows, "*", this._DefaultTableName, orderBy, where);

            OnBuildQueryCondition(qc);

            TSqlCommonAdapter adapter = new TSqlCommonAdapter(GetConnectionName());

            DataView result = adapter.SplitPageQuery(qc, ref totalCount);

            ObjectContextCache.Instance[ContextCacheKey] = totalCount;

            return result;
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
        protected virtual void OnAfterQuery(DataView result)
        {
        }

        /// <summary>
        /// 缓存QueryCount的Cache Key。默认是typeof(TCollection).Name + ".Query"
        /// </summary>
        protected virtual string ContextCacheKey
        {
            get
            {
                return "DataViewDataSourceQueryAdapterBase" + this.GetHashCode().ToString();
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
    }
}
