using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MCS.Web.Responsive.WebControls
{
    public class DateTimePickerDataPackConverter : JavaScriptConverter
    {
        private static readonly Type[] _SupportedTypes = new Type[] { typeof(DateTimePickerDataPack) };

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            DateTimePickerDataPack data = new DateTimePickerDataPack();

            data.Mode = DictionaryHelper.GetValue(dictionary, "Mode", DateTimePickerMode.DateTimePicker);
            DateTime originalTime = DictionaryHelper.GetValue(dictionary, "Value", DateTime.MinValue);

            data.Value = TimeZoneContext.Current.ConvertLocalTimeToCurrent(originalTime);

            return data;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DateTimePickerDataPack data = (DateTimePickerDataPack)obj;

            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            dictionary.Add("Mode", data.Mode);

            DateTime convertedTime = TimeZoneContext.Current.ConvertTimeToUtc(data.Value);
            dictionary.Add("Value", convertedTime);

            return dictionary;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return _SupportedTypes; }
        }
    }
}
