using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.SOA.DocServiceContract;
using System.IO;
using System.Data;

namespace MCS.Library.Office.OpenXml.Word.Test
{
	/// <summary>
	/// 生成Word文档测试
	/// </summary>
	[TestClass]
	public class BuildDocumentTest
	{
		[TestMethod]
		[Description("根据书签与控件，填写值")]
		[TestCategory("Word")]
		public void FillDocTest()
		{
			DCTWordDataObject wdo = new DCTWordDataObject();
			wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "姓名", Value = "张三", FormatString = "" , IsReadOnly=true });
			wdo.PropertyCollection.Add(new DCTSimpleProperty() { TagID = "年龄", Value = 1234, FormatString = "###,###" });
			wdo.PropertyCollection.Add(new DCTComplexProperty()
			{
				TagID = "学历信息",
				DataObjects = new DCTWordDataObjectCollection() 
                { 
                    new DCTWordDataObject()
                    {
                         PropertyCollection=new DCTDataPropertyCollection()
                         {
                             new DCTSimpleProperty(){ TagID="毕业日期", Value=DateTime.Now, FormatString="yyyy-MM-dd",Type=typeof(DateTime).FullName},
                             new DCTSimpleProperty(){ TagID="毕业院校",Value="XX1学校", FormatString=""},
                             new DCTSimpleProperty(){ TagID="专业",Value="XX1专业",FormatString=""}
                         }
                    },
                    new DCTWordDataObject()
                    {
                         PropertyCollection=new DCTDataPropertyCollection()
                         {
                             new DCTSimpleProperty(){ TagID="毕业日期", Value=DateTime.Now, FormatString="yyyy-MM-dd"},
                             new DCTSimpleProperty(){ TagID="毕业院校",Value="XX2学校", FormatString=""},
                             new DCTSimpleProperty(){ TagID="专业",Value="XX2专业",FormatString=""}
                         }
                    },
                    new DCTWordDataObject()
                    {
                         PropertyCollection=new DCTDataPropertyCollection()
                         {
                             new DCTSimpleProperty(){ TagID="毕业日期", Value=DateTime.Now, FormatString="yyyy-MM-dd"},
                             new DCTSimpleProperty(){ TagID="毕业院校",Value="XX3学校", FormatString=""},
                             new DCTSimpleProperty(){ TagID="专业",Value="XX3专业",FormatString=""}
                         }
                    }
                }
			});

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

			DirectoryInfo rootDirectory = new DirectoryInfo(path);

			byte[] templateBinary = File.ReadAllBytes(Path.Combine(rootDirectory.Parent.Parent.FullName, "Table.docx"));

			File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FilleTemp.docx"), WordEntry.GenerateDocument(templateBinary, wdo));
		}

		[TestMethod]
		[Description("根据书签与控件，填写值")]
		[TestCategory("Word")]
		public void FillCopyPageTest()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("tag1", typeof(string));
			dt.Columns.Add("y1", typeof(int));
			dt.Columns.Add("m1", typeof(int));
			dt.Columns.Add("d1", typeof(int));
			dt.Columns.Add("y2", typeof(int));
			dt.Columns.Add("m2", typeof(int));
			dt.Columns.Add("d2", typeof(int));

			for (int i = 1; i <= 120; i++)
			{
				DataRow dr = dt.NewRow();
				dr["tag1"] = "这是一份合同";
				dr["y1"] = System.DateTime.Now.Year;
				dr["m1"] = System.DateTime.Now.Month;
				dr["d1"] = System.DateTime.Now.Month;

				dr["y2"] = System.DateTime.Now.Year + 2;
				dr["m2"] = System.DateTime.Now.Month + 2;
				dr["d2"] = System.DateTime.Now.Month + 2;

				dt.Rows.Add(dr);
			}

			SimplePropertyCollection spcollection = new SimplePropertyCollection();
			spcollection.Add(new DCTSimpleProperty() { TagID = "tag1", IsReadOnly = true });
			spcollection.Add(new DCTSimpleProperty() { TagID = "y1", IsReadOnly = true });
			spcollection.Add(new DCTSimpleProperty() { TagID = "m1", IsReadOnly = true });
			spcollection.Add(new DCTSimpleProperty() { TagID = "d1", IsReadOnly = true });

			spcollection.Add(new DCTSimpleProperty() { TagID = "y2", IsReadOnly = true });
			spcollection.Add(new DCTSimpleProperty() { TagID = "m2", IsReadOnly = true });
			spcollection.Add(new DCTSimpleProperty() { TagID = "d2", IsReadOnly = true });
		

			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
			DirectoryInfo rootDirectory = new DirectoryInfo(path);
			byte[] templateBinary = File.ReadAllBytes(Path.Combine(rootDirectory.Parent.Parent.FullName, "劳动合同续签书.docx"));
			byte[] fillBinary = WordEntry.CopyPageFillData(templateBinary, dt.DefaultView, spcollection);
			File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "劳动合同续签书.docx"), fillBinary);
		}
	}
}
