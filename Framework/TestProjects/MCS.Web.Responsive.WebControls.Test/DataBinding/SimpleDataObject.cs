using MCS.Library.Core;
using MCS.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.Responsive.WebControls.Test.DataBinding
{
    public enum DataType
    {
        [EnumItemDescription("空")]
        Non = 0,
        [EnumItemDescription("整型")]
        Int = 1,
        [EnumItemDescription("串")]
        String = 2,

    }

    public class SimpleDataObject
    {
        [StringEmptyValidator(MessageTemplate = "StringInput不能为空")]
        public string StringInput
        {
            get;
            set;
        }

        //[DateTimeEmptyValidator(MessageTemplate = "DateInput不能为空")]
        //[DateTimeRangeValidator("2011-11-17", "2011-11-18", MessageTemplate = "日期范围不正确")]
        public DateTime DateInput
        {
            get;
            set;
        }

        //[IntegerRangeValidator(1, 20, MessageTemplate = "请输入1-20之间的整数")]
        //[StringByteLengthValidator(2, 5, MessageTemplate = "字节范围2-5")]
        //[RegexValidator(@"^\d{3,4}$", MessageTemplate = "IntegerInput格式不正确")]
        public int IntegerInput
        {
            get;
            set;
        }

        //[NotNullValidator(MessageTemplate = "不能为空")]
        //[ObjectNullValidator(MessageTemplate = "NullableFloat不能为空")]
        public float? NullableFloat
        {
            get;
            set;
        }

        //[EnumDefaultValueValidator(MessageTemplate = "不能默认值")]
        public DataType SimpleDataType { get; set; }


        // [EnumDefaultValueValidator(MessageTemplate = "不能默认值")]
        public SimpleUserCollection Users
        {
            get;
            set;
        }

        public static SimpleDataObject PrepareData()
        {
            SimpleDataObject data = new SimpleDataObject();

            data.StringInput = "Test Object";

            return data;
        }
    }
}