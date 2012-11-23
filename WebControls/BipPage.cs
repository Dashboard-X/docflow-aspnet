/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;
using System.Web.UI;
using System.Web;
using Bip.Components;

namespace Bip.WebControls
{
	/// <summary>
	/// Summary description for BipPage.
	/// </summary>
	public class BipPage : System.Web.UI.Page
	{
		public BipPage()
		{
		}

		protected virtual bool AllowTransaction(string name)
		{
			return true;
		}
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(UserIdentity.Current == null)
			{
				Response.Redirect("~/ErrorAccessDenied.aspx");
				// Standalone ERROR page - it should not be inhereted from BipPage.
				return;
			}
				

		}

		private void Page_Error(object sender, System.EventArgs e)
		{
			// BipLog.Log(e.GetType().FullName);
			//Control header = Page.FindControl("PageHeader");
			//header.Controls.Add( Page.LoadControl("~/WebControls/MainMenu.ascx"));
		}

		override protected void OnInit(EventArgs e)
		{
			this.Load += new System.EventHandler(this.Page_Load);
			//this.Error += new System.EventHandler(this.Page_Error);
			base.OnInit(e);
		}

		public virtual void ProcessException(Exception ex)
		{

			if(ex is System.Threading.ThreadAbortException ||
				ex is System.Threading.ThreadInterruptedException)
				throw ex;

			bool isFatal = false;
			Control errorPanel = Page.FindControl("PageError");

			if(!(ex is BipException) || ((BipException)ex).IsFatal || errorPanel == null)
				isFatal = true;

			if(isFatal)
			{
				BipLog.Log(ex.Message);
				Response.Redirect(GetErrorPage() + "?error="+System.Web.HttpUtility.UrlEncode(ex.Message), true);
				return;
			}
			errorPanel.Controls.Add( new LiteralControl("<font color=Red>" + HttpUtility.HtmlEncode(ex.Message) + "</font>"));

		}

		protected virtual string GetErrorPage()
		{
			return "~/Error.aspx";
		}


	}
}
