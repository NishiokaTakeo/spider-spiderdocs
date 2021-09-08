/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_preferences ADD
	exclude_archive bit NOT NULL CONSTRAINT DF_user_preferences_exclude_archive DEFAULT 0

ALTER TABLE dbo.user_preferences SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/
