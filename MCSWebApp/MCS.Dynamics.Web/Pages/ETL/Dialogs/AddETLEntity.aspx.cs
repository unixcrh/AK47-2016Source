using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Dynamics.Web.Dialogs;
using MCS.Dynamics.Web.Saplocalhost;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Facade;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Web.Library;
using MCS.Web.WebControls;
//using SAPLoginParams = MCS.Library.SOA.DataObjects.Dynamics.Configuration.SAPLoginParams;
using MCS.Library.Caching;
using UEP.DataObjects.UserPool.Adapters;
using UEP.DataObjects.UserPool.DataObjects;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class AddETLEntity : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
        #region
        string ITimeSceneDescriptor.NormalSceneName
        {
            get { return this.EditEnabled ? "Normal" : "ReadOnly"; }
        }

        string ITimeSceneDescriptor.ReadOnlySceneName
        {
            get { return "ReadOnly"; }
        }

        protected bool EditEnabled
        {
            get
            {
                var enabled = TimePointContext.Current.UseCurrentTime && Util.SuperVisiorMode;
                return enabled;
            }
        }

        public void AfterNormalSceneApplied()
        {
            //此处注释掉了   验证不成功时  页面还可以继续更改
            //this.okButton.Visible = this.Data != null && this.Data.Status == SchemaObjectStatus.Normal;
        }

        #endregion

        protected string scriptStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定字段类型
                this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)),
                    "EnumValue", "Description");
            }

        }

        //生成实体
        protected void btn_GenerateEntity_Click(object sender, EventArgs e)
        {
            try
            {
                //取TCode的定义信息
                var data = GetData(txt_TCode.Text.Trim());

                CreateEntities(data);
            }
            catch (Exception ex)
            {

                WebUtility.RegisterClientErrorMessage(ex);
            }



        }

        #region 获取SAP表定义信息

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public ETLSapTableResultCollection GetData(string sapTableName)
        {

            using (var srv = new Saplocalhost.WebServiceConnectSAPSoapClient())
            {
                // 新的取得SAP连接信息
                string sapInstanceId = this.hidSAPInstanceId.Value;
                sapInstanceId.CheckStringIsNullOrEmpty("sapInstanceId");
                SAPClient Params = SAPClientAdapter.Instance.LoadByID(sapInstanceId);

                Saplocalhost.SAPPara param = new Saplocalhost.SAPPara()
                {
                    ApplicationServer = Params.ApplicationServer,
                    Client = Params.Client,
                    Language = Params.Language,
                    Password = Params.Password,
                    SystemNumber = int.Parse(Params.SystemNumber),
                    User = Params.User,
                    AppServerService = Params.AppServerService,
                    LogonGroup = Params.LogonGroup,
                    MessageServerHost = Params.MessageServerHost,
                    MessageServerService = Params.MessageServerService,
                    SystemID = Params.SystemID
                };

                DataTable table = srv.SAP_TableFiled_Get(sapTableName, param);

                return GetDatas(table);
            }



        }

        public ETLSapTableResultCollection GetDatas(DataTable table)
        {

            ETLSapTableResultCollection resultList = new ETLSapTableResultCollection();

            var parentRows = table.Select();
            int sortNumber = 0;
            foreach (var item in parentRows)
            {
                sortNumber++;
                ETLSapTableResult result = new ETLSapTableResult();
                result.SortNo = sortNumber;
                result.EntityName = Convert.ToString(item["实体名"]);
                result.EntityDesc = Convert.ToString(item["实体描述"]);
                result.DefaultValue = Convert.ToString(item["默认值"]);
                result.IsMasterTable = Convert.ToString(item["主子标识"]) == "主" ? true : false;
                result.FieldName = Convert.ToString(item["字段名"]);
                result.IsPrimaryKey = bool.Parse(Convert.ToString(item["主键"]));
                if (result.IsPrimaryKey)
                {
                    result.IsEnable = true;
                }

                int fileLenth = Convert.ToInt32(item["字段长度"]);

                result.FieldType = SAPFileMapping.SAPFiledTypeToUEPFiledType(Convert.ToString(item["字段类型"]), ref fileLenth); ;

                item["字段长度"] = fileLenth;

                result.FieldDesc = Convert.ToString(item["字段描述"]);

                result.FieldLength = int.Parse(Convert.ToString(item["字段长度"]));

                resultList.Add(result);
            }
            return resultList;
        }

        private List<SapFieldDataItem> collectReapterData()
        {
            List<SapFieldDataItem> dataSource = new List<SapFieldDataItem>();
            if (rpt_Entities.Items.Count > 0)
            {

                for (int i = 0; i < this.rpt_Entities.Items.Count; i++)
                {
                    ETLSapTableResultCollection result = new ETLSapTableResultCollection();

                    var grid = (GridView)this.rpt_Entities.Items[i].FindControl("grid");

                    if (grid != null)
                    {
                        var dataItem = getDataByGridView(grid, i);

                        dataItem.ForEach(result.Add);

                        dataSource.Add(new SapFieldDataItem()
                        {
                            Key = grid.ToolTip,

                            Collection = result
                        });
                    }
                }
            }

            return dataSource;
        }

        public void CreateEntities(ETLSapTableResultCollection data)
        {
            data.NullCheck("SAP结构不能为Null");
            data.Any().FalseThrow("没有获取到SAP表结构");

            List<SapFieldDataItem> dataSource = new List<SapFieldDataItem>();

            //先将Reapter现在的数据源取出来
            dataSource = collectReapterData();

            //ETL实体获取表结构不可能获取多层级结构,即只能获取一张表的结构
            if (data.GroupBy(p => p.EntityName).Count() > 1)
            {
                WebUtility.ResponseShowClientErrorScriptBlock("获取表结构发生异常!", "", "错误");
                return;
            }

            if (dataSource.Any(p => p.Key.Equals(data.FirstOrDefault().EntityName)))
            {
                WebUtility.ResponseShowClientErrorScriptBlock("已存在相同的表!", "", "错误");
                return;
            }

            dataSource.Add(new SapFieldDataItem()
               {
                   Key = data.FirstOrDefault().EntityName,
                   SortNo = (dataSource.GroupBy(p => p.Key).Count()) + 1,
                   Collection = data
               });


            rpt_Entities.DataSource = dataSource.OrderBy(p => p.SortNo);
            rpt_Entities.DataBind();

            SetDorpDownListItem(dataSource);

        }

        protected void SetDorpDownListItem(List<SapFieldDataItem> dataSource)
        {
            //为引用表、引用字段下拉框赋值
            #region
            for (int i = 0; i < rpt_Entities.Items.Count; i++)
            {
                GridView grid = (GridView)rpt_Entities.Items[i].FindControl("grid");



                if (grid != null)
                {
                    grid.Columns[7].Visible = checkIsCommon.Checked;
                    grid.Columns[8].Visible = checkIsCommon.Checked;
                    if (i == 0)
                    {
                        // 如果是第一张表  则表 关联表和关联字段隐藏
                        grid.Columns[9].ItemStyle.CssClass = "commonData";
                        grid.Columns[9].HeaderStyle.CssClass = "commonData";
                        grid.Columns[10].ItemStyle.CssClass = "commonData";
                        grid.Columns[10].HeaderStyle.CssClass = "commonData";
                    }
                    string key = ((LinkButton)rpt_Entities.Items[i].FindControl("del_item")).CommandArgument;

                    for (var j = 0; j < grid.Rows.Count; j++)
                    {
                        HBDropDownList ddl_RefTableName = ((HBDropDownList)grid.Rows[j].FindControl("ddl_RefTableName"));

                        if (ddl_RefTableName != null)
                        {
                            ddl_RefTableName.Attributes["rptRowNum"] = i.ToString();
                            ddl_RefTableName.Attributes["gridRowNum"] = j.ToString();

                            List<string> ddl_dataSource = new List<string>();
                            ddl_dataSource.Add("--请选择--");
                            dataSource.Where(p => p.Key != key).Select(p => p.Key).ForEach(ddl_dataSource.Add);

                            ddl_RefTableName.DataSource = ddl_dataSource;
                            ddl_RefTableName.DataBind();

                            if (ddl_RefTableName.Items.FindByValue(dataSource[i].Collection[j].RefTableName) != null)
                            {
                                ddl_RefTableName.SelectedItem.Selected = false;
                                ddl_RefTableName.Items.FindByValue(dataSource[i].Collection[j].RefTableName).Selected = true;
                            }
                        }

                        HBDropDownList ddl_RefFieldName = ((HBDropDownList)grid.Rows[j].FindControl("ddl_RefFieldName"));
                        if (ddl_RefFieldName != null)
                        {
                            ddl_RefFieldName.DataSource = this.getRefFieldData(dataSource, dataSource[i].Collection[j].RefTableName);
                            ddl_RefFieldName.DataTextField = "Value";
                            ddl_RefFieldName.DataValueField = "Key";
                            ddl_RefFieldName.DataBind();

                            if (ddl_RefFieldName.Items.FindByValue(dataSource[i].Collection[j].RefFieldName) != null)
                            {
                                ddl_RefFieldName.SelectedItem.Selected = false;
                                ddl_RefFieldName.Items.FindByValue(dataSource[i].Collection[j].RefFieldName).Selected = true;
                            }
                        }
                    }
                }
            }

            #endregion
        }

        #endregion

        protected void rpt_Entities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = e.Item.DataItem as SapFieldDataItem;

                var grid = e.Item.FindControl("grid") as GridView;
                if (grid != null)
                {
                    grid.ToolTip = item.Key;
                    grid.DataSource = item.Collection;
                    grid.DataBind();
                }
            }
        }

        //保存实体及映射
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = collectReapterData();

                ETLSapTableResultCollection result = new ETLSapTableResultCollection();

                foreach (var sapFieldDataItem in dataSource)
                {
                    List<ETLSapTableResult> resultCollection = sapFieldDataItem.Collection.Where(p => p.IsEnable).ToList();

                    foreach (ETLSapTableResult etlSapTableResult in resultCollection)
                    {
                        result.Add(etlSapTableResult);
                    }
                }

                result.IsCommon = checkIsCommon.Checked;

                result.UepUsers = this.hidd_Users.Value;
                result.SAPInstanceId = this.hidSAPInstanceId.Value;
                result.SAPInstanceId.CheckStringIsNullOrEmpty("sapInstanceId");
                ETLDEObjectOperations.Instance.AddETLEntityBySapTable(Request.QueryString["categoryid"], result);

                // HttpContext.Current.Response.Write("<script>alert('添加成功');window.returnValue=true;window.close();</script>");
                //return;
                //Response.End();
                //scriptStr = "<script>alert('添加成功');window.returnValue=true;window.close();</script>";
                WebUtility.ResponseShowClientMessageScriptBlock("添加成功!", "", "添加成功");
                WebUtility.RegisterOnLoadScriptBlock(this, "window.returnValue=true;window.close();");

            }
            catch (Exception ex)
            {
                WebUtility.ResponseShowClientMessageScriptBlock("操作失败!", ex.Message, "操作失败");
            }
        }

        //取出GridView数据源中的绑定对象集合
        private ETLSapTableResultCollection getDataByGridView(GridView grid, int sortNo)
        {
            ETLSapTableResultCollection result = new ETLSapTableResultCollection();

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                ETLSapTableResult item = new ETLSapTableResult();
                item.EntityName = grid.ToolTip;
                item.IsEnable = ((CheckBox)grid.Rows[i].FindControl("cb_IsEnable")).Checked;
                item.IsPrimaryKey = ((CheckBox)grid.Rows[i].FindControl("cb_IsPrimaryKey")).Checked;
                item.FieldName = ((Label)grid.Rows[i].FindControl("lbl_FieldName")).Text;
                item.FieldDesc = ((Label)grid.Rows[i].FindControl("lbl_FieldDesc")).Text;

                FieldTypeEnum fieldType;
                FieldTypeEnum.TryParse(((Label)grid.Rows[i].FindControl("lbl_FieldType")).Text, out fieldType);
                item.FieldType = fieldType;

                item.FieldLength = Convert.ToInt32(((Label)grid.Rows[i].FindControl("lbl_FieldLength")).Text);
                item.IsIndex = ((CheckBox)grid.Rows[i].FindControl("cb_IsIndex")).Checked;
                if (checkIsCommon.Checked)
                {
                    item.IsKey = ((CheckBox)grid.Rows[i].FindControl("cb_IsKey")).Checked;
                    item.IsValue = ((CheckBox)grid.Rows[i].FindControl("cb_IsValue")).Checked;
                }

                item.RefTableName = ((HBDropDownList)grid.Rows[i].FindControl("ddl_RefTableName")).SelectedIndex == 0 ? string.Empty : ((HBDropDownList)grid.Rows[i].FindControl("ddl_RefTableName")).SelectedValue;
                item.RefFieldName = ((HBDropDownList)grid.Rows[i].FindControl("ddl_RefFieldName")).SelectedValue.Trim() == "" ? string.Empty : ((HBDropDownList)grid.Rows[i].FindControl("ddl_RefFieldName")).SelectedValue;
                item.SortNo = sortNo;
                result.Add(item);
            }

            return result;
        }

        //引用表发生改变时
        protected void ddl_RefTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            HBDropDownList ddl_RefTableName = (HBDropDownList)sender;
            HBDropDownList ddl_RefFieldName = null;

            GridView grid = (GridView)rpt_Entities.Items[Convert.ToInt32(ddl_RefTableName.Attributes["rptRowNum"])].FindControl("grid");
            if (grid != null)
                ddl_RefFieldName = (HBDropDownList)grid.Rows[Convert.ToInt32(ddl_RefTableName.Attributes["gridRowNum"])].FindControl("ddl_RefFieldName");

            if (ddl_RefFieldName == null || ddl_RefTableName == null)
                return;

            var dataSource = collectReapterData();

            ddl_RefFieldName.DataSource = this.getRefFieldData(dataSource, ddl_RefTableName.SelectedValue);
            ddl_RefFieldName.DataTextField = "Value";
            ddl_RefFieldName.DataValueField = "Key";
            ddl_RefFieldName.DataBind();
        }

        //根据引用表获取引用字段集合
        private Dictionary<string, string> getRefFieldData(List<SapFieldDataItem> dataSource, string refTableName)
        {

            Dictionary<string, string> result = new Dictionary<string, string>();

            var item = dataSource.FirstOrDefault(p => p.Key.Equals(refTableName));

            if (item != null)
            {
                item.Collection.ForEach(p => result.Add(p.FieldName, p.FieldDesc));
            }
            else
            {
                result.Add("", "--请选择--");
            }

            return result;
        }

        //Reapter的Command操作
        protected void rpt_Entities_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //移除某张表
            if (e.CommandName == "del_item")
            {
                var dataSource = collectReapterData();
                var item = dataSource.FirstOrDefault(p => p.Key.Equals(e.CommandArgument));
                dataSource.Remove(item);

                rpt_Entities.DataSource = dataSource;
                rpt_Entities.DataBind();

                SetDorpDownListItem(dataSource);
            }
        }

        protected void checkIsCommon_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rpt_Entities.Items.Count; i++)
            {
                GridView grid = (GridView)rpt_Entities.Items[i].FindControl("grid");

                if (grid != null)
                {
                    grid.Columns[7].Visible = checkIsCommon.Checked;
                    grid.Columns[8].Visible = checkIsCommon.Checked;
                }
            }
        }

        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["totalCount"] = LastQueryRowCount;
        }

        protected void objectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            LastQueryRowCount = (int)e.OutputParameters["totalCount"];
        }

        protected void gridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string sortExpression = this.gridUser.SortExpression;
            SortDirection sortDirection = this.gridUser.SortDirection;
            SetDeluxeGridSortInfo(sortExpression, sortDirection);
        }

        private void SetDeluxeGridSortInfo(string sortExp, SortDirection sortDirection)
        {
            string strSortDirection = "desc";
            ObjectContextCache.Instance.Add("deluxeGridSort", sortDirection + " " + strSortDirection);
        }

        private int LastQueryRowCount
        {
            get
            {
                return WebControlUtility.GetViewStateValue(ViewState, "LastQueryRowCount", -1);
            }
            set
            {
                WebControlUtility.SetViewStateValue(ViewState, "LastQueryRowCount", value);
            }
        }
    }

    /// <summary>
    /// 绑定Reapter所需的中间对象
    /// </summary>
    public class SapFieldDataItem
    {
        /// <summary>
        /// Reapter主键,即表名
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Reapter Item排序号
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 表字段集合
        /// </summary>
        public ETLSapTableResultCollection Collection { get; set; }
    }
}