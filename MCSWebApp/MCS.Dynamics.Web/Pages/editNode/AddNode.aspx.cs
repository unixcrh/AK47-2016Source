using System;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Web.Library;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System.Web;
using MCS.Library.Data.Builder;

namespace MCS.Dynamics.Web.Pages.editNode
{
    public partial class AddNode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = this.Request.QueryString["CODE"];
                if (!string.IsNullOrEmpty(code))
                {
                    DECategory root = CategoryAdapter.Instance.GetByID(code);
                    fjid.Value = root.ParentCode;
                    codeName.Value = root.DisplayName;
                    description.Value = root.Desc;
                    dataTime.Value = root.VersionStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
            }

        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void okButton_Click(object sender, EventArgs e)
        {
            //显示名称
            string codeName = Request.Form["codeName"].Trim();
            //父级ID
            string fjid = Request.Form["fjid"].Trim();

            if (string.IsNullOrEmpty(codeName) || string.IsNullOrEmpty(fjid))
            {
                return;
            }

            DECategory dn = CategoryAdapter.Instance.getCategoryByDisplayName(codeName, fjid);

            if (dn != null)
            {
                Response.Write("<script language=javascript>window.alert('显示名称已存在,请修改显示名称');</script>");
            }
            else
            {
                //登陆者
                string du = HttpContext.Current.User.Identity.Name;
                //自动生成ID 
                string guid = Guid.NewGuid().ToString();

                //描述
                string dec = Request.Form["description"];
                //ID 
                string id = this.Request.QueryString["CODE"];

                DECategory root = CategoryAdapter.Instance.GetByID(fjid);

                string level = (Int32.Parse(root.Level) + 1).ToString();

                string fullpath = root.FullPath + "/" + codeName;
                DateTime dt;
                if (string.IsNullOrEmpty(id))
                {
                    //登陆
                    dt = CategoryAdapter.Instance.AddData(guid, fjid, codeName, dec, level, fullpath, du);
                }
                else
                {
                    //更新条件
                    WhereSqlClauseBuilder sqlWhere = new WhereSqlClauseBuilder();
                    sqlWhere.AppendItem("Code", id);
                    sqlWhere.AppendItem("VersionStartTime", dataTime.Value);
                    //更新
                    dt = CategoryAdapter.Instance.updataNode(id, fjid, codeName, dec, sqlWhere, du, level, fullpath);
                }

                if (!dt.ToString().Equals(""))
                {

                    Response.Write("<script language=javascript>window.alert('操作成功');parent.location.reload();</script>");
                    cleanValue();
                }
            }
        }

        /// <summary>
        /// 清空画面控件的值
        /// </summary>
        private void cleanValue()
        {
            fjid.Value = "";
            codeName.Value = "";
            description.Value = "";
            dataTime.Value = "";
        }
    }
}