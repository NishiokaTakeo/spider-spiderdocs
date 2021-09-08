/*START_SQL_SCRIPT*/
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = 'archived' 
              AND object_id = OBJECT_ID('document_folder'))
BEGIN
   alter table [document_folder] add archived bit not null default 0;

END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.schedule_archive_folder') is not null drop table schedule_archive_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table schedule_archive_folder(
    id int default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.document_folder_archived') is not null drop table document_folder_archived;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[document_folder_archived](
	[id] [int] IDENTITY(1,1) primary key,
	[document_folder] [varchar](200) not null default '',
	[id_parent] [int] NOT NULL default 0,
	[archived] [bit] NOT NULL default 0
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if exists(SELECT * FROM sys.indexes WHERE name='idx_document_folder_idparent_archived' AND object_id = OBJECT_ID('document_folder'))
begin
	DROP INDEX [idx_document_folder_idparent_archived] ON [dbo].[document_folder];
end
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx_document_folder_idparent_archived] ON [dbo].[document_folder]
(
	[id_parent] ASC,
	[archived] ASC
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
set identity_insert document_folder_archived on;

insert into document_folder_archived (id,document_folder,id_parent,archived) SELECT [id]
      ,[document_folder]
      ,[id_parent]
      ,[archived]
  FROM [dbo].[document_folder] where archived = 1;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.TaskToArchive4Folder')) DROP PROCEDURE [dbo].[TaskToArchive4Folder];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[TaskToArchive4Folder] AS
BEGIN
    
    set identity_insert document_folder_archived on;

    declare @id_folder_root int;
    declare @id_folder int;

    declare curRoot CURSOR LOCAL for select id from schedule_archive_folder;
    
    open curRoot
    
    fetch next from curRoot into @id_folder_root;

    while @@FETCH_STATUS = 0 BEGIN

        exec ArchiveFolder @id_folder = @id_folder_root;
        
        insert into document_folder_archived (id,document_folder,id_parent) select id,document_folder,id_parent from document_folder where id = @id_folder_root;
        
        update document_folder set archived = 1 where id = @id_folder_root;

        fetch next from curRoot into @id_folder_root;
    END

    close curRoot;
    deallocate curRoot;
END
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
IF EXISTS ( SELECT *
            FROM   sysobjects
            WHERE  id = object_id(N'[dbo].[drillDownArchiveFoldersBy]')
                   AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[drillDownArchiveFoldersBy]
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[drillDownArchiveFoldersBy]
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

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.ArchiveFolder')) DROP PROCEDURE [dbo].[ArchiveFolder];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[ArchiveFolder]
@id_folder int
AS
BEGIN
    set identity_insert document_folder_archived on;
    CREATE TABLE #scheduled_folder
    (
        id int not null default 0,
        document_folder varchar(100) not null default '',
        id_parent int not null default ''
    )

    declare @id_folder_cur int;

    insert into #scheduled_folder Exec [dbo].[drillDownFoldersBy] @id_parent = @id_folder;

    declare cur CURSOR LOCAL for select id from #scheduled_folder;
    open cur
    fetch next from cur into @id_folder_cur;

    while @@FETCH_STATUS = 0 BEGIN

        /*
        delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder_cur) ;
        delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder_cur) ;

        delete from folder_group where id_folder = @id_folder_cur;
        delete from folder_user where id_folder = @id_folder_cur;
        delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder_cur);
        */
        exec RmFolderPermission @id_folder = @id_folder_cur;
        
        update document_folder set archived = 1 where id = @id_folder_cur;
        
        insert into document_folder_archived (id,document_folder,id_parent,archived) select id,document_folder,id_parent,archived from document_folder where archived = 1 and id = @id_folder_cur;

        fetch next from cur into @id_folder_cur;
    END

    close cur;
    deallocate cur;
END
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.RmFolderPermission')) DROP PROCEDURE [dbo].[RmFolderPermission];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[RmFolderPermission]
@id_folder int
AS
BEGIN
    
    delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder) ;
    delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder) ;

    delete from folder_group where id_folder = @id_folder;
    delete from folder_user where id_folder = @id_folder;
    delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder);
    

END
/*END_SQL_SCRIPT*/

