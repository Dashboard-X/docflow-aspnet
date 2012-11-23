/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Bip.Components
{
	/// <summary>
	/// Summary description for Database.
	/// </summary>
	public class Database : IDisposable
	{
		OleDbConnection m_Connection = null;
		public OleDbConnection Connection
		{	get
			{
				if( m_Connection == null )
				{
					string strConn = ConfigurationSettings.AppSettings["ConnectionString"];
					m_Connection = new OleDbConnection(strConn);
					m_Connection.Open();
				}
				return m_Connection;
			}
		}


		public void Dispose() 
		{
			// make sure connection is closed
			if (m_Connection != null) 
			{
				m_Connection.Dispose();
				m_Connection = null;
			}				
		}
		
	
		public Database()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
