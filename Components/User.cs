/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;


namespace Bip.Components
{
	public class UserEnt : Bip.Components.BipEntity
	{
		protected int m_Id = -1;
		protected string m_Login = "";
		protected string m_Password = "";
		protected string m_FirstName ="";
		protected string m_LastName = "";
		protected string m_Email = "";
		protected string m_Role = "";
		protected IEnumerable m_Groups = null;

		
		public int Id
		{
			get{return 0; }
		}

		public string Login
		{
			get{return m_Login;}
			set{m_Login = value.Trim();}
		}
		
		public string Password
		{
			get{return m_Password;}
			set{m_Password = value.Trim();}
		}

		public string FirstName
		{
			get{return m_FirstName;}
			set{m_FirstName = value;}
		}

		public string LastName
		{
			get {return m_LastName;}
			set {m_LastName = value;}
		}

		public string Email
		{
			get {return m_Email;}
			set {m_Email = value;}
		}


		public string Role
		{
			get {return m_Role;}
			set {m_Role = value;}
		}

		public IEnumerable Groups
		{
			get{
				if(m_Groups == null)
					return new ArrayList();
				return m_Groups;
			}
			set{m_Groups = value;}
		}

		public UserEnt()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		protected void Validate()
		{
			if(m_Login.Length==0 || m_Role.Length==0)
				throw new BipGenericException(BipResources.GetString("StrRequiredParameterNotSpecified"));
			
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"select count(1) from users where Login=? and id <> ?";
			cmd.Parameters.Add(new OleDbParameter("Login", m_Login));
			cmd.Parameters.Add(new OleDbParameter("id", m_Id));
			bool LoginExists = (bool)((int) cmd.ExecuteScalar() != 0);
			cmd.Dispose();
			if(LoginExists)
			{
				throw new BipGenericException(BipResources.GetString("StrSpecifiedNameIsNotUnique"));
			}
		}
		
		public override void New()
		{
			m_Id = -1;
			m_Id = -1;
			m_Login = "";
			m_FirstName ="";
			m_LastName = "";
			m_Role = "";
			m_Groups = null;		
		}

