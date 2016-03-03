using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Passport
{
    /// <summary>
    /// 授权对象描述，应用的描述加上附加权限对象描述
    /// </summary>
    [Serializable]
    public class ApplicationAndPermissionObjects
    {
        private List<string> permissionObjectCodeNames = null;

        /// <summary>
        /// 
        /// </summary>
        public ApplicationAndPermissionObjects()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appCodeName"></param>
        public ApplicationAndPermissionObjects(string appCodeName)
        {
            this.ApplicationCodeName = appCodeName;
        }

        /// <summary>
        /// 应用的代码名称
        /// </summary>
        public string ApplicationCodeName
        {
            get;
            set;
        }

        /// <summary>
        /// 权限对象描述集合
        /// </summary>
        public List<string> PermissionObjectCodeNames
        {
            get
            {
                if (this.permissionObjectCodeNames == null)
                    this.permissionObjectCodeNames = new List<string>();

                return this.permissionObjectCodeNames;
            }
        }
    }

    /// <summary>
    /// 授权对象描述的集合
    /// </summary>
    [Serializable]
    public class ApplicationAndPermissionObjectsCollection : SerializableEditableKeyedDataObjectCollectionBase<string, ApplicationAndPermissionObjects>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ApplicationAndPermissionObjectsCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 序列化相关的构造方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ApplicationAndPermissionObjectsCollection(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override string GetKeyForItem(ApplicationAndPermissionObjects item)
        {
            return item.ApplicationCodeName;
        }
    }
}
