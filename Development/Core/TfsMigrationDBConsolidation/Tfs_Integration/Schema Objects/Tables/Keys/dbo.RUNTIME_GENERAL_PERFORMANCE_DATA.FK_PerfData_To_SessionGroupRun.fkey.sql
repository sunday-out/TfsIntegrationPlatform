﻿ALTER TABLE [dbo].[RUNTIME_GENERAL_PERFORMANCE_DATA]
	ADD CONSTRAINT [FK_PerfData_To_SessionGroupRun] 
	FOREIGN KEY (SessionGroupRunId)
	REFERENCES RUNTIME_SESSION_GROUP_RUNS (Id)	

