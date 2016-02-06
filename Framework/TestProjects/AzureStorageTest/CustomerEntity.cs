using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageTest
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }

        public CustomerEntity() { }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string BigString { get; set; }

        public byte[] Data { get; set; }

        //public override void ReadEntity(IDictionary<string, EntityProperty> properties, Microsoft.WindowsAzure.Storage.OperationContext operationContext)
        //{
        //    base.ReadEntity(properties, operationContext);
        //}

        //public override IDictionary<string, EntityProperty> WriteEntity(Microsoft.WindowsAzure.Storage.OperationContext operationContext)
        //{
        //    IDictionary<string, EntityProperty> dict = new Dictionary<string, EntityProperty>();

        //    EntityProperty property1 = new EntityProperty("shenzheng72@hotmail.com");
        //    dict.Add("Email", property1);
        //    //IDictionary<string, EntityProperty> dict = base.WriteEntity(operationContext);

        //    return dict;
        //}
    }
}
