/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.allFolderPermissionByUser')) DROP PROCEDURE [dbo].[allFolderPermissionByUser];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[allFolderPermissionByUser]     @id_user int, 	@id_permission int = 0
AS
    SET nocount ON; 
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    
    CREATE TABLE #collected 
      ( 
         id_folder     INT, 
         id_permission INT, 
         allow         INT 
      ); 

/*Admin check. Full control if you are admin*/
if exists(select ug.id_user FROM user_group AS ug inner join [group] AS g on g.id = ug.id_group WHERE ug.id_user = @id_user AND g.is_admin = 1 )
begin

    select 
      f.id as id_folder, 
      p.id as id_permission, 
      1 as allow 
    from permission as p full 
    join document_folder as f on f.id >= 0 
    where exists(select id from view_folder as v where v.id_user = @id_user and v.id = f.id)
    order by f.id,p.id ;

	RETURN;
end


insert into #collected select id_folder,id_permission,allow from (
	SELECT id_folder,id_permission,allow FROM view_permission_folder_user AS u WHERE id_user = @id_user AND allow = 1 AND [deny] = 0
	UNION 
	SELECT id_folder,id_permission,allow FROM view_permission_folder_group AS g WHERE id_user = @id_user AND allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM view_permission_folder_group WHERE id_user = g.id_user AND id_folder = g.id_folder  AND [deny] = 1 AND id_permission = g.id_permission)
) AS your_permissions
WHERE not exists (SELECT id_permission FROM view_permission_folder_user AS u WHERE id_user = @id_user AND [deny] = 1 AND id_permission = your_permissions.id_permission AND u.id_folder= your_permissions.id_folder);


 
IF ( @id_permission = 0 ) 
BEGIN 
    SELECT * 
    FROM   #collected 
    order by id_folder, id_permission;  
END 
ELSE 
BEGIN 
    SELECT * 
    FROM   #collected where id_permission = @id_permission order by id_folder, id_permission;
END 
 
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_group') is not null drop view view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_permission_folder_group]
AS
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fg.id_folder
FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                dbo.folder_group AS fg with (nolock) ON ug.id_group = fg.id_group INNER JOIN
                dbo.document_permission AS fp with (nolock) ON fp.id_folder_group = fg.id;
/*END_SQL_SCRIPT*/                         

/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_folder]
AS
SELECT        df.id AS id, df.document_folder AS document_folder, ug.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.user_group AS ug WITH (NOLOCK) INNER JOIN 
				dbo.folder_group AS fg WITH (NOLOCK) ON fg.id_group = ug.id_group INNER JOIN
				dbo.document_folder AS df WITH (NOLOCK) ON df.id = fg.id_folder                          
UNION
SELECT        df.id AS id, df.document_folder AS document_folder, fu.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.folder_user  AS fu WITH (NOLOCK) INNER JOIN
                dbo.document_folder AS df WITH (NOLOCK) ON df.id = fu.id_folder;
/*END_SQL_SCRIPT*/   