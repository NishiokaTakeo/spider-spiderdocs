/*START_SQL_SCRIPT*/
delete from document_event where [event] = 'Canceled Archive';
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [dbo].[document_event] ([event]) values ('Canceled Archive');
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete from permission where [permission] = 'Canceled Archive';
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [dbo].[permission] ([permission],[sort]) values ('Canceled Archive',10);
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



/*
# Get rid of no-duplicate id_atbs and val_atbs
# id_atbs and val_atbs might contain attributes_document_type.duplication_chk = 0. Should this get rid of.
# Result: Both @id_atbs and @val_atbs will be override values
    E.g. When id_atb:3 has been made duplication check

    @id_atbs = '3,8' -> '3'
    @val_atbs = '121,124' -> '121'

*/
declare @tmp_atr_id int = 0;
declare @_id_atb varchar(max) = @id_atbs;
declare @_val_atb varchar(max) = @val_atbs;

declare cur cursor local for select a.item as item1,b.item as item2 from (select row_number() over (order by (select 100)) as num, item from dbo.fnSplit(@id_atbs, ',')) as a inner join (select row_number() over (order by (select 100)) as num, item from dbo.fnSplit(@val_atbs, ',')) as b on a.num = b.num;

open cur;
fetch next from cur into @_id_atb, @_val_atb;
set @id_atbs = '';
set @val_atbs = '';

while @@FETCH_STATUS = 0
begin

	if exists (select * from attribute_doc_type where id_doc_type  = @id_type and duplicate_chk = 1 and id_attribute = @_id_atb)
	begin
	 set @id_atbs = @id_atbs +','+ @_id_atb;
	 set @val_atbs = @val_atbs +','+@_val_atb;
	end

fetch next from cur into @_id_atb, @_val_atb;
end

set @id_atbs = STUFF(@id_atbs,1,1,''); /* ,3,8 -> 3,8 */
set @val_atbs = STUFF(@val_atbs,1,1,''); /* ,121,124 -> 121,124*/


if @id_atbs = '' or @val_atbs = ''
begin
    return select 0 as ans;
end

/*
# Insert id_atb, atb_val into #tmp2
# @id_atbs and @val_atbs will be joined.
# Result:
    @id_atbs = '3,8'
    @val_atbs = '121,124'
    #tmp2 =
        3   |   121
        8   |   124
*/
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
