use KTVDB;
create table roominfo(
	id int identity(1,1),
	roomid nvarchar(10) primary key,
	roomtype nvarchar(2),
	roomsize nvarchar(5),
	imageurl nvarchar(250),
	microphonenumber nvarchar(5)
)

create table actioninfo(
	id int identity(1,1),
	actioncode nvarchar(10),
	actionname nvarchar(50),
	groupcode nvarchar(10)
)

create table taskinfo(
	id int identity(1,1) primary key,
	roomid nvarchar(10),
	roomstate nvarchar(5),
	roomconsume nvarchar(50),
	starttime datetime,
	endtime datetime,
	customerid int
)

if exists(select * from dbo.sysobjects where name='customerinfo')  
    drop table customerinfo  
create table customerinfo(
	customerid int identity(1,1) primary key,
	customername nvarchar(50),
	customersex nvarchar(1),
	customertel nvarchar(50),
	customerage nvarchar(3),
	customeridcard nvarchar(24)
)

if exists(select * from dbo.sysobjects where name='roompriceinfo')  
    drop table roompriceinfo  
create table roompriceinfo(
	id int identity(1,1) primary key,
	roomtype nvarchar(5),
	roomprice nvarchar(5),
	starttime int,
	endtime int
)

if exists(select * from dbo.sysobjects where name='consumelog')  
    drop table consumelog  
create table consumelog(
	id int identity(1,1),
	roomid nvarchar(10),
	customerid int,
	roomconsume nvarchar(50),
	starttime datetime,
	endtime datetime
)