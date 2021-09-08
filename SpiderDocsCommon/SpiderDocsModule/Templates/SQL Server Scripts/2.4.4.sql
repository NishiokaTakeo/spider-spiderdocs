/*START_SQL_SCRIPT*/
IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'dbo.fnSplit')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].[fnSplit];  
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE FUNCTION dbo.fnSplit(
    @sInputList VARCHAR(8000) /* List of delimited items*/
  , @sDelimiter VARCHAR(8000) = ',' /* delimiter that separates items*/
) RETURNS @List TABLE (item VARCHAR(8000))

BEGIN
	DECLARE @sItem VARCHAR(8000)
	WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
	BEGIN
		SELECT
		@sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),
		@sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
		
		IF LEN(@sItem) > 0 
			INSERT INTO @List SELECT @sItem
	END

	IF LEN(@sInputList) > 0
	INSERT INTO @List SELECT @sInputList /* Put the last item in*/
	RETURN
END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.saveDocumentAttributes')) DROP PROCEDURE [dbo].[saveDocumentAttributes];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[saveDocumentAttributes] 
											@id_doc int = 0,
											@id_atb int = 0,
											@atb_value varchar(80)= '',
											@id_atb_prev int = 0
AS 


/* internal variable*/
declare @id_type int = 0;

if @id_atb_prev  = 0 
begin
	set @id_atb_prev = @id_atb
end 

/*
Remove previous attribute and relative data. 
*/
delete from [dbo].[document_attribute] where id_doc = @id_doc and id_atb = @id_atb_prev;


/*
Insert new attribute

-- Combobox
-- 4	Combo Box( Multi select )
-- 8	FixedCombo( Multi select )
-- 10	ComboSingleSelect
-- 11	FixedComboSingleSelect
*/
select @id_type = id_type from attributes where id = @id_atb;
if (CASE   
	WHEN @id_type = 4 THEN 1  
	WHEN @id_type = 8 THEN 1
	WHEN @id_type = 10 THEN 1  
	WHEN @id_type = 11 THEN 1  
	ELSE 0
END) = 1
begin
	insert into [dbo].[document_attribute]  (id_doc,id_atb,atb_value,id_field_type)
		select @id_doc as id_doc, @id_atb as id_atb, atb_value.item as atb_value, @id_type as id_field_type  
		from fnSplit(@atb_value, ',') as atb_value;
end 
else 
begin
	insert into [dbo].[document_attribute]  (id_doc,id_atb,atb_value,id_field_type) values (@id_doc,@id_atb,@atb_value,@id_type);
end 

/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_folder]
AS
SELECT        df.id AS id, df.document_folder AS document_folder, ug.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.user_group AS ug WITH (NOLOCK) INNER JOIN 
				dbo.folder_group AS fg WITH (NOLOCK) ON fg.id_group = ug.id_group INNER JOIN
				dbo.document_folder AS df WITH (NOLOCK) ON df.id = fg.id_folder                          
UNION
SELECT        df.id AS id, df.document_folder AS document_folder, fu.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.folder_user  AS fu WITH (NOLOCK) INNER JOIN
                dbo.document_folder AS df WITH (NOLOCK) ON df.id = fu.id_folder;
/*END_SQL_SCRIPT*/   


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='idx_id_folder_group' AND object_id = OBJECT_ID('document_permission'))
begin
	DROP INDEX [idx_id_folder_group] on [dbo].[document_permission];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE NONCLUSTERED INDEX [idx_id_folder_group] ON [dbo].[document_permission]
(
	[id_folder_group] ASC,
	[deny] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_permission_folder_group') is not null drop view view_permission_folder_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_permission_folder_group]
AS
SELECT        fp.id_permission, fp.allow, fp.[deny], ug.id_user, fg.id_folder
FROM            dbo.user_group AS ug with (nolock) INNER JOIN
                dbo.folder_group AS fg with (nolock) ON ug.id_group = fg.id_group INNER JOIN
                dbo.document_permission AS fp  WITH (INDEX (idx_id_folder_group))  ON fp.id_folder_group = fg.id;
/*END_SQL_SCRIPT*/              

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.folderDrillDownL2')) DROP PROCEDURE [dbo].[folderDrillDownL2];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[folderDrillDownL2] 
								@id_parent int = 0,
								@id_user int = 0
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
        id INT, 
        document_folder varchar(100), 
        id_parent INT 
    ); 

insert into #docfolder 
	select * from document_folder where id_parent = @id_parent
	union all
	select * from document_folder where id_parent in (select id from document_folder where id_parent = @id_parent);

select * from view_folder as v where id_user = @id_user and exists(select id from #docfolder as df where df.id = v.id) order by id_parent, document_folder;

/*
SELECT id_folder, 
           id_permission, 
           allow ,
		   document_folder,
		   id_parent
    FROM   (SELECT id_folder, 
                   id_permission, 
                   allow 
            FROM   view_permission_folder_user AS u 
            WHERE  id_user = @id_user 				   
				   AND exists(select id from #docfolder as df where df.id = u.id_folder)
                   AND allow = 1 
                   AND [deny] = 0 				   
            UNION 
            SELECT id_folder, 
                   id_permission, 
                   allow 
            FROM   view_permission_folder_group AS g 
            WHERE  id_user = @id_user 
				   AND exists(select id from #docfolder as df where df.id = g.id_folder)
                   AND allow = 1 
                   AND [deny] = 0 				   
                   AND NOT EXISTS (SELECT id_permission 
                                   FROM   view_permission_folder_group 
                                   WHERE  id_user = g.id_user 
                                          AND id_folder = g.id_folder 
                                          AND [deny] = 1 
                                          AND id_permission = g.id_permission)) 
           AS 
           your_permissions 
		   inner join #docfolder as df on your_permissions.id_folder = df.id

    WHERE  NOT EXISTS (SELECT id_permission 
                       FROM   view_permission_folder_user AS u 
                       WHERE  id_user = @id_user 
                              AND [deny] = 1 
                              AND id_permission = your_permissions.id_permission 
                              AND u.id_folder = your_permissions.id_folder
							  )
*/
/*END_SQL_SCRIPT*/              


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.drillUpfoldersby')) DROP PROCEDURE [dbo].[drillUpfoldersby];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[drillUpfoldersby] 
       @id INT ,
       @id_user INT
AS 
  BEGIN 
      SET nocount ON; /*declare @id_parent int set @@id_parent = 5688;*/ 

		WITH entitychildren 
			AS (SELECT * 
				FROM   document_folder 
				WHERE  id = @id 
				UNION ALL 
				SELECT f.* 
				FROM   document_folder f 
						INNER JOIN entitychildren p 
								ON f.id = p.id_parent) 
		SELECT * 
		FROM   entitychildren as ec where exists(select * from view_folder as v where v.id = ec.id and v.id_user = @id_user); 
  END
/*END_SQL_SCRIPT*/              

