CREATE PROCEDURE [DE].[InitData]

AS

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'28C09DB8-724B-4650-9BBE-5629A70B3923', N'销售合同', N'F3820927-27A4-4BD7-8CF0-DD59A7F375E3', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 2, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,2, N'/集团公司/销售板块/销售合同')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'2C7D802D-8192-46F9-ADDC-297B0E810425', N'集团公司', NULL, NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 0, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,0, N'/集团公司')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'48BE753C-630D-42F4-A02D-D2B50818F817', N'运输', N'763DF7AB-4B69-469A-8A01-041DDEAB19F7', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 3, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,2, N'/集团公司/管道板块/运输')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'763DF7AB-4B69-469A-8A01-041DDEAB19F7', N'管道板块', N'2C7D802D-8192-46F9-ADDC-297B0E810425', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 2, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,1, N'/集团公司/管道板块')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'7AAAAA74-2517-4E48-A09F-739EC1EA479E', N'销售价格', N'F3820927-27A4-4BD7-8CF0-DD59A7F375E3', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 3, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,2, N'/集团公司/销售板块/销售价格')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'97C6C8EA-F131-4D50-AEB0-0193D968D851', N'炼油', N'763DF7AB-4B69-469A-8A01-041DDEAB19F7', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 1, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,2, N'/集团公司/管道板块/炼油')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'EBABB15A-AFD8-4072-A5C9-03F1B0B5CDFF', N'销售订单', N'F3820927-27A4-4BD7-8CF0-DD59A7F375E3', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 1, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,2, N'/集团公司/销售板块/销售订单')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'F3820927-27A4-4BD7-8CF0-DD59A7F375E3', N'销售板块', N'2C7D802D-8192-46F9-ADDC-297B0E810425', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 1, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,1, N'/集团公司/销售板块')

INSERT [DE].[Categories] ([Code], [DisplayName], [ParentCode], [Description], [Status], [VersionStartTime], [VersionEndTime], [SortNo], [Creator], [CreateTime], [Modifier], [ModifyTime], [Level], [FullPath]) VALUES (N'F827528B-4F4F-4B0A-958F-80B035BBC4AD', N'勘探', N'763DF7AB-4B69-469A-8A01-041DDEAB19F7', NULL, 1, CAST(0x0000A28700000000 AS DateTime), NULL, 2, N'admin', CAST(0x0000A28700000000 AS DateTime), NULL, NULL,2, N'/集团公司/管道板块/勘探')

RETURN 0
