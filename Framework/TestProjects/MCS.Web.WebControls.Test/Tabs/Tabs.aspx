<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Tabs.aspx.cs" Inherits="MCS.Web.WebControls.Test.Tabs.Tabs" %>

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
            <cc1:TabContainer runat="server" ID="TabContainer1" AutoPostBack="true">
                <cc1:TabPanel runat="server" ID="TabPanel1" HeaderText="TabPanel1">
                    <ContentTemplate>
                        TabPanel1
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="TabPanel2" HeaderText="TabPanel2" Enabled="false">
                    <ContentTemplate>
                        TabPanel2
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="TabPanel3" HeaderText="TabPanel3">
                    <ContentTemplate>
                        <asp:Calendar ID="Calendar1" runat="server" />
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="TabPanel4" HeaderText="TabPanel4">
                    <ContentTemplate>
                        <asp:Button ID="Button3" runat="server" Text="TabPanel4" />
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <asp:Button runat="Server" ID="Button1" />
        </div>
    </form>
</body>
</html>
