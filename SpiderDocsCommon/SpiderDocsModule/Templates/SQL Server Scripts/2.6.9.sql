/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'attributes'
                      AND COLUMN_NAME = 'enabled'
                      AND TABLE_SCHEMA='DBO')
  BEGIN

    DECLARE @sql NVARCHAR(MAX)
    WHILE 1=1
    BEGIN
        SELECT TOP 1 @sql = N'alter table attributes drop constraint ['+dc.NAME+N']'
        from sys.default_constraints dc
        JOIN sys.columns c
            ON c.default_object_id = dc.object_id
        WHERE
            dc.parent_object_id = OBJECT_ID('attributes')
        AND c.name = N'enabled'
        IF @@ROWCOUNT = 0 BREAK
        EXEC (@sql)
    END

      ALTER TABLE attributes DROP COLUMN [enabled];
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table attributes add [enabled] bit not null default 1;
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
if object_id('dbo.view_attribute_doc_type') is not null drop view view_attribute_doc_type;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_attribute_doc_type as
select
	t.id,
	t.id_doc_type,
	t.id_attribute,
	t.position,
	t.duplicate_chk
from attribute_doc_type t
where exists (select * from attributes a where a.id = t.id_attribute and a.[enabled] = 1)
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_attribute') is not null drop view view_document_attribute;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_document_attribute] as
SELECT        attr_val.id, attr_val.id_doc, d.title,d.id_type, attr_val.id_atb, attr_val.atb_value,attr_val.atb_value_text, attr.id_type AS id_field_type, attr.system_field, d.id_status, d.id_folder,f.archived as folder_archived
FROM            (SELECT        id, id_doc, id_atb, atb_value, atb_value as atb_value_text
                          FROM            dbo.document_attribute AS da
                          WHERE        (id_field_type IN (1, 2, 3, 5, 7)) AND (id_atb < 1000)
                          UNION
                          SELECT        da.id, da.id_doc, da.id_atb, da.atb_value, i.value as atb_value_text
                          FROM            dbo.document_attribute AS da INNER JOIN
                                                   dbo.attribute_combo_item AS i ON da.atb_value = CAST(i.id AS varchar)
                          WHERE        (da.id_field_type IN (4, 8, 10, 11)) AND (da.id_atb < 1000)) AS attr_val INNER JOIN
                         dbo.[document] AS d ON attr_val.id_doc = d.id INNER JOIN
                         dbo.attributes AS attr ON attr_val.id_atb = attr.id INNER JOIN
						 dbo.document_folder as f on d.id_folder = f.id
where attr.[enabled] = 1;
/*END_SQL_SCRIPT*/





/*START_SQL_SCRIPT*/
if object_id('dbo.view_attribute_combo_item') is not null drop view view_attribute_combo_item;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_attribute_combo_item] as
SELECT i.[id]
      ,i.[id_atb]
      ,i.[value]
  FROM [dbo].[attribute_combo_item] as i
  where exists (select * from attributes as a where a.id = i.id_atb and a.[enabled] = 1 )
/*END_SQL_SCRIPT*/

