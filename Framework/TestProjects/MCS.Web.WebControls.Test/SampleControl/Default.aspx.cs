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
using System.Collections.Generic;
using MCS.Web.Library;
using MCS.Web.WebControls;

public partial class SampleControl_Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		//System.Threading.Thread.Sleep(1000); 
		if (!this.IsPostBack)
		{
			this.SampleControl1.InputStyle2.ForeColor = System.Drawing.Color.Blue;
		   // this.SampleControl1.InputStyle.Add("color", "red");
			//WebUtility.ShowClientMessage("Message", "Detailfdsafdsafsdafdsaf\nfdsafdsafsdafsdaf\nfdsafdsafdsafsdafsad", "titleOK");
			WebUtility.ShowClientConfirm("Message", "Detailfdsafdsafsdafdsaf\nfdsafdsafsdafsdaf\nfdsafdsafdsafsdafsad", "titleOK",
				"OK", "cancel", "onOK", "onCancel");

			WindowFeature wf = new WindowFeature();
			wf.Width = 200;
			wf.ShowAddressBar = true;
			Response.Write(WindowFeatureHelper.GetClientObject(wf));
		}
		//WebUtility.RegisterClientMessageScript();
	}

	public int SetSampleObject(MCS.Web.WebControls.SampleObject[] o)
	{
		return o.Length;
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		SampleControl ctr = new SampleControl();
		this.Form.Controls.Add(ctr);
	}

	protected void Button1_Click(object sender, EventArgs e)
	{

	}

	protected void OnSampleControlClick(object sender, EventArgs e)
	{
	}

	protected void SampleControl1_Unload(object sender, EventArgs e)
	{

	}
}

public class test
{
	public test()
	{
	}

	public ICollection GetSampleObjectList(int maximumRows, int startRowIndex)
	{
		ArrayList l = new ArrayList();
		l.Add(new SampleObject(DateTime.Now, "1", 2));
		l.Add(new SampleObject(DateTime.Now, "2", 2));
		l.Add(new SampleObject(DateTime.Now, "3", 2));
		l.Add(new SampleObject(DateTime.Now, "4", 2));
		return l;
	}

	public int GetSampleObjectCount()
	{
		return 200;
	}
}
