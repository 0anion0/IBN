namespace Mediachase.UI.Web.Incidents.Modules
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Resources;
	using Mediachase.IBN.Business;
	using Mediachase.UI.Web.Util;
	using Mediachase.UI.Web.Modules;

	/// <summary>
	///		Summary description for IncidentsToDo.
	/// </summary>
	public partial  class IncidentsToDo : System.Web.UI.UserControl
	{
    public ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.Incidents.Resources.strIncidentGeneral", typeof(IncidentsToDo).Assembly);

		#region IncidentId
		private int IncidentId
		{
			get
			{
				try
				{
					return int.Parse(Request["IncidentId"]);
				}
				catch
				{
					throw new Exception("Not valid Incident ID!!!");
				}
			}
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Visible = true;
			if (!IsPostBack)
				BindDataGrid();

			BindToolbar();
		}

		#region GetBool
		protected bool GetBool(int val)
		{
			if (val == 1) 
				return true;
			else
				return false;
		}
		#endregion

		#region BindToolbar
		private void BindToolbar()
		{
			tbToDo.AddText(LocRM.GetString("tbToDo"));
			if (Incident.CanAddToDo(IncidentId))
			{
				if (dgToDo.Items.Count == 0)
				{
					this.Visible = false;
				}
				else
					tbToDo.AddRightLink("<img alt='' src='../Layouts/Images/icons/task_create.gif'/> " + LocRM.GetString("tbAdd"),"../ToDo/ToDoEdit.aspx?IncidentId=" + IncidentId);
			}
			else if(dgToDo.Items.Count == 0)
				this.Visible = false;
		}

		#endregion

		#region BindDataGrid
		private void BindDataGrid()
		{
			dgToDo.Columns[4].Visible = Incident.CanDeleteToDo(IncidentId);

			DataTable dt = Incident.GetListToDoDataTable(IncidentId);
			DataView dv = dt.DefaultView;
			dv.Sort = "Title";
			dgToDo.DataSource = dv;
			for(int i=0;i < dt.Rows.Count;i++)
			{
				if(!(bool)dt.Rows[i]["IsCompleted"])
					dt.Rows[i]["ReasonName"] = "";
			}

			dgToDo.Columns[1].HeaderText = LocRM.GetString("Title");
			dgToDo.Columns[2].HeaderText = LocRM.GetString("Status");
			dgToDo.DataBind();
		}

		public string ShowCompletion(bool isCompleted)
		{
			if(isCompleted)
				return LocRM.GetString("Yes");
			return "";
		}

		protected string CompletionString(object ReasonId, object CompletedBy)
		{
			if (ReasonId!=DBNull.Value && CompletedBy!=DBNull.Value)
				return GetCompletionType((int)ReasonId) + " " + CommonHelper.GetUserStatus((int)CompletedBy);
			else
			return "";

		}

		private string GetCompletionType(int type)
		{
			CompletionReason rsn = (CompletionReason)type;
			switch (rsn)
			{
				case CompletionReason.SuspendedManually:
				case CompletionReason.SuspendedAutomatically:
					return LocRM.GetString("Suspended");
				case CompletionReason.CompletedManually:
					return LocRM.GetString("CompletedByManager");
				case CompletionReason.CompletedAutomatically:
					return LocRM.GetString("CompletedByResource");
				case CompletionReason.NotCompleted:
					return LocRM.GetString("NotCompleted");
			}
			return "";
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgToDo.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgToDo_DeleteCommand);
		}
		#endregion

		#region dgToDo_DeleteCommand
		private void dgToDo_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int trdt = int.Parse(e.Item.Cells[0].Text);
			ToDo.Delete(int.Parse(e.Item.Cells[0].Text));
			Util.CommonHelper.ReloadTopFrame("ActiveWork.ascx", "../Incidents/IncidentView.aspx?IncidentId=" + IncidentId, Response);
		}
		#endregion

		#region lbDeleteToDoAll_Click
		protected void lbDeleteToDoAll_Click(object sender, System.EventArgs e)
		{
			int ToDoId = int.Parse(hdnID.Value);
			ToDo.Delete(ToDoId);
			Util.CommonHelper.ReloadTopFrame("ActiveWork.ascx", "../Incidents/IncidentView.aspx?IncidentId=" + IncidentId, Response);
		}
		#endregion

//===========================================================================
// This public property was added by conversion wizard to allow the access of a protected, autogenerated member variable tbToDo.
//===========================================================================
    public Mediachase.UI.Web.Modules.BlockHeaderLightWithMenu tbToDo {
        get { return Migrated_tbToDo; }
        //set { Migrated_tbToDo = value; }
    }
	}
}