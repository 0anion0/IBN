﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mediachase.Ibn.Web.UI.Controls.Util.BaseType" ClassName="Mediachase.Ibn.Web.UI.Security.Primitives.Role.View" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Meta" %>
<%@ Import Namespace="Mediachase.Ibn.Data.Services" %>
<script language="c#" runat="server">
	protected string GetValue(MetaObject DataItem, string FieldName)
	{
		string retVal = "";

		if (DataItem != null && DataItem.Properties[FieldName].Value != null && DataItem.Properties[FieldName].Value is Mediachase.Ibn.Data.Services.RolePrincipal)
		{
			Mediachase.Ibn.Data.Services.RolePrincipal srp = (Mediachase.Ibn.Data.Services.RolePrincipal)DataItem.Properties[FieldName].Value;
			retVal = srp.Principal;
		}
		return retVal;
	}
</script>
<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ibn-propertysheet">
	<tr>
		<td valign="top">
			<%# GetValue(DataItem, FieldName) %>
		</td>
	</tr>
</table>