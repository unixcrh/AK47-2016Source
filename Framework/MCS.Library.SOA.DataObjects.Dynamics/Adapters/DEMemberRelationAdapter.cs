using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects.Dynamics.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Organizations;
using MCS.Library.SOA.DataObjects.Schemas.Adapters;
using MCS.Library.SOA.DataObjects.Dynamics.Objects;
using MCS.Library.SOA.DataObjects.Dynamics.Schemas;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;

namespace MCS.Library.SOA.DataObjects.Dynamics.Adapters
{
    /// <summary>
    /// 成员关系对象的适配器。这里的成员关系主要是群组和人员、应用和角色、应用和权限、角色和被授权对象之间的关系
    /// </summary>
    public class DEMemberRelationAdapter : DESchemaObjectAdapterBase<DESimpleRelationBase>
    {
        /// <summary>
        /// <see cref="DEMemberRelationAdapter"/>的实例，此字段为只读
        /// </summary>
        public static readonly DEMemberRelationAdapter Instance = new DEMemberRelationAdapter();

        private DEMemberRelationAdapter()
        {
        }

        /// <summary>
        /// 根据成员ID载入对象
        /// </summary>
        /// <param name="memberID">成员ID</param>
        /// <returns></returns>
        public DEObjectContainerRelationCollection LoadByMemberID(string memberID)
        {
            return LoadByMemberID(memberID, DateTime.MinValue);
        }

        /// <summary>
        /// 根据成员ID和容器模式类型载入对象
        /// </summary>
        /// <param name="memberID">成员ID</param>
        /// <param name="containerSchemaType">容器模式类型，如果空则忽略此参数</param>
        /// <returns></returns>
        public DEObjectContainerRelationCollection LoadByMemberID(string memberID, string containerSchemaType)
        {
            return LoadByMemberID(memberID, containerSchemaType, DateTime.MinValue);
        }

        /// <summary>
        /// 根据成员ID和时间点载入对象
        /// </summary>
        /// <param name="memberID">成员ID</param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DEObjectContainerRelationCollection LoadByMemberID(string memberID, DateTime timePoint)
        {
            return LoadByMemberID(memberID, string.Empty, timePoint);
        }

        /// <summary>
        /// 根据成员ID，容器模式类型和时间点载入对象
        /// </summary>
        /// <param name="memberID">成员ID</param>
        /// <param name="containerSchemaType">容器模式类型</param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DEObjectContainerRelationCollection LoadByMemberID(string memberID, string containerSchemaType, DateTime timePoint)
        {
            memberID.CheckStringIsNullOrEmpty("memberID");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("MemberID", memberID);

            if (containerSchemaType.IsNotEmpty())
                builder.AppendItem("ContainerSchemaType", containerSchemaType);

            return Load<DEObjectContainerRelationCollection>(builder, timePoint);
        }

        /// <summary>
        /// 根据容器ID载入对象
        /// </summary>
        /// <param name="containerID">容器ID</param>
        /// <returns></returns>
        public DEObjectMemberRelationCollection LoadByContainerID(string containerID)
        {
            return LoadByContainerID(containerID, DateTime.MinValue);
        }

        /// <summary>
        /// 根据容器ID和成员模式类型载入对象
        /// </summary>
        /// <param name="containerID">容器ID</param>
        /// <param name="memberSchemaType">成员模式类型，为空则忽略此参数</param>
        /// <returns></returns>
        public DEObjectMemberRelationCollection LoadByContainerID(string containerID, string memberSchemaType)
        {
            return LoadByContainerID(containerID, memberSchemaType, DateTime.MinValue);
        }

        /// <summary>
        /// 根据容器ID，时间点载入对象
        /// </summary>
        /// <param name="containerID">容器ID</param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DEObjectMemberRelationCollection LoadByContainerID(string containerID, DateTime timePoint)
        {
            return LoadByContainerID(containerID, string.Empty, timePoint);
        }

        /// <summary>
        /// 根据容器ID，成员模式类型和时间点载入对象
        /// </summary>
        /// <param name="containerID">容器ID</param>
        /// <param name="memberSchemaType">成员模式类型，如果为空则忽略此参数</param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DEObjectMemberRelationCollection LoadByContainerID(string containerID, string memberSchemaType, DateTime timePoint)
        {
            containerID.CheckStringIsNullOrEmpty("containerID");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("ContainerID", containerID);

            if (memberSchemaType.IsNotEmpty())
                builder.AppendItem("MemberSchemaType", memberSchemaType);

            return Load<DEObjectMemberRelationCollection>(builder, timePoint);
        }

