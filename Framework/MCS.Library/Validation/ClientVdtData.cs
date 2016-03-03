using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientVdtData
    {
        private string controlID = string.Empty;
        private bool isAnd = true;
        private string validateProp = "value";
        private bool clientIsHtmlElement = true;
        private bool isValidateOnblur = true;
        private bool isOnlyNum = false;
        private bool isFloat = false;
        private bool autoFormatOnBlur = true;
        private bool isValidateOnSubmit = true;
        private string formatString = string.Empty;
        private List<ClientVdtAttribute> cvtrList = new List<ClientVdtAttribute>();

        /// <summary>
        /// 客户端验证数据集合
        /// </summary>
        public List<ClientVdtAttribute> CvtList
        {
            get { return this.cvtrList; }
            set { this.cvtrList = value; }
        }

        /// <summary>
        /// 需要验证的数据对象的属性名称
        /// </summary>
        public string DataPropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// 控件ID（客户端）
        /// </summary>
        public string ControlID
        {
            get { return this.controlID; }
            set { this.controlID = value; }
        }

        /// <summary>
        /// 客户端是不是Html Element
        /// </summary>
        public bool ClientIsHtmlElement
        {
            get { return this.clientIsHtmlElement; }
            set { this.clientIsHtmlElement = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoFormatOnBlur
        {
            get { return this.autoFormatOnBlur; }
            set { this.autoFormatOnBlur = value; }
        }

        /// <summary>
        /// 是否是与的关系
        /// </summary>
        public bool IsAnd
        {
            get { return this.isAnd; }
            set { this.isAnd = value; }
        }

        /// <summary>
        /// 客户端验证属性名称
        /// </summary>
        public string ValidateProp
        {
            get { return this.validateProp; }
            set
            {
                this.validateProp = value;
            }
        }

        /// <summary>
        /// 是否在失去焦点的时候验证
        /// </summary>
        public bool IsValidateOnBlur
        {
            get { return this.isValidateOnblur; }
            set { this.isValidateOnblur = value; }
        }

        /// <summary>
        /// 是否提交是校验
        /// </summary>
        public bool IsValidateOnSubmit
        {
            get
            {
                return this.isValidateOnSubmit;
            }
            set
            {
                this.isValidateOnSubmit = value;
            }
        }

        /// <summary>
        /// 是否只允许输入数字
        /// </summary>
        public bool IsOnlyNum
        {
            get { return this.isOnlyNum; }
            set { this.isOnlyNum = value; }
        }

        /// <summary>
        /// 是否允许输入浮点型数据
        /// </summary>
        public bool IsFloat
        {
            get { return this.isFloat; }
            set { isFloat = value; }
        }

        /// <summary>
        /// 格式化输入
        /// </summary>
        public string FormatString
        {
            get { return this.formatString; }
            set { this.formatString = value; }
        }

        /// <summary>
        /// 校验时的组号，必须>=0
        /// </summary>
        public int ValidationGroup
        {
            get;
            set;
        }
    }
}
