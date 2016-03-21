<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PPTS.Portal.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-cn">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>学大教育PPTS</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link href="favicon.ico" rel="shortcut icon" />
    <script src="app/common/config/mcs.config.js"></script>
    <script src="app/common/util/mcs.global.js"></script>
    <%--<script src="app/common/util/mcs.global.min.js"></script>--%>
    <script>
        //mcs.loadCssAssets({
        //    cssFiles: [
        //        //<!--#bootstrap基础样式-->
        //        'libs/bootstrap-3.3.6/dist/css/bootstrap.min',
        //        //<!--#font awesome字体样式-->
        //        'libs/font-awesome-4.5.0/css/font-awesome.min',
        //        //<!--#ace admin基础样式-->
        //        'libs/ace-1.2.3/ace.min',
        //        //<!--#blockUI样式-->
        //        'libs/angular-block-ui-0.2.2/dist/angular-block-ui'
        //    ],
        //    localCssFiles: [
        //        //<!--#网站主样式-->
        //        'assets/css/site'
        //    ]
        //});
        mcs.loadCssAssets([
            //<!--#bootstrap基础样式-->
            'libs/bootstrap-3.3.5/css/bootstrap',
            //<!--#boostrap组件样式-->
            'libs/bootstrap-3.3.5/css/style',
            'libs/bootstrap-3.3.5/css/style-responsive',
            //<!--#全局组件样式-->
            'libs/mcs-component-1.0.1/mcs.component',
            //<!--#font awesome字体样式-->
            'libs/font-awesome-4.5.0/css/font-awesome.min',
            //<!--#ace admin基础样式-->
            'libs/ace-1.2.3/ace.min',
            //<!--#blockUI样式-->
            'libs/angular-block-ui-0.2.2/dist/angular-block-ui',
        ], [
            //<!--#网站主样式-->
            'assets/css/site'
        ]);
        mcs.loadRequireJsAssets(
            'libs/requirejs-2.1.22/require',
            './app/common/config/require.config');
    </script>
