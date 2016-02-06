using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCS.Web.WebControls;
 
namespace MCS.Web.WebControls.Test
{
    [Serializable]
    public class PagerPropertiesCls
    {
        private string boundControlID = "";

        private string firstPageImageUrl = "";
        private string lastPageImageUrl = "";
        private string nextPageImageUrl = "";
        private string previousPageImageUrl = "";
        private string firstPageText = "";
        private string lastPageText = "";
        private string nextPageText ="";
        private string previousPageText ="";
        private int pageSize = 0;

        private int pageButtonCount = 0;

        private string gotoButtonText = "";

        private PagerCodeShowMode pagerCodeMode;

        private DeluxePagerMode pagerButtonsMode;
        
        private bool isDataSourceControl = true;

        private bool pageControl = false;

        public string BoundControlID
        {
            get { return this.boundControlID; }
            set { this.boundControlID = value; }
        }

        public string FirstPageImageUrl
        {
            get { return this.firstPageImageUrl; }
            set { this.firstPageImageUrl = value; }
        }

        public string LastPageImageUrl
        {
            get { return this.lastPageImageUrl; }
            set { this.lastPageImageUrl = value; }
        }

        public string NextPageImageUrl
        {
            get { return this.nextPageImageUrl; }
            set { this.nextPageImageUrl = value; }
        }

        public string PreviousPageImageUrl
        {
            get { return this.previousPageImageUrl; }
            set { this.previousPageImageUrl = value; }
        }

        public string FirstPageText
        {
            get { return this.firstPageText; }
            set { this.firstPageText = value; }
        }

        public string LastPageText
        {
            get { return this.lastPageText; }
            set { this.lastPageText = value; }
        }

        public string NextPageText
        {
            get { return this.nextPageText; }
            set { this.nextPageText = value; }
        }

        public string PreviousPageText
        {
            get { return this.previousPageText; }
            set { this.previousPageText = value; }
        }
        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }
        public int PageButtonCount
        {
            get { return this.pageButtonCount; }
            set { this.pageButtonCount = value; }
        }

        public string GotoButtonText
        {
            get { return this.gotoButtonText; }
            set { this.gotoButtonText = value; }
        }

        public PagerCodeShowMode PagerCodeMode
        {
            get { return ParsePageCodeShowMode(this.pagerCodeMode);}
            set { this.pagerCodeMode = value; }
        }

        public DeluxePagerMode PagerButtonsMode
        {
            get { return ParsePagerButtonsMode(this.pagerButtonsMode); }
            set 
            {
                if ((value < DeluxePagerMode.Numeric) || (value > DeluxePagerMode.NextPreviousFirstLast))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (PagerButtonsMode != value)
                {
                    this.pagerButtonsMode = value; 
                } 
            }
        }

        public bool IsDataSourceControl
        {
            get { return this.isDataSourceControl; }
            set { this.isDataSourceControl = value; }
        }

        public bool IsPagedControl
        {
            get { return this.pageControl; }
            set { this.pageControl = value; }
        }

        public static PagerCodeShowMode ParsePageCodeShowMode(object o)
        {
            return ParsePageCodeShowMode(o, new PagerCodeShowMode());
        }

        public static DeluxePagerMode ParsePagerButtonsMode(object o)
        {
            return ParsePagerButtonsMode(o, new DeluxePagerMode());
        }

        /// <summary>
        /// 页码显示模式
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static PagerCodeShowMode ParsePageCodeShowMode(object o, PagerCodeShowMode defaultValue)
        {
            if (o == null || o.ToString() == "")
            {
                return defaultValue;
            }
            try
            {
                return (PagerCodeShowMode)Enum.Parse(typeof(PagerCodeShowMode), o.ToString(), true);
            }
            catch
            {
                throw new FormatException("'" + o.ToString() + "' 类型改变失败");
            }
        }

        /// <summary>
        /// 分页模式
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DeluxePagerMode ParsePagerButtonsMode(object o, DeluxePagerMode defaultValue)
        {
            if (o == null || o.ToString() == "")
            {
                return defaultValue;
            }
            try
            {
                return (DeluxePagerMode)Enum.Parse(typeof(DeluxePagerMode), o.ToString(), true);
            }
            catch
            {
                throw new FormatException("'" + o.ToString() + "' 类型改变失败");
            }
        }
        public void InitializeDeluxePager(MCS.Web.WebControls.DeluxePager DeluxePager1, PagerPropertiesCls ppc)
        {
            //MCS.Web.WebControls.DeluxePager DeluxePager1 = new MCS.Web.WebControls.DeluxePager();
            DeluxePager1.GotoButtonText = ppc.GotoButtonText;
            DeluxePager1.IsDataSourceControl = ppc.IsDataSourceControl;
            DeluxePager1.IsPagedControl = ppc.IsPagedControl;
            DeluxePager1.PageCodeShowMode = ppc.PagerCodeMode;

            if (ppc.BoundControlID != "")
                DeluxePager1.DataBoundControlID = ppc.BoundControlID;
            if (ppc.FirstPageImageUrl != "")
                DeluxePager1.PagerSettings.FirstPageImageUrl = ppc.FirstPageImageUrl;
            if (ppc.FirstPageText != "")
                DeluxePager1.PagerSettings.FirstPageText = ppc.FirstPageText;
            if (ppc.LastPageImageUrl != "")
                DeluxePager1.PagerSettings.LastPageImageUrl = ppc.LastPageImageUrl;
            if (ppc.LastPageText != "")
                DeluxePager1.PagerSettings.LastPageText = ppc.LastPageText;
            if (ppc.NextPageImageUrl != "")
                DeluxePager1.PagerSettings.NextPageImageUrl = ppc.NextPageImageUrl;
            if (ppc.NextPageText != "")
                DeluxePager1.PagerSettings.NextPageText = ppc.NextPageText;
            if (ppc.PreviousPageImageUrl != "")
                DeluxePager1.PagerSettings.PreviousPageImageUrl = ppc.PreviousPageImageUrl;
            if (ppc.PreviousPageText != "")
                DeluxePager1.PagerSettings.PreviousPageText = ppc.PreviousPageText;
            DeluxePager1.PageSize = ppc.PageSize;

            DeluxePager1.PagerSettings.Mode = ppc.PagerButtonsMode; 
        }
    }
}
