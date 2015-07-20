﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using Mediachase.Ibn.AssignmentsUI.Code;
using Mediachase.Ibn.AssignmentsUI.Modules.Primitives;
using Mediachase.Ibn.Assignments;
using Mediachase.Ibn.Core.Business;
using Mediachase.Ibn.AssignmentsUI.Modules.Hider;

namespace Mediachase.Ibn.AssignmentsUI.Modules
{
	public class WorkflowItem : CompositeDataBoundControl
	{
		#region --- const ---
		private const string _imageUpUrl = "~/Images/Arrow_Up_Green.png";
		private const string _imageDownUrl = "~/Images/Arrow_Down_Green.png";
		private const string _imageDenyUrl = "~/Images/deny.gif";
		private const string _imageEditUrl = "~/Images/edit.gif"; 
		#endregion

		#region prop: CurrentActivity
		/// <summary>
		/// Gets or sets the current activity.
		/// </summary>
		/// <value>The current activity.</value>
		public Activity CurrentActivity
		{
			get
			{
				if (ViewState["_CurrentNode"] == null)
				{
					throw new ArgumentNullException("CurrentNode");
				}

				return (Activity)ViewState["_CurrentNode"];

			}
			set
			{
				ViewState["_CurrentNode"] = value;
			}
		} 
		#endregion

		#region prop: CurrentContainer
		private WorkflowBuilder _currentContainer;

		/// <summary>
		/// Gets or sets the current container.
		/// </summary>
		/// <value>The current container.</value>
		private WorkflowBuilder CurrentContainer
		{
			get
			{
				return _currentContainer;

			}
			set
			{
				_currentContainer = value;
			}
		}
		#endregion

		#region prop: Prototypes
		private List<ListItem> _prototypes;
		/// <summary>
		/// Gets the prototypes.
		/// </summary>
		/// <value>The prototypes.</value>
		private List<ListItem> Prototypes
		{
			get
			{
				if (_prototypes == null)
				{
					_prototypes = new List<ListItem>();
					foreach (EntityObject eo in BusinessManager.List(AssignmentPrototypeEntity.ClassName, new Mediachase.Ibn.Data.FilterElement[] { }))
					{
						_prototypes.Add(new ListItem(eo.Properties["Name"].Value.ToString(), eo.Properties["AssignmentPrototypeId"].Value.ToString()));
					}
				}

				return _prototypes;
			}
		} 
		#endregion

		#region .ctor
		public WorkflowItem(WorkflowBuilder Container)
		{
			this.CurrentContainer = Container;
		} 
		#endregion

		#region --- Controls ---
		protected Panel divIcons;
		protected Panel divSubIcons;
		protected Panel divContainer;
		protected Panel divInnerContainer;

		protected DropDownList ddType;
		protected DropDownList ddPrototype;

		protected Label lblType;
		protected HtmlGenericControl lblEditPrototype;
		protected HiderExtender extType;

		protected Button btnCreate;
		protected ImageButton btnDelete;
		protected ImageButton btnUp;
		protected ImageButton btnDown;
		protected Button btnEdit;
		#endregion

		#region btnCreate_Click
		[Obsolete]
		/// <summary>
		/// Handles the Click event of the btnCreate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void btnCreate_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			SequentialWorkflowActivity root = this.CurrentContainer.CurrentRootActivity;
			Activity ac = root.GetActivityByName(CurrentActivity.Name);

			CreateAssignmentAndWaitResultActivity newAct = new CreateAssignmentAndWaitResultActivity();
			newAct.Name = String.Format("test dvs {0}", Guid.NewGuid().ToString("N"));
			//newAct.Subject = " Subject Here ";

			if (ac.Parent != null || ac is System.Workflow.ComponentModel.CompositeActivity)
			{
				((System.Workflow.ComponentModel.CompositeActivity)ac).Activities.Add(newAct);
			}
			else
			{
				root.Activities.Add(newAct);
			}

			CurrentContainer.CurrentRootActivity = root;

			this.CurrentContainer.PerformControls();
		}
		#endregion

		#region btnDelete_Click
		/// <summary>
		/// Handles the Click event of the btnDelete control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void btnDelete_Click(object sender, ImageClickEventArgs e)
		{
			SequentialWorkflowActivity root = this.CurrentContainer.CurrentRootActivity;
			Activity ac = root.GetActivityByName(CurrentActivity.Name);
			if (ac.Parent != null)
			{
				ac.Parent.Activities.Remove(ac);
			}

			CurrentContainer.CurrentRootActivity = root;
			this.CurrentContainer.PerformControls();
		} 
		#endregion

