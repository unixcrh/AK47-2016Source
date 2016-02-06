using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls.Test.DeluxeSelect
{
    public partial class DeluxeSelectTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelectItem s = new SelectItem();
                s.SelectListBoxText = "t";
                s.SelectListBoxValue = "t";
                this.ctrlDeluxeSelect.SelectedItems.Add(s);
            }

            #region 待测属性
            //ccDeluxeSelect.ShowSelectButton;              //'选择'按钮是否显示
            //ccDeluxeSelect.SelectButtonText;              //'选择'按钮的Text
            //ccDeluxeSelect.SelectButtonCssClass;          //'选择'按钮的Css样式

            //ccDeluxeSelect.CancelButtonText;              //'取消'按钮的Text
            //ccDeluxeSelect.CancelButtonCssClass;          //'取消'按钮的Css样式

            //ccDeluxeSelect.ShowAllSelectButton;           //'全部选择'按钮是否显示
            //ccDeluxeSelect.SelectAllButtonText;           //'全部选择'按钮的Text

            //ccDeluxeSelect.CancelAllButtonText;           //'全部取消'按钮的Text

            //ccDeluxeSelect.MoveButtonCssClass;            //'上下移'按钮的Css样式

            //ccDeluxeSelect.AppendDataBoundItems;          //是否附加Items
            //ccDeluxeSelect.ButtonItems;                   //按钮的数据集
            //ccDeluxeSelect.ClientID;

            //ccDeluxeSelect.DataSourceResult;              //数据源
            //ccDeluxeSelect.DataSourseSortField;           //数据源的SortFiled
            //ccDeluxeSelect.DataSourseTextField;           //数据源的TextFiled
            //ccDeluxeSelect.DataSourseValueField;          //数据源的ValueFiled
            //ccDeluxeSelect.DataTextFormatString;          //数据源的TextFiled的Format

            //ccDeluxeSelect.GetSelectDesignHTML();

            //ccDeluxeSelect.SelectedItems;                 //已选择列表的数据集
            //ccDeluxeSelect.SelectedListCssClass;          //已选择列表的Css样式
            //ccDeluxeSelect.SelectedListSortDirection;     //已选择列表排序方式（升序、降序）
            //ccDeluxeSelect.SelectedSelectionMode;         //已选择列表的选择模式（单选多选）

            //ccDeluxeSelect.CandidateItems;                //待选择列表的数据集
            //ccDeluxeSelect.CandidateListCssClass;         //待选择列表的Css样式
            //ccDeluxeSelect.CandidateListSortDirection;    //待选择列表排序方式（升序、降序）
            //ccDeluxeSelect.CandidateSelectionMode;        //待选择列表的选择模式（单选多选）

            //-------------------------------------------------------------------------------
            //ctrlDeluxeSelect.ButtonItems;                 按钮类别数据集合
            //ctrlDeluxeSelect.GetSelectDesignHTML();

            //ctrlDeluxeSelect.ShowSelectButton;            选择按钮是否显示
            //ctrlDeluxeSelect.ShowSelectAllButton;         全部选择按钮是否显示
            //ctrlDeluxeSelect.SelectButtonText;            选择按钮的Text
            //ctrlDeluxeSelect.SelectAllButtonText;         全部选择按钮的Text
            //ctrlDeluxeSelect.CancelButtonText;            取消按钮的Text
            //ctrlDeluxeSelect.CancelAllButtonText;         全部取消按钮的Text

            //ctrlDeluxeSelect.SelectButtonCssClass;        '选择'按钮的Css样式
            //ctrlDeluxeSelect.MoveButtonCssClass;          '上下移'按钮的Css样式 

            //ctrlDeluxeSelect.MoveOption;                  是否允许移动列表数据项

            //ctrlDeluxeSelect.DataSourseSortField;         数据源的SortFiled
            //ctrlDeluxeSelect.DataSourseTextField;         数据源的TextFiled
            //ctrlDeluxeSelect.DataSourseValueField;        数据源的ValueFiled

            //ctrlDeluxeSelect.SelectedItems;               已选择列表的数据集合
            //ctrlDeluxeSelect.SelectedListCssClass;        已选择列表的Css样式
            //ctrlDeluxeSelect.SelectedListSortDirection;   已选择列表排序方式（升序\降序）
            //ctrlDeluxeSelect.SelectedSelectionMode;       已选择列表的选择模式（单选\多选）

            //ctrlDeluxeSelect.CandidateItems;              待选择列表的数据集
            //ctrlDeluxeSelect.CandidateListCssClass;       待选择列表的Css样式
            //ctrlDeluxeSelect.CandidateListSortDirection;  待选择列表排序方式（升序\降序）
            //ctrlDeluxeSelect.CandidateSelectionMode;      待选择列表的选择模式（单选\多选）
            #endregion
        }

        private string BuildControlInfo()
        {
            StringBuilder strbInfo = new StringBuilder(512);

            strbInfo.Append("<cc1:DeluxeSelect ID=\"ctrlDeluxeSelect\" runat=\"server\"\n DataSourseSortField=\"\"\n DataSourseTextField=\"\"\n DataSourseValueField=\"\"");

            if (!ckbShowSelectButton.Checked)
            {
                strbInfo.Append("\n ShowSelectButton=\"" + ctrlDeluxeSelect.ShowSelectButton.ToString() + "\" ");
            }
            if (!ckbShowSelectAllButton.Checked)
            {
                strbInfo.Append("\n ShowSelectAllButton=\"" + ctrlDeluxeSelect.ShowSelectAllButton.ToString() + "\" ");
            }

            strbInfo.Append("\n SelectButtonText=\"" + ctrlDeluxeSelect.SelectButtonText.ToString() + "\" ");
            strbInfo.Append("\n SelectAllButtonText=\"" + ctrlDeluxeSelect.SelectAllButtonText.ToString() + "\" ");

            strbInfo.Append("\n CancelButtonText=\"" + ctrlDeluxeSelect.CancelButtonText.ToString() + "\" ");
            strbInfo.Append("\n CancelAllButtonText=\"" + ctrlDeluxeSelect.CancelAllButtonText.ToString() + "\" ");

            if (ddlSelectButtonCssClass.Text != "Default")
            {
                strbInfo.Append("\n SelectButtonCssClass=\"" + ctrlDeluxeSelect.SelectButtonCssClass.ToString() + "\" ");
            }
            if (ddlMoveButtonCssClass.Text != "Default")
            {
                strbInfo.Append("\n MoveButtonCssClass=\"" + ctrlDeluxeSelect.MoveButtonCssClass.ToString() + "\" ");
            }

            strbInfo.Append("\n MoveOption=\"" + ctrlDeluxeSelect.MoveOption.ToString() + "\" ");

            if (ddlSelectedListCssClass.Text != "Default")
            {
                strbInfo.Append("\n SelectedListCssClass=\"" + ctrlDeluxeSelect.SelectedListCssClass.ToString() + "\" ");
            }
            strbInfo.Append("\n SelectedListSortDirection=\"" + ctrlDeluxeSelect.SelectedListSortDirection.ToString() + "\" ");
            strbInfo.Append("\n SelectedSelectionMode=\"" + ctrlDeluxeSelect.SelectedSelectionMode.ToString() + "\" ");

            if (ddlCandidateListCssClass.Text != "Default")
            {
                strbInfo.Append("\n CandidateListCssClass=\"" + ctrlDeluxeSelect.CandidateListCssClass.ToString() + "\" ");
            }
            strbInfo.Append("\n CandidateListSortDirection=\"" + ctrlDeluxeSelect.CandidateListSortDirection.ToString() + "\" ");
            strbInfo.Append("\n CandidateSelectionMode=\"" + ctrlDeluxeSelect.CandidateSelectionMode.ToString() + "\" ");

            strbInfo.Append("\n/>");

            return strbInfo.ToString();
        }

        protected void btnSetProperties_Click(object sender, EventArgs e)
        {
            ctrlDeluxeSelect.ShowSelectButton = ckbShowSelectButton.Checked;
            ctrlDeluxeSelect.ShowSelectAllButton = ckbShowSelectAllButton.Checked;

            ctrlDeluxeSelect.SelectButtonText = ddlSelectButtonText.Text;
            ctrlDeluxeSelect.SelectAllButtonText = ddlSelectAllButtonText.Text;

            ctrlDeluxeSelect.CancelButtonText = ddlCancelButtonText.Text;
            ctrlDeluxeSelect.CancelAllButtonText = ddlCancelAllButtonText.Text;

            if (ddlSelectButtonCssClass.Text != "Default")
            {
                ctrlDeluxeSelect.SelectButtonCssClass = ddlSelectButtonCssClass.Text;
            }
            if (ddlMoveButtonCssClass.Text != "Default")
            {
                ctrlDeluxeSelect.MoveButtonCssClass = ddlMoveButtonCssClass.Text;
            }

            ctrlDeluxeSelect.MoveOption = ckbMoveOption.Checked;

            //已选择列表
            if (ddlSelectedListCssClass.Text != "Default")
            {
                ctrlDeluxeSelect.SelectedListCssClass = ddlSelectedListCssClass.Text;
            }
            if (ddlSelectedListSortDirection.Text == "升序")
            {
                ctrlDeluxeSelect.SelectedListSortDirection = System.ComponentModel.ListSortDirection.Ascending;
            }
            else if (ddlSelectedListSortDirection.Text == "降序")
            {
                ctrlDeluxeSelect.SelectedListSortDirection = System.ComponentModel.ListSortDirection.Descending;
            }
            if (ddlSelectedSelectionMode.Text == "单选")
            {
                ctrlDeluxeSelect.SelectedSelectionMode = false;
            }
            else if (ddlSelectedSelectionMode.Text == "多选")
            {
                ctrlDeluxeSelect.SelectedSelectionMode = true;
            }

            //待选择列表
            if (ddlCandidateListCssClass.Text != "Default")
            {
                ctrlDeluxeSelect.CandidateListCssClass = ddlCandidateListCssClass.Text;
            }
            if (ddlCandidateListSortDirection.Text == "升序")
            {
                ctrlDeluxeSelect.CandidateListSortDirection = System.ComponentModel.ListSortDirection.Ascending;
            }
            else if (ddlCandidateListSortDirection.Text == "降序")
            {
                ctrlDeluxeSelect.CandidateListSortDirection = System.ComponentModel.ListSortDirection.Descending;
            }
            if (ddlCandidateSelectionMode.Text == "单选")
            {
                ctrlDeluxeSelect.CandidateSelectionMode = false;
            }
            else if (ddlCandidateSelectionMode.Text == "多选")
            {
                ctrlDeluxeSelect.CandidateSelectionMode = true;
            }

            ctrlDeluxeSelectHtmlShow.Value = this.BuildControlInfo();
        }

        protected void btnSelectedAdd_Click(object sender, EventArgs e)
        {
            SelectItem sitemTrans = new SelectItem(); ;
            sitemTrans.SelectListBoxText = txbSelectedText.Text;
            sitemTrans.SelectListBoxValue = txbSelectedValue.Text;
            sitemTrans.SelectListBoxSortColumn = txbSelectedSort.Text;
            sitemTrans.Locked = ckbSelectedLocked.Checked;
            ctrlDeluxeSelect.SelectedItems.Add(sitemTrans);
        }

        protected void btnCandidateAdd_Click(object sender, EventArgs e)
        {
            SelectItem sitemTrans = new SelectItem(); ;
            sitemTrans.SelectListBoxText = txbCandidateText.Text;
            sitemTrans.SelectListBoxValue = txbCandidateValue.Text;
            sitemTrans.SelectListBoxSortColumn = txbCandidateSort.Text;
            sitemTrans.Locked = ckbCandidateLocked.Checked;
            ctrlDeluxeSelect.CandidateItems.Add(sitemTrans);
        }

        protected void btnSetDataSource_Click(object sender, EventArgs e)
        {
            DataTable dtTrans = new DataTable();
            dtTrans.Columns.Add("Column1", typeof(String));
            dtTrans.Columns.Add("Column2", typeof(String));
            dtTrans.Columns.Add("Column3", typeof(String));

            DataRow drTrans;
            drTrans = dtTrans.NewRow();
            drTrans["Column1"] = "北京海关";
            drTrans["Column2"] = "0001";
            drTrans["Column3"] = "1";
            dtTrans.Rows.Add(drTrans);

            drTrans = dtTrans.NewRow();
            drTrans["Column1"] = "南宁海关";
            drTrans["Column2"] = "7200";
            drTrans["Column3"] = "2";
            dtTrans.Rows.Add(drTrans);

            drTrans = dtTrans.NewRow();
            drTrans["Column1"] = "拱北海关";
            drTrans["Column2"] = "5700";
            drTrans["Column3"] = "3";
            dtTrans.Rows.Add(drTrans);

            ctrlDeluxeSelect.DataSource = dtTrans;

            ctrlDeluxeSelect.DataSourseTextField = "Column1";
            ctrlDeluxeSelect.DataSourseValueField = "Column2";
            ctrlDeluxeSelect.DataSourseSortField = "Column3";
        }

        protected void btnButtonItemAdd_Click(object sender, EventArgs e)
        {
            ButtonItem btnitmTrans = new ButtonItem();
            btnitmTrans.ButtonName = txbButtonName.Text;
            btnitmTrans.ButtonSortID = Convert.ToInt16(txbButtonSortID.Text);
            btnitmTrans.ButtonTypeMaxCount = Convert.ToInt16(txbButtonTypeMaxCount.Text);
            btnitmTrans.ButtonType = ButtonItem.ButtonTypeMode.Button;
            btnitmTrans.ButtonCssClass = ddlButtonCssClass.Text;
            ctrlDeluxeSelect.ButtonItems.Add(btnitmTrans);
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            this.Label1.Text =
                string.Format("InsertedItems {0} <br/> DeletedItems {1}",
                this.ctrlDeluxeSelect.DeltaItems.InsertedItems.Count.ToString(),
                this.ctrlDeluxeSelect.DeltaItems.DeletedItems.Count.ToString());
        }
    }
}
