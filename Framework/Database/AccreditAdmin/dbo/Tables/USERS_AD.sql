﻿CREATE TABLE [dbo].[USERS_AD] (
    [GUID]          NVARCHAR (36)  NOT NULL,
    [FIRST_NAME]    NVARCHAR (32)  NOT NULL,
    [LAST_NAME]     NVARCHAR (32)  NOT NULL,
    [LOGON_NAME]    NVARCHAR (64)  NOT NULL,
    [IC_CARD]       NVARCHAR (16)  NULL,
    [PWD_TYPE_GUID] NVARCHAR (36)  NULL,
    [USER_PWD]      NVARCHAR (255) NULL,
    [RANK_CODE]     NVARCHAR (32)  NOT NULL,
    [E_MAIL]        NVARCHAR (64)  NULL,
    [POSTURAL]      INT            NOT NULL,
    [CREATE_TIME]   DATETIME       NOT NULL,
    [MODIFY_TIME]   DATETIME       NOT NULL,
    [AD_COUNT]      MONEY          NOT NULL,
    [PERSON_ID]     NVARCHAR (7)   NULL,
    [SYSDISTINCT1]  NVARCHAR (16)  NULL,
    [SYSDISTINCT2]  NVARCHAR (32)  NULL,
    [SYSCONTENT1]   NVARCHAR (32)  NULL,
    [SYSCONTENT2]   NVARCHAR (64)  NULL,
    [SYSCONTENT3]   NVARCHAR (128) NULL,
    [PINYIN]        NVARCHAR (64)  NULL
);
