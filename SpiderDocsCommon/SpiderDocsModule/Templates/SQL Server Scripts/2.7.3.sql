/*START_SQL_SCRIPT*/
if exists(select * from sys.schemas where name = 'reporting') begin
	if object_id('[reporting].[Table_Joins]') is not null drop table [reporting].[Table_Joins];
	if object_id('[reporting].[TEMP_TABLE]') is not null drop table [reporting].[TEMP_TABLE];
	IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('reporting.SP_Move_Report_Field_Sort')) DROP PROCEDURE [reporting].[SP_Move_Report_Field_Sort];
	IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('reporting.SP_Generate_Dynamic_Report_Filter')) DROP PROCEDURE [reporting].[SP_Generate_Dynamic_Report_Filter];
	IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('reporting.SP_Generate_Dynamic_Report')) DROP PROCEDURE [reporting].[SP_Generate_Dynamic_Report];
	if object_id('[reporting].[report_fields]') is not null drop table [reporting].[report_fields];
	if object_id('[reporting].[report_filters]') is not null drop table [reporting].[report_filters];
	if object_id('[reporting].[categories]') is not null drop table [reporting].[categories];
	if object_id('[reporting].[Dropdown_Fields]') is not null drop table [reporting].[Dropdown_Fields];
	if object_id('[reporting].[field_types]') is not null drop table [reporting].[field_types];
	if object_id('[reporting].[fields]') is not null drop table [reporting].[fields];
	if object_id('[reporting].[field_type_comparator]') is not null drop table [reporting].[field_type_comparator];
	if object_id('[reporting].[comparators]') is not null drop table [reporting].[comparators];
	if object_id('[reporting].[reports]') is not null drop table [reporting].[reports];

	DROP SCHEMA [reporting]
end
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
    CREATE SCHEMA [reporting]
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
if object_id('[reporting].[reports]') is not null drop table [reporting].[reports];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[reports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Report_Name] [nchar](50) NULL,
	[User_Id] [int] NOT NULL,
	[Category_Id] [int] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
)
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
if object_id('[reporting].[comparators]') is not null drop table [reporting].[comparators];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[comparators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SQL_Value] [varchar](20) NOT NULL,
	[Display_Value] [varchar](50) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
)
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
delete from reporting.comparators;
insert into reporting.comparators (SQL_Value,Display_Value,Created_Date,Active)
    values
        ('>','Greater than','2020-09-18 14:11:02.430','1'),
        ('<','Lower than','2020-09-18 14:11:02.430','1'),
        ('=','Equal','2020-09-18 14:11:02.430','1'),
        ('!=','Not Equal','2020-09-18 14:11:02.430','1'),
        ('LIKE','Contain','2020-09-18 14:11:02.430','1'),
        ('NOT LIKE','Not Contain','2020-09-18 14:11:02.430','1'),
        ('BETWEEN','Between','2020-09-18 14:11:02.430','1');
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('[reporting].[field_type_comparator]') is not null drop table [reporting].[field_type_comparator];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[field_type_comparator](
	[Field_Type_Id] [int] NOT NULL,
	[Comparator_Id] [int] NOT NULL
)
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
delete from [reporting].[field_type_comparator];
insert into [reporting].[field_type_comparator] ([field_type_id],[comparator_id])
    values
        (1,3),
        (1,4),
        (1,5),
        (1,6),
        (2,1),
        (2,2),
        (2,3),
        (2,4),
        (2,7),
        (3,1),
        (3,2),
        (3,3),
        (3,4),
        (3,7),
        (4,3),
        (5,3),
        (5,4);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('[reporting].[fields]') is not null drop table [reporting].[fields];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[fields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Id] [int] NOT NULL,
	[SQL_Field_Name] [nchar](50) NOT NULL,
	[Display_Name] [nchar](50) NOT NULL,
	[Field_Type_Id] [int] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
)
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
delete from reporting.fields;
insert into reporting.fields ([category_id],sql_field_name,display_name,field_type_id,created_date,active)
    values
        (1,'id','ID',2, getdate(),'1'),
        (1,'title','File Name',1, getdate(),'1'),
        (1,'extension','Extension',1, getdate(),'1'),
        (1,'NAME','Author',1, getdate(),'1'),
        (1,'document_folder','Folder Name',1, getdate(),'1'),
        (1,'version','Version',1, getdate(),'1'),
        (1,'type','Document Type',1, getdate(),'1'),
        (1,'created_date','Created_Date',3, getdate(),'1'),
		(1,'id_status','Status',5, getdate(),'1');
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
if object_id('[reporting].[field_types]') is not null drop table [reporting].[field_types];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[field_types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
)
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
delete from reporting.field_types;
insert into reporting.field_types ([type],created_date,active)
    values
        ('Text', getdate(),'1'),
        ('Number', getdate(),'1'),
        ('Date', getdate(),'1'),
        ('Bool', getdate(),'1'),
        ('Dropdown', getdate(),'1');
