select * into _____attribute_combo_item from attribute_combo_item;
select * into _____attribute_combo_item_children from attribute_combo_item_children;
select * into _____attribute_doc_type from attribute_doc_type;
select * into _____attribute_field_type from attribute_field_type;
select * into _____attribute_params from attribute_params;
select * into _____attributes from attributes;
select * into _____document from document;
select * into _____document_attribute from document_attribute;
select * into _____document_deleted from document_deleted;
select * into _____document_event from document_event;
select * into _____document_folder from document_folder;
select * into _____document_folder_archived from document_folder_archived;
select * into _____document_folder_deleted from document_folder_deleted;
select * into _____document_historic from document_historic;
select * into _____document_notification_group from document_notification_group;
select * into _____document_review from document_review;
select * into _____document_review_users from document_review_users;
select * into _____document_rollback from document_rollback;
select * into _____document_status from document_status;
select * into _____document_tracked from document_tracked;
select * into _____document_type from document_type;
select id,id_doc,version,extension,reason,id_historic,filesize into _____document_version from document_version;
alter table _____document_version add filedata varbinary(max);
select * into _____email_server from email_server;
select * into _____favourite_property from favourite_property;
select * into _____favourite_property_item from favourite_property_item;
select * into _____group from [group];
select * into _____notification_group from notification_group;
select * into _____permission from permission;
select * into _____schedule_archive_folder from schedule_archive_folder;
select * into _____schedule_delete_folder from schedule_delete_folder;
select * into _____system_footer from system_footer;
select * into _____system_menu from system_menu;
select * into _____system_permission_level from system_permission_level;
select * into _____system_permission_menu from system_permission_menu;
select * into _____system_permission_submenu from system_permission_submenu;
select * into _____system_settings from system_settings;
select * into _____system_submenu from system_submenu;
select * into _____system_updates from system_updates;
select * into _____user from [user];
select * into _____user_event from [user_event];
select * into _____user_grid_size from user_grid_size;
select * into _____user_group from user_group;
select * into _____user_log from user_log;
select * into _____user_preferences from user_preferences;
select * into _____user_recent_document from user_recent_document;
select * into _____user_workspace_customize from user_workspace_customize;
select * into _____document_title_rule from document_title_rule;
select * into _____dragdrop_attribute from dragdrop_attribute;
select * into _____dragdrop_type from dragdrop_type;
select * into _____permission_folder_group from permission_folder_group;
select * into _____permission_folder_user from permission_folder_user;
select * into _____schedule_notification_amended from schedule_notification_amended;
select * into _____shell_behaviour from shell_behaviour;
select * into _____system_version from system_version;
select * into _____user_notification_group from user_notification_group;



exec sp_rename '_____attribute_combo_item', 'attribute_combo_item';
exec sp_rename '_____attribute_combo_item_children', 'attribute_combo_item_children';
exec sp_rename '_____attribute_doc_type', 'attribute_doc_type';
exec sp_rename '_____attribute_field_type', 'attribute_field_type';
exec sp_rename '_____attribute_params', 'attribute_params';
exec sp_rename '_____attributes', 'attributes';
exec sp_rename '_____document', 'document';
exec sp_rename '_____document_attribute', 'document_attribute';
exec sp_rename '_____document_deleted', 'document_deleted';
exec sp_rename '_____document_event', 'document_event';
exec sp_rename '_____document_folder', 'document_folder';
exec sp_rename '_____document_folder_archived', 'document_folder_archived';
exec sp_rename '_____document_folder_deleted', 'document_folder_deleted';
exec sp_rename '_____document_historic', 'document_historic';
exec sp_rename '_____document_notification_group', 'document_notification_group';
exec sp_rename '_____document_review', 'document_review';
exec sp_rename '_____document_review_users', 'document_review_users';
exec sp_rename '_____document_rollback', 'document_rollback';
exec sp_rename '_____document_status', 'document_status';
exec sp_rename '_____document_tracked', 'document_tracked';
exec sp_rename '_____document_type', 'document_type';
exec sp_rename '_____document_version', 'document_version';
exec sp_rename '_____email_server', 'email_server';
exec sp_rename '_____favourite_property', 'favourite_property';
exec sp_rename '_____favourite_property_item', 'favourite_property_item';
exec sp_rename '_____group', 'group';
exec sp_rename '_____notification_group', 'notification_group';
exec sp_rename '_____permission', 'permission';
exec sp_rename '_____schedule_archive_folder', 'schedule_archive_folder';
exec sp_rename '_____schedule_delete_folder', 'schedule_delete_folder';
exec sp_rename '_____system_footer', 'system_footer';
exec sp_rename '_____system_menu', 'system_menu';
exec sp_rename '_____system_permission_level', 'system_permission_level';
exec sp_rename '_____system_permission_menu', 'system_permission_menu';
exec sp_rename '_____system_permission_submenu', 'system_permission_submenu';
exec sp_rename '_____system_settings', 'system_settings';
exec sp_rename '_____system_submenu', 'system_submenu';
exec sp_rename '_____system_updates', 'system_updates';
exec sp_rename '_____user', 'user';
exec sp_rename '_____user_event', 'user_event';
exec sp_rename '_____user_grid_size', 'user_grid_size';
exec sp_rename '_____user_group', 'user_group';
exec sp_rename '_____user_log', 'user_log';
exec sp_rename '_____user_preferences', 'user_preferences';
exec sp_rename '_____user_recent_document', 'user_recent_document';
exec sp_rename '_____user_workspace_customize', 'user_workspace_customize';
exec sp_rename '_____document_title_rule', 'document_title_rule';
exec sp_rename '_____dragdrop_attribute', 'dragdrop_attribute';
exec sp_rename '_____dragdrop_type', 'dragdrop_type';
exec sp_rename '_____permission_folder_group', 'permission_folder_group';
exec sp_rename '_____permission_folder_user', 'permission_folder_user';
exec sp_rename '_____schedule_notification_amended', 'schedule_notification_amended';
exec sp_rename '_____shell_behaviour', 'shell_behaviour';
exec sp_rename '_____system_version', 'system_version';
exec sp_rename '_____user_notification_group', 'user_notification_group';






select * into #attribute_combo_item from attribute_combo_item;
ALTER TABLE #attribute_combo_item ADD id2 INT;
update #attribute_combo_item set id2 = id;
ALTER TABLE #attribute_combo_item DROP COLUMN id;
ALTER TABLE attribute_combo_item DROP COLUMN id;
ALTER TABLE attribute_combo_item ADD id INT IDENTITY(1,1);
delete from attribute_combo_item;
SET IDENTITY_INSERT attribute_combo_item ON;
insert into attribute_combo_item ([id_atb],[value] ,[id]) select * from #attribute_combo_item;
SET IDENTITY_INSERT attribute_combo_item OFF;


-- attribute_combo_item_children


select * into #attribute_doc_type from attribute_doc_type;
ALTER TABLE #attribute_doc_type ADD id2 INT;
update #attribute_doc_type set id2 = id;
ALTER TABLE #attribute_doc_type DROP COLUMN id;
ALTER TABLE attribute_doc_type DROP COLUMN id;
ALTER TABLE attribute_doc_type ADD id INT IDENTITY(1,1);
delete from attribute_doc_type;
SET IDENTITY_INSERT attribute_doc_type ON;
insert into attribute_doc_type ( [id_doc_type],[id_attribute] ,[position] ,[duplicate_chk],[id]) select * from #attribute_doc_type;
SET IDENTITY_INSERT attribute_doc_type OFF;




select * into #attribute_field_type from attribute_field_type;
ALTER TABLE #attribute_field_type ADD id2 INT;
update #attribute_field_type set id2 = id;
ALTER TABLE #attribute_field_type DROP COLUMN id;
ALTER TABLE attribute_field_type DROP COLUMN id;
ALTER TABLE attribute_field_type ADD id INT IDENTITY(1,1);
delete from attribute_field_type;
SET IDENTITY_INSERT attribute_field_type ON;
insert into attribute_field_type ( [type] ,[id] ) select * from #attribute_field_type;
SET IDENTITY_INSERT attribute_field_type OFF;


select * into #attribute_params from attribute_params;
ALTER TABLE #attribute_params ADD id2 INT;
update #attribute_params set id2 = id;
ALTER TABLE #attribute_params DROP COLUMN id;
ALTER TABLE attribute_params DROP COLUMN id;
ALTER TABLE attribute_params ADD id INT IDENTITY(1,1);
delete from attribute_params;
SET IDENTITY_INSERT attribute_params ON;
insert into attribute_params ( [id_attribute],[id_folder],[required],[id] ) select * from #attribute_params;
SET IDENTITY_INSERT attribute_params OFF;


select * into #attributes from attributes;
ALTER TABLE #attributes ADD id2 INT;
update #attributes set id2 = id;
ALTER TABLE #attributes DROP COLUMN id;
ALTER TABLE attributes DROP COLUMN id;
ALTER TABLE attributes ADD id INT IDENTITY(1,1);
delete from attributes;
SET IDENTITY_INSERT attributes ON;
insert into attributes ( [name],[id_type],[position],[system_field],[period_research],[max_lengh],[only_numbers],[appear_query],[appear_input],[id] ) select * from #attributes;
SET IDENTITY_INSERT attributes OFF;



