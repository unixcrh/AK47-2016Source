using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Dynamics.Objects
{
    /// <summary>
    /// 校验器定义实体
    /// </summary>
    [Serializable]
    public class ValidatorDefine
    {
        public ValidatorDefine()
        {
            this.Parameters = new List<ValidatorParameter>();
        }

        /// <summary>
        /// 校验器名称
        /// </summary>
        public string ValidatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 校验器描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 校验器类型
        /// </summary>
        public string ValidatorType
        {
            get;
            set;
        }

        /// <summary>
        /// 校验器参数列表
        /// </summary>
        public List<ValidatorParameter> Parameters
        {
            get;
            set;
        }

        public Validator ToValidator()
        {
            Type type = Type.GetType(this.ValidatorType);
            var validator = Activator.CreateInstance(type);
            //var validator =ValidationFactory.CreateValidator(type);
            PropertyInfo[] properties = type.GetProperties();
            foreach (ValidatorParameter param in this.Parameters)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.ToLower().Equals(param.Name.ToLower()))
                    {
                        if (param.DataType == PropertyDataType.Enum)
                        {
                            continue;
                        }
                        else
                        {
                            property.SetValue(validator, Convert.ChangeType(param.ParamValue, (TypeCode)((int)param.DataType)));
                        }
                       
                        break;
                    }
                }
            }
            return (Validator)validator;
        }
    }
    /// <summary>
    /// 校验器参数实体
    /// </summary>
    [Serializable]
    public class ValidatorParameter
    {
        public ValidatorParameter()
        { }

        public ValidatorParameter(string name, string paramValue,PropertyDataType dataType)
        {
            this._name = name;
            this._paramValue = paramValue;
            this._dataType = dataType;
        }

        private string _name;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _paramValue;
        /// <summary>
        /// 参数值
        /// </summary>
        public string ParamValue
        {
            get { return _paramValue; }
            set { _paramValue = value; }
        }

        private PropertyDataType _dataType;
        /// <summary>
        /// 数据类型
        /// </summary>
        public PropertyDataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }
    }
}
