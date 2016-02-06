using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using System.Web.Script.Serialization;

namespace MCS.Dynamics.Web.Pages.ETL.Dialogs
{
    public partial class WhereCondition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string etlID = Convert.ToString(Request["id"]);

            if (string.IsNullOrEmpty(etlID))
            {
                return;
            }

            ETLEntity entity = DESchemaObjectAdapter.Instance.Load(etlID) as ETLEntity;

            OutETLEntityCollection oEntities = entity.OutEtlEntitys;
            repeter.DataSource = oEntities;
            repeter.DataBind();

        }
        
        /// </summary>  
        /// <param name="json">JSON</param>  
        /// <returns>Dictionary`[string, object]</returns>  
        public static Dictionary<string, object> SelectDictionary(string json)  
        {  
            JavaScriptSerializer serializer = new JavaScriptSerializer();  
            return serializer.Deserialize<Dictionary<string, object>>(json);  
        }  

    }
}