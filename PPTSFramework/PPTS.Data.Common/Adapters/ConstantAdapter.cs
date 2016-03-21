using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    public class ConstantAdapter : UpdatableAndLoadableAdapterBase<ConstantEntity, ConstantEntityCollection>
    {
        public static ConstantAdapter Instance
        {
            get
            {
                return ConstantAdapter.GetAdapter(ConnectionDefine.PPTSMetaDataConnectionName);
            }
        }

        #region Adapter Key
        private class AdapterKey
        {
            public string ConnectionName
            {
                get;
                set;
            }

            public string TableName
            {
                get;
                set;
            }

            public override int GetHashCode()
            {
                string connName = this.ConnectionName ?? string.Empty;
                string tableName = this.TableName ?? string.Empty;

                return (connName + "~" + tableName).GetHashCode();
            }
        }
        #endregion Adapter Key

        private readonly string _ConnectionName = string.Empty;
        private readonly string _TableName = string.Empty;

        private static readonly Dictionary<AdapterKey, ConstantAdapter> _AdapterMap = new Dictionary<AdapterKey, ConstantAdapter>();

        public static ConstantAdapter GetAdapter(string connectionName)
        {
            return GetAdapter(connectionName, string.Empty);
        }

        public static ConstantAdapter GetAdapter(string connectionName, string tableName)
        {
            connectionName.CheckStringIsNullOrEmpty("connectionName");

            AdapterKey aKey = new AdapterKey() { ConnectionName = connectionName, TableName = tableName };

            ConstantAdapter result = null;

            lock (_AdapterMap)
            {
                if (_AdapterMap.TryGetValue(aKey, out result) == false)
                {
                    result = new ConstantAdapter(connectionName, tableName);
                    _AdapterMap.Add(aKey, result);
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存中获取常量
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConstantEntity Get(string category, string key)
        {
            category.CheckStringIsNullOrEmpty("category");
            key.CheckStringIsNullOrEmpty("key");

            return this.GetByCategory(category, false)[key];
        }

        /// <summary>
        /// 读取某个类别和某个Key的常量
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConstantEntity Load(string category, string key)
        {
            category.CheckStringIsNullOrEmpty("category");
            key.CheckStringIsNullOrEmpty("key");

            return this.LoadByCategory(category, false, "-")[key];
        }

        /// <summary>
        /// 从缓存中读取某个类别的常量
        /// </summary>
        /// <param name="category">类别</param>
        /// <param name="addEmptyItem">是否增加空项（全部）</param>
        /// <returns></returns>
        public ConstantEntityInCategoryCollection GetByCategory(string category, bool addEmptyItem)
        {
            return this.GetByCategory(category, addEmptyItem, "全部");
        }

        /// <summary>
        /// 根据一组类别名称返回简单的字典集合
        /// </summary>
        /// <param name="categories">类别的集合</param>
        /// <returns></returns>
        public Dictionary<string, IEnumerable<BaseConstantEntity>> GetSimpleEntitiesByCategories(params string[] categories)
        {
            Dictionary<string, IEnumerable<BaseConstantEntity>> result = new Dictionary<string, IEnumerable<BaseConstantEntity>>();

            if (categories != null)
            {
                foreach (string category in categories)
                    result[category] = this.GetByCategory(category, false).ToSimpleEntity();
            }

            return result;
        }

        /// <summary>
        /// 根据一组类别名称类型上顶一个类别返回简单的字典集合
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public Dictionary<string, IEnumerable<BaseConstantEntity>> GetSimpleEntitiesByCategories(params Type[] types)
        {
            Dictionary<string, IEnumerable<BaseConstantEntity>> result = null;

            if (types != null)
            {
                string[] categories = ConstantCategoryAttribute.GetConstantCategories(types);

                result = this.GetSimpleEntitiesByCategories(categories);
            }
            else
                result = new Dictionary<string, IEnumerable<BaseConstantEntity>>();

            return result;
        }

        public ConstantEntityInCategoryCollection GetByCategory(string category, bool addEmptyItem, string emptyText)
        {
            category.CheckStringIsNullOrEmpty("category");

            string cacheKey = CalculateCacheKey(category, addEmptyItem);

            return ConstantsCache.Instance.GetOrAddNewValue(cacheKey, (cache, innerKey) =>
            {
                ConstantEntityInCategoryCollection result = this.LoadByCategory(category, addEmptyItem, emptyText);

                MixedDependency dependency = new MixedDependency(
                    new UdpNotifierCacheDependency(),
                    new MemoryMappedFileNotifierCacheDependency(),
                    new AbsoluteTimeDependency(DateTime.Now.AddMinutes(10)));

                cache.Add(cacheKey, result, dependency);

                return result;
            });
        }

        public ConstantEntityInCategoryCollection LoadByCategory(string category, bool addEmptyItem, string emptyText)
        {
            category.CheckStringIsNullOrEmpty("category");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("Category", category);

            string sql = string.Format("SELECT * FROM {0} WHERE {1} ORDER BY SortNo",
                this.GetTableName(),
                builder.ToSqlString(TSqlBuilder.Instance));

            DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

            ConstantEntityInCategoryCollection result = new ConstantEntityInCategoryCollection();

            if (addEmptyItem)
                result.Add(new ConstantEntity { Category = category, Key = string.Empty, Value = emptyText, SortNo = -1 });

            ORMapping.DataViewToCollection(result, table.DefaultView);

            return result;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            CacheNotifyData notifyData = new CacheNotifyData(typeof(ConstantsCache), string.Empty, CacheNotifyType.Clear);
            SendCacheNotifyData(notifyData);
        }

        public void ClearCache(string category)
        {
            category.CheckStringIsNullOrEmpty("category");

            CacheNotifyData notifyData = new CacheNotifyData(typeof(ConstantsCache), category, CacheNotifyType.Invalid);
            SendCacheNotifyData(notifyData);
        }

        private static void SendCacheNotifyData(CacheNotifyData notifyData)
        {
            UdpCacheNotifier.Instance.SendNotifyAsync(notifyData);
            MmfCacheNotifier.Instance.SendNotify(notifyData);
        }

        private string CalculateCacheKey(string category, bool addEmptyItem)
        {
            return this.ConnectionName.ToLower() + "~" + this.GetTableName().ToLower() + "~" + category.ToLower() + "~" + addEmptyItem.ToString();
        }

        private ConstantAdapter(string connectionName)
        {
            connectionName.CheckStringIsNullOrEmpty("connectionName");

            this._ConnectionName = connectionName;
        }

        private ConstantAdapter(string connectionName, string tableName)
        {
            connectionName.CheckStringIsNullOrEmpty("connectionName");

            this._ConnectionName = connectionName;
            this._TableName = tableName;
        }

        protected override string GetConnectionName()
        {
            return this._ConnectionName;
        }

        protected override string GetTableName()
        {
            string result = this._TableName;

            if (result.IsNullOrEmpty())
                result = base.GetTableName();

            return result;
        }
    }
}
