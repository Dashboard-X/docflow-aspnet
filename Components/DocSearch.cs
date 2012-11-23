/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Bip.Components
{
	public class DocSearch : IDisposable
	{

		Database Db = new Database();
		DataTable m_ResultSet = null;
		int m_VirtualCount = 0;
		bool m_HasMoreResults = false;
		bool m_HasResults = false;
		
		public int VirtualCount
		{
			get {	return m_VirtualCount;	}
		}
		
		public DataTable ResultSet
		{
			get	{	return m_ResultSet;	}
		}
		
		public bool HasMoreResults
		{
			get{ return m_HasMoreResults; }
		}

		public bool HasResults
		{
			get{ return m_HasResults; }
		}

		public DocSearch()
		{
		}

		public bool Find(string attrFilter, string contextFilter, string attrOrderBy, int startRec, int numberOfRecs)
		{
			m_HasResults = false;
			m_HasMoreResults = false;
			m_VirtualCount = 0;

			if(numberOfRecs == 0)
				numberOfRecs = 50;
 
			string docQuery = @"select Id from Documents as D ";
			if(contextFilter != null && contextFilter.Length != 0)
			{
				docQuery += @" inner join OPENQUERY(ACCUFLOW_INDEXINGSRV,'select FileName from scope() where " + contextFilter + " ') as I "+
					        @" on D.StorageFileName = I.FileName ";
			}

			

			docQuery += @" where  1=1 ";
			if(attrFilter != null &&  attrFilter.Length != 0)
			{
				docQuery += @" and "	 + attrFilter;
				// SECURITY FILTER WILL BE POSTED HERE
			}
			
			string securityFilter = GetSecurityFilter();
			if(securityFilter.Length >0)
			docQuery += " and " + securityFilter;

			if(attrOrderBy != null &&  attrOrderBy.Length != 0)
				docQuery += @" order by D." + attrOrderBy;

			OleDbCommand cmd = Db.Connection.CreateCommand();
			cmd.CommandText = docQuery;
			OleDbDataReader reader = cmd.ExecuteReader();

			int rec =0;
			while(rec <= startRec)
			{
				if(!reader.Read())
				{
					m_VirtualCount = rec;
					m_HasMoreResults = false;
					reader.Close();
					cmd.Dispose();
					return false;
				}
				rec++;
			}

			System.Text.StringBuilder resIdSet = new System.Text.StringBuilder(1000);
			
			m_HasMoreResults = true;
			do
			{
				if(resIdSet.Length != 0)
					resIdSet.Append(" , ");

				resIdSet.Append(Convert.ToInt32(reader.GetDecimal(0)).ToString());

				if(!reader.Read())
				{
					m_HasMoreResults = false;
					m_VirtualCount = rec;
					break;
				}
				rec++;

			}while(rec <= startRec + numberOfRecs);

			if(m_HasMoreResults)
			{
				int readAhead = startRec + numberOfRecs * 6;
				for(;rec < readAhead && reader.Read(); rec++);
				m_VirtualCount = rec;
				m_HasMoreResults = (rec == readAhead);
			}

			reader.Close();
			cmd.Dispose();

			string docSelect = @"select *, 
						IsRead= case when exists (select top 1 1 from userReadDocs where UserId="
						+ UserIdentity.Current.UserId.ToString() + 
						@" and DocId=D.Id) then 1 else 0 end 
						 from documents as D where id in (" + resIdSet + ")";
			if(attrOrderBy != null && attrOrderBy.Length != 0)
					docSelect += " order by " + attrOrderBy + " , IsRead ";
			else	docSelect += " order by  IsRead ";



			OleDbDataAdapter resFiller = new OleDbDataAdapter(docSelect, Db.Connection);

			if(m_ResultSet == null)
				m_ResultSet = new DataTable();
			resFiller.Fill(m_ResultSet);

			m_HasResults = true;
			return true;

		}

		public string GetSecurityFilter()
		{
			UserIdentity user = UserIdentity.Current;
			if(user.UserRole == UserRoles.Administrator || 
				user.UserRole == UserRoles.SystemOperator)
				return "";

			string canRead = "(D.IsPublic=1 or D.OwnerUserId="+ user.UserId.ToString()+
				@"  or exists 
					(select top 1 1 from 
					UserGroups inner join DocGroups on
					UserGroups.GroupId = DocGroups.GroupId
					where DocGroups.DocId = D.id 
					and UserGroups.UserId = " + user.UserId.ToString() + " )) ";
			return canRead;
		}
	
		public void Dispose() 
		{
			// make sure connection is closed
			if (Db != null) 
			{
				Db.Dispose();
				Db = null;
			}				
		}

		public static string GetDateDiffSqlExpression(string field, DateTime date)
		{
			if(date == DateTime.MinValue)
				return "";
			return " datediff( day,  " + field + ", {d '" + date.ToString("yyyy-MM-dd") +  "' }) ";

		}


		public static string FilterDateRange(string field, DateTime fromDate, DateTime toDate)
		{
			// fixed
			if(fromDate == DateTime.MinValue &&
				toDate == DateTime.MinValue)
				return "";

			if(	toDate != DateTime.MinValue && fromDate > toDate)
				throw new  BipGenericException(BipResources.GetString("StrInvalidDateRange")); 

			string filter = "";
			if(fromDate != DateTime.MinValue)
			{
				filter = GetDateDiffSqlExpression(field, fromDate) + " <= 0 ";
				if(toDate != DateTime.MinValue)
					filter += " AND ";
			}

			if(toDate != DateTime.MinValue)
				filter +=  GetDateDiffSqlExpression(field, toDate) + " >= 0 ";

			return " ( " + filter.ToString() + " ) ";
		}

        public static string FilterEnumeration(string field, IEnumerator items)
		{ 
			if(items == null)
				return "";

			StringBuilder listItems = new StringBuilder();
			items.Reset();
			while(items.MoveNext())
			{
				if(listItems.Length > 0)
					listItems.Append(" , ");
				listItems.Append(items.Current.ToString());
			}
			
			if(listItems.Length == 0)
				return "";

			return " ( " + field + " IN ( " + listItems.ToString() + " )) ";
		}


		public static string FilterStringEquals(string field, string source)
		{
			if(source == null || source.Length == 0)
				return "";
			return " ( " + field + " = '" + source + "') ";
		}

		public static string FilterString(string field, string source)
		{
			if(source == null || source.Trim().Length ==0)
				return "";
			StringBuilder filter = new StringBuilder();
			StringTokenizer srcItems = new StringTokenizer(source.Trim(), ' ', '"');
			if(srcItems.Count == 0)
				return "";
			foreach(string item in srcItems)
			{
				string pattern = item.Replace("'", "''").Replace("%", "#%");
				
				if(filter.Length > 0)
					filter.Append(" AND ");
				filter.Append(" ( " + field + " LIKE '%" + pattern + "%' ESCAPE '#') ");
			}
			return filter.ToString();
		}

		public static string FilterContext(string source)
		{	// indexing service 
			//ms-help://MS.VSCC/MS.MSDNVS/indexsrv/ixrefqls_4qud.htm
			if(source == null || source.Trim().Length == 0)
				return "";
			StringBuilder filter = new StringBuilder();
			StringTokenizer srcItems = new StringTokenizer(source.Trim(), ' ', '"');
			if(srcItems.Count == 0)
				return "";
			foreach(string item in srcItems)
			{
				string pattern = item.Replace("'", "''");
				if(filter.Length > 0)
					filter.Append(" AND ");
				filter.Append(" \""  + pattern + "\" ");
			}
			return " CONTAINS(''" +  filter.ToString() + "'') ";
		}


		public static string FilterIsRead(bool isRead)
		{
			string ret = 
				@" exists 
				(select top 1 1 from userReadDocs
					where UserId=" + UserIdentity.Current.UserId.ToString()
				 + " and DocId=D.Id) ";
			if(!isRead)
				ret = " not " + ret;

			return ret;
		}

		public static string FilterIsFavorite(bool isFavorite)
		{
			string ret = 
				@" exists 
				(select top 1 1 from userFavoriteDocs
					where UserId=" + UserIdentity.Current.UserId.ToString()
				+ " and DocId=D.Id) ";
			if(!isFavorite)
				ret = " not " + ret;
			return ret;
		}

	}
}
