/*THIS IS FOR VERSION 0 TO 1.5.4*/


/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[attribute_params](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_attribute] [int] NOT NULL,
	[id_folder] [int] NOT NULL,
	[required] [int] NOT NULL,
 CONSTRAINT [PK_attribute_params] PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table [dbo].[system_settings] ALTER COLUMN webService_address varchar(50);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table [dbo].[system_settings] add delete_reason_length int;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table [dbo].[system_settings] add auto_update bit;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table [dbo].[system_submenu] add id_order int;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
EXEC sp_rename '[dbo].[user_preferences].show_db_file', 'show_import_dialog_new_mail', 'COLUMN';
UPDATE [dbo].[user_preferences] SET show_import_dialog_new_mail = 0;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add OpenedPanel int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_subject bit;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_subject_width int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_subject_position int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_from bit;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_from_width int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_from_position int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_to bit;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_to_width int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_to_position int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_time bit;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_time_width int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_time_position int;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
alter table [dbo].[user_workspace_customize] add c_mail_in_out_prefix bit;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
DROP VIEW [dbo].[view_extension];
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
DROP VIEW [dbo].[view_permission]
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
DROP VIEW [dbo].[view_version]
/*END_SQL_SCRIPT*/
