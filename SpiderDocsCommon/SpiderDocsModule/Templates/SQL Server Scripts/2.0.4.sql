/*START_SQL_SCRIPT*/
BEGIN TRANSACTION;  
begin try;
update document_attribute set atb_value = Replace(atb_value,'''','') where id_atb in 
(
	SELECT a.id
	FROM attributes as a
	where a.id_type in (4,8,10,11) and a.id < 10000
);



INSERT INTO document_attribute (id_atb,id_doc,id_field_type,atb_value)
	SELECT 
        id_atb,
        id_doc,
        id_field_type,
	    Replace(LTRIM(RTRIM(m.n.value('.[1]','varchar(8000)'))),'''','') AS atb_value
	FROM
	(
		SELECT da.id, da.id_atb,da.id_doc,da.id_field_type,CAST('<XMLRoot><RowData>' + REPLACE(da.atb_value,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS atb_value
		FROM document_attribute as da 
		inner join attributes as a on da.id_atb = a.id
		where a.id_type in (4,8,10,11)
		and da.atb_value like '%,%'
	)t
	CROSS APPLY atb_value.nodes('/XMLRoot/RowData')m(n);

delete from document_attribute where id in (
	SELECT da.id
	FROM document_attribute as da 
	inner join attributes as a on da.id_atb = a.id
	where a.id_type in (4,8,10,11)
	and da.atb_value like '%,%'
);
COMMIT;  
end try
begin catch
ROLLBACK;
end catch;


/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_search') is not null drop view view_document_search;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_search
as 
Select 
               d.id                           AS id, 
               d.id_latest_version            AS id_latest_version, 
               d.id_user                      AS id_user, 
               d.id_folder                    AS id_folder, 
               d.title                        AS title, 
               d.extension                    AS extension, 
               u.NAME                         AS NAME, 
               f.document_folder              AS document_folder, 
               d.id_status                    AS id_status, 
               dv_fulltext.version                     AS version, 
               d.id_type                      AS id_type, 
               dt.type                        AS type, 
               dh.date                        AS date, 
               d.id_review                    AS id_review, 
               d.id_sp_status                 AS id_sp_status, 
               d.created_date                 AS created_date, 
               d.id_checkout_user             AS id_checkout_user, 
               dv_fulltext.filesize                    AS filesize, 
               atb_mail_subject.atb_value     AS mail_subject, 
               atb_mail_from.atb_value        AS mail_from, 
               atb_mail_to.atb_value          AS mail_to, 
               atb_mail_time.atb_value        AS mail_time, 
               atb_mail_is_composed.atb_value AS mail_is_composed 

FROM   document AS d  WITH (NOLOCK)
       INNER JOIN [user] u  WITH (NOLOCK)
               ON d.id_user = u.id 
       INNER JOIN document_version dv_fulltext WITH (NOLOCK)
               ON dv_fulltext.id = d.id_latest_version 
       INNER JOIN document_historic dh WITH (NOLOCK)
               ON dv_fulltext.id_historic = dh.id 
       INNER JOIN document_folder f WITH (NOLOCK)
               ON d.id_folder = f.id 
       LEFT JOIN document_type dt WITH (NOLOCK)
              ON d.id_type = dt.id 
       LEFT JOIN [user] tCheckOutUser WITH (NOLOCK)
              ON d.id_checkout_user = tCheckOutUser.id 
       LEFT JOIN document_attribute atb_mail_subject WITH (NOLOCK)
              ON d.id = atb_mail_subject.id_doc 
                 AND atb_mail_subject.id_atb = 10000 
       LEFT JOIN document_attribute atb_mail_from WITH (NOLOCK)
              ON d.id = atb_mail_from.id_doc 
                 AND atb_mail_from.id_atb = 10001 
       LEFT JOIN document_attribute atb_mail_to WITH (NOLOCK)
              ON d.id = atb_mail_to.id_doc 
                 AND atb_mail_to.id_atb = 10003 
       LEFT JOIN document_attribute atb_mail_time WITH (NOLOCK)
              ON d.id = atb_mail_time.id_doc 
                 AND atb_mail_time.id_atb = 10002 
       LEFT JOIN document_attribute atb_mail_is_composed WITH (NOLOCK)
              ON d.id = atb_mail_is_composed.id_doc 
                 AND atb_mail_is_composed.id_atb = 10004 ;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_folder') is not null drop view view_folder;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_folder as SELECT df.id as id, df.document_folder as document_folder , ug.id_user as id_user FROM dbo.document_folder AS df INNER JOIN dbo.folder_group AS fg ON df.id = fg.id_folder INNER JOIN dbo.user_group AS ug ON fg.id_group = ug.id_group UNION SELECT df.id as id, df.document_folder as document_folder, fu.id_user as id_user FROM dbo.document_folder AS df INNER JOIN dbo.folder_user AS fu ON df.id = fu.id_folder;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
begin try; alter table [user] alter column password varchar(80);  end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; alter table [user] alter column login varchar(40);   end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; alter table [user] alter column name varchar(40);   end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; alter table [document_version] alter column version smallint;    end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; alter table [document_version] alter column reason varchar(500);     end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; alter table [document_historic] alter column comments varchar(500);  end try begin catch end catch;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_7_4_5_1_6] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_5_4_6_11_7_1] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_4_1_2_5_11] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_2_4_5_6] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_11_4_2_5_6] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_1_4_2_5_6_11] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document].[_dta_stat_557245040_4_2_5_7_6_11_1] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document_attribute].[_dta_stat_574625090_1_2_3] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document_attribute].[_dta_stat_574625090_1_3_4] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[document_attribute].[_dta_stat_574625090_3_4_2_1] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP STATISTICS [dbo].[user_recent_document].[_dta_stat_437576597_3_1] ; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; drop index _dta_index_document_5_557245040__K4_K1_K2_K5_K6_K11_K7_3_8_9_10_12 on [document];  end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; drop index _dta_index_document_attribute_5_574625090__K2_K3_K4_K5_1 on [document_attribute];  end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; drop index _dta_index_document_historic_5_1118627028__K2_K3_K5D_4_6 on [document_historic];  end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; drop index _dta_index_user_recent_document_5_437576597__K2_K1_K3 on [user_recent_document];  end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; drop index idx_document_attribute_atb_value on [document_attribute];  end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP INDEX [IX_document_attribute_1] ON [dbo].[document_attribute]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP INDEX [IX_document_attribute] ON [dbo].[document_attribute]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP INDEX [document_attribute_idx2] ON [dbo].[document_attribute]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DROP INDEX [IX_document_attribute_id_field_type] ON [dbo].[document_attribute]; end try begin catch end catch;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_7_4_5_1_6] ON [dbo].[document]([id_type], [id_folder], [id_status], [id], [id_user]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_5_4_6_11_7_1] ON [dbo].[document]([id_status], [id_folder], [id_user], [id_latest_version], [id_type], [id]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_4_1_2_5_11] ON [dbo].[document]([id_folder], [id], [title], [id_status], [id_latest_version]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_2_4_5_6] ON [dbo].[document]([title], [id_folder], [id_status], [id_user]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_11_4_2_5_6] ON [dbo].[document]([id_latest_version], [id_folder], [title], [id_status], [id_user]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_1_4_2_5_6_11] ON [dbo].[document]([id], [id_folder], [title], [id_status], [id_user], [id_latest_version]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_4_2_5_7_6_11_1] ON [dbo].[document]([id_folder], [title], [id_status], [id_type], [id_user], [id_latest_version], [id]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_1_2_3] ON [dbo].[document_attribute]([id], [id_doc], [id_atb]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_1_3_4] ON [dbo].[document_attribute]([id], [id_atb], [atb_value]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_3_4_2_1] ON [dbo].[document_attribute]([id_atb], [atb_value], [id_doc], [id]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_437576597_3_1] ON [dbo].[user_recent_document]([date], [id_user]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE NONCLUSTERED INDEX [_dta_index_document_5_557245040__K4_K1_K2_K5_K6_K11_K7_3_8_9_10_12] ON [dbo].[document]( [id_folder] ASC, [id] ASC, [title] ASC, [id_status] ASC, [id_user] ASC, [id_latest_version] ASC, [id_type] ASC) INCLUDE ( [extension], [id_sp_status], [id_review], [id_checkout_user], [created_date]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE NONCLUSTERED INDEX [_dta_index_document_attribute_5_574625090__K2_K3_K4_K5_1] ON [dbo].[document_attribute]( [id_doc] ASC, [id_atb] ASC, [atb_value] ASC) INCLUDE ( [id]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE NONCLUSTERED INDEX [_dta_index_document_historic_5_1118627028__K2_K3_K5D_4_6] ON [dbo].[document_historic]( [id_version] ASC, [id_event] ASC, [date] DESC) INCLUDE ( [id_user], [comments]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE NONCLUSTERED INDEX [_dta_index_user_recent_document_5_437576597__K2_K1_K3] ON [dbo].[user_recent_document]( [id_doc] ASC, [id_user] ASC, [date] ASC)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_1_4_11_2] ON [dbo].[document]([id], [id_folder], [id_latest_version], [title]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_1_4_6_11_7] ON [dbo].[document]([id], [id_folder], [id_user], [id_latest_version], [id_type]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_557245040_7_4_11_2_5_1] ON [dbo].[document]([id_type], [id_folder], [id_latest_version], [title], [id_status], [id]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_1_5_2_3_4] ON [dbo].[document_attribute]([id], [id_field_type], [id_doc], [id_atb], [atb_value]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_3_4_5_2] ON [dbo].[document_attribute]([id_atb], [atb_value], [id_field_type], [id_doc]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_4_1] ON [dbo].[document_attribute]([atb_value], [id]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_574625090_4_2_1_5] ON [dbo].[document_attribute]([atb_value], [id_doc], [id], [id_field_type]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE NONCLUSTERED INDEX [_dta_index_document_historic_11_1118627028__K1_5] ON [dbo].[document_historic]( [id] ASC) INCLUDE ( [date]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]; end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_1118627028_5_2] ON [dbo].[document_historic]([date], [id_version]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; CREATE STATISTICS [_dta_stat_845246066_7_1] ON [dbo].[document_version]([id_historic], [id]); end try begin catch end catch;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
begin try; DECLARE @sql nvarchar(MAX); SELECT @sql =(SELECT 'UPDATE STATISTICS ' + quotename(s.name) + '.' + quotename(o.name) + ' WITH FULLSCAN; ' AS [text()] FROM sys.objects o JOIN sys.schemas s ON o.schema_id = s.schema_id WHERE o.type = 'U' FOR XML PATH(''), TYPE).value('.', 'nvarchar(MAX)'); PRINT @sql; EXEC (@sql); end try begin catch end catch;
/*END_SQL_SCRIPT*/
