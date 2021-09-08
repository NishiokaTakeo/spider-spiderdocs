/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.folderDrillDownL1')) DROP PROCEDURE [dbo].[folderDrillDownL1];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[folderDrillDownL1] 
								@id_parent int = 0,
								@id_user int = 0,
                                @id_permission int = 0
AS 
    
SET TRANSACTION isolation level READ uncommitted; 


if( @id_permission > 0 )
    select id,document_folder,id_parent from view_folder as v where id_user = @id_user and exists(select id from document_folder as df where df.id_parent = @id_parent and df.id = v.id) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) order by id_parent, document_folder;
else
	select id,document_folder,id_parent from view_folder as v where id_user = @id_user and exists(select id from document_folder as df where df.id_parent = @id_parent and df.id = v.id) order by id_parent, document_folder;
	
/*END_SQL_SCRIPT*/              

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
WHERE not exists (SELECT id_permission FROM view_permission_folder_user AS u WHERE [deny] = 1 AND id_permission = your_permissions.id_permission and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder);
/*END_SQL_SCRIPT*/



