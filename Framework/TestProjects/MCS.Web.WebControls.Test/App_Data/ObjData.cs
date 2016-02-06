using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using MCS.Library.Data;

namespace MCS.Web.WebControls.Test 
{
    public class ObjData
    {
        public static string ConnectionString = "defaultDatabase";
       
        public ObjData()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }          

        public static DataSet RunSqlReturnDS(string strSql)
        {
            Database db = DatabaseFactory.Create(ConnectionString);

            return db.ExecuteDataSet(CommandType.Text, strSql);
        }
        public static object RunSqlReturnObject(string strSql)
        { 
            Database db = DatabaseFactory.Create(ConnectionString);
            return db.ExecuteScalar(CommandType.Text, strSql);
        }

        public static DataSet GetlistTop10()
        {
            string sql = "SELECT top 10 ORDER_ID,SORT_ID,CUSTOMER_NAME,(CASE PRIORITY WHEN 0 THEN 'Normal' WHEN 1 THEN 'High'  WHEN -1 THEN 'Low' END) PRIORITY,CREATE_USER,CREATE_TIME,UPDATE_TAG FROM ORDERS";

            return RunSqlReturnDS(sql);
        }

        public static DataSet Getlist()
        {
            string sql = "SELECT ORDER_ID,SORT_ID,CUSTOMER_NAME,(CASE PRIORITY WHEN 0 THEN 'Normal' WHEN 1 THEN 'High'  WHEN -1 THEN 'Low' END) PRIORITY,CREATE_USER,CREATE_TIME,UPDATE_TAG FROM ORDERS";

            return RunSqlReturnDS(sql); ;
        }

        public static DataSet GetQuery()
        {
            string sql = "SELECT ORDER_ID,SORT_ID,CUSTOMER_NAME,(CASE PRIORITY WHEN 0 THEN 'Normal' WHEN 1 THEN 'High'  WHEN -1 THEN 'Low' END) PRIORITY,CREATE_USER,CREATE_TIME,UPDATE_TAG FROM ORDERS  WHERE PRIORITY ='1'";

            return RunSqlReturnDS(sql); 
        }

        public static int GetOrdersCount()
        {
            string sql = "SELECT COUNT(*) FROM ORDERS";
            object obj = RunSqlReturnObject(sql);
            return obj != null ? (int)obj : 0;
        }

        public static int GetOrdersQueryCount()
        {
            string sql = "SELECT COUNT(*)  FROM ORDERS  WHERE PRIORITY ='1'";
            object obj = RunSqlReturnObject(sql);
            return obj != null ? (int)obj : 0;
        }

        public static DataSet GetPagerQuery(int pageSize, int pageIndex)
        {
            string sql = String.Format(" Select Top  {0} ORDER_ID,SORT_ID,CUSTOMER_NAME,(CASE PRIORITY WHEN 0 THEN 'Normal' WHEN 1 THEN 'High'  WHEN -1 THEN 'Low' END) PRIORITY,CREATE_USER,CREATE_TIME,UPDATE_TAG  FROM ORDERS Where PRIORITY ='1' AND ORDER_ID Not in ( Select Top {1} ORDER_ID FROM ORDERS  WHERE PRIORITY ='1' Order By ORDER_ID Desc) Order By ORDER_ID Desc", pageSize, pageIndex);
            return RunSqlReturnDS(sql);
        }

        public static DataSet GetPagerList(int pageSize, int pageIndex)
        {
            string sql = String.Format(" Select Top  {0} ORDER_ID,SORT_ID,CUSTOMER_NAME,(CASE PRIORITY WHEN 0 THEN 'Normal' WHEN 1 THEN 'High'  WHEN -1 THEN 'Low' END) PRIORITY,CREATE_USER,CREATE_TIME,UPDATE_TAG  FROM ORDERS Where  ORDER_ID Not in ( Select Top {1} ORDER_ID FROM ORDERS  Order By ORDER_ID Desc) Order By ORDER_ID Desc", pageSize, pageIndex);
            return RunSqlReturnDS(sql);
        }
    }
}
