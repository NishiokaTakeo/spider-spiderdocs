
/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.cnvDocumentTitle')) DROP PROCEDURE [dbo].[cnvDocumentTitle];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[cnvDocumentTitle]
											@title varchar(max) = ''
AS

declare @ext nvarchar(10) = '';
declare @format nvarchar(200) = '';
declare @ans nvarchar(200) = '';

set @ext = substring(@title, len(@title) - charindex('.',reverse(@title)) + 1 ,99);

print 'Debug > EXT = ' + @ext;

select @format = [format] from document_title_rule where extension = @ext;

print 'Debug > FORMAT = ' + @format;

if @format = ''
	select @title as ans;/*print 'RETURN = ' + @title;*/

set @ans = replace(@format, '@fname',replace(@title,@ext,''))
/*set @ans = replace(@ans, '@NOW',FORMAT (getdate(),' dd MMM yyyy hh:mm tt'))*/
set @ans = replace(@ans, '@NOW', (CONVERT(nvarchar, GETDATE(), 106) +' ' + replace(replace(replace(right(CONVERT(nvarchar, GETDATE(), 0),7),' ','0'),'P',' P'),'A',' A')) )


set @ans += @ext;
/*set @ans = replace(@ans,'  ',' ');*/

print 'Debug> ANS = ' + @ans

select @ans as ans;
/*END_SQL_SCRIPT*/





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

    /* remove first */
    if( @id_permission > 0)
        begin

            delete from permission_folder_group where id_folder = @id_folder and id_group = @id_group and id_permission = @id_permission;

        end
    else
        begin
            delete from permission_folder_group where id_folder = @id_folder and id_group = @id_group;
        end



    if( @id_permission > 0)
        begin

            INSERT INTO permission_folder_group (
						id_folder,
                        id_group,
                        id_permission,
                        [allow],
                        [deny] ) SELECT
									@id_folder,
                                    @id_group,
                                    id,
                                    @allow,
                                    @deny
                                FROM [permission] where id = @id_permission;

        end
    else
        begin
            INSERT INTO permission_folder_group (
						id_folder,
                        id_group,
                        id_permission,
                        [allow],
                        [deny] ) SELECT
									@id_folder,
                                    @id_group,
                                    id,
                                    @allow,
                                    @deny
                                FROM [permission];
        end
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.canUnCheckDuplicate')) DROP PROCEDURE [dbo].[canUnCheckDuplicate];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[canUnCheckDuplicate]
											@title varchar(max) = '',
											@id_type int = 0,
											@id_atbs varchar(max) = ''
AS

/*
declare @title varchar(max) = 'Job1000.pdf';
declare @id_type int = 49;
declare @id_atbs varchar(max) = '3,4';
declare @val_atbs varchar(max) = '40,10000';
declare @excld_id_doc int = 0 ;
*/
declare @cnt int = 0 ;

IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp;
IF OBJECT_ID('tempdb..#tmp2') IS NOT NULL DROP TABLE #tmp2;

create table #tmp (
	id_doc int,
	title varchar(max),
	atb_value varchar(max)
);

/*care atb, title, type*/
insert into #tmp select  virtual_attribute.id_doc,d.title,virtual_attribute.atb_value_joined  from (
	select distinct id_doc , (
		SELECT
			atb_value + ',' as [text()]
			FROM [dbo].[document_attribute] where id_doc = a.id_doc and id_atb in (select item from dbo.fnSplit(@id_atbs, ','))
		order by id_atb for xml path('')
	)  as atb_value_joined
	from document_attribute as a
) as virtual_attribute
	right join document as d on d.id = virtual_attribute.id_doc
		where
		d.id_type = @id_type
		and d.id_status not in (4,5) and id_latest_version > -1
		and exists(select * from document_folder where id = d.id_folder and archived = 0 );

select top 1 @cnt = count(atb_value) from #tmp group by title,atb_value order by count(atb_value) desc;

/*no type duplication check made*/
/*
if @id_atbs = ''
begin
	--select top 1 @cnt = count(title) from document where id_type = @id_type and id_status not in (4,5) group by id_type, title order by count(title) desc;
	select top 1 @cnt = count(title) from document where id_status not in (4,5) group by id_folder, title order by count(title) desc;
end
*/

select @cnt as ans
/*END_SQL_SCRIPT*/






/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.hasDuplicate')) DROP PROCEDURE [dbo].[hasDuplicate];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[hasDuplicate]
											@title varchar(max) = '',
											@id_type int = 0,
											@id_atbs varchar(max) = '',
											@val_atbs varchar(max) = '',
											@excld_id_doc int = 0 /*> 0 :id_doc, -1:new, 0:entire docs*/