select * into #document from document;
ALTER TABLE #document ADD id2 INT;
update #document set id2 = id;
ALTER TABLE #document DROP COLUMN id;
ALTER TABLE document DROP COLUMN id;
ALTER TABLE document ADD id INT IDENTITY(1,1);
delete from document;
SET IDENTITY_INSERT document ON;
insert into document ( [title],[extension],[id_folder],[id_status],[id_user],[id_type],[id_sp_status],[id_review],[id_checkout_user],[id_latest_version],[created_date],[id] ) select * from #document;
SET IDENTITY_INSERT document OFF;



select * into #document_attribute from document_attribute;
ALTER TABLE #document_attribute ADD id2 INT;
update #document_attribute set id2 = id;
ALTER TABLE #document_attribute DROP COLUMN id;
ALTER TABLE document_attribute DROP COLUMN id;
ALTER TABLE document_attribute ADD id INT IDENTITY(1,1);
delete from document_attribute;
SET IDENTITY_INSERT document_attribute ON;
insert into document_attribute ( [id_doc],[id_atb],[atb_value],[id_field_type],[id] ) select * from #document_attribute;
SET IDENTITY_INSERT document_attribute OFF;



select * into #document_deleted from document_deleted;
ALTER TABLE #document_deleted ADD id2 INT;
update #document_deleted set id2 = id;
ALTER TABLE #document_deleted DROP COLUMN id;
ALTER TABLE document_deleted DROP COLUMN id;
ALTER TABLE document_deleted ADD id INT IDENTITY(1,1);
delete from document_deleted;
SET IDENTITY_INSERT document_deleted ON;
insert into document_deleted ( [id_doc],[id_user],[reason],[date],[id] ) select * from #document_deleted;
SET IDENTITY_INSERT document_deleted OFF;



select * into #document_event from document_event;
ALTER TABLE #document_event ADD id2 INT;
update #document_event set id2 = id;
ALTER TABLE #document_event DROP COLUMN id;
ALTER TABLE document_event DROP COLUMN id;
ALTER TABLE document_event ADD id INT IDENTITY(1,1);
delete from document_event;
SET IDENTITY_INSERT document_event ON;
insert into document_event ( [event],[id] ) select * from #document_event;
SET IDENTITY_INSERT document_event OFF;

select * into #document_folder from document_folder;
ALTER TABLE #document_folder ADD id2 INT;
update #document_folder set id2 = id;
ALTER TABLE #document_folder DROP COLUMN id;
ALTER TABLE document_folder DROP COLUMN id;
ALTER TABLE document_folder ADD id INT IDENTITY(1,1);
delete from document_folder;
SET IDENTITY_INSERT document_folder ON;
insert into document_folder ( [document_folder],[id_parent],[archived],[id] ) select * from #document_folder;
SET IDENTITY_INSERT document_folder OFF;


select * into #document_folder_archived from document_folder_archived;
ALTER TABLE #document_folder_archived ADD id2 INT;
update #document_folder_archived set id2 = id;
ALTER TABLE #document_folder_archived DROP COLUMN id;
ALTER TABLE document_folder_archived DROP COLUMN id;
ALTER TABLE document_folder_archived ADD id INT IDENTITY(1,1);
delete from document_folder_archived;
SET IDENTITY_INSERT document_folder_archived ON;
insert into document_folder_archived ([document_folder],[id_parent],[archived],[id] ) select * from #document_folder_archived;
SET IDENTITY_INSERT document_folder_archived OFF;




select * into #document_folder_deleted from document_folder_deleted;
ALTER TABLE #document_folder_deleted ADD id2 INT;
update #document_folder_deleted set id2 = id;
ALTER TABLE #document_folder_deleted DROP COLUMN id;
ALTER TABLE document_folder_deleted DROP COLUMN id;
ALTER TABLE document_folder_deleted ADD id int;
delete from document_folder_deleted;
insert into document_folder_deleted ([document_folder],[id_parent],[id] ) select * from #document_folder_deleted;




select * into #document_historic from document_historic;
ALTER TABLE #document_historic ADD id2 INT;
update #document_historic set id2 = id;
ALTER TABLE #document_historic DROP COLUMN id;
ALTER TABLE document_historic DROP COLUMN id;
ALTER TABLE document_historic ADD id INT IDENTITY(1,1);
delete from document_historic;
SET IDENTITY_INSERT document_historic ON;
insert into document_historic ([id_version],[id_event],[id_user],[date],[comments],[id] ) select * from #document_historic;
SET IDENTITY_INSERT document_historic OFF;



select * into #document_notification_group from document_notification_group;
ALTER TABLE #document_notification_group ADD id2 INT;
update #document_notification_group set id2 = id;
ALTER TABLE #document_notification_group DROP COLUMN id;
ALTER TABLE document_notification_group DROP COLUMN id;
ALTER TABLE document_notification_group ADD id INT IDENTITY(1,1);
delete from document_notification_group;
SET IDENTITY_INSERT document_notification_group ON;
insert into document_notification_group ([id_doc],[id_notification_group],[id] ) select * from #document_notification_group;
SET IDENTITY_INSERT document_notification_group OFF;

select * into #document_review from document_review;
ALTER TABLE #document_review ADD id2 INT;
update #document_review set id2 = id;
ALTER TABLE #document_review DROP COLUMN id;
ALTER TABLE document_review DROP COLUMN id;
ALTER TABLE document_review ADD id INT IDENTITY(1,1);
delete from document_review;
SET IDENTITY_INSERT document_review ON;
insert into document_review ([id_doc],[status],[deadline],[id_user],[comment],[id_version],[allow_checkout],[id] ) select * from #document_review;
SET IDENTITY_INSERT document_review OFF;


select * into #document_review_users from document_review_users;
ALTER TABLE #document_review_users ADD id2 INT;
update #document_review_users set id2 = id;
ALTER TABLE #document_review_users DROP COLUMN id;
ALTER TABLE document_review_users DROP COLUMN id;
ALTER TABLE document_review_users ADD id INT IDENTITY(1,1);
delete from document_review_users;
SET IDENTITY_INSERT document_review_users ON;
insert into document_review_users ([id_review],[id_user],[comment],[action],[id_version],[id] ) select * from #document_review_users;
SET IDENTITY_INSERT document_review_users OFF;


select * into #document_rollback from document_rollback;
ALTER TABLE #document_rollback ADD id2 INT;
update #document_rollback set id2 = id;
ALTER TABLE #document_rollback DROP COLUMN id;
ALTER TABLE document_rollback DROP COLUMN id;
ALTER TABLE document_rollback ADD id INT IDENTITY(1,1);
delete from document_rollback;
SET IDENTITY_INSERT document_rollback ON;
insert into document_rollback ([id_historic],[rollback_to],[id] ) select * from #document_rollback;
SET IDENTITY_INSERT document_rollback OFF;

select * into #document_status from document_status;
ALTER TABLE #document_status ADD id2 INT;
update #document_status set id2 = id;
ALTER TABLE #document_status DROP COLUMN id;
ALTER TABLE document_status DROP COLUMN id;
ALTER TABLE document_status ADD id INT IDENTITY(1,1);
delete from document_status;
SET IDENTITY_INSERT document_status ON;
insert into document_status ([status],[id] ) select * from #document_status;
SET IDENTITY_INSERT document_status OFF;



select * into #document_tracked from document_tracked;
ALTER TABLE #document_tracked ADD id2 INT;
update #document_tracked set id2 = id;
ALTER TABLE #document_tracked DROP COLUMN id;
ALTER TABLE document_tracked DROP COLUMN id;
ALTER TABLE document_tracked ADD id INT IDENTITY(1,1);
delete from document_tracked;
SET IDENTITY_INSERT document_tracked ON;
insert into document_tracked ([id_doc],[id_user],[fired],[id] ) select * from #document_tracked;
SET IDENTITY_INSERT document_tracked OFF;

select * into #document_type from document_type;
ALTER TABLE #document_type ADD id2 INT;
update #document_type set id2 = id;
ALTER TABLE #document_type DROP COLUMN id;
ALTER TABLE document_type DROP COLUMN id;
ALTER TABLE document_type ADD id INT IDENTITY(1,1);
delete from document_type;
SET IDENTITY_INSERT document_type ON;
insert into document_type ([type],[id] ) select * from #document_type;
SET IDENTITY_INSERT document_type OFF;

select * into #document_version from document_version;
ALTER TABLE #document_version ADD id2 INT;
update #document_version set id2 = id;
ALTER TABLE #document_version DROP COLUMN id;
ALTER TABLE document_version DROP COLUMN id;
ALTER TABLE document_version ADD id INT IDENTITY(1,1);
delete from document_version;
SET IDENTITY_INSERT document_version ON;
insert into document_version ([id_doc],[version],[extension],[reason],[id_historic],[filesize],[filedata],[id] ) select * from #document_version;
SET IDENTITY_INSERT document_version OFF;




From here




