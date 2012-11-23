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
	/// Summary description for Dictionaries.
	/// </summary>
	public class SimpleDictionary : BipEntity
	{
		protected int m_Id = -1;
		protected string m_Name = "";

		
		public int Id
		{
			get{return 0; }
		}

        public string Name
		{
			get{return m_Name;}
			set{m_Name = value.Trim();}
		}


		public SimpleDictionary()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		protected void Validate()
		{
			if(m_Name.Length==0)
				throw new Exception("StrRequiredParameterNotSpecified");
			
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"select count(1) from " + this.GetDbTableName() + 
				@" where name=? and id <> ?";
		
			cmd.Parameters.Add(new OleDbParameter("name", m_Name));
			cmd.Parameters.Add(new OleDbParameter("id", m_Id));
			bool nameExists = (bool)((int) cmd.ExecuteScalar() != 0);
			cmd.Dispose();
			if(nameExists)
			{
				throw new BipGenericException(BipResources.GetString("StrSpecifiedNameIsNotUnique"));
			}
		}
		
		public override void New()
		{
			m_Id = -1;
			m_Name = "";
		}

		public override int  Create()
		{
			Validate();
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"insert into " + this.GetDbTableName() + 
							@" (name) values (?)";

			cmd.Parameters.Add(new OleDbParameter("name", m_Name));
			cmd.ExecuteNonQuery();
			cmd.CommandText="select @@identity";
			
			Decimal oid =  (Decimal) cmd.ExecuteScalar();
			m_Id = Convert.ToInt32(oid);
			return m_Id;
		}
		public override void Load(int id)
		{
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"select * from  " + this.GetDbTableName() + 
								@" where id = ?";
			cmd.Parameters.Add(new OleDbParameter("id", id));
			OleDbDataReader reader = cmd.ExecuteReader();
			if(!reader.Read())
			{
				reader.Close();
				cmd.Dispose();
				throw new BipObjectNotFoundException();
			}
			m_Name = (string)reader["name"];
			m_Id = id;
			reader.Close();
			cmd.Dispose();
		}

		public override void Update()
		{
			Validate();
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"update " + this.GetDbTableName() + 
				@" set name=? where id=?";

			cmd.Parameters.Add(new OleDbParameter("name", m_Name));
			cmd.Parameters.Add(new OleDbParameter("id", m_Id));

			cmd.ExecuteNonQuery();
		}

		public override void Delete()
		{
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbTransaction trans = con.BeginTransaction();
			try
			{
				OleDbCommand cmd = con.CreateCommand();
				cmd.Transaction = trans;
				cmd.CommandText =   @"delete from  " + GetDbTableName() + 
					@" where id = " + m_Id.ToString();
				cmd.ExecuteNonQuery();

				if(GetForeignDocField() != null)
				{
					cmd.CommandText =   @"update documents set " + GetForeignDocField()  
						+ " = NULL where " + GetForeignDocField() + " = " + m_Id.ToString();
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

		
		protected virtual string GetDbTableName()
		{
			return "Groups";
		}

		protected virtual string GetForeignDocField()
		{
			return null;
		}

		protected static DataTable FindAllEntries(string tableName)
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			string commandText = "select * from " + tableName + " order by name";
			OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, db.Connection);
			DataTable table= new DataTable();
			adapter.Fill(table);
			db.Dispose();
			return table;
		}

		protected static DataTable FindEnumEntries(string tableName, IEnumerable ids)
		{
			if(ids == null)
				return null;

			IEnumerator enum_ids = ids.GetEnumerator();
			enum_ids.Reset();
			if(!enum_ids.MoveNext())
				return null;

			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();

			string commandText = "select * from " + tableName + 
				" where id in ( " + EnumUtils.ConvertToString(ids) + " ) " +
				" order by name ";
			
			OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, db.Connection);
			DataTable table= new DataTable();
			adapter.Fill(table);
			db.Dispose();
			return table;
		}

		protected static string GetEntityName(string tableName, int id)
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			string commandText = "select * from " + tableName + " where id = " + id.ToString();
			OleDbDataReader reader = cmd.ExecuteReader();
			string res = "";
			if(reader.Read())
				res = (string)reader[0];

			reader.Close();
			cmd.Dispose();
			return res;
		}
	}

	public class DocTypeEnt : SimpleDictionary
	{
		protected static string  DbTableName = "DocTypes";
		protected override string GetDbTableName()
		{
			return DbTableName;
		}
		protected override string GetForeignDocField()
		{
			return "DocTypeId";
		}
		
		public static DataTable FindAll()
		{
			return FindAllEntries(DbTableName);
		}

		public static DataTable FindEnum(IEnumerable ids)
		{
			return FindEnumEntries(DbTableName, ids);
		}


		protected static string GetEntityName(int id)
		{
			return GetEntityName(DbTableName, id);
		}
	}

	public class DocSourceEnt : SimpleDictionary
	{
		protected static string  DbTableName = "DocSources";
		protected override string GetDbTableName()
		{
			return DbTableName;
		}
		protected override string GetForeignDocField()
		{
			return "DocSourceId";
		}

		public static DataTable FindAll()
		{
			return FindAllEntries(DbTableName);
		}

		public static DataTable FindEnum(IEnumerable ids)
		{
			return FindEnumEntries(DbTableName, ids);
		}

		protected static string GetEntityName(int id)
		{
			return GetEntityName(DbTableName, id);
		}

	}

	public class DocCategoryEnt : SimpleDictionary
	{
		protected static string  DbTableName = "DocCategories";
		protected override string GetDbTableName()
		{
			return DbTableName;
		}
		protected override string GetForeignDocField()
		{
			return "DocCategoryId";
		}
		public static DataTable FindAll()
		{
			return FindAllEntries(DbTableName);
		}

		public static DataTable FindEnum(IEnumerable ids)
		{
			return FindEnumEntries(DbTableName, ids);
		}

		protected static string GetEntityName(int id)
		{
			return GetEntityName(DbTableName, id);
		}

	}


}
