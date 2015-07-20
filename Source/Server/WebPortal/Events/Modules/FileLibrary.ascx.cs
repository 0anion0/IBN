namespace Mediachase.UI.Web.Events.Modules
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Resources;
	using Mediachase.IBN.Business;

	/// <summary>
	///		Summary description for FileLibrary.
	/// </summary>
	public partial  class FileLibrary : System.Web.UI.UserControl
	{


    protected ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.FileLibrary.Resources.strFileLibrary", typeof(FileLibrary).Assembly);
    protected UserLightPropertyCollection pc = Security.CurrentUser.Properties;
		private string pcKey = "EvFS_CurrentTab";

		#region EventId
		private int EventId
		{
			get 
			{
				try
				{
					return int.Parse(Request["EventId"]);
				}
				catch
				{
					return -1;
				}
			}
		}
		#endregion

		#region SubTab
		private string subTab;
		protected string SubTab
		{
			get 
			{
				if (subTab == null)
				{
					subTab = "";
					if (Request["SubTab"] != null)
						subTab = Request["SubTab"];
				}
				return subTab;
			}
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Add Tabs
      string sLinkBegin = (Security.CurrentUser.IsExternal) ? "ExternalEvent.aspx" : "EventView.aspx";

      string link = String.Format("{1}?EventId={0}&Tab=FileLibrary&SubTab=0", EventId, sLinkBegin);
			blockControl.AddTab("0", LocRM.GetString("tFileStorage"), link, "../FileStorage/Modules/FileStorageControl.ascx");

      if (!Security.CurrentUser.IsExternal)
      {
        link = String.Format("{1}?EventId={0}&Tab=FileLibrary&SubTab=1", EventId, sLinkBegin);
        blockControl.AddTab("1", LocRM.GetString("tFileList"), link, "../FileStorage/Modules/FilesList.ascx");

        link = String.Format("{1}?EventId={0}&Tab=FileLibrary&SubTab=2", EventId, sLinkBegin);
        blockControl.AddTab("2", LocRM.GetString("tSearch"), link, "../FileStorage/Modules/FileSearch.ascx");

        // Select Tab
        if ((SubTab == "0" || SubTab == "1" || SubTab == "2"))
          pc[pcKey] = SubTab;
        else if (pc[pcKey] == null)
          pc[pcKey] = "0";
      }
      else
        pc[pcKey] = "0";
			blockControl.SelectTab(pc[pcKey]);
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