select * into #dragdrop_attribute from dragdrop_attribute;
ALTER TABLE #dragdrop_attribute ADD id2 INT;
update #dragdrop_attribute set id2 = id;
ALTER TABLE #dragdrop_attribute DROP COLUMN id;
ALTER TABLE dragdrop_attribute DROP COLUMN id;
ALTER TABLE dragdrop_attribute ADD id INT IDENTITY(1,1);
delete from dragdrop_attribute;
SET IDENTITY_INSERT dragdrop_attribute ON;
insert into dragdrop_attribute ([id_folder],[id_atb],[value_from],[id] ) select * from #dragdrop_attribute;
SET IDENTITY_INSERT dragdrop_attribute OFF;




select * into #dragdrop_type from dragdrop_type;
ALTER TABLE #dragdrop_type ADD id2 INT;
update #dragdrop_type set id2 = id;
ALTER TABLE #dragdrop_type DROP COLUMN id;
ALTER TABLE dragdrop_type DROP COLUMN id;
ALTER TABLE dragdrop_type ADD id INT IDENTITY(1,1);
delete from dragdrop_type;
SET IDENTITY_INSERT dragdrop_type ON;
insert into dragdrop_type ([id_folder],[id_type],[id] ) select * from #dragdrop_type;
SET IDENTITY_INSERT dragdrop_type OFF;




select * into #favourite_property from favourite_property;
ALTER TABLE #favourite_property ADD id2 INT;
update #favourite_property set id2 = id;
ALTER TABLE #favourite_property DROP COLUMN id;
ALTER TABLE favourite_property DROP COLUMN id;
ALTER TABLE favourite_property ADD id INT IDENTITY(1,1);
delete from favourite_property;
SET IDENTITY_INSERT favourite_property ON;
insert into favourite_property ([id_user],[id_folder],[id_doc_type],[id] ) select * from #favourite_property;
SET IDENTITY_INSERT favourite_property OFF;


select * into #favourite_property_item from favourite_property_item;
ALTER TABLE #favourite_property_item ADD id2 INT;
update #favourite_property_item set id2 = id;
ALTER TABLE #favourite_property_item DROP COLUMN id;
ALTER TABLE favourite_property_item DROP COLUMN id;
ALTER TABLE favourite_property_item ADD id INT IDENTITY(1,1);
delete from favourite_property_item;
SET IDENTITY_INSERT favourite_property_item ON;
insert into favourite_property_item ([id_favourite_propery],[id_atb],[atb_value],[id] ) select * from #favourite_property_item;
SET IDENTITY_INSERT favourite_property_item OFF;



select * into #group from [group];
ALTER TABLE #group ADD id2 INT;
update #group set id2 = id;
ALTER TABLE #group DROP COLUMN id;
ALTER TABLE [group] DROP COLUMN id;
ALTER TABLE [group] ADD id INT IDENTITY(1,1);
delete from [group];
SET IDENTITY_INSERT [group] ON;
insert into [group] ([group_name],[obs],[ordination],[is_admin],[id] ) select * from #group;
SET IDENTITY_INSERT [group] OFF;



select * into #notification_group from notification_group;
ALTER TABLE #notification_group ADD id2 INT;
update #notification_group set id2 = id;
ALTER TABLE #notification_group DROP COLUMN id;
ALTER TABLE notification_group DROP COLUMN id;
ALTER TABLE notification_group ADD id INT IDENTITY(1,1);
delete from notification_group;
SET IDENTITY_INSERT notification_group ON;
insert into notification_group ([group_name],[id] ) select * from #notification_group;
SET IDENTITY_INSERT notification_group OFF;



select * into #permission from permission;
ALTER TABLE #permission ADD id2 INT;
update #permission set id2 = id;
ALTER TABLE #permission DROP COLUMN id;
ALTER TABLE permission DROP COLUMN id;
ALTER TABLE permission ADD id INT IDENTITY(1,1);
delete from permission;
SET IDENTITY_INSERT permission ON;
insert into permission ([permission],[sort],[id] ) select * from #permission;
SET IDENTITY_INSERT permission OFF;



select * into #permission_folder_group from permission_folder_group;
ALTER TABLE #permission_folder_group ADD id2 INT;
update #permission_folder_group set id2 = id;
ALTER TABLE #permission_folder_group DROP COLUMN id;
ALTER TABLE permission_folder_group DROP COLUMN id;
ALTER TABLE permission_folder_group ADD id INT IDENTITY(1,1);
delete from permission_folder_group;
SET IDENTITY_INSERT permission_folder_group ON;
insert into permission_folder_group ([id_folder],[id_group],[id_permission],[allow],[deny],[id] ) select * from #permission_folder_group;
SET IDENTITY_INSERT permission_folder_group OFF;


select * into #permission_folder_user from permission_folder_user;
ALTER TABLE #permission_folder_user ADD id2 INT;
update #permission_folder_user set id2 = id;
ALTER TABLE #permission_folder_user DROP COLUMN id;
ALTER TABLE permission_folder_user DROP COLUMN id;
ALTER TABLE permission_folder_user ADD id INT IDENTITY(1,1);
delete from permission_folder_user;
SET IDENTITY_INSERT permission_folder_user ON;
insert into permission_folder_user ([id_folder],[id_user],[id_permission],[allow],[deny],[id] ) select * from #permission_folder_user;
SET IDENTITY_INSERT permission_folder_user OFF;



-- schedule_archive_folder

-- schedule_delete_folder



select * into #schedule_notification_amended from schedule_notification_amended;
ALTER TABLE #schedule_notification_amended ADD id2 INT;
update #schedule_notification_amended set id2 = id;
ALTER TABLE #schedule_notification_amended DROP COLUMN id;
ALTER TABLE schedule_notification_amended DROP COLUMN id;
ALTER TABLE schedule_notification_amended ADD id INT IDENTITY(1,1);
delete from schedule_notification_amended;
SET IDENTITY_INSERT schedule_notification_amended ON;
insert into schedule_notification_amended ([id_doc],[new_version],[id]) select * from #schedule_notification_amended;
SET IDENTITY_INSERT schedule_notification_amended OFF;



select * into #shell_behaviour from shell_behaviour;
ALTER TABLE #shell_behaviour ADD id2 INT;
update #shell_behaviour set id2 = id;
ALTER TABLE #shell_behaviour DROP COLUMN id;
ALTER TABLE shell_behaviour DROP COLUMN id;
ALTER TABLE shell_behaviour ADD id INT IDENTITY(1,1);
delete from shell_behaviour;
SET IDENTITY_INSERT shell_behaviour ON;
insert into shell_behaviour ([extension],[override_behaviour],[id]) select * from #shell_behaviour;
SET IDENTITY_INSERT shell_behaviour OFF;


select * into #system_footer from system_footer;
ALTER TABLE #system_footer ADD id2 INT;
update #system_footer set id2 = id;
ALTER TABLE #system_footer DROP COLUMN id;
ALTER TABLE system_footer DROP COLUMN id;
ALTER TABLE system_footer ADD id INT IDENTITY(1,1);
delete from system_footer;
SET IDENTITY_INSERT system_footer ON;
insert into system_footer ([field_id],[field_type],[id]) select * from #system_footer;
SET IDENTITY_INSERT system_footer OFF;




select * into #system_menu from system_menu;
ALTER TABLE #system_menu ADD id2 INT;
update #system_menu set id2 = id;
ALTER TABLE #system_menu DROP COLUMN id;
ALTER TABLE system_menu DROP COLUMN id;
ALTER TABLE system_menu ADD id INT IDENTITY(1,1);
delete from system_menu;
SET IDENTITY_INSERT system_menu ON;
insert into system_menu ([menu],[menu_internal_name],[id]) select * from #system_menu;
SET IDENTITY_INSERT system_menu OFF;

select * into #system_permission_level from system_permission_level;
ALTER TABLE #system_permission_level ADD id2 INT;
update #system_permission_level set id2 = id;
ALTER TABLE #system_permission_level DROP COLUMN id;
ALTER TABLE system_permission_level DROP COLUMN id;
ALTER TABLE system_permission_level ADD id INT IDENTITY(1,1);
delete from system_permission_level;
SET IDENTITY_INSERT system_permission_level ON;
insert into system_permission_level ([permission],[obs],[id]) select * from #system_permission_level;
SET IDENTITY_INSERT system_permission_level OFF;


select * into #system_permission_menu from system_permission_menu;
ALTER TABLE #system_permission_menu ADD id2 INT;
update #system_permission_menu set id2 = id;
ALTER TABLE #system_permission_menu DROP COLUMN id;
ALTER TABLE system_permission_menu DROP COLUMN id;
ALTER TABLE system_permission_menu ADD id INT IDENTITY(1,1);
delete from system_permission_menu;
SET IDENTITY_INSERT system_permission_menu ON;
insert into system_permission_menu ([id_permission],[id_menu],[id]) select * from #system_permission_menu;
SET IDENTITY_INSERT system_permission_menu OFF;




