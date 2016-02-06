using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Dynamics.Web.Dialogs;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Web.Library;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Web.WebControls;
using MCS.Web.Library.MVC;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Job;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;
using System.Text;
using MCS.Library.SOA.DataObjects;
using MCS.Web.Library.Script;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Operations;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using System.Runtime.Serialization.Json;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Workflow;
using System.Transactions;
using MCS.Library.Data;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    [SceneUsage("~/App_Data/PropertyEditScene.xml", "PropertyEdit")]
    public partial class ETLEntityJobInfo : System.Web.UI.Page, ITimeSceneDescriptor, INormalSceneDescriptor
    {
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

        public string ErrorMsg
        { get; set; }

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

        private JobBase Data
        {
            get;
            set;
        }

        protected override void OnPreInit(EventArgs e)
        {
            //注册序列化器
            //JSONSerializerExecute.RegisterConverter(typeof(DynamicEntityFieldConverter));
            // JSONSerializerExecute.RegisterConverter(typeof(SchemaObjectSimpleConverter));
            base.OnPreInit(e);
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

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.IsPostBack == false && this.IsCallback == false)
                ControllerHelper.ExecuteMethodByRequest(this);

            this.PropertyEditorRegister();

            WebUtility.RequiredScript(typeof(ClientGrid));

            if (!IsPostBack)
            {
                //绑定字段类型
                this.ddl_JobType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(JobType)), "Name", "Description");
                if (this.Data != null)
                {
                    ddl_JobType.Enabled = false;
                    for (int i = 0; i < ddl_JobType.Items.Count; i++)
                    {
                        if (ddl_JobType.Items[i].Value == this.Data.JobType.ToString())
                        {
                            ddl_JobType.Items[i].Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ddl_JobType.Items.Count; i++)
                    {
                        if (ddl_JobType.Items[i].Value == JobType.ETLService.ToString())
                        {
                            ddl_JobType.Items[i].Selected = true;
                            break;
                        }
                    }
                    ch_IsAuto.Checked = true;
                }
            }


        }

        //初始化任务
        private ETLJob InitEntity(string entityId)
        {
            ETLJob result;

            if (entityId.IsNotEmpty())
            {
                result = (ETLJob)ETLJobAdapter.Instance.Load(entityId);

                OperationMode = SCObjectOperationMode.Update;
            }
            else
            {
                result = new ETLJob();
                OperationMode = SCObjectOperationMode.Add;
            }

            return result;
        }

        //保存
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //错误信息
            StringBuilder error = new StringBuilder();

            if (this.Data == null && Request["id"] != null)
            {
                //根据ID获取任务对象
                //this.Data = (ETLJob)ETLJobAdapter.Instance.Load(Convert.ToString(Request["id"]));
                this.Data = JobBaseAdapter.Instance.Load(c => c.AppendItem("job_id", Convert.ToString(Request["id"]))).FirstOrDefault();
            }

            if (!Util.CheckOperationSafe())
                return;
            //ETL任务
            if (ddl_JobType.SelectedValue == "ETLService")
            {
                #region

                JobScheduleCollection pvc = new JobScheduleCollection();
                //计划列表
                if (ch_IsAuto.Checked)
                {
                    //pvc = JSONSerializerExecute.Deserialize<JobScheduleCollection>(schedules.Value);
                    pvc = GetSchedules(schedules.Value);
                }

                ETLEntityCollection etls = GetETLEntities(etlEntities.Value);

                ETLWhereConditionCollection wheres = JSONSerializerExecute.Deserialize<ETLWhereConditionCollection>(conditions.Value);
                if (this.Data != null)
                {

                    ETLJob job = ETLJobAdapter.Instance.Load(this.Data.JobID) as ETLJob;
                    this.Data = job;
                    job.Category = txt_jobCategory.Text;
                    job.Enabled = ddl_Enabled.SelectedValue == "1" ? true : false;
                    job.JobType = JobType.ETLService;
                    job.Description = txt_JobDescription.Text;
                    job.Name = txt_JobName.Text;
                    job.Schedules = pvc;
                    job.ETLEntities = etls;
                    job.IsAuto = ch_IsAuto.Checked;
                    job.IsIncrement = ch_IsIncrement.Checked;
                    wheres.ForEach(w => { w.JOB_ID = this.Data.JobID; w.ID = Guid.NewGuid().ToString(); });

                    job.ETLWhereConditions = wheres;

                    if (CheckEtlEntities())
                    {
                        ETLJobOperations.Instance.DoOperation(EntityJobOperationMode.Update, job);
                    }
                }
                else
                {
                    ETLJob job = new ETLJob()
                    {
                        JobID = Guid.NewGuid().ToString(),
                        Category = txt_jobCategory.Text,
                        Enabled = ddl_Enabled.SelectedValue == "1" ? true : false,
                        JobType = JobType.ETLService,
                        Description = txt_JobDescription.Text,
                        Name = txt_JobName.Text,
                        Schedules = pvc,
                        ETLEntities = etls,
                        IsAuto = ch_IsAuto.Checked,
                        IsIncrement = ch_IsIncrement.Checked
                    };
                    wheres.ForEach(w => { w.JOB_ID = job.JobID; w.ID = Guid.NewGuid().ToString(); });

                    job.ETLWhereConditions = wheres;

                    this.Data = job;
                    if (CheckEtlEntities())
                    {
                        ETLJobOperations.Instance.DoOperation(EntityJobOperationMode.Add, job);
                    }

                }
                if (string.IsNullOrEmpty(ErrorMsg))
                {
                    HttpContext.Current.Response.Write("<script>window.returnValue=true;window.close();</script>");
                    //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "closeWindow", "window.returnValue=true;window.close()", true);
                }
                else
                {
                    //ViewState["conditions"] = conditions.Value;
                    ViewState["schedules"] = schedules.Value;
                    conditions.Value = JSONSerializerExecute.Serialize((this.Data as ETLJob).ETLWhereConditions);

                    ViewState["etlEntities"] = etlEntities.Value;


                    //计划列表
                    List<ScheduleGridObj> scheduleList = new List<ScheduleGridObj>();
                    foreach (JobSchedule item in this.Data.Schedules)
                    {
                        scheduleList.Add(new ScheduleGridObj()
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Description = item.Description
                        });
                    }
                    ViewState["schedulesGrid"] = JSONSerializerExecute.Serialize(scheduleList); ;
                    grid.InitialData = scheduleList;

                    //etl实体列表
                    List<EtlGridObj> etlJobs = new List<EtlGridObj>();
                    foreach (ETLEntity item in (this.Data as ETLJob).ETLEntities)
                    {
                        etlJobs.Add(new EtlGridObj()
                        {
                            ID = item.ID,
                            CodeName = item.Name,
                            Description = item.Description
                        });
                    }

                    ViewState["etlsGrid"] = JSONSerializerExecute.Serialize(etlJobs);
                    gridEtl.InitialData = etlJobs;

                    string msg = ErrorMsg.Replace("\r\n", string.Empty);
                    string scriptStr = string.Format("alert('{0}');", msg);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "closeWindow", scriptStr, true);
                    //HttpContext.Current.Response.Write(scriptStr);

                }
                #endregion
            }
            //WebService任务
            else
            {
                #region

                JobScheduleCollection pvc = new JobScheduleCollection();
                //计划列表
                if (ch_IsAuto.Checked)
                {
                    //pvc = JSONSerializerExecute.Deserialize<JobScheduleCollection>(schedules.Value);
                    pvc = GetSchedules(schedules.Value);
                }

                if (this.Data != null)
                {
                    InvokeWebServiceJob job = InvokeWebServiceJobAdapter.Instance.Load(w => w.AppendItem("Job_id", this.Data.JobID)).FirstOrDefault();// this.Data as InvokeWebServiceJob;
                    job.Category = txt_jobCategory.Text;
                    job.Enabled = ddl_Enabled.SelectedValue == "1" ? true : false;
                    job.JobType = JobType.InvokeService;
                    job.Description = txt_JobDescription.Text;
                    job.Name = txt_JobName.Text;
                    job.Schedules = pvc;
                    job.ISManual = !ch_IsAuto.Checked;
                    job.SvcOperationDefs = JSONSerializerExecute.Deserialize<WfServiceOperationDefinitionCollection>(this.services.Value);
                    //入库
                    DoUpdate(job);
                }
                else
                {
                    InvokeWebServiceJob job = new InvokeWebServiceJob()
                    {
                        JobID = Guid.NewGuid().ToString(),
                        Category = txt_jobCategory.Text,
                        Enabled = ddl_Enabled.SelectedValue == "1" ? true : false,
                        JobType = JobType.InvokeService,
                        Description = txt_JobDescription.Text,
                        Name = txt_JobName.Text,
                        Schedules = pvc,
                        ISManual = !ch_IsAuto.Checked,
                        SvcOperationDefs = JSONSerializerExecute.Deserialize<WfServiceOperationDefinitionCollection>(this.services.Value)
                    };

                    this.Data = job;

                    //入库
                    DoUpdate(job);
                }
                if (string.IsNullOrEmpty(ErrorMsg))
                {
                    HttpContext.Current.Response.Write("<script>window.returnValue=true;window.close();</script>");
                    //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "closeWindow", "window.returnValue=true;window.close()", true);
                }
                else
                {
                    //ViewState["conditions"] = conditions.Value;
                    ViewState["schedules"] = schedules.Value;
                    conditions.Value = JSONSerializerExecute.Serialize((this.Data as ETLJob).ETLWhereConditions);

                    ViewState["etlEntities"] = etlEntities.Value;


                    //计划列表
                    List<ScheduleGridObj> scheduleList = new List<ScheduleGridObj>();
                    foreach (JobSchedule item in this.Data.Schedules)
                    {
                        scheduleList.Add(new ScheduleGridObj()
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Description = item.Description
                        });
                    }
                    ViewState["schedulesGrid"] = JSONSerializerExecute.Serialize(scheduleList); ;
                    grid.InitialData = scheduleList;

                    //etl实体列表
                    List<EtlGridObj> etlJobs = new List<EtlGridObj>();
                    foreach (ETLEntity item in (this.Data as ETLJob).ETLEntities)
                    {
                        etlJobs.Add(new EtlGridObj()
                        {
                            ID = item.ID,
                            CodeName = item.Name,
                            Description = item.Description
                        });
                    }

                    ViewState["etlsGrid"] = JSONSerializerExecute.Serialize(etlJobs);
                    gridEtl.InitialData = etlJobs;

                    string msg = ErrorMsg.Replace("\r\n", string.Empty);
                    string scriptStr = string.Format("alert('{0}');", msg);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "closeWindow", scriptStr, true);

                }
                #endregion
            }

        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="newJob"></param>
        private void DoUpdate(JobBase newJob)
        {
            using (TransactionScope ts = TransactionScopeFactory.Create())
            {
                if (this.Page.Request["id"] != null)
                {
                    StartWorkflowJobAdapter.Instance.Delete(new string[] { newJob.JobID });
                    InvokeWebServiceJobAdapter.Instance.Delete(p => p.AppendItem("JOB_ID", newJob.JobID));
                }
                if (newJob is StartWorkflowJob)
                {
                    StartWorkflowJobAdapter.Instance.Update((StartWorkflowJob)newJob);
                }
                else if (newJob is InvokeWebServiceJob)
                {
                    InvokeWebServiceJobAdapter.Instance.Update((InvokeWebServiceJob)newJob);
                }
                else
                {
                    JobBaseAdapter.Instance.Update(newJob);
                }

                ts.Complete();
            }
        }
        /// <summary>
        /// 验证ETL实体是否完整
        /// </summary>
        /// <returns></returns>
        protected bool CheckEtlEntities()
        {
            ErrorMsg = "";
            bool result = false;
            foreach (ETLEntity entity in (this.Data as ETLJob).ETLEntities)
            {
                string errorMsg = "";
                if (!entity.CheckDBConnString(out errorMsg))
                {
                    //向隐藏域提供错误信息
                    ErrorMsg += errorMsg + "\r\n";
                }
            }

            if (string.IsNullOrEmpty(ErrorMsg))
            {
                result = true;
            }
            return result;
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (this.IsPostBack == false && this.IsCallback == false)
            {

            }

            base.OnPreRender(e);

            //if (this.Data.Status != SchemaObjectStatus.Normal)
            //    this.okButton.Visible = false;
        }

        public void AfterNormalSceneApplied()
        {
            //this.okButton.Visible = this.Data != null;
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
            this.CreateNewObject("DynamicEntity");
        }

        [ControllerMethod]
        protected void CreateNewObject(string schemaType)
        {
            if (this.sceneAdapter == null)
            {
                this.sceneAdapter = PropertyEditorSceneAdapter.Create(schemaType);
                this.sceneAdapter.Mode = SCObjectOperationMode.Add;
            }

        }

        [ControllerMethod]
        protected void LoadObject(string id)
        {
            JobBase result;

            //根据ID获取任务对象
            result = JobBaseAdapter.Instance.Load(w => w.AppendItem("job_id", id)).FirstOrDefault();// ETLJobAdapter.Instance.Load(id);
            this.Data = result;
            //给控件赋值
            if (this.Data != null)
            {
                txt_JobName.Text = this.Data.Name;
                txt_jobCategory.Text = this.Data.Category.ToString();
                txt_JobDescription.Text = this.Data.Description.ToString();
                ch_IsAuto.Checked = !this.Data.ISManual;
                
                //etl任务
                if (this.Data.JobType == JobType.ETLService)
                {
                    //etl实体列表
                    List<EtlGridObj> etls = new List<EtlGridObj>();
                    var etlJob = ETLJobAdapter.Instance.Load(this.Data.JobID);
                    foreach (ETLEntity item in etlJob.ETLEntities)
                    {
                        etls.Add(new EtlGridObj()
                        {
                            ID = item.ID,
                            CodeName = item.Name,
                            Description = item.Description
                        });
                    }
                    gridEtl.InitialData = etls;
                    ch_IsIncrement.Checked = etlJob.IsIncrement;
                    //Where条件
                    ETLJob loadEtl = ETLJobAdapter.Instance.Load(this.Data.JobID);
                    conditions.Value = JSONSerializerExecute.Serialize(loadEtl.ETLWhereConditions);
                }
                //WebService任务
                else
                {
                    this.Data = InvokeWebServiceJobAdapter.Instance.Load(w => w.AppendItem("JOB_ID", this.Data.JobID)).FirstOrDefault();
                    invokingServiceGrid.InitialData = (this.Data as InvokeWebServiceJob).SvcOperationDefs;
                }
                //是否手动任务
                if (!this.Data.ISManual)
                {
                    //计划列表
                    List<ScheduleGridObj> schedules = new List<ScheduleGridObj>();
                    foreach (JobSchedule item in this.Data.Schedules)
                    {
                        schedules.Add(new ScheduleGridObj()
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Description = item.Description
                        });
                    }
                    grid.InitialData = schedules;
                }
            }
        }

        [ControllerMethod]
        protected void LoadObject(string id, long reserved, string time)
        {
            TimePointContext.Current.UseCurrentTime = false;
            TimePointContext.Current.SimulatedTime = DateTime.Parse(time).ToLocalTime();
            this.LoadObject(id);
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected ETLEntityCollection GetETLEntities(string ids)
        {
            ETLEntityCollection etls = new ETLEntityCollection();
            if (!string.IsNullOrEmpty(ids))
            {
                string[] propertities = ids.TrimEnd(',').Split(',');
                for (int i = 0; i < propertities.Length; i++)
                {
                    string id = propertities[i];
                    if (!string.IsNullOrEmpty(id))
                    {
                        //根据ID获取ETL实体
                        ETLEntity entity = DESchemaObjectAdapter.Instance.Load(id) as ETLEntity;
                        if (entity != null)
                        {
                            etls.Add(entity);
                        }
                    }
                }
            }
            return etls;
        }

        /// <summary>
        /// 根据ID获取计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected JobScheduleCollection GetSchedules(string ids)
        {
            JobScheduleCollection scheduleList = new JobScheduleCollection();
            if (!string.IsNullOrEmpty(ids))
            {
                string[] propertities = ids.TrimEnd(',').Split(',');
                for (int i = 0; i < propertities.Length; i++)
                {
                    string id = propertities[i];
                    if (!string.IsNullOrEmpty(id))
                    {
                        //根据ID获取ETL实体
                        JobSchedule entity = JobScheduleAdapter.Instance.Load(c => c.AppendItem("SCHEDULE_ID", id)).FirstOrDefault() as JobSchedule;
                        if (entity != null)
                        {
                            scheduleList.Add(entity);
                        }
                    }
                }
            }
            return scheduleList;
        }

        public class EtlGridObj
        {
            public string ID { get; set; }
            public string CodeName { get; set; }
            public string Description { get; set; }
        }

        public class ScheduleGridObj
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

    }

}