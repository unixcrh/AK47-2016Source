<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimpleDataBindingTest.aspx.cs" Inherits="MCS.Web.Responsive.WebControls.Test.DataBinding.SimpleDataBindingTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>简单绑定对象测试页面</title>
</head>
<body>
    <div class="container">
        <form id="serverForm" runat="server" class="form-horizontal">
            <res:DataBindingControl runat="server" ID="bindingControl" IsValidateOnSubmit="true" ValidateUnbindProperties="false">
                <ItemBindings>
                    <res:DataBindingItem ControlID="StringInput" DataPropertyName="StringInput" />
                    <res:DataBindingItem ControlID="DateInput" ControlPropertyName="DateValue" DataPropertyName="DateInput" />
                </ItemBindings>
            </res:DataBindingControl>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">String Input</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" CssClass="form-control" type="text" ID="StringInput" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <label class="col-sm-2 control-label" for="Name">Date Input</label>
                <div class="col-sm-10">
                    <res:DateTimePicker runat="server" ID="DateInput" Mode="DatePicker" />
                </div>
            </div>
            <div class="form-group form-group-sm">
                <div class="col-sm-8"></div>
                <div class="col-sm-4" style="">
                    <asp:Button runat="server" AccessKey="S" CssClass="btn btn-primary" ID="save" Text="保存(S)" OnClick="save_Click" />
                </div>
            </div>
            <asp:Label runat="server" ID="serverInfo"></asp:Label>
        </form>
    </div>
</body>
</html>
