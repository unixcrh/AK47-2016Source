using MCS.Library.Core;
using MCS.Library.Data;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customer.Helper;
using PPTS.WebAPI.Customer.ViewModels.Customer;
using System.Transactions;
using System.Web.Http;

namespace PPTS.WebAPI.Customer.Controllers
{
    public class PotentialCustomersController : ApiController
    {
        private static PotentialCustomerAdapter customerAdapter = PotentialCustomerAdapter.Instance;
        private static ParentAdapter parentAdapter = ParentAdapter.Instance;
        private static CustomerRelationAdapter relationAdapter = CustomerRelationAdapter.Instance;

        #region api/potentialcustomers/getallcustomers

        [HttpPost]
        public CustomerQueryResultModel GetAllCustomers(CustomerQueryCriteriaModel criteria)
        {
            var isInitialRequest = criteria.PagedParam == null;
            if (isInitialRequest)
            {
                criteria.PagedParam = new PagedParam();
            }

            var result = new CustomerQueryResultModel(criteria.PagedParam);
            var customers = customerAdapter.Load((builder) =>
            {
                builder.AppendItem("CustomerName", "1111");
            });

            result.ViewModelList.Data.AddRange(customers.ConvertToViewModelList<PotentialCustomer, CustomerViewModel>());

            //foreach (var customer in customers)
            //{
            //    result.ViewModelList.Data.Add(customer.ConvertToViewModel<PotentialCustomer, CustomerViewModel>());
            //}

            //var customers = customerAdapter.LoadByBuilder(this.CreateQueryBuilder(criteria));
            //result.ViewModelList.Data.AddRange(customers.ConvertToViewModelList<PotentialCustomer, CustomerViewModel>());
            criteria.PagedParam.Initialize(1);

            if (isInitialRequest)
            {
                // 初次请求需加载字典
                DataHelper.LoadDictionaryData(result.Dictionaries, typeof(PotentialCustomer), typeof(Parent));
            }

            return result;
        }

        /// <summary>
        /// 潜客查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PotentialCustomerQueryResult QueryAllCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            PotentialCustomerQueryResult result = new PotentialCustomerQueryResult();

            result.QueryResult = GenericCustomerDataSource<PotentialCustomer, PotentialCustomerCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent));

            return result;
        }

        /// <summary>
        /// 潜客查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<PotentialCustomer, PotentialCustomerCollection> QueryPagedCustomers(PotentialCustomerQueryCriteriaModel criteria)
        {
            return GenericCustomerDataSource<PotentialCustomer, PotentialCustomerCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }
        #endregion

        #region api/potentialcustomers/getcustomerbaseinfo
        public CustomerViewModel GetCustomerBaseinfo(string customerId)
        {
            var result = new CustomerViewModel();
            result = customerAdapter.Load(customerId).ConvertToViewModel<PotentialCustomer, CustomerViewModel>();
            return result;
        }
        #endregion

        #region api/potentialcustomers/createcustomer

        [HttpGet]
        public CreatableCustomerViewModel CreateCustomer()
        {
            var result = new CreatableCustomerViewModel();
            DataHelper.LoadDictionaryData(result.Dictionaries, typeof(PotentialCustomer), typeof(Parent));
            return result;
        }

        [HttpPost]
        public void CreateCustomer(CreatableCustomerViewModel model)
        {
            model.NullCheck("model");
            model.Customer.NullCheck("Customer");
            model.PrimaryParent.NullCheck("PrimaryParent");

            var relation = new CustomerRelation
            {
                CustomerID = model.Customer.CustomerId,
                ParentID = model.PrimaryParent.ParentId,
                IsPrimary = true
            };

            using (var context = customerAdapter.GetDbContext())
            {
                customerAdapter.UpdateInContext(model.Customer.ConvertToModel<CustomerViewModel, PotentialCustomer>());
                parentAdapter.UpdateInContext(model.PrimaryParent.ConvertToModel<ParentViewModel, Parent>());
                relationAdapter.UpdateInContext(relation);

                using (TransactionScope scope = new TransactionScope())
                {
                    context.ExecuteNonQuerySqlInContext();
                    scope.Complete();
                }
            }
        }

        #endregion

        #region api/potentialcustomers/updatecustomer
        [HttpGet]
        public EditableCustomerViewModel UpdateCustomer(string customerId)
        {
            var result = new EditableCustomerViewModel();
            result.Customer = customerAdapter.Load(customerId).ConvertToViewModel<PotentialCustomer, CustomerViewModel>();
            DataHelper.LoadDictionaryData(result.Dictionaries, typeof(PotentialCustomer));
            return result;
        }

        [HttpPost]
        public void UpdateCustomer(string customerId, EditableCustomerViewModel model)
        {
        }
        #endregion
    }
}