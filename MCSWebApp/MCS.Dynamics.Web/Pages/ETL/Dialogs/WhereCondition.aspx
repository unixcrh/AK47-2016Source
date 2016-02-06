<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WhereCondition.aspx.cs"
    Inherits="MCS.Dynamics.Web.Pages.ETL.Dialogs.WhereCondition" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Css/dlg.css" rel="stylesheet" type="text/css" />
    <link href="../../../Css/form.css" type="text/css" rel="stylesheet" />
    <link href="../../../Css/pccom.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/jquery-1.7.2.min.js" type="text/javascript"></script>
    <style>
        .whereDataTable
        {
            width: 700px;
            text-align: center;
        }
        .outerEntityID
        {
            display: none;
        }
    </style>
    <script>
        function loadData() {
            var paramArray = window.dialogArguments;
            //alert(paramArray.length);
            $(".whereData").each(function () {

                for (var i = 0; i < paramArray.length; i++) {
                    if (paramArray[i].ETLOuterEntity_ID == $(this).find(".outerID").text()) {
                        $(this).find(".whereInput").val(paramArray[i].Condition);
                        break;
                    }
                }
            });
        }
        //获取URL参数
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        //生成一个guid
        function getGuidGenerator() {
            var S4 = function () {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            };
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }
        function onSaveClick() {
            var etlEntityID = getQueryString("id");
            var result = new Array();
            var i = 0;
            $(".whereData").each(function () {
                var returnJson = {};
                returnJson.ID = getGuidGenerator();
                if (getQueryString("job_id") != null) {
                    returnJson.Job_ID = getQueryString("job_id");
                }
                else {
                    returnJson.Job_ID = getGuidGenerator();
                }
                returnJson.ETLOuterEntity_ID = $(this).find(".outerID").text();
                returnJson.Condition = $(this).find(".whereInput").val();
                returnJson.ETLEntity_ID = etlEntityID;
                result.push(returnJson);
                i++;
            });

            window.returnValue = result;

            window.close();
        }
        $(document).ready(function () {
            
            loadData();
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" ID="scriptManager" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/Services/CommonService.asmx" />
            </Services>
        </asp:ScriptManager>
    </div>
    <div>
        <table style="height: 100%; width: 100%" width="100%">
            <tbody>
                <tr align="center">
                    <td>
                        <div id="dialogContent" class="dialogContent" style="overflow: auto; height: 100%;
                            width: 100%">
                            <div style="height: 100%; width: 100%">
                                <div class="dialogTitle">
                                    <div class="lefttitle" style="text-align: left;">
                                        <img src="../../../Images/icon_01.gif" />
                                        Where条件
                                    </div>
                                </div>
                                <div id="container" runat="server" style="text-align: center">
                                    <asp:Repeater ID="repeter" runat="server">
                                        <HeaderTemplate>
                                            <table class="whereDataTable">
                                                <tr>
                                                    <th class="outerEntityID">
                                                        外部实体ID
                                                    </th>
                                                    <th>
                                                        外部实体名称
                                                    </th>
                                                    <th>
                                                        where条件
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="whereData">
                                                <td class="outerEntityID">
                                                    <label class="outerID">
                                                        <%#Eval("ID")%></label>
                                                </td>
                                                <td>
                                                    <%#Eval("Name")%>:
                                                </td>
                                                <td>
                                                    <input type="text" class="whereInput" style="width: 300px;" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
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
                        <input type="button" runat="server" id="okButton" class="formButton" value="保存(S)"
                            accesskey="S" onclick="onSaveClick();" />
                        <input type="button" class="formButton" value="关闭(C)" accesskey="C" onclick="top.close();" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