/*
declare @id_folder_cur int = 0;

select id_folder into #rmpermison from folder_group fg where  not exists(select * from document_folder f where f.id = fg.id_folder )

declare cur CURSOR LOCAL for select id_folder from #rmpermison;
open cur
fetch next from cur into @id_folder_cur;

while @@FETCH_STATUS = 0 BEGIN
		
	if not exists(select * from document_folder where id = @id_folder_cur)
		exec RmFolderPermission @id_folder = @id_folder_cur;

    fetch next from cur into @id_folder_cur;
END

close cur;
deallocate cur;
*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.archiveFolderDrillDownL1')) DROP PROCEDURE [dbo].[archiveFolderDrillDownL1];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[archiveFolderDrillDownL1] 
								@id_parent int = 0
AS 
    
SET TRANSACTION isolation level READ uncommitted; 

IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;

select id,document_folder,id_parent from document_folder_archived where id_parent = @id_parent
	union all
select distinct 0 as id ,'dummy' as document_folder,id_parent from document_folder_archived where id_parent in (select id from document_folder_archived where id_parent = @id_parent)
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
        select id,document_folder,id_parent from view_folder as v where id_user = @id_user and id_parent = @id_parent and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission);

    select * from (
        select id,document_folder,id_parent from #docfolder
        union all
        select distinct 0 as id ,'dummy' as document_folder,id_parent from view_folder as v where id_user = @id_user and id_parent in (select id from #docfolder) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) ) as t  order by id_parent, document_folder;
end    
else
begin
	select * from (
        select id,document_folder,id_parent from view_folder as v where id_user = @id_user and id_parent = @id_parent
		    union all
        select distinct 0 as id ,'dummy' as document_folder,id_parent from view_folder where id_parent in (select id from view_folder where id_parent = @id_parent)
    ) as t  order by id_parent, document_folder
end
/*END_SQL_SCRIPT*/       




/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.addFolderAllPermissionGroup')) DROP PROCEDURE [dbo].[addFolderAllPermissionGroup];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[addFolderAllPermissionGroup] @id_folder INT = 0, 
                                                   @id_group INT = 1
AS 
begin

SET nocount ON; 
    declare @id_permission int;

    declare cur CURSOR LOCAL for select id from permission;
    open cur
    fetch next from cur into @id_permission;

    while @@FETCH_STATUS = 0 BEGIN
        exec  addFolderPermissionGroup @id_folder = @id_folder, 
                                        @id_group = @id_group, 
                                        @id_permission = @id_permission,
                                        @allow = 1,
                                        @deny =0
        
        fetch next from cur into @id_permission;
    END

    close cur;
    deallocate cur;
end
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
@id_parent int,
@archived bit = 0
AS
BEGIN

SET NOCOUNT ON;

if (@archived = 0)
begin
    WITH EntityChildren AS
    (
        SELECT *  FROM document_folder WHERE id = @id_parent
        UNION all
        SELECT f.* FROM document_folder f INNER JOIN EntityChildren p on f.id_parent = p.id
    )

    SELECT id,document_folder,id_parent from EntityChildren;
end 
else
begin
    WITH EntityChildren AS
    (
        SELECT *  FROM document_folder_archived WHERE id = @id_parent
        UNION all
        SELECT f.* FROM document_folder_archived f INNER JOIN EntityChildren p on f.id_parent = p.id
    )

    SELECT id,document_folder,id_parent from EntityChildren;
end
END
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.UnArchiveFolder')) DROP PROCEDURE [dbo].[UnArchiveFolder];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[UnArchiveFolder]
@id_folder int,
@to int
AS
BEGIN
    set identity_insert document_folder on;

    CREATE TABLE #scheduled_folder
    (
        id int not null default 0,
        document_folder varchar(100) not null default '',
        id_parent int not null default ''
    )

    declare @id_folder_cur int;

    insert into #scheduled_folder Exec [dbo].[drillDownFoldersBy] @id_parent = @id_folder, @archived = 1;

    declare cur CURSOR LOCAL for select id from #scheduled_folder;
    open cur
    fetch next from cur into @id_folder_cur;

    while @@FETCH_STATUS = 0 BEGIN
        
        if (exists(select * from document_folder where id = @id_folder_cur))
            delete from document_folder where id = @id_folder_cur;

        if ( @id_folder =  @id_folder_cur)        
            insert into document_folder (id,document_folder,id_parent) select id,document_folder, @to as id_parent from document_folder_archived where id = @id_folder_cur;
        else
            insert into document_folder (id,document_folder,id_parent) select id,document_folder,id_parent from document_folder_archived where id = @id_folder_cur;
        
        exec addFolderAllPermissionGroup @id_folder = @id_folder_cur, @id_group = 1;
        
        delete from document_folder_archived where id = @id_folder_cur;

        fetch next from cur into @id_folder_cur;
    END

    close cur;
    deallocate cur;
END
/*END_SQL_SCRIPT*/