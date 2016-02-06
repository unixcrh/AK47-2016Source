using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.Validator
{
    /// <summary>
    /// 实体实例长度校验器
    /// </summary>
    public class DEEntityInstanceLengthValidator : MCS.Library.Validation.Validator
    {
        public DEEntityInstanceLengthValidator(string template)
            : base(template)
        { }

        private DEEntityInstanceLengthValidator()
        { }

        protected override void DoValidate(object objectToValidate, object currentObject, string key, Validation.ValidationResults validateResults)
        {
            DEEntityInstance instanceData = objectToValidate as DEEntityInstance;
            instanceData.Fields.ForEach(f =>
            {
                if (f.Definition.FieldType == Enums.FieldTypeEnum.Collection)
                {
                    DEEntityInstanceBaseCollection childEntities = f.GetRealValue() as DEEntityInstanceBaseCollection;
                    if (childEntities.Count > f.Definition.Length)
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
                    if ((((f.Definition.FieldType == Enums.FieldTypeEnum.Bool) && (f.Definition.Length != 1))
                        || ((f.Definition.FieldType != Enums.FieldTypeEnum.Bool) && (f.StringValue.Length > f.Definition.Length))
                        ) && (f.Definition.FieldType != Enums.FieldTypeEnum.DateTime)
                        )
                    {
                        RecordValidationResult(validateResults,
                            string.Format(this.MessageTemplate, f.Definition.ID),
                            instanceData,
                            key);
                    }
                }
            });
        }
    }
}
