using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;

namespace MCS.Web.WebControls.Test
{
    public class Controls:IControls 
    {
        public Controls()
        { }

        public string InitializeControls(string controlName,PagerPropertiesCls ppc)
        {
            string url = "";
            switch (controlName.ToLower())
            {
                case "gridview":
                    url = "~/DeluxePager/PagerForGridView.aspx";
                    break;
                case "table":
                    url = "~/DeluxePager/PagerForTable.aspx";
                    break;
                case "datagrid":
                    url = "~/DeluxePager/PagerToDataGrid.aspx";
                    break;
                case "datalist":
                    url = "~/DeluxePager/PagerToDataList.aspx";
                    break;
                case "deluxegrid":
                    url = "~/DeluxePager/PagerToDeluxeGrid.aspx";
                    break;
                case "detailsview":
                    url = "~/DeluxePager/PagerToDetailsView.aspx";
                    break;
                case "formview":
                    url = "~/DeluxePager/PagerToFormView.aspx";
                    break;
                case "repeater":
                    url = "~/DeluxePager/PagerToRepeater.aspx";
                    break;
                case "reportviewer":
                    url = "~/DeluxePager/PagerToReportViewer.aspx";
                    break;
            }
            return url;
        }

        private void GetGridViewUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetTableUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetDataGridUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetDataListUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetDeluxeGridUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetDetailsViewUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetFormViewUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetRepeaterUrl(PagerPropertiesCls ppc)
        {
        }

        private void GetReportViewerUrl(PagerPropertiesCls ppc)
        {
        }

    }
}
