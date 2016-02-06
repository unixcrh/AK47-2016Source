using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Dynamics.Web.Saplocalhost;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.Library.MVC;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Instance.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Instance;
using MCS.Web.Library.Script;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Converters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using SapValue = MCS.Library.SOA.DataObjects.Dynamics.Contract.SapValue;


namespace MCS.Dynamics.Web
{
    public partial class SaleOrder_RFC : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            ////注册序列化器
            //JSONSerializerExecute.RegisterConverter(typeof(DEEntityInstanceConverter));
            //JSONSerializerExecute.RegisterConverter(typeof(DynamicEntityConvert));
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //主表
            SaleOrderEntityMain_RFC main = new SaleOrderEntityMain_RFC();

            #region 子表

            #region //子表1 -- 以前的主表
            List<SaleOrderEntityChild_I_ZR5_SS02_RFC> child_I_ZR5_SS02 = new List<SaleOrderEntityChild_I_ZR5_SS02_RFC>();

            SaleOrderEntityChild_I_ZR5_SS02_RFC saleEntity = new SaleOrderEntityChild_I_ZR5_SS02_RFC();

            saleEntity.BUKRS = this.BUKRS.SelectedValue;
            saleEntity.CZDAT = DateTime.Parse(this.CZDAT.Text).ToString("yyyyMMdd");
            saleEntity.EDAT = DateTime.Parse(this.EDAT.Text).ToString("yyyyMMdd");
            saleEntity.KUNNR = this.KUNNR.SelectedValue;
            saleEntity.OIC_MOT = this.OIC_MOT.Text;
            saleEntity.RMAN = this.RMAN.Text;
            saleEntity.SALESNUM = this.SALESNUM.Text;
            saleEntity.VKBUR = this.VKBUR.Text;
            saleEntity.VKORG = this.VKORG.SelectedValue;
            saleEntity.VTWEG = this.VTWEG.SelectedValue;
            saleEntity.YSHHS = this.YSHHS.SelectedValue;

            child_I_ZR5_SS02.Add(saleEntity);
            #endregion


            #region 以前的子表
            DEEntityInstanceBaseCollection entityItemsChild_I_ZR5_SS02 = new DEEntityInstanceBaseCollection();
            DEEntityInstanceBaseCollection entityItemsChild_T_ZR5_SS02P = new DEEntityInstanceBaseCollection();
            List<SaleOrderEntityChild_T_ZR5_SS02P_RFC> dd = JSONSerializerExecute.Deserialize<List<SaleOrderEntityChild_T_ZR5_SS02P_RFC>>(hiddenDetail.Value);
            dd.ForEach(s =>
            {
                entityItemsChild_T_ZR5_SS02P.Add(s.Instance);
            });


            child_I_ZR5_SS02.ForEach(s =>
            {
                entityItemsChild_I_ZR5_SS02.Add(s.Instance);
            });
            #endregion

            #endregion

            main.Instance.Fields.SetValue("I_ZR5_SS02", entityItemsChild_I_ZR5_SS02);
            main.Instance.Fields.SetValue("T_ZR5_SS02P", entityItemsChild_T_ZR5_SS02P);
            //进库
            DEInstanceAdapter.Instance.Update(main.Instance);

            #region 调用sap接口
            //Saplocalhost.WebServiceConnectSAP sapServiece = new Saplocalhost.WebServiceConnectSAP();
            var sapServiece = new Saplocalhost.WebServiceConnectSAPSoapClient();

            #region 给sap参数赋值
            Saplocalhost.InstanceParam sappar = new Saplocalhost.InstanceParam();

            //SAPLoginParams Params = SAPLoginParams.GetConfig();

            sappar.SapType = Saplocalhost.InType.CustomInterface;

            var itemList = main.Instance.ToParams("ZR5_SDIF_MH003");

