using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Others;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Job;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Saplocalhost;
using MCS.Web.Library.Script;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Enums;

namespace MCS.Dynamics.Web.Pages.Demo
{
    public partial class ResultoTabTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable oTab1 = new DataTable();
            oTab1.Columns.Add("姓名");
            oTab1.Columns.Add("班级");
            DataRow row = oTab1.NewRow();
            row["姓名"] = "张三";
            row["班级"] = "1";
            oTab1.Rows.Add(row);
            DataRow row1 = oTab1.NewRow();
            row1["姓名"] = "李四";
            row1["班级"] = "1";
            oTab1.Rows.Add(row1);
            DataRow row2 = oTab1.NewRow();
            row2["姓名"] = "王五";
            row2["班级"] = "2";
            oTab1.Rows.Add(row2);
            ds.Tables.Add(oTab1);

            DataTable oTab2 = new DataTable();
            oTab2.Columns.Add("班级");
            oTab2.Columns.Add("班级名称");
            oTab2.Columns.Add("性别");
            DataRow row3 = oTab2.NewRow();
            row3["班级"] = "1";
            row3["班级名称"] = "一班";
            row3["性别"] = "3";
            oTab2.Rows.Add(row3);
            DataRow row4 = oTab2.NewRow();
            row4["班级名称"] = "二班";
            row4["班级"] = "2";
            row4["性别"] = "4";
            oTab2.Rows.Add(row4);
            ds.Tables.Add(oTab2);

            DataTable oTab3 = new DataTable();
            oTab3.Columns.Add("性别");
            oTab3.Columns.Add("性别名称");
            oTab3.Columns.Add("年龄");
            DataRow row5 = oTab3.NewRow();
            row5["性别"] = "3";
            row5["性别名称"] = "男";
            row5["年龄"] = "5";
            oTab3.Rows.Add(row5);
            DataRow row6 = oTab3.NewRow();
            row6["性别名称"] = "女";
            row6["性别"] = "4";
            row6["年龄"] = "6";
            oTab3.Rows.Add(row6);
            ds.Tables.Add(oTab3);

            DataTable oTab4 = new DataTable();
            oTab4.Columns.Add("年龄");
            oTab4.Columns.Add("年龄大小");
            DataRow row7 = oTab4.NewRow();
            row7["年龄"] = "5";
            row7["年龄大小"] = 32;
            oTab4.Rows.Add(row7);
            DataRow row8 = oTab4.NewRow();
            row8["年龄大小"] = 23;
            row8["年龄"] = "6";
            oTab4.Rows.Add(row8);

            ds.Tables.Add(oTab4);


            //DataTable dt = ETLTools.GetMappingData(ds);

            List<string> updates = new List<string>();
            List<string> inserts = new List<string>();

            EntityMappingStruct entity = new EntityMappingStruct();

            //inserts = ETLTools.GetInsertSql(dt, etlEntity);
            //f6483e01-ab7f-4293-8dea-225d4686b839   913f8372-bb2d-4063-b375-825a9375ec46

        }

        /// <summary>
        /// 创建表语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //d472d522-d99d-45b1-a5df-43271a800d3a   4cf34d72-1736-44dc-88cd-e9e9d901621f
            ETLEntity entity = new ETLEntity();
            entity = (ETLEntity)DESchemaObjectAdapter.Instance.Load(txt_JobID.Text.Trim());
            TextBox1.Text = ETLTools.ETLEntityConvertToSql(entity);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //inserts = ETLTools.GetInsertSql(dt, etlEntity);
            //f6483e01-ab7f-4293-8dea-225d4686b839   913f8372-bb2d-4063-b375-825a9375ec46

            string strCode = txt_JobID.Text.Trim();

            ETLJob job = ETLJobAdapter.Instance.Load(strCode);
            job.Start();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ETLEntity etlEntityResult = DESchemaObjectAdapter.Instance.Load("f505ba56-2a11-45b4-8db2-dc6881204058") as ETLEntity;
            if (etlEntityResult != null)
            {
                List<SAPPara> list = etlEntityResult.GetSAPLoginParamByUepIdAndClient();
                Response.Write(JSONSerializerExecute.Serialize(list));
            }
        }

        /// <summary>
        /// 测试sql特殊字符处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects.ErrorLog el = CreateErrorLog();
            el.ErrorMsg = "' '";

            ErrorLogAdapter.Instance.AddErrorLog(el);
        }

        /// <summary>
        /// 创建一个错误日志
        /// </summary>
        /// <returns></returns>
        private MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects.ErrorLog CreateErrorLog()
        {
            MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects.ErrorLog elErrorLog = new MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects.ErrorLog()
            {
                Code = Guid.NewGuid().ToString(),
                CreateUser = "me",
                ErrorMsg = "this is an error",
                ErrorLogType = ErrorType.Single,
                ETLEntityCode = "Test",
                ExecutionTime = DateTime.Now,
                SqlStr = "Insert to"

            };
            return elErrorLog;
        }
    }
}