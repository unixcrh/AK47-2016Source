using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MCS.Library.Configuration;
using System.Collections.Specialized;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Others;
using System.Reflection;


namespace MCS.Library.SOA.DataObjects.Dynamics.Configuration
{
    public class SAPFileMapping : ConfigurationSection
    {
        public static SAPFileMapping GetConfig()
        {
            SAPFileMapping result = (SAPFileMapping)ConfigurationBroker.GetSection("DynamicsFiledTypesMapping");
            if (result == null)
            {
                result = new SAPFileMapping();
            }
            return result;
        }

        private SAPFiledElementCollection _SAPFileds;
        [ConfigurationProperty("properties", IsDefaultCollection = true)]
        public SAPFiledElementCollection SAPFileds
        {
            get
            {
                if (_SAPFileds == null)
                {
                    _SAPFileds = (SAPFiledElementCollection)base["properties"];
                }
                return _SAPFileds;
            }
        }

        /// <summary>
        /// 根据SAP字段类型和长度返回UEP字段和长度
        /// </summary>
        /// <param name="fileType">字段类型</param>
        /// <param name="fileLength">字段长度</param>
        public static FieldTypeEnum SAPFiledTypeToUEPFiledType(string fileType, ref int fileLength)
        {
            FieldTypeEnum fieldType = new FieldTypeEnum();

            SAPFileMapping settings = SAPFileMapping.GetConfig();

            //settings.SAPFileds
            string outfiletype = fileType;
            int outFilelenth = fileLength;
            var sapFiledElements = settings.SAPFileds.Cast<SAPFiledElement>();

            // GetTypeMapping gm = new GetTypeMapping(GetUepTypeMappingAdd);

            var sapElementMapping = sapFiledElements.FirstOrDefault(p => p.OutType.ToUpper().Equals(outfiletype.ToUpper()) && GetUepTypeMapping(p.Opration, outFilelenth, p.OutLength));
            if (sapElementMapping != null)
            {
                fieldType = (FieldTypeEnum)(Enum.Parse(typeof(FieldTypeEnum), sapElementMapping.UEPType));
                fileType = sapElementMapping.UEPType;
                string[] typeStrings = sapElementMapping.UEPLengthDelegate.Split(',');
                Type uepLenthType = Type.GetType(typeStrings[0]);
                object instance = Activator.CreateInstance(uepLenthType);
                GetUepTypeLength uepTypeMothend =
                    (GetUepTypeLength)Delegate.CreateDelegate(typeof(GetUepTypeLength), instance, typeStrings[1]);

                fileLength = uepTypeMothend(outFilelenth, sapElementMapping.OperationLength);

            }
            else
            {
                fieldType = FieldTypeEnum.String;
                fileType = "string";
            }
            return fieldType;

            // this.GetSapFileElement(settings.SAPFileds, fileType, fileLength);
        }

        private static bool GetUepTypeMapping(string filedType, int outFiledLenth, int uepFiledLenth)
        {
            bool result = false;
            switch (filedType)
            {
                case ">":
                    result = outFiledLenth - uepFiledLenth > 0;
                    break;
                case "=":
                    result = outFiledLenth == uepFiledLenth;
                    break;
                case "<":
                    result = outFiledLenth < uepFiledLenth;
                    break;
            }
            return result;
        }
    }

    public class SAPFiledElement : ConfigurationElement
    {
        #region 属性
        [ConfigurationProperty("name", IsRequired = true,IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("OutType", IsRequired = true)]
        public string OutType
        {
            get
            {
                return (string)base["OutType"];
            }
            set
            {
                base["OutType"] = value;
            }
        }

        [ConfigurationProperty("Opration", IsRequired = true)]
        public string Opration
        {
            get
            {
                return (string)base["Opration"];
            }
            set
            {
                base["Opration"] = value;
            }
        }

        [ConfigurationProperty("OutLength", IsRequired = true)]
        public int OutLength
        {
            get
            {
                return (int)base["OutLength"];
            }
            set
            {
                base["OutLength"] = value;
            }
        }
        [ConfigurationProperty("UEPType", IsRequired = true)]
        public string UEPType
        {
            get
            {
                return (string)base["UEPType"];
            }
            set
            {
                base["UEPType"] = value;
            }
        }
        [ConfigurationProperty("UEPLengthDelegate", IsRequired = true)]
        public string UEPLengthDelegate
        {
            get
            {
                return (string)base["UEPLengthDelegate"];
            }
            set
            {
                base["UEPLengthDelegate"] = value;
            }
        }
        [ConfigurationProperty("OperationLength", IsRequired = true)]
        public int OperationLength
        {
            get
            {
                return (int)base["OperationLength"];
            }
        }
        #endregion
    }

    public class SAPFiledElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SAPFiledElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SAPFiledElement) element).Name;
        }

        public SAPFiledElement this[int index]
        {
            get
            {
                return (SAPFiledElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }
}
