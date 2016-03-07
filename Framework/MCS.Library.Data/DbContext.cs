#region
// -------------------------------------------------
// Assembly	：	DeluxeWorks.Library.Data
// FileName	：	GenericDbContext.cs
// Remark	：	Generic database processing context。
// -------------------------------------------------
// VERSION  	AUTHOR			DATE			CONTENT
// 1.0		    王翔			20070430		创建
//	1.1			ccic\yuanyong	20070725		增加属性internal string ConnName
// -------------------------------------------------
#endregion
#region using
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
#endregion
namespace MCS.Library.Data
{
    /// <summary>
    /// Generic database processing context.
    /// <remarks>
    /// <list type="bullet">
    ///     <item>this context is attatch to current HttpContext(web app) or Thread CurrentContext property.</item>
    ///     <item>the primary goal is to harmonize the Transaction management in a call stack.</item>
    ///     <item>itself could be disposed automatically.</item>
    /// </list>
    /// </remarks>
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public abstract class DbContext : IDisposable
    {
        private bool _autoClose = true;
        private TimeSpan _commandTimeout = TimeSpan.FromSeconds(30);

        #region Public Methods
        /// <summary>
        /// 在上下文中添加待执行的SQL语句，自动添加语句分隔符
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public abstract void AppendSqlWithSperatorInContext(SqlBuilderBase sqlBuilder, string format, params object[] args);

        /// <summary>
        /// 在上下文中添加待执行的SQL语句
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public abstract void AppendSqlInContext(SqlBuilderBase sqlBuilder, string format, params object[] args);

        /// <summary>
        /// 清除上下文中的SQL语句
        /// </summary>
        public abstract void ClearSqlInContext();

        /// <summary>
        /// 得到上下文中的SQL语句
        /// </summary>
        public abstract string GetSqlInContext();

        /// <summary>
        /// 执行保存在上下文中的SQL语句，返回DataSet
        /// </summary>
        /// <param name="clearSqlAfterExecute">执行完后是否清除上下文中的SQL，默认是true</param>
        /// <param name="tableNames">可选参数，DataSet中的表名</param>
        /// <returns></returns>
        public abstract DataSet ExecuteDataSetSqlInContext(bool clearSqlAfterExecute = true, params string[] tableNames);

        /// <summary>
        /// 执行保存在上下文中的SQL语句，返回DataReader
        /// </summary>
        /// <param name="clearSqlAfterExecute">执行完后是否清除上下文中的SQL，默认是true</param>
        /// <returns></returns>
        public abstract DbDataReader ExecuteReaderSqlInContext(bool clearSqlAfterExecute = true);

        /// <summary>
        /// 执行保存在上下文中的SQL语句，返回单值
        /// </summary>
        /// <param name="clearSqlAfterExecute">执行完后是否清除上下文中的SQL，默认是true</param>
        /// <returns></returns>
        public abstract object ExecuteScalarSqlInContext(bool clearSqlAfterExecute = true);

        /// <summary>
        /// 执行保存在上下文中的SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="clearSqlAfterExecute">执行完后是否清除上下文中的SQL，默认是true</param>
        public abstract int ExecuteNonQuerySqlInContext(bool clearSqlAfterExecute = true);

        #endregion End Public Methods

        /// <summary>
        /// 获取一个DbContext对象
        /// </summary>
        /// <param name="name">连接名称</param>
        /// <param name="autoClose">是否自动关闭</param>
        /// <returns>DbContext对象</returns>
        public static DbContext GetContext(string name, bool autoClose)
        {
            //得到映射后连接名称
            name = DbConnectionMappingContext.GetMappedConnectionName(name);

            DbProviderFactory factory = DbConnectionManager.GetDbProviderFactory(name);

            DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();

            csb.ConnectionString = DbConnectionManager.GetConnectionString(name);

            bool enlist = true;

            if (csb.ContainsKey("enlist"))
                enlist = (bool)csb["enlist"];

            DbContext result = null;

            if (enlist)
                result = new AutoEnlistDbContext(name, autoClose);
            else
                result = new NotEnlistDbContext(name, autoClose);

            result.InitDbContext(name, autoClose);

            result._autoClose = autoClose;
            result._commandTimeout = DbConnectionManager.GetCommandTimeout(name);

            return result;
        }

        /// <summary>
        /// 重载获取DbContext对象
        /// </summary>
        /// <param name="name">连接名称</param>
        /// <returns></returns>
        public static DbContext GetContext(string name)
        {
            return GetContext(name, true);
        }

        /// <summary>
        /// 获取一个DbContext对象
        /// </summary>
        /// <param name="name">连接名称</param>
        /// <param name="autoClose">是否自动关闭</param>
        /// <returns>DbContext对象</returns>
        public static async Task<DbContext> GetContextAsync(string name, bool autoClose)
        {
            //得到映射后连接名称
            name = DbConnectionMappingContext.GetMappedConnectionName(name);

            DbProviderFactory factory = DbConnectionManager.GetDbProviderFactory(name);

            DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();

            csb.ConnectionString = DbConnectionManager.GetConnectionString(name);

            bool enlist = true;

            if (csb.ContainsKey("enlist"))
                enlist = (bool)csb["enlist"];

            DbContext result = null;

            if (enlist)
                result = new AutoEnlistDbContext(name, autoClose);
            else
                result = new NotEnlistDbContext(name, autoClose);

            await result.InitDbContextAsync(name, autoClose);

            result._autoClose = autoClose;
            result._commandTimeout = DbConnectionManager.GetCommandTimeout(name);

            return result;
        }

        /// <summary>
        /// 异步获取DbContext对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<DbContext> GetContextAsync(string name)
        {
            return await GetContextAsync(name, true);
        }

        #region Public 成员

        /// <summary>
        /// 是否自动关闭
        /// </summary>
        public bool AutoClose
        {
            get
            {
                return this._autoClose;
            }
            private set
            {
                this._autoClose = value;
            }
        }

        /// <summary>
        /// 数据连接对象
        /// </summary>
        public abstract DbConnection Connection
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// 数据事务对象
        /// </summary>
        public abstract DbTransaction LocalTransaction
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// 数据连接名称
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// SQL命令的超时时间
        /// </summary>
        public TimeSpan CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                this._commandTimeout = value;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// 初始化DbContext
        /// </summary>
        /// <param name="name"></param>
        /// <param name="autoClose"></param>
        protected virtual void InitDbContext(string name, bool autoClose)
        {
        }

        /// <summary>
        /// 异步初始化DbContext
        /// </summary>
        /// <param name="name"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        protected async virtual Task InitDbContextAsync(string name, bool autoClose)
        {
            await Task.Delay(0);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
