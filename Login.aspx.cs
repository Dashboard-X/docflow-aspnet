/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

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
using Bip.Components;
using System.Web.Security;

namespace Bip
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public partial class Login : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			lblLoginFailed.Visible = false;

			if(Request["Logout"] != null &&
				Request["Logout"].Length > 0)
			{
				UserIdentity.Reset();
				FormsAuthentication.SignOut();
				return;
			}

			if(UserIdentity.Current != null)
			{
				Response.Redirect("~/ErrorAccessDenied.aspx");
				return;
			}
			// Put user code to initialize the page here
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

		}
		#endregion

		protected void btnLogin_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid == true) 
			{
				string strLogin = txtLogin.Text.Trim();
				if (UserEnt.Authenticate(strLogin, txtPassword.Text)) 
				{
					if (FormsAuthentication.GetRedirectUrl(strLogin, false).ToLower().EndsWith("Login.aspx")) 
					{
						// creates authentication ticket for user then display confirmation
						FormsAuthentication.SetAuthCookie(strLogin, cbxAutoSignin.Checked);
						Response.Redirect("~/default.aspx");
					}						
					else 
					{
						// user came here by accessing a secure page, continue
						// on to the page they were trying to access
						FormsAuthentication.RedirectFromLoginPage(strLogin, cbxAutoSignin.Checked);
					}						
				}
				else 
				{				
					lblLoginFailed.Visible = true;
				}
			}
		
		}
	}
}
