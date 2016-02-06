using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Web.Library;
using MCS.Web.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
//using UEP.DataObjects.UserPool.Adapters;
//using UEP.DataObjects.UserPool.DataObjects;
using MCS.Library.Caching;

namespace MCS.Dynamics.Web.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class GenerateEntityAndMapping : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
        #region
        //private PropertyEditorSceneAdapter sceneAdapter = null;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定字段类型
                this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)), "EnumValue", "Description");
            }
        }

        public void BindData(RecordResultCollection data)
        {
            List<DataItem> dataSource = new List<DataItem>();
            try
            {
                data.GroupBy(p => p.TempFullPath).ForEach(p =>
                {
                    RecordResultCollection collection = new RecordResultCollection();
                    p.ForEach(f =>
                    {
                        collection.Add((RecordResult)f);
                    });

                    dataSource.Add(new DataItem()
                    {
                        Key = p.Key.Split('/').Last(),
                        Collection = collection
                    });
                });
            }
            catch (Exception)
            {
                throw new Exception("获取实体数据失败!");
            }

            rpt_Entities.DataSource = dataSource;
            rpt_Entities.DataBind();
        }

        protected void rpt_Entities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = e.Item.DataItem as DataItem;

                var grid = e.Item.FindControl("grid") as ClientGrid;
                if (grid != null)
                {
                    grid.InitialData = item.Collection;
                }
            }
        }

        public RecordResultCollection GetData(string tCode)
        {
            var srv = new Saplocalhost.WebServiceConnectSAPSoapClient();
            DataTable table = srv.GetEntityDefine(tCode);
            return ConvertTableToRecordResult(table);
        }

        public RecordResultCollection ConvertTableToRecordResult(DataTable table)
        {
            var resultList = new RecordResultCollection();

            var parentRows = table.Select("", "全路径 asc");
            int sortNumber = 0;
            foreach (var item in parentRows)
            {
                sortNumber++;
                var result = new RecordResult();

                result.SortNo = sortNumber;
                result.EntityName = Convert.ToString(item["实体名"]);
                result.EntityDesc = Convert.ToString(item["实体名"]);
                result.TempFullPath = Convert.ToString(item["全路径"]);
                result.DefaultValue = Convert.ToString(item["默认值"]);
                //result.IsMasterTable = Convert.ToString(item["主子标识"]) == "主" ? true : false;
                result.FieldName = Convert.ToString(item["字段名"]);
                int fileLenth = Convert.ToInt32(item["字段长度"]);
                result.FieldType = SAPFileMapping.SAPFiledTypeToUEPFiledType(Convert.ToString(item["字段类型"]), ref fileLenth);
                item["字段长度"] = fileLenth;
                result.FieldDesc = Convert.ToString(item["字段描述"]);
                result.FieldLength = int.Parse(Convert.ToString(item["字段长度"]));
                result.DecimalLength = int.Parse(Convert.ToString(item["字段小数长度"]));
                result.IsStruct = item["字段类型"].ToString().Equals("STRUCTURE");

                if (table.Columns.Contains("参数标识"))
                {
                    switch (item["参数标识"].ToString())
                    {
                        case "输入":
                            result.ParamDirection = ParamDirectionEnum.Import;
                            break;
                        case "输出":
                            result.ParamDirection = ParamDirectionEnum.Export;
                            break;
                        default:
                            result.ParamDirection = ParamDirectionEnum.NotKnown;
                            break;
                    }
                }

                resultList.Add(result);
            }
            return resultList;
        }

        //生成实体
        protected void btn_GenerateEntity_Click(object sender, EventArgs e)
        {
            //取TCode的定义信息
            var data = GetData(txt_TCode.Text.Trim());
            BindData(data);

        }
        //获取RFC结构
        protected void btn_GenerateEntityByRFC_Click(object sender, EventArgs e)
        {
            var srv = new Saplocalhost.WebServiceConnectSAPSoapClient();

            //SAPLoginParams Params = SAPLoginParams.GetConfig();
            //SAPClient sapInstance =
            //    SAPClientAdapter.Instance.LoadByID(ConfigurationManager.AppSettings["SAPInstanceId"].Trim());
            //SAPClient sapInstance = SAPClientAdapter.Instance.LoadByID(this.hidSAPInstanceId.Value.Trim());

            //var param = new Saplocalhost.SAPPara()
            //{
            //    ApplicationServer = sapInstance.ApplicationServer,
            //    Client = sapInstance.Client,
            //    Language = sapInstance.Language,
            //    Password = sapInstance.Password,
            //    SystemNumber = int.Parse(sapInstance.SystemNumber),
            //    User = sapInstance.User,
            //    MessageServerHost = sapInstance.MessageServerHost,
            //    MessageServerService = sapInstance.MessageServerService,
            //    LogonGroup = sapInstance.LogonGroup,
            //    AppServerService = sapInstance.AppServerService,
            //    SystemID = sapInstance.SystemID
            //};

            //DataTable table = srv.SAP_RFCParams_Get(txt_TCode.Text.Trim(), param);

            //RecordResultCollection collection = ConvertTableToRecordResult(table);
            //BindData(collection);
        }

        //保存实体及映射
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            RecordResultCollection result = new RecordResultCollection();

            for (int i = 0; i < this.rpt_Entities.Items.Count; i++)
            {
                var grid = (ClientGrid)this.rpt_Entities.Items[i].FindControl("grid");
                if (grid != null)
                {
                    var dataItem = grid.InitialData as RecordResultCollection;
                    dataItem.ForEach(result.Add);
                }
            }

            DEObjectOperations.InstanceWithPermissions.RecordResultGenerate(Request.QueryString["categoryid"], result);
            HttpContext.Current.Response.Write("<script>alert('添加成功！');window.returnValue=true;window.close();</script>");
        }

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    //Saplocalhost.WebServiceConnectSAP srv = new Saplocalhost.WebServiceConnectSAP();
        //    var srv = new Saplocalhost.WebServiceConnectSAPSoapClient();

        //    SAPLoginParams Params = SAPLoginParams.GetConfig();
        //    Saplocalhost.SAPPara param = new Saplocalhost.SAPPara()
        //    {
        //        ApplicationServer = Params.ApplicationServer,
        //        Client = Params.Client,
        //        Language = Params.Language,
        //        Password = Params.Password,
        //        SystemNumber = int.Parse(Params.SystemNumber),
        //        User = Params.User
        //    };
        //    DataTable table = srv.SAP_TableFiled_Get(txt_TCode.Text, param);
        //    RecordResultCollection collection = GetDatas(table);
        //    CreateEntities(collection);
        //}

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

    public class DataItem
    {
        public string Key { get; set; }

        public RecordResultCollection Collection { get; set; }
    }
}