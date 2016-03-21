using AutoMapper;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customer.ViewModels.Customer;

namespace PPTS.WebAPI.Customer.ViewModels
{
    public class CustomerMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PotentialCustomer, CustomerViewModel>();
            CreateMap<CustomerViewModel, PotentialCustomer>();

            CreateMap<ParentViewModel, Parent>();
            CreateMap<Parent, ParentViewModel>();
        }
    }
}