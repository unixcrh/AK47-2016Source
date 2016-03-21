using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.DataSources
{
    /// <summary>
    /// 客户子系统查询DataSource的通用类，可以直接使用Instance实例。已经重载了获取数据库连接名称的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public class GenericCustomerDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
        where T : new()
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericCustomerDataSource<T, TCollection> Instance = new GenericCustomerDataSource<T, TCollection>();

        private GenericCustomerDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
