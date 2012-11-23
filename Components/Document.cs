/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Bip.Components
{



	public class FileStorage
	{
		public static string OriginalDir
		{
			get	{ return ConfigurationSettings.AppSettings["OriginalStorageDir"];	}
		}
		public static string IndexingServiceDir
		{
			get	{ return ConfigurationSettings.AppSettings["IndexingServiceStorageDir"];	}
		}
	}


	public class DocumentEnt : BipEntity
	{
		// Document Attributes
		int m_Id = 0;
		DateTime	m_CreationTime = DateTime.MinValue;

		int m_SavedFileTypeId = -1;
		string m_SavedStorageFileName = null;
		
		TempFile m_UploadedFile = null;

		public DateTime	m_DateReceived= DateTime.MinValue,
			m_DocumentDate= DateTime.MinValue;
		public string	
			m_IncomingNumber = null,
			m_OutgoingNumber = null,
			m_Subject,
			m_Header,
			m_FileName,
			m_ArchiveFileNames;
		public int
			m_FileTypeId,
			m_DocTypeId,
			m_DocSourceId,
			m_DocCategoryId,
			m_ParentId,
			m_PreviousVersionId,
			m_OwnerUserId;
		public bool	
			m_IsPublic = true,
			m_IsRead = false, 
			m_IsFavorite = false;

		public IEnumerable m_Groups = null;
		public IEnumerable m_RefDocuments = null;

		// Document Attribute Accessors
		public int Id
		{
			get{ return m_Id; }
		}

		protected string StorageFileName
		{
			get	
			{
				if(m_FileTypeId == m_SavedFileTypeId)
					return m_SavedStorageFileName;

				DocFileType ftype = new DocFileType(m_FileTypeId);
				string fileExt = ftype.FileExtension;
				if(fileExt.Length > 0)
					fileExt = "." + fileExt;
				return m_Id.ToString() + fileExt;
			}
		}

		public DateTime CreationTime
		{
			get{ return m_CreationTime; }
		}

		public DateTime DateReceived
		{
			get{ return m_DateReceived;}
			set{ m_DateReceived = value;}
		}
		
		public DateTime DocumentDate
		{
			get{return m_DocumentDate;}
			set{m_DocumentDate = value;}
		}

		public string IncomingNumber
		{
			get{return m_IncomingNumber;}
			set{m_IncomingNumber = value;}
		}

		public string OutgoingNumber 
		{
			get{ return m_OutgoingNumber;}
			set{ m_OutgoingNumber = value;}
		}

		public string Subject
		{
			get{ return m_Subject;}
			set{ m_Subject = value;}
		}

		public string Header
		{
			get{ return m_Header;}
			set{ m_Header = value;}
		}

		public string FileName
		{
			get{ return m_FileName;}
			set{ m_FileName = value;}
		}

		public string ArchiveFileNames
		{ 
			get{ return m_ArchiveFileNames;}
			set{ m_ArchiveFileNames = value;}
		}

		public int FileTypeId
		{
			get{ return m_FileTypeId;}
		}

		public int DocTypeId
		{
			get{ return m_DocTypeId;}
			set{ m_DocTypeId = value;}
		}
		
		public int DocSourceId
		{ 
			get{ return m_DocSourceId;}
			set{ m_DocSourceId = value;}
		}

		public int DocCategoryId
		{
			get{ return m_DocCategoryId;}
			set{ m_DocCategoryId = value;}
		}
		
		public int ParentId
		{
			get{ return m_ParentId;}
			set{ m_ParentId = value;}
		}

		public int	PreviousVersionId
		{
			get{ return m_PreviousVersionId;}
			set{ m_PreviousVersionId = value;}
		}

		public int OwnerUserId
		{
			get{ return m_OwnerUserId;}
			set{ m_OwnerUserId = value;}
		}

		public bool IsPublic
		{
			get{ return m_IsPublic;}
			set{ m_IsPublic = value;}
		}

		public bool	IsRead
		{
			get{ return m_IsRead; }
			set{ m_IsRead = value;}
		}

		public bool IsFavorite
		{
			get{return m_IsFavorite;}
			set{m_IsFavorite = value;}
		}

		public IEnumerable Groups
		{
			get
			{ 
				if(m_Groups == null)
					return new ArrayList();
				 return m_Groups;
			}
			set{ m_Groups = value;}
		}

		public IEnumerable RefDocuments
		{
			get{return m_RefDocuments;}
			set{m_RefDocuments = value;}
		}

		public bool CanEdit
		{
			get
			{
				UserIdentity user = UserIdentity.Current;
				if(user.UserRole == UserRoles.Administrator ||
					user.UserRole == UserRoles.SystemOperator ||
					user.UserRole == UserRoles.Operator && user.UserId == this.OwnerUserId)
					return true;
				return false;
			}
		}
		

		// -------------------------------------
		public DocumentEnt()
		{
		}


		public  int  DbCreate(out OleDbTransaction trans)
		{
			Validate();
			System.Data.OleDb.OleDbConnection con = Db.Connection;
		
			trans = con.BeginTransaction();			
			OleDbCommand cmd = con.CreateCommand();
			cmd.Transaction = trans;
			try
			{
				CmdParams cp = new CmdParams(cmd);
				m_CreationTime = DateTime.Now;
				cmd.CommandText =   @"
				insert into documents
				(
					CreationTime,
					FileType, 
					[FileName],
					DateReceived, 
					DocumentDate, 
					IncomingNumber, 
					OutgoingNumber,
					Subject, 
					Header, 
					ArchiveFileNames, 
					DocTypeId, 
					DocSourceId, 
					DocCategoryId, 
					ParentId, 
					PreviousVersionId, 
					OwnerUserId, 
					IsPublic
				)values ( "	+ 
					cp.Add(m_CreationTime) + 
					cp.Add(m_FileTypeId) + 
					cp.Add(m_FileName) +
					cp.Add(m_DateReceived) +
					cp.Add(m_DocumentDate) +
					cp.Add(m_IncomingNumber) +
					cp.Add(m_OutgoingNumber) +
					cp.Add(m_Subject) + 
					cp.Add(m_Header) + 
					cp.Add(m_ArchiveFileNames) + 
					cp.Add(m_DocTypeId) + 
					cp.Add(m_DocSourceId) + 
					cp.Add(m_DocCategoryId) + 
					cp.Add(m_ParentId) +
					cp.Add(m_PreviousVersionId) + 
					cp.Add(m_OwnerUserId) +
					cp.Add(m_IsPublic) + " ) ";


				cmd.ExecuteNonQuery();
				cmd.CommandText="select @@identity";
				Decimal oid =  (Decimal) cmd.ExecuteScalar();
				m_Id = Convert.ToInt32(oid);

				StoreDocRefs(trans, "DocGroups", "GroupId", Groups);
				StoreDocRefs(trans, "DocRefRelated", "RelatedDocId", RefDocuments);
				cmd.Parameters.Clear();
				cmd.CommandText = "update Documents set StorageFileName = '" + StorageFileName + "' where Id = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				//trans.Commit();

				//.DEV. isRead
			}
			catch(Exception ex)
			{
				trans.Rollback();
				trans = null;
				throw ex;
			}
			
			MarkAsRead();
			
			return m_Id;
		}

		public  void  DbUpdate()
		{
			UserIdentity user = UserIdentity.Current;

			Validate();
			System.Data.OleDb.OleDbConnection con = Db.Connection;
		
			OleDbTransaction trans = con.BeginTransaction();			
			OleDbCommand cmd = con.CreateCommand();
			cmd.Transaction = trans;
			try
			{
				CmdParams cp = new CmdParams(cmd, false);
				m_CreationTime = DateTime.Now;
				cmd.CommandText =   @"
				update documents
				set	FileType = " + cp.Add(m_FileTypeId) + 
					", [FileName] = " + cp.Add(m_FileName) +
					", DateReceived = " + cp.Add(m_DateReceived) +
					", DocumentDate = " + cp.Add(m_DocumentDate) +
					", IncomingNumber = " +cp.Add(m_IncomingNumber) +
					", OutgoingNumber = " +cp.Add(m_OutgoingNumber) +
					", Subject = " +cp.Add(m_Subject) + 
					", Header = " + cp.Add(m_Header) + 
					", ArchiveFileNames = " + cp.Add(m_ArchiveFileNames) + 
					", DocTypeId = " + cp.Add(m_DocTypeId) + 
					", DocSourceId = " + cp.Add(m_DocSourceId) + 
					", DocCategoryId = " + cp.Add(m_DocCategoryId) + 
					", ParentId = " + cp.Add(m_ParentId) +
					", PreviousVersionId = " + cp.Add(m_PreviousVersionId) + 
					", IsPublic = " + cp.Add(m_IsPublic) +
					", StorageFileName = '" + StorageFileName + "' " + 
					" where id = " + m_Id.ToString();
				cmd.ExecuteNonQuery();

				StoreDocRefs(trans, "DocGroups", "GroupId", Groups);
				StoreDocRefs(trans, "DocRefRelated", "RelatedDocId", RefDocuments);

				cmd.Parameters.Clear();
				cmd.CommandText = "delete from UserReadDocs where DocId=" + m_Id.ToString() + " and UserId=" + user.UserId.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText = "delete from UserFavoriteDocs where DocId=" + m_Id.ToString() + " and UserId=" + user.UserId.ToString();
				cmd.ExecuteNonQuery();
				if(IsRead)
				{
					cmd.CommandText = "insert into UserReadDocs (DocId, UserId) values (" + m_Id.ToString() + ", " + user.UserId.ToString() + " ) ";
					cmd.ExecuteNonQuery();
				}

				if(IsFavorite)
				{
					cmd.CommandText = "insert into UserFavoriteDocs (DocId, UserId) values (" + m_Id.ToString() + ", " + user.UserId.ToString() +" ) ";
					cmd.ExecuteNonQuery();
				}

				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
		}



		public  void  DbDelete()
		{
			if(m_Id < 1)
				throw new BipFatalException("Internal Error");

			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbTransaction trans = con.BeginTransaction();			
			OleDbCommand cmd = con.CreateCommand();
			cmd.Transaction = trans;
			try
			{
				cmd.CommandText =   @"delete from DocGroups where DocId = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"delete from DocRefRelated where DocId = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"delete from UserFavoriteDocs where DocId = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"delete from UserReadDocs where DocId = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"update documents set ParentId = null where ParentId =" +  m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"update documents set PreviousVersionId = null where PreviousVersionId =" +  m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"delete from Documents where Id = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
		}



		public void StoreDocRefs(OleDbTransaction trans, string tableName, string refFieldName, IEnumerable refs)
		{
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			if(trans != null)
				cmd.Transaction = trans;
			cmd.CommandText = "delete from " + tableName + " where DocId=" + m_Id.ToString();
			cmd.ExecuteNonQuery();

			if(refs !=  null)
			{
				cmd.CommandText = "insert into " + tableName + " (DocId, " + refFieldName + " ) values ( "+ m_Id.ToString() + ", ?)";
				foreach(int ref_id in refs)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.Add(new OleDbParameter("id", ref_id));
					cmd.ExecuteNonQuery();
				}
			}

			cmd.Dispose();
		}

		public IEnumerable LoadDocRefs(string tableName, string refFieldName)
		{
			ArrayList res = new ArrayList();
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText = "select  " + refFieldName + " from " + tableName + " where DocId=" + m_Id.ToString();
			OleDbDataReader reader = cmd.ExecuteReader();
			while(reader.Read())
				res.Add(Convert.ToInt32( reader[0]));
			reader.Close();
			cmd.Dispose();
			return (IEnumerable)res;
		}


		public void Validate()
		{

			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();

			if(IncomingNumber != null && IncomingNumber.Length != 0)
			{
				cmd.CommandText =   @"select count(1) from documents where IncomingNumber=?  and id <> ?";
				cmd.Parameters.Clear();
				cmd.Parameters.Add(new OleDbParameter("IncomingNumber", IncomingNumber));
				cmd.Parameters.Add(new OleDbParameter("id", m_Id));
				bool exists = (bool)((int) cmd.ExecuteScalar() != 0);
				if(exists)
				{
					cmd.Dispose();
					throw new BipGenericException(BipResources.GetString("StrDocIncomingNumberIsNotUnique"));
				}
			}

			if(OutgoingNumber != null && OutgoingNumber.Length != 0)
			{
				cmd.CommandText =   @"select count(1) from documents where OutgoingNumber=?  and id <> ?";
				cmd.Parameters.Clear();
				cmd.Parameters.Add(new OleDbParameter("OutgoingNumber", OutgoingNumber));
				cmd.Parameters.Add(new OleDbParameter("id", m_Id));
				bool exists = (bool)((int) cmd.ExecuteScalar() != 0);
				if(exists)
				{
					cmd.Dispose();
					throw new BipGenericException(BipResources.GetString("StrDocOutgoingNumberIsNotUnique"));
				}
			}
			cmd.Dispose();
		}


		public override void New()
		{
			m_OwnerUserId = UserIdentity.Current.UserId;
			m_DateReceived = DateTime.Now;
			m_FileTypeId = 1;
		}

		public override int  Create()
		{ 
			if(! CanEdit)
				throw new BipAccessDeniedException();

			OleDbTransaction trans;
			DbCreate(out trans);
			if(trans == null)
				throw new BipFatalException();
			try
			{
				FileCreate();
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw ex;
			}

			return m_Id;
		}
		public override void Load(int id)
		{

			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText = "select IsPublic, OwnerUserId from documents where id = " + id.ToString();
			OleDbDataReader reader = cmd.ExecuteReader();
			if(!reader.Read())
			{
				reader.Close();
				throw new BipObjectNotFoundException();
			}
			int ownerUserId = DbConvert.ToInt32( reader["OwnerUserId"] );
			bool isPublic = DbConvert.ToBoolean( reader["IsPublic"] );
			reader.Close();

			UserIdentity user = UserIdentity.Current;
			if(!(
				isPublic ||
				user.UserRole == UserRoles.Administrator ||
				user.UserRole == UserRoles.SystemOperator ||
				user.UserRole == UserRoles.Operator && ownerUserId == user.UserId 
				))
			{
				cmd.CommandText = @"select top 1 1 from 
									UserGroups inner join DocGroups on
									UserGroups.GroupId = DocGroups.GroupId
									where DocGroups.DocId = " + id.ToString() +
									" and UserGroups.UserId = " +  user.UserId.ToString();
				reader = cmd.ExecuteReader();
				if(!reader.Read())
				{
					reader.Close();
					throw new BipAccessDeniedException();
				}
				reader.Close();
			}
			
			cmd.CommandText =   @"
				select documents.*, 
				IsRead=case 
				when exists (select top 1 1 from UserReadDocs where DocId = Documents.Id and UserId=" + user.UserId.ToString() + @") then 1 else 0 end,
				IsFavorite=case 
				when exists (select top 1 1 from UserFavoriteDocs where DocId = Documents.Id and UserId=" + user.UserId.ToString() + @") then 1 else 0 end
				from documents 
				where documents.Id= " + id.ToString();

			reader = cmd.ExecuteReader();
			if(!reader.Read())
				throw new BipObjectNotFoundException();
			
			m_Id = id;
			m_CreationTime = DbConvert.ToDateTime(reader["CreationTime"]);
			m_FileTypeId = DbConvert.ToInt32(reader["FileType"]);
			m_SavedStorageFileName = DbConvert.ToString(reader["StorageFileName"]);
			m_FileName = DbConvert.ToString(reader["FileName"]);
			m_DateReceived = DbConvert.ToDateTime(reader["DateReceived"]);
			m_DocumentDate = DbConvert.ToDateTime(reader["DocumentDate"]);
			m_IncomingNumber = DbConvert.ToString(reader["IncomingNumber"]);
			m_OutgoingNumber = DbConvert.ToString(reader["OutgoingNumber"]);
			m_Subject = DbConvert.ToString(reader["Subject"]);
			m_Header = DbConvert.ToString(reader["Header"]);
			m_ArchiveFileNames = DbConvert.ToString(reader["ArchiveFileNames"]);
			m_DocTypeId = DbConvert.ToInt32(reader["DocTypeId"]);
			m_DocSourceId = DbConvert.ToInt32(reader["DocSourceId"]);
			m_DocCategoryId = DbConvert.ToInt32(reader["DocCategoryId"]);
			m_ParentId = DbConvert.ToInt32(reader["ParentId"]);
			m_PreviousVersionId = DbConvert.ToInt32(reader["PreviousVersionId"]);
			m_OwnerUserId = DbConvert.ToInt32(reader["OwnerUserId"]);
			m_IsPublic = DbConvert.ToBoolean(reader["IsPublic"]);
		
			
			m_IsRead = DbConvert.ToBoolean(reader["IsRead"]);
			m_IsFavorite = DbConvert.ToBoolean(reader["IsFavorite"]);
			reader.Close();
			cmd.Dispose();

			if(!CanRead(m_ParentId))
				m_ParentId = 0;
			if(!CanRead(m_PreviousVersionId))
				m_PreviousVersionId = 0;

			m_RefDocuments = CanRead(LoadDocRefs( "DocRefRelated", "RelatedDocId" ));
			m_Groups = LoadDocRefs( "DocGroups", "GroupId" );
			
			m_SavedFileTypeId = m_FileTypeId;
			
			if(!m_IsRead)
				MarkAsRead();
		}
		public override void Update()
		{
			if(! CanEdit)
				throw new BipAccessDeniedException();

			DbUpdate(); 
			FileUpdate();
		}
		public override void Delete()
		{
			if(! CanEdit)
				throw new BipAccessDeniedException();

			DbDelete();
			FileDelete();
		}

		static public bool CanRead(int id)
		{
			UserIdentity user = UserIdentity.Current;
			if(user.UserRole == UserRoles.Administrator ||
				user.UserRole == UserRoles.SystemOperator)
				return true;

			Database db = new Database();
			System.Data.OleDb.OleDbConnection con = db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText = 
				@"select count(1) from documents
					where id = " + id.ToString() + 
					@" and( IsPublic=1 or OwnerUserId= " + user.UserId.ToString() + 
					@" or exists 
					(select top 1 1 from 
					UserGroups inner join DocGroups on
					UserGroups.GroupId = DocGroups.GroupId
					where DocGroups.DocId = Documents.id 
					and UserGroups.UserId = " + user.UserId.ToString() + " ))";

			bool canRead = (bool)(((int)cmd.ExecuteScalar()) > 0 );
			cmd.Dispose();
			db.Dispose();
			return canRead;
		}

		static public IEnumerable CanRead(IEnumerable ids)
		{
			if(ids == null)
				return null;
			IEnumerator enum_ids = ids.GetEnumerator();
			enum_ids.Reset();
			if(!enum_ids.MoveNext())
				return ids;

			
			UserIdentity user = UserIdentity.Current;
			if(user.UserRole == UserRoles.Administrator ||
				user.UserRole == UserRoles.SystemOperator)
				return ids;

			ArrayList res = new ArrayList();
			Database db = new Database();
			System.Data.OleDb.OleDbConnection con = db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText = @"
				select id from documents
				where id in ( " + EnumUtils.ConvertToString(ids)  + 
				@" ) and 
				(IsPublic=1 or OwnerUserId= " + user.UserId.ToString() + 
				@" or exists 
				(select top 1 1 from 
				UserGroups inner join DocGroups on
				UserGroups.GroupId = DocGroups.GroupId
				where DocGroups.DocId = Documents.id 
				and UserGroups.UserId = " + user.UserId.ToString() + " ))";

			OleDbDataReader reader = cmd.ExecuteReader();
			while(reader.Read())
			{
				res.Add(Convert.ToInt32(reader["id"]));
			}
			reader.Close();
			cmd.Dispose();
			db.Dispose();

			return res;
		}

	
		public void ConfigureFileType(int newFileType)
		{
			if(m_FileTypeId == newFileType)
				return;
			m_FileTypeId = newFileType;
		}

		protected void SaveFileTypeInfo()
		{
			m_SavedFileTypeId = m_FileTypeId;
			m_SavedStorageFileName= StorageFileName;
		}
		protected byte[] LoadFile(string filePath)
		{
			if(!File.Exists(filePath))
				throw new  BipObjectNotFoundException();

			FileStream stm = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte []fileContents = new byte[stm.Length];
			stm.Read(fileContents, 0, (int)stm.Length);
			stm.Close();
			return fileContents;
		}

		public byte[] DownloadFile(bool original)
		{
			// original param is not used because no HTML convertion is perfomed 
			if(m_Id < 1)
			{
				return LoadFile(m_UploadedFile.Path);
			}

			return LoadFile(FileStorage.OriginalDir + "\\" + m_Id.ToString());
		}

		public void UploadFile(Stream stm, string fileName)
		{
			if(m_UploadedFile == null)
				m_UploadedFile = new TempFile();
			string filePath = m_UploadedFile.Path;
			byte []buffer =new Byte[stm.Length];
			stm.Read(buffer, 0 , (int)stm.Length);
			stm.Close();
			File.Delete(filePath);
			Stream dstFile = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
			dstFile.Write(buffer, 0, (int)buffer.Length);
			dstFile.Close();
			ConfigureFileType( Bip.Components.DocFileType.ExamineFileType(Path.GetExtension(fileName)));
			FileName = fileName;
		}

		public void FileCreate()
		{
			if(m_Id <1)
				throw new BipFatalException();

			File.Copy(m_UploadedFile.Path, 	
						FileStorage.OriginalDir + "\\" + m_Id.ToString(),
						true);

			m_SavedFileTypeId = 0;
			m_SavedStorageFileName = "";
			FileUpdate();
		}

		public void FileUpdate()
		{
			if(m_SavedFileTypeId == m_FileTypeId)
				return;
			
			if(m_SavedStorageFileName != null && m_SavedStorageFileName.Length > 0)
				File.Delete(FileStorage.IndexingServiceDir + "\\" + m_SavedStorageFileName);
			string filePath = FileStorage.IndexingServiceDir + "\\" + StorageFileName;
			File.Copy(FileStorage.OriginalDir + "\\" + m_Id.ToString(), filePath, true); 
			
			SaveFileTypeInfo();
			
		}
		public void FileDelete()
		{
            if(m_Id <1)
				return;
			try
			{
				File.Delete(FileStorage.IndexingServiceDir + "\\" + m_SavedStorageFileName);
			}
			catch(Exception){}

			try
			{
				File.Delete(FileStorage.OriginalDir + "\\" + m_Id.ToString());
			}
			catch(Exception){}
		}
		
		public string GetFileUrl(bool original)
		{
			string url = "~/Documents/DocFileDownload.aspx?TC=1";
            if(original)
				url += "&Org=1";
			return url;
		}

		public void MarkAsRead()
		{
			if(m_Id < 1)
				return;

			OleDbCommand cmd = Db.Connection.CreateCommand();
			cmd.CommandText = "insert into userReadDocs (DocId, UserId) values ( " + m_Id.ToString() +
				"," + UserIdentity.Current.UserId.ToString() +" )";
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(Exception){}
		}

		public static DataTable FindEnum(IEnumerable ids)
		{
			if(ids == null)
				return null;

			IEnumerator enum_ids = ids.GetEnumerator();
			enum_ids.Reset();
			if(!enum_ids.MoveNext())
				return null;

			
			string securityConstraint = null;

			UserIdentity user = UserIdentity.Current;
			if(user.UserRole != UserRoles.Administrator &&
				user.UserRole != UserRoles.SystemOperator)
			securityConstraint = 
				" (IsPublic=1 or OwnerUserId= " + user.UserId.ToString() + 
				@" or exists 
				(select top 1 1 from 
				UserGroups inner join DocGroups on
				UserGroups.GroupId = DocGroups.GroupId
				where DocGroups.DocId = Documents.id 
				and UserGroups.UserId = " + user.UserId.ToString() + " )) ";

			string selectDocs = @"
				select * from documents
				where id in ( " + EnumUtils.ConvertToString(ids)  + " ) ";
			if(securityConstraint != null)
				selectDocs += " and " + securityConstraint;
		
			DataTable res = new DataTable();
			Database db = new Database();
			OleDbDataAdapter adapter = new OleDbDataAdapter(selectDocs,db.Connection);
			adapter.Fill(res);
			db.Dispose();

			return res;
		}
	}
}