</head>
<body class="no-skin" ng-controller="AppController">
    <!-- #开始：网站头部区域，包括logo，消息提醒，用户名，以及用户相关的关联菜单项（注销等） -->
    <div id="navbar" class="navbar navbar-default">
        <script type="text/javascript">
            try {
                ace.settings.check('navbar', 'fixed')
            } catch (e) { }
        </script>
        <div class="navbar-container" id="navbar-container">
            <!-- #开始：响应式布局下的菜单栏区域 -->
            <button type="button" class="navbar-toggle menu-toggler pull-left" id="menu-toggler">
                <span class="sr-only">Toggle sidebar</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <!-- /结束：响应式布局下的菜单栏区域 -->
            <div class="navbar-header pull-left">
                <!-- #开始：头部.logo区域 -->
                <a id="logo" href="#" class="navbar-brand">
                    <img src="assets/images/logo.gif" />
                </a>
                <!-- /结束：头部.logo区域 -->
                <!-- #section:basics/navbar.toggle -->
                <!-- /section:basics/navbar.toggle -->
            </div>
            <!-- #开始：头部.下拉菜单区域 -->
            <div class="navbar-buttons navbar-header pull-right" role="navigation">
                <ul class="nav ace-nav">
                    <li class="grey">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <i class="ace-icon fa fa-tasks"></i>
                            <span class="badge badge-grey">4</span>
                        </a>
                        <ul class="dropdown-menu-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                            <li class="dropdown-header">
                                <i class="ace-icon fa fa-check"></i>4 Tasks to complete
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">Software Update</span>
                                        <span class="pull-right">65%</span>
                                    </div>
                                    <div class="progress progress-mini">
                                        <div style="width: 65%" class="progress-bar"></div>
                                    </div>
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">Hardware Upgrade</span>
                                        <span class="pull-right">35%</span>
                                    </div>
                                    <div class="progress progress-mini">
                                        <div style="width: 35%" class="progress-bar progress-bar-danger"></div>
                                    </div>
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">Unit Testing</span>
                                        <span class="pull-right">15%</span>
                                    </div>
                                    <div class="progress progress-mini">
                                        <div style="width: 15%" class="progress-bar progress-bar-warning"></div>
                                    </div>
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">Bug Fixes</span>
                                        <span class="pull-right">90%</span>
                                    </div>
                                    <div class="progress progress-mini progress-striped active">
                                        <div style="width: 90%" class="progress-bar progress-bar-success"></div>
                                    </div>
                                </a>
                            </li>
                            <li class="dropdown-footer">
                                <a href="#">See tasks with details
                                <i class="ace-icon fa fa-arrow-right"></i>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="purple">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <i class="ace-icon fa fa-bell icon-animated-bell"></i>
                            <span class="badge badge-important">8</span>
                        </a>
                        <ul class="dropdown-menu-right dropdown-navbar navbar-pink dropdown-menu dropdown-caret dropdown-close">
                            <li class="dropdown-header">
                                <i class="ace-icon fa fa-exclamation-triangle"></i>8 Notifications
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">
                                            <i class="btn btn-xs no-hover btn-pink fa fa-comment"></i>
                                            New Comments
                                        </span>
                                        <span class="pull-right badge badge-info">+12</span>
                                    </div>
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <i class="btn btn-xs btn-primary fa fa-user"></i>Bob just signed up as an editor ...
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">
                                            <i class="btn btn-xs no-hover btn-success fa fa-shopping-cart"></i>
                                            New Orders
                                        </span>
                                        <span class="pull-right badge badge-success">+8</span>
                                    </div>
                                </a>
                            </li>
                            <li>
                                <a href="#">
                                    <div class="clearfix">
                                        <span class="pull-left">
                                            <i class="btn btn-xs no-hover btn-info fa fa-twitter"></i>
                                            Followers
                                        </span>
                                        <span class="pull-right badge badge-info">+11</span>
                                    </div>
                                </a>
                            </li>
                            <li class="dropdown-footer">
                                <a href="#">See all notifications
                                <i class="ace-icon fa fa-arrow-right"></i>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="green">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <i class="ace-icon fa fa-envelope icon-animated-vertical"></i>
                            <span class="badge badge-success">5</span>
                        </a>
                        <ul class="dropdown-menu-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                            <li class="dropdown-header">
                                <i class="ace-icon fa fa-envelope-o"></i>5 Messages
                            </li>
                            <li class="dropdown-content">
                                <ul class="dropdown-menu dropdown-navbar">
                                    <li>
                                        <a href="#">
                                            <img src="./assets/images/avatar.png" class="msg-photo" alt="Alex's Avatar" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">Alex:</span> Ciao sociis natoque penatibus et auctor ...
                                                </span>
                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>a moment ago</span>
                                                </span>
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </li>
                            <li class="dropdown-footer">
                                <a href="inbox.html">See all messages
                                <i class="ace-icon fa fa-arrow-right"></i>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <!-- #开始：头部.下拉菜单区域.用户菜单 -->
                    <li class="light-blue">
                        <a data-toggle="dropdown" href="javascript:void(0)" class="dropdown-toggle">
                            <img class="nav-user-photo" src="assets/images/user.png" alt="用户头像" />
                            <span class="user-info">
                                <small>您好,咨询师1</small>
                                <small>校教育咨询师-方庄</small>
                            </span>
                            <i class="ace-icon fa fa-caret-down"></i>
                        </a>
                        <ul class="user-menu dropdown-menu-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
                            <li class="dropdown-hover">
                                <a class="clearfix" tabindex="-1" href="#">
                                    <i class="ace-icon fa fa-user"></i>切换角色
                                <i class="ace-icon fa fa-caret-right pull-right"></i>
                                </a>
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <a href="#" tabindex="-1">校教育咨询师-方庄</a>
                                    </li>
                                    <li>
                                        <a href="#" tabindex="-1">校学管主任-方庄</a>
                                    </li>
                                    <li>
                                        <a href="#" tabindex="-1">校教育咨询师-校区2</a>
                                    </li>
                                    <li>
                                        <a href="#" tabindex="-1">系统管理员-总部</a>
                                    </li>
                                </ul>
                            </li>
                            <li class="divider"></li>
                            <li class="dropdown-hover">
                                <a class="clearfix" tabindex="-1" href="#">
                                    <i class="ace-icon fa fa-cogs"></i>页面设置
                                <i class="ace-icon fa fa-caret-right pull-right"></i>
                                </a>
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li>
                                        <a href="javascript:void(0);" ng-click="changeSettingNavbar()" tabindex="-1">固定头部导航条</a>
                                    </li>
                                    <li>
                                        <a href="javascript:void(0);" ng-click="changeSettingBreadcrumbs()" tabindex="-1">固定路径导航条</a>
                                    </li>
                                    <li>
                                        <a href="javascript:void(0);" ng-click="changeSettingMainContainer()" tabindex="-1">固定页面宽度</a>
                                    </li>
                                    <li>
                                        <a href="javascript:void(0);" ng-click="changeSettingSidebar()" tabindex="-1">固定左侧菜单</a>
                                    </li>
                                    <li>
                                        <a href="javascript:void(0);" ng-click="changeSettingCompact()" tabindex="-1">左侧复合菜单</a>
                                    </li>
                                    <!--<li class="dropdown-hover">
                                    <a href="javascript:void(0);" tabindex="-1">选择页面主题<i class="ace-icon fa fa-caret-right pull-right"></i></a>
                                    <ul class="dropdown-menu dropdown-menu-right">
                                        <li>
                                            <a href="#" tabindex="-1">主题1</a>
                                        </li>
                                        <li>
                                            <a href="#" tabindex="-1">主题2</a>
                                        </li>
                                        <li>
                                            <a href="#" tabindex="-1">主题3</a>
                                        </li>
                                    </ul>
                                </li>-->
                                </ul>
                            </li>
                            <li class="divider"></li>
                            <li>
                                <a href="#">
                                    <i class="ace-icon fa fa-power-off"></i>注销
                                </a>
                            </li>
                        </ul>
                    </li>
                    <!-- /结束：头部.下拉菜单区域.用户菜单 -->
                </ul>
            </div>
            <!-- /结束：头部.下拉菜单区域 -->
        </div>
    </div>
    <!-- /结束：网站头部区域，包括logo，消息提醒，用户名，以及用户相关的关联菜单项（注销等） -->
    <!-- #开始：网站主体内容区域 -->
    <div id="main-container" class="main-container">
        <script type="text/javascript">
            try {
                ace.settings.check('main-container', 'fixed')
            } catch (e) { }
        </script>
        <!-- #开始：网站主体内容区域.左侧区域 -->
        <div id="sidebar" class="sidebar responsive">
            <script type="text/javascript">
                try {
                    ace.settings.check('sidebar', 'fixed')
                } catch (e) { }
            </script>
            <!-- #开始：网站主体内容区域.左侧区域.顶部快捷按钮 -->
            <div class="sidebar-shortcuts" id="sidebar-shortcuts">
                <div class="sidebar-shortcuts-large" id="sidebar-shortcuts-large">
                    <button class="btn btn-success">
                        <i class="ace-icon fa fa-signal"></i>
                    </button>
                    <button class="btn btn-info">
                        <i class="ace-icon fa fa-pencil"></i>
                    </button>
                    <button class="btn btn-warning">
                        <i class="ace-icon fa fa-users"></i>
                    </button>
                    <button class="btn btn-danger">
                        <i class="ace-icon fa fa-cogs"></i>
                    </button>
                </div>
                <div class="sidebar-shortcuts-mini" id="sidebar-shortcuts-mini">
                    <span class="btn btn-success"></span>
                    <span class="btn btn-info"></span>
                    <span class="btn btn-warning"></span>
                    <span class="btn btn-danger"></span>
                </div>
            </div>
            <!-- /结束：网站主体内容区域.左侧区域.顶部快捷按钮 -->
            <!-- #开始：网站主体内容区域.左侧区域.主菜单 -->
            <%--<ul class="nav nav-list" ng-repeat="menu in menus">
                <li ui-sref-active="active" ng-class="{'active':menu.isActived}">
                    <a ui-sref="{{menu.href}}" ng-class="{'dropdown-toggle':menu.subMenus.length}">
                         <i class="menu-icon fa fa-{{menu.icon}}"></i>
                         <span class="menu-text" ng-bind="menu.text"></span>
                         <b class="arrow fa fa-angle-down" ng-show="menu.subMenus.length"></b>
                    </a>
                    <ul class="submenu" ng-repeat="sub in menu.subMenus">
                        <li>
                            <a ui-sref="{{sub.href}}">
                                 <i class="menu-icon fa fa-caret-right"></i>
                                 <span ng-bind="sub.text"></span>
                            </a>
                        </li>
                    </ul>
                </li>
            </ul>--%>
            <ul class="nav nav-list">
                <!--首页-->
                <li ui-sref-active="active">
                    <a ui-sref="dashboard">
                        <i class="menu-icon fa fa-home"></i>
                        <span class="menu-text">首页 </span>
                    </a>
                </li>
                <!--审批管理-->
                <li ui-sref-active="active">
                    <a ui-sref="#">
                        <i class="menu-icon fa fa-check-square-o"></i>
                        <span class="menu-text">审批管理 </span>
                    </a>
                </li>
                <!--客户管理-->
                <li ui-sref-active="active">
                    <a href="#" class="dropdown-toggle">
                        <i class="menu-icon fa fa-user"></i>
                        <span class="menu-text">客户管理 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li ng-class="{'active': $state.includes('customer')}">
                            <a ui-sref="customer">
                                <i class="menu-icon fa fa-caret-right"></i>潜客管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>市场资源
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>跟进管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>上门管理
                            </a>
                        </li>
                        <li ng-class="{'active': $state.includes('student')}">
                            <a ui-sref="student">
                                <i class="menu-icon fa fa-caret-right"></i>学员管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>回访记录
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>成绩管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>客户反馈
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>周反馈
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>学情会
                            </a>
                        </li>
                    </ul>
                </li>
                <!--缴费管理-->
                <li ui-sref-active="active">
                    <a href="javascript:void(0)" class="dropdown-toggle">
                        <i class="menu-icon fa fa-credit-card"></i>
                        <span class="menu-text">缴费管理 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>缴费单管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>付费管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>退费管理
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>资产兑换
                            </a>
                        </li>
                        <li class="">
                            <a href="jqgrid.html">
                                <i class="menu-icon fa fa-caret-right"></i>银联对接
                            </a>
                        </li>
                    </ul>
                </li>
                <!--订购管理-->
                <li ui-sref-active="active">
                    <a href="widgets.html" class="dropdown-toggle">
                        <i class="menu-icon fa fa-shopping-bag"></i>
                        <span class="menu-text">订购管理 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>订购列表
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>退订列表
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>剩余课时
                            </a>
                        </li>
                    </ul>
                </li>
                <!--排课管理-->
                <li ui-sref-active="active">
                    <a href="widgets.html" class="dropdown-toggle">
                        <i class="menu-icon fa fa-calendar"></i>
                        <span class="menu-text">排课管理 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>学员课表
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>教师课表
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>学员排课
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>教师排课
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>排课条件
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>班组课表
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>确认课时
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>班级管理
                            </a>
                        </li>
                    </ul>
                </li>
                <!--产品管理-->
                <li ui-sref-active="active">
                    <a href="gallery.html" class="dropdown-toggle">
                        <i class="menu-icon fa fa-suitcase"></i>
                        <span class="menu-text">产品管理 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>产品列表
                            </a>
                        </li>
                        <li class="">
                            <a href="form-elements.html">
                                <i class="menu-icon fa fa-caret-right"></i>产品类别
                            </a>
                        </li>
                    </ul>
                </li>
                <!--基础数据-->
                <li ui-sref-active="active">
                    <a href="#" class="dropdown-toggle">
                        <i class="menu-icon fa fa-gear"></i>
                        <span class="menu-text">基础数据 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="profile.html">
                                <i class="menu-icon fa fa-caret-right"></i>字典管理
                            </a>
                        </li>
                        <li class="">
                            <a href="inbox.html">
                                <i class="menu-icon fa fa-caret-right"></i>拓路折扣
                            </a>
                        </li>
                        <li class="">
                            <a href="pricing.html">
                                <i class="menu-icon fa fa-caret-right"></i>拓路折扣B
                            </a>
                        </li>
                        <li class="">
                            <a href="pricing.html">
                                <i class="menu-icon fa fa-caret-right"></i>服务费管理
                            </a>
                        </li>
                        <li class="">
                            <a href="pricing.html">
                                <i class="menu-icon fa fa-caret-right"></i>买赠表管理
                            </a>
                        </li>
                        <li class="">
                            <a href="pricing.html">
                                <i class="menu-icon fa fa-caret-right"></i>规则管理
                            </a>
                        </li>
                    </ul>
                </li>
                <!--客服中心-->
                <li ui-sref-active="active">
                    <a href="#" class="dropdown-toggle">
                        <i class="menu-icon fa fa-microphone"></i>
                        <span class="menu-text">客服中心 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="profile.html">
                                <i class="menu-icon fa fa-caret-right"></i>客户服务
                            </a>
                        </li>
                    </ul>
                </li>
                <!--合同管理-->
                <li ui-sref-active="active">
                    <a href="#" class="dropdown-toggle">
                        <i class="menu-icon fa fa-file-text"></i>
                        <span class="menu-text">合同管理 </span>
                        <b class="arrow fa fa-angle-down"></b>
                    </a>
                    <ul class="submenu">
                        <li class="">
                            <a href="profile.html">
                                <i class="menu-icon fa fa-caret-right"></i>合同列表
                            </a>
                        </li>
                        <li class="">
                            <a href="profile.html">
                                <i class="menu-icon fa fa-caret-right"></i>收款列表
                            </a>
                        </li>
                        <li class="">
                            <a href="profile.html">
                                <i class="menu-icon fa fa-caret-right"></i>退款列表
                            </a>
                        </li>
                    </ul>
                </li>
            </ul>
            <!-- /结束：网站主体内容区域.左侧区域.主菜单 -->
            <!-- #开始：网站主体内容区域.左侧区域.最小化左侧区域按钮 -->
            <div class="sidebar-toggle sidebar-collapse" id="sidebar-collapse">
                <i class="ace-icon fa fa-angle-double-left" data-icon1="ace-icon fa fa-angle-double-left" data-icon2="ace-icon fa fa-angle-double-right"></i>
            </div>
            <!-- /结束：网站主体内容区域.左侧区域.最小化左侧区域按钮 -->
            <script type="text/javascript">
                try {
                    ace.settings.check('sidebar', 'collapsed')
                } catch (e) { }
            </script>
        </div>
        <!-- /结束：网站主体内容区域.左侧区域 -->
        <!-- #开始：网站主体内容区域.中间区域 -->
        <div class="main-content" ui-view>
        </div>
        <!-- /结束：网站主体内容区域.底部区域 -->
        <!-- #开始：网站主体内容区域.底部区域 -->
        <div id="footer" class="footer">
            <div class="footer-inner">
                <div class="footer-content">
                    <span>技术支持邮箱：<a href="mailto:support@21edu.com">support@21edu.com</a>&nbsp;&nbsp;技术支持电话：010-64465149-666&nbsp;&nbsp;<a href="javascript:void(0);">意见反馈</a>
                        <br />
                        &copy;2010学大教育 (京ICP备05011326号)
                    </span>
                </div>
            </div>
        </div>
        <!-- /结束：网站主体内容区域.底部区域 -->
        <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
            <i class="ace-icon fa fa-angle-double-up icon-only bigger-110"></i>
        </a>
    </div>
    <!-- /结束：网站主体内容区域 -->
</body>
</html>