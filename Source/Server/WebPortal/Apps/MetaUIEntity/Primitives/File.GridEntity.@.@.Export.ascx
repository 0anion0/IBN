﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mediachase.Ibn.Web.UI.Controls.Util.BaseEntityType" ClassName="Mediachase.Ibn.Web.UI.MetaUI.EntityPrimitives.File_GridEntity_All_All_Export"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta" %>
<%@ Import Namespace="Mediachase.Ibn.Core.Business" %>
<script language="c#" runat="server">
	protected string GetValue(EntityObject DataItem, string FieldName)
	{
		string retVal = String.Empty;

		if (DataItem != null && DataItem.Properties[FieldName] != null && DataItem[FieldName] != null) 
		{
			FileInfo fi = (FileInfo)DataItem[FieldName];
			retVal = fi.Name;
		}
		return retVal;
	}
</script>
<%# GetValue(DataItem, FieldName) %>