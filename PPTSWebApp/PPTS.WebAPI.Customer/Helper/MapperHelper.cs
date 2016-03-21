using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;

namespace PPTS.WebAPI.Customer.Helper
{
    public static class MapperHelper
    {
        #region ConvertToViewModel
        public static TViewModel ConvertToViewModel<TModel, TViewModel>(this TModel model)
        {
            return model != null ? Mapper.Map<TViewModel>(model) : default(TViewModel);
        }

        #endregion

        #region ConvertToModel
        public static TModel ConvertToModel<TViewModel, TModel>(this TViewModel viewModel)
        {
            return viewModel != null ? Mapper.Map<TModel>(viewModel) : default(TModel);
        }

        #endregion

        #region ConvertToViewModelList

        public static IEnumerable<TViewModel> ConvertToViewModelList<TModel, TViewModel>(this IEnumerable<TModel> models)
        {
            return models != null ? Mapper.Map<IEnumerable<TViewModel>>(models) : default(IEnumerable<TViewModel>);
        }

        #endregion

        #region ConvertToModelList

        public static IEnumerable<TModel> ConvertToModelList<TViewModel, TModel>(this IEnumerable<TViewModel> viewModels)
        {
            return viewModels != null ? Mapper.Map<IEnumerable<TModel>>(viewModels) : default(IEnumerable<TModel>);
        }

        #endregion

        #region 实体映射扩展方法  

        public static T GetEntity<T>(this IDataReader reader)
        {
            return Mapper.Map<T>(reader);
        }

        public static T GetEntity<T>(this DataSet dataSet)
        {
            if (dataSet.IsNullOrEmptyDataSet())
            {
                return default(T);
            }
            var reader = dataSet.Tables[0].CreateDataReader();

            return Mapper.Map<T>(reader);
        }

        public static T GetEntity<T>(this DataTable dataTable)
        {
            if (dataTable.IsNullOrEmptyDataTable())
            {
                return default(T);
            }
            var reader = dataTable.CreateDataReader();

            return Mapper.Map<T>(reader);
        }
        #endregion

        #region 辅助扩展方法

        public static bool IsNullOrEmptyDataSet(this DataSet dataSet)
        {
            return dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0;
        }

        public static bool IsNullOrEmptyDataTable(this DataTable dataTable)
        {
            return dataTable == null || dataTable.Rows.Count == 0;
        }

        #endregion
    }
}
