use KTVDB;

if exists(select * from dbo.sysobjects where name='roominfo')  
    drop table roominfo  
create table roominfo(
	id int identity(1,1),
	roomid nvarchar(10) primary key,
	roomtype nvarchar(2),
	roomsize nvarchar(5),
	imageurl nvarchar(250),
	microphonenumber nvarchar(5),
	airconditionernumber nvarchar(5),
	poweramplifiernumber nvarchar(5),
	soundnumber nvarchar(5),
	effectornumber nvarchar(5),
	songdesknumber nvarchar(5),
	lcdtvnumber nvarchar(5),
	roomremark text,
)

if exists(select * from dbo.sysobjects where name='actioninfo')  
    drop table actioninfo  
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

if exists(select * from dbo.sysobjects where name='singerinfo')  
    drop table singerinfo  
create table singerinfo(
	id int identity(1,1) primary key,
	singername nvarchar(50),
	singerenglishname nvarchar(50),
	singerothername nvarchar(50),
	singerinitials nvarchar(10),
	singernationality nvarchar(3),
	singerphotourl nvarchar(250),
	singerclicknum nvarchar(250),
	singersex nvarchar(1),
	singerintroduce text
)

if exists(select * from dbo.sysobjects where name='musicinfo')  
    drop table musicinfo  
create table musicinfo(
	id int identity(1,1) primary key,
	musicname nvarchar(50),
	singerid int,
	singername nvarchar(50),
	languagetype nvarchar(10),
	category nvarchar(50),
	recordnumber nvarchar(250),
	mvurl nvarchar(250),
	musicnameinitials nvarchar(50),
	dubbingurl nvarchar(250),
	releasedate datetime,
)

if exists(select * from dbo.sysobjects where name='musiccategory')  
    drop table musiccategory  
create table musiccategory(
	id int identity(1,1) primary key,
	categoryname nvarchar(50),
	fatherid int,
)