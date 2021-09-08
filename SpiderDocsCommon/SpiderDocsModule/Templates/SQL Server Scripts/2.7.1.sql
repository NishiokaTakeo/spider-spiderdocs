/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'system_updates_individual')
BEGIN
    drop table system_updates_individual;
END
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table system_updates_individual(
    id int identity(1,1) primary key not null ,
    /*version [varchar](50) not null default '',*/
    name_computer [varchar](50) not null default ''

);
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'user'
                      AND COLUMN_NAME = 'name_computer'
                      AND TABLE_SCHEMA='DBO')
  BEGIN

    DECLARE @sql NVARCHAR(MAX)
    WHILE 1=1
    BEGIN
        SELECT TOP 1 @sql = N'alter table [dbo].[user] drop constraint ['+dc.NAME+N']'
        from sys.default_constraints dc
        JOIN sys.columns c
            ON c.default_object_id = dc.object_id
        WHERE
            dc.parent_object_id = OBJECT_ID('user')
        AND c.name = N'name_computer'
        IF @@ROWCOUNT = 0 BREAK
        EXEC (@sql)
    END

      ALTER TABLE [user] DROP COLUMN [name_computer];
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
alter table [user] add [name_computer] [varchar](50) not null default '';
/*END_SQL_SCRIPT*/
