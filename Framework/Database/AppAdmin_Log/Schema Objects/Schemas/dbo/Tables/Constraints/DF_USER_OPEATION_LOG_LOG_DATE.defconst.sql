﻿ALTER TABLE [dbo].[USER_OPEATION_LOG]
    ADD CONSTRAINT [DF_USER_OPEATION_LOG_LOG_DATE] DEFAULT (getdate()) FOR [LOG_DATE];
