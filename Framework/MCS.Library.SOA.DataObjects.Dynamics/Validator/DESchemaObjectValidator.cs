using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;

namespace MCS.Library.SOA.DataObjects.Dynamics.Validators
{
	/// <summary>
	/// 表示模式对象的校验器
	/// </summary>
	public class DESchemaObjectValidator : Validator
	{
		/// <summary>
		/// 初始化<see cref="DESchemaObjectValidator"/>的新实例。
		/// </summary>
		public DESchemaObjectValidator()
		{
		}

		/// <summary>
		/// 执行校验
		/// </summary>
		/// <param name="objectToValidate">要进行校验的对象</param>
		/// <param name="currentObject">当前的对象</param>
		/// <param name="key">键</param>
		/// <param name="validateResults">校验结果</param>
		protected override void DoValidate(object objectToValidate, object currentObject, string key, ValidationResults validateResults)
		{
			List<Validator> innerValidators = GenerateValidators((DESchemaObjectBase)objectToValidate);

			//如果已经存在SchemaPropertyValidatorContext则直接进行校验，否则自己创造一个上下文。
			if (DESchemaPropertyValidatorContext.ExistsInContext)
			{
				innerValidators.ForEach(v => v.Validate(objectToValidate, validateResults));
			}
			else
			{
				DESchemaPropertyValidatorContext.Current.DoActions(() =>
				{
					DESchemaPropertyValidatorContext.Current.Container = (DESchemaObjectBase)objectToValidate;

					innerValidators.ForEach(v => v.Validate(objectToValidate, validateResults));
				});
			}
		}

		private List<Validator> GenerateValidators(DESchemaObjectBase obj)
		{
			List<Validator> result = new List<Validator>();

			foreach (SchemaPropertyValue pv in obj.Properties)
			{
				foreach (Validator v in pv.Validators)
				{
					ValueAccessValidator validator = new ValueAccessValidator(new DESchemaPropertyValueAccess(pv), v, pv.Definition.Name);

					result.Add(validator);
				}
			}

			return result;
		}
	}
}