AS
/*
	===============
	hasDuplicate procedure will consider the type duplication check.
	if the check hasn't been setup then return no duplication.
	---------------
	RETURN: 1 if no dupilcation, otherwise will return more than 1
	===============
*/

/*
declare @title varchar(max) = 'Job1000.pdf';
declare @id_type int = 49;
declare @id_atbs varchar(max) = '3,4';
declare @val_atbs varchar(max) = '40,10000';
declare @excld_id_doc int = 0 ;
*/
declare @cnt int = 0 ;


/*CHECK first if the duplication type has been setup*/
if(not exists(select * from attribute_doc_type where duplicate_chk = 1 and id_doc_type = @id_type))
begin
	select 0 as ans;
	return;
end



IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp;
IF OBJECT_ID('tempdb..#tmp2') IS NOT NULL DROP TABLE #tmp2;

create table #tmp (
	id_doc int,
	title varchar(max),
	atb_value varchar(max)
);

create table #tmp2 (
	id int,
	item int,
	item_val varchar(99)
);

/*insert duplication check id_atb and value*/
insert into #tmp2
	select row_number() over (order by (select 100)) , t.id_attribute as item, cast(t.id_attribute as varchar) + ':' + t3.item_val
		FROM [attribute_doc_type] as t
		left join (select t1.id, t1.item, t2.item as item_val
			from (select row_number() over (order by (select 100)) as id, item from dbo.fnSplit(@id_atbs, ',')) as t1
			left join
			( select row_number() over (order by (select 100)) as id , item from dbo.fnSplit(@val_atbs, ',') )as t2
	on t1.id = t2.id ) as t3
	on t.id_attribute = t3.item
	where t.duplicate_chk = 1 and t.id_doc_type = @id_type;
		/*and (	t3.item_val is not null
				and isnull(t3.item_val,'') <> ''
				and not ( exists(select * from attribute_doc_type where exists(select * from attributes where id = t.id_attribute and id_type = 2 ) and id = t.id) and t3.item_val = 'False' ) )*/;

/*Fix tmp2 just in case*/
update #tmp2 set item_val = cast(item as varchar) +  (case when exists(select * from attributes where id = item and id_type = 2) then ':False' else ':' end) where item_val is null;


/*fix if there is NO records for the attributes*/
insert into document_attribute (id_doc,id_atb,atb_value,id_field_type)
    select d.id as id_doc, a.id as id_atb, (case when a.id_type = 2 then 'False' else '' end) as atb_value, a.id_type as id_field_type
    from document as d
        left join attribute_doc_type as t on d.id_type = t.id_doc_type
        left join attributes a on a.id = t.id_attribute
        left join document_attribute as da on da.id_doc = d.id and da.id_atb = a.id
    where title = @title and d.id_type = @id_type and id_status not in (4,5) and d.id not in (@excld_id_doc)
    and atb_value is null


/*Insert duplication based on #tmp2. care atb, title, type*/
insert into #tmp select  virtual_attribute.id_doc,d.title, isnull(virtual_attribute.atb_value_joined,'')  from (
	select distinct id_doc , (
		SELECT
			cast(id_atb as varchar)+':'+atb_value + ',' as [text()]
			FROM [dbo].[document_attribute]
			where id_doc = a.id_doc and id_atb in (select item from #tmp2 /*select item from dbo.fnSplit(@id_atbs, ',')*/)
		order by id_atb/*atb_value*/ for xml path('')
	)  as atb_value_joined
	from document_attribute as a
	WHERE a.atb_value like CASE WHEN @val_atbs = '' THEN '%'  ELSE @val_atbs END
) as virtual_attribute
	inner join document as d on d.id = virtual_attribute.id_doc
		where
		d.id_type = @id_type
		and d.id_status not in (4,5)
		and d.title  like CASE WHEN @title = '' THEN '%'  ELSE @title END
		and d.id not in (@excld_id_doc)
        and d.id_latest_version > -1;


/*include doc*/
/*if @val_atbs <> ''*/
insert into #tmp select @excld_id_doc as id_doc, @title as title, isnull((select item_val + ','as [text()] from #tmp2 order by item for xml path('')/*select item + ','as [text()] from dbo.fnSplit(@val_atbs, ',') order by item for xml path('')*/),'') as atb_value_joined

select top 1 @cnt = count(atb_value) from #tmp group by title,atb_value order by count(atb_value) desc;

/*search duplicate with same title if type is zero*/
/*if exists(select * from document where title = '' and type = 0 and id_folder=  )*/

