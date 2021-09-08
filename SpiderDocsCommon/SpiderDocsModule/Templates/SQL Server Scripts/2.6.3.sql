
/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnFindInheritedFolderFrom]') AND type in (N'FN', N'IF',N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnFindInheritedFolderFrom]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE FUNCTION [dbo].[fnFindInheritedFolderFrom]
(
    @id_folder as int /* id_folder: search from*/
)
RETURNS TABLE AS

RETURN (

    WITH entitychildren
            AS (SELECT id, id_parent
                FROM   document_folder
                WHERE  id = @id_folder
                UNION ALL
                SELECT f.id, f.id_parent
                FROM   document_folder f
                        INNER JOIN entitychildren p
                                ON f.id = p.id_parent
            )
            SELECT top 1 id_folder
            FROM entitychildren as ec
            inner join
            (
                select distinct id_folder from [permission_folder_group]
                union all
                select distinct id_folder from [permission_folder_user]
				union all
				select distinct id as id_folder from document_folder where id_parent = 0
            ) as p on p.id_folder = ec.id
)
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnFolderGroupsPermissions]') AND type in (N'FN', N'IF',N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnFolderGroupsPermissions]
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE FUNCTION [dbo].[fnFolderGroupsPermissions]
(
    @id_folder as int
)
RETURNS TABLE AS
/*This function is replaced from view_permission_folder_group*/

RETURN
(

        SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder,fp.id_group
            FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                            dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group
            where exists (select id_folder from fnFindInheritedFolderFrom(@id_folder) p where p.id_folder =  fp.id_folder)
);
/*
    SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder,fp.id_group
    FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                    dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group
    where fp.id_folder = 18 and ug.id_user = @id_user
*/
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnFolderUsersPermissions]') AND type in (N'FN', N'IF',N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnFolderUsersPermissions]
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE FUNCTION [dbo].[fnFolderUsersPermissions]
(
    @id_folder as int
)

RETURNS TABLE as

RETURN
(
	select id_user,id_folder, id_permission,allow,[deny] from permission_folder_user AS your_permissions
    WHERE exists ( select id_folder from fnFindInheritedFolderFrom(@id_folder) i where i.id_folder = your_permissions.id_folder )
)
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if exists(select * from sys.indexes where name = 'idx1_permission_folder_group' and object_id = object_id('permission_folder_group'))
    DROP INDEX [idx1_permission_folder_group] ON [dbo].[permission_folder_group]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx1_permission_folder_group] ON [dbo].[permission_folder_group]
