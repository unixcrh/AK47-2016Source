using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MCS.PrototypeWeb.Pages.Server
{
    public partial class GridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ControlDataBind();
            }
        }

        private void ControlDataBind()
        {
            List<Task> taskList = new List<Task>();
            taskList.Add(new Task("关于柳州店变更印章保管员的申请 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("信息中心一9年9月广告投放计划表  ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("信息中心分部二级市场违反合同付款申请表一  ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("信息中心分部二级市场违反合同付款申请表一  ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("信息中心分部二级市场违反合同付款申请表二测试[办结]  ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("待办测试信息中心上线评审 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("帐号申请单 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("SAP试运行上线评审 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("测试表单表格申请 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("变更系统管理员申请 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("AD帐号申请 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));
            taskList.Add(new Task("系统测试上向评审 ", "信息中心 系统实施部", "袁旭", "送签", "2009-10-27 13:27"));

            this.gridTasks.DataSource = taskList;
            this.gridTasks.DataBind();
        }

        protected void gridTasks_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }

        protected void gridTasks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridTasks.PageIndex = e.NewPageIndex;
            this.ControlDataBind();
        }
    }


    public class Task
    {
        public Task(string title, string department , string creator , string status, string createTime)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Title = title;
            this.Department = department;
            this.Creator = creator;
            this.Status = status;
            this.CreateTime = createTime;
        }

        public string ID { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        public string Creator { get; set; }

        public string Status { get; set; }

        public string CreateTime { get; set; }
    }
}