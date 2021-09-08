/*START_SQL_SCRIPT*/
/*IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'system_settings'
                      AND COLUMN_NAME = 'feature_multiaddress'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE system_settings DROP COLUMN feature_multiaddress;
  END
*/
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
--ALTER TABLE dbo.system_settings ADD feature_multiaddress bit NOT NULL default 0;
/*END_SQL_SCRIPT*/
