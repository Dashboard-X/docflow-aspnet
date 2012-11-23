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

namespace Bip.Dictionaries
{
	/// <summary>
	/// Summary description for DocTypeList.
	/// </summary>
	public partial class DocTypeList : Bip.WebControls.BipMainPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			grdItems.DataSource = Bip.Components.DocTypeEnt.FindAll();
			grdItems.DataBind();

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
