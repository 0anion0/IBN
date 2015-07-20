using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Mediachase.UI.Web.Util;

namespace Mediachase.UI.Web.Events
{
	/// <summary>
	/// Summary description for EventEdit.
	/// </summary>
	public partial class EventEdit : System.Web.UI.Page
	{
		private int EventID
		{
			get 
			{
				return CommonHelper.GetRequestInteger(Request, "EventID", 0);
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ApplyLocalization();
		}

		#region ApplyLocalization
		private void ApplyLocalization()
		{
      ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.Events.Resources.strPageTitles", typeof(EventEdit).Assembly);

			if(EventID != 0)
				pT.Title = LocRM.GetString("tEventEdit");
			else
				pT.Title = LocRM.GetString("tEventAdd");
		}
		#endregion

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
