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
using Bip.WebControls;
using Bip.Components;

namespace Bip.Documents
{
	/// <summary>
	/// Summary description for DocInfoView.
	/// </summary>
	public class DocInfoView : BipDocumentPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageHeader;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageError;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageFooter;
		protected MetaBuilders.WebControls.ConfirmedButton btnConfDelete;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PanDocEditAction;
		protected Bip.WebControls.DocInfoViewCtrl	CtrlDocInfoView;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				PanDocEditAction.Visible = m_Document.CanEdit;
				btnConfDelete.Visible = m_Document.CanEdit;
				CtrlDocInfoView.Document = m_Document;
				CtrlDocInfoView.DataBind();

			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			base.OnInit(e);
			InitializeComponent();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnConfDelete.Click += new System.EventHandler(this.btnConfDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnConfDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				m_Document.Delete();
				Response.Redirect("~/CloseWindow.html");
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		}
	}
}
