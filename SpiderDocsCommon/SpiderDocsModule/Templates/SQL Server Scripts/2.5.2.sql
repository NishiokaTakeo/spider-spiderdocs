/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * 
           FROM sys.foreign_keys 
           WHERE object_id = OBJECT_ID(N'[dbo].[FK_document_document_type]') 
             AND parent_object_id = OBJECT_ID(N'[dbo].[document]'))
BEGIN
    ALTER TABLE document DROP CONSTRAINT FK_document_document_type;
END
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.folderDrillDownL1')) DROP PROCEDURE [dbo].[folderDrillDownL1];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[folderDrillDownL1] 
								@id_parent int = 0,
								@id_user int = 0,
                                @id_permission int = 0
AS 
    
SET TRANSACTION isolation level READ uncommitted; 


if( @id_permission > 0 )
begin

    IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;

    CREATE TABLE #docfolder 
    ( 
        id INT, 
        document_folder varchar(255),
        id_parent INT 
    ); 

    insert into #docfolder 
        select id,document_folder,id_parent from view_folder as v where id_user = @id_user and id_parent = @id_parent and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission);

    select * from (
        select id,document_folder,id_parent from #docfolder
        union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from view_folder as v where id_user = @id_user and id_parent in (select id from #docfolder) and exists(select id_permission from view_user_folder_permission as p where id_user = @id_user and id_folder = v.id and id_permission = @id_permission) ) as t  order by id_parent, document_folder;
end    
else
begin
	select * from (
        select id,document_folder,id_parent from view_folder as v where id_user = @id_user and id_parent = @id_parent
		    union all
        select distinct -9999 as id ,'dummy' as document_folder,id_parent from view_folder where id_parent in (select id from view_folder where id_parent = @id_parent)
    ) as t  order by id_parent, document_folder
end
/*END_SQL_SCRIPT*/       

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.archiveFolderDrillDownL1')) DROP PROCEDURE [dbo].[archiveFolderDrillDownL1];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[archiveFolderDrillDownL1] 
								@id_parent int = 0
AS 
    
SET TRANSACTION isolation level READ uncommitted; 

IF OBJECT_ID('tempdb..#docfolder') IS NOT NULL DROP TABLE #docfolder;

select id,document_folder,id_parent from document_folder_archived where id_parent = @id_parent
	union all
select distinct -9999 as id ,'dummy' as document_folder,id_parent from document_folder_archived where id_parent in (select id from document_folder_archived where id_parent = @id_parent)
/*END_SQL_SCRIPT*/      
