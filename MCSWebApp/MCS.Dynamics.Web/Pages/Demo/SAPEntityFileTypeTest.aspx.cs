using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCS.Dynamics.Web.Saplocalhost;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;

namespace MCS.Dynamics.Web.Pages.Demo
{
    public partial class SAPEntityFileTypeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Saplocalhost.WebServiceConnectSAP srv = new Saplocalhost.WebServiceConnectSAP();
                var srv = new WebServiceConnectSAPSoapClient();
                DataTable table = srv.GetEntityDefine("ZR521");

                GridView1.DataSource = table;

                foreach (DataRow item in table.Rows)
                {
                    string fileType = Convert.ToString(item["字段类型"]);
                    int fileLenth = Convert.ToInt32(item["字段长度"]);
                    SAPFileMapping.SAPFiledTypeToUEPFiledType(Convert.ToString(item["字段类型"]), ref fileLenth);
                    item["字段类型"] = fileType;
                    item["字段长度"] = fileLenth;
                }
                GridView1.DataBind();
            }
        }
    }
}