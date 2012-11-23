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
	/// Summary description for DocCommitChanges.
	/// </summary>
	public partial class DocCommitChanges : Bip.WebControls.BipPage
	{
		protected MetaBuilders.WebControls.ConfirmedButton btnConfDelete;
		protected Bip.WebControls.DocInfoViewCtrl	CtrlDocInfoView;


		DocumentEnt doc = null;
	


		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{

				if(Request["btnCancel"] != null)
				{
					DocumentTransaction.End();
					Response.Redirect("~/CloseWindow.html");		
					return;
				}

			if(!DocumentTransaction.IsActive())
				throw new BipFatalException();
				doc = DocumentTransaction.Current.Document;
                if(doc.Id < 1)
					btnUpdate.Visible = false;
				else	btnCreate.Visible = false;
				CtrlDocInfoView.Document = doc;
				DataBind();


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

		}
		#endregion

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			DocumentTransaction.End();
			Response.Redirect("~/CloseWindow.html");
		}

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("DocumentEdit.aspx");
		}

		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int id = doc.Create();
				DocumentTransaction.End();
				Response.Redirect("~/CloseWindow.html");
			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
		}

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				doc.Update();
				int id = doc.Id;
				DocumentTransaction.End();
				Response.Redirect("~/CloseWindow.html");		
			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
		}


		private void btnConfDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				doc.Delete();
				DocumentTransaction.End();
				Response.Redirect("~/CloseWindow.html");
			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
		}

	}
}
