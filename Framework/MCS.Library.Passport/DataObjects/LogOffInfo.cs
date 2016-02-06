using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MCS.Library.Passport
{
    /// <summary>
    /// 注销的相关信息
    /// </summary>
    [Serializable]
    public class LogOffInfo
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LogOffInfo()
        {
            this.LogOffAll = true;
        }

        /// <summary>
        /// 登录Session ID
        /// </summary>
        public string SessionID
        {
            get;
            set;
        }

        /// <summary>
        /// 应用的ID
        /// </summary>
        public string ApplicationID
        {
            get;
            set;
        }

        /// <summary>
        /// 注销后返回的Url
        /// </summary>
        public string ReturnUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 得到级联注销的url
        /// </summary>
        public string CascadeLogOffUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 是否注销所有应用
        /// </summary>
        public bool LogOffAll
        {
            get;
            set;
        }

        /// <summary>
        /// 注销后，是否自动重定向到ReturnUrl
        /// </summary>
        public bool AutoRedirect
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CallbackUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是Windows集成认证
        /// </summary>
        public bool WindowsIntegrated
        {
            get;
            set;
        }

        /// <summary>
        /// 上一次认证的用户ID
        /// </summary>
        public string LastUserID
        {
            get;
            set;
        }

        /// <summary>
        /// 页面是否需要自动跳转。如果没有级联注销的url，则根据AutoRedirect判断，否则是true
        /// </summary>
        public bool NeedAutoRedirect
        {
            get
            {
                bool result = this.AutoRedirect;

                if (this.CascadeLogOffUrl.IsNotEmpty())
                    result = true;

                return result;
            }
        }

        /// <summary>
        /// 当前请求是否从级联注销的页面重定向而来
        /// </summary>
        public bool IsFromCascaseLogOffUrl
        {
            get
            {
                bool result = this.CascadeLogOffUrl.IsNotEmpty();

                if (EnvironmentHelper.IsUsingWebConfig)
                {
                    Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;

                    if (urlReferrer != null)
                    {
                        Uri uriCallback = new Uri(this.CascadeLogOffUrl, UriKind.RelativeOrAbsolute);

                        result = CompareCascadeUri(uriCallback, urlReferrer);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 得到每一个应用注销所使用的回调Url
        /// </summary>
        /// <returns></returns>
        public List<AppLogOffCallBackUrl> GetAllRelativeAppsLogOffCallBackUrl()
        {
            ExceptionHelper.CheckStringIsNullOrEmpty(this.SessionID, "SessionID");
            ExceptionHelper.CheckStringIsNullOrEmpty(this.ApplicationID, "ApplicationID");
            ExceptionHelper.CheckStringIsNullOrEmpty(this.CallbackUrl, "CallbackUrl");

            List<AppLogOffCallBackUrl> urls =
                PassportSignInSettings.GetConfig().PersistSignInInfo.GetAllRelativeAppsLogOffCallBackUrl(this.SessionID);

            if (AppLogOffCallBackUrlExist(urls, this.ApplicationID, this.CallbackUrl) == false)
            {
                AppLogOffCallBackUrl au = new AppLogOffCallBackUrl();

                au.AppID = this.ApplicationID;
                au.LogOffCallBackUrl = this.CallbackUrl;

                urls.Add(au);
            }

            return urls;
        }

        /// <summary>
        /// 从Url中构造LogOffInfo
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static LogOffInfo FromUrl(string url)
        {
            LogOffInfo result = new LogOffInfo();

            if (url.IsNotEmpty())
            {
                NameValueCollection urlParams = UriHelper.GetUriParamsCollection(url);

                result.SessionID = urlParams.GetValue("asid", true, string.Empty);
                result.ApplicationID = urlParams.GetValue("appID", true, string.Empty);
                result.ReturnUrl = urlParams.GetValue("ru", true, string.Empty);
                result.LogOffAll = urlParams.GetValue("loa", true, true);

                result.AutoRedirect = urlParams.GetValue("lar", true, false);
                result.CallbackUrl = urlParams.GetValue("lou", true, string.Empty);
                result.CascadeLogOffUrl = urlParams.GetValue("clu", true, string.Empty);

                result.WindowsIntegrated = urlParams.GetValue("wi", true, false);
                result.LastUserID = urlParams.GetValue("lu", true, string.Empty);
            }

            return result;
        }

        /// <summary>
        /// 从HttpContext中的Request中获取LogOffInfo。如果没有HttpContext，则返回null
        /// </summary>
        /// <returns></returns>
        public static LogOffInfo FromRequest()
        {
            LogOffInfo result = null;

            if (EnvironmentHelper.IsUsingWebConfig)
                result = FromUrl(HttpContext.Current.Request.Url.ToString());

            return result;
        }

        private static bool AppLogOffCallBackUrlExist(List<AppLogOffCallBackUrl> list, string appID, string callbackUrl)
        {
            bool result = false;

            for (int i = 0; i < list.Count; i++)
            {
                AppLogOffCallBackUrl au = list[i];

                if (string.Compare(au.AppID, appID, true) == 0 &&
                    string.Compare(au.LogOffCallBackUrl, callbackUrl, true) == 0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private static bool CompareCascadeUri(Uri uriCascade, Uri uriReferrer)
        {
            bool result = true;

            if (uriCascade.IsAbsoluteUri == false)
                uriCascade = uriCascade.MakeAbsolute(uriReferrer);

            result = string.Compare(uriCascade.Scheme, uriReferrer.Scheme, true) == 0;

            if (result)
                result = uriCascade.Port == uriReferrer.Port;

            if (result)
            {
                result = string.Compare(uriCascade.GetComponents(UriComponents.Path, UriFormat.Unescaped),
                    uriReferrer.GetComponents(UriComponents.Path, UriFormat.Unescaped), true) == 0;
            }

            return result;
        }
    }
}
