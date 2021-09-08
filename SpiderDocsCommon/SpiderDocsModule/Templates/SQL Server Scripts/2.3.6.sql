/*START_SQL_SCRIPT*/
IF OBJECT_ID('document_folder_deleted') is not null
    DROP TABLE document_folder_deleted;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[document_folder_deleted](
	[id] [int] NOT NULL,
	[document_folder] [varchar](200) NULL,
	[id_parent] [int] NOT NULL
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS ( SELECT *
            FROM   sysobjects
            WHERE  id = object_id(N'[dbo].[drillDownFoldersBy]')
                   AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[drillDownFoldersBy]
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[drillDownFoldersBy]
@id_parent int
AS
BEGIN

SET NOCOUNT ON;

/*declare @id_parent int
set @@id_parent = 5688;*/

WITH EntityChildren AS
(
	SELECT *  FROM document_folder WHERE id = @id_parent
	UNION all
	SELECT f.* FROM document_folder f INNER JOIN EntityChildren p on f.id_parent = p.id
)

SELECT * from EntityChildren;

END
/*END_SQL_SCRIPT*/