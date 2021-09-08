/*START_SQL_SCRIPT*/
IF OBJECT_ID('notification_group') is not null
    DROP TABLE notification_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[notification_group](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[group_name] [varchar](50) not NULL default ''
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF OBJECT_ID('user_notification_group') is not null
    DROP TABLE user_notification_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[user_notification_group](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[id_user] [int] not NULL default 0,
	[id_notification_group] [int] not NULL default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF OBJECT_ID('document_notification_group') is not null
    DROP TABLE document_notification_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[document_notification_group](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[id_doc] [int] not NULL default 0,
	[id_notification_group] [int] not NULL default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
SET IDENTITY_INSERT dbo.notification_group On;
insert into [dbo].[notification_group] (id,group_name) values (1,'All');
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [dbo].[user_notification_group] (id_user,id_notification_group) select id , 1 as id_notification_group from [dbo].[user] ;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_document') is not null drop view view_document;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_document]
AS
SELECT     d.id, d.id_latest_version AS id_version, d.id_user, d.id_folder, d.title, d.extension, u.name, f.document_folder, d.id_status, 'V ' + CAST(dv.version AS varchar(3)) AS version, d.id_type, dt.type, 
                      dh.date, d.id_review, d.id_checkout_user,tCheckOutUser.name AS CheckOutUser, d.id_sp_status, d.created_date, ng.id_notification_group
FROM         dbo.[document] AS d INNER JOIN
                      dbo.[user] AS u ON d.id_user = u.id INNER JOIN
                      dbo.document_version AS dv ON d.id = dv.id_doc AND d.id_latest_version = dv.id INNER JOIN
                      dbo.document_historic AS dh ON dv.id_historic = dh.id INNER JOIN
                      dbo.document_folder AS f ON d.id_folder = f.id LEFT OUTER JOIN
                      dbo.document_type AS dt ON d.id_type = dt.id LEFT OUTER JOIN
                      dbo.[user] AS tCheckOutUser ON d.id_checkout_user = tCheckOutUser.id LEFT OUTER JOIN
					  dbo.[document_notification_group] as ng ON ng.id_doc = d.id
WHERE     (d.id_status NOT IN (5))
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_group') is not null drop view view_document_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_group as 
SELECT      dh.id, dv.id_doc, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.event, dv.extension AS type, dh.comments,dv.reason,ng.id  as id_notification_group, ng.group_name, doc.id_sp_status,dv.extension
FROM            dbo.[document] AS doc INNER JOIN				
                         dbo.document_version AS dv ON dv.id = doc.id_latest_version INNER JOIN
							dbo.document_historic AS dh ON dh.id_version = dv.id  INNER JOIN         
                         dbo.document_event AS de ON de.id = dh.id_event  INNER JOIN         
						 dbo.[document_notification_group] as dng on dv.id_doc = dng.id_doc  left JOIN         
						 dbo.notification_group as ng on ng.id = dng.id_notification_group  left JOIN         
                         dbo.[user] AS u ON dh.id_user = u.id 											 
						 where (de.event = 'Saved as New Version' or de.event = 'Created' or de.event = 'Import') and doc.id_status in (1,2,3);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.schedule_delete_folder') is not null drop table schedule_delete_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table schedule_delete_folder(
    id int default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.TaskToDelete4Folder')) DROP PROCEDURE [dbo].[TaskToDelete4Folder];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[TaskToDelete4Folder] AS
BEGIN

    declare @id_folder_root int;
    declare @id_folder int;

    declare curRoot CURSOR LOCAL for select id from schedule_delete_folder;
    
    open curRoot
    
    fetch next from curRoot into @id_folder_root;

    while @@FETCH_STATUS = 0 BEGIN

        exec DeleteDocsByFolder @id_folder = @id_folder_root;
        
        delete from document_folder where id = @id_folder_root;

        fetch next from curRoot into @id_folder_root;
    END

    close curRoot;
    deallocate curRoot;
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.DeleteDocsByFolder')) DROP PROCEDURE [dbo].[DeleteDocsByFolder];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[DeleteDocsByFolder]
@id_folder int
AS
BEGIN
return;
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

        
        delete from document_attribute where exists (select * from document where id = document_attribute.id_doc and id_folder = @id_folder_cur);
        delete from document_deleted where exists (select * from document where id = document_deleted.id_doc and id_folder = @id_folder_cur);
        delete from document_historic where exists (select * from document_version as dv where id = document_historic.id_version and exists(select * from document where id = dv.id_doc and id_folder = @id_folder_cur));
        delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder_cur) ;
        delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder_cur) ;

        delete from folder_group where id_folder = @id_folder_cur;
        delete from folder_user where id_folder = @id_folder_cur;

        delete from document_review_users where exists (select * from document_review as dv where id = document_review_users.id_review and exists(select * from document where id = id_doc and id_folder = @id_folder_cur)) ;
        delete from document_review where exists (select * from document where id = document_review.id_doc and id_folder = @id_folder_cur);
        
        delete from document_tracked where exists (select * from document where id = document_tracked.id_doc and id_folder = @id_folder_cur);

        delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder_cur);

        delete from document where id_folder = @id_folder_cur;

        fetch next from cur into @id_folder_cur;
    END

    close cur;
    deallocate cur;
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete from system_submenu where submenu_internal_name = 'SubMenu_NotificationGroup';
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [system_permission_menu] (id_permission,id_menu) select 1 as id_permission, id from system_menu mm where not exists(select * from system_permission_menu m where m.id_menu = mm.id);
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
insert into [dbo].[system_submenu] (submenu,id_menu,submenu_internal_name,id_order) values('Notification Group',2,'SubMenu_NotificationGroup',6);
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
insert into [system_permission_submenu] (id_permission,id_submenu) select 1 as id_permission, id from system_submenu mm where not exists(select * from system_permission_submenu m where m.id_submenu = mm.id);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF OBJECT_ID('schedule_notification_amended') is not null
    DROP TABLE schedule_notification_amended;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[schedule_notification_amended](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[id_doc] [int] not NULL default 0,
    [new_version] [smallint] not NULL default 0,    
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_notification_amended') is not null drop view view_notification_amended;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_notification_amended as 
select u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
,dng.id_notification_group
,ng.group_name
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join notification_group AS ng ON ng.id = dng.id_notification_group
inner join user_notification_group AS ung ON ung.id_notification_group = dng.id_notification_group
inner join [user] as u ON u.id = ung.id_user
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version
where de.event = 'Saved as New Version'AND dng.id_notification_group <> 1 
union 
select u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
,dng.id_notification_group 
,'All' as group_name
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join [user] as u ON u.active = 1
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version 
where de.event = 'Saved as New Version' and dng.id_notification_group = 1
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_search') is not null drop view view_document_search;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_search
as 
Select 
               d.id                           AS id, 
               d.id_latest_version            AS id_latest_version, 
               d.id_user                      AS id_user, 
               d.id_folder                    AS id_folder, 
               d.title                        AS title, 
               d.extension                    AS extension, 
               u.NAME                         AS NAME, 
               f.document_folder              AS document_folder, 
               d.id_status                    AS id_status, 
               dv_fulltext.version                     AS version, 
               d.id_type                      AS id_type, 
               dt.type                        AS type, 
               dh.date                        AS date, 
               d.id_review                    AS id_review, 
               d.id_sp_status                 AS id_sp_status, 
               d.created_date                 AS created_date, 
               d.id_checkout_user             AS id_checkout_user, 
               dv_fulltext.filesize                    AS filesize, 
               atb_mail_subject.atb_value     AS mail_subject, 
               atb_mail_from.atb_value        AS mail_from, 
               atb_mail_to.atb_value          AS mail_to, 
               atb_mail_time.atb_value        AS mail_time, 
               atb_mail_is_composed.atb_value AS mail_is_composed, 
               ng.id_notification_group AS id_notification_group 
FROM   document AS d  WITH (NOLOCK)
       INNER JOIN [user] u  WITH (NOLOCK)
               ON d.id_user = u.id 
       INNER JOIN document_version dv_fulltext WITH (NOLOCK)
               ON dv_fulltext.id = d.id_latest_version 
       INNER JOIN document_historic dh WITH (NOLOCK)
               ON dv_fulltext.id_historic = dh.id 
       INNER JOIN document_folder f WITH (NOLOCK)
               ON d.id_folder = f.id 
       LEFT JOIN document_notification_group ng WITH (NOLOCK)
              ON ng.id_doc = d.id 
       LEFT JOIN document_type dt WITH (NOLOCK)
              ON d.id_type = dt.id 
       LEFT JOIN [user] tCheckOutUser WITH (NOLOCK)
              ON d.id_checkout_user = tCheckOutUser.id 
       LEFT JOIN document_attribute atb_mail_subject WITH (NOLOCK)
              ON d.id = atb_mail_subject.id_doc 
                 AND atb_mail_subject.id_atb = 10000 
       LEFT JOIN document_attribute atb_mail_from WITH (NOLOCK)
              ON d.id = atb_mail_from.id_doc 
                 AND atb_mail_from.id_atb = 10001 
       LEFT JOIN document_attribute atb_mail_to WITH (NOLOCK)
              ON d.id = atb_mail_to.id_doc 
                 AND atb_mail_to.id_atb = 10003 
       LEFT JOIN document_attribute atb_mail_time WITH (NOLOCK)
              ON d.id = atb_mail_time.id_doc 
                 AND atb_mail_time.id_atb = 10002 
       LEFT JOIN document_attribute atb_mail_is_composed WITH (NOLOCK)
              ON d.id = atb_mail_is_composed.id_doc 
                 AND atb_mail_is_composed.id_atb = 10004 ;
/*END_SQL_SCRIPT*/
