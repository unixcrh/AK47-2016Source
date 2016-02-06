<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnumDropDownFieldTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeGrid.EnumDropDownFieldTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="mcs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>枚举列</title>
	<style>
		fieldset
		{
			padding: 10px;
			margin-bottom: 20px;
			background: AliceBlue;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<fieldset>
		<legend>只读状态下绑定到实体</legend>
		<div>
			<p>
				只读状态下绑定到实体中的枚举字段时，只需配置好DataField即可。
			</p>
		</div>
		<div>
			<mcs:DeluxeGrid runat="server" ID="grid1" DataSourceMaxRow="0" GridTitle="" ShowExportControl="False"
				TitleColor="141, 143, 149" TitleFontSize="Large" AutoGenerateColumns="False"
				DataSourceID="ObjectDataSource1" AllowPaging="True">
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" />
					<mcs:EnumDropDownField DataField="Result" HeaderText="Result" SortExpression="Result">
					</mcs:EnumDropDownField>
				</Columns>
			</mcs:DeluxeGrid>
			<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetCachedCollection"
				TypeName="MCS.Web.WebControls.Test.DeluxeGrid.EnumDataEntityCollection" DataObjectTypeName="MCS.Web.WebControls.Test.DeluxeGrid.EnumDataEntity"
				InsertMethod="Add" UpdateMethod="UpdateCachedEntity"></asp:ObjectDataSource>
		</div>
	</fieldset>
	<fieldset>
		<legend>编辑状态下绑定到实体</legend>
		<div>
			<p>
				编辑状态下绑定到实体中的枚举字段时，必须配置好EnumTypeName属性。
			</p>
		</div>
		<div>
			<mcs:DeluxeGrid runat="server" ID="grid2" DataSourceMaxRow="0" ExportingDeluxeGrid="False"
				GridTitle="" ShowExportControl="False" TitleColor="141, 143, 149" TitleFontSize="Large"
				AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" AllowPaging="True"
				CascadeControlID="">
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" />
					<mcs:EnumDropDownField DataField="Result" HeaderText="Result" SortExpression="Result"
						EnumTypeName="MCS.Web.WebControls.Test.DeluxeGrid.SomeEnum, MCS.Web.WebControls.Test" />
					<asp:CommandField ShowEditButton="True" />
				</Columns>
			</mcs:DeluxeGrid>
			<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetCachedCollection"
				TypeName="MCS.Web.WebControls.Test.DeluxeGrid.EnumDataEntityCollection" DataObjectTypeName="MCS.Web.WebControls.Test.DeluxeGrid.EnumDataEntity"
				InsertMethod="Add" UpdateMethod="UpdateCachedEntity"></asp:ObjectDataSource>
		</div>
	</fieldset>
	<fieldset>
		<legend>编辑状态下绑定到表</legend>
		<div>
			<p>
				编辑状态下绑定到表中的字段（整数）。必须配置好EnumTypeName属性。
			</p>
		</div>
		<div>
			<mcs:DeluxeGrid runat="server" ID="DeluxeGrid1" DataSourceMaxRow="0" ExportingDeluxeGrid="False"
				GridTitle="" ShowExportControl="False" TitleColor="141, 143, 149" TitleFontSize="Large"
				AutoGenerateColumns="False" DataSourceID="ObjectDataSource3" AllowPaging="True"
				DataKeyNames="ID" CascadeControlID="">
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" />
					<mcs:EnumDropDownField DataField="Result" HeaderText="Result" SortExpression="Result"
						EnumTypeName="MCS.Web.WebControls.Test.DeluxeGrid.SomeEnum, MCS.Web.WebControls.Test" />
					<asp:CommandField ShowEditButton="True" />
				</Columns>
			</mcs:DeluxeGrid>
			<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetCachedTable"
				TypeName="MCS.Web.WebControls.Test.DeluxeGrid.EnumDataEntityCollection" UpdateMethod="UpdateCachedRow">
				<UpdateParameters>
					<asp:Parameter Name="id" Type="String" />
					<asp:Parameter Name="result" Type="Int32" />
				</UpdateParameters>
			</asp:ObjectDataSource>
		</div>
	</fieldset>
	<fieldset>
		<legend>编辑状态下绑定到表</legend>
		<div>
			<p>
				编辑状态下绑定到表中的字段（枚举名）。必须配置好EnumTypeName属性。 此时，还应将EnumDropDownField的UseNameAsValue属性设置为true。
			</p>
		</div>
		<div>
			<mcs:DeluxeGrid runat="server" ID="DeluxeGrid2" DataSourceMaxRow="0" ExportingDeluxeGrid="False"
				GridTitle="" ShowExportControl="False" TitleColor="141, 143, 149" TitleFontSize="Large"
				AutoGenerateColumns="False" DataSourceID="ObjectDataSource4" AllowPaging="True"
				DataKeyNames="ID" CascadeControlID="">
				<Columns>
					<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" />
					<mcs:EnumDropDownField DataField="Result" HeaderText="Result" SortExpression="Result"
						UseNameAsValue="true" EnumTypeName="MCS.Web.WebControls.Test.DeluxeGrid.SomeEnum, MCS.Web.WebControls.Test" />
					<asp:CommandField ShowEditButton="True" />
				</Columns>
			</mcs:DeluxeGrid>
			<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetCachedTable2"
				TypeName="MCS.Web.WebControls.Test.DeluxeGrid.EnumDataEntityCollection" UpdateMethod="UpdateCachedRow2">
				<UpdateParameters>
					<asp:Parameter Name="id" Type="String" />
					<asp:Parameter Name="result" Type="String" />
				</UpdateParameters>
			</asp:ObjectDataSource>
		</div>
	</fieldset>
	</form>
</body>
</html>
