﻿ALTER TABLE [dbo].[SESSION_GROUP_CONFIGS]
	ADD CONSTRAINT [FK_SessionGroupConfigs3] 
	FOREIGN KEY (LinkingSettingId)
	REFERENCES LINKING_SETTINGS (Id)	

