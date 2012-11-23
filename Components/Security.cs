/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace Bip.Components
{
	public class UserRoles
	{
		public static string Administrator = "A";
		public static string SystemOperator = "S";
		public static string Operator = "O";
		public static string Reader = "R";

		protected static string[] m_UserRoles = {Administrator, SystemOperator, Operator, Reader};
		protected static string[] m_UserRoleNames = {"StrRoleNameAdministrator", 
													 "StrRoleNameSystemOperator", 
													 "StrRoleNameOperator",
													 "StrRoleNameReader"};

		public static DataTable GetRoleTable()
		{
			DataTable roles = new DataTable();
			roles.Columns.Add("Id");
			roles.Columns.Add("Name");
			for(int i=0; i< m_UserRoles.Length; i++)
				roles.Rows.Add( new string[]{m_UserRoles[i], BipResources.GetString(m_UserRoleNames[i])});
			return roles;
		}

		public static string GetRoleName(string role)
		{
			for(int i=0; i< m_UserRoles.Length; i++)
				if(m_UserRoles[i] == role)
					return BipResources.GetString(m_UserRoleNames[i]);
			return BipResources.GetString(m_UserRoleNames[m_UserRoleNames.Length-1]);
		}

		public static bool HasRole(string role1, string role2)
		{
			int iRole1=0, iRole2=0;
			for(; iRole1< m_UserRoles.Length; iRole1++)
				if(m_UserRoles[iRole1] == role1)
					break;
			for(; iRole2< m_UserRoles.Length; iRole2++)
				if(m_UserRoles[iRole2] == role2)
					break;

			return iRole1 <= iRole2;
		}

		public static string[] HasRoles(string role)
		{
			int iRole=0;
			for(; iRole< m_UserRoles.Length; iRole++)
				if(m_UserRoles[iRole] == role)
					break;
			
			if(iRole == m_UserRoles.Length)
				throw new BipAccessDeniedException();

			string[] allRoles = new string[m_UserRoles.Length - iRole];

			int cur = iRole;
			for(; iRole <m_UserRoles.Length; iRole++)
				allRoles[iRole - cur] = m_UserRoles[iRole];

			return allRoles;
		}

		public static bool UserHasRole(string role)
		{
			return HasRole(UserIdentity.Current.UserRole, role);
		}

		public static void PermitRole(string role)
		{
			if(!HasRole(UserIdentity.Current.UserRole, role))
				throw new BipAccessDeniedException();
		}

	}


	/// <summary>
	/// Summary description for Security.
	/// </summary>
	public class UserIdentity
	{
		int m_UserId;
		string m_UserRole;

		public int UserId
		{
			get{return m_UserId;}
		}
		public string UserRole
		{
			get{return m_UserRole;}
		}
		
		public static void Reset()
		{
			System.Web.HttpContext.Current.Items.Remove("bipuser");
		}

		public static UserIdentity Current
		{
			get
			{
				object obj = System.Web.HttpContext.Current.Items["bipuser"];
				if(obj== null)
					return null;
				return (UserIdentity) obj;
			}
		}

		public UserIdentity(int userId, string userRole)
		{
			m_UserId = userId;
			m_UserRole = userRole;
		}
	}
/*	
	class BipUserAuthorization
	{
		public static void PermitRole(string role)
		{
			if(!BipUserRoles.HasRole(BipUserIdentity.Current.UserRole, role))
				throw new BipAccessDeniedException();
		}

		public static void PermitCurrentUser(int userId)
		{
			if(BipUserIdentity.Current.UserId != userId)
				throw new BipAccessDeniedException();
		}

	}
*/
	public class GroupEnt : SimpleDictionary
	{
		protected static string  DbTableName = "Groups";
		protected override string GetDbTableName()
		{
			return DbTableName;
		}
		public static DataTable FindAll()
		{
			return FindAllEntries(DbTableName);
		}

		public static DataTable FindEnum(IEnumerable ids)
		{
			return FindEnumEntries(DbTableName, ids);
		}

		public static DataTable FindUsers(int groupId)
		{
			Database db = new Database();
			OleDbCommand cmd = db.Connection.CreateCommand();
			string commandText = @"select * from users inner join usergroups on id=UserId 
								where GroupId=" + groupId.ToString() + " order by login";
			OleDbDataAdapter adapter = new OleDbDataAdapter(commandText, db.Connection);
			DataTable table= new DataTable();
			adapter.Fill(table);
			db.Dispose();
			return table;		
		}

		protected static string GetEntityName(int id)
		{
			return GetEntityName(DbTableName, id);
		}

	}
}
