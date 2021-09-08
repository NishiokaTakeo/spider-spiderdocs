/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attributes
	DROP CONSTRAINT FK_attributes_attribute_field_type

ALTER TABLE dbo.attribute_field_type SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_position

ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_system_field

ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_period_research

ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_max_lengh

ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_only_numbers

ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_appear_query

ALTER TABLE dbo.attributes
	DROP CONSTRAINT DF_custom_field_appear_input
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE dbo.Tmp_attributes
	(
	id int NOT NULL,
	name varchar(50) NOT NULL,
	id_type int NOT NULL,
	position int NOT NULL,
	system_field bit NOT NULL,
	period_research int NOT NULL,
	max_lengh int NOT NULL,
	only_numbers bit NOT NULL,
	appear_query bit NOT NULL,
	appear_input bit NOT NULL
	)  ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.Tmp_attributes SET (LOCK_ESCALATION = TABLE)

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_position DEFAULT ((0)) FOR position

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_system_field DEFAULT ((0)) FOR system_field

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_period_research DEFAULT ((0)) FOR period_research

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_max_lengh DEFAULT ((20)) FOR max_lengh

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_only_numbers DEFAULT ((0)) FOR only_numbers

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_appear_query DEFAULT ((0)) FOR appear_query

ALTER TABLE dbo.Tmp_attributes ADD CONSTRAINT
	DF_custom_field_appear_input DEFAULT ((0)) FOR appear_input

IF EXISTS(SELECT * FROM dbo.attributes)
	 EXEC('INSERT INTO dbo.Tmp_attributes (id, name, id_type, position, system_field, period_research, max_lengh, only_numbers, appear_query, appear_input)
		SELECT id, name, id_type, position, system_field, period_research, max_lengh, only_numbers, appear_query, appear_input FROM dbo.attributes WITH (HOLDLOCK TABLOCKX)')

ALTER TABLE dbo.attribute_doc_type
	DROP CONSTRAINT FK_attribute_doc_type_attributes

ALTER TABLE dbo.attribute_combo_item
	DROP CONSTRAINT FK_attribute_combo_item_attributes

ALTER TABLE dbo.document_attribute
	DROP CONSTRAINT FK_document_attribute_attributes
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
DROP TABLE dbo.attributes
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
EXECUTE sp_rename N'dbo.Tmp_attributes', N'attributes', 'OBJECT' 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attributes ADD CONSTRAINT
	PK_attributes PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attributes WITH NOCHECK ADD CONSTRAINT
	FK_attributes_attribute_field_type FOREIGN KEY
	(
	id_type
	) REFERENCES dbo.attribute_field_type
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attributes
	NOCHECK CONSTRAINT FK_attributes_attribute_field_type
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_attribute WITH NOCHECK ADD CONSTRAINT
	FK_document_attribute_attributes FOREIGN KEY
	(
	id_atb
	) REFERENCES dbo.attributes
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_attribute
	NOCHECK CONSTRAINT FK_document_attribute_attributes
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_attribute SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attribute_combo_item WITH NOCHECK ADD CONSTRAINT
	FK_attribute_combo_item_attributes FOREIGN KEY
	(
	id_atb
	) REFERENCES dbo.attributes
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attribute_combo_item
	NOCHECK CONSTRAINT FK_attribute_combo_item_attributes
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attribute_combo_item SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attribute_doc_type WITH NOCHECK ADD CONSTRAINT
	FK_attribute_doc_type_attributes FOREIGN KEY
	(
	id_attribute
	) REFERENCES dbo.attributes
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attribute_doc_type
	NOCHECK CONSTRAINT FK_attribute_doc_type_attributes
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.attribute_doc_type SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
INSERT INTO [attributes]
           ([id]
           ,[name]
           ,[id_type]
           ,[position]
           ,[system_field]
           ,[period_research]
           ,[max_lengh]
           ,[only_numbers]
           ,[appear_query]
           ,[appear_input])
     VALUES
           (10003
           ,'MailTo'
           ,1
           ,0
           ,1
           ,0
           ,0
           ,0
           ,1
           ,0)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
INSERT INTO [attributes]
           ([id]
           ,[name]
           ,[id_type]
           ,[position]
           ,[system_field]
           ,[period_research]
           ,[max_lengh]
           ,[only_numbers]
           ,[appear_query]
           ,[appear_input])
     VALUES
           (10004
           ,'MailIsComposed'
           ,2
           ,0
           ,1
           ,0
           ,0
           ,0
           ,2
           ,0)
/*END_SQL_SCRIPT*/

update [attributes] set name = 'MailSubject' where id = 10000
update [attributes] set name = 'MailFrom' where id = 10001
update [attributes] set name = 'MailTime' where id = 10002

/*START_SQL_SCRIPT*/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_user_workspace_customize_c_mail_received_time]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workspace_customize] DROP CONSTRAINT [DF_user_workspace_customize_c_mail_received_time]
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_user_workspace_customize_c_mail_sender]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workspace_customize] DROP CONSTRAINT [DF_user_workspace_customize_c_mail_sender]
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_user_workspace_customize_c_mail_subject]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workspace_customize] DROP CONSTRAINT [DF_user_workspace_customize_c_mail_subject]
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize
	DROP COLUMN c_mail_subject, c_mail_subject_width, c_mail_subject_position, c_mail_sender, c_mail_sender_width, c_mail_sender_position, c_mail_received_time, c_mail_received_time_width, c_mail_received_time_position

ALTER TABLE dbo.user_workspace_customize SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize ADD
	c_mail_subject bit NULL,
	c_mail_subject_width int NULL,
	c_mail_subject_position int NULL,
	c_mail_from bit NULL,
	c_mail_from_width int NULL,
	c_mail_from_position int NULL,
	c_mail_to bit NULL,
	c_mail_to_width int NULL,
	c_mail_to_position int NULL,
	c_mail_time bit NULL,
	c_mail_time_width int NULL,
	c_mail_time_position int NULL,
	c_mail_in_out_prefix bit NULL
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize ADD CONSTRAINT
	DF_user_workspace_customize_c_mail_subject DEFAULT 0 FOR c_mail_subject
/*END_SQL_SCRIPT*/	

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize ADD CONSTRAINT
	DF_user_workspace_customize_c_mail_from DEFAULT 0 FOR c_mail_from
/*END_SQL_SCRIPT*/	

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize ADD CONSTRAINT
	DF_user_workspace_customize_c_mail_to DEFAULT 0 FOR c_mail_to
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize ADD CONSTRAINT
	DF_user_workspace_customize_c_mail_time DEFAULT 0 FOR c_mail_time
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize ADD CONSTRAINT
	DF_user_workspace_customize_c_mail_in_out_prefix DEFAULT 0 FOR c_mail_in_out_prefix
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_workspace_customize SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/
