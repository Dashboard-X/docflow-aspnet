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
	/// Summary description for DocTextView_Frameset.
	/// </summary>
	public partial class DocTextView_Frameset : Bip.WebControls.BipPage
	{
		public string FramesetRows = "55,*";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*try
			{
				bool hasDocLinks = true;
				DocumentEnt doc = new DocumentEnt();
				doc.Load(Convert.ToInt32(Request["Id"]));
				if(doc.ParentId == 0 &&	doc.PreviousVersionId == 0 )
				{
					if(doc.RefDocuments != null)
					{
						IEnumerator enumDocs =doc.RefDocuments.GetEnumerator();
						enumDocs.Reset();
						if(!enumDocs.MoveNext())
							hasDocLinks = false;
					}	else hasDocLinks = false;
				}

				doc.Dispose();
				
				if(!doc.CanEdit && hasDocLinks == false)
					FramesetRows = "32,*";
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}*/
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
