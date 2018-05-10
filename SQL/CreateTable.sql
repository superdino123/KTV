use KTVDB;

if exists(select * from dbo.sysobjects where name='roominfo')  
    drop table roominfo  
create table roominfo(--包房信息表
	id int identity(1,1),
	roomid nvarchar(10) primary key,--房间号
	roomtype nvarchar(2),--房间类型：小包0，中包1，大包2，情侣包3，豪华包4，商务包5
	roomsize nvarchar(5),--房间面积
	imageurl nvarchar(250),--房间图片
	microphonenumber nvarchar(5),--麦克数量
	airconditionernumber nvarchar(5),--空调数量
	poweramplifiernumber nvarchar(5),--功放机数量
	soundnumber nvarchar(5),--音响数量
	effectornumber nvarchar(5),--效果器数量
	songdesknumber nvarchar(5),--点歌台数量
	lcdtvnumber nvarchar(5),--液晶电视数量
	roomremark text,--房间备注
)

if exists(select * from dbo.sysobjects where name='taskinfo')  
    drop table taskinfo  
create table taskinfo(--任务管理表
	id int identity(1,1) primary key,
	roomid nvarchar(10),--房间号
	roomstate nvarchar(5),--房间状态：空闲0，使用中1
	roomconsume nvarchar(50),--包房消费
	starttime datetime,--起始时间
	endtime datetime,--结束时间
	customerid int--消费者ID
)

if exists(select * from dbo.sysobjects where name='consumelog')  
    drop table consumelog  
create table consumelog(--经营日志表
	id int identity(1,1),
	roomid nvarchar(10),--房间号
	customerid int,--消费者ID
	roomconsume nvarchar(50),--包房消费
	starttime datetime,--起始时间
	endtime datetime--结束时间
)

if exists(select * from dbo.sysobjects where name='customerinfo')  
    drop table customerinfo  
create table customerinfo(--顾客人员信息表
	customerid int identity(1,1) primary key,
	customername nvarchar(50),--顾客姓名
	customersex nvarchar(1),--顾客性别 0男 1女
	customertel nvarchar(50),--顾客联系方式
	customerage nvarchar(3),--顾客年龄
	customeridcard nvarchar(24)--顾客身份证号
)

if exists(select * from dbo.sysobjects where name='actioninfo')  
    drop table actioninfo  
create table actioninfo(--选项代码表
	id int identity(1,1),
	actioncode nvarchar(10),--选项代码
	actionname nvarchar(50),--选项名称
	groupcode nvarchar(10)--组别代码
)

if exists(select * from dbo.sysobjects where name='roompriceinfo')  
    drop table roompriceinfo  
create table roompriceinfo(--房间价格表
	id int identity(1,1) primary key,
	roomtype nvarchar(5),--房间类型：小包0，中包1，大包2，情侣包3，豪华包4，商务包5
	roomprice nvarchar(5),--房间单价/时
	starttime int,--起始时间
	endtime int--终止时间
)

if exists(select * from dbo.sysobjects where name='singerinfo')  
    drop table singerinfo  
create table singerinfo(--歌手信息表
	id int identity(1,1) primary key,
	singername nvarchar(50),--歌手姓名
	singerenglishname nvarchar(50),--歌手外文名
	singerothername nvarchar(50),--歌手别名
	singerinitials nvarchar(10),--歌手姓名首字母
	singernationality nvarchar(3),--歌手国籍
	singerphotourl nvarchar(250),--歌手照片URL
	singerclicknum nvarchar(250),--歌手热度（每播放其一首歌热度加一）
	singersex nvarchar(1),--歌手性别（男0 女1 组合2）
	singerintroduce text--歌手介绍
)

if exists(select * from dbo.sysobjects where name='musicinfo')  
    drop table musicinfo  
create table musicinfo(--音乐信息表
	id int identity(1,1) primary key,
	musicname nvarchar(50),--歌曲名称
	singerid int,--歌手ID
	singername nvarchar(50),--歌手姓名
	languagetype nvarchar(10),--语言种类(国语/粤语/韩语/英语/日语/法语/德语/俄语/其他)
	category nvarchar(50),--分类
	recordnumber nvarchar(250),--歌曲热度
	mvurl nvarchar(250),--视频文件URL
	musicnameinitials nvarchar(50),--歌曲首字母字符串
	singrail nvarchar(2),--原唱声道（-1，1）
	releasedate datetime,--发行日期
)

if exists(select * from dbo.sysobjects where name='musiccategory')  
    drop table musiccategory  
create table musiccategory(--歌曲分类配置表
	id int identity(1,1) primary key,
	categoryname nvarchar(50),--分类名称
	fatherid int,--上级分类ID
)

if exists(select * from dbo.sysobjects where name='musicrecord')  
    drop table musicrecord  
create table musicrecord(--歌曲点击量表
	id int identity(1,1) primary key,
	musicid int,--歌曲ID
	musicname nvarchar(50),--歌曲名称
	clicknum int,--点击量
	clickdate datetime,--点击时间
)