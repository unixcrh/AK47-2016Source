using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Library;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;

namespace MCS.Dynamics.Web.Dialogs
{
    public partial class EntityMappingDetail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            // this.bindingControl.Data = LoadEntity(Request.QueryString["ID"], Request.QueryString["TableID"]);


            //this.whereCondition.Value = "ContainerID='" + Request.QueryString["ID"] + "'";

            if (!IsPostBack)
            {
                DynamicEntity entity = LoadEntity(Request.QueryString["ID"]);
                this.bindingControl.Data = entity;
                //this.DestinationName.Text = entity.OuterEntities.Any() ? entity.OuterEntities.FirstOrDefault().Name : string.Empty;
                //绑定字段类型
                //this.ddl_FieldType.BindData(EnumItemDescriptionAttribute.GetDescriptionList(typeof(FieldTypeEnum)), "EnumValue", "Description");
            }
        }

        private DynamicEntity LoadEntity(string entityID, string tableID)
        {
            entityID.CheckStringIsNullOrEmpty("[实体ID]");
            tableID.CheckStringIsNullOrEmpty("[关联表ID]");

            var loadEntity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            loadEntity.NullCheck("[获取实体]");
            return loadEntity;
        }

        private DynamicEntity LoadEntity(string entityID)
        {
            entityID.CheckStringIsNullOrEmpty("[实体ID]");
            var loadEntity = DESchemaObjectAdapter.Instance.Load(entityID) as DynamicEntity;
            //loadEntity

            loadEntity.NullCheck("[获取实体]");

            return loadEntity;
        }

        private string CreateWhereCondition()
        {
            var id = Request.QueryString["ID"].ToString();
            var entity = DESchemaObjectAdapter.Instance.Load(id) as DynamicEntity;
            List<string> fieldIDs = new List<string>();
            entity.Fields.ForEach(p => fieldIDs.Add(p.ID));

            return "";
        }

        protected void objectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }

        protected void objectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }

        //保存事件
        protected void btn_save_OnClick(object sender, EventArgs e)
        {
            DynamicEntity loadentity = LoadEntity(Request.QueryString["ID"]);
            this.bindingControl.Data = loadentity;
            bindingControl.CollectData(false);
            DynamicEntity entity = bindingControl.Data as DynamicEntity;

            //string HeadName = this.DestinationName.Text;
            //entity.OuterEntities.Add(new OuterEntity()
            //    {
            //        Name = HeadName,
            //        ID = Guid.NewGuid().ToString()
            //    });

            //entity.Fields.ForEach(p =>
            //{
            //    p.OuterEntityFields.Add(new OuterEntityField()
            //    {
            //        Name = entity.DynamicEntityMappingCollection.FirstOrDefault(f => f.FieldID.Equals(p.ID)).DestinationName,
            //        ID = Guid.NewGuid().ToString()
            //    });
            //});

            //DEObjectOperations.InstanceWithoutPermissions.AddEntityMapping(entity);

            WebUtility.ResponseCloseWindowScriptBlock();
        }
    }
}