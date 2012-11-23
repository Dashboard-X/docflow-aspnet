
/*Delete Indexing Service Linked Server*/
if exists (SELECT srvname FROM master.dbo.sysservers WHERE srvname = 'AccuFlow_IndexingSrv')
EXEC sp_dropserver @server='AccuFlow_IndexingSrv', @droplogins='droplogins'
GO


/*************************************************************/


/*Create Indexing Service Linked Server*/
EXEC sp_addlinkedserver @server='AccuFlow_IndexingSrv',	
			@srvproduct='AccuFlow_IndexingSrv', 
			@provider='MSIDXS', 
			@datasrc='AccuFlow_IndexingSrv', 
			@location='AccuFlow_IndexingSrv';
GO


/*************************************************************/


/* CreateDatabase */
if not exists (SELECT name FROM master.dbo.sysdatabases WHERE name ='AccuFlow')
CREATE DATABASE AccuFlow
GO
use AccuFlow
GO


/*************************************************************/

/****** Object:  Table [dbo].[DocCategories]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DocCategories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DocCategories]
GO

/****** Object:  Table [dbo].[DocGroups]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DocGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DocGroups]
GO

/****** Object:  Table [dbo].[DocRefRelated]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DocRefRelated]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DocRefRelated]
GO

/****** Object:  Table [dbo].[DocSources]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DocSources]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DocSources]
GO

/****** Object:  Table [dbo].[DocTypes]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DocTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DocTypes]
GO

/****** Object:  Table [dbo].[Documents]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Documents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Documents]
GO

/****** Object:  Table [dbo].[FileTypes]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FileTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FileTypes]
GO

/****** Object:  Table [dbo].[Groups]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Groups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Groups]
GO

/****** Object:  Table [dbo].[UserFavoriteDocs]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserFavoriteDocs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserFavoriteDocs]
GO

/****** Object:  Table [dbo].[UserGroups]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserGroups]
GO

/****** Object:  Table [dbo].[UserReadDocs]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserReadDocs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserReadDocs]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users]
GO

/****** Object:  User Defined Datatype UDT_ENUM    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.systypes where name = N'UDT_ENUM')
exec sp_droptype N'UDT_ENUM'
GO

/****** Object:  User Defined Datatype UDT_LONGSTRING    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.systypes where name = N'UDT_LONGSTRING')
exec sp_droptype N'UDT_LONGSTRING'
GO

/****** Object:  User Defined Datatype UDT_OBJID    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.systypes where name = N'UDT_OBJID')
exec sp_droptype N'UDT_OBJID'
GO

/****** Object:  User Defined Datatype UDT_STRING    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.systypes where name = N'UDT_STRING')
exec sp_droptype N'UDT_STRING'
GO

/****** Object:  User Defined Datatype UDT_TEXT    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.systypes where name = N'UDT_TEXT')
exec sp_droptype N'UDT_TEXT'
GO

/****** Object:  User Defined Datatype UDT_CODE    Script Date: 02.07.2002 4:01:35 PM ******/
if exists (select * from dbo.systypes where name = N'UDT_CODE')
exec sp_droptype N'UDT_CODE'
GO

/****** Object:  User Defined Datatype UDT_CODE    Script Date: 02.07.2002 4:01:36 PM ******/
setuser
GO

EXEC sp_addtype N'UDT_CODE', N'char (10)', N'not null'
GO

setuser
GO

/****** Object:  User Defined Datatype UDT_ENUM    Script Date: 02.07.2002 4:01:36 PM ******/
setuser
GO

EXEC sp_addtype N'UDT_ENUM', N'char (1)', N'not null'
GO

setuser
GO

/****** Object:  User Defined Datatype UDT_LONGSTRING    Script Date: 02.07.2002 4:01:36 PM ******/
setuser
GO

EXEC sp_addtype N'UDT_LONGSTRING', N'varchar (150)', N'not null'
GO

setuser
GO

/****** Object:  User Defined Datatype UDT_OBJID    Script Date: 02.07.2002 4:01:36 PM ******/
setuser
GO

EXEC sp_addtype N'UDT_OBJID', N'numeric(19,0)', N'not null'
GO

setuser
GO

/****** Object:  User Defined Datatype UDT_STRING    Script Date: 02.07.2002 4:01:36 PM ******/
setuser
GO

EXEC sp_addtype N'UDT_STRING', N'varchar (50)', N'not null'
GO

setuser
GO

/****** Object:  User Defined Datatype UDT_TEXT    Script Date: 02.07.2002 4:01:36 PM ******/
setuser
GO

