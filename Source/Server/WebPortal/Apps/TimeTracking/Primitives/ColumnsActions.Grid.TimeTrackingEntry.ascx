<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mediachase.Ibn.Web.UI.Controls.Util.CustomColumnBaseType" ClassName="Mediachase.UI.Web.Apps.TimeTracking.Primitives.ColumnsActions_Grid_TimeTrackingEntry_TT_MyGroupByWeekProject" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Hosting" %>
<%@ Import Namespace="System.Xml.XPath" %>
<%@ Import Namespace="Mediachase.Ibn.Core" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta.Management" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Services" %>
<%@ Import Namespace="Mediachase.Ibn.Web.UI" %>
<%@ Import Namespace="Mediachase.Ibn.Web.UI.Controls.Util" %>
<%@ Import Namespace="Mediachase.Ibn.Web.UI.WebControls" %>
<%@ Import Namespace="Mediachase.Ibn.XmlTools" %>
<%@ Import Namespace="Mediachase.IbnNext.TimeTracking" %>
<script language="c#" runat="server">
	protected string GetValue(MetaObject DataItem)
	{
		string retVal = "";
		if (DataItem != null)
			retVal = BindAction(DataItem);
		return retVal;
	}
	
	#region BindAction
	/// <summary>
	/// Binds the action.
	/// </summary>
	string BindAction(MetaObject DataItem)
	{
		string retVal = string.Empty;
		
		IXPathNavigable navigable = Mediachase.Ibn.XmlTools.XmlBuilder.GetXml(StructureType.MetaView, new Selector("TimeTrackingEntry", this.ViewName, this.Place));
		XPathNavigator actions = navigable.CreateNavigator().SelectSingleNode("MetaView/Grid/CustomColumns");

		foreach (XPathNavigator gridItem in actions.SelectChildren("Column", string.Empty))
		{
			string type = gridItem.GetAttribute("type", string.Empty);
			string id = gridItem.GetAttribute("id", string.Empty);

			if (type == "ColumnsActions" && id == this.ColumnId)
			{
				foreach (XPathNavigator actionItem in gridItem.SelectChildren("Item", string.Empty))
				{
					string imageUrl = actionItem.GetAttribute("imageUrl", string.Empty);
					string commandName = actionItem.GetAttribute("commandName", string.Empty);
					string paddingLeft = actionItem.GetAttribute("paddingLeft", string.Empty);
					string paddingRight = actionItem.GetAttribute("paddingRight", string.Empty);
					string tooltip = actionItem.GetAttribute("tooltip", string.Empty);

					Dictionary<string, string> dic = new Dictionary<string, string>();

					dic.Add("callFromGrid", "1");

					if (DataItem != null && DataItem.PrimaryKeyId == null && DataItem.Properties["ParentBlockId"].OriginalValue != null)	// Block
					{
						MetaObject ttb = MetaObjectActivator.CreateInstance<BusinessObject>(MetaDataWrapper.ResolveMetaClassByNameOrCardName("TimeTrackingBlock"), Convert.ToInt32(DataItem.Properties["ParentBlockId"].OriginalValue.ToString()));

						dic.Add("parentBlockId", DataItem.Properties["ParentBlockId"].OriginalValue.ToString());
						dic.Add("primaryKeyId", DataItem.Properties["ParentBlockId"].OriginalValue.ToString() + "::" + "TimeTrackingBlock");

						if (DataItem.Properties["BlockTypeInstanceId"].OriginalValue != null) // dla togo chtoby QuickAdd Control vybral nuzhnij block
							dic.Add("blockTypeInstanceId", DataItem.Properties["BlockTypeInstanceId"].OriginalValue.ToString());
					}
					else if (DataItem != null && DataItem.PrimaryKeyId != null)	// Entry
					{
						dic.Add("primaryKeyId", DataItem.PrimaryKeyId.ToString() + "::" + "TimeTrackingEntry");
					}

					CommandParameters cp = new CommandParameters(commandName, dic);
					bool isEnable = true;
					string clientScript = CommandManager.GetCurrent(this.Page).AddCommand("TimeTrackingEntry", this.ViewName, this.Place, cp, out isEnable);
					imageUrl = this.ResolveClientUrl(imageUrl);

					//create ImageButton control			
					ImageButton img = new ImageButton();
					img.ImageUrl = imageUrl;

					if (tooltip != string.Empty)
					{
						tooltip = CHelper.GetResFileString(tooltip);
						img.ToolTip = tooltip;
					}					
					
					img.OnClientClick = string.Format("{0}; return false;", clientScript);
					
					if (paddingRight != string.Empty)
						img.Style.Add(HtmlTextWriterStyle.PaddingRight, paddingRight + "px");
					
					if (paddingLeft != string.Empty)
						img.Style.Add(HtmlTextWriterStyle.PaddingLeft, paddingLeft + "px");

					//ImageButton control -> toString()
					StringBuilder sb = new StringBuilder();
					StringWriter tw = new StringWriter(sb);
					HtmlTextWriter hw = new HtmlTextWriter(tw);
					img.RenderControl(hw);
					//add image button
					if (isEnable)
						retVal += sb.ToString();
				}
			}
		}

		return retVal;
	}
	#endregion	
</script>
<%# GetValue(DataItem) %>