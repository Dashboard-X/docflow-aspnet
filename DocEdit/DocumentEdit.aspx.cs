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
	/// Summary description for DocumentEdit.
	/// </summary>
	public partial class DocumentEdit : Bip.WebControls.BipPage
	{
	
		DocumentEnt doc = null;
	
		protected override  bool AllowTransaction(string name)
		{
			return true;
		}

		protected void LoadDropDownList(DropDownList ddl, DataTable tab)
		{
			ddl.DataSource = tab;
			ddl.DataTextField = "Name";
			ddl.DataValueField = "Id";
			ddl.DataBind();
			ddl.Items.Insert(0, new ListItem("", "0"));
		}

		private void ShowParentDocLink(DocumentEnt doc)
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
					//PanParentDocView.Rows[0].Cells[1].InnerText= DbConvert.ToString(tab.Rows[0]["Id"]);
					PanParentDocView.Rows[0].Cells[2].InnerText= DbConvert.ToString(tab.Rows[0]["Header"]);
				}
			}
		}

		private void ShowPreviousVersionDocLink(DocumentEnt doc)
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
					//PanPreviousVersionDocView.Rows[0].Cells[1].InnerText= DbConvert.ToString(tab.Rows[0]["Id"]);
					PanPreviousVersionDocView.Rows[0].Cells[2].InnerText= DbConvert.ToString(tab.Rows[0]["Header"]);
				}
			}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				if(!DocumentTransaction.IsActive())
					throw new BipFatalException();
				
				doc = DocumentTransaction.Current.Document;

				if(Request["btnCancel"] != null)
				{
					DocumentTransaction.End();
					Response.Redirect("~/CloseWindow.html");		
					return;
				}
			
			
				if(Page.IsPostBack == false)
				{
					if(doc.Id<1)
					{
						PanExistingDocAttrs.Visible = false;
						btnBack.Text = "< " + BipResources.GetString("StrBtnBackCaption");
						btnBack.Width = new Unit("100px");
					}

					LoadDropDownList(ddlDocCategory, DocCategoryEnt.FindAll());
					LoadDropDownList(ddlDocType, DocTypeEnt.FindAll());
					LoadDropDownList(ddlDocSource, DocSourceEnt.FindAll());

					dlGroups.DataSource = Bip.Components.GroupEnt.FindAll();
					dlGroups.DataBind();


					LoadAttributes();
				}

				// ParentDocID ---------------------------------
				if(Page.IsPostBack ==false || 
					Request["_BIP_ACTION"] == "SelectParentDoc")
				{
					if(Request["_BIP_ACTION"]  == "SelectParentDoc")
						doc.ParentId = Convert.ToInt32(Request["_BIP_ACTION_ARGS"]);
					ShowParentDocLink(doc);
				}

				// PreviousVersionDocID ---------------------------------
				if(Page.IsPostBack ==false || 
					Request["_BIP_ACTION"] == "SelectPreviousVersionDoc")
				{
					if(Request["_BIP_ACTION"]  == "SelectPreviousVersionDoc")
						doc.PreviousVersionId = Convert.ToInt32(Request["_BIP_ACTION_ARGS"]);
					ShowPreviousVersionDocLink(doc);
				}

				// RelatedDocs ---------------------------------
				if(Page.IsPostBack ==false || 
					Request["_BIP_ACTION"] == "AddRelatedDoc" ||
					Request["_BIP_ACTION"] == "RemoveRelatedDoc")
				{
					if(Request["_BIP_ACTION"] == "AddRelatedDoc" ||
						Request["_BIP_ACTION"] == "RemoveRelatedDoc")
					{
						ArrayList docEnum = new ArrayList();
						if(doc.RefDocuments != null)
						{
							foreach(int ids in doc.RefDocuments)
								docEnum.Add(ids);
						}

						int id = Convert.ToInt32(Request["_BIP_ACTION_ARGS"]);
						if(Request["_BIP_ACTION"] == "AddRelatedDoc")
						{
							if(docEnum.IndexOf(id) == -1)
								docEnum.Add(id);
						}
						else docEnum.Remove(id);

						doc.RefDocuments = docEnum;
					}

					grdDocRefRelated.DataSource = DocumentEnt.FindEnum(doc.RefDocuments);
					grdDocRefRelated.DataBind();
				}

				string s1, s2;
				bool showInBrowser = false;
				DocFileType.GetTypeInfo(doc.FileTypeId, out s1, out s2,  out showInBrowser);
				btnShowHidePreview.Visible = showInBrowser;


			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
			
		}

		private ArrayList GetDocGroups()
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

		protected void LoadAttributes()
		{

			if(doc.Id >0)
			{
				lblId.Text = doc.Id.ToString();
				lblCreationTime.Text = doc.CreationTime.ToString();
			}


			foreach(DataListItem itm in dlGroups.Items)
			{
				HtmlInputControl hfGroupId = (HtmlInputControl) itm.FindControl("hfGroupId");
				CheckBox cbSel = (CheckBox)itm.FindControl("cbGroupSel");

				string groupId = hfGroupId.Value;
				cbSel.Checked = false;
				foreach(int docGroupId in doc.Groups)
					if(docGroupId.ToString() == groupId)
						cbSel.Checked = true;
			}



			ListUtils.SelectSingleListItem(ddlDocCategory.Items, doc.DocCategoryId.ToString());
			ListUtils.SelectSingleListItem(ddlDocType.Items, doc.DocTypeId.ToString());
			ListUtils.SelectSingleListItem(ddlDocSource.Items, doc.DocSourceId.ToString());
			ListUtils.SelectSingleListItem(ddlIsPublic.Items, doc.IsPublic ? "1" : "0");

			txtDateReceived.Text = Utils.DateToText(doc.DateReceived);
			txtDocumentDate.Text = Utils.DateToText(doc.DocumentDate);
			txtIncomingNumber.Text = doc.IncomingNumber;
			txtOutgoingNumber.Text = doc.OutgoingNumber;
			txtSubject.Text = doc.Subject;
			txtHeader.Text = doc.Header;
			txtFileName.Text = doc.FileName;
			txtArchiveFileNames.Text = doc.ArchiveFileNames;
		}

		protected void StoreAttributes()
		{
			doc.DocCategoryId = Convert.ToInt32(ddlDocCategory.SelectedItem.Value);
			doc.DocTypeId = Convert.ToInt32(ddlDocType.SelectedItem.Value);
			doc.DocSourceId = Convert.ToInt32(ddlDocSource.SelectedItem.Value);
			doc.IsPublic = (bool)(Convert.ToInt32(ddlIsPublic.SelectedItem.Value) != 0);

			doc.DateReceived = Utils.TextToDate(txtDateReceived.Text);
			doc.DocumentDate = Utils.TextToDate(txtDocumentDate.Text);
			doc.IncomingNumber = txtIncomingNumber.Text;
			doc.OutgoingNumber = txtOutgoingNumber.Text;
			doc.Subject = txtSubject.Text;
			doc.Header = txtHeader.Text.Replace("\n", " ").Replace("\r", " ");
			doc.FileName =  txtFileName.Text;
			doc.ArchiveFileNames = txtArchiveFileNames.Text;
			doc.Groups = GetDocGroups();

			doc.Validate();
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

		protected void btnNext_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!Page.IsValid)
					return;
				StoreAttributes();
				Response.Redirect("DocCommitChanges.aspx");
			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			DocumentTransaction.End();
			Response.Redirect("~/CloseWindow.html");
		}

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			try
			{
				valHeader.Enabled = false;
				Page.Validate();
				valHeader.Enabled = true;

				if(!Page.IsValid)
					return;
				StoreAttributes();
				Response.Redirect("DocFileTypeEdit.aspx");
			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
		}

		protected void btnShowHidePreview_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Page.Validators[1].Validate()
				//	Page.v
				valHeader.Enabled = false;
				Page.Validate();
				valHeader.Enabled = true;
				
				if(!Page.IsValid)
					return;
				StoreAttributes();
				Response.Redirect("ShowHideDocEditFrame.html");
			}
			catch(Exception ex)
			{
				this.ProcessException(ex);
			}
		}
	}
}
