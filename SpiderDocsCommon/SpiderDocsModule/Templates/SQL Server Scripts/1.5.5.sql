/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[document_review_users](
	[id_review] [int] NULL,
	[id_user] [int] NULL,
	[comment] [varchar](max) NULL,
	[action] [int] NULL,
	[id_version] [int] NULL,
) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [document_review_users] ([id_review],[id_user],[comment],[action],[id_version]) 
Select
 t2.id as [id_review]
,t1.[id_user]
,t1.[comment]     
,t1.[action]
,t1.[id_version]
FROM [document_review] t1
inner join 
	(select min(id) as id ,id_doc, serial from [document_review] group by id_doc, serial) t2
on t1.[id_doc] = t2.id_doc and t1.[serial] = t2.serial
where t1.[action] <> 1
order by [id_review]
/*END_SQL_SCRIPT*/
	 
/*START_SQL_SCRIPT*/
delete [document_review] where id not in (select min(id) as id from [document_review] group by id_doc, serial)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete [document_review] where id = 0
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete [document_review_users] where [id_review] = 0
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_review
	DROP COLUMN action

ALTER TABLE dbo.document_review SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_review
	DROP COLUMN serial
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE dbo.Tmp_document_review_users
	(
	id int NOT NULL IDENTITY (1, 1),
	id_review int NULL,
	id_user int NULL,
	comment varchar(MAX) NULL,
	action int NULL,
	id_version int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.Tmp_document_review_users SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
SET IDENTITY_INSERT dbo.Tmp_document_review_users OFF
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS(SELECT * FROM dbo.document_review_users)
	 EXEC('INSERT INTO dbo.Tmp_document_review_users (id_review, id_user, comment, action, id_version)
		SELECT id_review, id_user, comment, action, id_version FROM dbo.document_review_users WITH (HOLDLOCK TABLOCKX)')
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
DROP TABLE dbo.document_review_users
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
EXECUTE sp_rename N'dbo.Tmp_document_review_users', N'document_review_users', 'OBJECT' 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_review_users ADD CONSTRAINT
	PK_document_review_users PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_review_users ADD CONSTRAINT
	FK_document_review_users_document_review FOREIGN KEY
	(
	id_review
	) REFERENCES dbo.document_review
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.document_review ADD
	allow_checkout bit NOT NULL CONSTRAINT DF_document_review_allow_checkout DEFAULT 0
	
ALTER TABLE dbo.document_review SET (LOCK_ESCALATION = TABLE)
/*END_SQL_SCRIPT*/
