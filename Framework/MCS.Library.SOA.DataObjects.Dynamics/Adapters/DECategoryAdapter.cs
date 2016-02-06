using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Net.SNTP;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using System;
using System.Data;
using System.Linq;
namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    public class CategoryAdapter : TypeEntityBAdapterBase<DECategory, CategoryCollection>
    {
        public static readonly CategoryAdapter Instance = new CategoryAdapter();

        private CategoryAdapter() { }

        protected override string GetConnectionName()
        {
            return DEConnectionDefine.DBConnectionName;
        }


        /// <summary>
        /// 获取根
        /// </summary>
        /// <returns></returns>
        public DECategory GetRoot()
        {
            var result = new DECategory();
            result = this.LoadCurrentData(p => { p.AppendItem("Level", 0); p.AppendItem("Status", 1); }).SingleOrDefault();
            return result;
        }

        private DECategory getCategoryByID(string id)
        {
            id.CheckStringIsNullOrEmpty<ArgumentNullException>("id");

            return this.LoadCurrentData(p =>
            {
                p.AppendItem("Code", id);
                p.AppendItem("Status", 1);
            }).FirstOrDefault();
        }

        public DECategory getCategoryByDisplayName(string dn, string parentCode)
        {
            dn.CheckStringIsNullOrEmpty<ArgumentNullException>("dn");

            return this.LoadCurrentData(p =>
            {
                p.AppendItem("DisplayName", dn);
                p.AppendItem("ParentCode", parentCode);
            }).FirstOrDefault();
        }

        public bool Exists(string id)
        {
            id.CheckStringIsNullOrEmpty<ArgumentNullException>("id");

            DECategory category = getCategoryByID(id);

            return category != null;
        }

        public DECategory GetByID(string id)
        {
            id.CheckStringIsNullOrEmpty<ArgumentNullException>("id");

            DECategory category = getCategoryByID(id);

            ExceptionHelper.TrueThrow<ArgumentNullException>(category == null, string.Format("不能找到编码为{0}的分类！", id));

            return category;
        }

        /// <summary>
        /// 根据父节点获取子节点集合
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public CategoryCollection GetByParentCode(string parentCode)
        {
            parentCode.CheckStringIsNullOrEmpty<ArgumentNullException>("parentCode");
            var result = new CategoryCollection();
            result = this.LoadCurrentData(p => { p.AppendItem("ParentCode", parentCode); p.AppendItem("Status", 1); });
            return result;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parentCode"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="level"></param>
        /// <param name="fullPath"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public DateTime AddData(string code, string parentCode, string displayName, string description, string level, string fullPath, string creator)
        {
            DECategory addData = new DECategory();
            //父id
            addData.ParentCode = parentCode;
            //显示名称
            addData.DisplayName = displayName;
            //描述
            addData.Desc = description;
            //版本开始时间
            addData.VersionStartTime = SNTPClient.AdjustedTime;
            //id
            addData.Code = code;
            //状态
            addData.Status = "1";
            //全路径
            addData.FullPath = fullPath;
            //层级
            addData.Level = level;
            //创建时间
            addData.CreateTime = SNTPClient.AdjustedTime;
            //创建者
            addData.Creator = creator;
            return this.UpdateCurrentData(addData, DateTime.MinValue);
        }

        public DateTime updataNode(string code, string parentCode, string displayName, string description, WhereSqlClauseBuilder sqlWhere, string creator, string level, string fullPath)
        {

            DECategory addData = new DECategory();
            //id
            addData.Code = code;
            //父id
            addData.ParentCode = parentCode;
            //显示名称
            addData.DisplayName = displayName;
            //描述
            addData.Desc = description;
            //全路径
            addData.FullPath = fullPath;
            //层级
            addData.Level = level;
            //创建时间
            addData.CreateTime = SNTPClient.AdjustedTime;
            //创建者
            addData.Creator = creator;

            return this.UpdateData(addData, SNTPClient.AdjustedTime, sqlWhere);
        }
    }
}
