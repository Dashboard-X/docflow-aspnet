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

namespace Bip.Home
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public partial class _default : Bip.WebControls.BipMainPage
	{
		protected Bip.WebControls.DocSearchCtrl ctrlDocSearch;

		protected DateTime GetFirstDayOfMonth(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		protected DateTime GetFirstDayOfWeek(DateTime date)
		{
			DayOfWeek dayOfWeek = date.DayOfWeek;
			switch( dayOfWeek )
			{
				case DayOfWeek.Monday: return date;
				case DayOfWeek.Tuesday: return date.AddDays(-1);
				case DayOfWeek.Wednesday: return date.AddDays(-2);
				case DayOfWeek.Thursday: return date.AddDays(-3);
				case DayOfWeek.Friday: return date.AddDays(-4);
				case DayOfWeek.Saturday: return date.AddDays(-5);
				case DayOfWeek.Sunday: return date.AddDays(-6);
			}
			return date;


		}

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Page.IsPostBack == false)
			{
				string qryBackToSearch = "BackToSearch=" + HttpUtility.UrlEncode("~/Home");
				string qryUnread = "&UserRead=0";
				hlSearchAll.NavigateUrl = "~/Search/SearchResults.aspx"+ "?" + qryBackToSearch + qryUnread;
				hlSearchMonth.NavigateUrl = "~/Search/SearchResults.aspx?DateReceived_From=" + 
							HttpUtility.UrlEncode(GetFirstDayOfMonth(DateTime.Now).ToShortDateString())
					 + "&" + qryBackToSearch + qryUnread;
				hlSearchWeek.NavigateUrl = "~/Search/SearchResults.aspx?DateReceived_From=" + 
							HttpUtility.UrlEncode(GetFirstDayOfWeek(DateTime.Now).ToShortDateString()) + 
					 "&" + qryBackToSearch + qryUnread;

				if(cldDocDate.SelectedDate == DateTime.MinValue)
					cldDocDate.SelectedDate = DateTime.Now;

				DateTime selectedDate = cldDocDate.SelectedDate;
				lblDocListCaption.Text = Bip.Components.BipResources.GetString("StrDocumentsDeliveredOn") + " " + selectedDate.ToLongDateString();
				ctrlDocSearch.Reset();
				ctrlDocSearch.DateReceived_From = selectedDate;
				ctrlDocSearch.DateReceived_To = selectedDate;
				ctrlDocSearch.OrderByAttribute = "CreationTime desc";
				ctrlDocSearch.InitSearch();

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

		protected void cldDocDate_SelectionChanged(object sender, System.EventArgs e)
		{

			if(cldDocDate.SelectedDate == DateTime.MinValue)
				cldDocDate.SelectedDate = DateTime.Now;
			cldDocDate.DataBind();

			DateTime selectedDate = cldDocDate.SelectedDate;
			lblDocListCaption.Text = Bip.Components.BipResources.GetString("StrDocumentsDeliveredOn") + " " + selectedDate.ToLongDateString();
			ctrlDocSearch.Reset();
			ctrlDocSearch.DateReceived_From = selectedDate;
			ctrlDocSearch.DateReceived_To = selectedDate;
			ctrlDocSearch.OrderByAttribute = "CreationTime desc";
			ctrlDocSearch.InitSearch();

		}
	}
}
