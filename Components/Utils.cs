/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Text;


namespace Bip.Components
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class Utils
	{
		public static string DateToText(DateTime dt)
		{
			if(dt == DateTime.MinValue)
				return "";
			return dt.ToShortDateString();
		}

		public static DateTime TextToDate(string str)
		{
			if(str == null || str.Trim().Length == 0)
				return DateTime.MinValue;
			return DateTime.Parse(str);
		}


	}

	public class EnumUtils
	{
		public static string ConvertToString(IEnumerable lst)
		{
			StringBuilder res = new StringBuilder();
			bool needComma = false;
			foreach(object obj in lst)
			{
				if(needComma)
					res.Append(",");
				res.Append(obj.ToString());
				needComma = true;
			}

			return res.ToString();
		}
	}

	public class TempFile : IDisposable
	{
		protected string m_Path = "";
		public TempFile()
		{
		}

		public string Path
		{
			get
			{ 
				if(m_Path == null || m_Path.Length==0)
					m_Path = System.IO.Path.GetTempFileName();
				return m_Path;
			}
		}

		public void Dispose() 
		{
			// make sure connection is closed
			if (m_Path.Length != 0) 
			{
				System.IO.File.Delete(m_Path);
				m_Path = "";
			}				
		}

		~TempFile()
		{
			Dispose();
		}

		
	}

	class CmdParams
	{
		OleDbCommand m_Cmd;
		int m_ParamIndex = 0;
		bool m_Commas;
		public CmdParams(OleDbCommand cmd)
		{
			m_Cmd = cmd;
			m_ParamIndex = 0;
			m_Commas = true;
		}

		public CmdParams(OleDbCommand cmd, bool commas)
		{
			m_Cmd = cmd;
			m_ParamIndex = 0;
			m_Commas = commas;
		}


		public string Add(DateTime dt)
		{
			string ret;
			if(dt == DateTime.MinValue)
				ret = " null ";
			else 
			{
				ret = " ? ";
				m_Cmd.Parameters.Add(new OleDbParameter("param_" + m_ParamIndex.ToString(), dt));
			}

			if(m_Commas && m_ParamIndex >0)
				ret = " , " + ret;
			m_ParamIndex++;
			return ret;
		}

		public string Add(int id)
		{
			string ret;
			if(id <1)
				ret = " null ";
			else 
			{
				ret = " ? ";
				m_Cmd.Parameters.Add(new OleDbParameter("param_" + m_ParamIndex.ToString(),id));
			}

			if(m_Commas && m_ParamIndex >0)
				ret = " , " + ret;
			m_ParamIndex++;
			return ret;
		}
		
		public string Add(string str)
		{
			string ret;
			if(str == null )
				ret = " null ";
			else 
			{
				ret = " ? ";
				m_Cmd.Parameters.Add(new OleDbParameter("param_" + m_ParamIndex.ToString(),str));
			}

			if(m_Commas && m_ParamIndex >0)
				ret = " , " + ret;
			m_ParamIndex++;
			return ret;
		}

		public string Add(bool val)
		{
			string ret;
			if(val)
				ret = " 1 ";
			else 	ret = " 0 ";
			if(m_Commas && m_ParamIndex >0)
				ret = " , " + ret;
			m_ParamIndex++;
			return ret;
		}

	}



	public class DbConvert
	{
		public static int ToInt32(object val)
		{
			if(val == DBNull.Value)
				return 0;
			return Convert.ToInt32(val);
		}

		public static DateTime ToDateTime(object val)
		{
			if(val == DBNull.Value)
				return DateTime.MinValue;
			return Convert.ToDateTime(val);
		}

		public static bool ToBoolean(object val)
		{
			if(val == DBNull.Value)
				return false;
			return Convert.ToBoolean(val);
		}

		public static string ToString(object val)
		{
			if(val == DBNull.Value || val == null)
				return "";
			return val.ToString();
		}

	}
}
