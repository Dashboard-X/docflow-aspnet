/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Collections;

namespace Bip.Components
{
	public class StringTokenizer : IEnumerable
	{
		protected ArrayList m_Items;

		public IEnumerator GetEnumerator()
		{
			return m_Items.GetEnumerator();
		}

		public int Count
		{
			get
			{
				return m_Items.Count;
			}
		}

		public string this[int index]
		{
			get
			{
				return (string) m_Items[index];
			}
		}

		public StringTokenizer(string src, char delimeter, char quotationMark)
		{
			m_Items = new ArrayList();
			if(src == null || src.Length == 0)
				return;

			int i = 0, 
				iStart = -1, 
				iEnd = -1;
			bool isQuoted;
			int len = src.Length;

			while(i < len)
			{
				while(i<len && (src[i] == delimeter ))
					i++;
				if(i==len)
					break;

				if(src[i] == quotationMark)
				{
					iStart = i+1;
					isQuoted = true;
				}
				else
				{
					iStart = i;
					isQuoted = false;
				}

				i++;

				while(	i<len &&
					src[i] != quotationMark &&
					(isQuoted || src[i] != delimeter))
					i++;

				iEnd = i - 1;
				if(iEnd >= iStart)
					m_Items.Add(src.Substring(iStart, iEnd-iStart + 1));

				i++;
			}
		}
	}
}
