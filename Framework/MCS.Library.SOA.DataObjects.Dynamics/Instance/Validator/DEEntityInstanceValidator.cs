using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Validation;
using MCS.Library.SOA.DataObjects.Dynamics.Validators;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Validator
{
    public class DEEntityInstanceValidator : MCS.Library.Validation.Validator
    {
        /// <summary>
        /// 执行校验
        /// </summary>
        /// <param name="objectToValidate">要进行校验的对象</param>
        /// <param name="currentObject">当前的对象</param>
        /// <param name="key">键</param>
        /// <param name="validateResults">校验结果</param>
        protected override void DoValidate(object objectToValidate, object currentObject, string key, ValidationResults validateResults)
        {
            List<MCS.Library.Validation.Validator> innerValidators = new List<Validation.Validator>();

            //添加校验器
            innerValidators.Add(new DEEntityInstanceLengthValidator("'{0}':'长度没有通过验证'"));
            innerValidators.Add(new DEEntityInstanceTypeValidator("'{0}':'值类型没有通过验证'"));



            //如果已经存在SchemaPropertyValidatorContext则直接进行校验，否则自己创造一个上下文。
            if (DESchemaPropertyValidatorContext.ExistsInContext)
            {
                innerValidators.ForEach(v => v.Validate(objectToValidate, validateResults));
            }
            else
            {
                DEInstancePropertyValidatorContext.Current.DoActions(() =>
                {
                    DEInstancePropertyValidatorContext.Current.Container = (DEEntityInstance)objectToValidate;

                    innerValidators.ForEach(v => v.Validate(objectToValidate, validateResults));
                });
            }
        }
    }
}
