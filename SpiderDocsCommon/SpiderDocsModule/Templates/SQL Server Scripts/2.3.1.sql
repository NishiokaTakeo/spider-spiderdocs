/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_user') is not null drop view view_permission_folder_user;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_permission_folder_user] AS SELECT        fp.id_permission, fp.allow, fp.[deny], fu.id_user, fu.id_folder FROM            dbo.document_permission AS fp INNER JOIN                          dbo.folder_user AS fu ON fu.id = fp.id_folder_user; 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_group') is not null drop view view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_permission_folder_group] AS SELECT        fp.id_permission, fp.allow,fp.[deny], ug.id_user, fg.id_folder FROM            dbo.user_group AS ug INNER JOIN                          dbo.folder_group AS fg ON ug.id_group = fg.id_group INNER JOIN                          dbo.document_permission AS fp ON fg.id = fp.id_folder_group; 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS ( SELECT *
            FROM   sysobjects
            WHERE  id = object_id(N'[dbo].[folderPermissionByUser]')
                   AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[folderPermissionByUser]
END
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
select id_permission,allow from (
	SELECT id_permission,allow FROM view_permission_folder_user AS u WHERE id_user = @id_user AND id_folder = @id_folder AND allow = 1 AND [deny] = 0
	UNION 
	SELECT id_permission,allow FROM view_permission_folder_group AS g WHERE id_user = @id_user AND id_folder = @id_folder  AND allow = 1 AND [deny] = 0 AND g.id_permission not in (SELECT id_permission FROM view_permission_folder_group WHERE id_user = @id_user AND id_folder = @id_folder  AND [deny] = 1)
) AS your_permissions 
WHERE id_permission not in (SELECT id_permission FROM view_permission_folder_user AS u WHERE id_user = @id_user AND id_folder = @id_folder AND [deny] = 1);

select top 0 id AS id_permission, 0 AS allow FROM permission order by id;
return;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if exists(SELECT * FROM sys.indexes WHERE name='idx_document_permission_idfldgrp_allow' AND object_id = OBJECT_ID('document_permission'))
begin
	DROP INDEX [idx_document_permission_idfldgrp_allow] ON [dbo].[document_permission];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx_document_permission_idfldgrp_allow] ON [dbo].[document_permission]
(
	[id_folder_group] ASC,
	[allow] ASC,
	[deny] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if exists(SELECT * FROM sys.indexes WHERE name='NonClusteredIndex-20180618-162434' AND object_id = OBJECT_ID('document_permission'))
begin
	DROP INDEX [NonClusteredIndex-20180618-162434] ON [dbo].[document_permission]
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180618-162434] ON [dbo].[document_permission]
(
	[id_folder_user] ASC,
	[allow] ASC,
	[deny] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/

