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
using System.Data.OleDb;


namespace Bip.Search
{
	/// <summary>
	/// Summary description for SearchResults.
	/// </summary>
	public partial class SearchResults : Bip.WebControls.BipMainPage
	{
		protected Bip.WebControls.DocSearchCtrl ctrlDocSearch;
		protected Bip.WebControls.TitleCtrl ctrlTitle;

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

			ctrlTitle.DataBind();
			//26.03.2001
			//ctrlDocSearch.PageSize = ;
			if(Page.IsPostBack == false)
			{
				try
				{

					ctrlDocSearch.Reset();
					ctrlDocSearch.Id = Request["Id"];
					ctrlDocSearch.IncomingNumber = Request["IncomingNumber"];
					ctrlDocSearch.OutgoingNumber = Request["OutgoingNumber"];
					ctrlDocSearch.Subject = Request["Subject"];
					ctrlDocSearch.Header = Request["Header"];
					ctrlDocSearch.FileName = Request["FileName"];
					ctrlDocSearch.ArchiveFileNames = Request["ArchiveFileNames"];
					ctrlDocSearch.ContextQuery = Request["ContextQuery"];

					ctrlDocSearch.DateReceived_From = GetDateValue(Request["DateReceived_From"]);
					ctrlDocSearch.DateReceived_To = GetDateValue(Request["DateReceived_To"]);
					ctrlDocSearch.DocumentDate_From = GetDateValue(Request["DocumentDate_From"]);
					ctrlDocSearch.DocumentDate_To = GetDateValue(Request["DocumentDate_To"]);

				
					ctrlDocSearch.DocTypeId_Enum = GetEnumValue(Request["DocType"]);
					ctrlDocSearch.DocSourceId_Enum = GetEnumValue(Request["DocSource"]);
					ctrlDocSearch.DocCategoryId_Enum = GetEnumValue(Request["DocCategory"]);
					ctrlDocSearch.OrderByAttribute = Request["OrderByAttribute"];
					ctrlDocSearch.IsRead = Request["UserRead"];
					ctrlDocSearch.IsFavorite = Request["UserFavorite"];
				
					/*
					if(Request.UrlReferrer == null || Request.UrlReferrer.LocalPath.ToString().ToUpper().IndexOf("SEARCHFORM") == -1)
						hlSearchAgain.Visible = false;
					else
					{
					}
					*/

					string urlBackToSearch = "~/Search/SearchForm.aspx";
					if(Request["BackToSearch"]!= null)
						urlBackToSearch = Request["BackToSearch"];

					StringBuilder navBackToSearch = new StringBuilder(urlBackToSearch);
					if(Request.Url.Query != null)
						navBackToSearch.Append(Request.Url.Query);

					hlSearchAgain.NavigateUrl = navBackToSearch.ToString();




				
					ctrlDocSearch.InitSearch();
				}
				catch(OleDbException ex)
				{

					/*
					int res = ex.ErrorCode;
					string ee = ex.Source;
					for(int i=0; i<ex.Errors.Count; i++)
					{
						OleDbError err  = ex.Errors[i];
						string s;
						s = err.Message;
						s = err.NativeError.ToString();
						s = err.Source.ToString();
						s = err.SQLState.ToString();
						s = "x" + s;
					}*/

					ProcessException(ex);

				}

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
	}
}
