/*START_SQL_SCRIPT*/
ALTER TABLE [dbo].[group] ADD is_admin bit default 0 not null;
/*END_SQL_SCRIPT*/