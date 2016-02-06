using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Enums;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Common;
using MCS.Library.SOA.DataObjects.Dynamics.Test.Mock;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.Validation;
using MCS.Web.Library.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MCS.Library.SOA.DataObjects.Dynamics.Test.Objects
{
    /// <summary>
    /// EntityObjectTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ValidatorDefineTest
    {
        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义生成整数范围校验校测试")]
        public void CreateIntegerRangeValidatorTest()
        {
            ValidatorDefine define = new ValidatorDefine();
            define.ValidatorName = "IntegerRangeValidator";
            define.Description = "整数范围校验";
            define.ValidatorType = "MCS.Library.Validation.IntegerRangeValidator, MCS.Library";
            define.Parameters.Add(new ValidatorParameter("lowerBound", "0", PropertyDataType.Integer));
            define.Parameters.Add(new ValidatorParameter("upperBound", "10", PropertyDataType.Integer));
            define.Parameters.Add(new ValidatorParameter("messageTemplate", "值必须在0-10之间", PropertyDataType.String));
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.IntegerRangeValidator");
            Assert.AreEqual(validator.MessageTemplate, "值必须在0-10之间");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义JSON生成整数范围校验校测试")]
        public void CreateIntegerRangeValidatorByJSONTest()
        {
            string json = "{\"ValidatorName\":\"IntegerRangeValidator\",\"Description\":\"整数范围校验\",\"ValidatorType\":\"MCS.Library.Validation.IntegerRangeValidator, MCS.Library\",\"Parameters\":[{\"Name\":\"lowerBound\",\"ParamValue\":\"0\",\"DataType\":9},{\"Name\":\"upperBound\",\"ParamValue\":\"10\",\"DataType\":9},{\"Name\":\"messageTemplate\",\"ParamValue\":\"值必须在0-10之间\",\"DataType\":18}]}";
            ValidatorDefine define = JSONSerializerExecute.Deserialize<ValidatorDefine>(json);

            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.IntegerRangeValidator");
            Assert.AreEqual(validator.MessageTemplate, "值必须在0-10之间");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("生成字符串为空校验器测试")]
        public void CreateStringEmptyValidatorTest()
        {
            ValidatorDefine define = new ValidatorDefine();
            define.ValidatorName = "StringEmptyValidator";
            define.Description = "字符串为空校验";
            define.ValidatorType = "MCS.Library.Validation.StringEmptyValidator, MCS.Library";
            define.Parameters.Add(new ValidatorParameter("messageTemplate", "字符串不能为空", PropertyDataType.String));
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.StringEmptyValidator");
            Assert.AreEqual(validator.MessageTemplate, "字符串不能为空");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义JSON生成字符串为空校验器测试")]
        public void CreateStringEmptyValidatorByJSONTest()
        {
            string json = "{\"ValidatorName\":\"StringEmptyValidator\",\"Description\":\"字符串为空校验\",\"ValidatorType\":\"MCS.Library.Validation.StringEmptyValidator, MCS.Library\",\"Parameters\":[{\"Name\":\"messageTemplate\",\"ParamValue\":\"字符串不能为空\",\"DataType\":18}]}";
            ValidatorDefine define = JSONSerializerExecute.Deserialize<ValidatorDefine>(json);
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.StringEmptyValidator");
            Assert.AreEqual(validator.MessageTemplate, "字符串不能为空");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义生成字符串长度校验器测试")]
        public void CreateStringLengthValidatorTest()
        {
            ValidatorDefine define = new ValidatorDefine();
            define.ValidatorName = "StringLengthValidator";
            define.Description = "字符串长度校验";
            define.ValidatorType = "MCS.Library.Validation.StringLengthValidator, MCS.Library";
            define.Parameters.Add(new ValidatorParameter("lowerBound", "0", PropertyDataType.Integer));
            define.Parameters.Add(new ValidatorParameter("upperBound", "10", PropertyDataType.Integer));
            define.Parameters.Add(new ValidatorParameter("messageTemplate", "字符串长度必须在0-10之间", PropertyDataType.String));
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.StringLengthValidator");
            
            Assert.AreEqual(validator.MessageTemplate, "字符串长度必须在0-10之间");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义JSON生成字符串长度校验测试")]
        public void CreateStringLengthValidatorByJSONTest()
        {
            string json = "{\"ValidatorName\":\"StringLengthValidator\",\"Description\":\"字符串长度校验\",\"ValidatorType\":\"MCS.Library.Validation.StringLengthValidator, MCS.Library\",\"Parameters\":[{\"Name\":\"lowerBound\",\"ParamValue\":\"0\",\"DataType\":9},{\"Name\":\"upperBound\",\"ParamValue\":\"10\",\"DataType\":9},{\"Name\":\"messageTemplate\",\"ParamValue\":\"字符串长度必须在0-10之间\",\"DataType\":18}]}";
            ValidatorDefine define = JSONSerializerExecute.Deserialize<ValidatorDefine>(json);

            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.StringLengthValidator");
            Assert.AreEqual(validator.MessageTemplate, "字符串长度必须在0-10之间");
        }


        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义生成正则校验器测试")]
        public void CreateRegexValidatorTest()
        {
            ValidatorDefine define = new ValidatorDefine();
            define.ValidatorName = "RegexValidator";
            define.Description = "正则校验";
            define.ValidatorType = "MCS.Library.Validation.RegexValidator, MCS.Library";
            define.Parameters.Add(new ValidatorParameter("Pattern", "/^(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w{2,3}){1,3})$/", PropertyDataType.String));
            define.Parameters.Add(new ValidatorParameter("messageTemplate", "电子邮箱格式不正确", PropertyDataType.String));
            //string json = JSONSerializerExecute.Serialize(define,typeof(ValidatorDefine));
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.RegexValidator");

            Assert.AreEqual(validator.MessageTemplate, "电子邮箱格式不正确");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义JSON生成正则校验器测试")]
        public void CreateRegexValidatorByJSONTest()
        {
            string json = "{\"ValidatorName\":\"RegexValidator\",\"Description\":\"正则校验\",\"ValidatorType\":\"MCS.Library.Validation.RegexValidator, MCS.Library\",\"Parameters\":[{\"Name\":\"Pattern\",\"ParamValue\":\"/^(\\\\w)+(\\\\.\\\\w+)*@(\\\\w)+((\\\\.\\\\w{2,3}){1,3})$/\",\"DataType\":18},{\"Name\":\"messageTemplate\",\"ParamValue\":\"电子邮箱格式不正确\",\"DataType\":18}]}";
            ValidatorDefine define = JSONSerializerExecute.Deserialize<ValidatorDefine>(json);

            Validator validator = define.ToValidator();
            
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.RegexValidator");
            Assert.AreEqual(validator.MessageTemplate, "电子邮箱格式不正确");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义生成日期为空校验器测试")]
        public void CreateDateTimeEmptyValidatorTest()
        {
            ValidatorDefine define = new ValidatorDefine();
            define.ValidatorName = "DateTimeEmptyValidator";
            define.Description = "日期为空校验";
            define.ValidatorType = "MCS.Library.Validation.DateTimeEmptyValidator, MCS.Library";
            define.Parameters.Add(new ValidatorParameter("messageTemplate", "日期不能为空", PropertyDataType.String));
            //string json = JSONSerializerExecute.Serialize(define,typeof(ValidatorDefine));
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.DateTimeEmptyValidator");

            Assert.AreEqual(validator.MessageTemplate, "日期不能为空");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义JSON生成日期为空校验器测试")]
        public void CreateDateTimeEmptyValidatorByJSONTest()
        {
            string json = "{\"ValidatorName\":\"DateTimeEmptyValidator\",\"Description\":\"日期为空校验\",\"ValidatorType\":\"MCS.Library.Validation.DateTimeEmptyValidator, MCS.Library\",\"Parameters\":[{\"Name\":\"messageTemplate\",\"ParamValue\":\"日期不能为空\",\"DataType\":18}]}";
            ValidatorDefine define = JSONSerializerExecute.Deserialize<ValidatorDefine>(json);

            Validator validator = define.ToValidator();

            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.DateTimeEmptyValidator");
            Assert.AreEqual(validator.MessageTemplate, "日期不能为空");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义生成日期范围校验器测试")]
        public void CreateDateTimeRangeValidatorTest()
        {
            ValidatorDefine define = new ValidatorDefine();
            define.ValidatorName = "DateTimeRangeValidator";
            define.Description = "日期范围校验";
            define.ValidatorType = "MCS.Library.Validation.DateTimeRangeValidator, MCS.Library";
            define.Parameters.Add(new ValidatorParameter("lowerBound", "2015-08-01", PropertyDataType.DateTime));
            define.Parameters.Add(new ValidatorParameter("upperBound", "2015-08-10", PropertyDataType.DateTime));
            define.Parameters.Add(new ValidatorParameter("messageTemplate", "日期范围必须在2015-08-01至2015-08-10之间", PropertyDataType.String));
            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.DateTimeRangeValidator");

            Assert.AreEqual(validator.MessageTemplate, "日期范围必须在2015-08-01至2015-08-10之间");
        }

        [TestCategory("ValidatorDefine"), TestMethod]
        [Description("根据校验器定义JSON生成日期范围校验器测试")]
        public void CreateDateTimeRangeValidatorByJSONTest()
        {
            string json = "{\"ValidatorName\":\"DateTimeRangeValidator\",\"Description\":\"日期范围校验\",\"ValidatorType\":\"MCS.Library.Validation.DateTimeRangeValidator, MCS.Library\",\"Parameters\":[{\"Name\":\"lowerBound\",\"ParamValue\":\"2015-08-01\",\"DataType\":16},{\"Name\":\"upperBound\",\"ParamValue\":\"2015-08-10\",\"DataType\":16},{\"Name\":\"messageTemplate\",\"ParamValue\":\"日期范围必须在2015-08-01至2015-08-10之间\",\"DataType\":18}]}";
            ValidatorDefine define = JSONSerializerExecute.Deserialize<ValidatorDefine>(json);

            Validator validator = define.ToValidator();
            Assert.AreEqual(validator.GetType().FullName, "MCS.Library.Validation.DateTimeRangeValidator");
            Assert.AreEqual(validator.MessageTemplate, "日期范围必须在2015-08-01至2015-08-10之间");
        }
    }
}
