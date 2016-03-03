using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Validation
{
    /// <summary>
    /// 客户端验证数据
    /// </summary>
    [Serializable]
    public class ClientVdtAttribute
    {
        //private string expression = string.Empty;
        private string messageTemplate = string.Empty;

        //add by fenglilei
        private string clientValidateMethodName = string.Empty;
        private Dictionary<string, object> additionalData = null;

        /// <summary>
        /// 
        /// </summary>
        public ClientVdtAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorAttribute"></param>
        /// <param name="pi"></param>
        public ClientVdtAttribute(ValidatorAttribute validatorAttribute, PropertyInfo pi)
        {
            //this.tag = validatorAttribute.Tag;
            this.messageTemplate = validatorAttribute.MessageTemplate;

            //add by Fenglilei,2011/11/7
            //modified by Fenglilei,2012/2/27
            if (validatorAttribute.Validator != null && validatorAttribute.Validator is IClientValidatable)
            {
                this.clientValidateMethodName =
                    ((IClientValidatable)validatorAttribute.Validator).ClientValidateMethodName;
            }
        }

        /// <summary>
        /// 客户端校验函数名称
        /// </summary>
        public string ClientValidateMethodName
        {
            get { return this.clientValidateMethodName; }
            set { this.clientValidateMethodName = value; }
        }

        /// <summary>
        /// 校验上的附加信息，比如正则表达式，范围值，等等
        /// </summary>
        public Dictionary<string, object> AdditionalData
        {
            get { return this.additionalData; }
            set { this.additionalData = value; }
        }

        /// <summary>
        /// 出错信息
        /// </summary>
        public string MessageTemplate
        {
            get { return this.messageTemplate; }
            set { this.messageTemplate = value; }
        }
    }
}
