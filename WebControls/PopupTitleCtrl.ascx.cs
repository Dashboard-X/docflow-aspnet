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
	///		Summary description for PopupTitleCtrl.
	/// </summary>
	public partial  class PopupTitleCtrl : System.Web.UI.UserControl
	{



		public String Title = null;
		public String Action  = null;
		public String ActionUrl = null;

		protected void Page_Load(object sender, System.EventArgs e) 
		{
			ModuleTitle.Text = Title;
			if(Action != null && Action.Length != 0)
			{
				EditButton.Text = Action;
				EditButton.NavigateUrl = ActionUrl;
			}
			else EditButton.Visible = false;
				
			//EditButton.Target = EditTarget;
		}
        
		public PopupTitleCtrl() 
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Init(object sender, EventArgs e) 
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
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
