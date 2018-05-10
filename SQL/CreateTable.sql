use KTVDB;

if exists(select * from dbo.sysobjects where name='roominfo')  
    drop table roominfo  
create table roominfo(--������Ϣ��
	id int identity(1,1),
	roomid nvarchar(10) primary key,--�����
	roomtype nvarchar(2),--�������ͣ�С��0���а�1�����2�����°�3��������4�������5
	roomsize nvarchar(5),--�������
	imageurl nvarchar(250),--����ͼƬ
	microphonenumber nvarchar(5),--�������
	airconditionernumber nvarchar(5),--�յ�����
	poweramplifiernumber nvarchar(5),--���Ż�����
	soundnumber nvarchar(5),--��������
	effectornumber nvarchar(5),--Ч��������
	songdesknumber nvarchar(5),--���̨����
	lcdtvnumber nvarchar(5),--Һ����������
	roomremark text,--���䱸ע
)

if exists(select * from dbo.sysobjects where name='taskinfo')  
    drop table taskinfo  
create table taskinfo(--��������
	id int identity(1,1) primary key,
	roomid nvarchar(10),--�����
	roomstate nvarchar(5),--����״̬������0��ʹ����1
	roomconsume nvarchar(50),--��������
	starttime datetime,--��ʼʱ��
	endtime datetime,--����ʱ��
	customerid int--������ID
)

if exists(select * from dbo.sysobjects where name='consumelog')  
    drop table consumelog  
create table consumelog(--��Ӫ��־��
	id int identity(1,1),
	roomid nvarchar(10),--�����
	customerid int,--������ID
	roomconsume nvarchar(50),--��������
	starttime datetime,--��ʼʱ��
	endtime datetime--����ʱ��
)

if exists(select * from dbo.sysobjects where name='customerinfo')  
    drop table customerinfo  
create table customerinfo(--�˿���Ա��Ϣ��
	customerid int identity(1,1) primary key,
	customername nvarchar(50),--�˿�����
	customersex nvarchar(1),--�˿��Ա� 0�� 1Ů
	customertel nvarchar(50),--�˿���ϵ��ʽ
	customerage nvarchar(3),--�˿�����
	customeridcard nvarchar(24)--�˿����֤��
)

if exists(select * from dbo.sysobjects where name='actioninfo')  
    drop table actioninfo  
create table actioninfo(--ѡ������
	id int identity(1,1),
	actioncode nvarchar(10),--ѡ�����
	actionname nvarchar(50),--ѡ������
	groupcode nvarchar(10)--������
)

if exists(select * from dbo.sysobjects where name='roompriceinfo')  
    drop table roompriceinfo  
create table roompriceinfo(--����۸��
	id int identity(1,1) primary key,
	roomtype nvarchar(5),--�������ͣ�С��0���а�1�����2�����°�3��������4�������5
	roomprice nvarchar(5),--���䵥��/ʱ
	starttime int,--��ʼʱ��
	endtime int--��ֹʱ��
)

if exists(select * from dbo.sysobjects where name='singerinfo')  
    drop table singerinfo  
create table singerinfo(--������Ϣ��
	id int identity(1,1) primary key,
	singername nvarchar(50),--��������
	singerenglishname nvarchar(50),--����������
	singerothername nvarchar(50),--���ֱ���
	singerinitials nvarchar(10),--������������ĸ
	singernationality nvarchar(3),--���ֹ���
	singerphotourl nvarchar(250),--������ƬURL
	singerclicknum nvarchar(250),--�����ȶȣ�ÿ������һ�׸��ȶȼ�һ��
	singersex nvarchar(1),--�����Ա���0 Ů1 ���2��
	singerintroduce text--���ֽ���
)

if exists(select * from dbo.sysobjects where name='musicinfo')  
    drop table musicinfo  
create table musicinfo(--������Ϣ��
	id int identity(1,1) primary key,
	musicname nvarchar(50),--��������
	singerid int,--����ID
	singername nvarchar(50),--��������
	languagetype nvarchar(10),--��������(����/����/����/Ӣ��/����/����/����/����/����)
	category nvarchar(50),--����
	recordnumber nvarchar(250),--�����ȶ�
	mvurl nvarchar(250),--��Ƶ�ļ�URL
	musicnameinitials nvarchar(50),--��������ĸ�ַ���
	singrail nvarchar(2),--ԭ��������-1��1��
	releasedate datetime,--��������
)

if exists(select * from dbo.sysobjects where name='musiccategory')  
    drop table musiccategory  
create table musiccategory(--�����������ñ�
	id int identity(1,1) primary key,
	categoryname nvarchar(50),--��������
	fatherid int,--�ϼ�����ID
)

if exists(select * from dbo.sysobjects where name='musicrecord')  
    drop table musicrecord  
create table musicrecord(--�����������
	id int identity(1,1) primary key,
	musicid int,--����ID
	musicname nvarchar(50),--��������
	clicknum int,--�����
	clickdate datetime,--���ʱ��
)