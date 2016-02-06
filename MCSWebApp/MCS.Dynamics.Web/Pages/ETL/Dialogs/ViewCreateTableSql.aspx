<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCreateTableSql.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.ViewCreateTableSql" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生成创建表SQL语句</title>
    <script type="text/javascript">
        function copyMessage() {
            var selection = document.selection;
            selection.empty();
            var r = document.body.createControlRange();
            var ma = document.getElementById('txt_CreateSql');
            try {
                
                r.add(ma);
                r.select();

                r.execCommand("Copy");
                selection.empty();
                top.close();
            }
            finally {
                
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txt_CreateSql" TextMode="MultiLine" Text="创建表SQL生成失败!"
            Height="560px" Width="100%"></asp:TextBox>
    </div>
    <div style="text-align: center;">
        <input id="btn_copy" type="button" class="formButton" value="复制并关闭(C)" accesskey="C" onclick="copyMessage();"
            style="width: 120px;" />
    </div>
    </form>
</body>
</html>