		public override int  Create()
		{
			Validate();
			System.Data.OleDb.OleDbConnection con = Db.Connection;
		
			OleDbTransaction trans = con.BeginTransaction();			
			OleDbCommand cmd = con.CreateCommand();
			cmd.Transaction = trans;
			try
			{
				cmd.CommandText =   @"insert into users (Login, Password, FirstName, LastName, Email, Role) values (?,?,?,?,?,?)";

				cmd.Parameters.Add(new OleDbParameter("Login", m_Login));
				cmd.Parameters.Add(new OleDbParameter("Password", m_Password));
				cmd.Parameters.Add(new OleDbParameter("FirstName", m_FirstName));
				cmd.Parameters.Add(new OleDbParameter("LastName", m_LastName));
				cmd.Parameters.Add(new OleDbParameter("Email", m_Email));
				cmd.Parameters.Add(new OleDbParameter("Role", m_Role));
				cmd.ExecuteNonQuery();
				cmd.CommandText="select @@identity";
				Decimal oid =  (Decimal) cmd.ExecuteScalar();
				m_Id = Convert.ToInt32(oid);


				if(m_Groups != null)
				{

					cmd.CommandText =   @"insert into usergroups (UserId, GroupId) values (?,?)";
					cmd.Parameters.Clear();
					cmd.Parameters.Add( new OleDbParameter("UserId", typeof(int)) );
					cmd.Parameters.Add( new OleDbParameter("GroupId", typeof(int)) );


					//cmd.Prepare();
					foreach(int groupId in m_Groups)
					{
						cmd.Parameters["UserId"].Value=m_Id;
						cmd.Parameters["GroupId"].Value=groupId;
						cmd.ExecuteNonQuery();
					}
				}

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
			cmd.CommandText =   @"select * from  users where id =" + id.ToString();
			OleDbDataReader reader = cmd.ExecuteReader();
			if(!reader.Read())
			{
				reader.Close();
				cmd.Dispose();
				throw new BipObjectNotFoundException();
			}
			m_Login = (string)reader["Login"];
			m_Password = DbConvert.ToString(reader["Password"]);
			m_FirstName = DbConvert.ToString(reader["FirstName"]);
			m_LastName = DbConvert.ToString(reader["LastName"]);
			m_Email = DbConvert.ToString(reader["Email"]);
			m_Role = DbConvert.ToString(reader["Role"]);
			m_Id = id;

			cmd.CommandText =   @"select GroupId from  usergroups where Userid =" + id.ToString();
			reader.Close();
			reader = cmd.ExecuteReader();
			ArrayList groups = new ArrayList();
			while(reader.Read())
			{
				groups.Add(Convert.ToInt32(reader.GetDecimal(0)));
			}
			reader.Close();
			cmd.Dispose();

			if(groups.Count != 0)
					m_Groups = groups;
			else	m_Groups = null;

		}

		public override void Update()
		{
			Validate();


			System.Data.OleDb.OleDbConnection con = Db.Connection;
		
			OleDbTransaction trans = con.BeginTransaction();			
			OleDbCommand cmd = con.CreateCommand();
			cmd.Transaction = trans;
			try
			{
				cmd.CommandText =   @"update users set Login=?, Password=?, FirstName=?, LastName=?, Email=?, Role=? where id=" +m_Id.ToString();
				cmd.Parameters.Add(new OleDbParameter("Login", m_Login));
				cmd.Parameters.Add(new OleDbParameter("Password", m_Password));
				cmd.Parameters.Add(new OleDbParameter("FirstName", m_FirstName));
				cmd.Parameters.Add(new OleDbParameter("LastName", m_LastName));
				cmd.Parameters.Add(new OleDbParameter("Email", m_Email));
				cmd.Parameters.Add(new OleDbParameter("Role", m_Role));
				cmd.ExecuteNonQuery();

				cmd.CommandText = @"delete from usergroups where userid=" + m_Id.ToString();
				cmd.ExecuteNonQuery();


				if(m_Groups != null)
				{

					cmd.CommandText =   @"insert into usergroups (UserId, GroupId) values (?,?)";
					cmd.Parameters.Clear();
					cmd.Parameters.Add( new OleDbParameter("UserId", typeof(int)) );
					cmd.Parameters.Add( new OleDbParameter("GroupId", typeof(int)) );


					//cmd.Prepare();
					foreach(int groupId in m_Groups)
					{
						cmd.Parameters["UserId"].Value=m_Id;
						cmd.Parameters["GroupId"].Value=groupId;
						cmd.ExecuteNonQuery();
					}
				}

				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
		}

		public override void Delete()
		{
			System.Data.OleDb.OleDbConnection con = Db.Connection;
			OleDbTransaction trans = con.BeginTransaction();			
			OleDbCommand cmd = con.CreateCommand();
			cmd.Transaction = trans;
			try
			{
				cmd.CommandText =   @"delete from  usergroups where userid = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				cmd.CommandText =   @"delete from  users where id = " + m_Id.ToString();
				cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch(Exception ex)
			{
				trans.Rollback();
				throw ex;
			}


		}


		public static DataTable FindAll()
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			string commandText = "select * from Users ";
			OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, db.Connection);
			DataTable table= new DataTable();
			adapter.Fill(table);
			db.Dispose();
			return table;

		}

		public static DataTable FindInGroup(int groupId)
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			string commandText = "select * from Users inner join usergroups on id=userid where groupid=" + groupId.ToString();
			OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, db.Connection);
			DataTable table= new DataTable();
			adapter.Fill(table);
			db.Dispose();
			return table;
		}

		public static int GetPrincipalInfo(string login, out string role)
		{
			Database db = new Database();
			System.Data.OleDb.OleDbConnection con = db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"select Id, Role from  users where upper(login) = upper(?)";
			cmd.Parameters.Add(new OleDbParameter("login", login));
			OleDbDataReader reader = cmd.ExecuteReader();
			if(!reader.Read())
			{
				reader.Close();
				cmd.Dispose();
				db.Dispose();
				role = "";
				return 0;
				//throw new BipAccessDeniedException();
			}
			role = (string)reader["Role"];
			int id = Convert.ToInt32((Decimal)reader["Id"]);
			reader.Close();
			cmd.Dispose();
			db.Dispose();
			return id;
		}

		public static bool Authenticate(string login, string password)
		{
			Database db = new Database();
			System.Data.OleDb.OleDbConnection con = db.Connection;
			OleDbCommand cmd = con.CreateCommand();
			cmd.CommandText =   @"select count(1) from  users where upper(login) = upper(?) and Password=?";
			cmd.Parameters.Add(new OleDbParameter("login", login));
			cmd.Parameters.Add(new OleDbParameter("password", password));
			bool authenticated = (bool)((int) cmd.ExecuteScalar() > 0);
			cmd.Dispose();
			db.Dispose();
			return authenticated;
		}


	}
}
