<%@ Reference Control="~/Modules/BlockHeaderLightWithMenu.ascx" %>
<%@ Control Language="c#" Inherits="Mediachase.UI.Web.Tasks.Modules.TasksToDo" Codebehind="TasksToDo.ascx.cs" %>
<%@ register TagPrefix="dg" namespace="Mediachase.UI.Web.Modules.DGExtension" Assembly="Mediachase.UI.Web" %>
<%@ Register TagPrefix="ibn" TagName="BlockHeader" src="../../Modules/BlockHeaderLightWithMenu.ascx" %>
<script language="javascript">
function DeleteToDo(ToDoID)
{
	document.forms[0].<%=hdnID.ClientID %>.value = ToDoID;
	if(confirm('<%=LocRM.GetString("WarningToDo")%>'))
		<%=Page.ClientScript.GetPostBackEventReference(lbDeleteToDoAll,"") %>
}
</script>
<table cellspacing="0" cellpadding="0" width="100%" border="0" style="margin-top:5px">
	<tr>
		<td><ibn:blockheader id="Migrated_tbToDo" runat="server"></ibn:blockheader></td>
	</tr>
</table>
<table cellspacing="0" cellpadding="5" width="100%" border="0" class="ibn-stylebox-light">
	<tr>
		<td>
			<dg:datagridextended id="dgToDo" runat="server" width="100%" autogeneratecolumns="False" borderwidth="0px" CellSpacing="3" gridlines="None" cellpadding="1" allowsorting="True" allowpaging="False" ShowHeader="True">
				<HeaderStyle CssClass="ibn-vh2"></HeaderStyle>
				<ItemStyle CssClass="ibn-propertysheet"></ItemStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ToDoId"></asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Title" HeaderText="Title">
						<ItemTemplate>
							<a href='../ToDo/ToDoView.aspx?ToDoId=<%# DataBinder.Eval(Container.DataItem, "ToDoId")%>'>
								<%# DataBinder.Eval(Container.DataItem, "Title")%>
							</a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="PercentCompleted" HeaderText="PercentCompleted">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "PercentCompleted")%>%</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="ReasonName">
						<ItemTemplate>
							<%# CompletionString(
								DataBinder.Eval(Container.DataItem, "ReasonId"),
								DataBinder.Eval(Container.DataItem, "CompletedBy"))
							%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<HeaderStyle HorizontalAlign="Right" Width="55px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right" Width="55px"></ItemStyle>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0" width="100%">
								<tr>
									<td width="50%">
										<asp:HyperLink ImageUrl = "../../layouts/images/Edit.GIF" NavigateUrl='<%# "~/ToDo/ToDoEdit.aspx?ToDoID=" + DataBinder.Eval(Container.DataItem, "ToDoId").ToString() %>' Runat="server" ID="Hyperlink1">
										</asp:HyperLink>
									</td>
									<td>
										<asp:HyperLink runat="server" imageurl="../../layouts/images/DELETE.GIF" NavigateUrl='<%# "javascript:DeleteToDo("+DataBinder.Eval(Container.DataItem, "ToDoId").ToString() +")" %>' ID="Hyperlink2" NAME="Hyperlink2">
										</asp:HyperLink>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
			</dg:datagridextended>
		</td>
	</tr>
</table>
<input type="hidden" id="hdnID" runat="server" NAME="hdnID">
<asp:LinkButton id="lbDeleteToDoAll" runat="server" Visible="False" onclick="lbDeleteToDoAll_Click"></asp:LinkButton>