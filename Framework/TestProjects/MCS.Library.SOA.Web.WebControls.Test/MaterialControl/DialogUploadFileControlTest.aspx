<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogUploadFileControlTest.aspx.cs" Inherits="MCS.Library.SOA.Web.WebControls.Test.MaterialControl.DialogUploadFileControlTest" %>

<%@ Register Assembly="MCS.Library.SOA.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="SOA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传窗口的测试页面</title>
    <script type="text/javascript">
        function onShowDialog(controlID) {
            var materialControl = $find(controlID);

            window.dialogArguments = materialControl._prepareDialogUploadFileArguments();
            window.open(materialControl.get_preferedDialogUploadFileControlUrl());
        }
    </script>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <SOA:MaterialControl ID="activeXMaterialControl" MaterialUseMode="UploadFile" RootPathName="GenericProcess" runat="server" AllowEditContent="False" />
            <input id="showActiveXUploadDialog" type="button" value="使用ActiveX的上传对话框" onclick="onShowDialog('activeXMaterialControl');" />
        </div>
        <div>
            <SOA:MaterialControl ID="noActiveXMaterialControl1" MaterialUseMode="UploadFile" RootPathName="GenericProcess" runat="server" AllowEditContent="False" FileSelectMode="TraditionalSingle" />
            <input id="showNoActiveXUploadDialog" type="button" value="无ActiveX的上传对话框" onclick="onShowDialog('noActiveXMaterialControl1');" />
        </div>
    </form>
</body>
</html>
