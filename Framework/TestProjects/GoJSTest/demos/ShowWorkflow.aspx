<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowWorkflow.aspx.cs" Inherits="GoJSTest.demos.ShowWorkflow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>显示真正的流程信息</title>
    <script src="../lib/go-debug.js"></script>
    <script src="wfParser.js"></script>
</head>
<body>
    <form id="serverForm" runat="server">
        <input type="hidden" runat="server" id="activitiesInfoJson" />
        <input type="hidden" runat="server" id="transitionsInfoJson" />
        <div id="myDiagramDiv"
            style="width: 500px; height: 500px; background-color: #DAE4E4;">
        </div>
        <script>
            var $ = go.GraphObject.make;
            var diagram = $(go.Diagram, "myDiagramDiv", {
                initialContentAlignment: go.Spot.Center, // center Diagram contents
                "undoManager.isEnabled": true, // enable Ctrl-Z to undo and Ctrl-Y to redo
                layout: $(go.LayeredDigraphLayout, { direction: 90 })
            });

            diagram.nodeTemplate =
              $(go.Node, "Auto",
                $(go.Shape, "RoundedRectangle",
                  new go.Binding("fill", "activityType", wf.nodeTypeParser),
                  new go.Binding("stroke", "elapsed", wf.nodeElapsedParser),
                  new go.Binding("location", "loc", go.Point.parse)
                ),
                $(go.Panel,
                    "Table",
                    { defaultAlignment: go.Spot.Left, margin: 4 },
                    $(go.TextBlock, "名称",
                      { row: 1, column: 0, margin: 4 }),
                    $(go.TextBlock,
                      { row: 1, column: 2, margin: 4 },
                      new go.Binding("text", "name")),
                    $("Button",
                    {
                        margin: 2
                    },
                    new go.Binding("visible", "hasBranchProcess"),
                    { row: 1, column: 3 },
                    $(go.TextBlock, "S", { font: "9pt consolas" })),
                    $(go.TextBlock, "操作人",
                      { row: 2, column: 0, margin: 4 }),
                    $(go.TextBlock,
                      { row: 2, column: 2, margin: 4 },
                      new go.Binding("text", "op"))
                  ),
                  {
                      toolTip:
                      $(go.Adornment, "Auto",
                          $(go.Shape, "RoundedRectangle", { fill: "#FFFFCC", stroke: "silver" }),
                          $(go.Panel, "Table",
                          new go.Binding("itemArray", "", wf.nodeToolTip),
                          {
                              defaultAlignment: go.Spot.Left,
                              margin: 4,
                              itemTemplate: $(go.Panel, "TableRow",
                                  $(go.TextBlock, new go.Binding("text", "label"),
                                    { column: 0, margin: 4, font: "9pt 微软雅黑,sans-serif" }),
                                  $(go.TextBlock, new go.Binding("text", "description"),
                                    { column: 1, margin: 4, font: "9pt 微软雅黑,sans-serif" })
                                )
                          }
                        )
                      )
                  }
            );

            diagram.linkTemplate =
            $(go.Link,
              {
                  routing: go.Link.Orthogonal,
                  corner: 5,
                  curve: go.Link.JumpOver
              },
              $(go.Shape,
                { strokeWidth: 1.5 },
                new go.Binding("stroke", "elapsed", wf.linkElapsedParser),
                new go.Binding("strokeDashArray", "isReturn", wf.returnLinkParser)),
              $(go.Shape, { toArrow: "Standard", stroke: null, }, new go.Binding("fill", "elapsed", wf.linkElapsedParser))
            );

            diagram.model = new go.GraphLinksModel(
                JSON.parse(document.getElementById("activitiesInfoJson").value),
                JSON.parse(document.getElementById("transitionsInfoJson").value)
            );

            diagram.model.isReadOnly = true;
        </script>
    </form>
</body>
</html>
