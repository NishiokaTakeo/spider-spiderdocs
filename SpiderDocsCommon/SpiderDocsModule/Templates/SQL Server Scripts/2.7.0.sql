/*START_SQL_SCRIPT*/
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'favourite_property_item')
BEGIN
    drop table favourite_property_item;
END
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table favourite_property_item(
    id int identity(1,1) primary key not null ,
    id_favourite_propery int not null default 0,
    id_atb int not null default 0,
    atb_value  varchar(80) not null default '',
);
/*END_SQL_SCRIPT*/