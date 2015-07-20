﻿<%@ Control Language="C#" AutoEventWireup="true" ClassName="Mediachase.Ibn.Web.UI.MetaUI.Primitives.Integer_Grid" Inherits="Mediachase.Ibn.Web.UI.Controls.Util.BaseType" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta" %>
<script language="c#" runat="server">
	protected string GetValue(MetaObject DataItem, string FieldName)
	{
		string retVal = String.Empty;
		
		if (DataItem != null)
		{
			MetaObjectProperty property = DataItem.Properties[FieldName];
			if (property != null && property.Value != null)
			{
				retVal = property.Value.ToString();
			}
		}
		return retVal;
	}
</script>
<div style="float:right;"><%# GetValue(DataItem, FieldName) %></div>
