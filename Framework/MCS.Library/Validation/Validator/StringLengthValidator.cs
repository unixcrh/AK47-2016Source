using System;
using System.Collections.Generic;
using System.Text;

namespace MCS.Library.Validation
{
	internal class StringLengthValidator : Validator, IClientValidatable
	{
        private int lowerBound;
        /// <summary>
        /// 下限
        /// </summary>
        public int LowerBound
        {
            get { return lowerBound; }
            set { lowerBound = value; }
        }
        private int upperBound;
        /// <summary>
        /// 上限
        /// </summary>
        public int UpperBound
        {
            get { return upperBound; }
            set { upperBound = value; }
        }
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public StringLengthValidator()
        { }
		public StringLengthValidator(int upperBound)
			: this(0, upperBound)
		{
		}

		public StringLengthValidator(int lowerBound, int upperBound)
			: this(lowerBound, upperBound, string.Empty, string.Empty)
		{
		}

		public StringLengthValidator(int lowerBound, int upperBound, string messageTemplate, string tag)
			: base(messageTemplate, tag)
		{
			this.lowerBound = lowerBound;
			this.upperBound = upperBound;
		}

		protected override internal void DoValidate(object objectToValidate,
			object currentTarget,
			string key,
			ValidationResults validationResults)
		{
			bool isValid = false;

			if (objectToValidate != null)
			{
				RangeChecker<int> checker = new RangeChecker<int>(this.lowerBound, this.upperBound);
				isValid = checker.IsInRange(objectToValidate.ToString().Length);
			}
			else
				isValid = (this.lowerBound <= 0);

			if (isValid == false)
				RecordValidationResult(validationResults, this.MessageTemplate, currentTarget, key);
		}

		/// <summary>
		/// 客户端校验函数名称
		/// </summary>
		public string ClientValidateMethodName
		{
			get { return this.GetType().Name; }
		}

		/// <summary>
		/// 获取客户端校验方法脚本
		/// </summary>
		/// <returns></returns>
		public string GetClientValidateScript()
		{
			return Properties.ScriptResources.StringLengthValidator;
		}

		/// <summary>
		/// 获取客户端校验附加数据，比如正则表达式，范围值，等等
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> GetClientValidateAdditionalData(object info)
		{
			return new Dictionary<string, object> {
									{"lowerBound", this.lowerBound},
									{"upperBound", this.upperBound}
								};
		}
	}
}