        /// <summary>
        /// 根据指定条件和时间点，载入对象
        /// </summary>
        /// <typeparam name="T">表示返回结果的<see cref="SCMemberRelationCollectionBase"/>的派生类型</typeparam>
        /// <param name="builder">包含条件的<see cref="WhereSqlClauseBuilder"/></param>
        /// <param name="timePoint">时间点</param>
        /// <returns></returns>
        public T Load<T>(WhereSqlClauseBuilder builder, DateTime timePoint) where T : DEMemberRelationCollectionBase, new()
        {
            var timeBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(builder, timeBuilder);

            T result = null;

            VersionedObjectAdapterHelper.Instance.FillData(GetMappingInfo().TableName, connectiveBuilder, this.GetConnectionName(),
                view =>
                {
                    result = new T();

                    result.LoadFromDataView(view);
                });

            return result;
        }

        /// <summary>
        /// 根据容器ID，成员ID载入对象
        /// </summary>
        /// <param name="containerID">容器ID</param>
        /// <param name="memberID">成员ID</param>
        /// <returns></returns>
        public DESimpleRelationBase Load(string containerID, string memberID)
        {
            return Load(containerID, memberID, DateTime.MinValue);
        }

        /// <summary>
        /// 根据容器ID，成员ID和时间点载入对象
        /// </summary>
        /// <param name="containerID">容器ID</param>
        /// <param name="memberID">成员ID</param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DESimpleRelationBase Load(string containerID, string memberID, DateTime timePoint)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("ContainerID", containerID);
            builder.AppendItem("MemberID", memberID);

            DEMemberRelationCollection relations = Load(builder, timePoint);

            return relations.FirstOrDefault();
        }

        /// <summary>
        /// 根据条件和时间点载入对象
        /// </summary>
        /// <param name="builder">包含条件的<see cref="IConnectiveSqlClause"/></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public DEMemberRelationCollection Load(IConnectiveSqlClause builder, DateTime timePoint)
        {
            var timeBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint);

            ConnectiveSqlClauseCollection connectiveBuilder = new ConnectiveSqlClauseCollection(builder, timeBuilder);

            DEMemberRelationCollection result = null;

            VersionedObjectAdapterHelper.Instance.FillData(GetMappingInfo().TableName, connectiveBuilder, this.GetConnectionName(),
                view =>
                {
                    result = new DEMemberRelationCollection();

                    result.LoadFromDataView(view);
                });

            return result;
        }

        public void RelationAction(
            DEBase container,
            DESchemaObjectCollection members,
            DEMemberRelationCollection relation,
            bool saveTargetData,
            bool saveMemberData,
            DEStandardObjectSchemaType type
        )
        {
            //取出旧的关系集合
            var oldRelation = DEMemberRelationAdapter.Instance.LoadByContainerID(container.ID, type.ToString());

            //所有旧成员集合
            var oldMemberIDs = oldRelation.Select(p => p.ID).ToList();

            var newMemberIDs = members.Select(m => m.ID).ToList();

            List<string> needDelIDs = new List<string>();

            foreach (var id in oldMemberIDs)
            {
                if (!newMemberIDs.Contains(id))
                {
                    needDelIDs.Add(id);
                }
            }

            
            //删除关系
            oldRelation.ForEach(p =>
            {
                if (needDelIDs.Contains(p.ID))
                {
                    DEMemberRelationAdapter.Instance.UpdateStatus(p, SchemaObjectStatus.DeletedByContainer);
                }
            });

            if (saveTargetData)
                //容器入库
                DESchemaObjectAdapter.Instance.Update(container);

            if (saveMemberData)
            {
                //删除成员
                needDelIDs.ForEach(id => DESchemaObjectAdapter.Instance.UpdateStatus(DESchemaObjectAdapter.Instance.Load(id), SchemaObjectStatus.Deleted));
                //新成员入库
                members.ForEach(p => DESchemaObjectAdapter.Instance.Update(p));
            }

            //新关系入库

            relation.ForEach(p => DEMemberRelationAdapter.Instance.Update(p));

        }
    }
}
