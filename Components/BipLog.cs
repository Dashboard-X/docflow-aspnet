/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Diagnostics;

namespace Bip.Components
{
	/// <summary>
	/// Summary description for BipLog.
	/// </summary>
	public class BipLog
	{
		const string EVENT_LOG_SOURCE = "BIP";

		public BipLog()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		/// <summary>
		/// Log message to application log.
		/// </summary>
		/// <param name="message">Message to log.</param>
		public static void Log(string message) 
		{
//			throw new Exception("Log:" + message);
			/*
			EventLog m_eventLog = null;

			// make sure we have an event log
			if (!(EventLog.SourceExists(EVENT_LOG_SOURCE))) 
			{
				EventLog.CreateEventSource(EVENT_LOG_SOURCE, "Application");
			}

			if (m_eventLog == null) 
			{
				m_eventLog = new EventLog("Application");
				m_eventLog.Source = EVENT_LOG_SOURCE;
			}
		
			// log the message
			m_eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Error);
			*/
		}


	}
}
