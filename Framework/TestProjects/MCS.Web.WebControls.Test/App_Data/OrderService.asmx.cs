using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace MCS.Web.WebControls.Test
{
    /// <summary>
    /// OrderService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class OrderService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        public OrderService()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        public Order GetOrder(string orderID)
        {
            string sql = string.Format("SELECT * FROM ORDERS WHERE ORDER_ID = '{0}'", orderID.Replace("\'", "\'\'"));

            DataSet ds = ObjData.RunSqlReturnDS(sql);
            if (ds.Tables[0].Rows.Count == 0)
                throw new ApplicationException(string.Format("不能找到OrderID为{0}的记录", orderID));

            return GetOrderFromDataRow(ds.Tables[0].Rows[0]);            
        }

        [WebMethod]
        public Order GetOrderWithProducts(string orderID)
        {
            string sql = string.Format("SELECT * FROM ORDERS WHERE ORDER_ID = '{0}'", orderID.Replace("\'", "\'\'"));

            DataSet ds = ObjData.RunSqlReturnDS(sql);
            DataTable table = new DataTable("ORDERS");
            table = ds.Tables[0];
            if (table.Rows.Count == 0)
                throw new ApplicationException(string.Format("不能找到OrderID为{0}的记录", orderID));

            Order order =  GetOrderFromDataRow(table.Rows[0]);

            sql = string.Format("SELECT * FROM ORDERS_PRODUCTS WHERE ORDER_ID = '{0}' ORDER BY SUB_ID", orderID.Replace("\'", "\'\'"));

            table = new DataTable("ORDERS_PRODUCTS");

            ds = ObjData.RunSqlReturnDS(sql);
            table = ds.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                order.Products.Add(GetOrderProductsFromDataRow(row));
            }

            return order;

            
        }

        [WebMethod]
        public List<Order> GetOrdersList()
        {
            return GetOrdersFromDataView(ObjData.RunSqlReturnDS("SELECT * FROM ORDERS ORDER BY SORT_ID DESC").Tables[0].DefaultView); 
        }

        [WebMethod]
        public List<Order> GetPagedOrdersList(int startRowIndex, int maximumRows, string sortExpression)
        {
            string sql = @"WITH OrderedOrders AS
                    (
	                    SELECT ORDERS.*,1 AS CHECKED_ID, ROW_NUMBER() OVER(ORDER BY {0}) AS ROW_NUMBER
	                    FROM ORDERS
                    )
                    SELECT *,1 AS CHECKED_ID FROM OrderedOrders
                    WHERE ROW_NUMBER > {1} AND ROW_NUMBER <= {2}
                    ORDER BY {0}; ";

            if (string.IsNullOrEmpty(sortExpression))
                sortExpression = "SORT_ID DESC";

            sql = string.Format(sql, sortExpression, startRowIndex, startRowIndex + maximumRows);

            return GetOrdersFromDataView(ObjData.RunSqlReturnDS(sql).Tables[0].DefaultView); 
        }

        [WebMethod]
        public int GetOrdersCount()
        {
            return (int)ObjData.RunSqlReturnObject("SELECT COUNT(*) FROM ORDERS  "); 
        }

        [WebMethod]
        public int InsertOrder(Order order)
        {
            //if (string.IsNullOrEmpty(order.OrderID))
            //    order.OrderID = Guid.NewGuid().ToString();

            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            //{
            //    conn.Open();

            //    SqlTransaction ts = conn.BeginTransaction();
            //    try
            //    {
            //        int result = InternalInsertOrder(order, conn, ts);

            //        InsertOrderProducts(order.Products, conn, ts);

            //        ts.Commit();
            //        return result;
            //    }
            //    catch (System.Exception)
            //    {
            //        ts.Rollback();
            //        throw;
            //    }
            //}
            return 1;
        }

        [WebMethod]
        public int UpdateOrder(Order order)
        {
            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            //{
            //    conn.Open();

            //    SqlTransaction ts = conn.BeginTransaction();
            //    try
            //    {
            //        int result = InternalUpdateOrder(order, conn, ts);

            //        if (result == 0)
            //            result = InternalInsertOrder(order, conn, ts);

            //        UpdateOrderProducts(order.Products, conn, ts);

            //        ts.Commit();
            //        return result;
            //    }
            //    catch (System.Exception)
            //    {
            //        ts.Rollback();
            //        throw;
            //    }
            //}
            return 1;
        }

        private int InternalInsertOrder(Order order, SqlConnection conn, SqlTransaction ts)
        {
            //if (string.IsNullOrEmpty(order.OrderID))
            //    order.OrderID = Guid.NewGuid().ToString();

            //SqlCommand cmd = new SqlCommand("INSERT INTO ORDERS(ORDER_ID, CUSTOMER_NAME, PRIORITY, CREATE_USER) " +
            //        "VALUES(@ORDER_ID, @CUSTOMER_NAME, @PRIORITY, @CREATE_USER)",
            //        conn);

            //cmd.Transaction = ts;
            //cmd.Parameters.AddWithValue("ORDER_ID", order.OrderID);
            //cmd.Parameters.AddWithValue("CUSTOMER_NAME", order.CustomerName);
            //cmd.Parameters.AddWithValue("PRIORITY", (int)order.Priority);
            //cmd.Parameters.AddWithValue("CREATE_USER", order.CreateUser);

            //return (int)cmd.ExecuteNonQuery();
            return 1;
        }

        private int InternalUpdateOrder(Order order, SqlConnection conn, SqlTransaction ts)
        {
            //SqlCommand cmd = new SqlCommand("UPDATE ORDERS " +
            //        "SET CUSTOMER_NAME = @CUSTOMER_NAME, PRIORITY = @PRIORITY, CREATE_USER = @CREATE_USER, UPDATE_TAG = UPDATE_TAG + 1 " +
            //        "WHERE ORDER_ID = @ORDER_ID AND UPDATE_TAG = @UPDATE_TAG",
            //        conn);

            //cmd.Transaction = ts;
            //cmd.Parameters.AddWithValue("ORDER_ID", order.OrderID);
            //cmd.Parameters.AddWithValue("UPDATE_TAG", order.UpdateTag);
            //cmd.Parameters.AddWithValue("CUSTOMER_NAME", order.CustomerName);
            //cmd.Parameters.AddWithValue("PRIORITY", (int)order.Priority);
            //cmd.Parameters.AddWithValue("CREATE_USER", order.CreateUser);

            //int count = cmd.ExecuteNonQuery();

            //if (count == 0)
            //{
            //    cmd = new SqlCommand("SELECT COUNT(*) FROM ORDERS WHERE ORDER_ID = @ORDER_ID", conn);

            //    cmd.Transaction = ts;
            //    cmd.Parameters.AddWithValue("ORDER_ID", order.OrderID);

            //    int orderCount = (int)cmd.ExecuteScalar();

            //    if (orderCount > 0)
            //        throw new ApplicationException(string.Format("数据{0}已经被别人更新，请重新获取数据", order.OrderID));
            //}

            //return count;
            return 1;
        }

        [WebMethod]
        public DataTable GetUnitDefine()
        {
            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            //{
            //    conn.Open();

            //    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM UNIT_DEFINE ORDER BY UNIT_ID", conn);

            //    DataTable table = new DataTable("UNIT_DEFINE");
            //    adapter.Fill(table);

            //    return table;
            //}

            return ObjData.RunSqlReturnDS("SELECT * FROM UNIT_DEFINE ORDER BY UNIT_ID").Tables[0];
        }

        [WebMethod]
        public DataTable GetCategories()
        {             
            return ObjData.RunSqlReturnDS("SELECT * FROM CATEGORY ORDER BY CATEGORY_ID").Tables[0];
        }

        private void UpdateOrderProducts(List<OrderPruduct> products, SqlConnection conn, SqlTransaction ts)
        {
            if (products.Count > 0)
            {
                DeleteOrderProductsByOrderID(products[0].OrderID, conn, ts);

                InsertOrderProducts(products, conn, ts);
            }
        }

        private void InsertOrderProducts(List<OrderPruduct> products, SqlConnection conn, SqlTransaction ts)
        {
            foreach (OrderPruduct product in products)
            {
                InsertOrderProduct(product, conn, ts);
            }
        }

        private void DeleteOrderProductsByOrderID(string orderID, SqlConnection conn, SqlTransaction ts)
        {
            //SqlCommand cmd = new SqlCommand("DELETE ORDERS_PRODUCTS WHERE ORDER_ID = @ORDER_ID",
            //        conn);

            //cmd.Transaction = ts;
            //cmd.Parameters.AddWithValue("ORDER_ID", orderID);
            //cmd.ExecuteNonQuery();
        }

        private void InsertOrderProduct(OrderPruduct product, SqlConnection conn, SqlTransaction ts)
        {
            //SqlCommand cmd = new SqlCommand("INSERT ORDERS_PRODUCTS " +
            //        "(ORDER_ID, SUB_ID, PRODUCT_NAME, CATEGORY, ITEM_UNIT, QUANTITY, ITEM_PRICE)" +
            //        "VALUES(@ORDER_ID, @SUB_ID, @PRODUCT_NAME, @CATEGORY, @ITEM_UNIT, @QUANTITY, @ITEM_PRICE)",
            //        conn);

            //cmd.Transaction = ts;
            //cmd.Parameters.AddWithValue("ORDER_ID", product.OrderID);
            //cmd.Parameters.AddWithValue("SUB_ID", product.SubID);
            //cmd.Parameters.AddWithValue("PRODUCT_NAME", product.ProductName);
            //cmd.Parameters.AddWithValue("CATEGORY", product.Category);
            //cmd.Parameters.AddWithValue("ITEM_UNIT", product.ItemUnit);
            //cmd.Parameters.AddWithValue("QUANTITY", product.Quantity);
            //cmd.Parameters.AddWithValue("ITEM_PRICE", product.ItemPrice);

            //cmd.ExecuteNonQuery();
        }

        private Order GetOrderFromDataRow(DataRow row)
        {
            Order order = new Order();

            order.OrderID = row["ORDER_ID"].ToString();
            order.CustomerName = row["CUSTOMER_NAME"].ToString();
            order.Priority = (PriorityType)row["PRIORITY"];
            order.CreateTime = (System.DateTime?)row["CREATE_TIME"];
            order.CreateUser = row["CREATE_USER"].ToString();
            order.UpdateTag = (int)row["UPDATE_TAG"];

            return order;
        }

        private OrderPruduct GetOrderProductsFromDataRow(DataRow row)
        {
            OrderPruduct product = new OrderPruduct();

            product.OrderID = row["ORDER_ID"].ToString();
            product.SubID = (int)row["SUB_ID"];
            product.ProductName = row["PRODUCT_NAME"].ToString();
            product.Category = row["CATEGORY"].ToString();
            product.ItemUnit = row["ITEM_UNIT"].ToString();
            product.Quantity = (int)row["QUANTITY"];
            product.ItemPrice = (Decimal)row["ITEM_PRICE"];

            return product;
        }

        private List<Order> GetOrdersFromDataView(DataView view)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRowView drv in view)
                orders.Add(GetOrderFromDataRow(drv.Row));

            return orders;
        }
    }
}
