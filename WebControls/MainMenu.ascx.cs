/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

namespace Bip.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Bip.Components;
	using System.Text;

	/// <summary>
	///		Summary description for MainMenu.
	/// </summary>
	public partial  class MainMenu : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl MainMenuContainer;


		static string[] m_TabCaptions = {"StrMenuHome", "StrMenuDocuments", "StrMenuDictionaries", "StrMenuSecurity", "StrMenuAbout"};
		static string[] m_TabDirs = {"home", "search", "dictionaries", "security", "about"};
		static int[] m_GeneralTabs = {0,1,4};
		static int[] m_AdminTabs = {0,1,2,3,4};

		int[] m_UserTabs = null;
		
		int m_TabOrder = 1;
		int m_ActiveTab = -1;
		bool m_ActivePrevTab = false;
		
		



		StringBuilder m_MenuBuilder = new StringBuilder();


		void WriteTabBody(int tab)
		{
			if(tab == m_ActiveTab)
			{
				m_MenuBuilder.Append("<TD noWrap background=\"../Images/menu.on.bg.gif\">");
				m_ActivePrevTab = true;
			}
			else
			{
				m_MenuBuilder.Append("<TD noWrap background=\"../Images/menu.off.bg.gif\">");				
				m_ActivePrevTab = false;
			}

			m_MenuBuilder.Append("<A class=mainMenu tabIndex=");
			m_MenuBuilder.Append(m_TabOrder.ToString());
			m_MenuBuilder.Append("<A class=mainMenu href='../");
			m_MenuBuilder.Append(m_TabDirs[tab]);
			m_MenuBuilder.Append("'><FONT class='mainMenu'>");
			m_MenuBuilder.Append(Bip.Components.BipResources.GetString(m_TabCaptions[tab]));
			m_MenuBuilder.Append("</FONT></A>&nbsp;</TD>");
			
			m_TabOrder ++;
		}

		void WriteTabSeparator(int tab)
		{
			m_MenuBuilder.Append("<TD><IMG src='../Images/menu.");
			if(m_ActivePrevTab)
					m_MenuBuilder.Append("on.");
			else	m_MenuBuilder.Append("off.");

			if(tab == m_ActiveTab)
					m_MenuBuilder.Append("on.");
			else	m_MenuBuilder.Append("off.");
			m_MenuBuilder.Append("separator.gif'></TD>");
		}

		void WriteFirstTab(int tab)
		{
			m_MenuBuilder.Append("<TD vAlign=top width=130><IMG  align=\"top\" src=\"../Images/menu.");
			if(tab == m_ActiveTab)
					m_MenuBuilder.Append("on.");
			else	m_MenuBuilder.Append("off.");
			m_MenuBuilder.Append("begin.gif\"></TD>");

			WriteTabBody(tab);
		}


		void WriteLastTab(int tab)
		{
			WriteTabSeparator(tab);
			WriteTabBody(tab);
			
			m_MenuBuilder.Append("<TD><IMG src='../Images/menu.");
			if(tab == m_ActiveTab)
					m_MenuBuilder.Append("on.");
			else	m_MenuBuilder.Append("off.");
			m_MenuBuilder.Append("end.gif'></TD>");
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{

			m_MenuBuilder.Append("<TABLE id=MainTabbedMenuTable cellSpacing=0 cellPadding=0 width='100%' border=0 bgColor=#336699>");
			m_MenuBuilder.Append("<TR><TD><img src='../images/b00.gif' height=18 width=1 border=0></TD>");
			m_MenuBuilder.Append("<TR>");


			if(UserRoles.UserHasRole(UserRoles.Administrator))
					m_UserTabs = m_AdminTabs;
			else	m_UserTabs = m_GeneralTabs;

			string reqPath = Request.FilePath.ToLower().Replace("\\", "/");
			for(int i=0; i< m_UserTabs.Length; i++)
				if(reqPath.IndexOf("/" + m_TabDirs[m_UserTabs[i]] + "/") != -1 )
				{
					m_ActiveTab = m_UserTabs[i];
					break;
				}

			WriteFirstTab(m_UserTabs[0]);
			for(int i=1; i< m_UserTabs.Length-1; i++)
			{
				int tab = m_UserTabs[i];
				WriteTabSeparator(tab);
				WriteTabBody(tab);
			}
			WriteLastTab(m_UserTabs[m_UserTabs.Length-1]);

			m_MenuBuilder.Append("<TD noWrap align='middle' background='../Images/menu.end.bg.gif' bgColor=#336699>&nbsp;&nbsp;&nbsp;&nbsp;<A class='mainMenuEx' href='../Login.aspx?Logout=1'><FONT class='mainMenuEx'>Sign-out</FONT></A></TD>");
			m_MenuBuilder.Append("<TD noWrap align=right width='100%' background='../Images/menu.end.bg.gif' bgColor=#336699 >&nbsp;</TD> </TR></TABLE>");
			// !!!!!!!!!!!!!!!!!!!!!!!!!!
			//Response.Write();
		}

		protected override void Render(HtmlTextWriter output)
		{       
			output.Write(m_MenuBuilder.ToString());         
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
