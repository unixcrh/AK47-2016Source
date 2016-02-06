using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using System.Reflection;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Facade;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;

namespace MCS.Dynamics.Web.Pages.Demo
{
    public partial class ConvertFiledValueTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// bool值转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            IList<Test> testlist = new List<Test>();

            AddList(txtInt.Text.Trim(), "Int", ref testlist);
            AddList(txtBool.Text.Trim(), "Bool", ref testlist);
            AddList(txtDatetime.Text.Trim().ToString(), "DateTime", ref testlist);
            AddList(txtDecimal.Text.Trim(), "Decimal", ref testlist);

            foreach (Test t in testlist)
            {
                Label1.Text += ConvertUepFiledValueMapping.ConvertUEPFiledValue(t.strType, t.strFieldValue) + "</br>";
            }
        }


        private void AddList(string value, string type, ref IList<Test> testlist)
        {
            Test test1 = new Test();
            test1.strFieldValue = value;
            test1.strType = type;
            testlist.Add(test1);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ETLEntity etlEntity = (ETLEntity)DESchemaObjectAdapter.Instance.Load("624da4a3-ffc2-4379-bddd-3cc4543da1d6");
            var listSap = etlEntity.GetSAPLoginParamByUepIdAndClient();
            string resultStr = " Sap用户名：{0}，SapApplicationServer：{1}，共取出了{2}个用户";
            if (listSap.Any())
            {
                resultStr = string.Format(resultStr, listSap[0].User, listSap[0].ApplicationServer,listSap.Count);
            }
            else
            {
                resultStr = "未取出任何数据  可能有错误";
            }

            Response.Write(resultStr);
        }
    }



    public class Test
    {
        public string strType
        {
            get;
            set;
        }
        public string strFieldValue
        {
            get;
            set;
        }
    }
}