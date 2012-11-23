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
using System.IO;
using System.Text;

namespace Bip.WebControls
{
	/// <summary>
	/// Summary description for DocFileDownload.
	/// </summary>
	public partial class DocFileDownload : Bip.WebControls.BipPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				bool isInFrame = (Request["frame"] != null);
				PanFileNotShown.Visible = false;
				try
				{
					string s_id = Request["Id"];
					string s_transaction = Request["TC"];
					bool original = true;
					if(Request["Org"] == null || Request["Org"].Length == 0)
						original = false;
					bool isInline = (Request["inline"] != null && 
						Request["inline"].Length > 0);
			
					DocumentEnt doc = new DocumentEnt();

					if(s_id != null && s_id.Length >0)
					{
						int id = Convert.ToInt32(s_id);
						doc.Load(id);
					}
					else
						if(s_transaction == "1" && DocumentTransaction.IsActive())
						doc = DocumentTransaction.Current.Document;
					else throw new BipFatalException();



					DocFileType fileType = new DocFileType(doc.FileTypeId);
					bool isText = (fileType.ContentType.Trim().ToLower() == "text/plain");
					if(isInline == false)
					{

						Response.Clear();
						Response.ContentType = fileType.ContentType;
						Response.AppendHeader("Content-Disposition","attachment; filename=\"" + doc.FileName + "\"");
						Response.BinaryWrite(doc.DownloadFile(original));
						Response.End(); 
						return;
					}

					if(isText == false)
					{
						if(fileType.ShowInBrowser == false)
						{
							PanFileNotShown.Visible = true;
							lblFileType.Text = fileType.Name;
							if(s_transaction == null)
								hlDownload.NavigateUrl = "~/Documents/DocFileDownload.aspx?id=" + doc.Id.ToString();
							else	hlDownload.NavigateUrl = "~/Documents/DocFileDownload.aspx?TC=1";
							hlDownload.Text = doc.FileName;
							return;
						}

						Response.Clear();
						Response.ContentType = fileType.ContentType;
						Response.AppendHeader("Content-Disposition","inline; filename=\"" + doc.FileName + "\"");
						Response.BinaryWrite(doc.DownloadFile(original));
						Response.End(); 
						return;
					}


					byte [] buffer = doc.DownloadFile(original);
					// this should not be hardcoded
					Decoder d = Encoding.GetEncoding("Windows-1252").GetDecoder();
					char [] chars = new char[buffer.Length];
					d.GetChars(buffer,0,buffer.Length,chars,0);
					textFileContents.InnerText= new string(chars);
				}
				catch(Exception ex)
				{
					if(ex is System.Threading.ThreadAbortException ||
						ex is System.Threading.ThreadInterruptedException)
						throw ex;

					if(!isInFrame)
						throw ex;
					Response.Clear();
					Response.Redirect("~/Error.aspx?error=" + HttpUtility.UrlEncode(ex.Message));
				}
			}
			catch(Exception ex)
			{
				ProcessException(ex);
			}
			//HttpUtility.HtmlEncode(
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