select * into #system_permission_submenu from system_permission_submenu;
ALTER TABLE #system_permission_submenu ADD id2 INT;
update #system_permission_submenu set id2 = id;
ALTER TABLE #system_permission_submenu DROP COLUMN id;
ALTER TABLE system_permission_submenu DROP COLUMN id;
ALTER TABLE system_permission_submenu ADD id INT IDENTITY(1,1);
delete from system_permission_submenu;
SET IDENTITY_INSERT system_permission_submenu ON;
insert into system_permission_submenu ([id_permission],[id_submenu],[id]) select * from #system_permission_submenu;
SET IDENTITY_INSERT system_permission_submenu OFF;

-- system_settings

select * into #system_submenu from system_submenu;
ALTER TABLE #system_submenu ADD id2 INT;
update #system_submenu set id2 = id;
ALTER TABLE #system_submenu DROP COLUMN id;
ALTER TABLE system_submenu DROP COLUMN id;
ALTER TABLE system_submenu ADD id INT IDENTITY(1,1);
delete from system_submenu;
SET IDENTITY_INSERT system_submenu ON;
insert into system_submenu ([submenu],[id_menu],[submenu_internal_name],[id_order],[id]) select * from #system_submenu;
SET IDENTITY_INSERT system_submenu OFF;


select * into #system_updates from system_updates;
ALTER TABLE #system_updates ADD id2 INT;
update #system_updates set id2 = id;
ALTER TABLE #system_updates DROP COLUMN id;
ALTER TABLE system_updates DROP COLUMN id;
ALTER TABLE system_updates ADD id INT IDENTITY(1,1);
delete from system_updates;
SET IDENTITY_INSERT system_updates ON;
insert into system_updates ([version],[file_data],[date],[id]) select * from #system_updates;
SET IDENTITY_INSERT system_updates OFF;

-- system_version



select * into #user from [user];
ALTER TABLE #user ADD id2 INT;
update #user set id2 = id;
ALTER TABLE #user DROP COLUMN id;
ALTER TABLE [user] DROP COLUMN id;
ALTER TABLE [user] ADD id INT IDENTITY(1,1);
delete from [user];
SET IDENTITY_INSERT [user] ON;
insert into [user] ([login],[name],[password],[active],[id_permission],[email],[reviewer],[id]) select * from #user;
SET IDENTITY_INSERT [user] OFF;


select * into #user_event from user_event;
ALTER TABLE #user_event ADD id2 INT;
update #user_event set id2 = id;
ALTER TABLE #user_event DROP COLUMN id;
ALTER TABLE user_event DROP COLUMN id;
ALTER TABLE user_event ADD id INT IDENTITY(1,1);
delete from user_event;
SET IDENTITY_INSERT user_event ON;
insert into user_event ([event],[id]) select * from #user_event;
SET IDENTITY_INSERT user_event OFF;

-- user_grid_size

select * into #user_group from user_group;
ALTER TABLE #user_group ADD id2 INT;
update #user_group set id2 = id;
ALTER TABLE #user_group DROP COLUMN id;
ALTER TABLE user_group DROP COLUMN id;
ALTER TABLE user_group ADD id INT IDENTITY(1,1);
delete from user_group;
SET IDENTITY_INSERT user_group ON;
insert into user_group ([id_user],[id_group],[id]) select * from #user_group;
SET IDENTITY_INSERT user_group OFF;



select * into #user_log from user_log;
ALTER TABLE #user_log ADD id2 INT;
update #user_log set id2 = id;
ALTER TABLE #user_log DROP COLUMN id;
ALTER TABLE user_log DROP COLUMN id;
ALTER TABLE user_log ADD id INT IDENTITY(1,1);
delete from user_log;
SET IDENTITY_INSERT user_log ON;
insert into user_log ([id_user],[id_event],[obs],[date],[id]) select * from #user_log;
SET IDENTITY_INSERT user_log OFF;


select * into #user_notification_group from user_notification_group;
ALTER TABLE #user_notification_group ADD id2 INT;
update #user_notification_group set id2 = id;
ALTER TABLE #user_notification_group DROP COLUMN id;
ALTER TABLE user_notification_group DROP COLUMN id;
ALTER TABLE user_notification_group ADD id INT IDENTITY(1,1);
delete from user_notification_group;
SET IDENTITY_INSERT user_notification_group ON;
insert into user_notification_group ([id_key],[key_type],[id_notification_group],[id]) select * from #user_notification_group;
SET IDENTITY_INSERT user_notification_group OFF;


-- user_preferences


-- user_recent_document

-- user_workspace_customize


create VIEW [dbo].[view_document] AS SELECT     d.id, d.id_latest_version AS id_version, d.id_user, d.id_folder, d.title, d.extension, u.name, f.document_folder, d.id_status, 'V ' + CAST(dv.version AS varchar(3)) AS version, d.id_type, dt.type,                        dh.date, d.id_review, d.id_checkout_user,tCheckOutUser.name AS CheckOutUser, d.id_sp_status, d.created_date FROM         dbo.[document] AS d INNER JOIN                       dbo.[user] AS u ON d.id_user = u.id INNER JOIN                       dbo.document_version AS dv ON d.id = dv.id_doc AND d.id_latest_version = dv.id INNER JOIN                       dbo.document_historic AS dh ON dv.id_historic = dh.id INNER JOIN                       dbo.document_folder AS f ON d.id_folder = f.id LEFT OUTER JOIN                       dbo.document_type AS dt ON d.id_type = dt.id LEFT OUTER JOIN                       dbo.[user] AS tCheckOutUser ON d.id_checkout_user = tCheckOutUser.id WHERE     (d.id_status NOT IN (5))


create view view_document_attribute as
SELECT        attr_val.id, attr_val.id_doc, d.title, d.id_type, attr_val.id_atb, attr_val.atb_value, attr_val.atb_value_text, attr.id_type AS id_field_type, attr.system_field, d.id_status, d.id_folder, f.archived AS folder_archived
FROM            (SELECT        id, id_doc, id_atb, atb_value, atb_value AS atb_value_text
                          FROM            dbo.document_attribute AS da
                          WHERE        (id_field_type IN (1, 2, 3, 5, 7)) AND (id_atb < 1000)
                          UNION
                          SELECT        da.id, da.id_doc, da.id_atb, da.atb_value, i.value AS atb_value_text
                          FROM            dbo.document_attribute AS da INNER JOIN
                                                   dbo.attribute_combo_item AS i ON da.atb_value = CAST(i.id AS varchar)
                          WHERE        (da.id_field_type IN (4, 8, 10, 11)) AND (da.id_atb < 1000)) AS attr_val INNER JOIN
                         dbo.[document] AS d ON attr_val.id_doc = d.id INNER JOIN
                         dbo.attributes AS attr ON attr_val.id_atb = attr.id INNER JOIN
                         dbo.document_folder AS f ON d.id_folder = f.id
                         ;

create view view_document_attribute_value as
SELECT        attr_val.id, attr_val.id_doc, attr_val.id_atb, attr_val.atb_value, attr.id_type AS id_field_type, attr.system_field, d.id_status, d.id_folder
FROM            (SELECT        id, id_doc, id_atb, atb_value
                          FROM            dbo.document_attribute AS da
                          WHERE        (id_field_type IN (1, 3, 7)) AND (id_atb < 1000)
                          UNION
                          SELECT        da.id, da.id_doc, da.id_atb, i.value
                          FROM            dbo.document_attribute AS da INNER JOIN
                                                   dbo.attribute_combo_item AS i ON da.atb_value = CAST(i.id AS varchar)
                          WHERE        (da.id_field_type IN (4, 8, 10, 11)) AND (da.id_atb < 1000)) AS attr_val INNER JOIN
                         dbo.[document] AS d ON attr_val.id_doc = d.id INNER JOIN
                         dbo.attributes AS attr ON attr_val.id_atb = attr.id;


create view view_document_group as
SELECT DISTINCT dh.id, dv.id_doc, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.event, dv.extension AS type, dh.comments, dv.reason, ng.id AS id_notification_group, ng.group_name, doc.id_sp_status, dv.extension
FROM            dbo.[document] AS doc INNER JOIN
                         dbo.document_version AS dv ON dv.id = doc.id_latest_version INNER JOIN
                         dbo.document_historic AS dh ON dh.id_version = dv.id INNER JOIN
                         dbo.document_event AS de ON de.id = dh.id_event INNER JOIN
                         dbo.document_notification_group AS dng ON dv.id_doc = dng.id_doc LEFT OUTER JOIN
                         dbo.notification_group AS ng ON ng.id = dng.id_notification_group LEFT OUTER JOIN
                         dbo.[user] AS u ON dh.id_user = u.id
WHERE        (de.event = 'Saved as New Version' OR
                         de.event = 'Created' OR
                         de.event = 'Import' OR
                         de.event = 'New version' OR
                         de.event = 'Scanned') AND (doc.id_status IN (1, 2, 3))


create view view_document_search as
SELECT        d.id, d.id_latest_version, d.id_user, d.id_folder, d.title, d.extension, u.name, f.document_folder, d.id_status, dv_fulltext.version, d.id_type, dt.type, dh.date, d.id_review, d.id_sp_status, d.created_date, d.id_checkout_user,
                         dv_fulltext.filesize, atb_mail_subject.atb_value AS mail_subject, atb_mail_from.atb_value AS mail_from, atb_mail_to.atb_value AS mail_to, atb_mail_time.atb_value AS mail_time,
                         atb_mail_is_composed.atb_value AS mail_is_composed
