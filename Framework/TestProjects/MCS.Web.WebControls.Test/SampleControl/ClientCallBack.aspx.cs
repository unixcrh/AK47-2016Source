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

using MCS.Web.Library;
using MCS.Web.WebControls;

public partial class SampleControl_ClientCallBack : System.Web.UI.Page,
     System.Web.UI.ICallbackEventHandler

{
    private WindowFeature wf = new WindowFeature();
    protected string WindowFeatureStr
    {
        get
        {
            return wf.ToWindowFeatureClientString();
        }
    }

    protected string DialogFeatureStr
    {
        get
        {
            return wf.ToDialogFeatureClientString();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        wf.Width = 800;
        wf.Height = 700;
        wf.Center = true;
        Response.Clear();
        Response.Write(wf.ToAdjustWindowScriptBlock(true));
        Response.End();
        //base.OnInit(e);
    }

    protected System.Collections.Specialized.ListDictionary catalog;
    protected String returnValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        WebUtility.ShowClientConfirm("Message", "Detailfdsafdsafsdafdsaf\nfdsafdsafsdafsdaf\nfdsafdsafdsafsdafsad", "titleOK",
    "OK", "cancel", null, "onCancel");
        wf.Width = 500;
        wf.Height = 700;
        wf.Center = true;
        WebUtility.AdjustWindow(wf);

        String cbReference =
            Page.ClientScript.GetCallbackEventReference(this,
            "arg", "ReceiveServerData", "context");
        String callbackScript;
        callbackScript = "function CallServer(arg, context)" +
            "{ " + cbReference + ";}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
            "CallServer", callbackScript, true);

        catalog = new System.Collections.Specialized.ListDictionary();
        catalog.Add("monitor", 12);
        catalog.Add("laptop", 10);
        catalog.Add("keyboard", 23);
        catalog.Add("mouse", 17);

        ListBox1.DataSource = catalog;
        ListBox1.DataTextField = "key";
        ListBox1.DataBind();

    }

    public void RaiseCallbackEvent(String eventArgument)
    {
        if (catalog[eventArgument] == null)
        {
            returnValue = "-1";
        }
        else
        {
            returnValue = catalog[eventArgument].ToString();
        }
    }
    public String GetCallbackResult()
    {
        return returnValue;
    }

}
