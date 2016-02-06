<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showObjectInfo.aspx.cs"
    Inherits="MCS.Dynamics.Web.Dialogs.ShowObjectInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" scroll="no" style="overflow: hidden">
<head runat="server">
    <title>对象信息</title>
    <base target="_self" />
    <link href="../Css/dlg.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../scripts/JSLINQ.js" type="text/javascript"></script>
    <script src="../scripts/pc.js" type="text/javascript"></script>
    <script src="../scripts/Entity/showObjectInfo.js" type="text/javascript"></script>
</head>
<body onload="onDocumentLoad()">
    <form id="serverForm" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
    </div>
    <pc:SceneControl runat="server" />
    <div>
        <soa:PropertyForm runat="server" ID="propertyForm" Width="100%" Height="100%" AutoSaveClientState="False" />
    </div>
    <div class="pcdlg-floor">
        <div class="pcdlg-button-bar">
            <input type="button" id="okButton" runat="server" onclick="return ($pc.getEnabled(this) && onSaveClick())"
                accesskey="S" class="pcdlg-button btn-def" value="保存(S)" /><input type="button" accesskey="C"
                    class="pcdlg-button btn-cancel" onclick="window.returnValue = false;window.close();"
                    value="关闭(C)" />
        </div>
    </div>
    <div style="display: none">
        <input type="hidden" id="properties" runat="server" />
        <input type="hidden" id="currentSchemaType" runat="server" />
        <input type="hidden" id="currentParentID" runat="server" />
        <soa:SubmitButton runat="server" ID="btSave" OnClick="Save_Click" Text="保存(S)" CssClass="pcdlg-button btn-def"
            RelativeControlID="okButton" PopupCaption="正在保存..." />
    </div>
    <script type="text/javascript" src="../scripts/PermissionPropertyEditors.js"></script>
    <input type="hidden" id="hd_errorMsg"/>
    </form>
</body>
</html>