FROM            dbo.[document] AS d WITH (NOLOCK) INNER JOIN
                         dbo.[user] AS u WITH (NOLOCK) ON d.id_user = u.id INNER JOIN
                         dbo.document_version AS dv_fulltext WITH (NOLOCK) ON dv_fulltext.id = d.id_latest_version INNER JOIN
                         dbo.document_historic AS dh WITH (NOLOCK) ON dv_fulltext.id_historic = dh.id INNER JOIN
                         dbo.document_folder AS f WITH (NOLOCK) ON d.id_folder = f.id LEFT OUTER JOIN
                         dbo.document_type AS dt WITH (NOLOCK) ON d.id_type = dt.id LEFT OUTER JOIN
                         dbo.[user] AS tCheckOutUser WITH (NOLOCK) ON d.id_checkout_user = tCheckOutUser.id LEFT OUTER JOIN
                         dbo.document_attribute AS atb_mail_subject WITH (NOLOCK) ON d.id = atb_mail_subject.id_doc AND atb_mail_subject.id_atb = 10000 LEFT OUTER JOIN
                         dbo.document_attribute AS atb_mail_from WITH (NOLOCK) ON d.id = atb_mail_from.id_doc AND atb_mail_from.id_atb = 10001 LEFT OUTER JOIN
                         dbo.document_attribute AS atb_mail_to WITH (NOLOCK) ON d.id = atb_mail_to.id_doc AND atb_mail_to.id_atb = 10003 LEFT OUTER JOIN
                         dbo.document_attribute AS atb_mail_time WITH (NOLOCK) ON d.id = atb_mail_time.id_doc AND atb_mail_time.id_atb = 10002 LEFT OUTER JOIN
                         dbo.document_attribute AS atb_mail_is_composed WITH (NOLOCK) ON d.id = atb_mail_is_composed.id_doc AND atb_mail_is_composed.id_atb = 10004;

create view view_extension as
SELECT DISTINCT extension
FROM            dbo.[document];


create view view_historic as
SELECT        dh.id, dv.id_doc, dh.id_version, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.id AS id_event, de.event, dr.rollback_to, dv.extension AS type, dh.comments, dv.reason
FROM            dbo.document_historic AS dh INNER JOIN
                         dbo.document_version AS dv ON dh.id_version = dv.id INNER JOIN
                         dbo.[document] AS doc ON dv.id_doc = doc.id INNER JOIN
                         dbo.document_event AS de ON de.id = dh.id_event LEFT OUTER JOIN
                         dbo.document_rollback AS dr ON dh.id = dr.id_historic INNER JOIN
                         dbo.[user] AS u ON dh.id_user = u.id;

create view view_notification_amended as
with user_cte (id,name,email,type)
as
(
	select id,name,email,type from (
		select u.id,u.name,u.email,2 as type from user_group as g inner join [user] as u on g.id_user = g.id where u.active =1
		union all
		select id,name,email,1 type from [user] as u where  active =1
	) as newuser
)
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
from document as d
inner join document_version v on v.id_doc = d.id
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join notification_group AS ng ON ng.id = dng.id_notification_group
inner join user_notification_group AS ung ON ung.id_notification_group = dng.id_notification_group
inner join [user_cte] as u ON u.id = ung.id_key and u.type = ung.key_type
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
from document as d
inner join document_version v on v.id_doc = d.id
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join [user] as u ON u.active = 1
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version
where de.event = 'Saved as New Version' and dng.id_notification_group = 1




create view view_permission_folder_group as
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder, fp.id_group
FROM            dbo.user_group AS ug WITH (nolock) INNER JOIN
                         dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group

create view view_user_folder_permission as
SELECT        id_user, id_folder, id_permission, allow
FROM            (SELECT        id_user, id_folder, id_permission, allow
                          FROM            dbo.permission_folder_user AS u
                          WHERE        (allow = 1) AND ([deny] = 0)
                          UNION
                          SELECT        id_user, id_folder, id_permission, allow
                          FROM            dbo.view_permission_folder_group AS g
                          WHERE        (allow = 1) AND ([deny] = 0) AND (NOT EXISTS
                                                       (SELECT        id_permission
                                                         FROM            dbo.view_permission_folder_group
                                                         WHERE        (id_user = g.id_user) AND (id_folder = g.id_folder) AND ([deny] = 1) AND (id_permission = g.id_permission)))) AS your_permissions
WHERE        (NOT EXISTS
                             (SELECT        id_permission
                               FROM            dbo.permission_folder_user AS u
                               WHERE        ([deny] = 1) AND (id_permission = your_permissions.id_permission) AND (id_user = your_permissions.id_user) AND (id_folder = your_permissions.id_folder)))


create view view_user_log as
SELECT        id_user, name, event, date, id_doc, version, obs, frm
FROM            (SELECT        u.id AS id_user, u.name, ue.event, ul.date, '' AS id_doc, '' AS version, ul.obs, '1' AS frm
                          FROM            dbo.user_log AS ul INNER JOIN
                                                    dbo.[user] AS u ON ul.id_user = u.id INNER JOIN
                                                    dbo.user_event AS ue ON ul.id_event = ue.id
                          UNION
                          SELECT        id_user, name, event, date, id_doc, version, '' AS obs, '2' AS frm
                          FROM            dbo.view_historic) AS tbl;


create view view_version as
SELECT        dv.id_doc, dv.id, dv.version, u.name, dh.date, dv.reason, dh.comments, de.id AS id_event, de.event
FROM            dbo.document_version AS dv INNER JOIN
                         dbo.document_historic AS dh ON dv.id_historic = dh.id INNER JOIN
                         dbo.document_event AS de ON de.id = dh.id_event INNER JOIN
                         dbo.[user] AS u ON dh.id_user = u.id;





CREATE VIEW [dbo].[view_inherited_folder]
AS
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder,fp.id_group
FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group;







create PROCEDURE [dbo].[addFolderAllPermissionGroup] @id_folder INT = 0,                                                     @id_group INT = 1 AS  begin  SET nocount ON;      declare @id_permission int;      declare cur CURSOR LOCAL for select id from permission;     open cur     fetch next from cur into @id_permission;      while @@FETCH_STATUS = 0 BEGIN         exec  addFolderPermissionGroup @id_folder = @id_folder,                                          @id_group = @id_group,                                          @id_permission = @id_permission,                                         @allow = 1,                                         @deny =0                  fetch next from cur into @id_permission;     END      close cur;     deallocate cur; end



create PROCEDURE [dbo].[addFolderPermissionGroup] @id_folder INT = 0,                                                     @id_group INT = 0,                                                     @id_permission INT = 0,                                                    @allow int = 0,                                                    @deny int =0 AS    SET nocount ON;           /* remove first */     if( @id_permission > 0)         begin                   delete from permission_folder_group where id_folder = @id_folder and id_group = @id_group and id_permission = @id_permission;                  end      else          begin             delete from permission_folder_group where id_folder = @id_folder and id_group = @id_group;         end        if( @id_permission > 0)         begin               INSERT INTO permission_folder_group ( 						id_folder,                         id_group,                          id_permission,                         [allow],                         [deny] ) SELECT  									@id_folder,                                     @id_group,                                      id,                                      @allow,                                      @deny                                 FROM [permission] where id = @id_permission;           end      else          begin             INSERT INTO permission_folder_group ( 						id_folder,                         id_group,                          id_permission,                         [allow],                         [deny] ) SELECT  									@id_folder,                                     @id_group,                                      id,                                      @allow,                                      @deny                                 FROM [permission];          end

create PROCEDURE [dbo].[ArchiveFolder] @id_folder int AS BEGIN     set identity_insert document_folder_archived on;     CREATE TABLE #scheduled_folder     (         id int not null default 0,         document_folder varchar(100) not null default '',         id_parent int not null default ''     )      declare @id_folder_cur int;      insert into #scheduled_folder Exec [dbo].[drillDownFoldersBy] @id_parent = @id_folder;      declare cur CURSOR LOCAL for select id from #scheduled_folder;     open cur     fetch next from cur into @id_folder_cur;      while @@FETCH_STATUS = 0 BEGIN          /*         delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder_cur) ;         delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder_cur) ;          delete from folder_group where id_folder = @id_folder_cur;         delete from folder_user where id_folder = @id_folder_cur;         delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder_cur);         */         exec RmFolderPermission @id_folder = @id_folder_cur;                  update document_folder set archived = 1 where id = @id_folder_cur;                  insert into document_folder_archived (id,document_folder,id_parent,archived) select id,document_folder,id_parent,archived from document_folder where archived = 1 and id = @id_folder_cur;          fetch next from cur into @id_folder_cur;     END      close cur;     deallocate cur; END


create PROCEDURE [dbo].[archiveFolderDrillDownL1]  								@id_parent int = 0 AS       SET TRANSACTION isolation level READ uncommitted;   IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;  select id,document_folder,id_parent from document_folder_archived where id_parent = @id_parent 	union all select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder_archived where id_parent in (select id from document_folder_archived where id_parent = @id_parent)