		#region btnDown_Click
		/// <summary>
		/// Handles the Click event of the btnDown control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void btnDown_Click(object sender, ImageClickEventArgs e)
		{
			PerformMove(1);
		} 
		#endregion

		#region btnUp_Click
		/// <summary>
		/// Handles the Click event of the btnUp control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void btnUp_Click(object sender, ImageClickEventArgs e)
		{
			PerformMove(0);
		} 
		#endregion

		#region btnEdit_Click
		/// <summary>
		/// Handles the Click event of the btnEdit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void btnEdit_Click(object sender, EventArgs e)
		{
			string popupScript = string.Empty;
			popupScript = String.Format("openPopupWindow('{0}?ActivityId={1}&WfId={2}')", this.ResolveUrl("~/Modules/Pages/PrototypeEdit.aspx"), this.CurrentActivity.Name, this.CurrentContainer.WorkflowId);
			this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
		}
		#endregion


		#region ddType_SelectedIndexChanged
		/// <summary>
		/// Handles the SelectedIndexChanged event of the ddType control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void ddType_SelectedIndexChanged(object sender, EventArgs e)
		{
			//TODO
			btnCreate_Click(sender, e);
		}
		#endregion

		#region PerformMove(todo: move to util class)
		/// <summary>
		/// Performs the move.
		/// </summary>
		/// <param name="direction">The direction.</param>
		private void PerformMove(int direction)
		{
			SequentialWorkflowActivity root = this.CurrentContainer.CurrentRootActivity;
			Activity ac = root.GetActivityByName(CurrentActivity.Name);
			System.Workflow.ComponentModel.CompositeActivity parent = ac.Parent;

			if (parent != null && parent.Activities.Count > 1)
			{
				int index = parent.Activities.IndexOf(ac);

				if (direction == 0)
				{
					//move up
					if (index > 0)
					{
						parent.Activities.Remove(ac);
						index--;
						parent.Activities.Insert(index, ac);
					}

				}
				else
				{
					//movedown
					if (index != parent.Activities.Count - 1)
					{
						parent.Activities.Remove(ac);
						index++;
						parent.Activities.Insert(index, ac);
					}
				}
			}

			CurrentContainer.CurrentRootActivity = root;
			this.CurrentContainer.PerformControls();
		} 
		#endregion

