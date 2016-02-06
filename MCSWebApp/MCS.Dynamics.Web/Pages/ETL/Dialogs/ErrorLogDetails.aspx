<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorLogDetails.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.ErrorLogDetails" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>容错列表详细</title>
    <base target="_self" />
    <link href="../../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/form.css" type="text/css" rel="stylesheet" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../../scripts/MicrosoftAjax.debug.js" type="text/javascript"></script>
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../scripts/pc.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            overflow-y: auto;
        }
        .tb_form_grid
        {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }
        
        .tb_form_grid td
        {
            text-align: left;
            border: 1px solid #f0f0f0;
            line-height: 25px; /*height: 25px;*/
        }
    </style>
    <script type="text/javascript">
        function onSaveClick() {
            $get("btn_save").click();
        }
        function onRemoveClick() {
            $get("btn_remove_Error").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="height: 100%; width: 100%" width="100%">
                <tbody>
                    <tr align="center">
                        <td>
                            <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                                width: 100%">
                                <div style="height: 100%; width: 100%">
                                    <div class="dialogTitle">
                                    </div>
                                    <div>
                                    </div>
                                    <!--表单开始-->
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../../../Images/icon_01.gif" />
                                        容错列表属性
                                        <mcs:TimePointDisplayControl ID="TimePointDisplayControl1" runat="server" />
                                    </div>
                                    <div class="dialogContent">
                                        <table class="tb_form_grid" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    执行时间
                                                </td>
                                                <td>
                                                    <asp:Label ID="exTime" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    执行的SQL
                                                </td>
                                                <td title="单击复制到剪切板" style="height: auto; cursor: pointer;" onclick="clipboardData.setData('Text',$('#execSql').html());alert('复制成功');">
                                                    <asp:Label ID="execSql" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    创建人
                                                </td>
                                                <td>
                                                    <asp:Label ID="createor" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    错误信息
                                                </td>
                                                <td style="word-wrap: break-word;">
                                                    <asp:Label ID="errorMsg" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    任务
                                                </td>
                                                <td>
                                                    <asp:Label ID="taskJob" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    错误类型
                                                </td>
                                                <td>
                                                    <asp:Label ID="errorType" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <!--表单结束-->
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="gridfileBottom">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px; vertical-align: middle; text-align: center">
                            <input type="button" runat="server" id="btn_remove" class="formButton" value="移除(Q)"
                                accesskey="S" onclick="return ($pc.getEnabled(this) && onRemoveClick());" />
                            <input type="button" runat="server" id="okButton" class="formButton" value="执行(S)"
                                accesskey="S" onclick="return ($pc.getEnabled(this) && onSaveClick());" />
                            <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                            <div style="display: none;">
                                <soa:SubmitButton runat="server" Text="执行" PopupCaption="正在操作..." ID="btn_remove_Error"
                                    OnClick="btn_remove_Error_Click" />
                                <soa:SubmitButton runat="server" Text="执行" PopupCaption="正在操作..." ID="btn_save" OnClick="btn_Save_Click" />
                                <soa:HBDropDownList ID="ddl_FieldType" runat="server">
                                </soa:HBDropDownList>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
