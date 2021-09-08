/*START_SQL_SCRIPT*/
DELETE from [dbo].[attribute_field_type] WHERE id IN (5,7,8,9,10,11) ;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
SET identity_insert [dbo].[attribute_field_type] on;

insert into [dbo].[attribute_field_type] (id,type) values 
		(5,'DatePeriod'),
		(7,'DateTime'),
		(8,'FixedCombo'),
		(9,'Label'),
		(10,'ComboSingleSelect'),
		(11,'FixedComboSingleSelect');
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_attribute_value') is not null drop view view_document_attribute_value;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE view [dbo].[view_document_attribute_value] AS 
SELECT 
    attr_val.id, 
    attr_val.id_doc, 
    attr_val.id_atb, 
    attr_val.atb_value, 
    attr.id_type AS id_field_type, 
    attr.system_field,    
    d.id_status, 
    d.id_folder
 FROM 
    (
        SELECT 
            da.id,
            da.id_doc,
            da.id_atb,
            da.atb_value
            FROM [dbo].[document_attribute] AS da 
            WHERE id_field_type IN (1,3,7) AND id_atb < 1000
        UNION 
        SELECT 
            da.id,
            da.id_doc,
            da.id_atb,
            i.value
        FROM [dbo].[document_attribute] AS da 
        INNER JOIN [dbo].[attribute_combo_item] AS i
        ON da.atb_value = i.id
        WHERE id_field_type IN (4,8,10,11) AND da.id_atb < 1000 
    ) AS attr_val
INNER JOIN dbo.document AS d ON attr_val.id_doc = d.id
INNER JOIN dbo.attributes AS attr ON attr_val.id_atb = attr.id;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'favourite_property')
BEGIN
   drop TABLE favourite_property;
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
    CREATE TABLE favourite_property(
        id int identity(1,1) primary key not null ,
        id_user int not null default 0,
        id_folder int not null default 0,
        id_doc_type int not null default 0,
    );
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'favourite_property_item')
BEGIN
    drop table favourite_property_item;
END
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table favourite_property_item(
    id int identity(1,1) primary key not null ,
    id_favourite_propery int not null default 0,
    id_atb int not null default 0,
    atb_value  int not null default 0,
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF not EXISTS (SELECT * FROM sys.indexes WHERE name='idx_user' AND object_id = OBJECT_ID('favourite_property'))
begin
	create index idx_user on favourite_property (id_user);
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF not EXISTS (SELECT * FROM sys.indexes WHERE name='idx_user' AND object_id = OBJECT_ID('favourite_property_item'))
begin
	create index idx_user on favourite_property_item (id_favourite_propery);
end
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='IX_document_attribute_id_field_type' AND object_id = OBJECT_ID('document_attribute'))
begin
	DROP INDEX IX_document_attribute_id_field_type on [dbo].[document_attribute];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='_dta_index_document_attribute_5_574625090__K2_K3_K4_K5_1' AND object_id = OBJECT_ID('document_attribute'))
begin
	DROP INDEX _dta_index_document_attribute_5_574625090__K2_K3_K4_K5_1 on [dbo].[document_attribute];
end
/*END_SQL_SCRIPT*/