            List<Saplocalhost.SapValue> sapValueList = new List<Saplocalhost.SapValue>();

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].Value.GetType() == typeof(string))
                {
                    sapValueList.Add(new Saplocalhost.SapValue()
                    {
                        Key = itemList[i].Key,
                        Value = itemList[i].Value
                    });
                }
                else
                {
                    var list = itemList[i].Value as List<SapValue>;
                    List<Saplocalhost.SapValue> child = new List<Saplocalhost.SapValue>();
                    foreach (var item in list)
                    {
                        var result = new List<Saplocalhost.SapValue>();
                        var items = (List<SapValue>)item.Value;
                        foreach (var item3 in items)
                        {
                            result.Add(
                             new Saplocalhost.SapValue()
                             {
                                 Key = item3.Key,
                                 Value = item3.Value
                             });
                        }
                        child.Add(new Saplocalhost.SapValue()
                        {
                            Key = "mingxi",
                            Value = result.ToArray()
                        });

                    }

                    sapValueList.Add(new Saplocalhost.SapValue()
                    {
                        Key = itemList[i].Key,
                        Value = child.ToArray()
                    });


                }
            }
            sappar.Values = sapValueList.ToArray();

            //sappar.RFCName = "ZR5_SDIF_MH003";
            #endregion

            string resultParam = string.Empty;
            //string result1 = sapServiece.ExecuteSAP(sappar, out resultParam);
            sapServiece.ExecuteSAPRFC("ZR5_SDIF_MH003", sappar, new SAPPara()
            {
                ApplicationServer = Params.ApplicationServer,
                Client = Params.Client,
                Language = Params.Language,
                Password = Params.Password,
                SystemNumber = int.Parse(Params.SystemNumber),
                User = Params.User
            });

            #endregion

            Response.Write(resultParam);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //DEEntityInstanceBase result = DEInstanceAdapter.Instance.Load("651054a5-e4e5-4424-9db9-c586cbaf95cf");
            //SaleOrderEntity saleEntity = new SaleOrderEntity(result);
            //this.CZDAT.Text = saleEntity.CZDAT;
            //this.RMAN.Text = saleEntity.RMAN;
            //this.EDAT.Text = saleEntity.EDAT;
            ////result.Fields.Select
            //var coll = result.Fields.FirstOrDefault(p => p.Definition.FieldType == FieldTypeEnum.Collection).GetRealValue() as DEEntityInstanceBaseCollection;


            //hiddenDetail.Value = JSONSerializerExecute.Serialize(coll);
            ////DEEntityInstance
            //List<SaleOrderEntityDetails> details = new List<SaleOrderEntityDetails>();

            // JSONSerializerExecute.Serialize(null);
            DynamicEntity des = (DynamicEntity)DEDynamicEntityAdapter.Instance.LoadByCodeName("/集团公司/管道板块/勘探/SaleOrder");
            string json = JSONSerializerExecute.Serialize(des);
            Response.Write(json);
            //sapServiece.ExecuteSAP(
        }
    }


    /// <summary>
    /// 主表实体
    /// </summary>
    public class SaleOrderEntityMain_RFC
    {

        public SaleOrderEntityMain_RFC(DEEntityInstanceBase instance)
        {
            this._ID = instance.ID;
            this._I_ZJYBH = instance.Fields.GetValue<string>("I_ZJYBH", "");
            this._E_MESSAGE = instance.Fields.GetValue<string>("E_MESSAGE", "");
            this._E_STATE = instance.Fields.GetValue<string>("E_STATE", "");

        }
        public SaleOrderEntityMain_RFC()
        {

            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadByCodeName("/集团公司/销售板块/销售订单/ZR5_SDIF_MH003") as DynamicEntity;
            _instance = entity.CreateInstance();

        }

        private string _ID;

        public string ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                this._instance.ID = _ID;
            }
        }
        private DEEntityInstanceBase _instance;

        public DEEntityInstanceBase Instance
        {
            get { return _instance; }
        }

        private string _I_ZJYBH;
        public string I_ZJYBH
        {
            get { return _I_ZJYBH; }
            set
            {
                _I_ZJYBH = value;
                this._instance.Fields.TrySetValue<string>("I_ZJYBH", _I_ZJYBH);
            }
        }

        private string _E_MESSAGE;
        public string E_MESSAGE
        {
            get { return _E_MESSAGE; }
            set
            {
                _E_MESSAGE = value;
                this._instance.Fields.TrySetValue<string>("E_MESSAGE", _E_MESSAGE);
            }
        }

        private string _E_STATE;
        public string E_STATE
        {
            get { return _E_STATE; }
            set
            {
                _E_STATE = value;
                this._instance.Fields.TrySetValue<string>("E_STATE", _E_STATE);
            }
        }

    }

    public class SaleOrderEntityChild_I_ZR5_SS02_RFC
    {

        public SaleOrderEntityChild_I_ZR5_SS02_RFC(DEEntityInstanceBase instance)
        {
            this._ID = instance.ID;
            this._BUKRS = instance.Fields.GetValue<string>("BUKRS", "");
            this._CZDAT = instance.Fields.GetValue<string>("CZDAT", "");
            this._EDAT = instance.Fields.GetValue<string>("EDAT", "");
            this._KUNNR = instance.Fields.GetValue<string>("KUNNR", "");
            this._OIC_MOT = instance.Fields.GetValue<string>("OIC_MOT", "");
            this._RMAN = instance.Fields.GetValue<string>("RMAN", "");
            this._SALESNUM = instance.Fields.GetValue<string>("SALESNUM", "");
            this._VKBUR = instance.Fields.GetValue<string>("VKBUR", "");
            this._VKORG = instance.Fields.GetValue<string>("VKORG", "");
            this._VTWEG = instance.Fields.GetValue<string>("VTWEG", "");
            this._YSHHS = instance.Fields.GetValue<string>("YSHHS", "");
        }
        public SaleOrderEntityChild_I_ZR5_SS02_RFC()
        {
            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadByCodeName("/集团公司/销售板块/销售订单/I_ZR5_SS02") as DynamicEntity;
            _instance = entity.CreateInstance();

        }
        private string _YSHHS;

        public string YSHHS
        {
            get
            {
                return _YSHHS;
            }
            set
            {
                _YSHHS = value;
                this._instance.Fields.TrySetValue<string>("YSHHS", _YSHHS);
            }
        }

        private DEEntityInstanceBase _instance;

        public DEEntityInstanceBase Instance
        {
            get { return _instance; }
        }
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                this._instance.ID = _ID;
            }
        }
        private string _BUKRS;  //公司代码

        public string BUKRS
        {
            get { return _BUKRS; }
            set
            {
                _BUKRS = value;
                this._instance.Fields.TrySetValue<string>("BUKRS", _BUKRS);
            }
        }
        private string _OIC_MOT; //运输方式

        public string OIC_MOT
        {
            get { return _OIC_MOT; }
            set
            {
                _OIC_MOT = value;
                this._instance.Fields.TrySetValue<string>("OIC_MOT", _OIC_MOT);
            }
        }
        private string _KUNNR;  // 客户

        public string KUNNR
        {
            get { return _KUNNR; }
            set
            {
                _KUNNR = value;
                this._instance.Fields.TrySetValue<string>("KUNNR", _KUNNR);
            }
        }
        private string _SALESNUM; // 销售代表

        public string SALESNUM
        {
            get { return _SALESNUM; }
            set
            {
                _SALESNUM = value;
                this._instance.Fields.TrySetValue<string>("SALESNUM", _SALESNUM);
            }
        }
        private string _VKBUR; // 销售部门

        public string VKBUR
        {
            get { return _VKBUR; }
            set
            {
                _VKBUR = value;
                this._instance.Fields.TrySetValue<string>("VKBUR", _VKBUR);
            }
        }
        private string _CZDAT; // 单据日期

        public string CZDAT
        {
            get { return _CZDAT; }
            set
            {
                _CZDAT = value;
                this._instance.Fields.TrySetValue<string>("CZDAT", _CZDAT);
            }
        }
        private string _VKORG; // 单据销售主题

        public string VKORG
        {
            get { return _VKORG; }
            set
            {
                _VKORG = value;
                this._instance.Fields.TrySetValue<string>("VKORG", _VKORG);
            }
        }
        private string _VTWEG; // 销售方式

        public string VTWEG
        {
            get { return _VTWEG; }
            set
            {
                _VTWEG = value;
                this._instance.Fields.TrySetValue<string>("VTWEG", _VTWEG);
            }
        }
        private string _RMAN; //联系人

        public string RMAN
        {
            get { return _RMAN; }
            set
            {
                _RMAN = value;
                this._instance.Fields.TrySetValue<string>("RMAN", _RMAN);
            }
        }
        private string _EDAT; //有效截至日期

        public string EDAT
        {
            get { return _EDAT; }
            set
            {
                _EDAT = value;
                this._instance.Fields.TrySetValue<string>("EDAT", _EDAT);
            }
        }



    }

    public class SaleOrderEntityChild_T_ZR5_SS02P_RFC
    {
        public SaleOrderEntityChild_T_ZR5_SS02P_RFC()
        {
            DynamicEntity entity = DEDynamicEntityAdapter.Instance.LoadByCodeName("/集团公司/销售板块/销售订单/T_ZR5_SS02P") as DynamicEntity;
            _instance = entity.CreateInstance();
        }

        public SaleOrderEntityChild_T_ZR5_SS02P_RFC(DEEntityInstanceBase instance)
        {

        }

        private DEEntityInstanceBase _instance;

        public DEEntityInstanceBase Instance
        {
            get { return _instance; }
        }
        private string _MATNR; //油品代码

        public string MATNR
        {
            get { return _MATNR; }
            set
            {
                _MATNR = value;
                this._instance.Fields.TrySetValue("MATNR", _MATNR);
            }
        }
        private string _MAKTX; //名称规格

        public string MAKTX
        {
            get { return _MAKTX; }
            set
            {
                _MAKTX = value;
                this._instance.Fields.TrySetValue("MAKTX", _MAKTX);
            }
        }
        private string _KWMENG; //数量

        public string KWMENG
        {
            get { return _KWMENG; }
            set
            {
                _KWMENG = value;
                this._instance.Fields.TrySetValue("KWMENG", _KWMENG);
            }
        }
        private string _VRKME; //计量单位代码

        public string VRKME
        {
            get { return _VRKME; }
            set
            {
                _VRKME = value;
                this._instance.Fields.TrySetValue("VRKME", _VRKME);
            }
        }
        private string _VRKME_NAME; //计量单位名称

        public string VRKME_NAME
        {
            get { return _VRKME_NAME; }
            set
            {
                _VRKME_NAME = value;
                this._instance.Fields.TrySetValue("VRKME_NAME", _VRKME_NAME);
            }
        }
        private string _WERKS; //业务单元代码

        public string WERKS
        {
            get { return _WERKS; }
            set
            {
                _WERKS = value;
                this._instance.Fields.TrySetValue("WERKS", _WERKS);
            }
        }
        private string _WERKS_NAME; //业务单元名称

        public string WERKS_NAME
        {
            get { return _WERKS_NAME; }
            set
            {
                _WERKS_NAME = value;
                this._instance.Fields.TrySetValue("WERKS_NAME", _WERKS_NAME);
            }
        }
        private string _LGORT; //库存地点代码

        public string LGORT
        {
            get { return _LGORT; }
            set
            {
                _LGORT = value;
                this._instance.Fields.TrySetValue("LGORT", _LGORT);
            }
        }
        private string _LGORT_NAME; //库存地点名称

        public string LGORT_NAME
        {
            get { return _LGORT_NAME; }
            set
            {
                _LGORT_NAME = value;
                this._instance.Fields.TrySetValue("LGORT_NAME", _LGORT_NAME);
            }
        }

        private string _ZNETPR_C; //价格

        public string ZNETPR_C
        {
            get { return _ZNETPR_C; }
            set
            {
                _ZNETPR_C = value;
                this._instance.Fields.TrySetValue("ZNETPR_C", _ZNETPR_C);
            }
        }
        private string _ZongJinE; //总金额

        public string ZongJinE
        {
            get { return _ZongJinE; }
            set
            {
                _ZongJinE = value;
                this._instance.Fields.TrySetValue("ZongJinE", _ZongJinE);
            }
        }

        private string _LFDAT; //交货日期

        public string LFDAT
        {
            get { return _LFDAT; }
            set
            {
                _LFDAT = value;
                this._instance.Fields.TrySetValue("LFDAT", _LFDAT);
            }
        }
        private string _WAERK; //货币代码

        public string WAERK
        {
            get { return _WAERK; }
            set
            {
                _WAERK = value;
                this._instance.Fields.TrySetValue("WAERK", _WAERK);
            }
        }
        private string _WAERK_NAME; //货币名称

        public string WAERK_NAME
        {
            get { return _WAERK_NAME; }
            set
            {
                _WAERK_NAME = value;
                this._instance.Fields.TrySetValue("WAERK_NAME", _WAERK_NAME);
            }
        }

        public string BWTAR
        {
            get { return _BWTAR; }
            set
            {
                _BWTAR = value;
                this._instance.Fields.TrySetValue("BWTAR", _BWTAR);
            }
        }
        private string _BWTAR; //评估类型


        public string POSNR
        {
            get { return _POSNR; }
            set
            {
                _POSNR = value;
                this._instance.Fields.TrySetValue("POSNR", _POSNR);
            }
        }
        private string _POSNR; //行项目编码
        //
    }


}