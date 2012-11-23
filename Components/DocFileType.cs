/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Collections;

namespace Bip.Components
{
	/// <summary>
	/// Summary description for DocFileType.
	/// </summary>
	public class DocFileType
	{
		protected int m_Id;
		protected string m_Name;
		protected string m_FileExtension;
		protected string m_ContentType;
		protected bool m_ShowInBrowser;


		public int Id
		{
			get{ return m_Id;}
		}

		public string Name
		{
			get{ return m_Name; }
		}

		public bool ShowInBrowser
		{
			get{ return m_ShowInBrowser; }
		}

		public string FileExtension
		{
			get{ return m_FileExtension; } 
		}

		public string ContentType
		{
			get{ return m_ContentType;}
		}


		public DocFileType(int id)
		{
			m_Id = id;
			m_Name = GetTypeInfo(m_Id, out m_FileExtension, out m_ContentType, out m_ShowInBrowser);
		}

		public static DataTable FindAll()
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			string commandText = "select * from FileTypes";
			OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, db.Connection);
			DataTable table= new DataTable();
			adapter.Fill(table);
			db.Dispose();
			return table;
		}

		public static  string GetTypeInfo(int id, out string extension, out string contentType, out bool showInBrowser)
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			cmd.CommandText = "select * from FileTypes where id = " + id.ToString();
			OleDbDataReader reader = cmd.ExecuteReader();
			string typeName = "";
			extension = "";
			contentType = "";
			showInBrowser = false;
			if(reader.Read())
			{
				typeName = (string) reader["Name"];
				extension = (string) reader["Extension"];
				contentType = (string) reader["ContentType"];
				showInBrowser = DbConvert.ToBoolean(reader["ShowInBrowser"]);
			}
			
			reader.Close();
			cmd.Dispose();

			if(typeName == null || typeName == "")
				throw new BipFatalException();
			return typeName;
		}

		public static  string GetTypeName(int id)
		{
			string s1,s2;
			bool b1;
			return GetTypeInfo(id, out s1, out s2, out b1);
		}

		public static int ExamineFileType(string fileExtension) 
		{
			DataTable tab = FindAll();
			string fileExt = fileExtension.ToUpper().Trim(); 
			if(fileExt.StartsWith("."))
				fileExt = fileExt.Substring(1);

			foreach(DataRow row in tab.Rows)
			{
				string typeExt = Convert.ToString(row["Extension"]).ToUpper();
				if( typeExt.Equals( fileExt ))
				{
					int id = Convert.ToInt32(row["Id"]);
					tab.Dispose();
					return id;
				}
			}

			return 1; //Unknown
		}

	}


}
