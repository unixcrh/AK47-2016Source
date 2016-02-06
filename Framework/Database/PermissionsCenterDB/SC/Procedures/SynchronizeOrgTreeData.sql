-- =============================================
-- Author:		贺成德
-- Create date: 2015-07-21
-- Description:	处理组织机构与组织机构信息从主数据到权限中心
-- =============================================
CREATE PROCEDURE [dbo].[P_SynchronizeOrgTreeData]
		@BatchTime  Datetime
AS
BEGIN
	SET NOCOUNT ON;	

	BEGIN TRANSACTION

	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; 

	SET XACT_ABORT ON;
	--从主数据表中得到每个对象的最后一条记录，构造权限中心的记录的字段，插入到临时表中
	DECLARE @OrgTree TABLE([ParentID] NVARCHAR(36),[ObjectID] NVARCHAR(36), [Data] xml, [Status] INT,[InnerSort] BIGINT)
	INSERT INTO @OrgTree([ParentID],[ObjectID], [Data], [Status],[InnerSort])
	SELECT 
		(CASE WHEN A.PARENT_NODE_NAME='' THEN 'e588c4c6-4097-4979-94c2-9e2429989932'  
		ELSE A.SETID+'-'+A.PARENT_NODE_NAME END) AS [ParentID],
		A.SETID+'-'+A.TREE_NODE AS [ObjectID],
		dbo.GetOrgTreeXml(MDP_GUID) AS [Data],
		CASE WHEN A.MDP_OperationType='DELETE' THEN 3 ELSE 1 END AS [Status]
		,TREE_NODE_NUM AS [InnerSort]
		--,0 AS [InnerSort]
	FROM MDM.ORG_TREE_ALL A INNER JOIN
		(SELECT SETID, TREE_NODE,PARENT_NODE_NAME, MAX(MDP_BatchTime) AS MAX_BatchTime
		FROM MDM.ORG_TREE_ALL
		GROUP BY SETID, TREE_NODE,PARENT_NODE_NAME) LASTEST
		ON A.SETID = LASTEST.SETID AND A.TREE_NODE = LASTEST.TREE_NODE AND A.PARENT_NODE_NAME=LASTEST.PARENT_NODE_NAME
		 AND A.MDP_BatchTime = LASTEST.MAX_BatchTime
	WHERE MDP_Result IS NULL;

	IF @@ERROR <> 0 GOTO TransActionFail

	--与现有的记录进行比较，找出需要新增的记录，放在临时表@InsertedSchemaObjects中
	DECLARE @InsertedSchemaObjects TABLE([ParentID] NVARCHAR(36),[ObjectID] NVARCHAR(36), [Data] xml, [Status] INT,[InnerSort] BIGINT)
	INSERT INTO @InsertedSchemaObjects([ParentID],[ObjectID], Data, [Status],[InnerSort])
	SELECT [ParentID],[ObjectID], Data, [Status],[InnerSort]
	FROM @OrgTree OT
	WHERE NOT EXISTS(SELECT [ParentID] FROM SC.SchemaRelationObjects WHERE [ParentID] = OT.[ParentID] AND [ObjectID]=OT.[ObjectID])
	
	IF @@ERROR <> 0 GOTO TransActionFail

	--与现有的记录进行比较，找出需要修改的记录，放在临时表@UpdatedSchemaObjects中
	DECLARE @UpdatedSchemaObjects TABLE([ParentID] NVARCHAR(36),[ObjectID] NVARCHAR(36), [Data] xml, [Status] INT,[InnerSort] BIGINT)
	INSERT INTO @UpdatedSchemaObjects([ParentID],[ObjectID], Data, [Status],[InnerSort])
	SELECT [ParentID],[ObjectID], Data, [Status],[InnerSort]
	FROM @OrgTree OT
	WHERE EXISTS(SELECT [ParentID] FROM SC.SchemaRelationObjects
		WHERE [ParentID] = OT.[ParentID] AND [ObjectID]=OT.[ObjectID]  
		AND (CAST([Data] AS NVARCHAR(MAX)) <> CAST(OT.Data AS NVARCHAR(MAX)) OR [Status] <> OT.[Status]))

	IF @@ERROR <> 0 GOTO TransActionFail

	--下面是更新对象的操作
	--插入需要新增的对象
	INSERT INTO SC.SchemaRelationObjects([ParentID],[ObjectID],VersionStartTime, VersionEndTime, [Status], Data,IsDefault,InnerSort,FullPath,GlobalSort,SchemaType,ParentSchemaType,ChildSchemaType)
	SELECT [ParentID],[ObjectID],  @BatchTime, '9999-09-09 00:00:00.000', [Status], Data,1,InnerSort,'','','RelationObjects','Organizations','Organizations'
	FROM @InsertedSchemaObjects
 	
	IF @@ERROR <> 0 GOTO TransActionFail

	--将当前已存在的对象的结束时间封口
	UPDATE SC.SchemaRelationObjects
	SET VersionEndTime = @BatchTime
	FROM @UpdatedSchemaObjects OT INNER JOIN SC.SchemaRelationObjects SC 
	ON SC.[ParentID] = OT.[ParentID] AND  SC.[ObjectID] = OT.[ObjectID]
	WHERE SC.VersionEndTime = '9999-09-09 00:00:00.000'

	IF @@ERROR <> 0 GOTO TransActionFail
	--查询新变更的对象信息
	INSERT INTO SC.SchemaRelationObjects([ParentID],[ObjectID],VersionStartTime, VersionEndTime, [Status], Data,IsDefault,InnerSort,FullPath,GlobalSort,SchemaType,ParentSchemaType,ChildSchemaType)
	SELECT [ParentID],[ObjectID],  @BatchTime, '9999-09-09 00:00:00.000', [Status], Data,1,InnerSort,'','','RelationObjects','Organizations','Organizations'
	FROM @UpdatedSchemaObjects;

	IF @@ERROR <> 0 GOTO TransActionFail

	UPDATE A SET A.MDP_Result='DONE' FROM MDM.ORG_TREE_ALL A   WHERE MDP_Result IS NULL
	IF @@ERROR <> 0 GOTO TransActionFail

	COMMIT TRANSACTION;

	GOTO QuickOut

	TransActionFail:
		ROLLBACK TRANSACTION

	QuickOut:
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED;	
END
