

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.folderDrillDownL2')) DROP PROCEDURE [dbo].[folderDrillDownL2];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[folderDrillDownL2]
								@id_parent int = 0,
								@id_user int = 0,
                                @id_permission int = 0
AS

/*
declare @id_parent int = 0;
declare @id_user int = 1;
*/
/*declare @id_permission int = 1;*/

SET TRANSACTION isolation level READ uncommitted;

IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;

CREATE TABLE #docfolder
    (
        id INT primary key,
        /*document_folder varchar(100), */
        id_parent INT
    );

insert into #docfolder
	select id,id_parent from document_folder where id_parent = @id_parent and archived = 0
	union all
	select id,id_parent from document_folder where id_parent in (select id from document_folder where id_parent = @id_parent) and archived = 0

if( @id_permission > 0 )
    select id,document_folder,id_parent from document_folder as v where exists(select id from #docfolder as df where df.id = v.id) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) order by id_parent, document_folder;
else
	select id,document_folder,id_parent from document_folder as v where exists(select id from #docfolder as df where df.id = v.id) order by id_parent, document_folder;

/*END_SQL_SCRIPT*/





/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.folderDrillDownL1')) DROP PROCEDURE [dbo].[folderDrillDownL1];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[folderDrillDownL1]
								@id_parent int = 0,
								@id_user int = 0,
                                @id_permission int = 0,
								@archived int = 0
AS

SET TRANSACTION isolation level READ uncommitted;


if( @id_permission > 0 )
begin

    IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;

    CREATE TABLE #docfolder
    (
        id INT,
        document_folder varchar(255),
        id_parent INT
    );

    insert into #docfolder
        select id,document_folder,id_parent from document_folder as v where id_parent = @id_parent and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) and v.archived = @archived;

    select * from (
        select id,document_folder,id_parent from #docfolder
        union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder as v where id_parent in (select id from #docfolder) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) and v.archived = @archived ) as t  order by id_parent, document_folder;

end
else
begin
	select * from (
        select id,document_folder,id_parent from document_folder as v where id_parent = @id_parent and v.archived = @archived
		    union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder where id_parent in (select id from document_folder where id_parent = @id_parent) and archived = @archived
    ) as t  order by id_parent, document_folder
end
/*END_SQL_SCRIPT*/





/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.drillUpfoldersby')) DROP PROCEDURE [dbo].[drillUpfoldersby];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[drillUpfoldersby]
       @id INT ,
       @id_user INT,
	   @id_permission int = 2,
	   @archived int = 0
AS
  BEGIN
      SET nocount ON; /*declare @id_parent int set @@id_parent = 5688;*/

        WITH entitychildren
            AS (SELECT *
                FROM   document_folder
                WHERE  id = @id and archived = @archived
                UNION ALL
                SELECT f.*
                FROM   document_folder f
                        INNER JOIN entitychildren p
                                ON f.id = p.id_parent
						where f.archived = @archived
								)
        SELECT *
        FROM entitychildren as ec where 1 = case when @id_permission = 0 then 1 when exists(select id_folder from view_user_folder_permission as v where v.id_folder = ec.id and v.id_user = @id_user and v.id_permission = @id_permission) then 1 else 0 end;
  END
/*END_SQL_SCRIPT*/
