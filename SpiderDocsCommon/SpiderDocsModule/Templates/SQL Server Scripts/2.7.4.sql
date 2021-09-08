/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_exist') is not null drop view view_document_exist;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_exist as
select id, id_doc from document_version as v
where not exists(select id from document d where d.id = v.id_doc and d.id_status in (4,5))
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'system_settings'
                      AND COLUMN_NAME = 'feature_multiaddress'
                      AND TABLE_SCHEMA='DBO')
  BEGIN

    DECLARE @sql NVARCHAR(MAX)
    WHILE 1=1
    BEGIN
        SELECT TOP 1 @sql = N'alter table [dbo].[system_settings] drop constraint ['+dc.NAME+N']'
        from sys.default_constraints dc
        JOIN sys.columns c
            ON c.default_object_id = dc.object_id
        WHERE
            dc.parent_object_id = OBJECT_ID('system_settings')
        AND c.name = N'feature_multiaddress'
        IF @@ROWCOUNT = 0 BREAK
        EXEC (@sql)
    END

      ALTER TABLE [system_settings] DROP COLUMN [feature_multiaddress];
  END
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'system_settings'
                      AND COLUMN_NAME = 'feature_multiaddress'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE system_settings DROP COLUMN feature_multiaddress;
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.system_settings ADD feature_multiaddress bit NOT NULL default 0;
/*END_SQL_SCRIPT*/
