using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Core;

namespace MCS.Web.WebControls.Test.DeluxeGrid
{
	public enum SomeEnum
	{
		[EnumItemDescription("啥也没有")]
		None = 0,
		[EnumItemDescription("第一个")]
		One = 1,
		[EnumItemDescription("第二个")]
		Two = 2,
		[EnumItemDescription("第三个")]
		Three = 3,
		[EnumItemDescription("第四个")]
		Four = 4,
	}
}