using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Net.SNTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects
{
    [Serializable]
    [XElementSerializable]
    public abstract class JobScheduleFrequencyBase
    {
        public string ID
        {
            get;
            set;
        }

        public abstract string Description
        {
            get;
        }

        public TimeFrequencyBase FrequencyTime
        {
            get;
            protected set;
        }

        /// <summary>
        /// 预估后面多少次的执行时间
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="timeOffset"></param>
        /// <param name="count"></param>
        /// <param name="timeout">计算多久后会超时</param>
        /// <returns></returns>
        public List<DateTime> EstimateExecuteTime(DateTime startTime, TimeSpan timeOffset, int maxCount, TimeSpan timeout)
        {
            Stopwatch sw = new Stopwatch();

            List<DateTime> result = new List<DateTime>();

            DateTime lastExeTime = SNTPClient.AdjustedTime;
            DateTime checkPoint = lastExeTime;

            int count = 0;

            sw.Start();

            while (count < maxCount && sw.Elapsed < timeout)
            {
                Debug.Write(checkPoint);
                if (this.IsNextExecuteTime(startTime, lastExeTime, checkPoint, timeOffset))
                {
                    Debug.WriteLine("→OK");
                    result.Add(checkPoint);
                    lastExeTime = checkPoint;
                    count++;
                }
                else
                {
                    Debug.WriteLine("→Fail");
                }

                checkPoint = checkPoint.Add(GetEstimateSampleTime());
            }

            return result;
        }

        /*
        public DateTime LastModifyTime
        {
            get;
            set;
        }
        */

        public TimeScope GetTimeScope(DateTime startTime, DateTime timePoint, TimeSpan timeOffset)
        {
            TimeScope result = null;

            if (DateIsMatched(startTime, timePoint))
            {
                if (FrequencyTime != null)
                    result = FrequencyTime.GetTimeScope(timePoint.TimeOfDay, timeOffset);
            }

            return result;
        }

        /// <summary>
        /// 根据上次执行时间，判断检查点是否符合执行时间
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="lastExeTime"></param>
        /// <param name="nextCheckPoint"></param>
        /// <returns></returns>
        public bool IsNextExecuteTime(DateTime startTime, DateTime lastExeTime, DateTime nextCheckPoint, TimeSpan timeOffset)
        {
            bool result = false;

            if (lastExeTime.Date != nextCheckPoint.Date)
            {
                //上次执行时间和检查点不是同一天
                TimeScope scope = GetTimeScope(startTime, nextCheckPoint, timeOffset);

                result = scope != null;
            }
            else
            {
                //上次执行时间和检查点是同一天
                if (DateIsMatched(startTime, nextCheckPoint) && FrequencyTime != null)
                    result = FrequencyTime.IsNextExecuteTime(lastExeTime.TimeOfDay, nextCheckPoint.TimeOfDay, timeOffset);
            }

            return result;
        }

        /// <summary>
        /// 日期是否匹配
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        protected abstract bool DateIsMatched(DateTime startTime, DateTime timePoint);

        /// <summary>
        /// 得到预估时间时的采样周期
        /// </summary>
        /// <returns></returns>
        protected virtual TimeSpan GetEstimateSampleTime()
        {
            return TimeSpan.FromSeconds(20);
        }
    }
}
