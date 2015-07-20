<%@ Control Language="C#" AutoEventWireup="true" ClassName="Mediachase.Ibn.Web.UI.TimeTracking.Primitives.Text_Grid_TimeTrackingEntry_Title_TT_ManagerGroupByProjectUser" Inherits="Mediachase.Ibn.Web.UI.Controls.Util.BaseType" %>
<%@ Import Namespace="Mediachase.Ibn.Core" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta" %>
<%@ Import Namespace="Mediachase.Ibn.Data" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta.Management" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Services" %>
<%@ Import Namespace="Mediachase.Ibn.Web.UI.Controls.Util" %>
<%@ Import Namespace="Mediachase.Ibn.Web.UI.WebControls" %>
<script language="c#" runat="server">
    protected string GetValue(MetaObject DataItem, string FieldName)
    {
		string retVal = "";
        int paddingValue = 0;
        
        if (DataItem != null && DataItem.Properties[FieldName].Value != null)
		{
			string metaClassName = DataItem.GetMetaType().Name;
			string titleText;

			if (DataItem.Properties[FieldName].Value != null)
				titleText = DataItem.Properties[FieldName].Value.ToString();
			else
				titleText = string.Empty;

			int id = DataItem.PrimaryKeyId ?? -1;
			//string sUrl = CHelper.GetAbsolutePath(String.Format("/IbnNext/TestFolder/ObjectView.aspx?ClassName={0}&ObjectId={1}", metaClassName, id));
			//string baseText = String.Format("<a href='{0}'>{1}</a>", sUrl, titleText);
			string baseText = titleText;


			if (DataItem.PrimaryKeyId == null && DataItem.Properties["MetaViewGroupByType"] != null)
			{
				if (DataItem.Properties["MetaViewGroupByType"].OriginalValue != null)
				{
					if (DataItem.Properties["MetaViewGroupByType"].OriginalValue.ToString() == MetaViewGroupByType.Primary.ToString())
					{
						baseText = String.Format("<span>{0}</span>", titleText);
					}
					else if (DataItem.Properties["MetaViewGroupByType"].OriginalValue.ToString() == MetaViewGroupByType.Secondary.ToString())
					{
						string sReferencedClass = DataItem.Properties["OwnerId"].GetMetaType().Attributes[McDataTypeAttribute.ReferenceMetaClassName].ToString();
						MetaClass mc = MetaDataWrapper.GetMetaClassByName(sReferencedClass);
						MetaObject obj = MetaObjectActivator.CreateInstance(mc, int.Parse(DataItem.Properties["MetaViewGroupByKey"].OriginalValue.ToString()));
						baseText = obj.Properties[mc.TitleFieldName].Value.ToString();
						
						paddingValue = 25;
					}
				}
			}


			if (DataItem.PrimaryKeyId == null && DataItem.Properties["MetaViewGroupByType"] != null)
			{
				if (CHelper.GetFromContext("MetaViewName") == null)
					throw new ArgumentException("MetaViewName @ Context");

				MetaView mv = Mediachase.Ibn.Data.DataContext.Current.MetaModel.MetaViews[(string)CHelper.GetFromContext("MetaViewName")];
				if (mv == null)
					throw new ArgumentNullException(String.Format("MetaViewName: {0}", CHelper.GetFromContext("MetaViewName")));

				McMetaViewPreference mvPref = Mediachase.Ibn.Core.UserMetaViewPreference.Load(mv, Security.CurrentUserId);
				
				string imgPath = string.Empty;
				bool isPlus = false;
				if (DataItem.Properties["MetaViewGroupByType"].OriginalValue.ToString() == MetaViewGroupByType.Primary.ToString())
					isPlus = MetaViewGroupUtil.IsCollapsed(MetaViewGroupByType.Primary, mvPref, MetaViewGroupUtil.GetUniqueKey(DataItem));
				else if (DataItem.Properties["MetaViewGroupByType"].OriginalValue.ToString() == MetaViewGroupByType.Secondary.ToString())
					isPlus = MetaViewGroupUtil.IsCollapsed(MetaViewGroupByType.Secondary, mvPref, MetaViewGroupUtil.GetUniqueKey(DataItem));

				if (isPlus)
				{
					imgPath = CHelper.GetAbsolutePath("/Images/IbnFramework/plus9x9.gif");
				}
				else
				{
					imgPath = CHelper.GetAbsolutePath("/Images/IbnFramework/minus9x9.gif");
				}

				if (DataItem.Properties["MetaViewGroupByType"].OriginalValue.ToString() != MetaViewGroupByType.Total.ToString())
				{
					System.Collections.Generic.Dictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string>();
					dic.Add("primaryKeyId", MetaViewGroupUtil.GetUniqueKey(DataItem));
					dic.Add("groupType", DataItem.Properties["MetaViewGroupByType"].OriginalValue.ToString());
					string actionCollapseScript = CommandManager.GetCurrent(this.Page).AddCommand(metaClassName, "TT_MyGroupByWeekProject", string.Empty, new CommandParameters("MC_TimeSheet_CollapseExpandBlock", dic));
					actionCollapseScript = actionCollapseScript.Replace("\"", "&quot;");
					
					baseText = String.Format("<a href='#' onclick=\"{3}\"><img border='0' src='{0}' /></a>&nbsp;{2} ", imgPath, MetaViewGroupUtil.GetUniqueKey(DataItem), baseText, actionCollapseScript);
				}
			}
			else
			{
				paddingValue = 45;
				if (DataItem.Properties["ObjectId"].OriginalValue != null && DataItem.Properties["ObjectTypeId"].OriginalValue != null
				&& int.Parse(DataItem.Properties["ObjectId"].OriginalValue.ToString()) != 0 && int.Parse(DataItem.Properties["ObjectTypeId"].OriginalValue.ToString()) != 0)
				{
					baseText = String.Format("<a href='{0}'>{1}</a> ", CHelper.GetObjectLink(Convert.ToInt32(DataItem.Properties["ObjectTypeId"].OriginalValue), Convert.ToInt32(DataItem.Properties["ObjectId"].OriginalValue)), baseText);
				}				
			}

            retVal = String.Format("<span style='padding-left: 0px'>{1}</span>", paddingValue, baseText);
        }
        return String.Format("<div style='padding-left: {1}px;'>{0}</div>", retVal, paddingValue + 5);
    }
</script>
<%# (DataItem == null) ? "null" : GetValue(DataItem, FieldName)%>
