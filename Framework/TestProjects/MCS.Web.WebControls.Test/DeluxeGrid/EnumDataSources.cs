using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data.DataObjects;
using System.Data;

namespace MCS.Web.WebControls.Test.DeluxeGrid
{
	public class EnumDataEntity
	{
		public string ID { get; set; }
		public SomeEnum Result { get; set; }

	}

	public class EnumDataEntityCollection : EditableKeyedDataObjectCollectionBase<string, EnumDataEntity>
	{
		#region 缓存的数据
		static EnumDataEntityCollection cachedCollection;
		static DataTable cachedTable;
		static DataTable cachedTable2;

		public static EnumDataEntityCollection GetCachedCollection()
		{
			if (cachedCollection == null)
			{
				cachedCollection = Generate();
			}

			return cachedCollection;
		}

		public static System.Data.DataTable GetCachedTable()
		{

			if (cachedTable == null)
			{
				cachedTable = Generate().ToDataTable();
			}

			return cachedTable;
		}

		public static System.Data.DataTable GetCachedTable2()
		{
			if (cachedTable2 == null)
			{
				cachedTable2 = Generate().ToDataTable2();
			}

			return cachedTable2;
		}
		#endregion

		protected override string GetKeyForItem(EnumDataEntity item)
		{
			return item.ID;
		}

		public void Update(EnumDataEntity entity)
		{
			this[entity.ID].Result = entity.Result;
		}

		public static void UpdateCachedEntity(EnumDataEntity entity)
		{
			GetCachedCollection()[entity.ID].Result = entity.Result;
		}

		public static void UpdateCachedRow(string id, int result)
		{
			DataTable dt = GetCachedTable();
			DataRow row = dt.Rows.Find(id);
			if (row != null)
			{
				row["Result"] = result;
				row.AcceptChanges();
			}
		}

		public static void UpdateCachedRow2(string id, string result)
		{
			DataTable dt = GetCachedTable2();
			DataRow row = dt.Rows.Find(id);
			if (row != null)
			{
				row["Result"] = result;
				row.AcceptChanges();
			}
		}

		public static EnumDataEntityCollection Generate()
		{
			EnumDataEntityCollection set = new EnumDataEntityCollection();
			Random rnd = new Random();
			for (int i = 0; i < 999; i++)
			{
				set.Add(new EnumDataEntity() { ID = i.ToString(), Result = (SomeEnum)(i % 5) });
			}

			return set;
		}

		public System.Data.DataTable ToDataTable()
		{
			System.Data.DataTable tb = new System.Data.DataTable("data");
			tb.Columns.Add("ID");
			tb.Columns.Add("Result", typeof(int));
			tb.PrimaryKey = new DataColumn[] { tb.Columns[0] };

			foreach (EnumDataEntity item in this)
			{
				tb.Rows.Add(item.ID, (int)item.Result);
			}

			return tb;
		}

		public System.Data.DataTable ToDataTable2()
		{
			System.Data.DataTable tb = new System.Data.DataTable("data");
			tb.Columns.Add("ID");
			tb.Columns.Add("Result");
			tb.PrimaryKey = new DataColumn[] { tb.Columns[0] };

			foreach (EnumDataEntity item in this)
			{
				tb.Rows.Add(item.ID, item.Result);
			}

			return tb;
		}
	}


}