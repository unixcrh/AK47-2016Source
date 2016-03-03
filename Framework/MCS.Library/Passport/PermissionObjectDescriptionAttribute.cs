using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Passport
{
    /// <summary>
    /// 权限描述的特性
    /// </summary>
    public abstract class PermissionObjectDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionObjectDescriptionAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        public PermissionObjectDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// 权限信息的描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 分析出一个结构化数据
        /// </summary>
        /// <returns></returns>
        public ApplicationAndPermissionObjectsCollection Parse()
        {
            return PermissionDescriptionParser.ParseApplicationAndPermissionObjects(this.Description);
        }
    }

    /// <summary>
    /// 角色描述的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Struct, Inherited = true)]
    public class RoleDescriptionAttribute : PermissionObjectDescriptionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public RoleDescriptionAttribute()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        public RoleDescriptionAttribute(string description)
            : base(description)
        {
        }
    }

    /// <summary>
    /// 权限描述的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Struct, Inherited = true)]
    public class PermissionDescriptionAttribute : PermissionObjectDescriptionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionDescriptionAttribute()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        public PermissionDescriptionAttribute(string description)
            : base(description)
        {
        }
    }

    /// <summary>
    /// 角色组的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Struct, Inherited = true)]
    public class RoleGroupsAttribute : Attribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public RoleGroupsAttribute()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="groups"></param>
        public RoleGroupsAttribute(string groups)
        {
            this.Groups = groups;
        }

        /// <summary>
        /// 组的名称描述，以逗号和分号分隔
        /// </summary>
        public string Groups
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> Parse()
        {
            List<string> result = new List<string>();

            if (this.Groups.IsNotEmpty())
            {
                string[] groups = this.Groups.Split(';', ',', '，', '；');

                foreach (string group in groups)
                {
                    string trimmedGroup = group.Trim();

                    if (trimmedGroup.IsNotEmpty())
                        result.Add(trimmedGroup);
                }
            }

            return result;
        }
    }
}
