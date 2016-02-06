<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testTreeYibu.aspx.cs" Inherits="MCS.Dynamics.Web.testTreeYibu" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="DeluxeWorks" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="Css/home.css" rel="stylesheet" type="text/css" />
    <link href="Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/pc.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        wefw
        <div class="pc-frame-header">
            sadfsaefwaf
            <pc:Banner ID="pcBanner" runat="server" ActiveMenuIndex="2" />
        </div>
        sdafsdf
        <DeluxeWorks:DeluxeTree ID="tree" runat="server" OnGetChildrenData="tree_GetChildrenData">
            <Nodes>
                <DeluxeWorks:DeluxeTreeNode Text="第三个节点" CssClass="" NodeCloseImg="" NodeOpenImg=""
                    ChildNodesLoadingType="LazyLoading" NavigateUrl="" Target="" Selected="false" />
            </Nodes>
        </DeluxeWorks:DeluxeTree>
    </div>
    </form>
</body>
</html>
