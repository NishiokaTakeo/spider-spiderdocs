/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetUserPermission]') AND type in (N'FN', N'IF',N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetUserPermission]
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE FUNCTION [dbo].[fnGetUserPermission]
(
    @id_folder as int,
    @id_user as int
)

RETURNS  TABLE as /* @t TABLE(id_user int,id_folder int , id_permission int,allow bit) as*/
RETURN
(
	select id_user,id_folder, id_permission,allow from (
        SELECT id_user,id_folder,id_permission,allow FROM fnFolderUsersPermissions(@id_folder) AS u WHERE allow = 1 AND [deny] = 0
        UNION
        SELECT id_user,id_folder,id_permission,allow FROM fnFolderGroupsPermissions(@id_folder) AS g WHERE allow = 1 AND [deny] = 0
    ) AS your_permissions
    WHERE not exists (SELECT id_permission FROM fnFolderUsersPermissions(@id_folder) AS u WHERE ([deny] = 1 or [allow] = 0) AND id_permission = your_permissions.id_permission and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder) and your_permissions.id_user = @id_user

)
/*END_SQL_SCRIPT*/
