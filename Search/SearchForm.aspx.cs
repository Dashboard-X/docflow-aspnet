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
using System.Text;
using Bip.Components;
using Bip.WebControls;

namespace Bip.Search
{
	/// <summary>
	/// Summary description for SearchForm.
	/// </summary>
	public partial class SearchForm : Bip.WebControls.BipMainPage
	{
		protected Bip.WebControls.DateRangeCtrl dtrDocumentDate;
		protected Bip.WebControls.DateRangeCtrl dtrDateReceived;
		protected Bip.WebControls.TitleCtrl ctrlTitle;

		private void SelectListBoxItems(ListItemCollection listItems, string req)
		{
			if(req == null || req.Length == 0)
				return;
			string items_string = "," + req + ",";
			
			foreach(ListItem itm in listItems)
			{
				if(items_string.IndexOf("," + itm.Value + ",") != -1)
						itm.Selected = true;
				else	itm.Selected = false;
			}
		}
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ctrlTitle.DataBind();
			PanAddNewDoc.Visible = !Bip.Components.UserIdentity.Current.UserRole.Equals(Bip.Components.UserRoles.Reader);

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


				txtId.Text = Request["Id"];
				txtContext.Text = Request["ContextQuery"];
				txtFileName.Text = Request["FileName"];
				txtIncomingNumber.Text = Request["IncomingNumber"];
				txtOutgoingNumber.Text = Request["OutgoingNumber"];
				txtHeader.Text = Request["Header"];
				txtSubject.Text = Request["Subject"];
				txtArchiveFileNames.Text = Request["ArchiveFileNames"];

				SelectListBoxItems(lbxDocCategory.Items, Request["DocCategory"]);
				SelectListBoxItems(lbxDocType.Items, Request["DocType"]);
				SelectListBoxItems(lbxDocSource.Items, Request["DocSource"]);
				SelectListBoxItems(cbxUserRead.Items, Request["UserRead"]);
				SelectListBoxItems(cbxUserFavorite.Items, Request["UserFavorite"]);

				dtrDateReceived.TxtDateFrom = Request["DateReceived_From"];
				dtrDateReceived.TxtDateTo = Request["DateReceived_To"];
				dtrDocumentDate.TxtDateFrom = Request["DocumentDate_From"];
				dtrDocumentDate.TxtDateTo = Request["DocumentDate_To"];






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

		private string BuildUrlQuery(string name, string val)
		{	
			if(name.Length == 0 || val == null || val.Trim().Length == 0)
				return "";
			
			return "&" + name + "=" + HttpUtility.UrlEncode(val.Trim());
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{

			StringBuilder query = new StringBuilder("?x=1");
			query.Append(BuildUrlQuery("Id", txtId.Text) );
			query.Append(BuildUrlQuery("ContextQuery", txtContext.Text) );
			query.Append(BuildUrlQuery("FileName", txtFileName.Text) );
			query.Append(BuildUrlQuery("IncomingNumber", txtIncomingNumber.Text) );
			query.Append(BuildUrlQuery("OutgoingNumber", txtOutgoingNumber.Text) );
			query.Append(BuildUrlQuery("Header", txtHeader.Text) );
			query.Append(BuildUrlQuery("Subject", txtSubject.Text) );
			query.Append(BuildUrlQuery("ArchiveFileNames", txtArchiveFileNames.Text) );
			query.Append(BuildUrlQuery("DocCategory", Request["lbxDocCategory"]) );
			query.Append(BuildUrlQuery("DocType", Request["lbxDocType"]) );
			query.Append(BuildUrlQuery("DocSource", Request["lbxDocSource"]) );
			query.Append(BuildUrlQuery("UserRead", Request["cbxUserRead"]) );
			query.Append(BuildUrlQuery("UserFavorite", Request["cbxUserFavorite"]) );

			query.Append(BuildUrlQuery("DateReceived_From", dtrDateReceived.TxtDateFrom) );
			query.Append(BuildUrlQuery("DateReceived_To", dtrDateReceived.TxtDateTo) );
			query.Append(BuildUrlQuery("DocumentDate_From", dtrDocumentDate.TxtDateFrom) );
			query.Append(BuildUrlQuery("DocumentDate_To", dtrDocumentDate.TxtDateTo) );

			Response.Redirect("~/Search/SearchResults.aspx" + query);

//			TextBox1.Text =  HttpUtility.UrlDecode(query.ToString());
				



		}

		protected void btnReset_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("SearchForm.aspx");
		}
	}
}
