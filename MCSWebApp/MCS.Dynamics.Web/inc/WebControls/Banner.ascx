<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="MCS.Dynamics.Web.WebControls.Banner" %>
<%@ Register TagPrefix="mcsp" Namespace="MCS.Library.Web.Controls" Assembly="MCS.Library.Passport" %>
<h1 class="pc-frame-logo">
    <asp:HyperLink ID="bannerBtnHome" runat="server" NavigateUrl="~/default.aspx">中国石油</asp:HyperLink>
</h1>
<ul class="pc-frame-top-nav" id="frameTopNav">
    <li>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx?frameUrl=Entity">实体操作</asp:HyperLink></li>
    <li>
        <asp:HyperLink ID="bannerBtnLogs" runat="server" NavigateUrl="~/Pages/Logs/LogList.aspx">操作日志</asp:HyperLink></li>
    <li>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx?frameUrl=ETLEntity">ETL实体</asp:HyperLink></li>
    <li>
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Pages/PlanSchedule/ScheduleList.aspx">计划列表</asp:HyperLink></li>
    <li class="pc-dimension-menu" id="dimension_menu"><span style="display: inline-block;">
        <asp:HyperLink ID="HyperLink4" runat="server" CssClass="pc-menu-arrow" NavigateUrl="~/Pages/Job/ETLEntityJobList.aspx">任务列表<i></i></asp:HyperLink>
    </span>
        <div style="position: relative">
            <dl style="position: absolute">
                <dd>
                    <div class="pc-popup-nav" id="dimension_menu_coms">
                        <div style="max-height: 300px; overflow: hidden; position: relative;" class="pc-spin-container">
                            <ul id="dimension_menu_content">
                                <li>
                                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Pages/Job/ETLEntityJobList.aspx?auto=true">自动任务</asp:HyperLink></li>
                                <li>
                                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Pages/Job/ETLEntityJobList.aspx?auto=false">手动任务</asp:HyperLink></li>
                            </ul>
                        </div>
                        <div class="pc-spin-up">
                            <i></i>
                        </div>
                        <div class="pc-spin-down">
                            <i></i>
                        </div>
                    </div>
                </dd>
            </dl>
        </div>
    </li>
    <li>
        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Pages/Job/TaskMonitor.aspx">任务监控</asp:HyperLink></li>
    <li>
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Pages/ErrorLog/ErrorLogList.aspx">容错列表</asp:HyperLink></li>
    <li>
        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Pages/editNode/EditDetails.aspx">节点维护</asp:HyperLink></li>
</ul>
<ul class="pc-user-menu">
    <li class="pc-timetrap" id="timetrap">
        <div class="clear pc-timetrap-sub">
            <div>
                <asp:LinkButton ID="btnPresent" runat="server" CssClass="pc-cmd" OnClick="ShuttleNow">现在</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="btnPickTime" runat="server" CssClass="pc-cmd" OnClientClick="return $pc.popups.pickTime(this);"
                    OnClick="ShuttleAny">过去……</asp:LinkButton>
                <asp:HiddenField runat="server" ID="bannerCustomTime" />
            </div>
            <asp:Repeater runat="server" ID="recentList" OnItemCommand="HandleItemCommand">
                <HeaderTemplate>
                    <dl>
                        <dt>最近的时间点</dt>
                </HeaderTemplate>
                <ItemTemplate>
                    <dd>
                        <div class="pc-recent">
                            <asp:LinkButton ID="btnRecent" runat="server" CssClass="pc-recent-item pc-cmd" CommandName="TimeShuttle"
                                CommandArgument='<%#Eval("TimePoint") %>'><%#Eval("TimePoint","{0:yyyy-MM-dd HH:mm:ss}")%></asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteRecent" runat="server" CommandName="Delete" CssClass="pc-recent-delete"
                                CommandArgument='<%#Eval("TimePoint") %>'></asp:LinkButton>
                        </div>
                    </dd>
                </ItemTemplate>
                <FooterTemplate>
                    </dl>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear pc-timetrap-mt">
                <asp:LinkButton ID="btnClearRecent" runat="server" CssClass="pc-cmd" CommandName="ClearTimes"
                    OnClick="ClearRecent">清除所有</asp:LinkButton></div>
        </div>
        <a href="javascript:void(0)" class="hd pc-menu-switch pc-menu-arrow"><span id="timemark"
            runat="server">现在</span><i></i></a> </li>
    <li id="userprofile" class="pc-userprofile">
        <div class="clear pc-userprofile-sub">
            <ul class="pc-p">
                <li class="pc-lp" style="display: none;">
                    <div class="pc-photo-pan" style="margin-left: -15px">
                        <%--<soa:UserPresence runat="server" ID="userPresence" ShowUserIcon="true" StatusImage="LongBar">
						</soa:UserPresence>--%>
                        <%--<a href="#">
							<asp:Image runat="server" ID="imgLogonImage" Height="32" Width="32" AlternateText="修改头像"
								ToolTip="修改头像" />
						</a>--%>
                    </div>
                </li>
                <li class="pc-rp">
                    <div>
                        <div>
                            <asp:HyperLink runat="server" ID="lnkSysMan" CssClass="pc-cmd" NavigateUrl="~/dialogs/Maintain.aspx">管理功能</asp:HyperLink>
                        </div>
                        <div>
                            <asp:HyperLink ID="lnkProfile1" runat="server" CssClass="pc-cmd" NavigateUrl="~/dialogs/Profile.aspx">个人账户设置
                            </asp:HyperLink>
                        </div>
                        <div>
                            <asp:HyperLink runat="server" ID="lnkPassword" CssClass="pc-cmd" onclick="$pc.showDialog(this.href,'','',false,400,300,true);return false; ">修改口令</asp:HyperLink>
                        </div>
                        <div>
                            <mcsp:SignInLogoControl runat="server" ID="SignInLogo" CssClass="pc-signin-logo"
                                AutoRedirect="true" />
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <asp:HyperLink ID="lnkProfile" runat="server" CssClass="pc-menu-switch pc-menu-arrow"
            NavigateUrl="~/dialogs/Profile.aspx">
            <span id="userLogonName" runat="server">登录用户名</span><i></i>
        </asp:HyperLink>
    </li>
</ul>
<div>
    <script type="text/javascript">
        $pc.ui.hoverBehavior("dimension_menu");
        $pc.ui.hoverBehavior("timetrap");
        $pc.ui.hoverBehavior("userprofile");
        //		$pc.ui.autoSticky($pc.get('frameTopNav').parentNode);
        //		$pc.ui.configSpinner("dimension_menu_coms", "dimension_menu");
    </script>
</div>
