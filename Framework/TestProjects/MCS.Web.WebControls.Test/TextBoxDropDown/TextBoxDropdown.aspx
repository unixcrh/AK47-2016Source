<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBoxDropdown.aspx.cs" Inherits="MCS.Web.WebControls.Test.TextBoxDropDown.TextBoxDropdown" %>
<%@ Register Assembly="MCS.Web.WebControls" Namespace="MCS.Web.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                    <table class="style1">
        <tr>
            <td align="right">
            <asp:TextBox ID="TextBox2" runat="server" />
            <cc1:TextBoxDropdownExtender ID="TextBoxDropdownExtender1" runat="server" TargetControlID="TextBox2"/>
            </td>
        </tr>
        <tr>
            <td>
            <select id="Select2">
            <option>1</option>
            <option>1</option>
            <option>1</option>
        </select>
</td>
        </tr>
    </table>

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />


            <table class="style1">
        <tr>
            <td align="right">
            <asp:TextBox ID="TextBox1" runat="server" />
            <cc1:TextBoxDropdownExtender ID="asd" runat="server" TargetControlID="TextBox1"/>
            </td>
        </tr>
        <tr>
            <td>
            <select id="Select1">
            <option>1</option>
            <option>1</option>
            <option>1</option>
        </select>
</td>
        </tr>
    </table>

    
    
    </form>
</body>
</html>
