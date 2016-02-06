using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library;
using MCS.Web.Library.Script;
using MCS.Web.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Web.Library.MVC;
using MCS.Dynamics.Web.Dialogs;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Facade;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Others;
using PetroChina.UEP.Web.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class EditETLEntity : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
        #region
        private bool sceneDirty = true;
        private bool enabled = false;
        private PropertyEditorSceneAdapter sceneAdapter = null;

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
                if (this.sceneDirty)
                {
                    this.enabled = TimePointContext.Current.UseCurrentTime;

                    if (this.enabled && Util.SuperVisiorMode == false && this.sceneAdapter != null)
                    {
                        this.enabled = this.sceneAdapter.IsEditable();
                    }

                    this.sceneDirty = false;
                }

                return this.enabled;
            }
        }

        /// <summary>
        /// 基础数据实体
        /// </summary>
        private DESchemaObjectBase Data
        {
            get;
            set;
        }

        //操作类型
        private SCObjectOperationMode OperationMode
        {
            get
            {
                return WebControlUtility.GetViewStateValue(this.ViewState, "OperationMode", SCObjectOperationMode.Add);
            }
            set
            {
                WebControlUtility.SetViewStateValue(this.ViewState, "OperationMode", value);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!TimePointContext.Current.UseCurrentTime)
            {
                this.grid.ReadOnly = true;
            }
            //给隐藏域赋值
            Dictionary<string, string> enumValueKey = new Dictionary<string, string>();
            Dictionary<string, string> enumKeyValue = new Dictionary<string, string>();

            Type fileTypes = typeof(FieldTypeEnum);
            foreach (string s in Enum.GetNames(fileTypes))
            {
                FieldTypeEnum myEnum = (FieldTypeEnum)Enum.Parse(typeof(FieldTypeEnum), s);
                enumValueKey.Add(((int)myEnum).ToString(), s);
                enumKeyValue.Add(s, ((int)myEnum).ToString());
            }

            HF_EnumValueKey.Value = JSONSerializerExecute.Serialize(enumValueKey, typeof(object));
            HF_EnumKeyValue.Value = JSONSerializerExecute.Serialize(enumKeyValue, typeof(object));
            if (this.IsPostBack == false && this.IsCallback == false)
                ControllerHelper.ExecuteMethodByRequest(this);

            this.PropertyEditorRegister();

            WebUtility.RequiredScript(typeof(ClientGrid));
            ETLEntity etlEntity = InitETLEntity(Request.QueryString["ID"], Request.QueryString["CategoryID"]);
            this.bindingControl.Data = etlEntity;


            if (!IsPostBack)
            {
                //绑定字段类型
                this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)), "Name", "Description");
                this.gridUepUsers.InitialData = etlEntity.sapInstanceParams;
                this.lastUpdateTime.Value = etlEntity.LastUpdateTime == DateTime.MinValue ? Convert.ToDateTime("1900-01-01 00:00:00") : etlEntity.LastUpdateTime;

                List<DBInfo> dbBaseInfo = new List<DBInfo>();
                //判断数据库登录主键是否存在
                if (etlEntity.TargetConnCode.IsNullOrEmpty())
                {
                    //数据库登录信息显示
                    if (!etlEntity.ServerAddress.IsNullOrEmpty() && !etlEntity.DataBase.IsNullOrEmpty() && !etlEntity.Uid.IsNullOrEmpty())
                    {
                        List<DBInfo> result = DBInfoAdapter.Instance.DBInfoIsExit(etlEntity.Uid, etlEntity.ServerAddress, etlEntity.DataBase);

                        if (result.Count == 0)
                        {
                            var dbInfo = new DBInfo();
                            string code = Guid.NewGuid().ToString();
                            //数据库登录信息的主键
                            dbInfo.DBCode = code;
                            //数据库登录账号
                            dbInfo.DBLoginID = etlEntity.Uid;
                            //数据库登录地址
                            dbInfo.DBAddr = etlEntity.ServerAddress;
                            //数据库名称
                            dbInfo.DBName = etlEntity.DataBase;
                            //数据库登录密码
                            dbInfo.DBPassword = etlEntity.Pwd;
                            DBInfoAdapter.Instance.Update(dbInfo);
                        }
                    }

                    //没数据时的显示
                    var showDbinfo = new DBInfo();
                    showDbinfo.DBCode = "";
                    showDbinfo.DBLoginID = "";
                    showDbinfo.DBAddr = "";
                    showDbinfo.DBName = "";
                    showDbinfo.DBPassword = "";
                    dbBaseInfo.Add(showDbinfo);

                }
                else
                {
                    //绑定登录数据库信息主键
                    HF_TargetConnCode.Value = etlEntity.TargetConnCode;
                    if (DBInfoAdapter.Instance.GetByID(etlEntity.TargetConnCode) != null)
                    {
                        dbBaseInfo.Add(DBInfoAdapter.Instance.GetByID(etlEntity.TargetConnCode));
                    }
                    else
                    {
                        //没数据时的显示
                        var showDbinfo = new DBInfo();
                        showDbinfo.DBCode = "";
                        showDbinfo.DBLoginID = "";
                        showDbinfo.DBAddr = "";
                        showDbinfo.DBName = "";
                        showDbinfo.DBPassword = "";
                        dbBaseInfo.Add(showDbinfo);
                    }

                }
                this.gridDBInfo.InitialData = dbBaseInfo;

                string etlConStr = string.Empty;
                try
                {
                    etlConStr = etlEntity.ETLConnectionString;
                }
                catch (Exception)
                { }
                //给隐藏域赋值
                string connStr = string.Format("权限中心:{0},该ETL实体的数据库地址：{1}",
                    DbConnectionManager.GetConnectionString("PermissionsCenter"), etlConStr);

                this.connStrHidd.Value = connStr;
            }
        }

        //初始化实体
        private ETLEntity InitETLEntity(string entityId, string categoryId)
        {
            ETLEntity result;

            if (entityId.IsNotEmpty())
            {
                result = (ETLEntity)DESchemaObjectAdapter.Instance.Load(entityId);
                OperationMode = SCObjectOperationMode.Update;
            }
            else
            {
                categoryId.CheckStringIsNullOrEmpty("[类别编码]");
                result = (ETLEntity)SchemaExtensions.CreateObject(DEStandardObjectSchemaType.ETLEntity.ToString());
                result.CategoryID = categoryId;
                OperationMode = SCObjectOperationMode.Add;
                result.Fields = new DynamicEntityFieldCollection();
            }
            return result;
        }

        //保存
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //取值
            this.bindingControl.CollectData();
            var entity = (ETLEntity)this.bindingControl.Data;
            try
            {
                var error = new StringBuilder();

                //是否历史时间
                if (!Util.CheckOperationSafe())
                    return;

                PropertyValueCollection pvc = JSONSerializerExecute.Deserialize<PropertyValueCollection>(Request.Form.GetValue("properties", string.Empty));
                //取出PropertyForm值
                entity.Properties.FromPropertyValues(pvc);

                string parentCodeName = entity.ParentCodeName;
                //DESchemaObjectAdapter.Instance.Exist(entity.ID, DateTime.Now.SimulateTime());
                try
                {
                    if (!string.IsNullOrEmpty(parentCodeName.Trim()))
                    {
                        ETLEntity parent = ETLEntityAdapter.Instance.LoadWithCache(parentCodeName, DateTime.Now);
                    }
                }
                catch (Exception ee)
                {
                    error.Append(ee.Message);
                }

                //检查重复项
                entity.Fields.GroupBy(p => p.Name).ForEach(p => { if (p.Count() > 1)error.Append(p.Key + "有重复项!\r\n"); });

                bool needCheckExist = false;
                #region
                if (DESchemaObjectAdapter.Instance.Exist(entity.ID, DateTime.Now.SimulateTime()))
                {
                    //更新实体
                    ETLEntity old = DESchemaObjectAdapter.Instance.Load(entity.ID, DateTime.Now.SimulateTime()) as ETLEntity;
                    if (!old.Name.Equals(entity.Name))
                        needCheckExist = true;
                }
                else
                    needCheckExist = true;

                if (needCheckExist)
                {
                    //生成CodeName
                    entity.BuidCodeName();

                    if (DEDynamicEntityAdapter.Instance.ExistByCodeName(entity.CodeName, DateTime.Now.SimulateTime()))
                        error.AppendFormat("已存在名称为[{0}]的实体!\r\n", entity.Name);
                }
                var sapInstanceParams = this.gridUepUsers.InitialData[0] as SAPInstanceParams;
                if (sapInstanceParams.SAPInstanceId.IsNullOrEmpty())
                {
                    error.AppendFormat("ETL抽数账号不能为空!\r\n", entity.Name);
                }
                (error.Length > 0).TrueThrow(error.ToString());

                // 数据库信息的赋值
                if (this.gridDBInfo.InitialData.Count > 0)
                {
                    DBInfo dbBaseInfo = this.gridDBInfo.InitialData[0] as DBInfo;

                    entity.TargetConnCode = dbBaseInfo.DBCode;

                    //if (entity.TargetConnCode.IsNullOrEmpty())
                    //{
                    //    //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "reloadPage", "alert('请选择数据库登录信息！'); ", true);

                    //    string errorMsg = "请选择数据库登录信息！";

                    //    WebUtility.ShowClientError(errorMsg, errorMsg, "错误");

                    //    return;
                    //}
                }

                entity.SAPInstanceId = sapInstanceParams.SAPInstanceId;
                var sapList = entity.sapInstanceParams;
                #endregion

                //校验数据库连接字符串是否正确
                string msg;
                if (!entity.CheckDBConnString(out msg))
                {
                    entity.TargetConnCode = HF_TargetConnCode.Value;
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "reloadPage", "alert('" + msg.Replace("\r\n", "").Replace("'", "") + "'); ", true);
                    InitPropertyForm(entity, false);
                    this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)), "Name", "Description");
                    this.okButton.Visible = true;
                }
                else if (error.Length > 0)
                {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "reloadPage", "alert('" + error.ToString().Replace("\r\n", "").Replace("'", "") + "'); ", true);
                    //WebUtility.RegisterClientErrorMessage(error.ToString(), error.ToString(), "失败");
                    InitPropertyForm(entity, false);
                    this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)), "Name", "Description");
                    this.okButton.Visible = true;
                }
                else
                {
                    //入库
                    ETLDEObjectOperations.Instance.DoOperation(this.OperationMode, entity, null);
                    entity.ClearCacheData();

                    DateTime last = lastUpdateTime.Value == null ? DateTime.MinValue : lastUpdateTime.Value;
                    ETLPropertiesMappingAdapter.Instance.Update(new ETLEntityPropertiesMapping() { ETLEntityID = entity.ID, LastUpdateTime = last });

                    HttpContext.Current.Response.Write("<script>window.returnValue=true;window.close()</script>");
                }
            }
            catch (Exception ee)
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "reloadPage", "alert('" + ee.Message.ToString().Replace("\r\n", "").Replace("'", "") + "'); ", true);
                InitPropertyForm(entity, false);
                this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)), "Name", "Description");
                this.okButton.Visible = true;
            }
        }

        //初始化PropertyForm
        protected override void OnPreRender(EventArgs e)
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (this.IsPostBack == false && this.IsCallback == false)
            {
                this.InitPropertyForm(this.Data, this.EditEnabled == false);
            }
            base.OnPreRender(e);
        }

        #region
        public void AfterNormalSceneApplied()
        {
            //此处注释掉了   验证不成功时  页面还可以继续更改
            //this.okButton.Visible = this.Data != null && this.Data.Status == SchemaObjectStatus.Normal;
        }

        public string NormalSceneName
        {
            get { return this.EditEnabled ? "Normal" : "ReadOnly"; }
        }

        public string ReadOnlySceneName
        {
            get { return "ReadOnly"; }
        }

        private void PropertyEditorRegister()
        {
            PropertyEditorHelper.RegisterEditor(new StandardPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new BooleanPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new EnumPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new ObjectPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new DatePropertyEditor());
            PropertyEditorHelper.RegisterEditor(new DateTimePropertyEditor());
            PropertyEditorHelper.RegisterEditor(new CodeNameUniqueEditor());
            PropertyEditorHelper.RegisterEditor(new GetPinYinEditor());
            PropertyEditorHelper.RegisterEditor(new ImageUploaderPropertyEditor());
            PropertyEditorHelper.RegisterEditor(new PObjectNameEditor());

            //PropertyEditorHelper.RegisterEditor(new EntityFieldPropertyEditorSceneAdapter());

        }
        [ControllerMethod(true)]
        protected void CreateNewObject()
        {
            this.CreateNewObject("ETLEntity");
        }

        [ControllerMethod]
        protected void CreateNewObject(string schemaType)
        {
            if (this.sceneAdapter == null)
            {
                this.sceneAdapter = PropertyEditorSceneAdapter.Create(schemaType);
                this.sceneAdapter.Mode = SCObjectOperationMode.Add;
            }

            this.CreateNewObjectBySchemaType(schemaType);

        }
        private void CreateNewObjectBySchemaType(string schemaType)
        {
            this.Data = SchemaExtensions.CreateObject(schemaType);
            this.currentSchemaType.Value = schemaType;
            this.Data.ID = UuidHelper.NewUuidString();
        }

        private void InitPropertyForm(DESchemaObjectBase data, bool readOnly)
        {
            this.propertyForm.ReadOnly = readOnly;

            data.Properties.ForEach(p =>
            {
                if (Request.QueryString[p.Definition.Name].IsNotEmpty())
                {
                    p.StringValue = Request.QueryString[p.Definition.Name].Trim();
                }
            });

            this.propertyForm.Properties.CopyFrom(data.Properties.ToPropertyValues());


            var layouts = new PropertyLayoutSectionCollection();
            layouts.LoadLayoutSectionFromConfiguration("ETLLayout");

            this.propertyForm.Layouts.InitFromLayoutSectionCollection(layouts);

            this.propertyForm.Style["width"] = "100%";

        }

        [ControllerMethod]
        protected void LoadObject(string id)
        {
            ETLEntity result;

            result = DESchemaObjectAdapter.Instance.Load(id) as ETLEntity;
            this.Data = result;

            this.sceneAdapter = PropertyEditorSceneAdapter.Create(this.Data.SchemaType);
            this.sceneAdapter.ObjectID = this.Data.ID;
            this.sceneAdapter.Mode = SCObjectOperationMode.Update;
            this.currentSchemaType.Value = this.Data.SchemaType;
        }

        [ControllerMethod]
        protected void LoadObject(string id, long reserved, string time)
        {
            TimePointContext.Current.UseCurrentTime = false;
            TimePointContext.Current.SimulatedTime = DateTime.Parse(time).ToLocalTime();
            this.LoadObject(id);
        }
        #endregion
    }
}