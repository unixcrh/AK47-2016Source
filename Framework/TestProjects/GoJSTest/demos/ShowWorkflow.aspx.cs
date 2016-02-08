using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Workflow.DTO;
using System.Collections;
using MCS.Web.Library.Script;
using System.Xml;
using MCS.Library.Core;

namespace GoJSTest.demos
{
    public partial class ShowWorkflow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WorkflowInfo workflowInfo = LoadWorkflowInfo();

            //XmlDocument graphDescription = XmlHelper.CreateDomDocument(workflowInfo.GraphDescription);

            ArrayList activities = new ArrayList();

            foreach (ActivityInfo actInfo in workflowInfo.Activities)
            {
                activities.Add(new
                {
                    id = actInfo.ID,
                    key = actInfo.Key,
                    name = actInfo.Name,
                    op = actInfo.Operator,
                    hasBranchProcess = actInfo.HasBranchProcess,
                    status = actInfo.Status.ToString(),
                    activityType = actInfo.ActivityType.ToString()
                });
            }

            ArrayList transitions = new ArrayList();

            foreach (TransitionInfo transInfo in workflowInfo.Transitions)
            {
                transitions.Add(new
                {
                    from = transInfo.FromActivityKey,
                    to = transInfo.ToActivityKey,
                    elapsed = transInfo.IsPassed,
                    isReturn = transInfo.WfReturnLine
                });
            }

            this.activitiesInfoJson.Value = JSONSerializerExecute.Serialize(activities);
            this.transitionsInfoJson.Value = JSONSerializerExecute.Serialize(transitions);
        }

        private static WorkflowInfo LoadWorkflowInfo()
        {
            string filePath = HttpContext.Current.Server.MapPath("workflowInfo.json");

            string json = System.IO.File.ReadAllText(filePath);

            return JSONSerializerExecute.Deserialize<WorkflowInfo>(json);
        }
    }
}