using MCS.Library.Passport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MCS.Library.Test.CoreTest
{
    [TestClass]
    public class PermissionDescriptionTest
    {
        [TestMethod]
        public void SingleAppRoleTest()
        {
            string description = "AppCodeName:RoleCodeName";

            ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects(description);

            Assert.AreEqual(1, pods.Count);
            Assert.AreEqual("AppCodeName", pods[0].ApplicationCodeName);
            Assert.AreEqual("RoleCodeName", pods[0].PermissionObjectCodeNames[0]);
        }

        [TestMethod]
        public void SingleAppWithTwoRoleTest()
        {
            string description = "AppCodeName:RoleCodeName1,RoleCodeName2";

            ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects(description);

            Assert.AreEqual(1, pods.Count);
            Assert.AreEqual("AppCodeName", pods[0].ApplicationCodeName);
            Assert.AreEqual("RoleCodeName1", pods[0].PermissionObjectCodeNames[0]);
            Assert.AreEqual("RoleCodeName2", pods[0].PermissionObjectCodeNames[1]);
        }

        [TestMethod]
        public void TwoAppWithTwoRoleTest()
        {
            string description = "AppCodeName1:RoleCodeName1,RoleCodeName2;AppCodeName2:RoleCodeName3,RoleCodeName4";

            ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects(description);

            Assert.AreEqual(2, pods.Count);
            Assert.AreEqual("AppCodeName1", pods[0].ApplicationCodeName);
            Assert.AreEqual("RoleCodeName1", pods[0].PermissionObjectCodeNames[0]);
            Assert.AreEqual("RoleCodeName2", pods[0].PermissionObjectCodeNames[1]);

            Assert.AreEqual("AppCodeName2", pods[1].ApplicationCodeName);
            Assert.AreEqual("RoleCodeName3", pods[1].PermissionObjectCodeNames[0]);
            Assert.AreEqual("RoleCodeName4", pods[1].PermissionObjectCodeNames[1]);
        }

        [TestMethod]
        public void EmptyDescriptionTest()
        {
            ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects("");

            Assert.AreEqual(0, pods.Count);
        }

        [TestMethod]
        public void SingleSemiColonTest()
        {
            ApplicationAndPermissionObjectsCollection pods = PermissionDescriptionParser.ParseApplicationAndPermissionObjects(";");

            Assert.AreEqual(0, pods.Count);
        }

        [TestMethod]
        public void RoleDescriptionAttributeTest()
        {
            string description = "AppCodeName:RoleCodeName";

            RoleDescriptionAttribute attr = new RoleDescriptionAttribute(description);
            ApplicationAndPermissionObjectsCollection pods = attr.Parse();

            Assert.AreEqual(1, pods.Count);
            Assert.AreEqual("AppCodeName", pods[0].ApplicationCodeName);
            Assert.AreEqual("RoleCodeName", pods[0].PermissionObjectCodeNames[0]);
        }

        [TestMethod]
        public void RoleGroupsAttributeTest()
        {
            string description = "RoleGroup1,RoleGroup2; RoleGroup3";

            RoleGroupsAttribute attr = new RoleGroupsAttribute(description);
            List<string> pods = attr.Parse();

            Assert.AreEqual(3, pods.Count);
            Assert.AreEqual("RoleGroup1", pods[0]);
            Assert.AreEqual("RoleGroup2", pods[1]);
            Assert.AreEqual("RoleGroup3", pods[2]);
        }
    }
}