create PROCEDURE [dbo].[canUnCheckDuplicate] 											@title varchar(max) = '', 											@id_type int = 0, 											@id_atbs varchar(max) = '' AS  /* declare @title varchar(max) = 'Job1000.pdf'; declare @id_type int = 49; declare @id_atbs varchar(max) = '3,4'; declare @val_atbs varchar(max) = '40,10000'; declare @excld_id_doc int = 0 ; */ declare @cnt int = 0 ;  IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp; IF OBJECT_ID('tempdb..#tmp2') IS NOT NULL DROP TABLE #tmp2;  create table #tmp ( 	id_doc int, 	title varchar(max), 	atb_value varchar(max) );  /*care atb, title, type*/ insert into #tmp select  virtual_attribute.id_doc,d.title,virtual_attribute.atb_value_joined  from ( 	select distinct id_doc , ( 		SELECT 			atb_value + ',' as [text()] 			FROM [dbo].[document_attribute] where id_doc = a.id_doc and id_atb in (select item from dbo.fnSplit(@id_atbs, ',')) 		order by id_atb for xml path('') 	)  as atb_value_joined 	from document_attribute as a		 ) as virtual_attribute  	right join document as d on d.id = virtual_attribute.id_doc 		where 		d.id_type = @id_type 		and d.id_status not in (4,5) and id_latest_version > -1 		and exists(select * from document_folder where id = d.id_folder and archived = 0 );  select top 1 @cnt = count(atb_value) from #tmp group by title,atb_value order by count(atb_value) desc;  /*no type duplication check made*/ /* if @id_atbs = '' begin 	--select top 1 @cnt = count(title) from document where id_type = @id_type and id_status not in (4,5) group by id_type, title order by count(title) desc; 	select top 1 @cnt = count(title) from document where id_status not in (4,5) group by id_folder, title order by count(title) desc; end  */  select @cnt as ans


create PROCEDURE [dbo].[cnvDocumentTitle]
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



create PROCEDURE [dbo].[DeleteDocsByFolder] @id_folder int AS BEGIN     CREATE TABLE #scheduled_folder     (         id int not null default 0,         document_folder varchar(100) not null default '',         id_parent int not null default ''     )      declare @id_folder_cur int;      insert into #scheduled_folder Exec [dbo].[drillDownFoldersBy] @id_parent = @id_folder;      declare cur CURSOR LOCAL for select id from #scheduled_folder;     open cur     fetch next from cur into @id_folder_cur;      while @@FETCH_STATUS = 0 BEGIN                   delete from document_attribute where exists (select * from document where id = document_attribute.id_doc and id_folder = @id_folder_cur);         delete from document_deleted where exists (select * from document where id = document_deleted.id_doc and id_folder = @id_folder_cur);         delete from document_historic where exists (select * from document_version as dv where id = document_historic.id_version and exists(select * from document where id = dv.id_doc and id_folder = @id_folder_cur));         delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder_cur) ;         delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder_cur) ;          delete from folder_group where id_folder = @id_folder_cur;         delete from folder_user where id_folder = @id_folder_cur;          delete from document_review_users where exists (select * from document_review as dv where id = document_review_users.id_review and exists(select * from document where id = id_doc and id_folder = @id_folder_cur)) ;         delete from document_review where exists (select * from document where id = document_review.id_doc and id_folder = @id_folder_cur);                  delete from document_tracked where exists (select * from document where id = document_tracked.id_doc and id_folder = @id_folder_cur);          delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder_cur);          delete from document where id_folder = @id_folder_cur;          fetch next from cur into @id_folder_cur;     END      close cur;     deallocate cur; END


create PROCEDURE [dbo].[DeleteDocument] @id_doc int, @id_user int, @reason varchar(200) AS BEGIN  	begin tran t1;  	update document set id_status = 5 where id = @id_doc ;  	insert into document_deleted (id_doc,id_user,reason,date) values (@id_doc,@id_user,@reason,getdate());  	commit tran t1; END


create PROCEDURE [dbo].[dragDropAttribute] 								@id_folder int = 0 AS  SET TRANSACTION isolation level READ uncommitted;  IF OBJECT_ID('tempdb..#ans') IS NOT NULL DROP TABLE #ans; IF OBJECT_ID('tempdb..#drillup') IS NOT NULL DROP TABLE #drillup;  create table #ans (     id_folder int default 0,     id_atb int default 0,     value_from varchar(200) default '',     value_taken varchar(200) default '' );  create table #drillup (     id int default 0,     document_folder varchar(250) default 0,     id_parent int default 0,     archived int default 0, );  declare @cur_id_folder int; declare @cur2_id_atb int; declare @cur2_value_from varchar(max); declare @_cur2_id_atb int; declare @_cur2_value_from varchar(max); declare @cur_document_folder varchar(max);  declare @prev_folder_name varchar(max) = '';  /*set @prev_folder_name = '##WRONG_SETTING##';*/ set @prev_folder_name = '';  insert into #drillup exec drillUpfoldersby @id = @id_folder, @id_user = 1;  /*select top 1  @prev_folder_name = document_folder from document_folder where id_parent = @id_folder;*/  declare cur CURSOR local for select id, document_folder from #drillup; open cur;  fetch next from cur into @cur_id_folder, @cur_document_folder;  while @@FETCH_STATUS = 0 begin      declare cur2 CURSOR local for select id_atb, value_from from dragdrop_attribute where id_folder = @cur_id_folder;     open cur2;      fetch next from cur2 into @cur2_id_atb, @cur2_value_from;      while @@FETCH_STATUS = 0 begin         set @_cur2_id_atb = @cur2_id_atb;         set @_cur2_value_from = @cur2_value_from; 		 		/*First fetch so that you can use continue command in anywhere*/         fetch next from cur2 into @cur2_id_atb, @cur2_value_from;          /*take nearst folder logic*/         if exists( select * from #ans where id_atb = @_cur2_id_atb)         begin             continue;         end          /*value from folder name. This applies next to start @id_folder.*/         if (@_cur2_value_from = '##CHILD-FOLDER-NAME##'  and @cur_id_folder <> @id_folder)         begin             insert into #ans (id_folder,id_atb,value_from,value_taken) values (@cur_id_folder,@_cur2_id_atb,@_cur2_value_from,@prev_folder_name);             continue;         end 		 		if( left(@_cur2_value_from,2) <> '##' ) 		begin             insert into #ans (id_folder,id_atb,value_from,value_taken) values (@cur_id_folder,@_cur2_id_atb,'LITERAL',@_cur2_value_from);             continue; 		end     end      close cur2     deallocate cur2  	set @prev_folder_name = @cur_document_folder;  	fetch next from cur into @cur_id_folder, @cur_document_folder;  end  close cur deallocate cur  select id_folder,id_atb,value_from,value_taken from #ans;



create PROCEDURE [dbo].[dragDropType] 								@id_folder int = 0 AS  SET TRANSACTION isolation level READ uncommitted;  IF OBJECT_ID('tempdb..#ans') IS NOT NULL DROP TABLE #ans; IF OBJECT_ID('tempdb..#drillup') IS NOT NULL DROP TABLE #drillup;  create table #ans (     id int default 0,     id_folder int default 0,     id_type int default 0 );  create table #drillup (     id int default 0,     document_folder varchar(250) default 0,     id_parent int default 0,     archived int default 0, );  declare @cur_id_folder int; declare @id int;  insert into #drillup exec drillUpfoldersby @id = @id_folder, @id_user = 1;  declare cur CURSOR local for select id from #drillup; open cur;  fetch next from cur into @cur_id_folder;  while @@FETCH_STATUS = 0 begin      select @id = id from dragdrop_type where id_folder = @cur_id_folder;      if exists( select * from dragdrop_type where id = @id)     begin         insert into #ans (id,id_folder,id_type) select id,id_folder,id_type from dragdrop_type where id = @id;         break;     end  	fetch next from cur into @cur_id_folder; end  close cur deallocate cur  select id,id_folder,id_type from #ans;



create PROCEDURE [dbo].[drillDownArchiveFoldersBy] @id_parent int AS BEGIN  SET NOCOUNT ON;  /*declare @id_parent int set @@id_parent = 5688;*/  WITH EntityChildren AS ( 	SELECT *  FROM document_folder WHERE id = @id_parent 	UNION all 	SELECT f.* FROM document_folder f INNER JOIN EntityChildren p on f.id_parent = p.id )  SELECT * from EntityChildren;  END



create PROCEDURE [dbo].[drillDownFoldersBy] @id_parent int, @archived bit = 0 AS BEGIN  SET NOCOUNT ON;  if (@archived = 0) begin     WITH EntityChildren AS     (         SELECT *  FROM document_folder WHERE id = @id_parent         UNION all         SELECT f.* FROM document_folder f INNER JOIN EntityChildren p on f.id_parent = p.id     )      SELECT id,document_folder,id_parent from EntityChildren; end  else begin     WITH EntityChildren AS     (         SELECT *  FROM document_folder_archived WHERE id = @id_parent         UNION all         SELECT f.* FROM document_folder_archived f INNER JOIN EntityChildren p on f.id_parent = p.id     )      SELECT id,document_folder,id_parent from EntityChildren; end END


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


