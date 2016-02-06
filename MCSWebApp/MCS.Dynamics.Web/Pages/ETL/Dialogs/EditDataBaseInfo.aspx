<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDataBaseInfo.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.EditDataBaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据库登录信息</title>
    <script src="../../../JavaScript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../../../Css/form.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/pccssreform.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/basePage.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="table_form m_25" width="95%" align="center" id="addtable">
            <tr>
                <th>
                    <label for="txtLoginID">
                        数据库登录账号</label>
                </th>
                <td style="width: 20%">
                    <asp:TextBox ID="txtLoginID" runat="server">
                    </asp:TextBox>
                </td>
                <th>
                    <label for="txtDBAddr">
                        吉林数据库连接地址</label>
                </th>
                <td style="width: 20%">
                    <input type="text" id="txtDBAddr" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <label for="txtDBName">
                        数据库名</label>
                </th>
                <td style="width: 20%">
                    <asp:TextBox ID="txtDBName" runat="server">
                    </asp:TextBox>
                </td>
                 <th>
                    <label for="">
                        北京服务器地址</label>
                </th>
                <td style="width: 20%">
                    <asp:TextBox ID="txtRDBAddr" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                 <th>
                    <label for="txtPassword">
                        数据库登录密码</label>
                </th>
                <td style="width: 20%">
                    <asp:TextBox ID="txtPassword" runat="server">
                    </asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>

            <tr>
                <td colspan="4">
                    <div style="text-align: right">
                        <input type="submit" runat="server" id="btnOk" class="formButton" value="确定(O)" accesskey="O"
                            onserverclick="btnSave_Click" />
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
