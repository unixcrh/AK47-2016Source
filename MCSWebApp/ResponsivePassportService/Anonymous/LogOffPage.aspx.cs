using MCS.Library.Core;
using MCS.Library.Globalization;
using MCS.Library.Passport;
using MCS.Web.Responsive.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ResponsivePassportService.Anonymous
{
    public partial class LogOffPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            LogOffInfo info = LogOffInfo.FromRequest();

            if (info.SessionID.IsNotEmpty())
            {
                if (info.LogOffAll)
                {
                    this.LogOffAllAPP(info);

                    if (info.ReturnUrl.IsNotEmpty())
                    {
                        string retuenUrl = info.ReturnUrl;

                        bool isFromApp = info.IsFromCascaseLogOffUrl == false && IsFromSelf() == false;

                        if (info.CascadeLogOffUrl.IsNotEmpty() && isFromApp)
                            retuenUrl = info.CascadeLogOffUrl;
                        else
                            retuenUrl = ModifyReturnUrlWhenWindowsintegrated(info.ReturnUrl,
                                info.LastUserID,
                                info.WindowsIntegrated);

                        returnHref.HRef = retuenUrl;

                        if (isFromApp)
                            autoRedirect.Value = (info.NeedAutoRedirect).ToString();
                        else
                            autoRedirect.Value = (info.AutoRedirect).ToString();
                    }
                }
                else
                {
                    if (info.ReturnUrl.IsNotEmpty())
                        Response.Redirect(info.ReturnUrl);
                }
            }

            Response.Expires = -1;
        }

        protected override void OnPreRender(EventArgs e)
        {
            returnHref.InnerText = Translate("注销完成，点击这里返回应用");
            base.OnPreRender(e);
        }

        /// <summary>
        /// 在集成Windows认证时，修改返回的地址为切换用户的地址
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="lastUserID"></param>
        /// <param name="windowsIntegrated"></param>
        /// <returns></returns>
        private static string ModifyReturnUrlWhenWindowsintegrated(string returnUrl, string lastUserID, bool windowsIntegrated)
        {
            string result = returnUrl;

            if (windowsIntegrated)
            {
                result = string.Format("../Integration/ChangeUser.aspx?lastUserID={0}&ru={1}",
                    HttpUtility.UrlEncode(lastUserID ?? string.Empty),
                    HttpUtility.UrlEncode(returnUrl));
            }

            return result;
        }

        private void LogOffAllAPP(LogOffInfo info)
        {
            List<AppLogOffCallBackUrl> urls = info.GetAllRelativeAppsLogOffCallBackUrl();

            this.RenderCallBackUrls(urls);

            PassportSignInSettings.GetConfig().PersistSignInInfo.DeleteRelativeSignInInfo(info.SessionID);
            PassportManager.ClearSignInCookie();
        }

        private void RenderCallBackUrls(List<AppLogOffCallBackUrl> urls)
        {
            foreach (AppLogOffCallBackUrl au in urls)
            {
                HtmlGenericControl tRow = new HtmlGenericControl("div");
                appsContainer.Controls.Add(tRow);

                tRow.Attributes["class"] = "form-group";

                HtmlGenericControl imgCell = new HtmlGenericControl("div");

                tRow.Controls.Add(imgCell);
                imgCell.Attributes["class"] = "col-lg-2 col-md-2 col-sm-2 col-xs-2";

                HtmlImage img = new HtmlImage();
                img.Align = "absmiddle";
                img.Src = au.LogOffCallBackUrl;
                img.Alt = img.Src;
                imgCell.Controls.Add(img);

                HtmlGenericControl txtCell = new HtmlGenericControl("div");

                tRow.Controls.Add(txtCell);

                txtCell.Attributes["class"] = "col-lg-10 col-md-10 col-sm-10 col-xs-10";

                txtCell.InnerText = string.Format(Translate("退出应用程序{0}"), au.AppID);
            }
        }

        private static string Translate(string sourceText)
        {
            CultureInfo culture = new CultureInfo(GlobalizationWebHelper.GetUserDefaultLanguage());

            return Translator.Translate(Define.DefaultCategory, sourceText, culture);
        }

        private static bool IsFromSelf()
        {
            bool result = false;

            Uri uriReferrer = HttpContext.Current.Request.UrlReferrer;
            Uri uriRequest = HttpContext.Current.Request.Url;

            if (uriReferrer != null)
            {
                result = string.Compare(uriRequest.Scheme, uriReferrer.Scheme, true) == 0;

                if (result)
                    result = uriRequest.Port == uriReferrer.Port;

                if (result)
                {
                    result = string.Compare(uriRequest.GetComponents(UriComponents.Path, UriFormat.Unescaped),
                        uriReferrer.GetComponents(UriComponents.Path, UriFormat.Unescaped), true) == 0;
                }
            }

            return result;
        }
    }
}