using MCS.Library.Core;
using MCS.Library.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MCS.Library.SOA.DataObjects
{
    internal class WriteToLoggerUserTaskOperationImpl : IUserTaskOperation
    {
        public void Init(UserTaskOpEventContainer eventContainer)
        {
            eventContainer.BeforeSendUserTasks += eventContainer_BeforeSendUserTasks;
            eventContainer.SendUserTasks += eventContainer_SendUserTasks;
            eventContainer.SetUserTasksAccomplished += eventContainer_SetUserTasksAccomplished;
            eventContainer.UpdateUserTask += eventContainer_UpdateUserTask;
            eventContainer.DeleteUserAccomplishedTasks += eventContainer_DeleteUserAccomplishedTasks;
            eventContainer.DeleteUserTasks += eventContainer_DeleteUserTasks;
        }

        private void eventContainer_BeforeSendUserTasks(UserTaskCollection tasks, Dictionary<object, object> context)
        {
            Debug.WriteLine("BeforeSendUserTasks");
        }

        private void eventContainer_DeleteUserTasks(UserTaskCollection tasks, Dictionary<object, object> context)
        {
            Write(GetMessageContent(tasks), "DeleteUserTasks");
        }

        private void eventContainer_DeleteUserAccomplishedTasks(UserTaskCollection tasks, Dictionary<object, object> context)
        {
            Write(GetMessageContent(tasks), "DeleteUserAccomplishedTasks");
        }

        private int eventContainer_UpdateUserTask(UserTask task, UserTaskIDType idType, UserTaskFieldDefine fields, Dictionary<object, object> context)
        {
            Write(GetMessageContent(task), "UpdateUserTask");

            return 0;
        }

        private void eventContainer_SetUserTasksAccomplished(UserTaskCollection tasks, Dictionary<object, object> context)
        {
            Write(GetMessageContent(tasks), "SetUserTasksAccomplished");
        }

        private void eventContainer_SendUserTasks(UserTaskCollection tasks, Dictionary<object, object> context)
        {
            Write(GetMessageContent(tasks), "SendUserTasks");
        }

        private static string GetMessageContent(UserTask task)
        {
            return XmlHelper.SerializeObjectToXml(task).OuterXml;
        }

        private static string GetMessageContent(UserTaskCollection tasks)
        {
            StringBuilder strB = new StringBuilder();

            if (tasks.Count > 0)
            {
                strB.AppendLine("UserTasks");

                foreach (UserTask task in tasks)
                    strB.AppendLine(XmlHelper.SerializeObjectToXml(task).OuterXml);
            }

            return strB.ToString();
        }

        public void Write(string message, string title)
        {
            Logger logger = LoggerFactory.Create("WfRuntime");

            if (logger != null && message.IsNotEmpty())
                logger.Write(message, LogPriority.Lowest, 51000, TraceEventType.Information, title);
        }
    }
}
