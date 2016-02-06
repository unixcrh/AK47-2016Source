using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Web.Library.Script;
using MCS.Library.SOA.DataObjects.Dynamics.Converters;

namespace MCS.Dynamics.Web.Services
{
    /// <summary>
    /// GetEntityDefine 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GetEntityDefine : System.Web.Services.WebService
    {

        //[WebMethod]
        //public ServerResult GetDefineByCodeName(string codeName)
        //{
        //    ServerResult result = new ServerResult();
        //    try
        //    {
        //        var entnity = DESchemaObjectAdapter.Instance.LoadByCodeName(codeName);

        //        string json = JSONSerializerExecute.Serialize(entnity);

        //        result.Result = json;
        //    }
        //    catch (Exception e)
        //    {

        //        result.IsSuccess = false;
        //        result.Result = e.Message;
        //    }



        //    return result;
        //}

       
    }



    /// <summary>
    /// Server返回值
    /// </summary>
    [Serializable]
    public class ServerResult
    {
        private bool _IsSuccess = true;

        public bool IsSuccess
        {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }

        public string Result { get; set; }

        public object ObjResult { get; set; }
    }

    public class DbConnectionStr
    {
        private bool _IsSuccess = true;

        public bool IsSuccess
        {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }

        public string Result { get; set; }
        public string Schame { get; set; }
        public string TableName { get; set; }
    }
}
