/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

namespace Bip.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Text;
	using System.Data.OleDb;

	using Bip.Components;

	/// <summary>
	///		Summary description for DocSearchCtrl.
	/// </summary>
	public partial  class DocSearchCtrl : System.Web.UI.UserControl
	{


		public string DocLinkUrl_Begin = "javascript:ViewDoc(";
		public string DocLinkUrl_End = ")";

		public DateTime DateReceived_From;
		public DateTime DateReceived_To;
		public DateTime CreationTime_From;
		public DateTime CreationTime_To;

		public DateTime DocumentDate_From;
		public DateTime DocumentDate_To;
		public string Id;
		public string IncomingNumber;
		public string OutgoingNumber;
		public string Subject;
		public string Header;
		public string FileName;
		public string ArchiveFileNames;
		public IEnumerator DocTypeId_Enum;
		public IEnumerator DocSourceId_Enum;
		public IEnumerator DocCategoryId_Enum;

		public string IsRead = "";
		public string IsFavorite = "";

		public string ContextQuery;
		public string OrderByAttribute;

		public int  PageSize = 20;
		public bool AllowPaging;
		public bool AllowSorting;

		protected int m_VirtualCount = 0;
		protected bool m_HasMoreResults = false;
		protected bool m_HasResults = false;
		protected bool m_Error = false;
	
		public bool SearchError
		{
			get{return m_Error; }
		}
		public bool HasResults
		{
			get{return m_HasResults;}
		}

		public int VirtualCount
		{
			get{return m_VirtualCount;}
        }
		public bool HasMoreResults
		{
			get{ return m_HasMoreResults; }
		}
		


		protected void UpdateResultInfo(DocSearch search)
		{
			lblMessage.ForeColor = Color.Black;
				if(search.HasResults)
				{
					if(search.HasMoreResults)
							lblMessage.Text = String.Format(Bip.Components.BipResources.GetString("StrMoreThenNumDocumentsFound"),  search.VirtualCount.ToString());
					else	lblMessage.Text = String.Format(Bip.Components.BipResources.GetString("StrNumDocumentsFound"),  search.VirtualCount.ToString());
					

					grdSearchResults.PagerStyle.Visible = (bool)(grdSearchResults.PageCount  > 1);
					
					
				}
				else
					lblMessage.Text = Bip.Components.BipResources.GetString("StrNoSearchResults");
		}

		public void Reset()
		{
	
			m_Error = false;
			DateReceived_From = DateTime.MinValue;
			DateReceived_To = DateTime.MinValue;
			DocumentDate_From = DateTime.MinValue;
			DocumentDate_To = DateTime.MinValue;
			CreationTime_From = DateTime.MinValue;
			CreationTime_To = DateTime.MinValue;
			Id = "";
			IncomingNumber = "";
			OutgoingNumber = "";
			Subject = "";
			Header = "";
			FileName = "";
			ArchiveFileNames = "";
			DocTypeId_Enum = null;
			DocSourceId_Enum = null;
			DocCategoryId_Enum = null;
			PageSize = 20;
		}


		public DocSearchCtrl()
		{
			Reset();
		}

		public void InitSearch()
		{
			try
			{
				ArrayList attrFilters = new ArrayList();
			
				attrFilters.Add(DocSearch.FilterDateRange("DateReceived", DateReceived_From, DateReceived_To));
				attrFilters.Add(DocSearch.FilterDateRange("DocumentDate", DocumentDate_From, DocumentDate_To));
				attrFilters.Add(DocSearch.FilterDateRange("CreationTime", CreationTime_From, CreationTime_To));
			
				attrFilters.Add(DocSearch.FilterStringEquals("Id", Id));
				attrFilters.Add(DocSearch.FilterString("IncomingNumber", IncomingNumber));
				attrFilters.Add(DocSearch.FilterString("OutgoingNumber", OutgoingNumber));
				attrFilters.Add(DocSearch.FilterString("Subject", Subject));
				attrFilters.Add(DocSearch.FilterString("Header", Header));
				attrFilters.Add(DocSearch.FilterString("D.FileName", FileName));
				attrFilters.Add(DocSearch.FilterString("ArchiveFileNames", ArchiveFileNames));
			
				attrFilters.Add(DocSearch.FilterEnumeration("DocTypeId", DocTypeId_Enum));
				attrFilters.Add(DocSearch.FilterEnumeration("DocSourceId", DocSourceId_Enum));
				attrFilters.Add(DocSearch.FilterEnumeration("DocCategoryId", DocCategoryId_Enum));

				if(IsRead != null && IsRead.Length >0)
					attrFilters.Add(DocSearch.FilterIsRead( IsRead == "1"));

				if(IsFavorite != null && IsFavorite.Length >0)
					attrFilters.Add(DocSearch.FilterIsFavorite( IsFavorite == "1"));


				StringBuilder attrFilterDef = new StringBuilder();
				foreach(string flt in attrFilters)
				{
					if(flt == null ||  flt.Length == 0)
						continue;
					if(attrFilterDef.Length != 0)
						attrFilterDef.Append(" AND ");
					attrFilterDef.Append(flt);
				}

				//if(ContextQuery != null && ContextQuery.Trim().Length >0)
				//	ContextQuery = ContextQuery + " zmqpxnowcbievruytalskdjfhgxs";
				// well, this is the only solution filter  IDX IGNORED WORDS exception
				// unfortunately it's impossible to identify this error 

				String contextFilterDef = DocSearch.FilterContext(ContextQuery);

				ViewState["AttributesFilter"] = attrFilterDef.ToString();
				ViewState["ContextFilter"] = contextFilterDef.ToString();
				ViewState["OrderByAttribute"] = OrderByAttribute;


				ViewState["DocLinkUrl_Begin"] = DocLinkUrl_Begin;
				ViewState["DocLinkUrl_End"] = DocLinkUrl_End;




				DocSearch search = new DocSearch();
				if(search.Find(attrFilterDef.ToString(), contextFilterDef, OrderByAttribute, 0, PageSize))
				{
					grdSearchResults.DataSource = search.ResultSet;
					grdSearchResults.PageSize = PageSize;
					//grdSearchResults.CurrentPageIndex = 0;
					grdSearchResults.VirtualItemCount = search.VirtualCount;
					grdSearchResults.DataBind();
					grdSearchResults.Visible = true;
				}
				else grdSearchResults.Visible = false;

				UpdateResultInfo(search);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		}


     
		protected void ProcessException(Exception ex)
		{
			if(!(ex is OleDbException))
				throw ex;
			OleDbException dbEx = (OleDbException) ex;
			string errMessage = null;
			for(int iErr=0; iErr < dbEx.Errors.Count; iErr++)
			{
				OleDbError err = dbEx.Errors[iErr];
				if(err.NativeError == 7312)
				{
					errMessage = err.Message;
					break;
				}
			}

			if(errMessage == null || errMessage.Length == 0)
				throw ex;
			if(errMessage.ToUpper().IndexOf("IGNORED") != -1)
				errMessage = "  ";
			else throw ex; // ???
			// well, this is the only solution to filter  IDX IGNORED WORDS exception
			// unfortunately it's impossible to identify this error 

			lblMessage.Text = errMessage;
			lblMessage.ForeColor = Color.Red;
			m_Error = true;
			m_HasResults = false;
			m_VirtualCount = 0;
			m_HasMoreResults = false;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{

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
			this.grdSearchResults.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdSearchResults_PageIndexChanged);
			this.grdSearchResults.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.grdSearchResults_SortCommand);

		}
		#endregion

		private void grdSearchResults_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			string attrFilterDef = (string)ViewState["AttributesFilter"];
			string contextFilterDef = (string)ViewState["ContextFilter"];
			int recStart = e.NewPageIndex * grdSearchResults.PageSize;
			int recNumber = grdSearchResults.PageSize;
			string orderByAttribute = (string)ViewState["OrderByAttribute"];

			DocLinkUrl_Begin = (string) ViewState["DocLinkUrl_Begin"];
			DocLinkUrl_End = (string) ViewState["DocLinkUrl_End"];



			try
			{
				DocSearch search = new DocSearch();
				if(search.Find(attrFilterDef.ToString(), contextFilterDef, orderByAttribute, recStart, recNumber))
				{
					grdSearchResults.CurrentPageIndex = e.NewPageIndex;
					grdSearchResults.DataSource = search.ResultSet;
					grdSearchResults.VirtualItemCount = search.VirtualCount;
					grdSearchResults.DataBind();
					grdSearchResults.Visible = true;
				}
				else grdSearchResults.Visible = false;

				UpdateResultInfo(search);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		
		}

		private void grdSearchResults_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			
			string attrFilterDef = (string)ViewState["AttributesFilter"];
			string contextFilterDef = (string)ViewState["ContextFilter"];
			int recStart = grdSearchResults.CurrentPageIndex * grdSearchResults.PageSize;
			int recNumber = grdSearchResults.PageSize;
			string orderByAttribute = e.SortExpression;
			ViewState["OrderByAttribute"] = e.SortExpression;
			OrderByAttribute = e.SortExpression;

			DocLinkUrl_Begin = (string) ViewState["DocLinkUrl_Begin"];
			DocLinkUrl_End = (string) ViewState["DocLinkUrl_End"];


			try
			{
				DocSearch search = new DocSearch();
				if(search.Find(attrFilterDef.ToString(), contextFilterDef, orderByAttribute, recStart, recNumber))
				{
					//grdSearchResults.CurrentPageIndex = e.NewPageIndex;
					grdSearchResults.DataSource = search.ResultSet;
					grdSearchResults.VirtualItemCount = search.VirtualCount;
					grdSearchResults.DataBind();
					grdSearchResults.Visible = true;
				}
				else grdSearchResults.Visible = false;

				UpdateResultInfo(search);
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
		
		}
	}
}
