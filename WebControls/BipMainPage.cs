/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Web.UI;
using System.Web;
using Bip.Components;

namespace Bip.WebControls
{
	public class BipMainPage :  BipPage
	{
		public BipMainPage()
		{
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Control header = Page.FindControl("PageHeader");
			if(header != null)
				header.Controls.Add( Page.LoadControl("~/WebControls/MainMenu.ascx"));
/*
			Control footer = Page.FindControl("PageFooter");
			if(footer != null)
				footer.Controls.Add( Page.LoadControl("~/WebControls/PageFooterCtrl.ascx"));
*/

		}

		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		protected override string GetErrorPage()
		{
			return "~/System/ErrorMainPage.aspx";
		}
	
	}
}
