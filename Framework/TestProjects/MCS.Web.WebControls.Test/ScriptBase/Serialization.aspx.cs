using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using MCS.Web.Library.Script;

public partial class ScriptBase_Serialization : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //JavaScriptSerializer s = JSONSerializerFactory.GetJavaScriptSerializer(typeof(MCS.Web.WebControls.SampleObject));
        Label1.Style.Add("Width", "200px");
        Label1.Style.Add("Height", "100px");
       
        //s.Serialize(Label1.Style);
        TextBox1.Text = "{\"DT\":\"\\/Date(1181611809135)\\/\",\"Name\":\"Hujintao\",\"Height\":180,\"__type\":\"MCS.Web.WebControls.SampleObject\"}";//"{\"name\":\"SetSampleObject\",\"args\":[{\"DT\":\"\\/Date(1181611809135)\\/\",\"Name\":\"Hujintao\",\"Height\":180,\"__type\":\"MCS.Web.WebControls.SampleObject, MCS.Web.WebControls\"}],\"state\":null}";// s.Serialize(Label1.Style);

		object o = JSONSerializerExecute.DeserializeObject(TextBox1.Text, typeof(MCS.Web.WebControls.SampleObject));
        Response.Write(o.GetType().AssemblyQualifiedName);
    }
}
