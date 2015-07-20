namespace Mediachase.UI.Web.Tasks.Modules
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Resources;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Mediachase.Ibn;
	using Mediachase.IBN.Business;
	using Mediachase.UI.Web.Util;
	using Mediachase.UI.Web.Modules;
	using Mediachase.Ibn.Web.UI.WebControls;

	/// <summary>
	///		Summary description for TaskPredecessors.
	/// </summary>
	public partial class TaskPredecessors : System.Web.UI.UserControl
	{
		protected ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.Tasks.Resources.strPredecessors", typeof(TaskPredecessors).Assembly);

		#region BaseTaskID
		protected int BaseTaskID
		{
			get
			{
				try
				{
					return int.Parse(Request["TaskID"]);
				}
				catch
				{
					throw new AccessDeniedException();
				}
			}
		}
		#endregion

		private int projectID = -1;
		protected bool isMSProject = false;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Task.CanUpdateConfigurationInfo(BaseTaskID))
			{
				this.Visible = false;
				return;
			}

			projectID = Task.GetProject(BaseTaskID);
			isMSProject = Project.GetIsMSProject(projectID);

			this.Visible = true;
			if (!IsPostBack)
				BindDG();
		}

		#region BindDG
		private void BindDG()
		{
			Project.ProjectSecurity ps = Project.GetSecurity(projectID);
			if (!(ps.IsManager || ps.IsExecutiveManager || Security.IsUserInGroup(InternalSecureGroups.PowerProjectManager) || Security.IsUserInGroup(InternalSecureGroups.ExecutiveManager)))
				dgPredecessors.Columns[7].Visible = false;

			if (dgPredecessors.EditItemIndex >= 0)
				dgPredecessors.Columns[6].ItemStyle.Width = Unit.Pixel(130);
			else
				dgPredecessors.Columns[6].ItemStyle.Width = Unit.Pixel(60);

			dgPredecessors.Columns[3].HeaderText = LocRM.GetString("ID");
			dgPredecessors.Columns[4].HeaderText = LocRM.GetString("Title");
			dgPredecessors.Columns[5].HeaderText = LocRM.GetString("EndDate");
			dgPredecessors.Columns[6].HeaderText = LocRM.GetString("Lag");

			dgPredecessors.DataSource = Task.GetListPredecessorsDetails(BaseTaskID);
			dgPredecessors.DataBind();

			foreach (DataGridItem dgi in dgPredecessors.Items)
			{
				ImageButton ib = (ImageButton)dgi.FindControl("ibDelete");
				if (ib != null)
					ib.Attributes.Add("onclick", "return confirm('" + LocRM.GetString("Warning") + "')");
			}
		}
		#endregion

		#region BindToolbar
		private void BindToolbar()
		{
			if (dgPredecessors.Items.Count == 0)
				this.Visible = false;
			else
			{
				secHeader.AddText(LocRM.GetString("tTitle"));
				using (IDataReader rdr = Task.GetListVacantPredecessors(BaseTaskID))
				{
					if (rdr.Read())
					{
						CommandManager cm = CommandManager.GetCurrent(this.Page);
						CommandParameters cp = new CommandParameters("MC_PM_AddPredTask");
						string cmd = cm.AddCommand("Task", "", "TaskView", cp);
						cmd = cmd.Replace("\"", "&quot;");
						secHeader.AddRightLink("<img alt='' src='../Layouts/Images/icons/task_predecessors.gif'/> " + LocRM.GetString("Add"),
							String.Format("javascript:{{{0}}}", cmd));
					}
				}
			}
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
			this.dgPredecessors.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dg_cancel);
			this.dgPredecessors.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dg_edit);
			this.dgPredecessors.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dg_update);
			this.dgPredecessors.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dg_delete);
			this.dgPredecessors.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dg_databound);

		}
		#endregion

		#region dg_delete
		private void dg_delete(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgPredecessors.EditItemIndex = -1;
			int LinkID = int.Parse(e.Item.Cells[2].Text);

			Task.DeletePredecessor(LinkID);

			Response.Redirect("../Tasks/TaskView.aspx?TaskID=" + BaseTaskID, true);
		}
		#endregion

		#region dg_edit
		private void dg_edit(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgPredecessors.EditItemIndex = e.Item.ItemIndex;
			BindDG();
		}
		#endregion

		#region dg_databound
		private void dg_databound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			ListItemType lit = e.Item.ItemType;
			if (lit == ListItemType.EditItem)
			{
				int TaskId = int.Parse(e.Item.Cells[0].Text);
				int Lag = int.Parse(e.Item.Cells[1].Text);

				// Retrieve the drop-down list control to set up

				TextBox tbH = (TextBox)e.Item.FindControl("tbH");
				TextBox tbMin = (TextBox)e.Item.FindControl("tbMin");

				if (Lag >= 0)
				{
					tbH.Text = TotalHours(Lag).ToString();
					tbMin.Text = Minutes(Lag).ToString();
				}
				else
				{
					tbH.Text = "-" + TotalHours(-Lag).ToString();
					tbMin.Text = Minutes(-Lag).ToString();
				}
			}
		}
		#endregion

		#region dg_cancel
		private void dg_cancel(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			Response.Redirect("../Tasks/TaskView.aspx?TaskID=" + BaseTaskID, true);
		}
		#endregion

		#region dg_update
		private void dg_update(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgPredecessors.EditItemIndex = -1;

			int LinkID = int.Parse(e.Item.Cells[2].Text);

			TextBox tbH = (TextBox)e.Item.FindControl("tbH");
			TextBox tbMin = (TextBox)e.Item.FindControl("tbMin");

			int lag;
			if (tbH.Text.Trim().StartsWith("-"))
				lag = int.Parse(tbH.Text) * 60 - int.Parse(tbMin.Text);
			else
				lag = int.Parse(tbH.Text) * 60 + int.Parse(tbMin.Text);

			Task.UpdatePredecessor(LinkID, lag);

			Response.Redirect("../Tasks/TaskView.aspx?TaskID=" + BaseTaskID, true);
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			BindToolbar();
		}
		#endregion

		#region FormatTaskDuration
		protected string FormatTaskDuration(int minutes)
		{
			string ret = "";
			if (minutes < 0)
			{
				minutes = -minutes;
				ret = "-";
			}
			TimeSpan span = TimeSpan.FromMinutes(minutes);
			if (span.TotalMinutes >= 60)
			{
				ret += (int)span.TotalHours + LocRM.GetString("h");
			}
			if (ret.Length > 0)
				ret += " ";
			ret += span.Minutes + LocRM.GetString("m");
			return ret;
		}
		#endregion

		#region TotalHours
		protected int TotalHours(int minutes)
		{
			TimeSpan span = TimeSpan.FromMinutes(minutes);
			return (int)span.TotalHours;
		}
		#endregion

		#region Minutes
		protected int Minutes(int minutes)
		{
			TimeSpan span = TimeSpan.FromMinutes(minutes);
			return span.Minutes;
		}
		#endregion
	}
}
