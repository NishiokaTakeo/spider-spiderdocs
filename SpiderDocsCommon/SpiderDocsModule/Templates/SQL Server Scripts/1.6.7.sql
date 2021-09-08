/*START_SQL_SCRIPT*/
if not exists (select * from sysobjects where name='attribute_combo_item_children' and xtype='U')
CREATE TABLE [dbo].[attribute_combo_item_children](
	[id_combo_item] [int] NULL,
	[id_atb] [int] NULL,
	[atb_value] [varchar](max) NULL
) ON [PRIMARY]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update document_attribute set id_field_type = 9 where id_atb in (10000, 10001, 10003)
update attributes set id_type = 9 where id in (10000, 10001, 10003)
/*END_SQL_SCRIPT*/