select @cnt as ans
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
						 dbo.document_folder as f on d.id_folder = f.id;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.__view_document_attributes') is not null drop view __view_document_attributes;
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo._MergeDocs')) DROP PROCEDURE [dbo].[_MergeDocs];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[_MergeDocs]
AS
BEGIN
begin tran;

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

	IF OBJECT_ID('tempdb..#merge_cur') IS NOT NULL DROP TABLE #merge_cur;
	create table #merge_cur (id_doc int);


	IF OBJECT_ID('tempdb..#tmp') IS NOT NULL DROP TABLE #tmp;
	create table #tmp (id_doc int, id_latest_version int, last_file_update datetime );

	IF OBJECT_ID('_merge_document') is null
	begin
		create table _merge_document(
			id_doc int default 0,
			id_doc_to int default 0,
			title varchar(max) default '',
			id_version int default 0,
			atb_value varchar(max) default ''
		);
	end

	IF OBJECT_ID('_merge_document_historic') is null
	begin
		CREATE TABLE [dbo].[_merge_document_historic](
			[id] [int] NOT NULL,
			[id_version] [int] NOT NULL,
			[id_event] [int] NOT NULL,
			[id_user] [int] NOT NULL,
			[date] [datetime] NOT NULL,
			[comments] [varchar](max) NULL
		);
	end

	IF OBJECT_ID('_merge_document_latest_version') is null
	begin
		CREATE TABLE [dbo].[_merge_document_latest_version](
			[id_doc] int NOT NULL default 0,
			[comment] varchar(max) NOT NULL default ''
		);
	end
	/*
			1	Created
			4	New version
			17	Saved as New Version
	TEST DATA
	*/

	delete from #tmp;
	/*insert into #tmp values (2070,2067,'2019-09-18 13:40:00.000');*/

	declare @title varchar(255);
	declare @atb_value varchar(80);
	declare @cnt int;

	declare @cur_id_version int;
	declare @cur_id_doc int;
	declare @cur_date datetime;

	declare @to_id_doc int;
	declare @version_number int = 1;

	declare @reason varchar(255);

	BEGIN TRY
		/*Get targeted documents that will be merged.*/
		declare  cur cursor local for SELECT [title] ,[atb_value_text] ,count(title) as cnt FROM view_document_attribute
										where
											id_atb = 3 and
											id_status not in (4,5) /*and
											atb_value in ('13661','SWD02581')*/
											and title not like '%.msg' and title not like '%.eml' and title not like '%.png' and title not like '%.jpeg' and title not like '%.jpg' and title not like '%.bmp'
											and folder_archived = 0
										group by title,atb_value_text
										having count(title) > 1
										order by atb_value_text desc;
		open cur;
		fetch next from cur into @title,@atb_value,@cnt;
		while @@FETCH_STATUS = 0 begin

			/*
				merge duplicated documents. Reorder version
			*/



			/* A doc to merge into.(all of duplicated documents will be merge to this) */
			select top 1 @to_id_doc = id_doc from view_historic
				where
					id_doc in (SELECT id_doc FROM view_document_attribute where title = @title and atb_value_text = @atb_value and id_status not in (4,5) and id_atb = 3 and folder_archived = 0 )
				order by date desc, id desc;

			delete from #merge_cur;

			/*Get all duplicate docs with sort by created date*/
			declare upcur cursor local for 	select  id_doc,
													id as id_version,
													(case when exists( select * from #tmp where id_latest_version = id)
																			then (select top 1 last_file_update from #tmp where id_latest_version = id)
																			else date
													end) as date
												from view_version
												where
													id_doc in (SELECT id_doc FROM view_document_attribute where title = @title and atb_value_text = @atb_value and id_status not in (4,5) and id_atb = 3 and folder_archived = 0)
													and id_event in (1,2,3,4,17)
												order by date;

			open upcur;
			fetch next from upcur into @cur_id_doc, @cur_id_version, @cur_date;
			while @@FETCH_STATUS = 0 begin
				/*
					Merge duplicate documents here.
					Logical remove first then merge the documents
				*/

				SET @reason = '';

				if @to_id_doc <>  @cur_id_doc
				begin
					/*Delete documents for duplication*/

					SET @reason = 'Merged by system, id_doc:' + convert(varchar(255), @cur_id_doc) + ' -> ' + convert(varchar(255), @to_id_doc)/* + ', created_at ' + convert(nvarchar(MAX), @cur_date, 21)*/;

					exec DeleteDocument @id_doc  = @cur_id_doc, @id_user = 1, @reason = @reason;
				end

				update document_version set id_doc = @to_id_doc, version = @version_number, reason = @reason where id = @cur_id_version;

				set @version_number = @version_number + 1;

				/* This is just for history for the task.*/
				insert into _merge_document (id_doc,id_doc_to,title,id_version,atb_value) values(@cur_id_doc,@to_id_doc,@title,@cur_id_version,@atb_value);
				insert into #merge_cur (id_doc) values(@cur_id_doc);
				fetch next from upcur into @cur_id_doc, @cur_id_version, @cur_date;
			end

			close upcur;
			deallocate upcur;

			/*
				Update document.id_latest_version for the merged document.
			*/
			declare @id_latest_version int;
			declare @doc_id_latest_version int;

			select  @doc_id_latest_version = id_latest_version from document where id = @to_id_doc;
			set @reason = 'id_latest_version was ' + cast(@doc_id_latest_version as varchar(max));

			select  top 1 @id_latest_version = id from document_version where id_doc = @to_id_doc order by cast(version as int) desc;
			update document set id_latest_version = @id_latest_version where id = @to_id_doc;

			if @doc_id_latest_version <> @id_latest_version
			begin
				insert into _merge_document_latest_version (id_doc,comment) values (@to_id_doc, @reason);
			end

			/*

				update historic

			*/
			declare hiscur cursor local for 	select  id as id_historic,
													id_version,
													id_event
												from view_historic
												where
													/*id_doc in (SELECT id_doc FROM __view_document_attributes where title = @title and atb_value = @atb_value *and id_status not in (4,5)* and id_atb = 3)*/
													id_doc in (SELECT id_doc FROM #merge_cur)
												order by date, id;

			open hiscur;
			declare @hiscur_id_historic int;
			declare @hiscur_id_version int;
			declare @hiscur_id_event int;
			declare @hiscur_last_id_version int;
			declare @hiscur_comments varchar(max);
			fetch next from hiscur into @hiscur_id_historic, @hiscur_id_version, @hiscur_id_event;


			while @@FETCH_STATUS = 0 begin



				if @hiscur_id_event = 1 or @hiscur_id_event = 2 or @hiscur_id_event = 3 or @hiscur_id_event = 4 or @hiscur_id_event = 17
				begin
					set @hiscur_last_id_version = @hiscur_id_version;
				end
				else
				begin
					set @hiscur_comments = 'id_version was ' + cast(@hiscur_id_version as varchar(max));
					update document_historic set id_version = @hiscur_last_id_version , comments = @hiscur_comments where id = @hiscur_id_historic and id_version <> @hiscur_last_id_version;
				end



				/*insert into _merge_document_historic (id_doc,id_doc_to,title,id_version,atb_value) values(@cur_id_doc,@to_id_doc,@title,@cur_id_version,@atb_value);*/

				fetch next from hiscur into @hiscur_id_historic, @hiscur_id_version, @hiscur_id_event;
			end

			close hiscur;
			deallocate hiscur;

			set @version_number = 1;

			fetch next from cur into @title,@atb_value,@cnt;
		end

		close cur;
		deallocate cur;

		commit tran;
	END TRY
	BEGIN CATCH
		ROLLBACK;
	END CATCH ;
END
/*END_SQL_SCRIPT*/












/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo._unique4name')) DROP PROCEDURE [dbo].[_unique4name];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/

create procedure _unique4name as
BEGIN

declare @tmp_title varchar(max) = '';
declare @new_title varchar(max) = '';
declare @id_doc int = 0;
declare @title varchar(max) = '';
declare @i int = 1;
declare @ext nvarchar(20) = '';

IF OBJECT_ID('_unique4name_history') is null
begin
    create table _unique4name_history(
        id_doc int default 0,
        title varchar(max) default '',
        title_to varchar(max) default ''
    );
end

declare cur cursor for select title from document where extension in ('.jpg','.jpeg','.png','.bmp','.tiff','.eml','.msg') group by title having count(title) > 1;

open cur;

fetch next from cur into @title;
while @@FETCH_STATUS = 0 begin
    set @i = 1;

    declare cur2 cursor for select id,title from document where title = @title;
    open cur2;

    fetch next from cur2 into @id_doc, @tmp_title;

    while @@FETCH_STATUS = 0 begin

        set @ext = substring(@tmp_title, len(@tmp_title) - charindex('.',reverse(@tmp_title)) + 1 ,99);
        set @new_title = replace(@tmp_title,@ext,'') + ' ' +cast(@i as varchar(50)) + @ext;

        update document set title = @new_title where id = @id_doc;
        insert into _unique4name_history (id_doc,title,title_to) values (@id_doc,@tmp_title,@new_title);

        set @i = @i + 1;
        fetch next from cur2 into @id_doc, @tmp_title;
    end

    close cur2;
    deallocate cur2;


    fetch next from cur into @title;
end

close cur;
deallocate cur;
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.warnUnCheckDuplicate')) DROP PROCEDURE [dbo].[warnUnCheckDuplicate];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.hasWarnDuplicate')) DROP PROCEDURE [dbo].[hasWarnDuplicate];
/*END_SQL_SCRIPT*/