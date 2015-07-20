﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reference.Edit.ascx.cs" Inherits="Mediachase.Ibn.Web.UI.MetaUI.Primitives.Reference_Edit" %>
<%@ Reference Control="~/Apps/MetaUIEntity/Modules/EntityDropDown.ascx" %>
<%@ Register TagPrefix="mc" TagName="EntityDD" Src="~/Apps/MetaUIEntity/Modules/EntityDropDown.ascx" %>
<asp:UpdatePanel runat="server" ID="ReferenceUpdatePanel">
<ContentTemplate>
	<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ibn-propertysheet">
		<tr>
			<td>
				<div style="border: 2px inset; height:20px; overflow-y: auto; padding-top:1px; margin-top:1px; padding-left:3px;">
					<asp:Label runat="server" ID="lblReference"></asp:Label>
				</div>
			</td>
			<td style="width:20px;" runat="server" id="tdSelect">
				<asp:ImageButton Runat="server" ToolTip="<%$Resources: IbnFramework.GlobalMetaInfo, Select %>" Width="16" Height="16" ID="ibSelect" ImageAlign="AbsMiddle" ImageUrl="~/Images/IbnFramework/search.gif" TabIndex="-1"></asp:ImageButton> 
			</td>
			<td style="width:20px;" runat="server" id="tdClear">
				<asp:ImageButton Runat="server" ToolTip="<%$Resources: IbnFramework.GlobalMetaInfo, Clear %>" Width="16" Height="16" ID="ibClear" ImageAlign="AbsMiddle" OnClick="ibClear_Click" CausesValidation="false" ImageUrl="~/Images/IbnFramework/delete.gif" TabIndex="-1"></asp:ImageButton> 
			</td>
			<td style="width:20px;">
				<asp:CustomValidator runat="server" ID="vldCustom" ErrorMessage="*" Display="dynamic" OnServerValidate="vldCustom_ServerValidate"></asp:CustomValidator>
			</td>
		</tr>
	</table>
	<asp:Button id="btnRefresh" runat="server" CausesValidation="False" style="display:none;" OnClick="btnRefresh_Click"></asp:Button>
</ContentTemplate>
</asp:UpdatePanel>
<table id="tblEntity" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%" class="ibn-propertysheet">
	<tr>
		<td>
			<mc:EntityDD ID="refObjects" ItemCount="6" runat="server" Width="100%" />
		</td>
		<td style="width:20px;">
			<asp:CustomValidator runat="server" ID="vldCustomEntity" ErrorMessage="*" Display="dynamic" OnServerValidate="vldCustom_ServerValidate"></asp:CustomValidator>
		</td>
	</tr>
</table>