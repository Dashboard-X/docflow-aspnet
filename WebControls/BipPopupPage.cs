/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;

namespace Bip.WebControls
{
	/// <summary>
	/// Summary description for BipPopupPage.
	/// </summary>
	public class BipPopupPage :  BipPage
	{
		public BipPopupPage()
		{
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		protected override string GetErrorPage()
		{
			return "~/ErrorPopupPage.aspx";
		}
	
	}
}
