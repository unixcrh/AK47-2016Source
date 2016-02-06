using MCS.Library.SOA.DocServiceContract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Office.OpenXml.Word.Test
{
    [TestClass]
    public class ProjectInfermation
    {
        [TestMethod]
        [Description("根据书签与控件，填写值")]
        [TestCategory("Word")]
        public void WriteProject()
        {
            string[] txtID = new string[] { 
                                            "tgProjectName", "tgProjectType", "tgCoustomerName", "tgParkName",
                                            "tgIndustrialParkName","tgSector","tgOwnerName","tgOwnerDeptName",
                                            "tgIsDiversion","tgMarketingActivityName","tgDescription","tgIsKeyProject",
                                            "tgProjectEndStatus", "tgCreatorName","tgCreateTime","tgCreatorDeptName",
                                            "tgPhaseName","tgPhaseStatus","tgPhaseCreatorName","tgPhaseCreateTime"
                                           };
            DataTable dt = new DataTable();
            for (int i = 0; i < txtID.Length; i++)
            {
                dt.Columns.Add(txtID[i]);
            }

            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dr[dt.Columns[i].ColumnName] = dt.Columns[i].ColumnName;
            }
            dt.Rows.Add(dr);

            SimplePropertyCollection spcollection = new SimplePropertyCollection();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                spcollection.Add(new DCTSimpleProperty() { TagID = dt.Columns[i].ColumnName, IsReadOnly = true });
            }

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo rootDirectory = new DirectoryInfo(path);
            byte[] templateBinary = File.ReadAllBytes(Path.Combine(rootDirectory.Parent.Parent.FullName, "项目信息.docx"));
            byte[] fillBinary = WordEntry.CopyPageFillData(templateBinary, dt.DefaultView, spcollection);
            File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "项目信息.docx"), fillBinary);
        }
    }
}
