/*START_SQL_SCRIPT*/
if OBJECT_ID('dbo.user_workspace') is not null drop table user_workspace;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table user_workspace (
    id int identity(1,1) primary key not null ,
    id_user int not null default 0,
    id_version int not null default 0,
    filename varchar(255) not null default '',
    filedata varbinary(MAX),
    filehash varchar(64) not null default '',
    created_date datetime not null default GETUTCDATE(),
    lastaccess_date datetime ,
    lastmodified_date datetime
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if exists (select * from sys.indexes where name = 'idx0_user_workspace' and object_id = object_id('user_workspace'))
begin
 drop index idx0_user_workspace on [dbo].[user_workspace];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create NONCLUSTERED index [idx0_user_workspace] on [dbo].[user_workspace]
(
    [id_user] asc
);
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.transferToUserWorkSpace')) DROP PROCEDURE [dbo].[transferToUserWorkSpace];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[transferToUserWorkSpace]
    @id_version int,
    @id_user int,
    @hash varchar(64)
AS

insert into user_workspace (id_user,id_version,filename,filedata,filehash,created_date) select @id_user as id_user, v.id, d.title,v.filedata, @hash as filehash,GETUTCDATE()
from document_version as v inner join document as d on d.id_latest_version = v.id
where d.id_latest_version = @id_version;


SELECT SCOPE_IDENTITY() as id;
return ;

/*END_SQL_SCRIPT*/





/*START_SQL_SCRIPT*/
if OBJECT_ID('dbo.user_workspace_deleted') is not null drop table user_workspace_deleted;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table user_workspace_deleted (
    id int not null ,
    id_user int not null default 0,
    id_version int not null default 0,
    filename varchar(255) not null default '',
    filedata varbinary(MAX),
    filehash varchar(64) not null default '',
    created_date datetime not null default GETUTCDATE(),
    lastaccess_date datetime ,
    lastmodified_date datetime
);



/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.transferToUserWorkSpaceBack')) DROP PROCEDURE [dbo].[transferToUserWorkSpaceBack];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[transferToUserWorkSpaceBack]
    @id int
AS

    insert into user_workspace_deleted (id,id_user,id_version,filename,filedata,filehash,created_date,lastaccess_date,lastmodified_date) select top 1 id,id_user,id_version,filename,filedata,filehash,created_date,lastaccess_date,lastmodified_date from user_workspace where id = @id;

return ;

/*END_SQL_SCRIPT*/
