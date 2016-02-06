<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerToFormView.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerToFormView1" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"><font size ="10" color="blue">FormView</font> </div>
    <div>
        <asp:FormView ID="FormView1" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="FormView1_PageIndexChanging">
            <ItemTemplate> 
                 <table style="width: 100%">
                         <tr>
                            <td style="text-align: right">
                                用户:
                            </td>
                            <td>
                                <asp:TextBox ID="CREATE_USER" runat="server" Text='<%# Bind("CREATE_USER") %>'>
                                </asp:TextBox>
                            </td>
                        
                            <td style="text-align: right">
                                PRIORITY:
                            </td>
                            <td>
                                <asp:TextBox ID="PRIORITY" runat="server" Text='<%# Bind("PRIORITY") %>'>
                                </asp:TextBox>
                            </td>
                         
                            <td style="text-align: right">
                                产品名称:
                            </td>
                            <td>
                                <asp:TextBox ID="CUSTOMER_NAME" runat="server" Text='<%# Bind("CUSTOMER_NAME") %>'>
                                </asp:TextBox>
                            </td>
                         
                            <td style="text-align: right">
                                时间:
                            </td>
                            <td>
                                <asp:TextBox ID="CREATE_TIME" runat="server" Text='<%# Bind("CREATE_TIME") %>'>
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
            </ItemTemplate>
            <PagerSettings Mode="NumericFirstLast" />
        </asp:FormView>
          <cc1:deluxepager id="DeluxePager1" runat="server"  DataBoundControlID="FormView1"
            pagecontrol="True" width="100%" IDataSource="False"  PageSize="10" OnCommonPageIndexChanged="DeluxePager1_CommonPageIndexChanged">
<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings>
 
</cc1:deluxepager><asp:FormView ID="FormView2" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="FormView1_PageIndexChanging" DataSourceID="SqlDataSource1">
    <PagerSettings Mode="NumericFirstLast" />
    <ItemTemplate>
        <table style="width: 100%">
            <tr>
                <td style="text-align: right">
                    用户:
                </td>
                <td>
                    <asp:TextBox ID="CREATE_USER" runat="server" Text='<%# Bind("CREATE_USER") %>'>
                                </asp:TextBox>
                </td>
                <td style="text-align: right">
                    PRIORITY:
                </td>
                <td>
                    <asp:TextBox ID="PRIORITY" runat="server" Text='<%# Bind("PRIORITY") %>'>
                                </asp:TextBox>
                </td>
                <td style="text-align: right">
                    产品名称:
                </td>
                <td>
                    <asp:TextBox ID="CUSTOMER_NAME" runat="server" Text='<%# Bind("CUSTOMER_NAME") %>'>
                                </asp:TextBox>
                </td>
                <td style="text-align: right">
                    时间:
                </td>
                <td>
                    <asp:TextBox ID="CREATE_TIME" runat="server" Text='<%# Bind("CREATE_TIME") %>'>
                                </asp:TextBox>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:FormView>
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename='|DataDirectory|MCS-SampleDB.mdf';Integrated Security=True;User Instance=True"
            OnSelected="SqlDataSource1_Selected" ProviderName="System.Data.SqlClient" SelectCommand="SELECT  *  FROM ORDERS">
            <FilterParameters>
                <asp:Parameter DefaultValue="10" Name="@PageSize" />
            </FilterParameters>
        </asp:SqlDataSource><asp:FormView ID="FormView3" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="FormView1_PageIndexChanging">
            <ItemTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            用户:
                        </td>
                        <td>
                            <asp:TextBox ID="CREATE_USER" runat="server" Text='<%# Bind("CREATE_USER") %>'>
                                </asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            PRIORITY:
                        </td>
                        <td>
                            <asp:TextBox ID="PRIORITY" runat="server" Text='<%# Bind("PRIORITY") %>'>
                                </asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            产品名称:
                        </td>
                        <td>
                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" Text='<%# Bind("CUSTOMER_NAME") %>'>
                                </asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            时间:
                        </td>
                        <td>
                            <asp:TextBox ID="CREATE_TIME" runat="server" Text='<%# Bind("CREATE_TIME") %>'>
                                </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <PagerSettings Mode="NumericFirstLast" />
        </asp:FormView>
    </form>
</body>
</html>
