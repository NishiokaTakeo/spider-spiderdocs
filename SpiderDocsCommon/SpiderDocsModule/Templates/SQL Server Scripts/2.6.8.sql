

/*START_SQL_SCRIPT*/
/*if object_id('dbo.document_attribute_link') is not null drop table dbo.document_attribute_link;*/
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.document_attribute_link') is null
begin
CREATE TABLE [dbo].[document_attribute_link]
(
    id_atb int not null default 0,
    atb_value varchar(80) not null default '',
    linked_id_atb  int not null default 0,
    linked_value varchar(80) not null default '',
    primary key(id_atb,atb_value)
)
end
/*END_SQL_SCRIPT*/


