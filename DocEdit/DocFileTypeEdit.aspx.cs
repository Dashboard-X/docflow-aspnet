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
using Bip.WebControls;

namespace Bip.Documents
{
	/// <summary>
	/// Summary description for DocFileTypeEdit.
	/// </summary>
	public class DocFileTypeEdit : Bip.WebControls.BipPage
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageHeader;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageError;
		protected System.Web.UI.WebControls.DropDownList ddlFileType;
		protected System.Web.UI.WebControls.Button btnPreview;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageFooter;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnBack;
		protected System.Web.UI.HtmlControls.HtmlTableCell TD1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PanPreview;
		protected System.Web.UI.WebControls.Label lblFileName;
		DocumentEnt doc = null;
	
		protected override  bool AllowTransaction(string name)
		{
			return true;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				if(Request["btnCancel"] != null)
				{
					DocumentTransaction.End();
					Response.Redirect("~/CloseWindow.html");	
					return;
				}
				PanPreview.Visible = false;

				if(!DocumentTransaction.IsActive())
					throw new BipFatalException();

				doc = DocumentTransaction.Current.Document;
				lblFileName.Text=doc.FileName;
				if(Page.IsPostBack == false)
				{
					ddlFileType.DataSource = DocFileType.FindAll();
					ddlFileType.DataTextField = "Name";
					ddlFileType.DataValueField = "Id";
					ddlFileType.DataBind();
					//ddlFileType.Items.Insert(0, new ListItem("", "0"));
					ListUtils.SelectSingleListItem(ddlFileType.Items, doc.FileTypeId.ToString());

				}
				else
				{
					int fileType = Convert.ToInt32(ddlFileType.SelectedItem.Value);
					if(fileType == 0)
						throw new BipGenericException(Bip.Components.BipResources.GetString("StMustSelectFileType"));
					doc.ConfigureFileType( Convert.ToInt32(ddlFileType.SelectedItem.Value));
				}

				if(doc.Id > 0)
					btnBack.Visible = false;
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
			
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
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			PanPreview.Visible = true;
			//Response.Redirect(doc.GetFileUrl(false));
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("DocumentEdit.aspx");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DocumentTransaction.End();
			Response.Redirect("~/CloseWindow.html");
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("DocFileUpload.aspx");
		}
	}
}
