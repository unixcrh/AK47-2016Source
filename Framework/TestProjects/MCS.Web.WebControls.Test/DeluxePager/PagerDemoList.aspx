<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerDemoList.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerDemoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<script runat="server">
public String UserName {
　get {
     return this.hidPagerObject.Value;
　}
}
</script>
<script type="text/javascript" language="javascript">

    function WebForm_PostBackOptions(eventTarget, eventArgument, validation, validationGroup, actionUrl, trackFocus, clientSubmit) {
    this.eventTarget = eventTarget;
    this.eventArgument = eventArgument;
    this.validation = validation;
    this.validationGroup = validationGroup;
    this.actionUrl = actionUrl;
    this.trackFocus = trackFocus;
    this.clientSubmit = clientSubmit;
}
 
function WebForm_DoPostBackWithOptions(options) {
    var validationResult = true;
    if (options.validation) {
        if (typeof(Page_ClientValidate) == 'function') {
            validationResult = Page_ClientValidate(options.validationGroup);
        }
    }

    if (validationResult) {
        if ((typeof(options.actionUrl) != "undefined") && (options.actionUrl != null) && (options.actionUrl.length > 0)) {
            theForm.action = options.actionUrl;
        }
        if (options.trackFocus) {
            var lastFocus = theForm.elements["__LASTFOCUS"];
            if ((typeof(lastFocus) != "undefined") && (lastFocus != null)) {
                if (typeof(document.activeElement) == "undefined") {
                    lastFocus.value = options.eventTarget;
                }
                else {
                    var active = document.activeElement;
                    if ((typeof(active) != "undefined") && (active != null)) {
                        if ((typeof(active.id) != "undefined") && (active.id != null) && (active.id.length > 0)) {
                            lastFocus.value = active.id;
                        }
                        else if (typeof(active.name) != "undefined") {
                            lastFocus.value = active.name;
                        }
                    }
                }
            }
        }
    }

    if (options.clientSubmit) {
        __doPostBack(options.eventTarget, options.eventArgument);
    }
}

</script>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="90%">
        <tr>
            <td>
                需要绑定的控件类型 
                <asp:DropDownList ID="ddlControlType" runat="server" OnSelectedIndexChanged="ddlControlType_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
         <td>
            是否为IDataSouce类型的数据源
                <asp:DropDownList ID="ddlIDataSource" runat="server" Width="46px" AutoPostBack="True" OnSelectedIndexChanged="ddlIDataSource_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="是" Value="yes"></asp:ListItem>
                    <asp:ListItem Text="否" Value="no"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                绑定的控件ID：<asp:TextBox ID="txtControlID" runat="server" Width="155px" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td >
               <table width="100%">
                <tr>
                    <td style="height: 20px">
                      翻页属性设置部分
                    </td>
                </tr>
                <tr>
                    <td style="height: 26px">
                      FirstPageImageUrl:
                        <asp:TextBox ID="txtFPIUrl" runat="server" Width="145px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                      FirstPageText
                        <asp:TextBox ID="txtFPText" runat="server" Width="177px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                      LastPageImageUrl
                        <asp:TextBox ID="txtLPIUrl" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                      LastPageText
                        <asp:TextBox ID="txtLPText" runat="server" Width="179px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                      分页模式Mode
                        <asp:DropDownList ID="ddlPageMode" runat="server" Width="165px"> 
                            <asp:ListItem Value="0">带编号的链接按钮</asp:ListItem>
                            <asp:ListItem Value="1">“上一页”、“下一页”、“首页”和“尾页”</asp:ListItem> 
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                      NextPageImageUrl
                        <asp:TextBox ID="txtNPIUrl" runat="server" Width="144px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                      NextPageText
                        <asp:TextBox ID="txtNPText" runat="server" Width="174px"></asp:TextBox></td>
                </tr> 
                <tr>
                    <td style="height: 26px">
                      PreviousPageImageUrl
                        <asp:TextBox ID="txtPPIUrl" runat="server" Width="123px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 26px">
                      PreviousPageText
                        <asp:TextBox ID="txtPPText" runat="server" Width="152px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 26px">
                      PageSize
                        <asp:TextBox ID="txtPageSize" runat="server" Width="152px"></asp:TextBox></td>
                </tr>
               </table>
            </td>
        </tr>
        <tr>
            <td>
            设置跳转页按钮的Text
                <asp:TextBox ID="txtGotoButtonText" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
            页码显示模式PagerCodeShowMode
                <asp:DropDownList ID="ddlPageCodeMode" runat="server">
                    <asp:ListItem Selected="True" Text="总页码" Value="RecordCount"></asp:ListItem>
                    <asp:ListItem Text="当前页/总页码" Value="CurrentRecordCount"></asp:ListItem>
                    <asp:ListItem Text="全部显示" Value="All"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
            当前的数据展示控件是否具有翻页功能
                <asp:DropDownList ID="ddlPageControl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageControl_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="是" Value="yes"></asp:ListItem>
                    <asp:ListItem Text="否" Value="no"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
              
            </td>
        </tr>
        <tr>
            <td>
                设置控件属性：<asp:Button ID="btnSetProperties" runat="server" Text="设置控件属性"  OnClick="btnSetProperties_Click"  />
                <asp:Button ID="btnGoto" runat="server" PostBackUrl="~/DeluxePager/PagerForGridView.aspx"
                    Text="控件演示" /></td>
        </tr>
        
    </table>
    </div>
        <input id="hidPagerObject" runat="server" type="hidden" />
    </form>
</body>
</html>
