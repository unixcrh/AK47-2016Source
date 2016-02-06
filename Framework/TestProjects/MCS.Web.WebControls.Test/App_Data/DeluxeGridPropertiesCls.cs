using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using MCS.Web.WebControls;

namespace MCS.Web.WebControls.Test 
{
    [Serializable]   
    public class DeluxeGridPropertiesCls
    {
        private int dataSourceMaxRow = 0;
        private string dataSourceType = "";
        private Object setObjectDataSource;
        private string gridTitle = "";
        private  Color titleColor;
        private bool pagerExportMode;
        private bool iDataSource = false;
        private string exportCommandArgument = "";
        private bool checkBoxAdd;
        private RowPosition checkBoxPosition;
        private RowPosition exportPosition;

		/// <summary>
		/// 是否多选
		/// </summary>
		public bool MultiSelect
		{
			get;
			set;
		}

        /// <summary>
        /// 指定GridView上导出内容的数据源的最大行数
        /// </summary> 
        public int DataSourceMaxRow
        {
            get { return this.dataSourceMaxRow; }
            set { this.dataSourceMaxRow = value; }
        }

        /// <summary>
        /// 指定GridView上导出内容的数据源类型
        /// </summary> 
        public string DataSourceType
        {
            get
            {
                return this.dataSourceType ?? "";
            }
            set { this.dataSourceType = value; }
        }
 
        public Object SetObjectDataSource
        {
            get
            {
                return this.setObjectDataSource ?? null;
            }
            set { this.setObjectDataSource = value; }
        }


        /// <summary>
        /// 指定GridView上显示标题内容
        /// </summary> 
        public string GridTitle
        {
            get { return this.gridTitle ?? "标题"; }
            set { this.gridTitle = value; }
        }

        /// <summary>
        /// 标题字体颜色
        /// </summary> 
        public Color TitleColor
        {
            get { return ParseColor(this.titleColor, Color.FromArgb(141, 143, 149)); }
            set { this.titleColor = value; }
        }

        //[Browsable(true),
        //Category("扩展"),
        //DefaultValue(FontUnit.Large),
        //Description("标题字体")]
        //public  TitleFont
        //{
        //    get { return (FontUnit)(ViewState["TitleFont"] ?? FontUnit.Large); }
        //    set { ViewState["TitleFont"] = value; }
        //}

        /// <summary>
        /// 设置是否显示导出部分
        /// </summary> 
        public bool PagerExportMode
        {
            get
            {
                object o = this.pagerExportMode;
                return o == null ? false : (bool)o;
            }
            set { this.pagerExportMode = value; }
        }
        /// <summary>
        /// 设置导出Text
        /// </summary> 
        public bool IDataSource
        {
            get
            {
                return this.iDataSource ;
            }
            set { this.iDataSource = value; }
        }

        /// <summary>
        /// 导出规则
        /// </summary> 
        public string ExportCommandArgument
        {
            get
            {
                return this.exportCommandArgument ?? string.Empty;
            }
            set { this.exportCommandArgument = value; }
        }


        /// <summary>
        /// 设置是否增加checkbox列
        /// </summary> 
        public bool CheckBoxAdd
        {
            get
            {
                object o = this.checkBoxAdd;
                return o == null ? false : (bool)o;
            }
            set { this.checkBoxAdd = value; }
        }

        /// <summary>
        /// 设置checkbox列的位置
        /// </summary>  
        public RowPosition CheckBoxPosition
        {
            get { return ParseRowPosition(this.checkBoxPosition); }
            set { this.checkBoxPosition = value; }
        }

        /// <summary>
        /// 设置导出列的位置
        /// </summary>  
        public RowPosition ExportPosition
        {
            get { return ParseRowPosition(this.exportPosition); }
            set { this.exportPosition = value; }
        }
 
        public static RowPosition ParseRowPosition(object o)
        {
            return ParseRowPosition(o, new RowPosition());
        }

         
        public static RowPosition ParseRowPosition(object o, RowPosition defaultValue)
        {
            if (o == null || o.ToString() == "")
            {
                return defaultValue;
            }
            try
            {
                return (RowPosition)Enum.Parse(typeof(RowPosition), o.ToString(), true);
            }
            catch
            {
                throw new FormatException("'" + o.ToString() + "' 类型改变失败");
            }
        }
        public static Color ParseColor(object o, Color defaultValue)
        {
            if (o == null || o.ToString() == "")
            {
                return defaultValue;
            }
            try
            {
                return ColorTranslator.FromHtml(o.ToString());
            }
            catch
            {
                throw new FormatException("'" + o.ToString() + "' can not be parsed as a color.");
            }
        }
    }
}
