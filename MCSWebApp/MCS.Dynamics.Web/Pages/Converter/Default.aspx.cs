using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Web.Library.Script;

namespace MCS.Dynamics.Web.Pages.Converter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Convert_Click(object sender, EventArgs e)
        {
            string json = string.Empty;
            json = txt_json.Text.Trim();

            try
            {
                //动态实体实例JSON反序列化
                var instance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(json);
                if (instance != null && instance.Fields.Any())
                {
                    lbl_msg.Text = "转换成功！";
                }
            }
            catch (Exception ex)
            {
                lbl_msg.Text = "转换失败！原因：" + ex.Message;
            }
        }
    }
}