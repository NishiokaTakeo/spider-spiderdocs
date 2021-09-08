/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_user') is not null drop view view_permission_folder_user;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_permission_folder_user] as SELECT        fp.id_permission, fp.allow, fu.id_user, fu.id_folder FROM            dbo.document_permission AS fp INNER JOIN                          dbo.folder_user AS fu ON fu.id = fp.id_folder_user; 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_group') is not null drop view view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_permission_folder_group] as SELECT        fp.id_permission, fp.allow, ug.id_user, fg.id_folder FROM            dbo.user_group AS ug INNER JOIN                          dbo.folder_group AS fg ON ug.id_group = fg.id_group INNER JOIN                          dbo.document_permission AS fp ON fg.id = fp.id_folder_group; 
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS ( SELECT *
            FROM   sysobjects
            WHERE  id = object_id(N'[dbo].[allFolderPermissionByUser]')
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[allFolderPermissionByUser]
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[allFolderPermissionByUser]
    @id_user int,
	@id_permission int = 0
AS
	SET NOCOUNT ON;

	CREATE TABLE #first
	(
	   id_permission INT,
	   allow int,
	);

	CREATE TABLE #collected
	(
	   id_folder INT,
	   id_permission INT,
	   allow int
	);

    declare @id_folder int;

	declare cur CURSOR LOCAL for
    select id from view_folder where id_user = @id_user;

	open cur
	fetch next from cur into @id_folder;

	while @@FETCH_STATUS = 0 BEGIN

		delete from #first;

		INSERT into #first EXECUTE folderPermissionByUser @id_folder,@id_user;

		if (@id_permission = 0 )
		BEGIN
			insert into #collected (id_folder,id_permission,allow) select @id_folder as id_folder, id_permission,allow from #first;
		END
		else
		BEGIN
			insert into #collected (id_folder,id_permission,allow) select @id_folder as id_folder, id_permission,allow from #first where id_permission = @id_permission;
		END


		fetch next from cur into @id_folder;
	END

close cur;
deallocate cur;

select * from #collected;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS ( SELECT *
            FROM   sysobjects
            WHERE  id = object_id(N'[dbo].[folderPermissionByUser]')
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
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
if exists(select ug.id_user from user_group as ug inner join [group] as g on g.id = ug.id_group where ug.id_user = @id_user and g.is_admin = 1 )
begin
	select distinct  id as id_permission, 1 as allow from permission order by id;
	RETURN;
end 


/*User Check*/
if exists(select id_permission from view_permission_folder_user
				where id_user = @id_user and id_folder = @id_folder)
begin
	
	select distinct id_permission,allow
				from view_permission_folder_user
				where id_user = @id_user and id_folder = @id_folder and allow=1
				order by id_permission;

	return;
end 


/*Group check*/
if exists(select id_permission from view_permission_folder_group
	where id_user = @id_user and id_folder = @id_folder)
begin

	select  id_permission, (case when sum(allow)> 0 then 1 else 0 end) as allow from view_permission_folder_group as vp where vp.id_user = @id_user and vp.id_folder = @id_folder and vp.[allow] = 1 group by id_permission order by id_permission;

	return;
end

select top 0  id as id_permission,0 as allow from permission order by id;

return;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='idx_user_group_idusr_idgrp' AND object_id = OBJECT_ID('user_group'))
begin
	DROP INDEX idx_user_group_idusr_idgrp on [dbo].[user_group];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx_user_group_idusr_idgrp] ON [dbo].[user_group]
(
	[id_user] ASC,
	[id_group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='idx_folder_group_idfld' AND object_id = OBJECT_ID('folder_group'))
begin
	DROP INDEX idx_folder_group_idfld on [dbo].[folder_group];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx_folder_group_idfld] ON [dbo].[folder_group]
(
	[id_folder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='idx_document_permission_idfldgrp_allow' AND object_id = OBJECT_ID('document_permission'))
begin
	DROP INDEX idx_document_permission_idfldgrp_allow on [dbo].[document_permission];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx_document_permission_idfldgrp_allow] ON [dbo].[document_permission]
(
	[id_folder_group] ASC,
	[allow] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/