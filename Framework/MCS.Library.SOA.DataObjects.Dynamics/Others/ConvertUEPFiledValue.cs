using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MCS.Library.SOA.DataObjects.Dynamics.Others
{
    public class ConvertUEPFiledValue
    {
        /// <summary>
        /// 字符串类型值转换
        /// </summary>
        /// <param name="filedValue"></param>
        /// <returns></returns>
        public string ConvertStringValue(string filedValue, string rule)
        {
            string newFiledValue = string.Empty;
            if (!string.IsNullOrEmpty(filedValue))
            {
                newFiledValue = filedValue;
            }
            return newFiledValue;
        }

        /// <summary>
        /// Bool类型值得转换
        /// </summary>
        /// <param name="filedValue"></param>
        /// <returns></returns>
        public string ConvertBoolValue(string filedValue, string rule)
        {
            string newFiledValue = string.Empty;
            if (!string.IsNullOrEmpty(filedValue) && !string.IsNullOrEmpty(rule))
            {
                string[] rules = rule.Split(',');
                if (rules.Length == 2)
                {
                    newFiledValue = filedValue.ToUpper() == "TRUE" ? rules[0] : rules[1];
                }
            }
            return newFiledValue;
        }

        /// <summary>
        /// Int类型值得转换
        /// </summary>
        /// <param name="filedValue"></param>
        /// <returns></returns>
        public string ConvertIntValue(string filedValue, string rule)
        {
            string newFiledValue = string.Empty;
            Regex rex = new Regex("^[0-9]*$");
            if (!string.IsNullOrEmpty(filedValue) && rex.IsMatch(filedValue))
            {
                newFiledValue = filedValue;
            }
            return newFiledValue;
        }

        /// <summary>
        /// 时间类型值转换
        /// </summary>
        /// <param name="filedValue"></param>
        /// <returns></returns>
        public string ConvertDateTimeValue(string filedValue, string rule)
        {
            string newFiledValue = string.Empty;
            if (!string.IsNullOrEmpty(filedValue) && !string.IsNullOrEmpty(rule))
            {
                //如果是时间类型则值可以转换成时间
                DateTime dateTime = DateTime.MinValue;

                if (DateTime.TryParse(filedValue, out dateTime))
                {
                    newFiledValue = dateTime.ToString("yyyyMMdd");
                }

                //string strDateTime = dateTime.ToString("yyyy-MM-dd");
                //if (strDateTime.Contains("-"))
                //{
                //    newFiledValue = filedValue.Replace("-", rule);//替换空
                //}
            }
            return newFiledValue;
        }

        /// <summary>
        /// decimel值类型转换
        /// </summary>
        /// <param name="filedValue"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public string ConvertDecimalValue(string filedValue, string rule)
        {
            string newFiledValue = string.Empty;
            Regex rex = new Regex("^[0-9]*$");
            if (!string.IsNullOrEmpty(filedValue) && !string.IsNullOrEmpty(rule) && rex.IsMatch(filedValue))
            {
                newFiledValue = decimal.Parse(filedValue).ToString(rule);
            }
            return newFiledValue;
        }
    }
}
