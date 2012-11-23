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

namespace Bip.Documents
{
	/// <summary>
	/// Summary description for DocFileUpload.
	/// </summary>
	public partial class DocFileUpload : Bip.WebControls.BipPage
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
			
				if(Page.IsPostBack)
				{
					if(Request["btnCancel"] != null)
					{
						DocumentTransaction.End();
						Response.Redirect("~/CloseWindow.html");		
						return;
					}

					if(UploadedFile.PostedFile == null  ||
						UploadedFile.PostedFile.ContentLength ==0 ||
						UploadedFile.PostedFile.FileName == null)
						throw new BipGenericException(Bip.Components.BipResources.GetString("StrFileUploadError"));

					DocumentEnt doc = null;
					if(!DocumentTransaction.IsActive())
					{
						doc = new DocumentEnt();
						doc.New();
						DocumentTransaction.Begin(doc);
					}
					else
						doc = DocumentTransaction.Current.Document;

					doc.UploadFile(	UploadedFile.PostedFile.InputStream, System.IO.Path.GetFileName(UploadedFile.PostedFile.FileName));
					//doc.FileName = ;
					Response.Redirect("DocFileTypeEdit.aspx");
				}
			}
			catch(Exception ex)
			{
				ProcessException(ex);
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

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			DocumentTransaction.End();
			Response.Redirect("~/CloseWindow.html");
		}

		protected void btnNext_Click(object sender, System.EventArgs e)
		{
		
		}


	}
}