EXEC sp_addtype N'UDT_TEXT', N'varchar (2000)', N'not null'
GO

setuser
GO

/****** Object:  Table [dbo].[DocCategories]    Script Date: 02.07.2002 4:01:36 PM ******/
CREATE TABLE [dbo].[DocCategories] (
	[Id] [UDT_OBJID] IDENTITY (1, 1) NOT NULL ,
	[Name] [UDT_LONGSTRING] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DocGroups]    Script Date: 02.07.2002 4:01:36 PM ******/
CREATE TABLE [dbo].[DocGroups] (
	[GroupId] [UDT_OBJID] NOT NULL ,
	[DocId] [UDT_OBJID] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DocRefRelated]    Script Date: 02.07.2002 4:01:36 PM ******/
CREATE TABLE [dbo].[DocRefRelated] (
	[RelatedDocId] [UDT_OBJID] NOT NULL ,
	[DocId] [UDT_OBJID] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DocSources]    Script Date: 02.07.2002 4:01:36 PM ******/
CREATE TABLE [dbo].[DocSources] (
	[Id] [UDT_OBJID] IDENTITY (1, 1) NOT NULL ,
	[Name] [UDT_LONGSTRING] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DocTypes]    Script Date: 02.07.2002 4:01:36 PM ******/
CREATE TABLE [dbo].[DocTypes] (
	[Id] [UDT_OBJID] IDENTITY (1, 1) NOT NULL ,
	[Name] [UDT_LONGSTRING] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Documents]    Script Date: 02.07.2002 4:01:36 PM ******/
CREATE TABLE [dbo].[Documents] (
	[Id] [UDT_OBJID] IDENTITY (1, 1) NOT NULL ,
	[DocCategoryId] [UDT_OBJID] NULL ,
	[DocSourceId] [UDT_OBJID] NULL ,
	[DocTypeId] [UDT_OBJID] NULL ,
	[StorageFileName] [char] (25) COLLATE Cyrillic_General_CI_AS NULL ,
	[DateReceived] [datetime] NULL ,
	[CreationTime] [datetime] NOT NULL ,
	[DocumentDate] [datetime] NULL ,
	[IncomingNumber] [UDT_LONGSTRING] NULL ,
	[OutgoingNumber] [UDT_LONGSTRING] NULL ,
	[Subject] [UDT_TEXT] NULL ,
	[Header] [UDT_TEXT] NOT NULL ,
	[FileName] [UDT_LONGSTRING] NOT NULL ,
	[ArchiveFileNames] [UDT_TEXT] NULL ,
	[FileType] [numeric](3, 0) NULL ,
	[PreviousVersionId] [UDT_OBJID] NULL ,
	[ParentId] [UDT_OBJID] NULL ,
	[OwnerUserId] [UDT_OBJID] NOT NULL ,
	[IsPublic] [bit] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FileTypes]    Script Date: 02.07.2002 4:01:37 PM ******/
CREATE TABLE [dbo].[FileTypes] (
	[Id] [UDT_OBJID] NOT NULL ,
	[Extension] [UDT_STRING] NULL ,
	[ContentType] [UDT_STRING] NULL ,
	[Name] [UDT_STRING] NOT NULL ,
	[ShowInBrowser] [bit] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Groups]    Script Date: 02.07.2002 4:01:37 PM ******/
CREATE TABLE [dbo].[Groups] (
	[Id] [UDT_OBJID] IDENTITY (1, 1) NOT NULL ,
	[Name] [UDT_LONGSTRING] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserFavoriteDocs]    Script Date: 02.07.2002 4:01:37 PM ******/
CREATE TABLE [dbo].[UserFavoriteDocs] (
	[DocId] [UDT_OBJID] NOT NULL ,
	[UserId] [UDT_OBJID] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserGroups]    Script Date: 02.07.2002 4:01:37 PM ******/
CREATE TABLE [dbo].[UserGroups] (
	[UserId] [UDT_OBJID] NOT NULL ,
	[GroupId] [UDT_OBJID] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserReadDocs]    Script Date: 02.07.2002 4:01:37 PM ******/
