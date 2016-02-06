using MCS.Library.Core;
using MCS.Library.Configuration;
using MCS.Web.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Dynamics.Web.DataSource
{
    /// <summary>
    /// 动态实体字段属性类型下拉列表数据源
    /// </summary>
    [Serializable]
    public class PropertyDropdownListDataSource
    {
        /// <summary>
        /// 获取属性类别数据源
        /// </summary>
        /// <returns></returns>
        public List<DropdownLitsItem> GetPropertyCategoryDataSource()
        {
            List<DropdownLitsItem> data = new List<DropdownLitsItem>();
            data.Add(new DropdownLitsItem("基本属性", "基本属性"));
            data.Add(new DropdownLitsItem("扩展属性", "扩展属性"));
            return data;
        }

        /// <summary>
        /// 获取编辑控件数据源
        /// </summary>
        /// <returns></returns>
        public List<DropdownLitsItem> GetPropertyEditorDataSource()
        {
            List<DropdownLitsItem> data = new List<DropdownLitsItem>();
            foreach(TypeConfigurationElement element in PropertyEditorSettings.GetConfig().Editors)
            {
                if (element.Description.IsNotEmpty())
                {
                    data.Add(new DropdownLitsItem(element.Description, element.Name));
                }
            }
            return data;
        }
    }

    /// <summary>
    /// 下拉列表数据项
    /// </summary>
    [Serializable]
    public class DropdownLitsItem
    {
        public DropdownLitsItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}