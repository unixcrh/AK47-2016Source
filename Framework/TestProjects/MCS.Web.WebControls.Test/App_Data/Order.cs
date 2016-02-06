using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace MCS.Web.WebControls.Test
{
    public enum PriorityType
    {
        Low = -1,
        Normal = 0,
        High = 1
    }



    /// <summary>
    /// 订单对象
    /// </summary>
    [Serializable]
    public class Order
    {
        private List<OrderPruduct> products = new List<OrderPruduct>();

        private string orderID = string.Empty;

        public string OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; }
        }

        private string customerName = string.Empty;

        public string CustomerName
        {
            get { return this.customerName; }
            set { this.customerName = value; }
        }

        private PriorityType priority = PriorityType.Normal;

        public PriorityType Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        private Nullable<System.DateTime> createTime = null;

		public Nullable<System.DateTime> CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }

        private string createUser = string.Empty;

        public string CreateUser
        {
            get { return this.createUser; }
            set { this.createUser = value; }
        }

        private int updateTag = 0;

        public int UpdateTag
        {
            get { return this.updateTag; }
            set { this.updateTag = value; }
        }

        public bool checkedID = false;

        public bool CheckedID
        {
            get { return this.checkedID; }
            set { this.checkedID = value; }
        }

        public Order()
        {
        }

        public List<OrderPruduct> Products
        {
            get
            {
                return this.products;
            }
        }

        public OrderPruduct AppendEmptyProduct()
        {
            OrderPruduct product = new OrderPruduct();

            product.OrderID = this.OrderID;

            int subID = 1;

            if (Products.Count > 0)
                subID = Products[Products.Count - 1].SubID + 1;

            product.SubID = subID;

            Products.Add(product);

            return product;
        }

        public void DeleteProductByIndex(int index)
        {
            Products.RemoveAt(index);

            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].SubID = i + 1;
            }
        }
    }

    /// <summary>
    /// 订单中的产品
    /// </summary>
    [Serializable]
    public class OrderPruduct
    {
        private string orderID = string.Empty;

        public string OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; }
        }

        private int subID = 0;

        public int SubID
        {
            get { return this.subID; }
            set { this.subID = value; }
        }

        private string productName = string.Empty;

        public string ProductName
        {
            get { return this.productName; }
            set { this.productName = value; }
        }

        private string category = string.Empty;

        public string Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        private string itemUnit = string.Empty;

        public string ItemUnit
        {
            get { return this.itemUnit; }
            set { this.itemUnit = value; }
        }

        private int quantity = 0;

        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }

        private Decimal itemPrice = 0;

        public Decimal ItemPrice
        {
            get { return this.itemPrice; }
            set { this.itemPrice = value; }
        }
    }
}
