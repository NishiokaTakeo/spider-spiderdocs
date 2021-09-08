/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'system_settings'
                      AND COLUMN_NAME = 'feature_reportbuilder'
                      AND TABLE_SCHEMA='DBO')
  BEGIN

    DECLARE @sql NVARCHAR(MAX)
    WHILE 1=1
    BEGIN
        SELECT TOP 1 @sql = N'alter table system_settings drop constraint ['+dc.NAME+N']'
        from sys.default_constraints dc
        JOIN sys.columns c
            ON c.default_object_id = dc.object_id
        WHERE
            dc.parent_object_id = OBJECT_ID('system_settings')
        AND c.name = N'feature_reportbuilder'
        IF @@ROWCOUNT = 0 BREAK
        EXEC (@sql)
    END

      ALTER TABLE system_settings DROP COLUMN [feature_reportbuilder];
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'system_settings'
                      AND COLUMN_NAME = 'feature_reportbuilder'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE system_settings DROP COLUMN feature_reportbuilder;
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.system_settings ADD feature_reportbuilder bit NOT NULL default 0;
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
delete from [dbo].[system_permission_submenu] where id_permission = 1 and id_submenu in (select id from [dbo].[system_submenu] b where b.submenu_internal_name = 'SubMenu_ReportBuilder')
delete from system_submenu where submenu_internal_name = 'SubMenu_ReportBuilder';
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [dbo].[system_submenu] (submenu,id_menu,submenu_internal_name,id_order) values('Report Builder',4,'SubMenu_ReportBuilder',4);
insert into [dbo].[system_permission_submenu] (id_permission,id_submenu) values (1,SCOPE_IDENTITY());
/*END_SQL_SCRIPT*/
