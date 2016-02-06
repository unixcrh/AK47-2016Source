using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;

namespace MCS.Library.SOA.DataObjects.Dynamics.Instance.ValueDefine
{
    /// <summary>
	/// 属性值的存取器
	/// </summary>
    public interface IFieldValueAccessor
	{
		string StringValue
		{
			get;
			set;
		}

        DynamicEntityField Definition
		{
			get;
			set;
		}
	}

    public interface IDefinitionFields<T> where T : IFieldValueAccessor
	{
		SerializableEditableKeyedDataObjectCollectionBase<string, T> Fields { get; }
	}
}
