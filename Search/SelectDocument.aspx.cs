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

namespace Bip.Search
{
	/// <summary>
	/// Summary description for SelectDocument.
	/// </summary>
	public partial class SelectDocument : Bip.WebControls.BipPopupPage
	{
		protected Bip.WebControls.DocSearchCtrl ctrlDocSearch;


		protected Bip.WebControls.DateRangeCtrl dtrDocumentDate;
		protected Bip.WebControls.DateRangeCtrl dtrDateReceived;
	

		protected string DocLinkUrl_Begin, DocLinkUrl_End;


		private DateTime GetDateValue(string sDate)
		{
			if(sDate == null || sDate.Trim().Length == 0)
				return DateTime.MinValue;
			return DateTime.Parse(sDate);
		}

		private IEnumerator GetEnumValue(string sEnum)
		{
			if(sEnum == null || sEnum.Trim().Length == 0)
				return null;
			ArrayList list = new ArrayList();
			string sRes = sEnum.Trim();
			int startIndex=0;
			int i= 0;
			
			while(i != -1)
			{
				i= sRes.IndexOf(',', startIndex);
				if(i != -1)
				{
					list.Add(sRes.Substring(startIndex, i - startIndex));
					startIndex = i+1;
					if(startIndex == sRes.Length)
						break;
				}
				else list.Add(sRes.Substring(startIndex));
			}
			

			return list.GetEnumerator();
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Page.IsPostBack == false)
			{


				lbxDocCategory.DataSource = Bip.Components.DocCategoryEnt.FindAll();
				lbxDocCategory.DataTextField = "Name";
				lbxDocCategory.DataValueField = "Id";

				lbxDocType.DataSource = Bip.Components.DocTypeEnt.FindAll();
				lbxDocType.DataTextField = "Name";
				lbxDocType.DataValueField = "Id";

				lbxDocSource.DataSource = Bip.Components.DocSourceEnt.FindAll();
				lbxDocSource.DataTextField = "Name";
				lbxDocSource.DataValueField = "Id";

				lbxDocCategory.DataBind();
				lbxDocType.DataBind();
				lbxDocSource.DataBind();

				lbxDocCategory.Items.Insert(0,"");
				lbxDocType.Items.Insert(0,"");
				lbxDocSource.Items.Insert(0,"");
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

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			ctrlDocSearch.Reset();
			ctrlDocSearch.Id = txtId.Text;
			ctrlDocSearch.IncomingNumber = txtIncomingNumber.Text;
			ctrlDocSearch.OutgoingNumber = txtOutgoingNumber.Text;
			ctrlDocSearch.Subject = txtSubject.Text;
			ctrlDocSearch.Header = txtHeader.Text;
			ctrlDocSearch.FileName = txtFileName.Text;
			ctrlDocSearch.ArchiveFileNames = txtArchiveFileNames.Text;
			ctrlDocSearch.ContextQuery = txtContext.Text;

			ctrlDocSearch.DateReceived_From = Utils.TextToDate( dtrDateReceived.TxtDateFrom);
			ctrlDocSearch.DateReceived_To = Utils.TextToDate(dtrDateReceived.TxtDateTo);
			ctrlDocSearch.DocumentDate_From = Utils.TextToDate(dtrDocumentDate.TxtDateFrom);
			ctrlDocSearch.DocumentDate_To = Utils.TextToDate(dtrDocumentDate.TxtDateTo);

				
			ctrlDocSearch.DocTypeId_Enum = GetEnumValue(Request["lbxDocType"]);
			ctrlDocSearch.DocSourceId_Enum = GetEnumValue(Request["lbxDocSource"]);
			ctrlDocSearch.DocCategoryId_Enum = GetEnumValue(Request["lbxDocCategory"]);
			ctrlDocSearch.OrderByAttribute = Request["OrderByAttribute"];
			ctrlDocSearch.IsRead = cbxUserRead.SelectedItem.Value;
			ctrlDocSearch.IsFavorite = cbxUserFavorite.SelectedItem.Value;


			if(Request["DocLinkUrl_Begin"] != null)
			{
				ctrlDocSearch.DocLinkUrl_Begin = Request["DocLinkUrl_Begin"];
				if(Request["DocLinkUrl_End"] != null)
						ctrlDocSearch.DocLinkUrl_End = Request["DocLinkUrl_End"];
				else	ctrlDocSearch.DocLinkUrl_End = "";
			}
				
			try
			{
				ctrlDocSearch.InitSearch();
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}

		
		}
	}
}
