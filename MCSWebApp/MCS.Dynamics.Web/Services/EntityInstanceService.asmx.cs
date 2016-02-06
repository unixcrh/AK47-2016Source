using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MCS.Dynamics.Web.Saplocalhost;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.ETL.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Web.Library.Script;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System.Web.Script.Services;
using MCS.Library.Validation;
using InType = MCS.Library.SOA.DataObjects.Dynamics.Contract.InType;
//using SAPLoginParams = MCS.Library.SOA.DataObjects.Dynamics.Configuration.SAPLoginParams;
using SapValue = MCS.Library.SOA.DataObjects.Dynamics.Contract.SapValue;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UEP.DataObjects.UserPool.Adapters;
using UEP.DataObjects.UserPool.DataObjects;

namespace MCS.Dynamics.Web.Services
{
    /// <summary>
    /// EntityInstanceService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class EntityInstanceService : System.Web.Services.WebService
    {
        [WebMethod(Description = "调用SAP RFC")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ServerResult ExecuteSAPRFC(string rfcName, string jsonData, string SAPInstanceId)
        {
            ServerResult result = new ServerResult();

            try
            {
                //反序列化
                DEEntityInstanceBase resultInstance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(jsonData);

                //调用sap服务 
                #region
                if (string.IsNullOrEmpty(SAPInstanceId))
                {
                    throw new Exception("请检查Config文件中是否配置了SAPInstanceId");
                }

                //todo:目前实体跟sap结构是1对1 所以只取第一个外部实体，将来会改成一对多，需要调用方传入外部实体名
                OuterEntity outerEntity = resultInstance.EntityDefine.OuterEntities.FirstOrDefault();
                outerEntity.NullCheck(string.Format("找不到实体定义【{0}】的外部实体!", resultInstance.EntityDefine.CodeName));

                string tCode = outerEntity.Name;
                List<SapValue> values = resultInstance.ToParams(tCode);

                //sap服务定义
                //Saplocalhost.WebServiceConnectSAP sapService = new Saplocalhost.WebServiceConnectSAP();
                var sapService = new Saplocalhost.WebServiceConnectSAPSoapClient();
                Saplocalhost.InstanceParam sappar = new Saplocalhost.InstanceParam();

                #region 给sap参数赋值

                //todo:这的转换不太好，以后WebService改为WCF后会解决此问题
                sappar.SapType = outerEntity.CustomType == InType.CustomInterface ? Saplocalhost.InType.CustomInterface : Saplocalhost.InType.StandardInterface;
                sappar.Values = ChangeToWSValueType(values);

                //获取Sap用户信息
                string errorMessage = string.Empty;

                // 获取SAP用户连接信息
                SAPClient sapParamLoad = SAPClientAdapter.Instance.LoadByID(SAPInstanceId);

                if (sapParamLoad == null)
                {
                    errorMessage = "请检查Config文件的SAPInstanceId的配置项";
                }
                //if (string.IsNullOrEmpty(sapParamLoad.ApplicationServer) || string.IsNullOrEmpty(sapParamLoad.SystemNumber)
                //     || string.IsNullOrEmpty(sapParamLoad.Client) || string.IsNullOrEmpty(sapParamLoad.User)
                //     || string.IsNullOrEmpty(sapParamLoad.Password) || string.IsNullOrEmpty(sapParamLoad.Language))
                //{
                //    errorMessage = "请检查SAP连接数据";
                //}
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                #endregion

                //构造SAP登录参数
                var sapParam = buildSapLogonParam(sapParamLoad);

                //返回值
                string resultSap = string.Empty;
                Saplocalhost.InstanceParam returnValue = sapService.ExecuteSAPRFC(rfcName, sappar, sapParam);

                resultInstance.FromParams(ChangeToSapValue(returnValue.Values.ToArray()));
                //将返回结果序列化之后return给上一层（解决WS传输问题）
                result.ObjResult = JSONSerializerExecute.Serialize(resultInstance);
                #endregion
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Result = string.Format("{{'ErrorMsg':'{0}'}}", e.Message);
            }

            return result;
        }

        [WebMethod(Description = "调用SAP BDC")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ServerResult ExecuteSAPBDC(string jsonData, string clientID)
        {
            ServerResult result = new ServerResult();
            string resultSap = string.Empty;
            try
            {
                //反序列化
                DEEntityInstanceBase resultInstance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(jsonData);

                DEInstanceAdapter.Instance.Update(resultInstance);

                //调用sap服务 
                #region

                //todo:目前实体跟sap结构是1对1 所以只取第一个外部实体，将来会改成一对多，需要调用方传入外部实体名
                OuterEntity outerEntity = resultInstance.EntityDefine.OuterEntities.FirstOrDefault();
                outerEntity.NullCheck(string.Format("找不到实体定义【{0}】的外部实体!", resultInstance.EntityDefine.CodeName));

                string tCode = outerEntity.Name;
                List<SapValue> values = resultInstance.ToParams(tCode);

                //sap服务定义
                Saplocalhost.WebServiceConnectSAPSoapClient sapService = new Saplocalhost.WebServiceConnectSAPSoapClient();
                Saplocalhost.InstanceParam sappar = new Saplocalhost.InstanceParam();

                #region 给sap参数赋值
                //todo:这的转换不太好，以后WebService改为WCF后会解决此问题
                sappar.SapType = outerEntity.CustomType == InType.CustomInterface ? Saplocalhost.InType.CustomInterface : Saplocalhost.InType.StandardInterface;
                sappar.Values = GetRealSapValues(values);

                //SAPLoginParams Params = SAPLoginParams.GetConfig();

                string errorMessage = string.Empty;

                // 获取SAP用户连接信息
                SAPClient sapParamLoad = SAPClientAdapter.Instance.LoadByID(clientID);

                if (sapParamLoad == null)
                {
                    errorMessage = "请检查Config文件的SapAppServer和SapClient配置项";
                }
                if (string.IsNullOrEmpty(sapParamLoad.ApplicationServer) || string.IsNullOrEmpty(sapParamLoad.Client))
                {
                    errorMessage = "请检查Config文件的SapAppServer和SapClient配置项";
                }
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                #endregion

                var sapParam = buildSapLogonParam(sapParamLoad);

                //返回值
                //(问海军)
                DataTable returnResult = sapService.ExecuteSAP(out resultSap, tCode, sappar, sapParam);
                returnResult.TableName = "sapResult";
                result.ObjResult = SerializeDataTableXml(returnResult);

                #endregion
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Result = e.Message;
            }

            return result;
        }

        [WebMethod(Description = "更新实体实例")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ServerResult UpdateInstance(string jsonData)
        {
            ServerResult result = new ServerResult();

            try
            {
                //反序列化
                DEEntityInstanceBase resultInstance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(jsonData);

                DEInstanceAdapter.Instance.Update(resultInstance);

                result.Result = string.Format("{{'ID':'{0}'}}", resultInstance.ID);
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Result = string.Format("{{'ErrorMsg':'{0}'}}", e.Message);
            }

            return result;
        }

        [WebMethod(Description = "获取新的实体实例")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ServerResult GetNewInstance(string codeName, DateTime timeStamp)
        {
            ServerResult result = new ServerResult();

            try
            {
                codeName.CheckStringIsNullOrEmpty<ArgumentNullException>("CodeName不能为空");
                timeStamp.NullCheck<ArgumentNullException>("时间戳不能为NULL");

                var entnity = DEDynamicEntityAdapter.Instance.LoadByCodeName(codeName, timeStamp) as DynamicEntity;
                DEEntityInstanceBase instance = entnity.CreateInstance();

                string json = JSONSerializerExecute.Serialize(instance);

                result.Result = json;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Result = e.Message;
            }

            return result;

        }

        [WebMethod(Description = "获取存在的实体实例")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ServerResult GetExistInstance(string instanceID)
        {
            ServerResult result = new ServerResult();

            try
            {
                instanceID.CheckStringIsNullOrEmpty<ArgumentNullException>("实例ID不能为空");

                //根据实例ID获取实例对象
                DEEntityInstanceBase instance = DEInstanceAdapter.Instance.Load(instanceID);

                string json = JSONSerializerExecute.Serialize(instance);

                result.Result = json;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Result = e.Message;
            }

            return result;

        }

        [WebMethod(Description = "获取某实体定义的实例列表")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ServerResult GetInstanceList(string entityCodeName, int pageIndex, int pageSize, ref int totalNum)
        {
            ServerResult result = new ServerResult();

            try
            {
                entityCodeName.CheckStringIsNullOrEmpty<ArgumentNullException>("实体定义CodeName不能为空");
                DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadByCodeName(entityCodeName) as DynamicEntity;

                //根据实例ID获取实例对象
                DEEntityInstanceBaseCollection instances = DEInstanceAdapter.Instance.LoadByEntityID(entity.ID, pageIndex, pageSize, ref totalNum);

                string json = JSONSerializerExecute.Serialize(instances);

                result.Result = json;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Result = e.Message;
            }

            return result;
        }

        [WebMethod(Description = "验证实体实例")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool ValidateInstance(string jsonData, out string errorMsg)
        {
            errorMsg = "";
            bool result = false;
            ValidationResults validateResult = new ValidationResults();

            try
            {
                //反序列化
                DEEntityInstance resultInstance = JSONSerializerExecute.Deserialize<DEEntityInstanceBase>(jsonData) as DEEntityInstance;
                validateResult = resultInstance.Validate();

                if (validateResult.IsValid())
                {
                    result = true;
                }
                else
                {
                    //错误信息格式 json格式
                    errorMsg = "[";
                    foreach (var rst in validateResult)
                    {
                        errorMsg += string.Format("{{{0}}},", rst.Message.ToString());
                    }
                    errorMsg = errorMsg.TrimEnd(',') + "]";
                }

            }
            catch (Exception e)
            {
                errorMsg = string.Format("[{{'ErrorMsg':'{0}'}}]", e.Message);
            }

            return result;
        }

        /// <summary>
        /// 获取ETL实体的连接字符串
        /// </summary>
        /// <param name="codeName">ETL实体的标识</param>
        /// <returns></returns>
        [WebMethod(Description = "获取ETL实体DB连接字符串")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public DbConnectionStr GetDbServerStr(string codeName)
        {
            DbConnectionStr result = new DbConnectionStr();
            try
            {
                var entnity = DEDynamicEntityAdapter.Instance.LoadByCodeName(codeName) as ETLEntity;

                result.Result = entnity.ETLConnectionString;
                result.Schame = entnity.DBSchema;
                result.TableName = entnity.TableName;
            }
            catch (Exception e)
            {

                result.IsSuccess = false;
                result.Result = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 将契约类型转为
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private Saplocalhost.SapValue[] ChangeToWSValueType(List<SapValue> values)
        {
            List<Saplocalhost.SapValue> realValues = new List<Saplocalhost.SapValue>();
            foreach (var value in values)
            {
                Saplocalhost.SapValue newSapValue = new Saplocalhost.SapValue();

                newSapValue.Key = value.Key;

                if (value.Value.GetType() == typeof(string))
                {
                    newSapValue.Value = value.Value;
                }
                else
                {
                    newSapValue.Value = ChangeToWSValueType(value.Value as List<SapValue>);
                }

                realValues.Add(newSapValue);
            }
            return realValues.ToArray();
        }

        /// <summary>
        /// 转换为SAPValue
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private List<SapValue> ChangeToSapValue(Saplocalhost.SapValue[] values)
        {
            var realValues = new List<SapValue>();
            foreach (var value in values)
            {
                var newSapValue = new SapValue();

                newSapValue.Key = value.Key;

                if (value.Value.GetType() == typeof(string))
                {
                    newSapValue.Value = value.Value;
                }
                else
                {
                    newSapValue.Value = ChangeToSapValue(value.Value as Saplocalhost.SapValue[]);
                }

                realValues.Add(newSapValue);
            }
            return realValues;
        }
        private Saplocalhost.SapValue[] GetRealSapValues(List<SapValue> values)
        {
            List<Saplocalhost.SapValue> realValues = new List<Saplocalhost.SapValue>();
            foreach (var value in values)
            {
                Saplocalhost.SapValue newSapValue = new Saplocalhost.SapValue();

                newSapValue.Key = value.Key;

                if (value.Value.GetType() == typeof(string))
                {
                    newSapValue.Value = value.Value;
                }
                else
                {
                    newSapValue.Value = GetRealSapValues(value.Value as List<SapValue>);
                }
                realValues.Add(newSapValue);
            }
            return realValues.ToArray();
        }

        /// <summary>
        /// 序列化DataTable
        /// </summary>
        /// <param name="pDt">包含数据的DataTable</param>
        /// <returns>序列化的DataTable</returns>
        private string SerializeDataTableXml(DataTable pDt)
        {
            // 序列化DataTable
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();

            return sb.ToString();
        }

        private SAPPara buildSapLogonParam(SAPClient sapParamLoad)
        {
            var sapParam = new SAPPara()
            {
                ApplicationServer = sapParamLoad.ApplicationServer,
                Client = sapParamLoad.Client,
                Language = sapParamLoad.Language,
                Password = sapParamLoad.Password,
                SystemNumber = int.Parse(sapParamLoad.SystemNumber),
                User = sapParamLoad.User,
                MessageServerHost = sapParamLoad.MessageServerHost,
                MessageServerService = sapParamLoad.MessageServerService,
                LogonGroup = sapParamLoad.LogonGroup,
                AppServerService = sapParamLoad.AppServerService,
                SystemID = sapParamLoad.SystemID
            };

            return sapParam;
        }
    }
}
