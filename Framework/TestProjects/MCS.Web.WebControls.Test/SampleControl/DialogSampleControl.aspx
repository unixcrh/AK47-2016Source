<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DialogSampleControl.aspx.cs"
    Inherits="MCS.Web.WebControls.Test.SampleGrid.DialogSampleControl" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;
            <cc1:ShowModalDialogSampleControl ID="ShowModalDialogSampleControl" runat="server" Text="OKKKKKKK">
                <DialogControlTemplate>
                    <cc1:DialogSampleControl ID="DialogSampleControl1" DataControlClientPropertyName="text" runat="server">
                        <datacontroltemplate>
                         <cc1:SampleControl ID="SampleControl1" runat="server">
                        </cc1:SampleControl>
                       </datacontroltemplate>
                    </cc1:DialogSampleControl>
                </DialogControlTemplate>
            </cc1:ShowModalDialogSampleControl>
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <asp:Button ID="Button2" runat="server" Text="Button" />
        </div>
    </form>
</body>
</html>