create PROCEDURE [dbo].[GetNotificationInfo] @id_doc int =0, @version int =0 AS  IF OBJECT_ID('tempdb..#nusers') IS NOT NULL DROP TABLE #nusers; create table #nusers (     id int default 0,     name varchar(max) default '',     email varchar(max) default '', 	id_notification_group int default 0 );  declare @id_notification_group int = 0;  /*  Create list of notification users */ declare cur CURSOR local for select id from notification_group a where exists(select id from document_notification_group b where id_doc = @id_doc and b.id_notification_group = a.id);  /*Get notification Id that subject for this @id_doc*/  open cur;  fetch next from cur into @id_notification_group;  while @@FETCH_STATUS = 0 begin  	with nuser_cte 	as 	( 		select * from ( 			select id_key, key_type,id_notification_group from user_notification_group where id_notification_group = @id_notification_group and key_type = 1 		union all 			select id_key, key_type,id_notification_group from user_notification_group where id_notification_group in (select id_key from user_notification_group where id_notification_group = @id_notification_group and key_type = 2) and key_type = 1 		) as nuser 	) 	, nuserall 	as 	( 		select u.id, u.name, u.email, @id_notification_group as id_notification_group from nuser_cte as n inner join [user] as u on u.id = n.id_key where u.active =1 and key_type = 1 	) 	insert into #nusers  select * from nuserall ; 	 	fetch next from cur into @id_notification_group; end  close cur deallocate cur   select distinct u.id as id_user ,u.name  ,u.email ,d.id as id_doc ,d.title ,v.version ,u2.id as id_amendedBy ,u2.name as amendedBy ,dh.date as amendedDate ,v.reason from document as d  inner join document_version v on v.id_doc = d.id  inner join document_notification_group AS dng ON dng.id_doc = d.id inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version inner join notification_group AS ng ON ng.id = dng.id_notification_group inner join #nusers u ON u.id_notification_group = dng.id_notification_group inner JOIN dbo.document_historic AS dh on dh.id_version = v.id inner join [user] as u2 ON u2.id = dh.id_user inner join document_event AS de ON de.id = dh.id_event      where de.event = 'Saved as New Version' and v.version = @version and d.id = @id_doc;



create PROCEDURE [dbo].[hasDuplicate] 											@title varchar(max) = '', 											@id_type int = 0, 											@id_atbs varchar(max) = '', 											@val_atbs varchar(max) = '', 											@excld_id_doc int = 0 /*> 0 :id_doc, -1:new, 0:entire docs*/ AS /* 	=============== 	hasDuplicate procedure will consider the type duplication check. 	if the check hasn't been setup then return no duplication. 	--------------- 	RETURN: 1 if no dupilcation, otherwise will return more than 1 	=============== */  /* declare @title varchar(max) = 'Job1000.pdf'; declare @id_type int = 49; declare @id_atbs varchar(max) = '3,4'; declare @val_atbs varchar(max) = '40,10000'; declare @excld_id_doc int = 0 ; */ declare @cnt int = 0 ;   /*CHECK first if the duplication type has been setup*/ if(not exists(select * from attribute_doc_type where duplicate_chk = 1 and id_doc_type = @id_type)) begin 	select 0 as ans; 	return; end    IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp; IF OBJECT_ID('tempdb..#tmp2') IS NOT NULL DROP TABLE #tmp2;  create table #tmp ( 	id_doc int, 	title varchar(max), 	atb_value varchar(max) );  create table #tmp2 ( 	id int, 	item int, 	item_val varchar(99) );    /* # Get rid of no-duplicate id_atbs and val_atbs # id_atbs and val_atbs might contain attributes_document_type.duplication_chk = 0. Should this get rid of. # Result: Both @id_atbs and @val_atbs will be override values     E.g. When id_atb:3 has been made duplication check      @id_atbs = '3,8' -> '3'     @val_atbs = '121,124' -> '121'  */ declare @tmp_atr_id int = 0; declare @_id_atb varchar(max) = @id_atbs; declare @_val_atb varchar(max) = @val_atbs;  declare cur cursor local for select a.item as item1,b.item as item2 from (select row_number() over (order by (select 100)) as num, item from dbo.fnSplit(@id_atbs, ',')) as a inner join (select row_number() over (order by (select 100)) as num, item from dbo.fnSplit(@val_atbs, ',')) as b on a.num = b.num;  open cur; fetch next from cur into @_id_atb, @_val_atb; set @id_atbs = ''; set @val_atbs = '';  while @@FETCH_STATUS = 0 begin  	if exists (select * from attribute_doc_type where id_doc_type  = @id_type and duplicate_chk = 1 and id_attribute = @_id_atb) 	begin 	 set @id_atbs = @id_atbs +','+ @_id_atb; 	 set @val_atbs = @val_atbs +','+@_val_atb; 	end  fetch next from cur into @_id_atb, @_val_atb; end  set @id_atbs = STUFF(@id_atbs,1,1,''); /* ,3,8 -> 3,8 */ set @val_atbs = STUFF(@val_atbs,1,1,''); /* ,121,124 -> 121,124*/   if @id_atbs = '' or @val_atbs = '' begin     return select 0 as ans; end  /* # Insert id_atb, atb_val into #tmp2 # @id_atbs and @val_atbs will be joined. # Result:     @id_atbs = '3,8'     @val_atbs = '121,124'     #tmp2 =         3   |   121         8   |   124 */ insert into #tmp2 	select row_number() over (order by (select 100)) , t.id_attribute as item, cast(t.id_attribute as varchar) + ':' + t3.item_val 		FROM [attribute_doc_type] as t 		left join (select t1.id, t1.item, t2.item as item_val 			from (select row_number() over (order by (select 100)) as id, item from dbo.fnSplit(@id_atbs, ',')) as t1 			left join 			( select row_number() over (order by (select 100)) as id , item from dbo.fnSplit(@val_atbs, ',') )as t2 	on t1.id = t2.id ) as t3 	on t.id_attribute = t3.item 	where t.duplicate_chk = 1 and t.id_doc_type = @id_type; 		/*and (	t3.item_val is not null 				and isnull(t3.item_val,'') <> '' 				and not ( exists(select * from attribute_doc_type where exists(select * from attributes where id = t.id_attribute and id_type = 2 ) and id = t.id) and t3.item_val = 'False' ) )*/;   /*Fix tmp2 just in case*/ update #tmp2 set item_val = cast(item as varchar) +  (case when exists(select * from attributes where id = item and id_type = 2) then ':False' else ':' end) where item_val is null;   /*fix if there is NO records for the attributes*/ insert into document_attribute (id_doc,id_atb,atb_value,id_field_type)     select d.id as id_doc, a.id as id_atb, (case when a.id_type = 2 then 'False' else '' end) as atb_value, a.id_type as id_field_type     from document as d         left join attribute_doc_type as t on d.id_type = t.id_doc_type         left join attributes a on a.id = t.id_attribute         left join document_attribute as da on da.id_doc = d.id and da.id_atb = a.id     where title = @title and d.id_type = @id_type and id_status not in (4,5) and d.id not in (@excld_id_doc)     and atb_value is null   /*Insert duplication based on #tmp2. care atb, title, type*/ insert into #tmp select  virtual_attribute.id_doc,d.title, isnull(virtual_attribute.atb_value_joined,'')  from ( 	select distinct id_doc , ( 		SELECT 			cast(id_atb as varchar)+':'+atb_value + ',' as [text()] 			FROM [dbo].[document_attribute] 			where id_doc = a.id_doc and id_atb in (select item from #tmp2 /*select item from dbo.fnSplit(@id_atbs, ',')*/) 		order by id_atb/*atb_value*/ for xml path('') 	)  as atb_value_joined 	from document_attribute as a 	WHERE a.atb_value like CASE WHEN @val_atbs = '' THEN '%'  ELSE @val_atbs END ) as virtual_attribute 	inner join document as d on d.id = virtual_attribute.id_doc 		where 		d.id_type = @id_type 		and d.id_status not in (4,5) 		and d.title  like CASE WHEN @title = '' THEN '%'  ELSE @title END 		and d.id not in (@excld_id_doc)         and d.id_latest_version > -1;   /*include doc*/ /*if @val_atbs <> ''*/ insert into #tmp select @excld_id_doc as id_doc, @title as title, isnull((select item_val + ','as [text()] from #tmp2 order by item for xml path('')/*select item + ','as [text()] from dbo.fnSplit(@val_atbs, ',') order by item for xml path('')*/),'') as atb_value_joined  select top 1 @cnt = count(atb_value) from #tmp group by title,atb_value order by count(atb_value) desc;  /*search duplicate with same title if type is zero*/ /*if exists(select * from document where title = '' and type = 0 and id_folder=  )*/  select @cnt as ans


create PROCEDURE [dbo].[hasFolderPermissionByUser]
  @id_user int = 0,
  @permission int = 0,
  @id_folders varchar(500) = ''
as

IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #nusers;

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
select @total = count(*) from #tmp where id_permission = @permission;
select @hasPer = count(*) from #tmp where id_permission = @permission and allow = 1;

if @hasPer = @total
	return 1;
else
	return 0;






