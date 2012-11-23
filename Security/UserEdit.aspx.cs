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
	/// Summary description for UserEdit.
	/// </summary>
	public class UserEdit : Bip.WebControls.BipMainPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageHeader;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageError;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PanCreateNew;
		protected System.Web.UI.WebControls.DataList dlGroups;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox txtLogin;
		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.DropDownList ddlRoles;
		protected System.Web.UI.WebControls.Button btnCreate;
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label lblGroupId;
		protected System.Web.UI.WebControls.RegularExpressionValidator valEmail;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageFooter;
		protected MetaBuilders.WebControls.ConfirmedButton btnConfDelete;
		protected System.Web.UI.WebControls.RequiredFieldValidator valReqFistName;
		protected System.Web.UI.WebControls.RequiredFieldValidator valReqLastName;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtConfirmPassword;
		protected System.Web.UI.WebControls.RequiredFieldValidator valReqPassword;
		protected System.Web.UI.WebControls.RequiredFieldValidator valReqConfirmPassword;
		protected System.Web.UI.WebControls.CompareValidator valCmpPasswords;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PanEditExisting;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{

				if(Page.IsPostBack==false)
				{
					Bip.Components.UserEnt entity = new Bip.Components.UserEnt();
					if (Request.Params["Id"] != null) 
					{
						int id = Int32.Parse(Request.Params["Id"]);
						entity.Load(id);
						ViewState["id"]=id;
						PanCreateNew.Visible = false;
						valReqPassword.Enabled = false;
						valReqConfirmPassword.Enabled = false;
					}
					else
					{
						PanEditExisting.Visible=false;
						entity.New();
						valReqPassword.Enabled = true;
						valReqConfirmPassword.Enabled = true;
					}

					txtLogin.Text = entity.Login;
					txtFirstName.Text= entity.FirstName;
					txtLastName.Text = entity.LastName;
					txtEmail.Text = entity.Email;
					ddlRoles.DataSource = Bip.Components.UserRoles.GetRoleTable();
					ddlRoles.DataValueField = "Id";
					ddlRoles.DataTextField = "Name";
					ddlRoles.DataBind();
					foreach(ListItem itm in ddlRoles.Items)
						if(itm.Value==entity.Role)
								itm.Selected = true;
						else	itm.Selected = false;


					dlGroups.DataSource = Bip.Components.GroupEnt.FindAll();
					//dlGroups.DataValueField = "Id";
					//dlGroups.DataTextField = "Name";
					dlGroups.DataBind();
					foreach(DataListItem itm in dlGroups.Items)
					{
						HtmlInputControl hfGroupId = (HtmlInputControl) itm.FindControl("hfGroupId");
						CheckBox cbSel = (CheckBox)itm.FindControl("cbGroupSel");

						string groupId = hfGroupId.Value;
						cbSel.Checked = false;
						foreach(int userGroupId in entity.Groups)
							if(userGroupId.ToString() == groupId)
								cbSel.Checked = true;
					}



					entity.Dispose();
					if(Request.UrlReferrer != null)
							ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
					else	ViewState["UrlReferrer"] = "~/Security/UserList.aspx";
				}
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}

		}

		private ArrayList GetUserGroups()
		{
			ArrayList groups = new ArrayList();
			foreach(DataListItem itm in dlGroups.Items)
			{
				HtmlInputControl hfGroupId = (HtmlInputControl) itm.FindControl("hfGroupId");
				CheckBox cbSel = (CheckBox)itm.FindControl("cbGroupSel");
				if(cbSel.Checked)
					groups.Add(Convert.ToInt32(hfGroupId.Value));
			}
			return groups;
		}
		
		
		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			try
			{

				Bip.Components.UserEnt entity = new Bip.Components.UserEnt();	
				entity.New();
				entity.Login = txtLogin.Text;
				entity.Password = txtPassword.Text;
				entity.FirstName = txtFirstName.Text;
				entity.LastName = txtLastName.Text;
				entity.Email = txtEmail.Text;
				entity.Role = ddlRoles.SelectedItem.Value;
				entity.Groups = GetUserGroups();

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

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Bip.Components.UserEnt entity = new Bip.Components.UserEnt();	
				entity.Load( (int)ViewState["id"] );
				entity.Login = txtLogin.Text;
				if(txtPassword.Text.Length > 0)
					entity.Password = txtPassword.Text;
				entity.FirstName = txtFirstName.Text;
				entity.LastName = txtLastName.Text;
				entity.Email = txtEmail.Text;
				entity.Role = ddlRoles.SelectedItem.Value;
				entity.Groups = GetUserGroups();
				entity.Update();
				entity.Dispose();
				Response.Redirect((String) ViewState["UrlReferrer"]);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		}


		private void btnCancel_Click(object sender, System.EventArgs e)
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
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.btnConfDelete.Click += new System.EventHandler(this.btnConfDelete_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnConfDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				Bip.Components.UserEnt entity = new Bip.Components.UserEnt();	
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
	}
}
