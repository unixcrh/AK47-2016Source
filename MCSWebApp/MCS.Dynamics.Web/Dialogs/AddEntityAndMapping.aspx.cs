using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Core;
using MCS.Web.Library;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Web.WebControls;
using System.Data;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;

namespace MCS.Dynamics.Web.Dialogs
{
    public partial class AddEntityAndMapping : System.Web.UI.Page
    {
        List<string> controlIDs = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_save_OnClick(object sender, EventArgs e)
        {
            var con = this.FindControls(typeof(ClientGrid), true);
            var aa = center_panel.FindControl(typeof(ClientGrid), true);
            if (ViewState["controlids"] != null)
            {
                var list = ViewState["controlids"] as List<string>;
                foreach (var controlID in list)
                {
                    var bb = center_panel.FindControl(controlID);
                }

            }
            Response.Write(con.Count);
        }

        protected void btn_getEntityDefine_Click(object sender, EventArgs e)
        {
            center_panel.Visible = true;






            RecordResultCollection rrc = GetData(txt_Code.Text.Trim());

            List<string> cou = rrc.Select(p => p.EntityName).Distinct().ToList();
            foreach (string entityName in cou)
            {
                ClientGrid gr = new ClientGrid();

                gr.EnableViewState = false;


                gr.ID = entityName;
                gr.AllowPaging = false;
                gr.ShowEditBar = true;
                gr.AutoPaging = false;
                gr.ShowCheckBoxColumn = true;
                gr.ID = Guid.NewGuid().ToString();
                controlIDs.Add(gr.ID);
                var recordCollention = rrc.Where(p => p.EntityName.Equals(entityName));
                ClientGridColumn cgc = new ClientGridColumn();  //checkbox
                cgc.SelectColumn = true;
                cgc.ShowSelectAll = true;
                cgc.HeaderStyle = "{width:'30px',textAlign:'left',fontWeight:'bold'}";
                cgc.ItemStyle = "{width:'30px',textAlign:'left'}";

                gr.Columns.Add(cgc);

                ClientGridColumn cgIndex = new ClientGridColumn();

                cgIndex.DataField = "rowIndex";
                cgIndex.HeaderText = "序号";
                cgIndex.HeaderStyle = "{width:'30px',textAlign:'center'}";
                cgIndex.ItemStyle = "{width:'30px',textAlign:'center'}";
                cgIndex.DataType = DataType.Integer;
                gr.Columns.Add(cgIndex);

                ClientGridColumn cgFieldName = new ClientGridColumn();
                cgFieldName.DataField = "FieldName";
                cgFieldName.HeaderText = "字段名";
                cgFieldName.HeaderStyle = "{textAlign:'left'}";
                cgFieldName.ItemStyle = "{textAlign:'left'}";
                cgFieldName.DataType = DataType.String;
                cgFieldName.EditTemplate.EditMode = ClientGridColumnEditMode.TextBox;
                gr.Columns.Add(cgFieldName);

                ClientGridColumn cgFieldDesc = new ClientGridColumn();
                cgFieldDesc.DataField = "FieldDesc";
                cgFieldDesc.HeaderText = "字段描述";
                cgFieldDesc.HeaderStyle = "{textAlign:'left'}";
                cgFieldDesc.ItemStyle = "{textAlign:'left'}";
                cgFieldDesc.DataType = DataType.String;
                cgFieldDesc.EditTemplate.EditMode = ClientGridColumnEditMode.TextBox;
                gr.Columns.Add(cgFieldDesc);

                ClientGridColumn cgFieldTypeName = new ClientGridColumn();
                cgFieldTypeName.DataField = "FieldType";
                cgFieldTypeName.HeaderText = "字段类型";
                cgFieldTypeName.HeaderStyle = "{textAlign:'left'}";
                cgFieldTypeName.ItemStyle = "{textAlign:'left'}";
                cgFieldTypeName.DataType = DataType.String;
                cgFieldTypeName.EditTemplate.EditMode = ClientGridColumnEditMode.TextBox;
                gr.Columns.Add(cgFieldTypeName);


                ClientGridColumn cgFieldLength = new ClientGridColumn();
                cgFieldLength.DataField = "FieldLength";
                cgFieldLength.HeaderText = "字段长度";
                cgFieldLength.HeaderStyle = "{textAlign:'left'}";
                cgFieldLength.ItemStyle = "{textAlign:'left'}";
                cgFieldLength.DataType = DataType.String;
                cgFieldLength.EditTemplate.EditMode = ClientGridColumnEditMode.TextBox;
                gr.Columns.Add(cgFieldLength);

                ClientGridColumn cgFieldDefaultValue = new ClientGridColumn();
                cgFieldDefaultValue.DataField = "DefaultValue";
                cgFieldDefaultValue.HeaderText = "字段默认值";
                cgFieldDefaultValue.HeaderStyle = "{textAlign:'left'}";
                cgFieldDefaultValue.ItemStyle = "{textAlign:'left'}";
                cgFieldDefaultValue.DataType = DataType.String;
                cgFieldDefaultValue.EditTemplate.EditMode = ClientGridColumnEditMode.TextBox;
                gr.Columns.Add(cgFieldDefaultValue);

                ClientGridColumn cgOuterFieldName = new ClientGridColumn();
                cgOuterFieldName.DataField = "FieldName";
                cgOuterFieldName.HeaderText = "关联字段/结构";
                cgOuterFieldName.HeaderStyle = "{textAlign:'left'}";
                cgOuterFieldName.ItemStyle = "{textAlign:'left'}";
                cgOuterFieldName.DataType = DataType.String;
                gr.Columns.Add(cgOuterFieldName);
                gr.InitialData = recordCollention.ToList();



                DataBindingItem dbItem = new DataBindingItem();
                dbItem.ControlID = entityName;
                dbItem.DataPropertyName = "EntityFieldMappingCollection";
                dbItem.ControlPropertyName = "InitialData";
                dbItem.Direction = BindingDirection.Both;

                Panel pa = new Panel();

                Label lb = new Label();
                lb.Text = string.Format(@"<div class='dialogTitle'>
                                <div class='lefttitle' style='text-align: left;'>
                                    <img src='../Images/icon_01.gif'/>
                                    编辑{0}主实体字段及关联</div>
                            </div>", entityName);
                pa.Controls.Add(lb);

                Panel pn = new Panel();
                pn.ID = "Panel_" + entityName;
                pn.CssClass = "dialogContent";

                pn.Controls.Add(gr);

                center_panel.Controls.Add(pa);
                center_panel.Controls.Add(pn);
            }



            ViewState["controlids"] = controlIDs;










            EntityMapping mapping = new EntityMapping();


   
        }

        public RecordResultCollection GetData(string tCode)
        {
            //Saplocalhost.WebServiceConnectSAP srv = new Saplocalhost.WebServiceConnectSAP();
            var srv = new Saplocalhost.WebServiceConnectSAPSoapClient();
            DataTable table = srv.GetEntityDefine(tCode);
            RecordResultCollection resultList = new RecordResultCollection();

            var parentRows = table.Select();
            int sortNumber = 0;
            foreach (var item in parentRows)
            {
                sortNumber++;
                RecordResult result = new RecordResult();
                result.SortNo = sortNumber;
                result.EntityName = Convert.ToString(item["实体名"]);
                result.EntityDesc = Convert.ToString(item["实体描述"]);
                result.DefaultValue = Convert.ToString(item["默认值"]);
                result.IsMasterTable = Convert.ToString(item["主子标识"]) == "主" ? true : false;
                result.FieldName = Convert.ToString(item["字段名"]);
                FieldTypeEnum type = new FieldTypeEnum();
                switch (Convert.ToString(item["字段类型"]).ToLower())
                {
                    case "string":
                        type = FieldTypeEnum.String;
                        break;
                    case "int":
                        type = FieldTypeEnum.Int;
                        break;
                    case "bool":
                        type = FieldTypeEnum.Bool;
                        break;
                    case "datetime":
                        type = FieldTypeEnum.DateTime;
                        break;
                    case "decimal":
                        type = FieldTypeEnum.Decimal;
                        break;
                }
                result.FieldType = type;

                result.FieldDesc = Convert.ToString(item["字段描述"]);

                result.FieldLength = int.Parse(Convert.ToString(item["字段长度"]));

                resultList.Add(result);
            }
            return resultList;
        }
    }
}