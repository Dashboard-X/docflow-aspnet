/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;

namespace Bip.Components
{
	public class BipException : System.Exception
	{
		protected bool m_IsFatal = false;

		public bool IsFatal
		{
			get{ return m_IsFatal; }
		}

		public BipException()
		{
		}

		public BipException(string message)
			:base(message)
		{
			
		}

		public BipException(string message, bool isFatal)
			:base(message)
		{
			
			m_IsFatal = isFatal;
		}
	}
	
	public class BipGenericException : BipException
	{
		public BipGenericException(string message)
			: base(message, false)
		{
			
		}

	}

	public class BipFatalException : BipException
	{
		public BipFatalException()
			 : base("", true)
		{
			
		}

		public BipFatalException(string message)
			: base(message, true)
		{
		}
	}

	public class BipObjectNotFoundException : BipFatalException
	{
		public BipObjectNotFoundException()
			: base(BipResources.GetString("StrObjectNotFoundException"))
		{
		}
	}

	public class BipAccessDeniedException  : BipFatalException
	{
		public BipAccessDeniedException()
			: base(BipResources.GetString("StrAccessDeniedException"))
		{
		}
	}


}
