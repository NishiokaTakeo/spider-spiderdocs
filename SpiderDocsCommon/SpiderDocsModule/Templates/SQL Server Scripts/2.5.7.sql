/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.GetNotificationUser')) DROP PROCEDURE [dbo].[GetNotificationUser];
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.GetNotificationInfo')) DROP PROCEDURE [dbo].[GetNotificationInfo];
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[GetNotificationInfo]
@id_doc int =0,
@version int =0
AS

IF OBJECT_ID('tempdb..#nusers') IS NOT NULL DROP TABLE #nusers;
create table #nusers
(
    id int default 0,
    name varchar(max) default '',
    email varchar(max) default '',
	id_notification_group int default 0
);

declare @id_notification_group int = 0;

/*
 Create list of notification users
*/
declare cur CURSOR local for select id from notification_group a where exists(select id from document_notification_group b where id_doc = @id_doc and b.id_notification_group = a.id);  /*Get notification Id that subject for this @id_doc*/

open cur;

fetch next from cur into @id_notification_group;

while @@FETCH_STATUS = 0 begin

	with nuser_cte
	as
	(
		select * from (
			select id_key, key_type,id_notification_group from user_notification_group where id_notification_group = @id_notification_group and key_type = 1
		union all
			select id_key, key_type,id_notification_group from user_notification_group where id_notification_group in (select id_key from user_notification_group where id_notification_group = @id_notification_group and key_type = 2) and key_type = 1
		) as nuser
	)
	, nuserall
	as
	(
		select u.id, u.name, u.email, @id_notification_group as id_notification_group from nuser_cte as n inner join [user] as u on u.id = n.id_key where u.active =1 and key_type = 1
	)
	insert into #nusers  select * from nuserall ;
	
	fetch next from cur into @id_notification_group;
end

close cur
deallocate cur


select distinct u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version
inner join notification_group AS ng ON ng.id = dng.id_notification_group
inner join #nusers u ON u.id_notification_group = dng.id_notification_group
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
where de.event = 'Saved as New Version' and v.version = @version and d.id = @id_doc;

/*END_SQL_SCRIPT*/







/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_group') is not null drop view view_document_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_group as 
SELECT distinct dh.id, dv.id_doc, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.event, dv.extension AS type, dh.comments,dv.reason,ng.id  as id_notification_group, ng.group_name, doc.id_sp_status,dv.extension
FROM            dbo.[document] AS doc INNER JOIN				
                         dbo.document_version AS dv ON dv.id = doc.id_latest_version INNER JOIN
							dbo.document_historic AS dh ON dh.id_version = dv.id  INNER JOIN         
                         dbo.document_event AS de ON de.id = dh.id_event  INNER JOIN         
						 dbo.[document_notification_group] as dng on dv.id_doc = dng.id_doc  left JOIN         
						 dbo.notification_group as ng on ng.id = dng.id_notification_group  left JOIN         
                         dbo.[user] AS u ON dh.id_user = u.id 											 
						 where (de.event = 'Saved as New Version' or de.event = 'Created' or de.event = 'Import' or de.event = 'New version' or de.event = 'Scanned') and doc.id_status in (1,2,3);
/*END_SQL_SCRIPT*/
