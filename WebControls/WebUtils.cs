/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bip.Components;

namespace Bip.WebControls
{
	/// <summary>
	/// Summary description for WebUtils.
	/// </summary>
	public class WebUtils
	{
		public WebUtils()
		{
			
		
			//
			// TODO: Add constructor logic here
			//
			
		}
	}

	class ListUtils
	{
		public static void SelectListItem(ListItemCollection items, string val)
		{
			foreach(ListItem itm in items)
				if(itm.Value == val)
					itm.Selected = true;
		}

		public static void SelectSingleListItem(ListItemCollection items, string val)
		{
			foreach(ListItem itm in items)
				if(itm.Value == val)
						itm.Selected = true;
				else	itm.Selected = false;
		}

	}

	

}
