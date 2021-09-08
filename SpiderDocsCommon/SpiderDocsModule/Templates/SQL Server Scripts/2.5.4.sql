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


/*fix if there is no records for the attributes*/
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
) as virtual_attribute 
	right join document as d on d.id = virtual_attribute.id_doc
		where
		d.id_type = @id_type
		and d.id_status not in (4,5)
		and d.title  like CASE WHEN @title = '' THEN '%'  ELSE @title END
		and d.id not in (@excld_id_doc)	;



/*include doc*/
/*if @val_atbs <> ''*/
insert into #tmp select @excld_id_doc as id_doc, @title as title, isnull((select item_val + ','as [text()] from #tmp2 order by item for xml path('')/*select item + ','as [text()] from dbo.fnSplit(@val_atbs, ',') order by item for xml path('')*/),'') as atb_value_joined

select top 1 @cnt = count(atb_value) from #tmp group by title,atb_value order by count(atb_value) desc;

/*search duplicate with same title if type is zero*/
/*if exists(select * from document where title = '' and type = 0 and id_folder=  )*/

select @cnt as ans
/*END_SQL_SCRIPT*/