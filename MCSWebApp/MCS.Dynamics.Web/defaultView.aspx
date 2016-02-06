<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="defaultView.aspx.cs" Inherits="MCS.Dynamics.Web.defaultView" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--	<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectCountMethod="GetFilteredDataCount"
				SelectMethod="GetFilteredData" TypeName="MCS.Dynamics.Web.DEEntitySnapshot"
				EnablePaging="True" SortParameterName="sortExpression">
				<SelectParameters>
					<asp:ControlParameter ControlID="prioritySelector" PropertyName="SelectedValue" Name="priority"
						Type="String" />
				</SelectParameters>
			</asp:ObjectDataSource>--%>
       <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="server=.;database=DynamicsEntityDB;uid=sa;pwd=COM.aec;"
            ProviderName="System.Data.SqlClient" SelectCommand="select * from DE.EntitySnapshot">
        </asp:SqlDataSource>--%>
        <cc1:DeluxeGrid ID="grid" runat="server" >
            <EmptyDataTemplate>
                没有任何数据
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField AccessibleHeaderText="列1" DataField="col1" Visible="false" />
            </Columns>
        </cc1:DeluxeGrid>

    </div>
    </form>
</body>
</html>
