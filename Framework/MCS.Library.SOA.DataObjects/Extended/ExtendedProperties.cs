using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MCS.Library.SOA.DataObjects
{
    [Serializable]
    [TenantRelativeObject]
    [ORTableMapping("WF.EXTENDED_PROPERTIES")]
    public class ExtendedProperties
    {
        private PropertyValueCollection _Properties = null;

        /// <summary>
        /// ID of data
        /// </summary>
        [ORFieldMapping("ID", PrimaryKey = true)]
        public virtual string ID
        {
            get;
            set;
        }

        [ORFieldMapping("RESOURCE_ID")]
        public virtual string ResourceID
        {
            get;
            set;
        }

        [ORFieldMapping("TYPE")]
        public virtual string Type
        {
            get;
            set;
        }

        [ORFieldMapping("FORMAT")]
        [SqlBehavior(DefaultExpression="'xml'")]
        public virtual string Format
        {
            get;
            set;
        }

        [ORFieldMapping("DATA")]
        protected virtual string Data
        {
            get
            {
                string result = string.Empty;

                if (this.Properties.Count > 0)
                {
                    XElementFormatter formatter = new XElementFormatter();

                    formatter.OutputShortType = true;

                    return formatter.Serialize(this.Properties).ToString();
                }

                return result;
            }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    this.Properties.Clear();
                }
                else
                {
                    XElementFormatter formatter = new XElementFormatter();

                    formatter.OutputShortType = true;

                    XElement root = XElement.Parse(value);

                    this._Properties = (PropertyValueCollection)formatter.Deserialize(root);
                }
            }
        }

        [NoMapping]
        public PropertyValueCollection Properties
        {
            get
            {
                if (this._Properties == null)
                    this._Properties = new PropertyValueCollection();

                return this._Properties;
            }
        }
    }

    [Serializable]
    public class ExtendedPropertiesCollection : EditableDataObjectCollectionBase<ExtendedProperties>
    {
    }
}
