/*START_SQL_SCRIPT*/
delete from [document_attribute] where [id_field_type] = 4 and [atb_value] = '0'
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update [document_attribute] set [atb_value] = '''' + [atb_value] + '''' where [id_field_type] = 4
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.system_settings ADD
	ignore_subject_prefix bit NOT NULL CONSTRAINT DF_system_settings_ignore_subject_prefix DEFAULT 0

ALTER TABLE dbo.system_settings SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/
