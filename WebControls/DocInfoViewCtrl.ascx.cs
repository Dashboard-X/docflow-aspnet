/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

namespace Bip.WebControls
{
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
	/// <summary>
	///		Summary description for DocInfoViewCtrl.
	/// </summary>
	public partial  class DocInfoViewCtrl : System.Web.UI.UserControl
	{

		protected DocumentEnt doc;
		protected bool IsEditMode;
		public DocumentEnt Document
		{
			set{doc = value; }
		}

		public override void DataBind()
		{
				if(Request["Id"] != null)
				{
					IsEditMode = false;
					doc = new DocumentEnt();
					doc.Load(Convert.ToInt32(Request["Id"]) );

					hlDownloadFile.NavigateUrl = "~/Documents/DocFileDownload.aspx?Org=1&Id=" + doc.Id.ToString();
					if(Page.IsPostBack == false && 
						Request.UrlReferrer != null &&
						Session["MAIN_PAGE"] == null)
					{
						if(Request.UrlReferrer.AbsolutePath.ToLower().IndexOf("Documents") == -1)
							Session["MAIN_PAGE"] = Request.UrlReferrer.ToString();
					}
				}
				else
				{
					//Session.Remove("MAIN_PAGE");
					IsEditMode = true;
					if(!DocumentTransaction.IsActive())
						throw new BipFatalException();
					doc = DocumentTransaction.Current.Document;

					hlDownloadFile.NavigateUrl = "~/Documents/DocFileDownload.aspx?Org=1&TC=1";
				}

				if(doc.Id <1 )
					PanExistingDocAttrs.Visible = false;
				else
				{
					lblId.Text = doc.Id.ToString();
					lblCreationTime.Text = doc.CreationTime.ToString();
				}



				ShowParentDocLink();
				ShowPreviousVersionDocLink();

				if(doc.RefDocuments != null)
					doc.RefDocuments.GetEnumerator().Reset();
				if(doc.RefDocuments == null ||
					!doc.RefDocuments.GetEnumerator().MoveNext())
					PanRelatedDocView.Visible=false;
				else
				{
					grdRelatedDocs.DataSource = DocumentEnt.FindEnum(doc.RefDocuments);
					grdRelatedDocs.DataBind();
				}

			if(doc.Groups != null)
			{
				dlGroups.DataSource = GroupEnt.FindEnum(doc.Groups);
				dlGroups.DataBind();
			}
			else dlGroups.Visible = false;
				


				LoadAttributes();
		}

		private void ShowParentDocLink()
		{
			if(doc.ParentId < 1)
				PanParentDocView.Visible = false;
			else
			{
				ArrayList docEnum = new ArrayList();
				docEnum.Add(doc.ParentId);
				DataTable tab = DocumentEnt.FindEnum(docEnum);
				if(tab == null || tab.Rows.Count <1)
					PanParentDocView.Visible = false;
				else
				{
					PanParentDocView.Visible = true;

					string sid = DbConvert.ToString(tab.Rows[0]["Id"]);
					//PanParentDocInfo.Rows[0].Cells[0].InnerText= sid;
					
					PanParentDocInfo.Rows[0].Cells[1].InnerHtml= 
						"<a href='javascript:ViewDoc(" +  sid + ")' >"+
						HttpUtility.HtmlEncode(DbConvert.ToString(tab.Rows[0]["Header"])) + 
						"</a>";
				}
			}
		}

		private void ShowPreviousVersionDocLink()
		{
			if(doc.PreviousVersionId < 1)
				PanPreviousVersionDocView.Visible = false;
			else
			{
						
				ArrayList docEnum = new ArrayList();
				docEnum.Add(doc.PreviousVersionId);
				DataTable tab = DocumentEnt.FindEnum(docEnum);
				if(tab == null || tab.Rows.Count <1)
					PanPreviousVersionDocView.Visible = false;
				else
				{
					PanPreviousVersionDocView.Visible = true;
					string sid = DbConvert.ToString(tab.Rows[0]["Id"]);
					PanPreviousVersionDocInfo.Rows[0].Cells[1].InnerHtml= 
						"<a href='javascript:ViewDoc(" +  sid + ")' >"+
						HttpUtility.HtmlEncode(DbConvert.ToString(tab.Rows[0]["Header"])) + 
						"</a>";

				}
			}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void LoadAttributes()
		{
			string docCategoryName = "", docTypeName = "", docSourceName = "", docFileType = "";

	

			if(doc.DocCategoryId >0)
			{
				DocCategoryEnt docCategory = new DocCategoryEnt();
				try
				{
					docCategory.Load(doc.DocCategoryId);
					docCategoryName = docCategory.Name;
				}
				catch(BipObjectNotFoundException)
				{
					docCategoryName = "";
				}
				docCategory.Dispose();
			}



			if(doc.DocTypeId >0)
			{
				DocTypeEnt docType = new DocTypeEnt();
				try
				{
					docType.Load(doc.DocTypeId);
					docTypeName = docType.Name;
				}
				catch(BipObjectNotFoundException)
				{
					docTypeName = "";
				}
				docType.Dispose();
			}

			if(doc.DocSourceId >0)
			{
				DocSourceEnt docSource = new DocSourceEnt();
				try
				{
					docSource.Load(doc.DocSourceId);
					docSourceName = docSource.Name;
				}
				catch(BipObjectNotFoundException)
				{
					docSourceName = "";
				}
				docSource.Dispose();
			}
			if(doc.FileTypeId >0)
			{
				docFileType = DocFileType.GetTypeName(doc.FileTypeId);
			}



			lblDocCategory.Text =  docCategoryName;
			lblDocType.Text = docTypeName;
			lblDocSource.Text = docSourceName;
			lblFileType.Text = docFileType;
			
			if(doc.IsPublic)
				lblIsPublic.Text = "Yes";
			else	lblIsPublic.Text = "No";

			lblDateReceived.Text = Utils.DateToText(doc.DateReceived);
			lblDocumentDate.Text = Utils.DateToText(doc.DocumentDate);
			lblIncomingNumber.Text = doc.IncomingNumber;
			lblOutgoingNumber.Text = doc.OutgoingNumber;
			lblSubject.Text = doc.Subject;
			lblHeader.Text = doc.Header;
			lblFileName.Text = doc.FileName;
			lblArchiveFileNames.Text = doc.ArchiveFileNames;

			
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}