/*END_SQL_SCRIPT*/






/*START_SQL_SCRIPT*/
if object_id('[reporting].[Dropdown_Fields]') is not null drop table [reporting].[Dropdown_Fields];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[dropdown_fields](
	[Field_Id] [int] NOT NULL,
	[Source_Table] [nchar](100) NOT NULL,
	[Value_Field] [nchar](100) NOT NULL,
	[Text_Field] [nchar](100) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
)
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
delete from reporting.dropdown_fields;
insert into reporting.dropdown_fields (field_id,source_table,value_field,text_field,created_date,active)
    values
        (9,'document_status','id','status', getdate(),'1');
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
if object_id('[reporting].[categories]') is not null drop table [reporting].[categories];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Table_Name] [nchar](50) NOT NULL,
	[Alias] [nchar](10) NOT NULL,
	[Display_Name] [nchar](50) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL)
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
delete from reporting.categories;
insert into reporting.categories (table_name,alias,display_name,created_date,active)
    values
        ('view_document_search','vd','Documents', getdate(),'1');
/*END_SQL_SCRIPT*/







/*START_SQL_SCRIPT*/
if object_id('[reporting].[report_filters]') is not null drop table [reporting].[report_filters];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[report_filters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Report_Id] [int] NOT NULL,
	[Field_Id] [int] NULL,
	[Filter_Group] [int] NULL,
	[Filter_Order] [int] NULL,
	[Comparator_Id] [int] NULL,
	[Value_1] [varchar](100) NULL,
	[Value_2] [varchar](100) NULL,
	[Conditional] [varchar](10) NULL
)
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
if object_id('[reporting].[report_fields]') is not null drop table [reporting].[report_fields];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[report_fields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Report_Id] [int] NOT NULL,
	[Field_Id] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Display_Name] [varchar](50) NULL
)
/*END_SQL_SCRIPT*/






