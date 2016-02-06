<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridView.aspx.cs" Inherits="MCS.PrototypeWeb.Pages.Server.GridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>GridView</title>
    <link href="../../Resources/Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../../Resources/Font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../../Resources/Styles/layout.css" rel="stylesheet" />
    <link href="../../Resources/Styles/table.css" rel="stylesheet" />
    <link href="../../Resources/Styles/dropdown.css" rel="stylesheet" />
    <link href="../../Resources/Styles/poptip.css" rel="stylesheet" />

    <script src="../../Resources/Jquery/jquery-2.0.3.js" type="text/javascript"></script>
    <script src="../../Resources/Bootstrap/js/bootstrap.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 100px; background-color: white; padding: 50px">
            <div class="table-header">
                <div class="dataTables-title">
                    <span>待办列表</span>
                </div>
            </div>
            <div class="table-responsive">
                <div class="dataTables_wrapper" id="sample-table-2_wrapper" role="grid">
                    <asp:GridView ID="gridTasks" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover dataTable"
                        OnSelectedIndexChanging="gridTasks_SelectedIndexChanging" OnPageIndexChanging="gridTasks_PageIndexChanging">
                        <HeaderStyle CssClass="headertr" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    标题
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="#"><%#Eval("Title") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    报送单位
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Department") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    发起人
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Creator") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    状态
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Status") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    创建时间
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("CreateTime") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    操作
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                        <button class="btn btn-xs btn-success">
                                            <i class="icon-ok bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-info">
                                            <i class="icon-edit bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-danger">
                                            <i class="icon-trash bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-warning">
                                            <i class="icon-flag bigger-120"></i>
                                        </button>
                                    </div>

                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                        <div class="dropdown">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only bigger-110"></i>
                                            </button>

                                            <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    <a href="#" class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                        <span class="blue">
                                                            <i class="icon-zoom-in bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                        <span class="green">
                                                            <i class="icon-edit bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                        <span class="red">
                                                            <i class="icon-trash bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>


                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                        <PagerTemplate>
                            <div class="row">
                                <div class="col-lg-2 col-md-1 hidden-sm hidden-xs">
                                </div>
                                <div class="col-lg-7 col-md-8 col-sm-8 col-xs-12">
                                    <div class="dataTables_paginate">
                                        <ul class="pagination">
                                            <li class="disabled">
                                                <a href="#">
                                                    <i class="icon-double-angle-left"></i>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <i class="icon-angle-left"></i>
                                                </a></li>
                                            <li class="active hidden-sm hidden-xs">
                                                <a href="#">1001
                                                </a>
                                            </li>
                                            <li class="hidden-sm hidden-xs">
                                                <a href="#">1002
                                                </a>
                                            </li>
                                            <li class="hidden-sm hidden-xs">
                                                <a href="#">1003
                                                </a>
                                            </li>
                                            <li class="hidden-sm hidden-xs">
                                                <a href="#">1004
                                                </a>
                                            </li>
                                            <li class="hidden-sm hidden-xs">
                                                <a href="#">1005
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <i class="icon-angle-right"></i>
                                                </a></li>
                                            <li>
                                                <a href="#"><i class="icon-double-angle-right"></i></a></li>
                                        </ul>
                                        <div class="input-group">
                                            <input class="form-control" type="text" />
                                            <span class="input-group-btn">
                                                <a class="btn btn-default">跳转</a>
                                                <input type="hidden" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 hidden-xs ">
                                    <div class="dataTables_info">
                                        记录数162/17页
                             <label>
                                 <select name="sample-table-2_length" aria-controls="sample-table-2" size="1">
                                     <option selected="selected" value="10">10</option>
                                     <option value="25">25</option>
                                     <option value="50">50</option>
                                     <option value="100">100</option>
                                 </select>
                             </label>
                                    </div>

                                </div>
                            </div>
                        </PagerTemplate>
                        <PagerStyle CssClass="pagertr" />
                    </asp:GridView>

                    <%--<table id="sample-table-1" class="table table-striped table-bordered table-hover dataTable">
                        <thead>
                            <tr>
                                <th class="center">
                                    <label>
                                        <input type="checkbox" />
                                        <span class="lbl"></span>
                                    </label>
                                </th>
                                <th>标题</th>
                                <th>报送单位</th>
                                <th class="hidden-480">发起人</th>

                                <th>
                                    <i class="bigger-110 hidden-480"></i>
                                    状态
                                </th>
                                <th class="hidden-480">发起时间</th>

                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="center">
                                    <label>
                                        <input type="checkbox" />
                                        <span class="lbl"></span>
                                    </label>
                                </td>
                                <td>
                                    <a href="#">关于柳州店变更印章保管员的申请</a>
                                </td>
                                <td>信息中心 系统实施部</td>
                                <td class="hidden-480">袁旭</td>
                                <td>送签</td>

                                <td class="hidden-480">
                                    2009-10-27 13:27
                                </td>
                                <td>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                        <button class="btn btn-xs btn-success">
                                            <i class="icon-ok bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-info">
                                            <i class="icon-edit bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-danger">
                                            <i class="icon-trash bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-warning">
                                            <i class="icon-flag bigger-120"></i>
                                        </button>
                                    </div>

                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                        <div class="dropdown">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only bigger-110"></i>
                                            </button>

                                            <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    <a href="#" class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                        <span class="blue">
                                                            <i class="icon-zoom-in bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                        <span class="green">
                                                            <i class="icon-edit bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                        <span class="red">
                                                            <i class="icon-trash bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>


                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    <label>
                                        <input type="checkbox" />
                                        <span class="lbl"></span>
                                    </label>
                                </td>

                                <td>
                                    <a href="#">信息中心一9年9月广告投放计划表</a>
                                </td>
                                <td>信息中心 系统实施部</td>
                                <td class="hidden-480">袁旭</td>
                                <td>送签</td>

                                <td class="hidden-480">
                                    2009-10-27 13:27
                                </td>

                                <td>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                        <button class="btn btn-xs btn-success">
                                            <i class="icon-ok bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-info">
                                            <i class="icon-edit bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-danger">
                                            <i class="icon-trash bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-warning">
                                            <i class="icon-flag bigger-120"></i>
                                        </button>
                                    </div>

                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                        <div class="dropdown">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only bigger-110"></i>
                                            </button>

                                            <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    <a href="#" class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                        <span class="blue">
                                                            <i class="icon-zoom-in bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                        <span class="green">
                                                            <i class="icon-edit bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                        <span class="red">
                                                            <i class="icon-trash bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    <label>
                                        <input type="checkbox" />
                                        <span class="lbl"></span>
                                    </label>
                                </td>

                                <td>
                                    <a href="#">信息中心分部二级市场违反合同付款申请表一</a>
                                </td>
                                <td>信息中心 系统实施部</td>
                                <td class="hidden-480">袁旭</td>
                                <td>送签</td>

                                <td class="hidden-480">
                                    2010-01-22 18:21
                                </td>

                                <td>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                        <button class="btn btn-xs btn-success">
                                            <i class="icon-ok bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-info">
                                            <i class="icon-edit bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-danger">
                                            <i class="icon-trash bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-warning">
                                            <i class="icon-flag bigger-120"></i>
                                        </button>
                                    </div>

                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                        <div class="dropdown">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only bigger-110"></i>
                                            </button>

                                            <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    <a href="#" class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                        <span class="blue">
                                                            <i class="icon-zoom-in bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                        <span class="green">
                                                            <i class="icon-edit bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                        <span class="red">
                                                            <i class="icon-trash bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    <label>
                                        <input type="checkbox" />
                                        <span class="lbl"></span>
                                    </label>
                                </td>

                                <td>
                                    <a href="#">信息中心分部二级市场违反合同付款申请表一</a>
                                </td>
                                <td>信息中心 系统实施部</td>
                                <td class="hidden-480">袁旭</td>
                                <td>送签</td>

                                <td class="hidden-480">
                                    2010-01-22 18:21
                                </td>

                                <td>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                        <button class="btn btn-xs btn-success">
                                            <i class="icon-ok bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-info">
                                            <i class="icon-edit bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-danger">
                                            <i class="icon-trash bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-warning">
                                            <i class="icon-flag bigger-120"></i>
                                        </button>
                                    </div>

                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                        <div class="dropdown">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only bigger-110"></i>
                                            </button>

                                            <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    <a href="#" class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                        <span class="blue">
                                                            <i class="icon-zoom-in bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                        <span class="green">
                                                            <i class="icon-edit bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                        <span class="red">
                                                            <i class="icon-trash bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    <label>
                                        <input type="checkbox" />
                                        <span class="lbl"></span>
                                    </label>
                                </td>

                                <td>
                                    <a href="#">信息中心分部二级市场违反合同付款申请表二测试[办结]</a>
                                </td>
                                <td>信息中心 系统实施部</td>
                                <td class="hidden-480">袁旭</td>
                                <td>送签</td>

                                <td class="hidden-480">
                                    2010-01-22 18:21
                                </td>

                                <td>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs btn-group">
                                        <button class="btn btn-xs btn-success">
                                            <i class="icon-ok bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-info">
                                            <i class="icon-edit bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-danger">
                                            <i class="icon-trash bigger-120"></i>
                                        </button>

                                        <button class="btn btn-xs btn-warning">
                                            <i class="icon-flag bigger-120"></i>
                                        </button>
                                    </div>

                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                        <div class="dropdown">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only bigger-110"></i>
                                            </button>

                                            <ul class="dropdown-menu dropdown-only-icon dropdown-yellow pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    <a href="#" class="tooltip-info" data-rel="tooltip" title="" data-original-title="View">
                                                        <span class="blue">
                                                            <i class="icon-zoom-in bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-success" data-rel="tooltip" title="" data-original-title="Edit">
                                                        <span class="green">
                                                            <i class="icon-edit bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a href="#" class="tooltip-error" data-rel="tooltip" title="" data-original-title="Delete">
                                                        <span class="red">
                                                            <i class="icon-trash bigger-120"></i>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>--%>
                </div>
            </div>
            <!-- /.table-responsive -->
        </div>
    </form>
</body>
</html>
