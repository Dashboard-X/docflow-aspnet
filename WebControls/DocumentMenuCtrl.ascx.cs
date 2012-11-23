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
	///		Summary description for DocumentMenuCtrl.
	/// </summary>
	public partial  class DocumentMenuCtrl : System.Web.UI.UserControl
	{
		protected int m_DocId = 0;
		public int DocumentId
		{
			set{m_DocId = value; }
			get{return m_DocId; }
		}

		public override void DataBind()
		{
			hlDocInfoView.NavigateUrl = "~/Documents/DocInfoView.aspx?Id=" + m_DocId.ToString();
			hlDocTextView.NavigateUrl = "~/Documents/DocTextView_FrameSet.aspx?Id=" + m_DocId.ToString();
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
