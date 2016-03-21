using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Adapters;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class ConstantAdapterTest
    {
        [TestMethod]
        public void LoadConstantTest()
        {
            ConstantEntityInCategoryCollection entities = ConstantAdapter.Instance.GetByCategory("C_CODE_ABBR_STUDENTBRANCH", false);

            Assert.IsTrue(entities.Count > 0);
        }
    }
}
