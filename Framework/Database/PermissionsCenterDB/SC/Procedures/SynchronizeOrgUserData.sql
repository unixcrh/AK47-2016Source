-- =============================================
-- Author:		贺成德
-- Create date: 2015-07-21
-- Description:	处理用户组织机构信息从主数据到权限中心
-- =============================================
CREATE PROCEDURE [dbo].[P_SynchronizeOrgUserData]
		@BatchTime  Datetime
AS
BEGIN
	SET NOCOUNT ON;	

	BEGIN TRANSACTION

	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; 

	SET XACT_ABORT ON;
	--从主数据表中得到每个对象的最后一条记录，构造权限中心的记录的字段，插入到临时表中
	DECLARE @OrgUser TABLE([ParentID] NVARCHAR(36),[ObjectID] NVARCHAR(36), [Data] xml, [Status] INT,[IsDefault] INT,[InnerSort] INT)
	INSERT INTO @OrgUser([ParentID],[ObjectID], [Data], [Status],[IsDefault],[InnerSort])
	SELECT 
		(A.SETID_DEPT+'-'+A.DEPTID) AS [ParentID]
		,A.EMPLID AS [ObjectID]
		,dbo.GetOrgUserXml(MDP_GUID) AS [Data]
		,(CASE WHEN A.MDP_OperationType='DELETE' THEN 3 ELSE 1 END) AS [Status]
		,(CASE WHEN A.JOB_INDICATOR='P' THEN 1 ELSE 0 END ) AS [IsDefault]
		,EFFSEQ AS [InnerSort]
		--,0 AS [InnerSort]
	 FROM MDM.JOB_ALL A INNER JOIN
		(SELECT SETID_DEPT, DEPTID,EMPLID, MAX(MDP_BatchTime) AS MAX_BatchTime,MAX(EMPL_RCD) AS EMPL_RCD
		FROM MDM.JOB_ALL
		GROUP BY SETID_DEPT,DEPTID,EMPLID) LASTEST
		ON A.SETID_DEPT = LASTEST.SETID_DEPT AND A.DEPTID = LASTEST.DEPTID AND A.EMPLID=LASTEST.EMPLID 
		AND A.MDP_BatchTime = LASTEST.MAX_BatchTime AND A.EMPL_RCD=LASTEST.EMPL_RCD  
	WHERE MDP_Result IS NULL AND HR_STATUS='A'
		
	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '用户组织机构清洗完毕';

	--与现有的记录进行比较，找出需要新增的记录，放在临时表@InsertedSchemaObjects中
	DECLARE @InsertedSchemaObjects TABLE([ParentID] NVARCHAR(36),[ObjectID] NVARCHAR(36), [Data] xml, [Status] INT,[IsDefault] INT,[InnerSort] INT)
	INSERT INTO @InsertedSchemaObjects([ParentID],[ObjectID], Data, [Status],[IsDefault],[InnerSort])
	SELECT [ParentID],[ObjectID], Data, [Status],[IsDefault],[InnerSort]
	FROM @OrgUser OU
	WHERE NOT EXISTS(SELECT [ParentID] FROM SC.SchemaRelationObjects WHERE [ParentID] = OU.[ParentID] AND [ObjectID]=OU.[ObjectID])
	
	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '与现有的记录进行比较，找出需要新增的记录';

	--与现有的记录进行比较，找出需要修改的记录，放在临时表@UpdatedSchemaObjects中
	DECLARE @UpdatedSchemaObjects TABLE([ParentID] NVARCHAR(36),[ObjectID] NVARCHAR(36), [Data] xml, [Status] INT,[IsDefault] INT,[InnerSort] INT)
	INSERT INTO @UpdatedSchemaObjects([ParentID],[ObjectID], Data, [Status],[IsDefault],[InnerSort])
	SELECT [ParentID],[ObjectID], Data, [Status],[IsDefault],[InnerSort]
	FROM @OrgUser OU
	WHERE EXISTS(SELECT [ParentID] FROM SC.SchemaRelationObjects
		WHERE [ParentID] = OU.[ParentID] AND [ObjectID]=OU.[ObjectID]  
		AND (CAST([Data] AS NVARCHAR(MAX)) <> CAST(OU.Data AS NVARCHAR(MAX)) OR [Status] <> OU.[Status]));
	
	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '与现有的记录进行比较，找出需要修改的记录';

	--下面是更新对象的操作
	--插入需要新增的对象
	INSERT INTO SC.SchemaRelationObjects([ParentID],[ObjectID],VersionStartTime, VersionEndTime, [Status], Data,IsDefault,InnerSort,FullPath,GlobalSort,SchemaType,ParentSchemaType,ChildSchemaType)
	SELECT [ParentID],[ObjectID],  @BatchTime, '9999-09-09 00:00:00.000', [Status], Data,[IsDefault],InnerSort,'','','RelationObjects','Organizations','Organizations'
	FROM @InsertedSchemaObjects;	

	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '插入需要新增的对象';

	--将当前已存在的对象的结束时间封口
	UPDATE SC.SchemaRelationObjects
	SET VersionEndTime = @BatchTime
	FROM @UpdatedSchemaObjects OT INNER JOIN SC.SchemaRelationObjects SC 
	ON SC.[ParentID] = OT.[ParentID] AND  SC.[ObjectID] = OT.[ObjectID]
	WHERE SC.VersionEndTime = '9999-09-09 00:00:00.000';

	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '将当前已存在的对象的结束时间封口';

	--查询新变更的对象信息
	INSERT INTO SC.SchemaRelationObjects([ParentID],[ObjectID],VersionStartTime, VersionEndTime, [Status], Data,IsDefault,InnerSort,FullPath,GlobalSort,SchemaType,ParentSchemaType,ChildSchemaType)
	SELECT [ParentID],[ObjectID],  @BatchTime, '9999-09-09 00:00:00.000', [Status], Data,[IsDefault],InnerSort,'','','RelationObjects','Organizations','Organizations'
	FROM @UpdatedSchemaObjects;

	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '查询新变更的对象信息';

	UPDATE JA SET JA.MDP_Result='DONE' FROM MDM.JOB_ALL JA   WHERE MDP_Result IS NULL

	IF @@ERROR <> 0 GOTO TransActionFail
	PRINT '修改MDP_Result='+'DONE'+'';

	COMMIT TRANSACTION;

	GOTO QuickOut

	TransActionFail:
		ROLLBACK TRANSACTION

	QuickOut:
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED;	
END
