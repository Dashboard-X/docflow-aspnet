using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Bip
{
	/// <summary>
	/// Summary description for ErrorMainPage.
	/// </summary>
	public class ErrorMainPage : Bip.WebControls.BipMainPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageHeader;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageError;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageFooter;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		private void Page_Load(object sender, System.EventArgs e)
		{
				lblErrorMessage.Text = Request["error"];
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
