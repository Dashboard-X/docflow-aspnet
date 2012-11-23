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

namespace Bip.Security
{
	/// <summary>
	/// Summary description for GroupEdit.
	/// </summary>
	public partial class GroupEdit : Bip.WebControls.BipMainPage
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{

				if(Page.IsPostBack==false)
				{
					Bip.Components.GroupEnt entity = new Bip.Components.GroupEnt();
					if (Request.Params["Id"] != null) 
					{
						int id = Int32.Parse(Request.Params["Id"]);
						entity.Load(id);
						ViewState["id"]=id;
						PanCreateNew.Visible = false;
					}
					else
					{
						PanEditExisting.Visible=false;
						entity.New();
					}

					txtName.Text = entity.Name;
					entity.Dispose();
					if(Request.UrlReferrer != null)
							ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
					else	ViewState["UrlReferrer"] = "~/Security/GroupList.aspx";
				}
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		}

		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
			try
			{

				Bip.Components.GroupEnt entity = new Bip.Components.GroupEnt();	
				entity.New();
				entity.Name = txtName.Text;
				entity.Create();
				entity.Dispose();
				Response.Redirect((String) ViewState["UrlReferrer"],true);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
				return;
			}



		}

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Bip.Components.GroupEnt entity = new Bip.Components.GroupEnt();	
				entity.Load( (int)ViewState["id"] );
				entity.Name = txtName.Text;
				entity.Update();
				entity.Dispose();
				Response.Redirect((String) ViewState["UrlReferrer"]);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		}

		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				Bip.Components.GroupEnt entity = new Bip.Components.GroupEnt();	
				entity.Load( (int)ViewState["id"] );
				entity.Delete();
				entity.Dispose();
				Response.Redirect((String) ViewState["UrlReferrer"]);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect((String) ViewState["UrlReferrer"]);		
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
	}
}
