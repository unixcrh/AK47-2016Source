using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Contract;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    [Serializable]
    public class EntityMapping
    {
        /// <summary>
        /// 内部实体
        /// </summary>
        public DynamicEntity InnerEntity { get; set; }

        public string OuterEntityID { get; set; }

        public string OuterEntityName { get; set; }

        public InType OuterEntityInType { get; set; }

        private List<EntityFieldMapping> _EntityFieldMappingCollection;
        public List<EntityFieldMapping> EntityFieldMappingCollection
        {
            get
            {
                if (InnerEntity != null && _EntityFieldMappingCollection == null)
                {
                    _EntityFieldMappingCollection = new List<EntityFieldMapping>();
                    InnerEntity.Fields.OrderBy(p => p.SortNo).ForEach(p =>
                    {
                        #region 注释掉OuterEntity的Mapping逻辑。王雷平 2015-8-13
                        
                        #endregion
                        //string _outerFieldName = string.Empty;
                        //string _outerFieldID = string.Empty;
                        //if (!string.IsNullOrEmpty(OuterEntityID))
                        //{
                        //    OuterEntityField outerField = p.OuterEntityFields.Where(f => f.OuterEntity.ID.Equals(OuterEntityID)).FirstOrDefault();
                        //    if (outerField != null)
                        //    {
                        //        _outerFieldName = outerField.Name;
                        //        _outerFieldID = outerField.ID;
                        //    }
                        //}

                        _EntityFieldMappingCollection.Add(new EntityFieldMapping()
                        {
                            FieldID = p.ID,
                            FieldDesc = p.Description,
                            FieldTypeName = p.FieldType.ToString(),
                            FieldName = p.Name,
                            FieldLength = p.Length,
                            FieldDefaultValue = p.DefaultValue,
                            //OuterFieldName = _outerFieldName,
                            //OuterFieldID = _outerFieldID,
                            SortNo = p.SortNo
                        });
                    });
                }

                return _EntityFieldMappingCollection;
            }
            set { _EntityFieldMappingCollection = value; }
        }
    }

    [Serializable]
    public class EntityFieldMapping
    {
        public int SortNo { get; set; }

        public string FieldID { get; set; }

        public string FieldName { get; set; }

        public string FieldDesc { get; set; }

        public string FieldTypeName { get; set; }

        public int FieldLength { get; set; }

        public string FieldDefaultValue { get; set; }

        /// <summary>
        /// 外部字段编码
        /// </summary>
        //public string OuterFieldID { get; set; }

        /// <summary>
        /// 外部字段名称
        /// </summary>
        //public string OuterFieldName { get; set; }

        public void BuildFromField(DynamicEntityField field)
        {
            this.FieldID = field.ID;
            this.FieldName = field.Name;
            this.FieldDesc = field.Description;
            this.FieldTypeName = field.FieldType.ToString();
            this.FieldLength = field.Length;
            this.FieldDefaultValue = field.DefaultValue;
        }
    }
}
