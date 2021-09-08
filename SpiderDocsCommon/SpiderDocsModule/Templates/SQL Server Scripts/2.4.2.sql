/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.addFolderPermissionGroup')) DROP PROCEDURE [dbo].[addFolderPermissionGroup];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[addFolderPermissionGroup] @id_folder INT = 0, 
                                                   @id_group INT = 0, 
                                                   @id_permission INT = 0,
                                                   @allow int = 0,
                                                   @deny int =0
AS 


SET nocount ON; 
    
    DECLARE @last_id INT; 
    DECLARE @id_folder_group INT; 

    Set @id_folder_group = 0;
    
	select @id_folder_group = id from folder_group as f where f.id_folder = @id_folder and f.id_group = @id_group;

	if( @id_folder_group = 0 )
		begin 
			/* insert */
			INSERT INTO [folder_group] (id_folder,id_group)  values (@id_folder,@id_group);
			SET	@id_folder_group = SCOPE_IDENTITY();

		end 


    /* remove first */
    if( @id_permission > 0)
        begin 
    
            delete from document_permission where id_folder_group = @id_folder_group and id_permission = @id_permission;
        
        end 
    else 
        begin
            delete from document_permission where id_folder_group = @id_folder_group ;


        end



    if( @id_permission > 0)
        begin 

            INSERT INTO document_permission (
                        id_folder_group, 
						id_folder_user,
                        id_permission,
                        [allow],
                        [deny] ) SELECT 
                                    @id_folder_group, 
									0,
                                    id, 
                                    @allow, 
                                    @deny
                                FROM [permission] where id = @id_permission; 

        end 
    else 
        begin
            INSERT INTO document_permission (
                        id_folder_group, 
						id_folder_user,
                        id_permission,
                        [allow],
                        [deny] ) SELECT 
                                    @id_folder_group, 
									0,
                                    id, 
                                    @allow, 
                                    @deny
                                FROM [permission]; 
        end
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
            FROM [dbo].[document_attribute] AS da WITH (NOLOCK) 
            WHERE id_field_type IN (1,3,7) AND id_atb < 1000
        UNION 
        SELECT 
            da.id,
            da.id_doc,
            da.id_atb,
            i.value
        FROM [dbo].[document_attribute] AS da WITH (NOLOCK) 
        INNER JOIN [dbo].[attribute_combo_item] AS i WITH (NOLOCK) 
        ON id_field_type IN (4,8,10,11) and da.atb_value = i.id
        WHERE da.id_atb < 1000 
    ) AS attr_val
INNER JOIN dbo.document AS d WITH (NOLOCK) ON attr_val.id_doc = d.id
INNER JOIN dbo.attributes AS attr WITH (NOLOCK) ON attr_val.id_atb = attr.id;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE view [dbo].[view_folder] AS 
SELECT        df.id AS id, df.document_folder AS document_folder, ug.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.document_folder AS df WITH (NOLOCK) INNER JOIN
                         dbo.folder_group AS fg WITH (NOLOCK) ON df.id = fg.id_folder INNER JOIN
                         dbo.user_group AS ug WITH (NOLOCK) ON fg.id_group = ug.id_group
UNION
SELECT        df.id AS id, df.document_folder AS document_folder, fu.id_user AS id_user, df.id_parent AS id_parent
FROM            dbo.document_folder AS df WITH (NOLOCK) INNER JOIN
                         dbo.folder_user AS fu WITH (NOLOCK) ON df.id = fu.id_folder;
/*END_SQL_SCRIPT*/                         


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='NonClusteredIndex-20181030-112938' AND object_id = OBJECT_ID('folder_user'))
begin
	DROP INDEX [NonClusteredIndex-20181030-112938] on [dbo].[folder_user];
end
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF not EXISTS (SELECT * FROM sys.indexes WHERE name='idx_user' AND object_id = OBJECT_ID('folder_user'))
begin
	create index idx_user on folder_user (id_user);
end
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='NonClusteredIndex-20181030-112825' AND object_id = OBJECT_ID('user_group'))
begin
	DROP INDEX [NonClusteredIndex-20181030-112825] on [dbo].[user_group];
end
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF not EXISTS (SELECT * FROM sys.indexes WHERE name='idx_user' AND object_id = OBJECT_ID('user_group'))
begin
	create index idx_user on user_group (id_user);
end
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.indexes WHERE name='NonClusteredIndex-20181030-140016' AND object_id = OBJECT_ID('document_attribute'))
begin
	DROP INDEX [NonClusteredIndex-20181030-140016] on [dbo].[document_attribute];
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF not EXISTS (SELECT * FROM sys.indexes WHERE name='idx_id_field_type' AND object_id = OBJECT_ID('document_attribute'))
begin
	create index idx_id_field_type on document_attribute (id_field_type);
end
/*END_SQL_SCRIPT*/
