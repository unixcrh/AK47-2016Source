using MCS.Library.Data.DataObjects;
using MCS.Library.OGUPermission;
using MCS.Library.Passport;
using MCS.Library.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Tenant.Test.Workflow.Helper
{
    public static class SOARoleHelper
    {
        public static SOARole PrepareSOARole(IRole originalRole)
        {
            SOARolePropertyDefinitionCollection pds = UpdateRolePropertiesDefinition(originalRole);

            SOARole role = new SOARole(originalRole.FullCodeName);

            role.Rows.Clear();

            SOARolePropertyRow row1 = new SOARolePropertyRow(role) { RowNumber = 1, OperatorType = SOARoleOperatorType.User, Operator = "fanhy,wanhw" };

            row1.Values.Add(new SOARolePropertyValue(pds["CostCenter"]) { Value = "1001" });
            row1.Values.Add(new SOARolePropertyValue(pds["PayMethod"]) { Value = "1" });
            row1.Values.Add(new SOARolePropertyValue(pds["Age"]) { Value = "30" });

            SOARolePropertyRow row2 = new SOARolePropertyRow(role) { RowNumber = 2, OperatorType = SOARoleOperatorType.User, Operator = "wangli5" };

            row2.Values.Add(new SOARolePropertyValue(pds["CostCenter"]) { Value = "1002" });
            row2.Values.Add(new SOARolePropertyValue(pds["PayMethod"]) { Value = "2" });
            row2.Values.Add(new SOARolePropertyValue(pds["Age"]) { Value = "40" });

            SOARolePropertyRow row3 = new SOARolePropertyRow(role) { RowNumber = 3, OperatorType = SOARoleOperatorType.Role, Operator = RolesDefineConfig.GetConfig().RolesDefineCollection["nestedRole"].Roles };

            row3.Values.Add(new SOARolePropertyValue(pds["CostCenter"]) { Value = "1002" });
            row3.Values.Add(new SOARolePropertyValue(pds["PayMethod"]) { Value = "2" });
            row3.Values.Add(new SOARolePropertyValue(pds["Age"]) { Value = "60" });

            role.Rows.Add(row1);
            role.Rows.Add(row2);
            role.Rows.Add(row3);

            SOARolePropertiesAdapter.Instance.Update(role);

            return role;
        }

        public static SOARolePropertyDefinitionCollection PreparePropertiesDefinition(IRole role)
        {
            SOARolePropertyDefinitionCollection propertiesDefinition = new SOARolePropertyDefinitionCollection();

            propertiesDefinition.Add(new SOARolePropertyDefinition(role) { Name = "CostCenter", SortOrder = 0 });
            propertiesDefinition.Add(new SOARolePropertyDefinition(role) { Name = "PayMethod", SortOrder = 1 });
            propertiesDefinition.Add(new SOARolePropertyDefinition(role) { Name = "Age", SortOrder = 2, DataType = ColumnDataType.Integer });
            propertiesDefinition.Add(new SOARolePropertyDefinition(role) { Name = "OperatorType", SortOrder = 3, DataType = ColumnDataType.String });
            propertiesDefinition.Add(new SOARolePropertyDefinition(role) { Name = "Operator", SortOrder = 4, DataType = ColumnDataType.String });

            return propertiesDefinition;
        }

        private static SOARolePropertyDefinitionCollection UpdateRolePropertiesDefinition(IRole role)
        {
            SOARolePropertyDefinitionCollection propertiesDefinition = PreparePropertiesDefinition(role);

            SOARolePropertyDefinitionAdapter.Instance.Update(role, propertiesDefinition);

            return propertiesDefinition;
        }
    }
}
