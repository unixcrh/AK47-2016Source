using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI;
using MCS.Web.Library;

namespace MCS.Web.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlStylePageModule : BasePageModule
    {
        private class PageDocumentType
        {
            public bool IsXmlDocument
            {
                get;
                set;
            }

            public bool HasDocType
            {
                get;
                set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        protected override void Init(System.Web.UI.Page page)
        {
            page.PreRenderComplete += new EventHandler(PagePreRender);
        }

        private void PagePreRender(object sender, EventArgs e)
        {
            System.Web.UI.Page page = sender as System.Web.UI.Page;
            SetControlsStyle(page);
            SetHeaderContent(page);
        }

        private void SetControlsStyle(System.Web.UI.Control control)
        {
            if (control.Controls.Count <= 0)
                return;
        }

        private void SetWebControlCss(WebControl ctrl, string cssName)
        {
            if (string.IsNullOrEmpty(ctrl.CssClass))
                ctrl.CssClass = cssName;
        }

        private void SetCssAttribute(IAttributeAccessor aa, string cssName)
        {
            if (string.IsNullOrEmpty(aa.GetAttribute("class")))
                aa.SetAttribute("class", cssName);
        }

        private void SetHeaderContent(Page page)
        {
            if (page.Header != null)
            {
                PageDocumentType docType = GetPageContentType(page);

                if (docType.HasDocType)
                    SetCompatibleMeta(page, "IE=7");
                else
                    SetCompatibleMeta(page, "IE=5");

                ////不是对话框
                //if (IsXHtmlDocument(page))
                //    SetCompatibleMeta(page, "IE=7");
                //else
                //    SetCompatibleMeta(page, "IE=5");
            }
        }

        private static void SetCompatibleMeta(Page page, string metaContent)
        {
            if (IsCompatibleSet(page) == false)
            {
                HtmlMeta metaA = new HtmlMeta();
                metaA.HttpEquiv = "X-UA-Compatible";
                metaA.Content = metaContent;
                page.Header.Controls.AddAt(0, metaA);
            }
        }

        /// <summary>
        /// 是否在Header中已经设置了X-UA-Compatible
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private static bool IsCompatibleSet(Page page)
        {
            Control compatibleMeta = page.Header.FindControl(ctrl =>
            {
                bool set = false;

                if (ctrl is HtmlMeta)
                {
                    HtmlMeta meta = (HtmlMeta)ctrl;

                    if (string.Compare(meta.HttpEquiv, "X-UA-Compatible", true) == 0)
                        set = true;
                }

                return set;
            }, true);

            return compatibleMeta != null;
        }

        //private static bool IsXHtmlDocument(Page page)
        //{
        //    bool result = false;

        //    foreach (Control control in page.Controls)
        //    {
        //        if (control is HtmlHead)
        //            break;

        //        if (control is LiteralControl)
        //        {
        //            string text = ((LiteralControl)control).Text;

        //            if (text.IndexOf("!DOCTYPE", StringComparison.OrdinalIgnoreCase) >= 0)
        //                if (text.IndexOf("XHTML", StringComparison.OrdinalIgnoreCase) >= 0)
        //                {
        //                    result = true;
        //                    break;
        //                }
        //        }
        //    }

        //    return result;
        //}

        private static PageDocumentType GetPageContentType(Page page)
        {
            PageDocumentType result = new PageDocumentType();

            foreach (Control control in page.Controls)
            {
                if (control is HtmlHead)
                    break;

                if (control is LiteralControl)
                {
                    string text = ((LiteralControl)control).Text;

                    if (text.IndexOf("!DOCTYPE", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        result.HasDocType = true;

                        if (text.IndexOf("XHTML", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            result.IsXmlDocument = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