/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('reporting.SP_Generate_Dynamic_Report')) DROP PROCEDURE [reporting].[SP_Generate_Dynamic_Report];
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
CREATE PROCEDURE [reporting].[SP_Generate_Dynamic_Report]
@REPORT_ID INT,
@DEBUG BIT = 0
AS
BEGIN
	/*CLEAN REPORTS*/
	DELETE R FROM reporting.Reports R WHERE R.Active = 0 AND R.Report_Name IS NULL AND DATEDIFF(DAY, R.Created_Date, GETDATE()) > = 1
	DELETE R FROM reporting.Reports R WHERE R.Active = 0 AND DATEDIFF(DAY, R.Created_Date, GETDATE()) > = 30
	DELETE R FROM reporting.Report_Fields R WHERE R.Report_Id NOT IN (SELECT REPORT_ID FROM reporting.Reports)
	DELETE R FROM reporting.Report_Filters R WHERE R.Report_Id NOT IN (SELECT REPORT_ID FROM reporting.Reports)

	DECLARE @SELECT VARCHAR(MAX) = 'SELECT DISTINCT',
			@FIELD_ID INT

	DECLARE @FIELDS TABLE(	ID INT IDENTITY(1,1),
							TABLE_NAME VARCHAR(50),
							ALIAS VARCHAR(10),
							SQL_FIELD_NAME VARCHAR(50),
							DISPLAY_NAME VARCHAR(50),
							SORT INT,
							FIELD_ID INT)

	INSERT INTO @FIELDS(TABLE_NAME, ALIAS, SQL_FIELD_NAME, DISPLAY_NAME, SORT, FIELD_ID)
	SELECT DISTINCT C.Table_Name, C.Alias, F.SQL_Field_Name, RF.Display_Name, RF.Sort, RF.Field_Id
	FROM	reporting.Report_Fields RF
	JOIN	reporting.Fields F ON F.Id = RF.Field_Id
	JOIN	reporting.Categories C ON C.ID = F.Category_Id
	WHERE	RF.Report_Id = @REPORT_ID
	ORDER BY RF.Sort

	DECLARE @TABLES TABLE(ID INT IDENTITY(1,1), TABLE_NAME VARCHAR(50), ALIAS VARCHAR(10))

	INSERT INTO @TABLES(TABLE_NAME, ALIAS)
	SELECT	DISTINCT TABLE_NAME, ALIAS
	FROM	@FIELDS

	DECLARE @COUNT INT = 0

	WHILE((SELECT COUNT(*) FROM @FIELDS) > 0)
	BEGIN
		DECLARE @SQL_FIELD_NAME VARCHAR(MAX),
				@FIELD_AUX VARCHAR(100),
				@DISPLAY_NAME VARCHAR(100),
				@SORT INT,
				@ID	INT

		SELECT	@SQL_FIELD_NAME = RTRIM(LTRIM(ALIAS)) + '.' + RTRIM(LTRIM(SQL_FIELD_NAME)),
				@FIELD_AUX = RTRIM(LTRIM(ALIAS)) + '.' + RTRIM(LTRIM(SQL_FIELD_NAME)),
				@DISPLAY_NAME = 'AS ''' + RTRIM(LTRIM(DISPLAY_NAME)) + '''',
				@SORT = SORT,
				@ID = ID,
				@FIELD_ID = FIELD_ID
		FROM	@FIELDS
		ORDER BY ID DESC

		IF ((SELECT COUNT(*) FROM reporting.Dropdown_Fields D WHERE D.Field_Id = @FIELD_ID ) > 0)
		BEGIN
			DECLARE @SOURCE_TABLE VARCHAR(100),
					@VALUE_FIELD VARCHAR(100),
					@TEXT_FIELD VARCHAR(100)

			SELECT	@SOURCE_TABLE = DF.Source_Table,
					@VALUE_FIELD = DF.Value_Field,
					@TEXT_FIELD = DF.Text_Field
			FROM	reporting.Dropdown_Fields DF
			WHERE	DF.Field_Id = @FIELD_ID

			SELECT @SQL_FIELD_NAME = '(SELECT TOP 1 ' + RTRIM(LTRIM(@TEXT_FIELD)) + ' FROM ' + RTRIM(LTRIM(@SOURCE_TABLE)) + ' WHERE ' + RTRIM(LTRIM(@VALUE_FIELD)) + ' = ' + RTRIM(LTRIM(@FIELD_AUX)) + ')'
		END

		IF @COUNT = 0
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + RTRIM(LTRIM(@SQL_FIELD_NAME))
		END
		ELSE
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ', ' + RTRIM(LTRIM(@SQL_FIELD_NAME))
		END

		SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + RTRIM(LTRIM(@DISPLAY_NAME))

		SET @COUNT = @COUNT + 1

		DELETE @FIELDS WHERE ID = @ID
	END

	SET @COUNT = 0

	WHILE((SELECT COUNT(*) FROM @TABLES) > 0)
	BEGIN
		DECLARE @TABLE_NAME VARCHAR(50),
				@ALIAS VARCHAR(10),
				@MAIN_TABLE VARCHAR(50),
				@JOIN VARCHAR(MAX) = NULL

		SELECT	@TABLE_NAME = TABLE_NAME,
				@ALIAS = ALIAS,
				@ID = ID
		FROM	@TABLES

		IF @COUNT = 0
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' FROM ' + RTRIM(LTRIM(@TABLE_NAME)) + ' ' + RTRIM(LTRIM(@ALIAS))
			SELECT @MAIN_TABLE = @TABLE_NAME
		END
		ELSE
		BEGIN
			DECLARE @JOIN_SORT INT = 0,
					@JOIN_SORT_MAX INT = 0

			SELECT @JOIN_SORT_MAX = MAX(TJ.Sort)
			FROM	reporting.Table_Joins TJ
			WHERE	TJ.Main_Table = @MAIN_TABLE
			AND		TJ.Join_Table = @TABLE_NAME

			WHILE @JOIN_SORT < @JOIN_SORT_MAX
			BEGIN
				SET @JOIN_SORT = @JOIN_SORT + 1

				SELECT	@JOIN = RTRIM(LTRIM(TJ.Join_Query))
				FROM	reporting.Table_Joins TJ
				WHERE	TJ.Main_Table = @MAIN_TABLE
				AND		TJ.Join_Table = @TABLE_NAME
				AND		TJ.Sort = @JOIN_SORT

				IF CHARINDEX(@JOIN, @SELECT) = 0
				BEGIN
					SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + ISNULL(@JOIN, '')
				END
			END
		END

		SET @COUNT = @COUNT + 1

		DELETE @TABLES WHERE ID = @ID
	END

	DELETE reporting.TEMP_TABLE WHERE REPORT_ID = @REPORT_ID

	EXEC reporting.SP_Generate_Dynamic_Report_Filter @REPORT_ID, 1

	SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + TEMP_VALUE FROM reporting.TEMP_TABLE WHERE REPORT_ID = @REPORT_ID

	IF @DEBUG = 0
	BEGIN
		EXEC(@SELECT)
	END
	ELSE
	BEGIN
		SELECT @SELECT
	END
END
/*END_SQL_SCRIPT*/














/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('reporting.SP_Generate_Dynamic_Report_Filter')) DROP PROCEDURE [reporting].[SP_Generate_Dynamic_Report_Filter];
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/
CREATE PROCEDURE [reporting].[SP_Generate_Dynamic_Report_Filter]
@REPORT_ID INT,
@QUERY_TO_EXECUTE BIT = 1

AS
BEGIN
DECLARE @COUNT INT = 0,
			@SELECT VARCHAR(MAX) = '',
			@FIELD_ID INT

	DECLARE @FILTERS TABLE(	ID INT IDENTITY(1,1),
							FIELD_ID INT,
							COMPARATOR_ID INT,
							VALUE_1 VARCHAR(100),
							VALUE_2 VARCHAR(100),
							CONDITIONAL VARCHAR(10),
							FILTER_GROUP INT)

	INSERT INTO @FILTERS(FIELD_ID, COMPARATOR_ID, VALUE_1, VALUE_2, CONDITIONAL, FILTER_GROUP)
	SELECT	RF.Field_Id, RF.Comparator_Id, RF.Value_1, RF.Value_2, RF.Conditional, RF.Filter_Group
	FROM	reporting.Report_Filters RF
	WHERE	RF.Report_Id = @REPORT_ID
	ORDER BY rf.Filter_Group, RF.Filter_Order

	DECLARE @FILTER_COUNT INT = 0
	SELECT @FILTER_COUNT = COUNT(DISTINCT FILTER_GROUP) FROM @FILTERS

	WHILE((SELECT COUNT(*) FROM @FILTERS) > 0)
	BEGIN

		DECLARE	@COMPARATOR_ID INT,
				@VALUE_1 VARCHAR(100),
				@VALUE_2 VARCHAR(100),
				@COMPARATOR VARCHAR(100),
				@FIELD_TYPE_ID INT,
				@CONDITIONAL VARCHAR(10),
				@FILTER_GROUP INT,
				@FILTER_GROUP_PREV INT,
				@ID INT,
				@TABLE_NAME VARCHAR(50),
				@SQL_FIELD_NAME VARCHAR(MAX),
				@ALIAS VARCHAR(10),
				@DISPLAY_NAME VARCHAR(100),
				@COMPARATOR_DISPLAY_VALUE VARCHAR(100)

		SELECT	TOP 1
				@FIELD_ID = FIELD_ID,
				@COMPARATOR_ID = COMPARATOR_ID,
				@VALUE_1 = VALUE_1,
				@VALUE_2 = VALUE_2,
				@ID = FI.ID,
				@SQL_FIELD_NAME = F.SQL_Field_Name,
				@TABLE_NAME = C.Table_Name,
				@ALIAS = C.Alias,
				@COMPARATOR = CO.SQL_Value,
				@FIELD_TYPE_ID = F.Field_Type_Id,
				@CONDITIONAL = FI.CONDITIONAL,
				@FILTER_GROUP = FI.FILTER_GROUP,
				@DISPLAY_NAME = F.Display_Name,
				@COMPARATOR_DISPLAY_VALUE = CO.Display_Value
		FROM	@FILTERS FI
		JOIN	reporting.Fields F ON F.Id = FI.FIELD_ID
		JOIN	reporting.Categories C ON C.ID = F.Category_Id
		JOIN	reporting.Comparators CO ON CO.Id = FI.COMPARATOR_ID
		ORDER BY FI.ID


		IF @FILTER_GROUP_PREV IS NOT NULL AND (SELECT COUNT(*) FROM @FILTERS WHERE FILTER_GROUP = @FILTER_GROUP_PREV) = 0
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' )'
			IF @QUERY_TO_EXECUTE = 0
			BEGIN
				SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + '<br/>'
			END
		END

		IF @COUNT = 0
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' WHERE '
		END
		ELSE
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + RTRIM(LTRIM(@CONDITIONAL))
		END

		IF @FILTER_GROUP != ISNULL(@FILTER_GROUP_PREV, -1)
		BEGIN

			IF @FILTER_COUNT > 0
			BEGIN
				SELECT	@SELECT = RTRIM(LTRIM(@SELECT)) + ' ('
				SELECT	@FILTER_GROUP_PREV = @FILTER_GROUP,
						@FILTER_COUNT = @FILTER_COUNT - 1
			END
		END

		SET @SQL_FIELD_NAME = RTRIM(LTRIM(@ALIAS)) + '.' + RTRIM(LTRIM(@SQL_FIELD_NAME))

		IF @QUERY_TO_EXECUTE = 0
		BEGIN
			IF ((SELECT COUNT(*) FROM reporting.Dropdown_Fields D WHERE D.Field_Id = @FIELD_ID ) > 0)
			BEGIN
				/*CREATE TABLE reporting.TEMP_TABLE(REPORT_ID INT, TEMP_VALUE VARCHAR(MAX))*/

				DELETE reporting.TEMP_TABLE WHERE REPORT_ID = @REPORT_ID

				DECLARE @SOURCE_TABLE VARCHAR(100),
						@VALUE_FIELD VARCHAR(100),
						@TEXT_FIELD VARCHAR(100)

				SELECT	@SOURCE_TABLE = DF.Source_Table,
						@VALUE_FIELD = DF.Value_Field,
						@TEXT_FIELD = DF.Text_Field
				FROM	reporting.Dropdown_Fields DF
				WHERE	DF.Field_Id = @FIELD_ID

				DECLARE @SQL VARCHAR(MAX)

				SELECT @SQL = 'INSERT INTO REPORTING.TEMP_TABLE'
				SELECT @SQL = RTRIM(LTRIM(@SQL)) + ' SELECT ' + CONVERT(VARCHAR, @REPORT_ID) + ', '
				SELECT @SQL = RTRIM(LTRIM(@SQL)) + ' (SELECT TOP 1 ' + RTRIM(LTRIM(@TEXT_FIELD)) + ' FROM ' + RTRIM(LTRIM(@SOURCE_TABLE))
				SELECT @SQL = RTRIM(LTRIM(@SQL)) + ' WHERE ' + RTRIM(LTRIM(@VALUE_FIELD)) + ' = ''' + RTRIM(LTRIM(@VALUE_1)) + ''')'

				EXEC(@SQL)

				SELECT @VALUE_1 = TEMP_VALUE FROM REPORTING.TEMP_TABLE WHERE REPORT_ID = @REPORT_ID
			END

			SET @SQL_FIELD_NAME = @DISPLAY_NAME
			SET @COMPARATOR = @COMPARATOR_DISPLAY_VALUE
		END

		SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + RTRIM(LTRIM(@SQL_FIELD_NAME))
		SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ' + RTRIM(LTRIM(@COMPARATOR))

		IF CHARINDEX('LIKE', @COMPARATOR) > 0
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ''%' + RTRIM(LTRIM(@VALUE_1)) + '%'''
		END
		ELSE IF @COMPARATOR = 'BETWEEN'
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ''' + RTRIM(LTRIM(@VALUE_1)) + '''' + ' AND ''' + RTRIM(LTRIM(@VALUE_2)) + ''''
		END
		ELSE
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' ''' + RTRIM(LTRIM(@VALUE_1)) + ''''
		END

		/*LAST FILTER*/
		IF ((SELECT COUNT(*) FROM @FILTERS) = 1)
		BEGIN
			SELECT @SELECT = RTRIM(LTRIM(@SELECT)) + ' )'
		END

		SET @COUNT = @COUNT + 1

		DELETE @FILTERS WHERE ID = @ID
	END

	IF @QUERY_TO_EXECUTE = 1
	BEGIN
		DELETE reporting.TEMP_TABLE WHERE REPORT_ID = @REPORT_ID

		INSERT INTO reporting.TEMP_TABLE
		SELECT @REPORT_ID, @SELECT
	 END
	 ELSE
	 BEGIN
		SELECT @SELECT
	 END
END
/*END_SQL_SCRIPT*/
















/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('reporting.SP_Move_Report_Field_Sort')) DROP PROCEDURE [reporting].[SP_Move_Report_Field_Sort];
/*END_SQL_SCRIPT*/



/*START_SQL_SCRIPT*/

create PROCEDURE [reporting].[SP_Move_Report_Field_Sort]
@ID INT,
@SORT_CHANGE INT
AS
BEGIN

	DECLARE @REPORT_ID INT,
			@SORT INT

	SELECT	@REPORT_ID = RF.Report_Id,
			@SORT = RF.Sort + @SORT_CHANGE
	FROM	reporting.Report_Fields RF
	WHERE	RF.Id = @ID

	UPDATE	reporting.Report_Fields
	SET		SORT = SORT + (@SORT_CHANGE * -1)
	WHERE	Report_Id = @REPORT_ID
	AND		Sort = @SORT

	UPDATE	reporting.Report_Fields
	SET		SORT = SORT + @SORT_CHANGE
	WHERE	Id = @ID

END

/*END_SQL_SCRIPT*/











/*START_SQL_SCRIPT*/
if object_id('[reporting].[TEMP_TABLE]') is not null drop table [reporting].[TEMP_TABLE];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[TEMP_TABLE](
	[REPORT_ID] [int] NULL,
	[TEMP_VALUE] [varchar](max) NULL
)
/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
if object_id('[reporting].[Table_Joins]') is not null drop table [reporting].[Table_Joins];
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
CREATE TABLE [reporting].[Table_Joins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Main_Table] [varchar](50) NOT NULL,
	[Join_Table] [varchar](50) NOT NULL,
	[Sort] [int] NOT NULL,
	[Join_Query] [varchar](250) NULL,
	[Created_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL
)

/*END_SQL_SCRIPT*/




/*START_SQL_SCRIPT*/
if object_id('reporting.view_rpt_field_comparator') is not null drop view reporting.view_rpt_field_comparator;
/*END_SQL_SCRIPT*/

/*START_SQL_SCRIPT*/
create view reporting.view_rpt_field_comparator as
SELECT c.*,f.id as field_Id FROM reporting.Comparators c
JOIN reporting.Field_Type_Comparator ftc on ftc.Comparator_Id = c.Id
JOIN reporting.Fields f on f.Field_Type_Id = ftc.Field_Type_Id --and f.id =
where c.Active = 1

/*END_SQL_SCRIPT*/