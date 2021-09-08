

/*START_SQL_SCRIPT*/
if object_id('dbo.view_user_folder_permission') is not null drop view view_user_folder_permission;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_user_folder_permission as
select id_user,id_folder, id_permission,allow from (
	SELECT id_user,id_folder,id_permission,allow FROM view_permission_folder_user AS u WHERE allow = 1 AND [deny] = 0
	UNION 
	SELECT id_user,id_folder,id_permission,allow FROM view_permission_folder_group AS g WHERE allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM view_permission_folder_group WHERE id_user = g.id_user AND id_folder = g.id_folder  AND [deny] = 1 AND id_permission = g.id_permission)
) AS your_permissions
WHERE not exists (SELECT id_permission FROM view_permission_folder_user AS u WHERE [deny] = 1 AND id_permission = your_permissions.id_permission  and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.folderPermissionByUser')) DROP PROCEDURE [dbo].[folderPermissionByUser];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[folderPermissionByUser]
    @id_folder int,
    @id_user int
AS

/*Admin check. Full control if you are admin*/
if exists(select ug.id_user FROM user_group AS ug inner join [group] AS g on g.id = ug.id_group WHERE ug.id_user = @id_user AND g.is_admin = 1 )
begin
	select distinct  id AS id_permission, 1 AS allow FROM permission order by id;
	RETURN;
end

/* Indivisual/Group; Indivisual:Allow -> Group:Allow BUT no include Deny*/
select id_permission,allow from view_user_folder_permission where id_user = @id_user and id_folder = @id_folder;
/*END_SQL_SCRIPT*/



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
	select id,id_parent from document_folder where id_parent = @id_parent
	union all
	select id,id_parent from document_folder where id_parent in (select id from document_folder where id_parent = @id_parent)
	
if( @id_permission > 0 )
    select id,document_folder,id_parent from view_folder as v where id_user = @id_user and exists(select id from #docfolder as df where df.id = v.id) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) order by id_parent, document_folder;
else
	select id,document_folder,id_parent from view_folder as v where id_user = @id_user and exists(select id from #docfolder as df where df.id = v.id) order by id_parent, document_folder;
    --select id,document_folder,id_parent from view_folder as v where id_user = @id_user and (id_parent = @id_parent or exists(select id from document_folder as df where df.id = v.id and id_parent in (select id from document_folder where id_parent = @id_parent) ))  order by id_parent, document_folder;
	
/*END_SQL_SCRIPT*/              





