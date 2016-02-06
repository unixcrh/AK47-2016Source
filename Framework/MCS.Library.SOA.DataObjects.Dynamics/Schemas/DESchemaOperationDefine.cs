using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects.Dynamics.Executors;
using MCS.Library.SOA.DataObjects.Schemas.Configuration;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Schemas
{
	/// <summary>
	/// 表示模式操作的定义
	/// </summary>
	[Serializable]
	public class DESchemaOperationDefine
	{            
		/// <summary>
		/// 初始化<see cref="DESchemaOperationDefine"/>的新实例
		/// </summary>
		public DESchemaOperationDefine()
		{
		}

		/// <summary>
		/// 根据指定的配置元素 初始化<see cref="DESchemaOperationDefine"/>的新实例
		/// </summary>
		/// <param name="element"><see cref="ObjectSchemaOperationElement"/>对象</param>
		public DESchemaOperationDefine(ObjectSchemaOperationElement element)
		{
			element.NullCheck("element");

            this.OperationMode = (SCObjectOperationMode)Enum.Parse(typeof(SCObjectOperationMode), element.Name, true);
			this.MethodName = element.Method;
			this.HasParentParemeter = element.HasParentParemeter;
		}

		/// <summary>
		/// 获取或设置<see cref="SCObjectOperationMode"/>值之一，表示操作的类型
		/// </summary>
        public SCObjectOperationMode OperationMode
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置方法名称
		/// </summary>
		public string MethodName
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置一个<see cref="bool"/>值，表示是否具有父参数
		/// </summary>
		public bool HasParentParemeter
		{
			get;
			set;
		}

		/// <summary>
		/// 执行操作
		/// </summary>
		/// <param name="data">用于操作的<see cref="DESchemaObjectBase"/>对象</param>
		/// <param name="parent">用于操作的父<see cref="DESchemaObjectBase"/>对象</param>
		/// <param name="deletedByContainer">是否被容器删除</param>
		/// <returns></returns>
		public DESchemaObjectBase DoOperation(IDEObjectOperations operationFacade, DESchemaObjectBase data, DESchemaObjectBase parent, bool deletedByContainer = false)
		{
			MethodName.CheckStringIsNullOrEmpty("MethodName");

			Type type = DEObjectOperations.Instance.GetType();

			MethodInfo mi = type.GetMethod(MethodName);

			(mi != null).FalseThrow("不能在类型{0}中找到方法{1}", type.FullName, MethodName);

			object[] parameters = null;

			if (HasParentParemeter || parent != null)
			{
                if (this.OperationMode == SCObjectOperationMode.Delete)
					parameters = new object[] { data, parent, deletedByContainer };
				else
					parameters = new object[] { data, parent };
			}
			else
				parameters = new object[] { data };

			try
			{
				return (DESchemaObjectBase)mi.Invoke(operationFacade, parameters);
			}
			catch (TargetParameterCountException ex)
			{
				Exception realException = ex.GetRealException();

				throw new ApplicationException(string.Format("调用方法{0}出现异常，参数个数为{1}: {2}",
					this.MethodName, parameters.Length, realException.Message), realException);
			}
			catch (System.Exception ex)
			{
				throw ex.GetRealException();
			}
		}
	}

	/// <summary>
	/// 表示模式操作定义的集合
	/// </summary>
	[Serializable]
    public class DESchemaOperationDefineCollection : SerializableEditableKeyedDataObjectCollectionBase<SCObjectOperationMode, DESchemaOperationDefine>
	{
		/// <summary>
		/// 初始化<see cref="SchemaOperationDefineCollection"/>的新实例
		/// </summary>
		public DESchemaOperationDefineCollection()
		{
		}

		/// <summary>
		/// 从配置信息中初始化
		/// </summary>
		/// <param name="elements"></param>
		public DESchemaOperationDefineCollection(ObjectSchemaOperationElementCollection elements)
		{
			this.LoadFromConfiguration(elements);
		}

		/// <summary>
		/// 反序列化方式专用
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
        protected DESchemaOperationDefineCollection(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}

        public void LoadFromConfiguration(ObjectSchemaOperationElementCollection elements)
		{
			this.Clear();

			if (elements != null)
			{
				foreach (ObjectSchemaOperationElement opElem in elements)
				{
					this.Add(new DESchemaOperationDefine(opElem));
				}
			}
		}

		/// <summary>
		/// 获取指定对象的键
		/// </summary>
		/// <param name="item">要在集合中查找键的<see cref="DESchemaOperationDefine"/>对象</param>
        /// <returns><see cref="SCObjectOperationMode"/>值</returns>
        protected override SCObjectOperationMode GetKeyForItem(DESchemaOperationDefine item)
		{
			return item.OperationMode;
		}
	}
}
