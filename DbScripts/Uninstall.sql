
/*Delete Indexing Service Linked Server*/
if exists (SELECT srvname FROM master.dbo.sysservers WHERE srvname = 'AccuFlow_IndexingSrv')
EXEC sp_dropserver @server='AccuFlow_IndexingSrv', @droplogins='droplogins'
GO


/*************************************************************/


/*Drop AccuFlow Database*/
use Master
GO
if exists (SELECT name FROM master.dbo.sysdatabases WHERE name ='AccuFlow')
drop Database AccuFlow
GO

