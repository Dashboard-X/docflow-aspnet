/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Web;
using System.Web.UI;


namespace Bip.Components
{
	/// <summary>
	/// Summary description for TransactionContext.
	/// </summary>
	/// 
/*
	public class TransactionContext	: IDisposable
	{
		protected string m_Name;
		public string Name
		{
			get{ return m_Name; }
		}

		public BipEntity Entity;

		public TransactionContext(string name)
		{
			m_Name=name;
		}

		public static TransactionContext Current
		{
			get
			{
				object tc = HttpContext.Current.Session["TC"];
				if(tc==null)
					return null;
				return (TransactionContext) tc;
			}
		}

		public static void Set(TransactionContext tc)
		{
			if(Current != null)
				Close();

			HttpContext.Current.Session["TC"] = tc;
		}

		public static void Close()
		{
			object tc = HttpContext.Current.Session["TC"];
			if(tc!=null)
				((IDisposable)tc).Dispose();
			HttpContext.Current.Session.Remove("TC");
		}


		public void Dispose() 
		{
			// make sure connection is closed
			if (Entity != null) 
			{
				Entity.Dispose();
				Entity = null;
			}				
		}
		
		~TransactionContext()
		{
			 Dispose();
		}
	}
*/
	public class DocumentTransaction 	: IDisposable
	{
		protected DocumentEnt m_Document = null;

		public DocumentEnt Document
		{
			get{ return m_Document; }
		}

		DocumentTransaction(DocumentEnt doc)
		{
			m_Document = doc;
		}

		public void Dispose() 
		{
			// make sure connection is closed
			if (m_Document != null) 
			{
				m_Document.Dispose();
				m_Document = null;
			}				
		}


		public static bool IsActive()
		{
			object obj = HttpContext.Current.Session["DocumentTransaction"];
			return (obj != null);
		}
		public static void Begin(DocumentEnt doc)
		{
			if(IsActive())
				End();
			HttpContext.Current.Session["DocumentTransaction"] = new DocumentTransaction(doc);
		}

		public static void End()
		{
			object obj = HttpContext.Current.Session["DocumentTransaction"];
			if(obj != null)
			{
				DocumentTransaction dtrans = (DocumentTransaction) obj;
				dtrans.Dispose();
				HttpContext.Current.Session.Remove("DocumentTransaction");
			}
		}
		
		
		public static DocumentTransaction Current
		{
			get
			{
				object obj = HttpContext.Current.Session["DocumentTransaction"];
				DocumentTransaction dtrans = (DocumentTransaction) obj;
				return dtrans;
			}
		}



	}
}