		#region OnLoad
		/// <summary>
		/// Handles the <see cref="E:System.Web.UI.Control.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> object that contains event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			btnDown.Click += new ImageClickEventHandler(btnDown_Click);
			btnDown.OnClientClick = this.CurrentContainer.Page.ClientScript.GetPostBackEventReference(btnDown, string.Empty);
			if (!this.CurrentContainer.Page.IsPostBack)
			{
				ViewState["_test"] = "123123";
			}
			base.OnLoad(e);
			
		}
		#endregion

		#region OnPreRender
		/// <summary>
		/// Handles the <see cref="E:System.Web.UI.Control.PreRender"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			extType.ExchangeTarget = lblType.ClientID;
			lblEditPrototype.Attributes.Add("onclick", this.CurrentContainer.Page.ClientScript.GetPostBackEventReference(btnEdit, string.Empty));
			base.OnPreRender(e);
		} 
		#endregion

		#region Render
		/// <summary>
		/// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
		/// </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "20px");
			//writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "10px");
			if (CurrentActivity is System.Workflow.ComponentModel.CompositeActivity && ((System.Workflow.ComponentModel.CompositeActivity)CurrentActivity).Activities.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "compositeBlock");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "simpleBlock");
			}

			writer.RenderBeginTag(HtmlTextWriterTag.Div);

			//divContainer.RenderControl(writer);
			base.Render(writer);

			//writer.Write(
			writer.RenderEndTag();
		} 
		#endregion

		#region CreateChildControls
		/// <summary>
		/// When overridden in an abstract class, creates the control hierarchy that is used to render the composite data-bound control based on the values from the specified data source.
		/// </summary>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable"/> that contains the values to bind to the control.</param>
		/// <param name="dataBinding">true to indicate that the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)"/> is called during data binding; otherwise, false.</param>
		/// <returns>
		/// The number of items created by the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)"/>.
		/// </returns>
		protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
		{
			divContainer = new Panel();
			divInnerContainer = new Panel();
			divIcons = new Panel();
			divSubIcons = new Panel();

			divIcons.CssClass = "iconBlock";
			divSubIcons.CssClass = "subiconBlock";
			divContainer.CssClass = "itemcontainerBlock";
			divInnerContainer.CssClass = "innerContainerBlock";

			#region Create and init controls
			#region ddType/lblType/extType
			ddType = new DropDownList();
			ddType.ID = "_ddType";
			List<string> typeItems = ActivityFactory.GetAvailableActivities();
			ddType.Items.Add(new ListItem("New activity", "0"));

			foreach (String s in typeItems)
			{
				ddType.Items.Add(new ListItem(s, s));
			}

			ddType.AutoPostBack = true;
			ddType.SelectedIndexChanged += new EventHandler(ddType_SelectedIndexChanged);
			ddType.Style.Add(HtmlTextWriterStyle.Display, "none");

			lblType = new Label();
			lblType.Text = "New activity";
			lblType.ID = "_lblType";
			lblType.CssClass = "createAssignmentLink";

			extType = new HiderExtender();
			extType.ID = "_extType";

			extType.TargetControlID = ddType.ID;
			#endregion

			#region ddPrototype
			ddPrototype = new DropDownList();
			ddPrototype.Items.AddRange(this.Prototypes.ToArray());
			ddPrototype.DataBind(); 
			#endregion

			#region button Create
			btnCreate = new Button();
			btnCreate.Text = "Add";
			btnCreate.ID = "_btnCreate";
			btnCreate.Click += new EventHandler(btnCreate_Click);
			#endregion

			#region button Delete
			btnDelete = new ImageButton();
			//btnDelete.Text = "Delete";
			btnDelete.OnClientClick = "return confirm('Delete?');";
			btnDelete.ImageUrl = _imageDenyUrl;
			btnDelete.Click += new ImageClickEventHandler(btnDelete_Click);
			#endregion

			#region btnUp
			btnUp = new ImageButton();
			//btnUp.Text = "Up";
			btnUp.ImageUrl = _imageUpUrl;
			btnUp.Click += new ImageClickEventHandler(btnUp_Click);
			#endregion

			#region btnDown
			btnDown = new ImageButton();
			//btnDown.Text = "Down";
			btnDown.ID = "btnDown";
			btnDown.ImageUrl = _imageDownUrl;
			#endregion

			#region btnEdit
			btnEdit = new Button();
			btnEdit.ID = "btnEdit";
			btnEdit.Text = "Edit prototype";
			btnEdit.Click += new EventHandler(btnEdit_Click);
			#endregion 

			lblEditPrototype = new HtmlGenericControl("DIV");
			lblEditPrototype.Attributes.Add("class", "editPrototypeLabel");
			lblEditPrototype.InnerHtml = string.Format("<img src='{0}' border='0'/><span>Edit prototype</span>", this.ResolveUrl(_imageEditUrl));
//			lblEditPrototype.Attributes.Add("onclick", "");

			#endregion

			divContainer.ID = this.ID + "_divContainer";
			divContainer.Style.Add(HtmlTextWriterStyle.Display, "inline");
			Control c = ActivityFactory.GetActivityPrimitive(this.CurrentActivity, this.CurrentContainer.Page);
			((IWorkflowPrimitive)c).DataItem = this.CurrentActivity;
			//extType.TestPerform(lblType.ClientID);
			#region Create inner control structure
			divInnerContainer.Controls.Add(c);

			divIcons.Controls.Add(btnDelete);
			divIcons.Controls.Add(btnUp);
			divIcons.Controls.Add(btnDown);

			divIcons.Controls.Add(ddType);
			divIcons.Controls.Add(lblType);
			divIcons.Controls.Add(extType);

			//divIcons.Controls.Add(lblEditPrototype);

			btnCreate.Visible = false;
			divIcons.Controls.Add(btnCreate);

			divSubIcons.Controls.Add(ddPrototype);
			divSubIcons.Controls.Add(btnEdit);
			divIcons.Controls.Add(lblEditPrototype);

			divContainer.Controls.Add(divIcons);
			divContainer.Controls.Add(divSubIcons);
			divContainer.Controls.Add(divInnerContainer); 
			#endregion

			this.Controls.Add(divContainer);
			//this.Controls.Add(extType);
			//lblEditPrototype.Attributes.Add("onclick", this.CurrentContainer.Page.ClientScript.GetPostBackEventReference(btnEdit, string.Empty));
			return 1;
		}
		#endregion
	}
}
