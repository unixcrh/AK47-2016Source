using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Configuration;

namespace MCS.Library.SOA.DataObjects.Dynamics.Validators
{
    /// <summary>
    /// 校验CodeName
    /// </summary>
    internal sealed class CodeNameValidator : MCS.Library.Validation.Validator
    {
        public CodeNameValidator(string messageTemplate, string tag)
            : base(messageTemplate, tag)
        {

        }

        protected override void DoValidate(object objectToValidate, object currentObject, string key, Validation.ValidationResults validateResults)
        {
            DESchemaObjectBase doValidateObj = currentObject as DESchemaObjectBase;
            if (doValidateObj != null)
            {
                string strValue = objectToValidate.ToString();

                if (strValue.IsNotEmpty())
                {
                    bool exist = DEDynamicEntityAdapter.Instance.ExistByCodeName(strValue, DateTime.MinValue);
                    //不存在
                    if (exist == false)
                    {
                        ObjectSchemaConfigurationElement config = ObjectSchemaSettings.GetConfig().Schemas[doValidateObj.SchemaType];
                        RecordValidationResult(validateResults, string.Format(this.MessageTemplate, string.IsNullOrEmpty(config.Description) ? config.Description : config.Name, doValidateObj.Properties["Name"].StringValue, doValidateObj.ID), doValidateObj, key);
                    }
                }

            }
        }
    }

}
