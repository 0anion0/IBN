<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObjectDropDownNew.ascx.cs" Inherits="Mediachase.UI.Web.Modules.ObjectDropDownNew" %>
<table width="250px" cellpadding="0" cellspacing="0" border="0" class="ibn-propertysheet" style="table-layout: fixed" runat="server" id="tblMain">
	<tr>
		<td id="tdValue" runat="server"><div class="dropstyle"><asp:Label runat="server" ID="lblSelected"></asp:Label></div></td>
		<td class="btndown" id="tdChange" runat="server"><asp:Label ID="lblChange" runat="server"></asp:Label></td>
	</tr>
</table>
<div id="divDropDown" runat="server" style="position: absolute; top: 30px; left: 100px; z-index: 10000; padding: 0px; display: none; border: 1px solid #95b7f3;" class="ibn-rtetoolbarmenu ibn-propertysheet ibn-selectedtitle">
	<table cellpadding="0" cellspacing="0" border="0" id="tableDD" runat="server" class="text"></table>
</div>
<input type="hidden" runat="server" id="hidTypes" />
<input type="hidden" runat="server" id="hidSelId" />
<input type="hidden" runat="server" id="hidSelType" />