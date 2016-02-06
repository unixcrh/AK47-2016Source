using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Validator
{
    /// <summary>
    /// 实体实例类型校验器
    /// </summary>
    class DEEntityInstanceTypeValidator : MCS.Library.Validation.Validator
    {
        private DEEntityInstanceTypeValidator()
        {
        }

        public DEEntityInstanceTypeValidator(string template)
            : base(template)
        {
        }

        /// <summary>
        /// 验证实体实例的值得类型是否合法
        /// </summary>
        /// <param name="objectToValidate"></param>
        /// <param name="currentObject"></param>
        /// <param name="key"></param>
        /// <param name="validateResults"></param>
        protected override void DoValidate(object objectToValidate, object currentObject, string key, Validation.ValidationResults validateResults)
        {
            DEEntityInstance instanceData = objectToValidate as DEEntityInstance;
            instanceData.Fields.ForEach(f =>
            {
                //字段的值
                object fieldValue = f.GetRealValue();

                if (f.Definition.FieldType == Enums.FieldTypeEnum.Collection)
                {
                    DEEntityInstanceBaseCollection childEntities = f.GetRealValue() as DEEntityInstanceBaseCollection;
                    if (fieldValue is string)
                    {
                        //"实体实例'{0}'(实体的ID：{1})的字段'{2}'长度没有通过验证"
                        RecordValidationResult(validateResults,
                            string.Format(this.MessageTemplate, f.Definition.ID),
                            instanceData,
                            key);
                    }
                    childEntities.ForEach(ce =>
                    {
                        ce.Validate();
                    });
                }
                else
                {
                    
                    //添加验证，调用值转换函数
                    if (!IsOk(f.Definition.FieldType.ToString(), f.StringValue))
                    {
                        RecordValidationResult(validateResults,
                            string.Format(this.MessageTemplate, f.Definition.ID),
                            instanceData,
                            key);
                    }
                    //if (f.StringValue is string && f.Definition.FieldType != Enums.FieldTypeEnum.String)
                    //{
                    //    RecordValidationResult(validateResults,
                    //        string.Format(this.MessageTemplate, instanceData.Name, instanceData.ID, f.Definition.Name),
                    //        instanceData,
                    //        key);
                    //}
                }
            });
        }
        private bool IsOk(string fieldType, string value)
        {
            bool isOk = true;
            try
            {
                switch (fieldType.ToLower())
                {
                    case "datetime":
                        var resultD = Convert.ToDateTime(value);
                        break;
                    case "int":
                        var resultI = Convert.ToInt32(value);
                        break;
                    case "bool":
                        var resultB = Convert.ToBoolean(value);
                        break;
                    case "decimal":
                        var resultde = Convert.ToDecimal(value);
                        break;
                }
            }
            catch (Exception)
            {
                isOk = false;
            }

            return isOk;
            
        }
    }
}
