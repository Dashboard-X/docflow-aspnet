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

	/// <summary>
	///		Summary description for DateRangeCtrl.
	/// </summary>
	public partial  class DateRangeCtrl : System.Web.UI.UserControl
	{

		public string TxtDateFrom
		{
			get{ return txtDateFrom.Text.Trim(); }
			set{ txtDateFrom.Text = value; }
		}

		public string TxtDateTo
		{
			get{ return txtDateTo.Text.Trim(); }
			set{ txtDateTo.Text = value; }
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
