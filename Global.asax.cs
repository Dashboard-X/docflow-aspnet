using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Security.Principal;
using Bip.Components;

namespace Bip 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{


		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			if (Request.IsAuthenticated == true) 
			{			
				// Add our own custom principal to the request containing the roles in the auth ticket
				if(Context.Request.Path.ToUpper().EndsWith("ERROR.ASPX"))
					return;

				try
				{
					string role;
					int userId = UserEnt.GetPrincipalInfo(Context.User.Identity.Name.ToString(),out role); 
					if(userId > 0)
					{
						string[] roles = UserRoles.HasRoles(role);
						Context.User = new GenericPrincipal(Context.User.Identity, roles);
						Context.Items.Add("bipuser", new UserIdentity(userId, role));
						//Context.Items.Add("bipuser", new UserIdentity(1, "A"));
					}
					else
					{
						Context.User = new GenericPrincipal(Context.User.Identity, null);
						/*
						if(!Request.Path.ToUpper().EndsWith("RegisterUser.aspx"))
						{
							Response.Redirect("~/RegisterUser.aspx");
						}
						*/
					}

				}
				catch(Exception ex)
				{
					//if(Context.Request.Path.ToUpper().EndsWith("ERROR.ASPX"))
						Response.Redirect("~/Error.aspx?error=" + HttpUtility.UrlEncode(ex.Message));
				}
			}
		}
	

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}

