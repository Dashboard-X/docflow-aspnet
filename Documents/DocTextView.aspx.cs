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
	/// Summary description for DocTextView.
	/// </summary>
	public partial class DocTextView : Bip.WebControls.BipDocumentPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{

				if(Page.IsPostBack && Request["_BIP_ACTION"] == "AddRelatedDoc" )
				{
					ArrayList docEnum = new ArrayList();
					if(m_Document.RefDocuments != null)
					{
						foreach(int ids in m_Document.RefDocuments)
							docEnum.Add(ids);
					}

					int id = Convert.ToInt32(Request["_BIP_ACTION_ARGS"]);
					if(Request["_BIP_ACTION"] == "AddRelatedDoc")
					{
						if(docEnum.IndexOf(id) == -1)
							docEnum.Add(id);
					}
					m_Document.RefDocuments = docEnum;
					m_Document.Update();
				}
			
				ddlDocLinks.Items.Clear();
				System.Collections.ArrayList docList = new System.Collections.ArrayList();
				if(m_Document.ParentId >0)
					docList.Add(m_Document.ParentId);
				if(m_Document.PreviousVersionId >0)
					docList.Add(m_Document.PreviousVersionId);
				foreach(object obj in m_Document.RefDocuments)
					docList.Add(obj);
				DataTable tab =DocumentEnt.FindEnum(docList);
				if(tab != null)
				{
					ddlDocLinks.DataSource = tab;
					ddlDocLinks.DataValueField = "Id";
					ddlDocLinks.DataTextField = "Header";
					ddlDocLinks.DataBind();
					tab.Dispose();
				
				}
				
				ddlDocLinks.Items.Insert(0, new ListItem("<" + Bip.Components.BipResources.GetString("StrRelatedDocuments") + ">", "0"));

				PanAddDocLink.Visible = m_Document.CanEdit;

			
				if(m_Document.CanEdit || tab != null)

						PanOpenRefDocuments.Visible = true;
				else	PanOpenRefDocuments.Visible = false;

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
			base.OnInit(e);
			InitializeComponent();
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
