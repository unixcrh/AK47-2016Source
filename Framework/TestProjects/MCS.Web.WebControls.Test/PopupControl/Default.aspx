<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="MCS.Web.WebControls.Test.PopupControl.Default1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Scripts>
                    <%--<asp:ScriptReference Assembly="MCS.Web.Library" Name="MCS.Web.Library.Script.Resources.Blocking.js" />
                    <asp:ScriptReference Assembly="MCS.Web.Library" Name="MCS.Web.Library.Script.Resources.DeluxeAjax.js" />
                    <asp:ScriptReference Assembly="MCS.Web.Library" Name="MCS.Web.Library.Script.Resources.PopupControl.js" />--%>
					<asp:ScriptReference Assembly="MCS.Web.Library" Name="MCS.Web.Library.Script.Resources.ControlBase.js" />
                </Scripts>
            </asp:ScriptManager>
            <div style="height: 100px">
                fffff</div>
            <input type="button" onclick="showPopupWindow()" value="test" />
            <input type="text" id="tb1" />
            <br />
            <input type="text" id="Text1" />
            <div id="divtest" style="padding-right: 10px; padding-left: 13px; filter: progid:DXImageTransform.Microsoft.Shadow(direction=135,color=#ff00ff,strength=5);
                font: bold 9pt/1.3 verdana; width: 305px; color: darkred; height: 150px; background-color: #ffffff">
                fdsafsdafsdafsdafsdfsfdsafsdaf
            </div>
            <div id="imgObj" style="padding-right: 10px; padding-left: 13px; filter: progid:DXImageTransform.Microsoft.Shadow(direction=135,color=#ff00ff,strength=5);
                font: bold 9pt/1.3 verdana; color: darkred; background-color: skyblue">
               
                <div id="imgObjText">
                    <br>
                    The image, dark red text, and sky blue ba<asp:Menu ID="Menu1" runat="server">
                        <Items>
                            <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                            <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                            <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                            <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                            <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                            <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                            <asp:MenuItem SeparatorImageUrl="~/PopupMenu/calenderDrop.gif" Text="22222222" Value="新建项">
                            </asp:MenuItem>
                        </Items>
                        <StaticItemTemplate>
                            <%# Eval("Text") %>
                        </StaticItemTemplate>
                    </asp:Menu>
                    ckground make up the content of the filtered
                    SPAN.<asp:TreeView ID="TreeView1" runat="server">
                        <Nodes>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点">
                                <asp:TreeNode Text="新建节点" Value="新建节点">
                                    <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                                </asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                            <asp:TreeNode Text="新建节点" Value="新建节点">
                                <asp:TreeNode Text="新建节点" Value="新建节点">
                                    <asp:TreeNode Text="新建节点" Value="新建节点">
                                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                                    </asp:TreeNode>
                                </asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                    </asp:TreeView>
                </div>
            </div>
        </div>

        <script type="text/javascript" language="javascript">
    var popupControl = null;
               form1.Text1.style.filter = "progid:DXImageTransform.Microsoft.Fade(duration=0.5,overlap=0.75) progid:DXImageTransform.Microsoft.Shadow(direction=60,color=#333333,strength=5)";
divtest.style.filter = "progid:DXImageTransform.Microsoft.Shadow(direction=60,color=#333333,strength=5)";
    Sys.Application.add_init(function() {
        popupControl = $create($HGRootNS.PopupControl, {style:{margin:"0px", backgroundColor:"#cccccc"},positioningMode:$HGRootNS.PositioningMode.Center}, null, null, null);
        //popupControl.get_popupBody().innerHTML = "<div><select><option>1</option><option>3</option><option>2</option></select><span>fffffffff</span><div>";
         popupControl.createElementFromTemplate({
            nodeName : "span", properties:{innerText:"fffff"}
        }, popupControl.get_popupBody());
                var selectMonthList = popupControl.createElementFromTemplate({ nodeName : "select" , properties : {
//                tabIndex : 0
            } }, popupControl.get_popupBody());
       
       $HGDomElement.addSelectOption(selectMonthList, "1", 1, popupControl.get_popupDocument());
       $HGDomElement.addSelectOption(selectMonthList, "1", 1, popupControl.get_popupDocument());
       $HGDomElement.addSelectOption(selectMonthList, "1", 1, popupControl.get_popupDocument());
       
//        popupControl2 = $create($HGRootNS.PopupDialog, {width:100, height:100, style:{backgroundColor:"#cccccc" }, positioningMode:$HGRootNS.PositioningMode.Center}, null, null, null);
   });
    
    function onBtnClick(elt, e)
    {
        alert(elt.value);
        alert(e.target.tagName);
    }
    
    function showPopupWindow()
           {
            Sys.UI.DomElement.addCssClass(popupControl.get_popupBody(), "css1");
            var elt = $HGDomElement.createElementFromTemplate(
               {    
                nodeName:"span",
                properties : 
                {                    
                    innerText : "中文显示"
                 }
               },
               popupControl.get_popupBody(),
               null,
               popupControl.get_popupDocument()
            );
            
            var btn = popupControl.createElementFromTemplate(
               {    
                nodeName:"input",
                properties : 
                {
                    type : "button",      
                    value : "btn1"
                 },
                 events:{"click": onBtnClick}
               },
               popupControl.get_popupBody());         
                 
              var btn = $HGDomElement.createElementFromTemplate(
               {    
                nodeName:"input",
                properties : 
                {
                    type : "button",      
                    value : "btn1"
                 },
                 events:{"click": onBtnClick}
               },
               document.body);    
            Sys.UI.DomElement.addCssClass(elt, "css2");
            $addHandler(elt, "click", onSpanClick);
            popupControl.show();
           }
           
           function onSpanClick(e)
           {   alert(e.clientX);
               this.innerText = this.innerText + "aaa";
           }
    
        </script>

    </form>
</body>
</html>
