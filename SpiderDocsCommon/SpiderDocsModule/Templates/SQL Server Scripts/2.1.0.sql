/*START_SQL_SCRIPT*/
if object_id('dbo.view_historic') is not null drop view view_historic;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_historic as 
SELECT        dh.id, dv.id_doc, dh.id_version, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.id as id_event,de.event, dr.rollback_to, dv.extension AS type, dh.comments
FROM            dbo.document_historic AS dh INNER JOIN
                         dbo.document_version AS dv ON dh.id_version = dv.id INNER JOIN
                         dbo.[document] AS doc ON dv.id_doc = doc.id INNER JOIN
                         dbo.document_event AS de ON de.id = dh.id_event LEFT OUTER JOIN
                         dbo.document_rollback AS dr ON dh.id = dr.id_historic INNER JOIN
                         dbo.[user] AS u ON dh.id_user = u.id;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF not EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[document_folder]') 
         AND name = 'id_parent'
) 
BEGIN
ALTER TABLE [dbo].[document_folder] ADD id_parent int default 0 not null;
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_folder
as 
SELECT        df.id AS id, df.document_folder AS document_folder, ug.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.document_folder AS df INNER JOIN
                         dbo.folder_group AS fg ON df.id = fg.id_folder INNER JOIN
                         dbo.user_group AS ug ON fg.id_group = ug.id_group
UNION
SELECT        df.id AS id, df.document_folder AS document_folder, fu.id_user AS id_user,df.id_parent AS id_parent
FROM            dbo.document_folder AS df INNER JOIN
                         dbo.folder_user AS fu ON df.id = fu.id_folder;
/*END_SQL_SCRIPT*/                         