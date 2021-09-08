/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_attribute') is not null drop view view_document_attribute;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_attribute as 
SELECT 
    document_attribute.id_doc AS id_doc,
    document_attribute.id_atb,
    attributes.id_type AS id_field_type,
    attributes.system_field AS system_field,
    document_attribute.atb_value,
    document.id_status ,
    document.id_folder    
FROM document_attribute
INNER JOIN document
    ON document_attribute.id_doc = document.id
INNER JOIN attributes
    ON document_attribute.id_atb = attributes.id;
/*END_SQL_SCRIPT*/
