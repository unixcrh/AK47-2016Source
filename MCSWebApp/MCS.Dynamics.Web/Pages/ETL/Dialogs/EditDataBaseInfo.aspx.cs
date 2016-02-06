using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library;
using PetroChina.UEP.Web.DataObjects;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class EditDataBaseInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    DBInfo condition = DBInfoAdapter.Instance.GetByID(code);
                    //数据库登录账号
                    txtLoginID.Text = condition.DBLoginID;
                    //数据库登录地址
                    this.txtDBAddr.Value = condition.DBAddr;
                    //数据库名称
                    this.txtDBName.Text = condition.DBName;
                    //数据库登录密码
                    this.txtPassword.Text = condition.DBPassword;
                    //北京服务器地址
                    this.txtRDBAddr.Text = condition.RDBAddress;

                }
            }
        }

        //保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {

            var dbBaseInfo = new DBInfo();
            string code = Request.QueryString["code"];
            string dbUid = txtLoginID.Text.Trim();
            string dbAddr = txtDBAddr.Value.Trim();
            string dbName = txtDBName.Text.Trim();
            //插入和修改判断
            if (string.IsNullOrEmpty(code))
            {
                code = Guid.NewGuid().ToString();
                if (DBInfoAdapter.Instance.DBInfoIsExit(dbUid, dbAddr, dbName).Count > 0)
                {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "reloadPage", "alert('该数据库信息已存在!'); ", true);
                    return;
                }
            }
            else
            {
                dbBaseInfo = DBInfoAdapter.Instance.GetByID(code);
            }

            //数据库登录信息的主键
            dbBaseInfo.DBCode = code;
            //数据库登录账号
            dbBaseInfo.DBLoginID = txtLoginID.Text;
            //数据库登录地址
            dbBaseInfo.DBAddr = this.txtDBAddr.Value;
            //数据库名称
            dbBaseInfo.DBName = this.txtDBName.Text;
            //数据库登录密码
            dbBaseInfo.DBPassword = this.txtPassword.Text;
            //北京服务器地址
            dbBaseInfo.RDBAddress = this.txtRDBAddr.Text;

            //添加或更新
            DBInfoAdapter.Instance.Update(dbBaseInfo);

            WebUtility.RegisterOnLoadScriptBlock(this, "top.close();");
        }
    }
}