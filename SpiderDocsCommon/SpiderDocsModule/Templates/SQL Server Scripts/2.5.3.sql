/*START_SQL_SCRIPT*/
if object_id('dbo.view_version') is not null drop view view_version;
/*END_SQL_SCRIPT*/
/*START_SQL_SCRIPT*/
create VIEW [dbo].[view_version]
AS
SELECT dv.id_doc, dv.id, dv.version, u.name, dh.date, dv.reason, dh.comments, de.id as id_event,de.event
FROM dbo.document_version AS dv
INNER JOIN dbo.document_historic AS dh ON dv.id_historic = dh.id
INNER JOIN dbo.document_event AS de ON de.id = dh.id_event
INNER JOIN dbo.[user] AS u ON dh.id_user = u.id
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
if object_id('dbo.view_document') is not null drop view view_document;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE VIEW [dbo].[view_document]
AS
SELECT     d.id, d.id_latest_version AS id_version, d.id_user, d.id_folder, d.title, d.extension, u.name, f.document_folder, d.id_status, 'V ' + CAST(dv.version AS varchar(3)) AS version, d.id_type, dt.type, 
                      dh.date, d.id_review, d.id_checkout_user,tCheckOutUser.name AS CheckOutUser, d.id_sp_status, d.created_date
FROM         dbo.[document] AS d INNER JOIN
                      dbo.[user] AS u ON d.id_user = u.id INNER JOIN
                      dbo.document_version AS dv ON d.id = dv.id_doc AND d.id_latest_version = dv.id INNER JOIN
                      dbo.document_historic AS dh ON dv.id_historic = dh.id INNER JOIN
                      dbo.document_folder AS f ON d.id_folder = f.id LEFT OUTER JOIN
                      dbo.document_type AS dt ON d.id_type = dt.id LEFT OUTER JOIN
                      dbo.[user] AS tCheckOutUser ON d.id_checkout_user = tCheckOutUser.id
WHERE     (d.id_status NOT IN (5))
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.__view_document_attributes') is not null drop view __view_document_attributes;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[__view_document_attributes] as
SELECT        attr_val.id, attr_val.id_doc, d.title,d.id_type, attr_val.id_atb, attr_val.atb_value, attr.id_type AS id_field_type, attr.system_field, d.id_status, d.id_folder
FROM            (SELECT        id, id_doc, id_atb, atb_value
                          FROM            dbo.document_attribute AS da
                          WHERE        (id_field_type IN (1, 3, 7)) AND (id_atb < 1000)
                          UNION
                          SELECT        da.id, da.id_doc, da.id_atb, i.value
                          FROM            dbo.document_attribute AS da INNER JOIN
                                                   dbo.attribute_combo_item AS i ON da.atb_value = CAST(i.id AS varchar)
                          WHERE        (da.id_field_type IN (4, 8, 10, 11)) AND (da.id_atb < 1000)) AS attr_val INNER JOIN
                         dbo.[document] AS d ON attr_val.id_doc = d.id INNER JOIN
                         dbo.attributes AS attr ON attr_val.id_atb = attr.id
where exists(select * from document_folder where id = d.id_folder and archived = 0 )

/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('dbo.view_historic') is not null drop view view_historic;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view [dbo].[view_historic] as  SELECT        dh.id, dv.id_doc, dh.id_version, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.id as id_event,de.event, dr.rollback_to, dv.extension AS type, dh.comments as comments, dv.reason as reason FROM            dbo.document_historic AS dh INNER JOIN                          dbo.document_version AS dv ON dh.id_version = dv.id INNER JOIN                          dbo.[document] AS doc ON dv.id_doc = doc.id INNER JOIN                          dbo.document_event AS de ON de.id = dh.id_event LEFT OUTER JOIN                          dbo.document_rollback AS dr ON dh.id = dr.id_historic INNER JOIN                          dbo.[user] AS u ON dh.id_user = u.id;
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.DeleteDocument')) DROP PROCEDURE [dbo].[DeleteDocument];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create PROCEDURE [dbo].[DeleteDocument]
@id_doc int,
@id_user int,
@reason varchar(200)
AS
BEGIN

	begin tran t1;

	update document set id_status = 5 where id = @id_doc ;

	insert into document_deleted (id_doc,id_user,reason,date) values (@id_doc,@id_user,@reason,getdate());

	commit tran t1;
