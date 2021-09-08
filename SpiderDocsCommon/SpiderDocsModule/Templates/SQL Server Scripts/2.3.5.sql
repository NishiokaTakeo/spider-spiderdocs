/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[document_folder_deleted](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[document_folder] [varchar](200) NULL,
	[id_parent] [int] NOT NULL
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
declare @schema_name nvarchar(256);
declare @table_name nvarchar(256);
declare @col_name nvarchar(256);
declare @Command  nvarchar(1000);

set @schema_name = N'dbo';
set @table_name = N'document_folder';
set @col_name = N'id_parent';

select @Command = 'ALTER TABLE ' + @schema_name + '.' + @table_name + ' drop constraint ' + d.name
 from sys.tables t
  join    sys.default_constraints d
   on d.parent_object_id = t.object_id
  join    sys.columns c
   on c.object_id = t.object_id
    and c.column_id = d.parent_column_id
 where t.name = @table_name
  and t.schema_id = schema_id(@schema_name)
  and c.name = @col_name;

execute (@Command)

/*print @Command*/
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE [dbo].[document_folder] ADD  DEFAULT ((0)) FOR [id_parent]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_document') is not null drop view view_document;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_document]
AS
SELECT     d.id, d.id_latest_version AS id_version, d.id_user, d.id_folder, d.title, d.extension, u.name, f.document_folder, d.id_status, 'V ' + CAST(dv.version AS varchar(3)) AS version, d.id_type, dt.type, 
                      dh.date, d.id_review, d.id_checkout_user,tCheckOutUser.name AS CheckOutUser, d.id_sp_status, d.created_date
FROM         dbo.[document] AS d INNER JOIN
                      dbo.[user] AS u ON d.id_user = u.id INNER JOIN
                      dbo.document_version AS dv ON d.id = dv.id_doc AND d.id_latest_version = dv.id INNER JOIN
                      dbo.document_historic AS dh ON dv.id_historic = dh.id INNER JOIN
                      dbo.document_folder AS f ON d.id_folder = f.id LEFT OUTER JOIN
                      dbo.document_type AS dt ON d.id_type = dt.id LEFT OUTER JOIN
                      dbo.[user] AS tCheckOutUser ON d.id_checkout_user = tCheckOutUser.id
WHERE     (d.id_status NOT IN (5))
/*END_SQL_SCRIPT*/
