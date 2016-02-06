using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Others;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class ViewCreateTableSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.QueryString["Code"].IsNullOrEmpty())
                {
                    //etl实体编码
                    string etlCode = Request.QueryString["Code"].Trim();

                    ETLEntity etlEntity = DESchemaObjectAdapter.Instance.Load(etlCode) as ETLEntity;

                    if (etlEntity != null)
                        txt_CreateSql.Text = ETLTools.ETLEntityConvertToSql(etlEntity);
                }
            }
        }
    }
}