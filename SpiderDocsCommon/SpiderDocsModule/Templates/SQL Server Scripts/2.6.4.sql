/*START_SQL_SCRIPT*/
IF OBJECT_ID('explorer_doubleclick_behaviour_type') is not null
    DROP TABLE explorer_doubleclick_behaviour_type;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table explorer_doubleclick_behaviour_type
(
  id int IDENTITY(1,1) primary key,
  behaviour varchar(20) not null default '' /*search, or etc*/
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
insert into explorer_doubleclick_behaviour_type (behaviour) values ('Search');
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF OBJECT_ID('explorer_doubleclick_behaviour') is not null
    DROP TABLE explorer_doubleclick_behaviour;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table explorer_doubleclick_behaviour
(
  id int IDENTITY(1,1) primary key,
  id_folder int not null default 0,
  id_behaviour int not null default '' /*search, or etc*/
);
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
IF OBJECT_ID('explorer_doubleclick_behaviour_search') is not null
    DROP TABLE explorer_doubleclick_behaviour_search;
/*END_SQL_SCRIPT*/


/*START_SQL_SCRIPT*/
create table explorer_doubleclick_behaviour_search
(
  id int IDENTITY(1,1) primary key,
  id_explorer_doubleclick_behaviour int not null default 0,
  id_attr int not null default 0,
  value_from varchar(200) not null default 0
);
/*END_SQL_SCRIPT*/


