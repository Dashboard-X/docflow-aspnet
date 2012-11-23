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
	/// Summary description for BipDocumentPage.
	/// </summary>
	public class BipDocumentPage : BipPopupPage
	{
		protected int m_DocumentId = 0;
		protected DocumentEnt m_Document = null;
		public BipDocumentPage()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private void Page_Load(object sender, System.EventArgs e)
		{

			if(Request["Id"] == null && ViewState["Id"] == null)
			{
				ProcessException(new BipFatalException());
				return;
			}

			try
			{
				if(Request["Id"] != null)
					m_DocumentId = Convert.ToInt32(Request["Id"]);
				else	m_DocumentId = Convert.ToInt32(ViewState["Id"]);

				if(!Page.IsPostBack)
					ViewState["Id"] = m_DocumentId.ToString();

				m_Document = new DocumentEnt();
				m_Document.Load(m_DocumentId);

				Control header = Page.FindControl("PageHeader");
				if(header != null)
				{
					Bip.WebControls.DocumentMenuCtrl docMenu = (Bip.WebControls.DocumentMenuCtrl)Page.LoadControl("~/WebControls/DocumentMenuCtrl.ascx");
					docMenu.DocumentId = m_DocumentId;
					header.Controls.Add(docMenu );
					docMenu.DataBind();
				
					string strJSTitleValue = m_Document.Header.Replace("\\", "\\\\").Replace("\"", "\\\"");
					string strSetTitleCtrl = "<script language=javascript>	window.document.title=\"" + strJSTitleValue + 
						"\";   if(window.parent != null)  window.parent.document.title = \"" +strJSTitleValue + 
						"\"; </script>";
					header.Controls.Add( new LiteralControl(strSetTitleCtrl));
				}
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}

		}

		private void Page_Unload(object sender, System.EventArgs e)
		{
			if(m_Document != null)
				m_Document.Dispose();


		}

		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Unload += new System.EventHandler(this.Page_Unload);
		}

	}
}
