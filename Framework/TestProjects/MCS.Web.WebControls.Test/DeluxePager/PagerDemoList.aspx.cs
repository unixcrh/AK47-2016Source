using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using MCS.Web.WebControls;

namespace MCS.Web.WebControls.Test.DeluxePager
{
    public partial class PagerDemoList : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 30;
            if (!IsPostBack)
                this.InitializePageControl();
        }

        private void InitializePageControl()
        { 
            ddlControlType.Items.Insert(0,new ListItem("GridView","GridView"));
            ddlControlType.Items.Insert(1, new ListItem("Table", "Table"));
            ddlControlType.Items.Insert(2, new ListItem("DataGrid", "DataGrid"));
            ddlControlType.Items.Insert(3, new ListItem("DataList", "DataList"));
            ddlControlType.Items.Insert(4, new ListItem("DeluxeGrid", "DeluxeGrid"));
            ddlControlType.Items.Insert(5, new ListItem("DetailsView", "DetailsView"));
            ddlControlType.Items.Insert(6, new ListItem("FormView", "FormView"));
            ddlControlType.Items.Insert(7, new ListItem("Repeater", "Repeater"));

            txtControlID.Text = ddlControlType.SelectedValue + "2";
        }

        protected void btnSetProperties_Click(object sender, EventArgs e)
        {
            if (this.txtControlID.Text == null || this.txtControlID.Text.Trim().Length == 0)
                return;
            if (this.txtPageSize.Text == null || this.txtPageSize.Text.Trim().Length == 0)
                return;
            IControls controls = new Controls();
            PagerPropertiesCls ppc = new PagerPropertiesCls();

            ppc.BoundControlID = txtControlID.Text;
            ppc.GotoButtonText = txtGotoButtonText.Text == "" ? "Ìø×ªµ½" : txtGotoButtonText.Text;

            switch (ddlPageCodeMode.SelectedValue)
            {
                case "RecordCount":
                    ppc.PagerCodeMode = PagerCodeShowMode.RecordCount;
                    break;
                case "CurrentRecordCount":
                    ppc.PagerCodeMode = PagerCodeShowMode.CurrentRecordCount;
                    break;
                case "All":
                    ppc.PagerCodeMode = PagerCodeShowMode.All;
                    break;
            }

            switch (ddlPageMode.SelectedValue)
            {
                case "0":
                    ppc.PagerButtonsMode = DeluxePagerMode.Numeric;
                    break;
                case "1":
                    ppc.PagerButtonsMode = DeluxePagerMode.NextPreviousFirstLast;
                    break;
            }

            ppc.IsDataSourceControl = ddlIDataSource.SelectedValue == "yes" ? true : false;
            ppc.IsPagedControl = ddlPageControl.SelectedValue == "yes" ? true : false;

            if (txtFPIUrl.Text != "")
                ppc.FirstPageImageUrl = txtFPIUrl.Text;
            if (txtFPText.Text != "")
                ppc.FirstPageText = txtFPText.Text;
            if (txtLPIUrl.Text != "")
                ppc.LastPageImageUrl = txtLPIUrl.Text;
            if (txtLPText.Text != "")
                ppc.LastPageText = txtLPText.Text;
            if (txtNPIUrl.Text != "")
                ppc.NextPageImageUrl = txtNPIUrl.Text;
            if (txtNPText.Text != "")
                ppc.NextPageText = txtNPText.Text;
            if (txtPPIUrl.Text != "")
                ppc.PreviousPageImageUrl = txtPPIUrl.Text;
            if (txtPPText.Text != "")
                ppc.PreviousPageText = txtPPText.Text;
            if (txtPageSize != null && txtPageSize.Text.Trim().Length > 0)
                ppc.PageSize = Convert.ToInt32(txtPageSize.Text);
             
            Tools tools = new Tools();           

            byte[] b = Tools.SerializeBinary(ppc);
            string str = "";
            for (int i = 0; i < b.Length; i++)
            {
                str += b[i] + ";";
            }
            str = str.TrimEnd(';');
            hidPagerObject.Value = str;

            btnGoto.PostBackUrl = controls.InitializeControls(ddlControlType.SelectedValue, ppc);
            //this.btnGoto.OnClientClick = "window.open(this.PostBackUrl,'','top=0,left=0,width=780,height=480,menubar=no,toolbar=no,location=no,scrollbars=yes,resizable=yes,status=no');return false;";
            
        }

        protected void ddlControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bl = ddlIDataSource.SelectedValue == "yes" ? true : false;
            bool blPControl = ddlPageControl.SelectedValue == "yes" ? true : false;
            txtControlID.Text = "";
            if (ddlControlType.SelectedValue == "Table")
            {
                ddlPageControl.SelectedValue = "no";
                ddlPageControl.Enabled = false;
                txtControlID.Text = ddlControlType.SelectedValue + "1";
            }
            else if (ddlControlType.SelectedValue == "DataList" || ddlControlType.SelectedValue == "Repeater")
            {
                ddlPageControl.SelectedValue = "no";
                ddlPageControl.Enabled = false;
                if (bl)
                    txtControlID.Text = ddlControlType.SelectedValue + "2";
                else
                    txtControlID.Text = ddlControlType.SelectedValue + "3";
            }
            else
            {
                if (bl)
                    txtControlID.Text = ddlControlType.SelectedValue + "2";
                else
                    txtControlID.Text = ddlControlType.SelectedValue + "1";
                ddlPageControl.SelectedValue = "yes";
                ddlPageControl.Enabled = true;
            }
             
        }

        protected void ddlIDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bl = ddlIDataSource.SelectedValue == "yes" ? true : false;
            bool blPControl = ddlPageControl.SelectedValue == "yes" ? true : false;
            txtControlID.Text = "";
            if (blPControl)
            {
                if (bl)
                    txtControlID.Text = ddlControlType.SelectedValue + "2";
                else
                    txtControlID.Text = ddlControlType.SelectedValue + "1";
                ddlPageControl.SelectedValue = "yes";
                ddlPageControl.Enabled = true;
            }
            else
            {
                if (ddlControlType.SelectedValue == "DataList" || ddlControlType.SelectedValue == "Repeater")
                {
                    ddlPageControl.SelectedValue = "no";
                    ddlPageControl.Enabled = false;
                    if (bl)
                        txtControlID.Text = ddlControlType.SelectedValue + "2";
                    else
                        txtControlID.Text = ddlControlType.SelectedValue + "3";
                }
                else
                {
                    if (!bl)
                        txtControlID.Text = ddlControlType.SelectedValue + "3";
                }
            }
            if (ddlControlType.SelectedValue == "Table")
                txtControlID.Text = ddlControlType.SelectedValue + "1";
            //bool bl = ddlIDataSource.SelectedValue == "yes" ? true : false;
            //if (bl) 
            //    txtControlID.Text = ddlControlType.SelectedValue + "2";             
            //else
            //    txtControlID.Text = ddlControlType.SelectedValue + "1";
        }

        protected void ddlPageControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bl = ddlIDataSource.SelectedValue == "yes" ? true : false;
            bool blPControl = ddlPageControl.SelectedValue == "yes" ? true : false;
            txtControlID.Text = "";
            if (blPControl)
            {
                if (bl)
                    txtControlID.Text = ddlControlType.SelectedValue + "2";
                else
                    txtControlID.Text = ddlControlType.SelectedValue + "1";
                ddlPageControl.SelectedValue = "yes";
                ddlPageControl.Enabled = true;
            }
            else
            {
                if (!bl)
                    txtControlID.Text = ddlControlType.SelectedValue + "3";
            }
            if (ddlControlType.SelectedValue == "Table")
                txtControlID.Text = ddlControlType.SelectedValue + "1";
        }

         
    }
}