create PROCEDURE [dbo].[hasFoldersPermissionByUser]
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


create PROCEDURE [dbo].[RmFolderPermission] @id_folder int AS BEGIN          delete from document_permission where exists (select * from folder_group as fg where id = document_permission.id_folder_group and id_folder = @id_folder) ;     delete from document_permission where exists (select * from folder_user as ug where id = document_permission.id_folder_user and id_folder = @id_folder) ;      delete from folder_group where id_folder = @id_folder;     delete from folder_user where id_folder = @id_folder;     delete from user_recent_document where exists (select * from document where id = user_recent_document.id_doc and id_folder = @id_folder);       END



create PROCEDURE [dbo].[saveDocumentAttributes] 											@id_doc int = 0, 											@id_atb int = 0, 											@atb_value varchar(80)= '', 											@id_atb_prev int = 0, 											@id_type int = 0  AS   /* internal variable*/ declare @_id_type int = 0;  if @id_atb_prev  = 0 begin 	set @id_atb_prev = @id_atb end  /* Update type */ select @_id_type = id_type from document where id = @id_doc; if @_id_type = 0 and @id_type > 0 begin 	update document set id_type = @id_type where id = @id_doc; end  /* Remove previous attribute and relative data. */ delete from [dbo].[document_attribute] where id_doc = @id_doc and id_atb = @id_atb_prev;   /* Insert new attribute  -- Combobox -- 4	Combo Box( Multi select ) -- 8	FixedCombo( Multi select ) -- 10	ComboSingleSelect -- 11	FixedComboSingleSelect */ select @_id_type = id_type from attributes where id = @id_atb; if (CASE 	WHEN @_id_type = 4 THEN 1 	WHEN @_id_type = 8 THEN 1 	WHEN @_id_type = 10 THEN 1 	WHEN @_id_type = 11 THEN 1 	ELSE 0 END) = 1 begin 	insert into [dbo].[document_attribute]  (id_doc,id_atb,atb_value,id_field_type) 		select @id_doc as id_doc, @id_atb as id_atb, atb_value.item as atb_value, @_id_type as id_field_type 		from fnSplit(@atb_value, ',') as atb_value; end else begin 	insert into [dbo].[document_attribute]  (id_doc,id_atb,atb_value,id_field_type) values (@id_doc,@id_atb,@atb_value,@_id_type); end



create PROCEDURE [dbo].[TaskToArchive4Folder] AS BEGIN          set identity_insert document_folder_archived on;      declare @id_folder_root int;     declare @id_folder int;      declare curRoot CURSOR LOCAL for select id from schedule_archive_folder;          open curRoot          fetch next from curRoot into @id_folder_root;      while @@FETCH_STATUS = 0 BEGIN          exec ArchiveFolder @id_folder = @id_folder_root;                  insert into document_folder_archived (id,document_folder,id_parent) select id,document_folder,id_parent from document_folder where id = @id_folder_root;                  update document_folder set archived = 1 where id = @id_folder_root;          fetch next from curRoot into @id_folder_root;     END      close curRoot;     deallocate curRoot; END


create PROCEDURE [dbo].[TaskToDelete4Folder] AS BEGIN      declare @id_folder_root int;     declare @id_folder int;      declare curRoot CURSOR LOCAL for select id from schedule_delete_folder;          open curRoot          fetch next from curRoot into @id_folder_root;      while @@FETCH_STATUS = 0 BEGIN          exec DeleteDocsByFolder @id_folder = @id_folder_root;                  delete from document_folder where id = @id_folder_root;          fetch next from curRoot into @id_folder_root;     END      close curRoot;     deallocate curRoot; END



create PROCEDURE [dbo].[UnArchiveFolder] @id_folder int, @to int AS BEGIN     set identity_insert document_folder on;      CREATE TABLE #scheduled_folder     (         id int not null default 0,         document_folder varchar(100) not null default '',         id_parent int not null default ''     )      declare @id_folder_cur int;      insert into #scheduled_folder Exec [dbo].[drillDownFoldersBy] @id_parent = @id_folder, @archived = 1;      declare cur CURSOR LOCAL for select id from #scheduled_folder;     open cur     fetch next from cur into @id_folder_cur;      while @@FETCH_STATUS = 0 BEGIN                  if (exists(select * from document_folder where id = @id_folder_cur))             delete from document_folder where id = @id_folder_cur;          if ( @id_folder =  @id_folder_cur)                     insert into document_folder (id,document_folder,id_parent) select id,document_folder, @to as id_parent from document_folder_archived where id = @id_folder_cur;         else             insert into document_folder (id,document_folder,id_parent) select id,document_folder,id_parent from document_folder_archived where id = @id_folder_cur;                  exec addFolderAllPermissionGroup @id_folder = @id_folder_cur, @id_group = 1;                  delete from document_folder_archived where id = @id_folder_cur;          fetch next from cur into @id_folder_cur;     END      close cur;     deallocate cur; END


CREATE PROCEDURE [dbo].[copyPermissions]
											@fromFolderId int,
                                            @toFolderId int
AS

delete permission_folder_group where id_folder = @toFolderId;
delete permission_folder_user where id_folder = @toFolderId;

insert into permission_folder_group ([id_folder],[id_group],[id_permission],[allow],[deny]) select @toFolderId as [id_folder],[id_group],[id_permission],[allow],[deny] from permission_folder_group where id_folder = @fromFolderId;
insert into permission_folder_user ([id_folder],[id_user],[id_permission],[allow],[deny]) select @toFolderId as [id_folder],[id_user],[id_permission],[allow],[deny] from permission_folder_user where id_folder = @fromFolderId;

/*END_SQL_SCRIPT*/
/*
archive manually
declare @current_folder_id int = 0;

declare @tbl table (	id	int,
							document_folder	varchar(200),
							id_parent	int);


declare cur cursor local for SELECT [id]
  FROM [dbo].[document_folder] where document_folder like 'e' and id_parent = 323578 order by document_folder;

open cur;

fetch next from cur into @current_folder_id;
while @@FETCH_STATUS = 0 begin

	insert @tbl (id, document_folder, id_parent ) exec [dbo].[drillDownFoldersBy] @id_parent = @current_folder_id;

	update document_folder set document_folder.archived = 1 from @tbl where [@tbl].id = document_folder.id;

	delete from  @tbl;

	fetch next from cur into @current_folder_id;

end

set IDENTITY_INSERT document_folder_archived on;

delete from document_folder_archived;

insert into document_folder_archived (id,document_folder,id_parent,archived) select id,document_folder,id_parent,archived from document_folder where archived = 1 or id = 196;

*/


CREATE FUNCTION [dbo].[fnFindInheritedFolderFrom]
(
    @id_folder as int /* id_folder: search from*/
)
RETURNS TABLE AS

RETURN (

    WITH entitychildren
            AS (SELECT *
                FROM   document_folder
                WHERE  id = @id_folder
                UNION ALL
                SELECT f.*
                FROM   document_folder f
                        INNER JOIN entitychildren p
                                ON f.id = p.id_parent
            )
            SELECT top 1 id_folder
            FROM entitychildren as ec
            inner join
            (
                select id_folder from [permission_folder_group]
                union
                select id_folder from [permission_folder_user]
            ) as p on p.id_folder = ec.id
)


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
            where fp.id_folder in (select id_folder from fnFindInheritedFolderFrom(@id_folder))
);
/*
    SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fp.id_folder,fp.id_group
    FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                    dbo.permission_folder_group AS fp ON fp.id_group = ug.id_group
    where fp.id_folder = 18 and ug.id_user = @id_user
*/




CREATE FUNCTION [dbo].[fnFolderUsersPermissions]
(
    @id_folder as int
)

RETURNS TABLE as

RETURN
(
	select id_user,id_folder, id_permission,allow,[deny] from permission_folder_user AS your_permissions
    WHERE your_permissions.id_folder in (select id_folder from fnFindInheritedFolderFrom(@id_folder))
)



CREATE FUNCTION [dbo].[fnGetUserPermission]
(
    @id_folder as int,
    @id_user as int
)

RETURNS @t TABLE(id_user int,id_folder int , id_permission int,allow bit) as
begin

declare @cte_id_folder INT ;
set @cte_id_folder = 0;

insert into @t
	select id_user,id_folder, id_permission,allow from (
        SELECT id_user,id_folder,id_permission,allow FROM fnFolderUsersPermissions(@id_folder) AS u WHERE allow = 1 AND [deny] = 0
        UNION
        SELECT id_user,id_folder,id_permission,allow FROM fnFolderGroupsPermissions(@id_folder) AS g WHERE allow = 1 AND [deny] = 0 AND not exists (SELECT id_permission FROM fnFolderGroupsPermissions(@id_folder) WHERE ([deny] = 1 or [allow] = 0) AND id_permission = g.id_permission and id_user = g.id_user)
    ) AS your_permissions
    WHERE not exists (SELECT id_permission FROM fnFolderUsersPermissions(@id_folder) AS u WHERE ([deny] = 1 or [allow] = 0) AND id_permission = your_permissions.id_permission and id_user = your_permissions.id_user and id_folder = your_permissions.id_folder) and your_permissions.id_user = @id_user

	return;
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
end