END
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
		declare  cur cursor local for SELECT [title] ,[atb_value] ,count(title) as cnt FROM __view_document_attributes
										where
											id_atb = 3 and
											id_status not in (4,5) /*and
											atb_value in ('13661','13686','13726','13731','13822','13824','13831','13836','13856','13861','13870','13878','13883','13887','13890','13892','13898','13900','13901','13902','13903','13904','13906','13907','13908','13909','13910','13911','13913','13914','13918','13919','13920','13921','13923','13924','13925','13927','13929','13930','13931','13932','13933','13935','13936','13937','13940','13941','13942','13943','13944','13946','13947','13949','13950','13952','13954','13955','13956','13957','13958','13959','13960','13961','13963','13964','13966','13968','13969','13970','13971','13972','13973','13974','13975','13976','13978','13981','13982','13983','13984','13985','13986','13987','13988','13989','13990','13991','13992','13994','13995','13997','13999','14000','14001','14002','14004','14005','14006','14007','14009','14010','14012','14014','14015','14016','14019','14021','14022','14024','14025','14026','14028','14029','14030','14031','14032','14033','14034','14037','14038','14041','14043','14044','14045','14046','14047','14048','14049','14050','14051','14052','14053','14054','14055','14057','14058','14059','14060','14061','14062','14064','14065','14066','14067','14068','14069','14071','14073','14074','14075','14076','14077','14078','14080','14082','14083','14084','14085','14086','14088','14089','14090','14091','14092','14093','14095','14097','14098','14099','14100','14101','14102','14103','14104','14106','14107','14108','14109','14110','14111','14112','14113','14114','14115','14116','14117','14118','14119','14120','14121','14122','14123','14124','14125','14126','14127','14128','14129','14130','14131','14132','14133','14134','14135','14136','14137','14138','14139','AGL1021','AGL1022','AGL1023','AGL1024','AGL1025','AGL1026','AGL1027','AGL1028','CL1052','CL1054','CL1055','CL1056','CL1057','CL1059','CL1060','CL1061','CL1062','CL1063','CL1064','CL1065','D02302','D02345','D02384','D02385','D02402','D02403','D02404','D02407','D02417','D02421','D02423','D02426','D02427','D02428','D02429','D02430','D02435','D02440','D02441','D02447','D02451','D02455','D02460','D02461','D02462','D02463','D02464','D02465','D02467','D02468','D02471','D02472','D02473','D02474','D02476','D02477','D02478','D02486','D02488','D02489','D02490','D02491','D02493','D02494','D02495','D02496','D02497','D02498','D02499','D02501','D02504','D02505','D02507','D02508','D02509','D02510','D02511','D02513','D02514','D02515','D02516','D02517','D02518','D02520','D02522','D02523','D02529','D02530','D02531','D02532','D02533','D02536','D02537','D02538','D02541','D02542','D02543','D02544','D02546','D02550','D02552','D02553','D02554','D02555','D02556','D02560','D02561','D02564','D02565','D02566','D02567','D02569','D02570','D02571','D02572','D02573','D02574','D02575','D02576','D02578','D02586','D02587','D02588','D02589','D02591','D02592','D02593','D02595','D02598','D02599','D02600','D02602','D02603','D02605','D02606','D02607','D02609','D02611','D02613','D02614','D02616','D02617','D02619','D02621','D02622','D02623','D02624','D02627','D02629','D02631','D02633','D02634','D02635','D02638','D02639','D02640','D02641','D02642','D02643','D02647','D02648','D02651','D02652','D02655','D02656','D02657','D02659','D02660','D02662','D02663','D02665','D02667','D02668','D02669','D02670','D02671','D02672','D02674','D02675','D02676','D02677','D02678','D02679','D02680','D02681','D02682','D02683','D02684','D02686','D02688','D02691','D02692','D02693','D02694','D02695','D02696','D02698','D02699','D02700','D02701','D02702','D02703','D02704','D02705','D02706','D02707','D02708','D02710','D02711','D02712','D02713','D02714','D02715','D02717','D02718','D02720','D02721','D02722','D02723','D02724','D02726','D02727','D02728','D02729','D02730','D02731','D02732','D02733','D02734','D02735','D02736','D02737','D02738','D02739','D02740','D02741','D02743','D02744','D02745','D02746','D02747','D02748','D02749','D02750','D02751','D02752','D02753','D02754','D02755','D02756','D02757','D02758','D02759','D02760','D02761','D02762','D02763','D02764','DFP0275','DFP0277','DFP0278','DFP0279','DFP0280','DFP0282','DFP0283','DFP0284','DFP0285','DS1040','DS1041','DS1042','DS1043','DS1044','FP14096','FS1036','FS1037','M01014','M01070','M01134','M01140','M01143','M01164','M01168','M01176','M01180','M01181','M01184','M01185','M01186','M01188','M01190','M01191','M01192','M01193','M01194','M01195','M01196','M01197','M01198','M01199','M01200','M01202','M01203','M01204','M01205','M01206','M01207','M01209','M01210','M01211','M01213','M01214','M01215','M01216','M01218','M01220','M01221','M01222','M01223','M01224','M01225','M01226','M01227','M01228','M01234','M01235','M01237','M01239','M01240','M01241','M01242','M01243','M01244','M01245','M01251','M01252','M01253','M01254','M01255','M01256','M01258','M01259','M01260','M01261','M01262','M01267','M01270','M01271','M01272','M01273','M01274','M01275','M01276','M01277','M01278','M01279','M01280','M01281','M01282','M01283','M01284','M01285','M01286','M01288','M01289','M01290','M01291','M01292','M01293','M01294','M01295','M01296','M01297','M01298','M01300','M01301','M01302','M01304','M01305','M01306','M01307','M01308','M01309','M01310','M01311','M01312','M01313','M01314','M01315','M01316','M01320','M01321','M01322','M01324','M01326','M01327','M01328','M01329','M01333','M01334','M01335','M01336','M01337','M01338','M01339','M01340','M01341','M01342','M01343','M01344','M01345','M01346','M01347','M01348','M01349','M01350','M01351','M01352','M01353','MFP01317','MFP01325','MFP01330','MFP01331','MFP01332','MFS1008','MFS1009','MFS1010','MFS1011','MFS1012','MFS1013','MFS1014','MFS1015','SW2637','SW2643','SWD00246','SWD02581')*/
											and title not like '%.msg' and title not like '%.eml' and title not like '%.png' and title not like '%.jpeg' and title not like '%.jpg' and title not like '%.bmp'
										group by title,atb_value
										having count(title) > 1
										order by atb_value desc;
		open cur;
		fetch next from cur into @title,@atb_value,@cnt;
		while @@FETCH_STATUS = 0 begin

			/*
				merge duplicated documents. Reorder version
			*/



			/* A doc to merge into.(all of duplicated documents will be merge to this) */
			select top 1 @to_id_doc = id_doc from view_historic
				where
					id_doc in (SELECT id_doc FROM __view_document_attributes where title = @title and atb_value = @atb_value and id_status not in (4,5) and id_atb = 3 )
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
													id_doc in (SELECT id_doc FROM __view_document_attributes where title = @title and atb_value = @atb_value and id_status not in (4,5) and id_atb = 3)
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
IF OBJECT_ID('dragdrop_attribute') is not null
    DROP TABLE dragdrop_attribute;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table dragdrop_attribute(
    id int identity(1,1) primary key not null,
    id_folder int not null default 0,
    id_atb int not null default 0,
    value_from varchar(200) not null default''
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.dragDropAttribute')) DROP PROCEDURE [dbo].[dragDropAttribute];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[dragDropAttribute]
								@id_folder int = 0
AS

SET TRANSACTION isolation level READ uncommitted;

IF OBJECT_ID('tempdb..#ans') IS NOT NULL DROP TABLE #ans;
IF OBJECT_ID('tempdb..#drillup') IS NOT NULL DROP TABLE #drillup;

create table #ans
(
    id_folder int default 0,
    id_atb int default 0,
    value_from varchar(200) default '',
    value_taken varchar(200) default ''
);

create table #drillup
(
    id int default 0,
    document_folder varchar(250) default 0,
    id_parent int default 0,
    archived int default 0,
);

declare @cur_id_folder int;
declare @cur2_id_atb int;
declare @cur2_value_from varchar(max);
declare @_cur2_id_atb int;
declare @_cur2_value_from varchar(max);
declare @cur_document_folder varchar(max);

declare @prev_folder_name varchar(max) = '';

/*set @prev_folder_name = '##WRONG_SETTING##';*/
set @prev_folder_name = '';

insert into #drillup exec drillUpfoldersby @id = @id_folder, @id_user = 1;

/*select top 1  @prev_folder_name = document_folder from document_folder where id_parent = @id_folder;*/

declare cur CURSOR local for select id, document_folder from #drillup;
open cur;

fetch next from cur into @cur_id_folder, @cur_document_folder;

while @@FETCH_STATUS = 0 begin

    declare cur2 CURSOR local for select id_atb, value_from from dragdrop_attribute where id_folder = @cur_id_folder;
    open cur2;

    fetch next from cur2 into @cur2_id_atb, @cur2_value_from;

    while @@FETCH_STATUS = 0 begin
        set @_cur2_id_atb = @cur2_id_atb;
        set @_cur2_value_from = @cur2_value_from;
		
		/*First fetch so that you can use continue command in anywhere*/
        fetch next from cur2 into @cur2_id_atb, @cur2_value_from;

        /*take nearst folder logic*/
        if exists( select * from #ans where id_atb = @_cur2_id_atb)
        begin
            continue;
        end

        /*value from folder name. This applies next to start @id_folder.*/
        if (@_cur2_value_from = '##CHILD-FOLDER-NAME##'  and @cur_id_folder <> @id_folder)
        begin
            insert into #ans (id_folder,id_atb,value_from,value_taken) values (@cur_id_folder,@_cur2_id_atb,@_cur2_value_from,@prev_folder_name);
            continue;
        end
		
		if( left(@_cur2_value_from,2) <> '##' )
		begin
            insert into #ans (id_folder,id_atb,value_from,value_taken) values (@cur_id_folder,@_cur2_id_atb,'LITERAL',@_cur2_value_from);
            continue;
		end
    end

    close cur2
    deallocate cur2

	set @prev_folder_name = @cur_document_folder;

	fetch next from cur into @cur_id_folder, @cur_document_folder;

end

close cur
deallocate cur

select id_folder,id_atb,value_from,value_taken from #ans;

/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
IF OBJECT_ID('dragdrop_type') is not null
    DROP TABLE dragdrop_type;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table dragdrop_type(
    id int identity(1,1) primary key not null,
    id_folder int not null default 0,
    id_type int not null default 0
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.dragDropType')) DROP PROCEDURE [dbo].[dragDropType];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[dragDropType]
								@id_folder int = 0
AS

SET TRANSACTION isolation level READ uncommitted;

IF OBJECT_ID('tempdb..#ans') IS NOT NULL DROP TABLE #ans;
IF OBJECT_ID('tempdb..#drillup') IS NOT NULL DROP TABLE #drillup;

create table #ans
(
    id int default 0,
    id_folder int default 0,
    id_type int default 0
);

create table #drillup
(
    id int default 0,
    document_folder varchar(250) default 0,
    id_parent int default 0,
    archived int default 0,
);

declare @cur_id_folder int;
declare @id int;

insert into #drillup exec drillUpfoldersby @id = @id_folder, @id_user = 1;

declare cur CURSOR local for select id from #drillup;
open cur;

fetch next from cur into @cur_id_folder;

while @@FETCH_STATUS = 0 begin

    select @id = id from dragdrop_type where id_folder = @cur_id_folder;

    if exists( select * from dragdrop_type where id = @id)
    begin
        insert into #ans (id,id_folder,id_type) select id,id_folder,id_type from dragdrop_type where id = @id;
        break;
    end

	fetch next from cur into @cur_id_folder;
end

close cur
deallocate cur

select id,id_folder,id_type from #ans;
/*END_SQL_SCRIPT*/






/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.saveDocumentAttributes')) DROP PROCEDURE [dbo].[saveDocumentAttributes];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[saveDocumentAttributes]
											@id_doc int = 0,
											@id_atb int = 0,
											@atb_value varchar(80)= '',
											@id_atb_prev int = 0,
											@id_type int = 0

AS


/* internal variable*/
declare @_id_type int = 0;

if @id_atb_prev  = 0
begin
	set @id_atb_prev = @id_atb
end

/*
Update type
*/
select @_id_type = id_type from document where id = @id_doc;
if @_id_type = 0 and @id_type > 0
begin
	update document set id_type = @id_type where id = @id_doc;
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
select @_id_type = id_type from attributes where id = @id_atb;
if (CASE
	WHEN @_id_type = 4 THEN 1
	WHEN @_id_type = 8 THEN 1
	WHEN @_id_type = 10 THEN 1
	WHEN @_id_type = 11 THEN 1
	ELSE 0
END) = 1
begin
	insert into [dbo].[document_attribute]  (id_doc,id_atb,atb_value,id_field_type)
		select @id_doc as id_doc, @id_atb as id_atb, atb_value.item as atb_value, @_id_type as id_field_type
		from fnSplit(@atb_value, ',') as atb_value;
end
else
begin
	insert into [dbo].[document_attribute]  (id_doc,id_atb,atb_value,id_field_type) values (@id_doc,@id_atb,@atb_value,@_id_type);
end

/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = 'duplicate_chk'
              AND object_id = OBJECT_ID('attribute_doc_type'))
BEGIN
    alter table attribute_doc_type add duplicate_chk bit not null default 0;
END
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

/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.hasWarnDuplicate')) DROP PROCEDURE [dbo].[hasWarnDuplicate];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[hasWarnDuplicate]											
					@title varchar(max) = '',
					@id_folder int = 0,
					@excld_id_doc int = 0

AS
declare @cnt int = 0 ;

/*no type duplication check made*/
begin
	select top 1 @cnt = count(title) from document where id_status not in (4,5) and title = @title and id_folder = @id_folder and id <> @excld_id_doc group by title order by count(title) desc;
end 

select @cnt as ans
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
		and d.id_status not in (4,5) ;

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
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.warnUnCheckDuplicate')) DROP PROCEDURE [dbo].[warnUnCheckDuplicate];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[warnUnCheckDuplicate]											
AS
declare @cnt int = 0 ;
/*no type duplication check made*/
begin
	/*select top 1 @cnt = count(title) from document where id_type = @id_type and id_status not in (4,5) group by id_type, title order by count(title) desc;*/
	select top 1 @cnt = count(title) from document where id_status not in (4,5) group by id_folder, title order by count(title) desc;
end 

select @cnt as ans
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
IF OBJECT_ID('user_notification_group') is not null
    DROP TABLE user_notification_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[user_notification_group](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[id_key] [int] not NULL default 0,
	[key_type] [int] not NULL default 0,
	[id_notification_group] [int] not NULL default 0
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.view_document_group') is not null drop view view_document_group;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_document_group as 
SELECT      dh.id, dv.id_doc, doc.title, dv.version, dh.date, u.id AS id_user, u.name, de.event, dv.extension AS type, dh.comments,dv.reason,ng.id  as id_notification_group, ng.group_name, doc.id_sp_status,dv.extension
FROM            dbo.[document] AS doc INNER JOIN				
                         dbo.document_version AS dv ON dv.id = doc.id_latest_version INNER JOIN
							dbo.document_historic AS dh ON dh.id_version = dv.id  INNER JOIN         
                         dbo.document_event AS de ON de.id = dh.id_event  INNER JOIN         
						 dbo.[document_notification_group] as dng on dv.id_doc = dng.id_doc  left JOIN         
						 dbo.notification_group as ng on ng.id = dng.id_notification_group  left JOIN         
                         dbo.[user] AS u ON dh.id_user = u.id 											 
						 where (de.event = 'Saved as New Version' or de.event = 'Created' or de.event = 'Import' or de.event = 'New version' or de.event = 'Scanned') and doc.id_status in (1,2,3);
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
if object_id('dbo.view_notification_amended') is not null drop view view_notification_amended;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view view_notification_amended as 
with user_cte (id,name,email,type)
as
(
	select id,name,email,type from (
		select u.id,u.name,u.email,2 as type from user_group as g inner join [user] as u on g.id_user = g.id where u.active =1 
		union all 
		select id,name,email,1 type from [user] as u where  active =1 
	) as newuser
)
select u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join notification_group AS ng ON ng.id = dng.id_notification_group
inner join user_notification_group AS ung ON ung.id_notification_group = dng.id_notification_group
inner join [user_cte] as u ON u.id = ung.id_key and u.type = ung.key_type
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version
where de.event = 'Saved as New Version'AND dng.id_notification_group <> 1 
union 
select u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join [user] as u ON u.active = 1
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version 
where de.event = 'Saved as New Version' and dng.id_notification_group = 1
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo._view_notification_amended') is not null drop view _view_notification_amended;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view _view_notification_amended as 
with user_cte (id,name,email,type)
as
(
	select id,name,email,type from (
		select u.id,u.name,u.email,2 as type from user_group as g inner join [user] as u on g.id_user = g.id where u.active =1 
		union all 
		select id,name,email,1 type from [user] as u where  active =1 
	) as newuser
)
select u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
,dng.id_notification_group
,ng.group_name
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join notification_group AS ng ON ng.id = dng.id_notification_group
inner join user_notification_group AS ung ON ung.id_notification_group = dng.id_notification_group
inner join [user_cte] as u ON u.id = ung.id_key and u.type = ung.key_type
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version
where de.event = 'Saved as New Version'AND dng.id_notification_group <> 1 
union 
select u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
,dng.id_notification_group 
,'All' as group_name
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join [user] as u ON u.active = 1
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version 
where de.event = 'Saved as New Version' and dng.id_notification_group = 1
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'user_preferences'
                      AND COLUMN_NAME = 'default_ocr_import'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE user_preferences DROP COLUMN default_ocr_import
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_preferences ADD default_ocr_import bit NOT NULL default  1;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update dbo.user_preferences set default_ocr_import = 1;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF EXISTS (SELECT 1
               FROM   INFORMATION_SCHEMA.COLUMNS
               WHERE  TABLE_NAME = 'user_preferences'
                      AND COLUMN_NAME = 'ocr'
                      AND TABLE_SCHEMA='DBO')
  BEGIN
      ALTER TABLE user_preferences DROP COLUMN ocr;
  END
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
ALTER TABLE dbo.user_preferences ADD ocr bit NOT NULL default 1;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
update dbo.user_preferences set ocr = 1 ;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF OBJECT_ID('document_title_rule') is not null
    DROP TABLE document_title_rule;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [dbo].[document_title_rule](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[extension] varchar(10) not NULL default '',
	[format] varchar(200) not NULL default ''
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into document_title_rule (extension,format) values ('.msg','@FNAME @NOW'),('.eml','@FNAME @NOW');
/*END_SQL_SCRIPT*/









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
set @ans = replace(@ans,'  ',' ');

print 'Debug> ANS = ' + @ans

select @ans as ans;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete [dbo].[user_notification_group] where id_notification_group = 1;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into [dbo].[user_notification_group] (id_key, key_type, id_notification_group) select id , 1 as key_type , 1 as id_notification_group from [dbo].[user] ;
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
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.GetNotificationUser')) DROP PROCEDURE [dbo].[GetNotificationUser];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE PROCEDURE [dbo].[GetNotificationUser]
@id_doc int =0,
@version int =0
AS

IF OBJECT_ID('tempdb..#nusers') IS NOT NULL DROP TABLE #nusers;
create table #nusers
(
    id int default 0,
    name varchar(max) default '',
    email varchar(max) default '',
	id_notification_group int default 0
);

declare @id_notification_group int = 0;

declare cur CURSOR local for select id from notification_group;
open cur;

fetch next from cur into @id_notification_group;


while @@FETCH_STATUS = 0 begin

	with nuser_cte
	as
	(
		select * from (
			select id_key, key_type,id_notification_group from user_notification_group where id_notification_group = @id_notification_group and key_type = 1
		union all
			select id_key, key_type,id_notification_group from user_notification_group where id_notification_group in (select id_key from user_notification_group where id_notification_group = @id_notification_group and key_type = 2) and key_type = 1
		) as nuser
	)
	, nuserall
	as
	(
		select u.id, u.name, u.email, @id_notification_group as id_notification_group from nuser_cte as n inner join [user] as u on u.id = n.id_key where u.active =1 and key_type = 1
	)
	insert into #nusers  select * from nuserall
	
	fetch next from cur into @id_notification_group;
end

close cur
deallocate cur


select distinct u.id as id_user
,u.name 
,u.email
,d.id as id_doc
,d.title
,v.version
,u2.id as id_amendedBy
,u2.name as amendedBy
,dh.date as amendedDate
,v.reason
from document as d 
inner join document_version v on v.id_doc = d.id 
inner join document_notification_group AS dng ON dng.id_doc = d.id
inner join schedule_notification_amended as sna on sna.id_doc = v.id_doc and sna.new_version = v.version
inner join notification_group AS ng ON ng.id = dng.id_notification_group
inner join #nusers u ON u.id_notification_group = dng.id_notification_group
inner JOIN dbo.document_historic AS dh on dh.id_version = v.id
inner join [user] as u2 ON u2.id = dh.id_user
inner join document_event AS de ON de.id = dh.id_event     
where de.event = 'Saved as New Version';

/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
delete from [dbo].[document_event] where event = 'Duplication OK';
insert into [dbo].[document_event] ([event]) values ('Duplication OK');
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('dbo.shell_behaviour') is not null drop table shell_behaviour;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create table shell_behaviour
(
	id int IDENTITY(1,1) NOT NULL,
	extension varchar(5)  not null default '',
	/*default_behaviour int not null default 0, */
	override_behaviour int not null default 0 /*1:Edit, 2:New, 3:Open, 4:Print, 5:Printto*/
);
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
insert into shell_behaviour (extension, override_behaviour) values ('.dot',3);
insert into shell_behaviour (extension, override_behaviour) values ('.dotm',3);
insert into shell_behaviour (extension, override_behaviour) values ('.dotx',3);
/*END_SQL_SCRIPT*/



/*
extra task

insert into dragdrop_type (id_folder,id_type) values (196,10);

insert into dragdrop_attribute (id_folder,id_atb,value_from) values (196,3,'##CHILD-FOLDER-NAME##');

*/            