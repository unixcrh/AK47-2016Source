﻿CREATE TABLE [WF].[INITIALIZATION_USER_AD_IMAGE] (
    [LOGIN_NAME]   NVARCHAR (100)   NOT NULL,
    [PICTURE_ID]   UNIQUEIDENTIFIER NOT NULL,
    [UPDATETIME]   DATETIME         NULL,
    [UPDATESTATUS] BIT              CONSTRAINT [DF_INITIALIZATION_USER_AD_IMAGE_UPDATESTATUS] DEFAULT ((0)) NOT NULL,
    [VALIDSTATUS]  BIT              CONSTRAINT [DF_INITIALIZATION_USER_AD_IMAGE_VALIDSTATUS] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_INITIALIZATION_USER_AD_IMAGE] PRIMARY KEY CLUSTERED ([LOGIN_NAME] ASC)
);
