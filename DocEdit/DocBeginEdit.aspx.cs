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

namespace Bip.Documents
{
	/// <summary>
	/// Summary description for DocBeginEdit.
	/// </summary>
	public partial class DocBeginEdit : Bip.WebControls.BipPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				int id = Convert.ToInt32(Request["Id"]);
				DocumentEnt doc = new DocumentEnt();
				doc.Load(id);
				DocumentTransaction.Begin(doc);
				Response.Redirect("DocumentEdit.aspx");
			}

			catch(Exception ex)
			{
				this.ProcessException(ex);
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


	}
}
