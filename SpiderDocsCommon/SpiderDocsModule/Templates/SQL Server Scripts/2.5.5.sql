 /*START_SQL_SCRIPT*/
/*IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'user_preferences'
                      AND COLUMN_NAME = 'pdf_merge'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE user_preferences DROP COLUMN pdf_merge;
  END*/
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
/*ALTER TABLE dbo.user_preferences ADD pdf_merge bit NOT NULL default 0;*/
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
/*update dbo.user_preferences set pdf_merge = 0;*/
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'user_preferences'
                      AND COLUMN_NAME = 'default_pdf_merge'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE user_preferences DROP COLUMN default_pdf_merge;
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_preferences ADD default_pdf_merge bit NOT NULL default 0;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update dbo.user_preferences set default_pdf_merge = 0;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.hasFoldersPermissionByUser')) DROP PROCEDURE [dbo].[hasFoldersPermissionByUser];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create procedure hasFoldersPermissionByUser
  @id_user int = 0,
  @permission int = 0,
  @id_folders varchar(500) = ''
as

IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp;

create table #tmp
(
    id_permission int default 0,
    allow int  default 0
);

declare @id_folder int = 0;

declare cur cursor for select item from fnSplit(@id_folders,',')

open cur;
fetch next from cur into @id_folder;

while (@@FETCH_STATUS = 0 ) begin
	insert into #tmp Exec folderPermissionByUser @id_folder = @id_folder, @id_user = @id_user

	fetch next from cur into @id_folder;
end

close cur
deallocate cur

declare @total int = 0;declare @hasPer int = 0;
select @total = count(*) from fnSplit(@id_folders,',');
select @hasPer = count(*) from #tmp where id_permission = @permission and allow = 1;

if @hasPer = @total
	select 1 as ans;
else
	select 0 as ans;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_group') is not null drop view view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_permission_folder_group]
AS
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fg.id_folder,fg.id_group
FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                dbo.folder_group AS fg with (nolock) ON ug.id_group = fg.id_group INNER JOIN
                dbo.document_permission AS fp ON fp.id_folder_group = fg.id;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF OBJECT_ID('permission_folder_group') is not null
    DROP TABLE permission_folder_group;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table permission_folder_group(
  id int IDENTITY(1,1) primary key,
  id_folder int not null default 0,
  id_group int not null default 0,
  id_permission int not null default 0,
  [allow] bit not null default 0,
  [deny] bit not null default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create index idfolder_on_permission_folder_group on permission_folder_group ( id_folder );
create index idgroup_on_permission_folder_group on permission_folder_group( id_folder,id_group );
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete from permission_folder_group;
insert into permission_folder_group (id_folder,id_group,id_permission,allow,[deny]) select distinct id_folder,id_group,id_permission,allow,[deny] from view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF OBJECT_ID('permission_folder_user') is not null
    DROP TABLE permission_folder_user;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table permission_folder_user(
  id int IDENTITY(1,1) primary key,
  id_folder int not null default 0,
  id_user int not null default 0,
  id_permission int not null default 0,
  [allow] bit not null default 0,
  [deny] bit not null default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create index idfolder_on_permission_folder_user on permission_folder_user( id_folder );
create index idgroup_on_permission_folder_user on permission_folder_user( id_folder,id_user );
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete from permission_folder_user;
insert into permission_folder_user (id_folder,id_user,id_permission,allow,[deny]) select distinct id_folder,id_user,id_permission,allow,[deny] from view_permission_folder_user;
/*END_SQL_SCRIPT*/


/*
Update twice : view_permission_folder_group
*/
/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_group') is not null drop view view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_permission_folder_group]
AS
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder,fp.id_group
FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group;
/*END_SQL_SCRIPT*/






/*

UPDATE ALL REFERENCE : view_user_folder_permission

*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_user_folder_permission') is not null drop view view_user_folder_permission;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_user_folder_permission as
select id_user,id_folder, id_permission,allow from (
	SELECT id_user,id_folder,id_permission,allow FROM permission_folder_user AS u WHERE allow = 1 AND [deny] = 0
	UNION
	SELECT id_user,id_folder,id_permission,allow FROM view_permission_folder_group AS g WHERE allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM view_permission_folder_group WHERE id_user = g.id_user AND id_folder = g.id_folder  AND [deny] = 1 AND id_permission = g.id_permission)
) AS your_permissions
WHERE not exists (SELECT id_permission FROM permission_folder_user AS u WHERE [deny] = 1 AND id_permission = your_permissions.id_permission and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder);
/*END_SQL_SCRIPT*/

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
	SELECT id_folder,id_permission,allow FROM permission_folder_user AS u WHERE id_user = @id_user AND allow = 1 AND [deny] = 0
	UNION
	SELECT id_folder,id_permission,allow FROM view_permission_folder_group AS g WHERE id_user = @id_user AND allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM view_permission_folder_group WHERE id_user = g.id_user AND id_folder = g.id_folder  AND [deny] = 1 AND id_permission = g.id_permission)
) AS your_permissions
WHERE not exists (SELECT id_permission FROM permission_folder_user AS u WHERE id_user = @id_user AND [deny] = 1 AND id_permission = your_permissions.id_permission AND u.id_folder= your_permissions.id_folder);



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

