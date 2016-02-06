using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using MCS.Library.Data.Builder;

namespace MCS.Web.WebControls.Test
{
    public class OrdersDataViewAdapter
    {
		private static OrdersDataViewAdapter instance = new OrdersDataViewAdapter();

		public static OrdersDataViewAdapter Instance
		{
			get
			{
				return instance;
			}
		}

        public OrdersDataViewAdapter()
        {
        }
		
        public DataView GetData()
        {
            return ObjData.RunSqlReturnDS("SELECT * FROM ORDERS").Tables[0].DefaultView;             
        }

		public DataTable GetData(int startRowIndex, int maximumRows)
		{
			string sql = @"WITH OrderedOrders AS
                    (
	                    SELECT ORDERS.*, ROW_NUMBER() OVER(ORDER BY {0}) AS ROW_NUMBER
	                    FROM ORDERS
                    )
                    SELECT * FROM OrderedOrders
                    WHERE ROW_NUMBER > {1} AND ROW_NUMBER <= {2};";
			string sortExpression = string.Empty;
			if (string.IsNullOrEmpty(sortExpression))
				sortExpression = "SORT_ID DESC";

			sql = string.Format(sql, sortExpression, startRowIndex, startRowIndex + maximumRows);

			return ObjData.RunSqlReturnDS(sql).Tables[0];
		}

		public DataTable GetFilteredData(int startRowIndex, int maximumRows, string priority)
		{
			return GetFilteredData(startRowIndex, maximumRows, priority, string.Empty);
		}

		public DataTable GetFilteredData(int startRowIndex, int maximumRows, string priority, string sortExpression)
		{
			if (maximumRows == 0)
				maximumRows = int.MaxValue;

			string sql = @"WITH OrderedOrders AS
                    (
	                    SELECT ORDERS.*, ROW_NUMBER() OVER(ORDER BY {0}) AS ROW_NUMBER
	                    FROM ORDERS
						WHERE PRIORITY = ISNULL({3},  PRIORITY)
                    )
                    SELECT * FROM OrderedOrders
                    WHERE ROW_NUMBER > {1} AND ROW_NUMBER <= {2};";

			if (string.IsNullOrEmpty(sortExpression))
				sortExpression = "SORT_ID DESC";

			sql = string.Format(sql, sortExpression, startRowIndex, startRowIndex + maximumRows, 
				string.IsNullOrEmpty(priority) ? "NULL" : TSqlBuilder.Instance.CheckQuotationMark(priority, true));

			return ObjData.RunSqlReturnDS(sql).Tables[0];
		}

		public int GetFilteredDataCount(string priority)
		{
			string sql = "SELECT COUNT(*) FROM ORDERS";

			if (string.IsNullOrEmpty(priority) == false)
				sql += " WHERE PRIORITY = " + TSqlBuilder.Instance.CheckQuotationMark(priority, true);

			return (int)ObjData.RunSqlReturnObject(sql); 
		}

        public DataView GetData(int startRowIndex, int maximumRows, string sortExpression)
        {
            string sql = @"WITH OrderedOrders AS
                    (
	                    SELECT ORDERS.*, ROW_NUMBER() OVER(ORDER BY {0}) AS ROW_NUMBER
	                    FROM ORDERS
                    )
                    SELECT * FROM OrderedOrders
                    WHERE ROW_NUMBER > {1} AND ROW_NUMBER <= {2};";

            if (string.IsNullOrEmpty(sortExpression))
                sortExpression = "SORT_ID DESC";

            sql = string.Format(sql, sortExpression, startRowIndex, startRowIndex + maximumRows);

            return ObjData.RunSqlReturnDS(sql).Tables[0].DefaultView;    
        }

        public int GetOrdersCount()
        { 
            return (int)ObjData.RunSqlReturnObject("SELECT COUNT(*) FROM ORDERS");            
        }

        public DataView GetOrdersPruductsByOrderIDs(params string[] orderIDs)
        {
            if (orderIDs.Length == 0)
                throw new ArgumentException("参数orderIDs的个数必须大于零", "orderIDs");

            StringBuilder strB = new StringBuilder(256);

            for (int i = 0; i < orderIDs.Length; i++)
            {
                if (strB.Length > 0)
                    strB.Append(", ");

                strB.AppendFormat("\'{0}\'", orderIDs[i]);
            }

            string sql = string.Format("SELECT * FROM ORDERS_PRODUCTS WHERE ORDER_ID IN ({0})", strB.ToString());

            return ObjData.RunSqlReturnDS(sql).Tables[0].DefaultView;   
        }
    }
}