(
	[id_folder] ASC,
	[id_group] ASC,
	[id_permission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/





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
        SELECT id_user,id_folder,id_permission,allow FROM fnFolderGroupsPermissions(@id_folder) AS g WHERE allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM fnFolderGroupsPermissions(@id_folder) WHERE ([deny] = 1 or [allow] = 0) AND id_permission = g.id_permission and id_user = g.id_user)
    ) AS your_permissions
    WHERE not exists (SELECT id_permission FROM fnFolderUsersPermissions(@id_folder) AS u WHERE ([deny] = 1 or [allow] = 0) AND id_permission = your_permissions.id_permission and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder) and your_permissions.id_user = @id_user

)
/*
RETURN
(
    select id_user,id_folder, id_permission,allow from (
        SELECT id_user,id_folder,id_permission,allow FROM permission_folder_user AS u WHERE allow = 1 AND [deny] = 0
        UNION
        SELECT id_user,id_folder,id_permission,allow FROM view_permission_folder_group AS g WHERE allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM view_permission_folder_group WHERE id_user = g.id_user AND id_folder = g.id_folder  AND [deny] = 1 AND id_permission = g.id_permission)
    ) AS your_permissions
    WHERE not exists (SELECT id_permission FROM permission_folder_user AS u WHERE [deny] = 1 AND id_permission = your_permissions.id_permission and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder) AND your_permissions.id_folder = @id_folder and your_permissions.id_user = @id_user
);
*/

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
        FROM entitychildren as ec where 1 = case when @id_permission = 0 then 1 when exists(select id_folder from fnGetUserPermission(ec.id, @id_user) as v where v.id_permission = @id_permission) then 1 else 0 end;
  END
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
        select id,document_folder,id_parent from document_folder as v where id_parent = @id_parent and exists(select id_permission from fnGetUserPermission(v.id, @id_user) as p where id_permission = @id_permission) and v.archived = @archived;

    select * from (
        select id,document_folder,id_parent from #docfolder
        union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder as v where id_parent in (select id from #docfolder) and exists(select id_permission from fnGetUserPermission(v.id, @id_user) as p where id_permission = @id_permission) and v.archived = @archived ) as t  order by id_parent, document_folder;

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
    select id,document_folder,id_parent from document_folder as v where exists(select id from #docfolder as df where df.id = v.id) and exists(select id_permission from fnGetUserPermission(v.id, @id_user) as p where id_permission = @id_permission) order by id_parent, document_folder;
else
	select id,document_folder,id_parent from document_folder as v where exists(select id from #docfolder as df where df.id = v.id) order by id_parent, document_folder;

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

select id_permission,allow from fnGetUserPermission(@id_folder, @id_user);

return;
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.cnvDocumentTitle')) DROP PROCEDURE [dbo].[cnvDocumentTitle];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[cnvDocumentTitle]
											@title varchar(max) = ''
AS

declare @ext nvarchar(10) = '';
declare @format nvarchar(200) = '';
declare @ans nvarchar(200) = '';

set @ext = substring(@title, len(@title) - charindex('.',reverse(@title)) + 1 ,99);

print 'Debug > EXT = ' + @ext;

select @format = [format] from document_title_rule where extension = @ext;

print 'Debug > FORMAT = ' + @format;

if @format = ''
	select @title as ans;/*print 'RETURN = ' + @title;*/

set @ans = replace(@format, '@fname',replace(@title,@ext,''))
/*set @ans = replace(@ans, '@NOW',FORMAT (getdate(),' dd MMM yyyy hh:mm tt'))*/
set @ans = replace(@ans, '@NOW', (CONVERT(nvarchar, GETDATE(), 106) +' ' + right(CONVERT(nvarchar, GETDATE(), 8),11) + replace(replace(replace(right(CONVERT(nvarchar, GETDATE(), 0),2),' ','0'),'P',' P'),'A',' A')) )


set @ans += @ext;
/*set @ans = replace(@ans,'  ',' ');*/

print 'Debug> ANS = ' + @ans

select @ans as ans;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_inherited_folder') is not null drop view view_inherited_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_inherited_folder]
AS
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder,fp.id_group
FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group;
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.copyPermissions')) DROP PROCEDURE [dbo].[copyPermissions];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[copyPermissions]
											@fromFolderId int,
                                            @toFolderId int
AS

delete permission_folder_group where id_folder = @toFolderId;
delete permission_folder_user where id_folder = @toFolderId;

insert into permission_folder_group ([id_folder],[id_group],[id_permission],[allow],[deny]) select @toFolderId as [id_folder],[id_group],[id_permission],[allow],[deny] from permission_folder_group where id_folder = @fromFolderId;
insert into permission_folder_user ([id_folder],[id_user],[id_permission],[allow],[deny]) select @toFolderId as [id_folder],[id_user],[id_permission],[allow],[deny] from permission_folder_user where id_folder = @fromFolderId;

/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission') is not null drop view view_permission;
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.RmFolderPermission')) DROP PROCEDURE [dbo].[RmFolderPermission];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[RmFolderPermission]
@id_folder int
AS
BEGIN

    /*delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder) ;
    delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder) ;*/
    delete from permission_folder_group where id_folder = @id_folder;
    delete from permission_folder_user where id_folder = @id_folder;

    /*delete from folder_group where id_folder = @id_folder;
    delete from folder_user where id_folder = @id_folder;*/
    delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder);


END
/*END_SQL_SCRIPT*/






/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetFolderPermissions]') AND type in (N'FN', N'IF',N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetFolderPermissions]
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE FUNCTION [dbo].[fnGetFolderPermissions]
(
    @id_folder as int
)

RETURNS  TABLE as
RETURN
(
    SELECT id_user,id_folder,id_permission,allow,[deny] FROM fnFolderUsersPermissions(@id_folder)
    UNION
    SELECT id_user,id_folder,id_permission,allow,[deny] FROM fnFolderGroupsPermissions(@id_folder)

)
/*END_SQL_SCRIPT*/


























/*
use test4_____TESTSpiderDocs


declare @id_folder int = 323594

    --set identity_insert document_folder_archived off;

    declare @id_folder_cur int;


	IF OBJECT_ID('tempdb..#scheduled_folder') IS NOT NULL DROP TABLE #scheduled_folder;

	CREATE TABLE #scheduled_folder
    (
        id int not null default 0,
        document_folder varchar(100) not null default '',
        id_parent int not null default ''
    )

    insert into #scheduled_folder Exec [dbo].[drillDownFoldersBy] @id_parent = @id_folder, @archived = 0 ;
	insert into #scheduled_folder select id,document_folder,id_parent from document_folder where archived = 1;


    declare cur CURSOR LOCAL for select id from #scheduled_folder where id <> 196;
    open cur
    fetch next from cur into @id_folder_cur;

    while @@FETCH_STATUS = 0 BEGIN

        exec RmFolderPermission @id_folder = @id_folder_cur;

        fetch next from cur into @id_folder_cur;
    END

    close cur;
    deallocate cur;



	delete from permission_folder_group where not exists(select * from document_folder f where f.id = permission_folder_group.id_folder );
	delete from permission_folder_user where not exists(select * from document_folder f where f.id = permission_folder_user.id_folder );



	delete from #scheduled_folder;
	set identity_insert document_folder_deleted on;

	declare @i int = 20;
	while @i > 0
	begin

		insert into #scheduled_folder select id,document_folder,id_parent from document_folder f where not exists (select * from document_folder f2 where f2.id = f.id_parent) and id_parent <> 0;

		declare cur2 CURSOR LOCAL for select id from #scheduled_folder where id <> 196;
		open cur2
		fetch next from cur2 into @id_folder_cur;

		while @@FETCH_STATUS = 0 BEGIN

			exec RmFolderPermission @id_folder = @id_folder_cur;

			fetch next from cur2 into @id_folder_cur;
		END

		close cur2;
		deallocate cur2;

		insert into document_folder_deleted (id,document_folder,id_parent) select id,document_folder,id_parent from document_folder f where not exists (select * from document_folder f2 where f2.id = f.id_parent)  and id_parent <> 0;
		delete from document_folder where not exists (select * from document_folder f2 where  f2.id = document_folder.id_parent) and id_parent <> 0;

		set @i = @i -1
	end
*/