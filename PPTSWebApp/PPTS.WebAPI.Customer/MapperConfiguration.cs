using AutoMapper;
using PPTS.WebAPI.Customer.ViewModels;

namespace PPTS.WebAPI.Customer
{
    public static class MapperConfiguration
    {
        /// <summary>  
        /// 注册 Mapper 转换规则约定  
        /// </summary>  
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<CustomerMappingProfile>();
            });
        }
    }
}