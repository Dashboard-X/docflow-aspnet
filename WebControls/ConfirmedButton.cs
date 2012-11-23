namespace MetaBuilders.WebControls 
{

    using System;
    using System.Configuration;
    using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    
	/// <summary>
	/// Displays a push button control on the Web page.
	/// When the user clicks the button, a dialog box appears asking to confirm the action.
	/// </summary>
	/// <remarks>
	/// Use the ConfirmedButton to create a button on the page which makes sure the user really wanted to push it.
	/// This is a common requirement of buttons which delete or change data.
	/// After clicking the button, but before the page posts back to the server, the user is asked to confirm the action.
	/// The text of the message displayed is set with the <see cref="ConfirmedButton.Message"/> property.
	/// </remarks>
	/// <example>
	/// The following example demonstrates how to create a submit Button which confirms an important action.
	/// <code>
	/// <![CDATA[
    /// <%@ Page Language="C#" %>
    /// <%@ Register tagprefix="mbcb" namespace="MetaBuilders.WebControls" assembly="MetaBuilders.WebControls.ConfirmedButton" %>
    /// <script runat="server">
    ///     protected void MyButton_Click( Object Sender, EventArgs E ) {
    ///         Result.Text = System.DateTime.Now.ToString();
    ///     }
    /// </script>
    /// <html>
    /// 	<body>
    /// 	<form runat="server">
    ///         <mbcb:ConfirmedButton runat="server" id="MyButton" Text="Click Me" Message="Are you sure?" onclick="MyButton_Click" />
    ///         <asp:Label runat="server" id="Result" />
    /// 	</form>
    /// 	</body>
    /// </html>
	/// ]]>
	/// </code>
	/// </example>
	[
		ToolboxData("<{0}:ConfirmedButton runat=server></{0}:ConfirmedButton>"),
		ToolboxBitmap(typeof(Bitmap))
	]
	public class ConfirmedButton : Button {
        
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfirmedButton"/> class.
		/// </summary>
		/// <remarks>
		/// Use this constructor to create and initialize a new instance of the <see cref="ConfirmedButton"/> class.
		/// </remarks>
		public ConfirmedButton() : base() {
			Message = "Are you sure you want to do this?";
		}

		/// <summary>
		/// Gets or sets the message displayed to confirm the action before postback.
		/// </summary>
		/// <value>The message displayed to confirm the action before postback. The default value is "Are you sure you want to do this?".</value>
		[
			DefaultValue("Are you sure you want to do this?"),
			Category("Appearance")
		]
        public String Message {
            get { return (String)ViewState["Message"]; }
            set { ViewState["Message"] = value; }
        }
        
        /// <summary>
		/// Adds to the specified writer those HTML attributes and styles that need to be rendered. This method is primarily used by control developers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        /// <remarks>
        /// Overridden to add the <see cref="ConfirmedButton.Message"/> to the Attributes collection.
        /// </remarks>
		protected override void AddAttributesToRender( HtmlTextWriter writer ) {
            Attributes.Add("confirmationmessage", Message );
            base.AddAttributesToRender( writer );
        }
        
		/// <summary>
		/// Raises the PreRender event, which notifies the server control that is about to be rendered to the page.
		/// </summary>
		/// <remarks>Overridden to register client script with the <see cref="Page"/></remarks>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data. </param>
        protected override void OnPreRender( EventArgs e ) {
            Page.RegisterClientScriptBlock( "WebUIConfirmation", "<script language='javascript' src='" + ResolveUrl( ScriptPath + "/WebUIConfirmation.js" ) + "'></script>" );
            Page.RegisterArrayDeclaration("Page_Confirmations", "'" + ClientID + "'" );
            Page.RegisterStartupScript( "WebUIConfirmation Startup", "<script language='javascript' src='" + ResolveUrl( ScriptPath + "/WebUIConfirmationStartup.js" ) + "'></script>" );
            base.OnPreRender( e );
        }
        
        /// <summary>
        /// The path to the the clientside script library supporting the confirmation.
        /// </summary>
        /// <remarks>
        /// The property simply combines the values of ScriptPath and ScriptVersion
        /// </remarks>
        protected virtual String ScriptPath {
            get {
                return "~/Scripts";
            }
        }
        
        /// <summary>
        /// The script version
        /// </summary>
        /// <remarks>
        /// This determines the folder, under the ScriptPathRoot, where this version of the control has its library.
        /// </remarks>
        protected virtual String ScriptVersion {
            get {
                return "1_7";
            }
        }
        
        /// <summary>
        /// The virtual path to the script library folder
        /// </summary>
        /// <remarks>
        /// By default, this property will hold the string "/MetaBuilders_WebControls_client/Confirmation/".
        /// However, this can be changed by specifiying the path in the application's web.config.
        /// </remarks>
	    /// <example>
	    /// The following example demonstrates how to set the script library path via web.config to state that the library is under the application's folder.
	    /// <code>
	    /// <![CDATA[
        /// <configSections>
        ///    <sectionGroup name="metaBuilders.webControls">
        ///       <section name="confirmationScript"
        ///          type="System.Configuration.NameValueSectionHandler,system, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
        ///    </sectionGroup>
        /// </configSections>
        /// 
        /// <metaBuilders.webControls>
        ///    <confirmationScript>
        ///       <add key="scriptPathRoot" value="~/MetaBuilders_WebControls_client/Confirmation/" />
        ///    </confirmationScript>
        /// </metaBuilders.webControls>
	    /// ]]>
	    /// </code>
	    /// </example>
        protected virtual String ScriptPathRoot {
            get {
                NameValueCollection thisSettings = (NameValueCollection)ConfigurationSettings.GetConfig("metaBuilders.webControls/confirmationScript");
                if ( thisSettings != null ) {
                    return thisSettings["scriptPathRoot"];
                } else {
                    return "/MetaBuilders_WebControls_client/Confirmation/";
                }
            }
        }
        
        
    }
}
