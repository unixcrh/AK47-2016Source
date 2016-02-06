using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Library.SOA.DataObjects.Tenant.Test.Jobs
{
    [TestClass]
    public class JobAdapterTest
    {
        [TestMethod]
        [TestCategory("Job")]
        public void JobBaseAdapterTest()
        {
            JobBase job = new StartWorkflowJob();

            job.JobID = Guid.NewGuid().ToString();
            job.Name = "NameTest" + DateTime.Now.ToString();
            job.Description = "DescTest" + DateTime.Now.ToString();
            job.Creator = new OguUser("6872ac4c-48a2-47fc-a12f-05415dc50042"); //张媛媛

            var schedule = CreateMonthlySchedule();
            JobScheduleAdapter.Instance.Update(schedule);

            job.Schedules.Add(schedule);
            JobBaseAdapter.Instance.Update(job);
            job.LastExecuteTime = DateTime.Now;
            JobBaseAdapter.Instance.Update(job);

            JobCollection coll = JobBaseAdapter.Instance.Load(p => p.AppendItem("JOB_ID", job.JobID));
            Assert.IsTrue(coll.Count == 1);

            Assert.AreEqual(job.Description, coll[0].Description);
            Assert.AreEqual(job.Schedules[0].Description, coll[0].Schedules[0].Description);

            JobBaseAdapter.Instance.Delete(job);
            coll = JobBaseAdapter.Instance.Load(p => p.AppendItem("JOB_ID", job.JobID));
            Assert.IsTrue(coll.Count == 0);
        }

        private static JobSchedule CreateMonthlySchedule()
        {
            string schID = Guid.NewGuid().ToString();
            string schName = "TestName" + DateTime.Now.ToString();
            DateTime startTime = new DateTime(2011, 4, 1);
            DateTime endTime = new DateTime(2012, 4, 1);
            TimeFrequencyBase timeFrequency = new FixedTimeFrequency(new TimeSpan(9, 0, 0));

            JobScheduleFrequencyBase schFrequency = new MonthlyJobScheduleFrequency(10, 1, timeFrequency);
            schFrequency.ID = Guid.NewGuid().ToString();

            JobSchedule schedule = new JobSchedule(schID, schName, startTime, endTime, schFrequency);
            return schedule;
        }
    }
}
