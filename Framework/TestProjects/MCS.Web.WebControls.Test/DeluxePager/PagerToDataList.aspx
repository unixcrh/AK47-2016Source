<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagerToDataList.aspx.cs" Inherits="MCS.Web.WebControls.Test.DeluxePager.PagerToDataList1" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center"><font size ="10" color="blue">DataList</font> </div>
    <div>
        <asp:DataList ID="DataList3" runat="server" 
                CellPadding="2" ForeColor="#333333"  
                  Width="100%">
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SeparatorTemplate>
                    <hr />
                </SeparatorTemplate><ItemTemplate>
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
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            </asp:DataList>
        <cc1:deluxepager id="DeluxePager1" runat="server" oncommonpageindexchanged="DeluxePager1_CommonPageIndexChanged"
             width="100%" PagerSettings-Mode="nextpreviousfirstlast"  DataBoundControlID="OrderProductsList" PageSize="10">

</cc1:deluxepager><asp:DataList ID="DataList2" runat="server" 
                CellPadding="2" ForeColor="#333333"  
                  Width="100%" DataSourceID="SqlDataSource1" DataKeyField="SORT_ID">
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
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
    <AlternatingItemStyle BackColor="White" />
    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <SeparatorTemplate>
        <hr />
    </SeparatorTemplate>
</asp:DataList></div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\MCS-SampleDB.mdf;Integrated Security=True;User Instance=True"
            OnSelected="SqlDataSource1_Selected" ProviderName="System.Data.SqlClient" SelectCommand="SELECT TOP (@PageSize) ORDER_ID, SORT_ID, CUSTOMER_NAME, (CASE PRIORITY WHEN 0 THEN 'Normal' WHEN 1 THEN 'High' WHEN - 1 THEN 'Low' END) AS PRIORITY, CREATE_USER, CREATE_TIME, UPDATE_TAG FROM ORDERS WHERE (ORDER_ID NOT IN (SELECT TOP (@PageIndex) ORDER_ID FROM ORDERS AS ORDERS_1 ORDER BY ORDER_ID DESC)) ORDER BY ORDER_ID DESC">
            <SelectParameters>
                <asp:ControlParameter  ControlID="DeluxePager1" Name="PageSize" DefaultValue="10" PropertyName ="PageSize" />
                <asp:ControlParameter ControlID="DeluxePager1" Name="PageIndex"  DefaultValue="0" PropertyName ="PageIndex" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
