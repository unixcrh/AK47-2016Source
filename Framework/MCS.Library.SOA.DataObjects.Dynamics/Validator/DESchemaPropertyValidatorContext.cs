using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;

namespace MCS.Library.SOA.DataObjects.Dynamics.Validators
{
	/// <summary>
	/// Schema对象属性验证器的容器对象
	/// </summary>
	[ActionContextDescription(Key = "DESchemaPropertyValidatorContext")]
	public class DESchemaPropertyValidatorContext : ActionContextBase<DESchemaPropertyValidatorContext>
	{
		public DESchemaPropertyValidatorContext()
		{
		}

		/// <summary>
		/// Schema对象的属性验证器所涉及到的对象
		/// </summary>
		public DESchemaObjectBase Target
		{
			get;
			internal set;
		}

		/// <summary>
		/// Schema对象的属性验证器所涉及到的容器对象
		/// </summary>
		public DESchemaObjectBase Container
		{
			get;
			internal set;
		}
	}
}