CREATE TABLE [dbo].[UserReadDocs] (
	[UserId] [UDT_OBJID] NOT NULL ,
	[DocId] [UDT_OBJID] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 02.07.2002 4:01:37 PM ******/
CREATE TABLE [dbo].[Users] (
	[Id] [UDT_OBJID] IDENTITY (1, 1) NOT NULL ,
	[Login] [UDT_STRING] NOT NULL ,
	[Password] [UDT_STRING] NOT NULL ,
	[FirstName] [UDT_STRING] NOT NULL ,
	[LastName] [UDT_STRING] NOT NULL ,
	[Email] [UDT_LONGSTRING] NULL ,
	[Role] [UDT_ENUM] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocCategories] WITH NOCHECK ADD 
	CONSTRAINT [PK__DocCategories__2D27B809] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [IX_DocCategories] ON [dbo].[DocCategories]([Name]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocGroups] WITH NOCHECK ADD 
	 PRIMARY KEY  NONCLUSTERED 
	(
		[DocId],
		[GroupId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[DocRefRelated] WITH NOCHECK ADD 
	 PRIMARY KEY  NONCLUSTERED 
	(
		[RelatedDocId],
		[DocId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[DocSources] WITH NOCHECK ADD 
	CONSTRAINT [PK__DocSources__32E0915F] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [IX_DocSources] ON [dbo].[DocSources]([Name]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocTypes] WITH NOCHECK ADD 
	CONSTRAINT [PK__DocTypes__34C8D9D1] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [IX_DocTypes] ON [dbo].[DocTypes]([Name]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Documents] WITH NOCHECK ADD 
	CONSTRAINT [PK__Documents__36B12243] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [XAK1Documents] ON [dbo].[Documents]([StorageFileName]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FileTypes] WITH NOCHECK ADD 
	CONSTRAINT [PK__FileTypes__38996AB5] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Groups] WITH NOCHECK ADD 
	CONSTRAINT [PK__Groups__3A81B327] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [IX_Groups] ON [dbo].[Groups]([Name]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserFavoriteDocs] WITH NOCHECK ADD 
	 PRIMARY KEY  NONCLUSTERED 
	(
		[DocId],
		[UserId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[UserGroups] WITH NOCHECK ADD 
	 PRIMARY KEY  NONCLUSTERED 
	(
		[GroupId],
		[UserId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[UserReadDocs] WITH NOCHECK ADD 
	 PRIMARY KEY  NONCLUSTERED 
	(
		[DocId],
		[UserId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users] WITH NOCHECK ADD 
	CONSTRAINT [PK__Users__4222D4EF] PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  INDEX [IX_Users] ON [dbo].[Users]([Login]) ON [PRIMARY]
GO


/*************************************************************/


/*Load master data*/
use AccuFlow
GO
delete  from documents
delete  from users
delete  from groups
delete  from filetypes
delete  from doctypes
delete  from docsources
delete  from doccategories

INSERT INTO groups (name) VALUES('Administration')
INSERT INTO groups (name) VALUES('Book-keeping')
INSERT INTO groups (name) VALUES('Research and Development')
INSERT INTO groups (name) VALUES('Marketing department')

INSERT INTO users (login, password, firstname, lastname, email, role) VALUES('admin', 'admin', 'FirstName', 'LastName', 'admin@email.com', 'A')

INSERT INTO filetypes (id, extension, contenttype, name,ShowInBrowser) VALUES(1, '', 'Unknown', 'Unknown', '0')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(2, 'txt', 'text/plain', 'Plain text', '1')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(3, 'rtf', 'application/msword', 'RTF', '1')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(4, 'doc', 'application/msword', 'MS Word', '1')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(5, 'xls', 'application/vnd.ms-excel', 'MS Excel', '1')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(6, 'pdf', 'application/pdf', 'PDF', '1')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(7, 'ppt', 'application/vnd.ms-powerpoint', 'MS PowerPoint', '1')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(8, 'mpp', 'application/msword', 'MS Project', '0')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(9, 'mdb', 'application/msaccess', 'MS Access', '0')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(10, 'zip', 'application/x-compressed', 'ZIP file', '0')
INSERT INTO filetypes (id, extension, contenttype, name, ShowInBrowser) VALUES(11, 'exe', 'application/x-msdownload', 'Application', '0')

INSERT INTO doctypes (name) VALUES('General')
INSERT INTO doctypes (name) VALUES('Instruction')
INSERT INTO doctypes (name) VALUES('Action Item')

INSERT INTO docsources (name) VALUES('Administration')
INSERT INTO docsources (name) VALUES('Book-keeping')
INSERT INTO docsources (name) VALUES('Reporting service')

INSERT INTO doccategories (name) VALUES('Personal')
INSERT INTO doccategories (name) VALUES('Confidential')
INSERT INTO doccategories (name) VALUES('Category3')
INSERT INTO doccategories (name) VALUES('Category4')
go