select id_permission,allow from view_user_folder_permission WHERE id_user = @id_user AND id_folder = @id_folder;

return;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
/*This returns only folders that you can READ*/
CREATE VIEW [dbo].[view_folder]
AS
SELECT        distinct df.id AS id, df.document_folder AS document_folder, fg.id_user AS id_user, df.id_parent AS id_parent
FROM			dbo.view_user_folder_permission AS fg WITH (NOLOCK) INNER JOIN
				dbo.document_folder AS df WITH (NOLOCK) ON df.id = fg.id_folder
WHERE           fg.id_permission = 2;

/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update [permission] set permission = 'Check In/Out' where permission = 'Save\Edit';
/*END_SQL_SCRIPT*/





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
begin

    IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;

    CREATE TABLE #docfolder
    (
        id INT,
        document_folder varchar(255),
        id_parent INT
    );

    insert into #docfolder
        select id,document_folder,id_parent from document_folder as v where id_parent = @id_parent and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission);

    select * from (
        select id,document_folder,id_parent from #docfolder
        union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder as v where id_parent in (select id from #docfolder) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) ) as t  order by id_parent, document_folder;

end
else
begin
	select * from (
        select id,document_folder,id_parent from document_folder as v where id_parent = @id_parent
		    union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder where id_parent in (select id from document_folder where id_parent = @id_parent)
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
	select id,id_parent from document_folder where id_parent = @id_parent
	union all
	select id,id_parent from document_folder where id_parent in (select id from document_folder where id_parent = @id_parent)

if( @id_permission > 0 )
    select id,document_folder,id_parent from document_folder as v where exists(select id from #docfolder as df where df.id = v.id) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) order by id_parent, document_folder;
else
	select id,document_folder,id_parent from document_folder as v where exists(select id from #docfolder as df where df.id = v.id) order by id_parent, document_folder;

/*END_SQL_SCRIPT*/







/*
    Here inheritance of permissions logic

*/









/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.drillUpfoldersby')) DROP PROCEDURE [dbo].[drillUpfoldersby];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[drillUpfoldersby]
       @id INT ,
       @id_user INT,
	   @id_permission int = 2
AS
  BEGIN
      SET nocount ON; /*declare @id_parent int set @@id_parent = 5688;*/

        WITH entitychildren
            AS (SELECT *
                FROM   document_folder
                WHERE  id = @id
                UNION ALL
                SELECT f.*
                FROM   document_folder f
                        INNER JOIN entitychildren p
                                ON f.id = p.id_parent)
        SELECT *
        FROM entitychildren as ec where 1 = case when @id_permission = 0 then 1 when exists(select id_folder from view_user_folder_permission as v where v.id_folder = ec.id and v.id_user = @id_user and v.id_permission = @id_permission) then 1 else 0 end;
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
/*if exists(SELECT
    t.name AS TableName,
    c.name AS FTCatalogName ,
    i.name AS UniqueIdxName,
    cl.name AS ColumnName
FROM
    sys.tables t
INNER JOIN
    sys.fulltext_indexes fi
ON
    t.[object_id] = fi.[object_id]
INNER JOIN
    sys.fulltext_index_columns ic
ON
    ic.[object_id] = t.[object_id]
INNER JOIN
    sys.columns cl
ON
        ic.column_id = cl.column_id
    AND ic.[object_id] = cl.[object_id]
INNER JOIN
    sys.fulltext_catalogs c
ON
    fi.fulltext_catalog_id = c.fulltext_catalog_id
INNER JOIN
    sys.indexes i
ON
        fi.unique_index_id = i.index_id
    AND fi.[object_id] = i.[object_id]
where t.name = 'document_version' and c.name = 'SpiderFullText')
begin
    DROP FULLTEXT INDEX ON dbo.document_version;
end */
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
/*ALTER TABLE [document_version] ALTER COLUMN [extension] varchar(20);*/
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
/*create fulltext index on dbo.document_version(filedata TYPE COLUMN extension) key index PK_document_versionn WITH CHANGE_TRACKING AUTO;*/
/*END_SQL_SCRIPT*/







/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'user_preferences'
                      AND COLUMN_NAME = 'enable_folder_creation_by_user'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE user_preferences DROP COLUMN enable_folder_creation_by_user;
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_preferences ADD enable_folder_creation_by_user bit NOT NULL default 0;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update dbo.user_preferences set enable_folder_creation_by_user = 0;
/*END_SQL_SCRIPT*/




















/*
This is no longer used
EXEC sp_rename 'REM_view_permission_folder_user', 'view_permission_folder_user';
EXEC sp_rename 'REM_folder_group', 'folder_group';
EXEC sp_rename 'REM_folder_user', 'folder_user';
EXEC sp_rename 'REM_document_permission', 'document_permission';
EXEC sp_rename 'REM_allFolderPermissionByUser', 'allFolderPermissionByUser';
*/
/*START_SQL_SCRIPT*/
EXEC sp_rename 'view_permission_folder_user', 'REM_view_permission_folder_user';
EXEC sp_rename 'folder_group', 'REM_folder_group';
EXEC sp_rename 'folder_user', 'REM_folder_user';
EXEC sp_rename 'document_permission', 'REM_document_permission';
EXEC sp_rename 'allFolderPermissionByUser', 'REM_allFolderPermissionByUser';
/*END_SQL_SCRIPT*/





