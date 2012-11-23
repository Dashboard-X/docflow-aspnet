/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Collections;
using System.Data;
using System.Web;


namespace Bip.Components
{
	/// <summary>
	/// Summary description for BipResources.
	/// </summary>
	public class BipResources
	{
		protected static Hashtable m_StringTable = null;
		protected static void LoadStringTable()
		{
			if(m_StringTable != null)
				return;
			DataSet dsStringTable = new DataSet("StringTable");
			System.IO.StreamReader myStreamReader = 
				new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/StringTable.xml"));
			dsStringTable.ReadXml(myStreamReader);
			
			DataTable tab = dsStringTable.Tables[0];
			m_StringTable = new Hashtable(tab.Rows.Count);
			
			foreach(DataRow row in tab.Rows)
				m_StringTable.Add(row[0], row[1]);
			
			myStreamReader.Close();
			tab.Dispose();
			dsStringTable.Dispose();
		}

		public static string GetString(string id)
		{
			if(m_StringTable == null)
				LoadStringTable();
			return (string)m_StringTable[id];
		}
	}
}
