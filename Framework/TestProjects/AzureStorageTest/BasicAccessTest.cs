using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Text;

namespace AzureStorageTest
{
    /// <summary>
    /// 参考https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-tables/
    /// </summary>
    [TestClass]
    public class BasicAccessTest
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=zhshen;AccountKey=xvradPYka/j8bKV7cBS5og0uKSBtwfZPumwL4ImW+Jy2dn0KlgWQIhGgTL5fvSTaIv6mKXOxFGO5y+QzIp417Q==";

        [TestMethod]
        public void CustomerEntityTest()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("People");
            table.CreateIfNotExists();

            CustomerEntity customer1 = new CustomerEntity("Harp", "Walter");
            customer1.Email = "Walter@contoso.com";
            customer1.PhoneNumber = "425-555-0101";
            customer1.BigString = new string('A', 10240);
            customer1.Data = Encoding.UTF8.GetBytes(new string('B', 10240));

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(customer1);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        [TestMethod]
        public void DynamicTableEntityTest()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("People");
            table.CreateIfNotExists();

            DynamicTableEntity customer1 = new DynamicTableEntity("Zheng", "Shen");

            EntityProperty email = new EntityProperty("shenzheng72@hotmail.com");

            customer1.Properties.Add("Email", email);

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(customer1);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }
    }
}
