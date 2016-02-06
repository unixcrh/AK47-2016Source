<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeluxeSelectTest.aspx.cs"
	Inherits="MCS.Web.WebControls.Test.DeluxeSelect.DeluxeSelectTest" %>

<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>数据选择控件测试页面</title>
	<script type="text/javascript" language="javascript" type="text/javascript">
		function getResult() {
			//        var qq= document.getElementById("ccDeluxeSelect_ClientState");
			//        alert(document.getElementById("ccDeluxeSelect_ClientState").value);
			alert(document.getElementById("ctrlDeluxeSelect_ClientState").value);
		}
	</script>
	<link href="DeluxeSelect.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	<table>
		<tr style="vertical-align: top">
			<td style="width: 400px">
				<asp:CheckBox ID="ckbShowSelectButton" runat="server" Text="ShowSelectButton" TextAlign="left"
					Checked="True" /><br />
				<label style="font-size: small; font-style: italic">
					'选择'按钮是否显示</label><br />
				<asp:CheckBox ID="ckbShowSelectAllButton" runat="server" Text="ShowSelectAllButton"
					TextAlign="left" Checked="True" /><br />
				<label style="font-size: small; font-style: italic">
					'全部选择'按钮是否显示</label><br />
				<br />
				SelectButtonText
				<asp:DropDownList ID="ddlSelectButtonText" runat="server">
					<asp:ListItem>选择</asp:ListItem>
					<asp:ListItem>></asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					'选择'按钮的Text</label><br />
				SelectAllButtonText
				<asp:DropDownList ID="ddlSelectAllButtonText" runat="server">
					<asp:ListItem>全部选择</asp:ListItem>
					<asp:ListItem>>></asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					'全部选择'按钮的Text</label><br />
				<br />
				CancelButtonText
				<asp:DropDownList ID="ddlCancelButtonText" runat="server">
					<asp:ListItem>取消</asp:ListItem>
					<asp:ListItem><</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					'取消'按钮的Text</label><br />
				CancelAllButtonText
				<asp:DropDownList ID="ddlCancelAllButtonText" runat="server">
					<asp:ListItem>全部取消</asp:ListItem>
					<asp:ListItem><<</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					'全部取消'按钮的Text</label><br />
				<br />
				SelectButtonCssClass
				<asp:DropDownList ID="ddlSelectButtonCssClass" runat="server">
					<asp:ListItem>Default</asp:ListItem>
					<asp:ListItem>SelectButtonCssClass_Demo1</asp:ListItem>
					<asp:ListItem>SelectButtonCssClass_Demo2</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					选择按钮的Css样式</label><br />
				MoveButtonCssClass
				<asp:DropDownList ID="ddlMoveButtonCssClass" runat="server">
					<asp:ListItem>Default</asp:ListItem>
					<asp:ListItem>MoveButtonCssClass_Demo1</asp:ListItem>
					<asp:ListItem>MoveButtonCssClass_Demo2</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					上下移按钮的Css样式</label><br />
				<br />
				<asp:CheckBox ID="ckbMoveOption" runat="server" Text="MoveOption" TextAlign="left"
					Checked="True" /><br />
				<label style="font-size: small; font-style: italic">
					是否允许移动列表数据项</label><br />
			</td>
			<td>
				DataSource<br />
				<label style="font-size: small; font-style: italic">
					设置数据源，将初始化DeluxeSelect控件的CandidateItems</label><br />
				<table style="text-align: center; border-style: double; font-size: small">
					<tr>
						<td style="width: 100px; border-style: double">
							Column1
						</td>
						<td style="width: 100px; border-style: double">
							Column2
						</td>
						<td style="width: 100px; border-style: double">
							Column3
						</td>
					</tr>
					<tr>
						<td style="border-style: double">
							北京海关
						</td>
						<td style="border-style: double">
							0001
						</td>
						<td style="border-style: double">
							1
						</td>
					</tr>
					<tr>
						<td style="border-style: double">
							南宁海关
						</td>
						<td style="border-style: double">
							7200
						</td>
						<td style="border-style: double">
							2
						</td>
					</tr>
					<tr>
						<td style="border-style: double">
							拱北海关
						</td>
						<td style="border-style: double">
							5700
						</td>
						<td style="border-style: double">
							3
						</td>
					</tr>
				</table>
				DataSourseTextField = "Column1"<br />
				<label style="font-size: small; font-style: italic">
					数据源的TextFiled</label><br />
				DataSourseValueField = "Column2"<br />
				<label style="font-size: small; font-style: italic">
					数据源的ValueFiled</label><br />
				DataSourseSortField = "Column3"<br />
				<label style="font-size: small; font-style: italic">
					数据源的SortFiled</label><br />
				&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
				<asp:Button ID="btnSetDataSource" runat="server" Text="Set DataSource" OnClick="btnSetDataSource_Click" />
				<hr />
				ButtonItems<br />
				<label style="font-size: small; font-style: italic">
					按钮的数据集，ButtonItem类型值的集合</label><br />
				<label style="font-size: small">
					ButtonName</label>
				<asp:TextBox ID="txbButtonName" runat="server" Width="70px">主办</asp:TextBox><br />
				<label style="font-size: small">
					ButtonSortID</label>
				<asp:TextBox ID="txbButtonSortID" runat="server" Width="70px">0</asp:TextBox><br />
				<label style="font-size: small">
					ButtonTypeMaxCount</label>
				<asp:TextBox ID="txbButtonTypeMaxCount" runat="server" Width="70px">1</asp:TextBox><br />
				<label style="font-size: small">
					ButtonCssClass</label>
				<asp:DropDownList ID="ddlButtonCssClass" runat="server">
					<asp:ListItem>ajax_deluxeselect_button</asp:ListItem>
					<asp:ListItem>ButtonCssClass_Demo1</asp:ListItem>
					<asp:ListItem>ButtonCssClass_Demo2</asp:ListItem>
				</asp:DropDownList>
				<br />
				&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
				<asp:Button ID="btnButtonItemAdd" runat="server" Text="Add" OnClick="btnButtonItemAdd_Click" /><br />
			</td>
		</tr>
	</table>
	<hr />
	<table>
		<tr style="vertical-align: top">
			<td style="width: 400px">
				CandidateListCssClass
				<asp:DropDownList ID="ddlCandidateListCssClass" runat="server">
					<asp:ListItem>Default</asp:ListItem>
					<asp:ListItem>CandidateListCssClass_Demo1</asp:ListItem>
					<asp:ListItem>CandidateListCssClass_Demo2</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					待选择列表的Css样式</label><br />
				CandidateListSortDirection
				<asp:DropDownList ID="ddlCandidateListSortDirection" runat="server">
					<asp:ListItem>升序</asp:ListItem>
					<asp:ListItem>降序</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					待选择列表排序方式（升序、降序），默认为升序</label><br />
				CandidateSelectionMode
				<asp:DropDownList ID="ddlCandidateSelectionMode" runat="server">
					<asp:ListItem>单选</asp:ListItem>
					<asp:ListItem>多选</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					待选择列表的选择模式（单选、多选），默认为单选</label><br />
				CandidateItems<br />
				<label style="font-size: small; font-style: italic">
					待选择列表的数据集，SelectItem类型值的集合</label><br />
				<label>
					SelectListBoxText</label>
				<asp:TextBox ID="txbCandidateText" runat="server" Width="70px">测试A</asp:TextBox><br />
				<label>
					SelectListBoxValue</label>
				<asp:TextBox ID="txbCandidateValue" runat="server" Width="70px">2</asp:TextBox><br />
				<label>
					SelectListBoxSortColumn</label>
				<asp:TextBox ID="txbCandidateSort" runat="server" Width="70px">2</asp:TextBox><br />
				<asp:CheckBox ID="ckbCandidateLocked" runat="server" Text="　　Locked" TextAlign="left"
					Checked="false" /><br />
				&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
				<asp:Button ID="btnCandidateAdd" runat="server" Text="Add" OnClick="btnCandidateAdd_Click" /><br />
			</td>
			<td style="width: 339px">
				SelectedListCssClass
				<asp:DropDownList ID="ddlSelectedListCssClass" runat="server">
					<asp:ListItem>Default</asp:ListItem>
					<asp:ListItem>SelectedListCssClass_Demo1</asp:ListItem>
					<asp:ListItem>SelectedListCssClass_Demo2</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					已选择列表的Css样式</label><br />
				SelectedListSortDirection
				<asp:DropDownList ID="ddlSelectedListSortDirection" runat="server">
					<asp:ListItem>升序</asp:ListItem>
					<asp:ListItem>降序</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					已选择列表排序方式（升序、降序），默认为升序</label><br />
				SelectedSelectionMode
				<asp:DropDownList ID="ddlSelectedSelectionMode" runat="server">
					<asp:ListItem>单选</asp:ListItem>
					<asp:ListItem>多选</asp:ListItem>
				</asp:DropDownList>
				<br />
				<label style="font-size: small; font-style: italic">
					已选择列表的选择模式（单选、多选），默认为单选</label><br />
				SelectedItems<br />
				<label style="font-size: small; font-style: italic">
					已选择列表的数据集，SelectItem类型值的集合</label><br />
				<label>
					SelectListBoxText</label>
				<asp:TextBox ID="txbSelectedText" runat="server" Width="70px">测试B</asp:TextBox><br />
				<label>
					SelectListBoxValue</label>
				<asp:TextBox ID="txbSelectedValue" runat="server" Width="70px">1</asp:TextBox><br />
				<label>
					SelectListBoxSortColumn</label>
				<asp:TextBox ID="txbSelectedSort" runat="server" Width="70px">1</asp:TextBox><br />
				<asp:CheckBox ID="ckbSelectedLocked" runat="server" Text="　　Locked" TextAlign="left"
					Checked="false" /><br />
				&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
				<asp:Button ID="btnSelectedAdd" runat="server" Text="Add" OnClick="btnSelectedAdd_Click" /><br />
			</td>
		</tr>
	</table>
	<hr />
	<table>
		<tr>
			<td>
				<asp:Button ID="btnSetProperties" runat="server" Text="SetProperties" OnClick="btnSetProperties_Click" />
				<asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="Submit" /><br />
				<asp:Label ID="Label1" runat="server"></asp:Label><br />
				<cc1:DeluxeSelect ID="ctrlDeluxeSelect" runat="server" DataSourseSortField="" DataSourseTextField=""
					DataSourseValueField="">
				</cc1:DeluxeSelect>
			</td>
			<td>
				<textarea id="ctrlDeluxeSelectHtmlShow" runat="server" style="width: 550px; height: 270px;"
					enableviewstate="true"></textarea>
			</td>
		</tr>
	</table>
	<input id="Button1" type="button" value="Get ClientState" onclick="getResult();" />
	</form>
</body>
</html>
