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
using System.Resources;

namespace Mediachase.UI.Web.Projects
{
	/// <summary>
	/// Summary description for AddFincanceActual.
	/// </summary>
	public partial class AddFincanceActual : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.Projects.Resources.strProjectFinances", typeof(AddFincanceActual).Assembly);

			if (Request["ActualFinancesId"] == null)
				pT.Title = LocRM.GetString("tbAdd");
			else
				pT.Title = LocRM.GetString("tbEdit");
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
