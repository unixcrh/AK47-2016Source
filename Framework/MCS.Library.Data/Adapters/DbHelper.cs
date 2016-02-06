using MCS.Library.Core;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbHelper
    {
        #region  数据访问函数

        //public static DbContext GetDBContext()
        //{
        //    return DbContext.GetContext(ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static DbContext GetDBContext(string connectionName)
        {
            return DbContext.GetContext(connectionName);
        }

        //public static Database GetDBDatabase()
        //{
        //    return DatabaseFactory.Create(ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static Database GetDBDatabase(string connectionName)
        {
            return DatabaseFactory.Create(connectionName);
        }

        //public static void RunSql(Action<Database> action)
        //{
        //    RunSql(action, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="connectionName"></param>
        public static void RunSql(Action<Database> action, string connectionName)
        {
            using (DbContext context = DbContext.GetContext(connectionName))
            {
                Database db = DatabaseFactory.Create(connectionName);

                action(db);
            }
        }

        //public static object RunSqlReturnScalar(string strSql)
        //{
        //    return RunSqlReturnScalar(strSql, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static object RunSqlReturnScalar(string strSql, string connectionName)
        {
            using (DbContext dbi = DbContext.GetContext(connectionName))
            {
                Database db = DatabaseFactory.Create(dbi);

                return db.ExecuteScalar(CommandType.Text, strSql);
            }
        }

        //public static int RunSql(string strSql)
        //{
        //    return RunSql(strSql, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 周杨于20090715修改
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static int RunSql(string strSql, string connectionName)
        {
            using (DbContext dbi = DbContext.GetContext(connectionName))
            {
                Database db = DatabaseFactory.Create(dbi);
                return db.ExecuteNonQuery(CommandType.Text, strSql);
            }
        }

        //public static int RunSqlWithTransaction(string strSql)
        //{
        //    return RunSqlWithTransaction(strSql, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static int RunSqlWithTransaction(string strSql, string connectionName)
        {
            using (TransactionScope ts = TransactionScopeFactory.Create(TransactionScopeOption.Required))
            {
                int result = RunSql(strSql, connectionName);

                ts.Complete();

                return result;
            }
        }

        //public static IDataReader RunSqlReturnDR(string strSql)
        //{
        //    return RunSqlReturnDR(strSql, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static IDataReader RunSqlReturnDR(string strSql, string connectionName)
        {
            using (DbContext dbi = DbContext.GetContext(connectionName))
            {
                Database db = DatabaseFactory.Create(dbi);

                return db.ExecuteReader(CommandType.Text, strSql);
            }
        }

        //public static DataSet RunSqlReturnDS(string strSql)
        //{
        //    return RunSqlReturnDS(strSql, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static DataSet RunSqlReturnDS(string strSql, string connectionName)
        {
            using (DbContext dbi = DbContext.GetContext(connectionName))
            {
                Database db = DatabaseFactory.Create(dbi);

                return db.ExecuteDataSet(CommandType.Text, strSql);
            }
        }

        //public static XmlDocument RunSQLReturnXmlDoc(string strSql)
        //{
        //    return RunSQLReturnXmlDoc(strSql, ConnectionDefine.DBConnectionName);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static XmlDocument RunSQLReturnXmlDoc(string strSql, string connectionName)
        {
            string xmlStr = string.Empty;

            if (strSql != string.Empty)
            {
                strSql += " FOR XML AUTO, ELEMENTS";
            }

            object obj = RunSqlReturnScalar(strSql, connectionName);

            if (obj != null)

                xmlStr = obj.ToString();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlStr);

            return xmlDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static DataSet RunSPReturnDS(string spName, string connectionName, params object[] parameterValues)
        {
            using (DbContext dbi = GetDBContext(connectionName))
            {
                Database db = DatabaseFactory.Create(dbi);

                return db.ExecuteDataSet(spName, parameterValues);
            }
        }

        //public static IDataReader RunSPReturnDR(string spName, params object[] parameterValues)
        //{
        //    using (DbContext dbi = GetDBContext())
        //    {
        //        Database db = DatabaseFactory.Create(dbi);

        //        return db.ExecuteReader(spName, parameterValues);
        //    }
        //}
        #endregion

        #region 数据验证
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validateObject"></param>
        /// <param name="unValidates"></param>
        public static void ValidateFalseThrow(object validateObject, params string[] unValidates)
        {
            ValidateFalseThrow<SystemSupportException>(validateObject, unValidates);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validateObject"></param>
        /// <param name="unValidates"></param>
        public static void ValidateFalseThrow<T>(object validateObject, params string[] unValidates) where T : System.Exception
        {
            List<string> unValidatesList = new List<string>();

            if (unValidates != null)
                unValidatesList.AddRange(unValidates);

            Validator validator = ValidationFactory.CreateValidator(validateObject.GetType(), unValidatesList);
            ValidationResults validationResults = validator.Validate(validateObject);

            CheckValidationResults<T>(validationResults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validateObject"></param>
        /// <param name="ruleset"></param>
        public static void ValidateFalseThrow<T>(object validateObject, string ruleset) where T : System.Exception
        {
            Validator validator = ValidationFactory.CreateValidator(validateObject.GetType(), ruleset);
            ValidationResults validationResults = validator.Validate(validateObject);

            CheckValidationResults<T>(validationResults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validationResults"></param>
        public static void CheckValidationResults<T>(ValidationResults validationResults) where T : System.Exception
        {
            if (validationResults.IsValid() == false)
            {
                string errorMessage = BuildErrorMessage(validationResults);

                errorMessage.IsNotEmpty().TrueThrow<T>(errorMessage);
            }
        }

        private static string BuildErrorMessage(IEnumerable<ValidationResult> validationResults)
        {
            StringBuilder strB = new StringBuilder();

            foreach (ValidationResult result in validationResults)
                strB.AppendLine(result.Message);

            return strB.ToString();
        }
        #endregion
    }
}
