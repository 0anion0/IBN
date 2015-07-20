using System;
using System.Resources;
using System.Text;

using ComponentArt.Web.UI;
using Mediachase.Ibn.Web.UI.WebControls;

namespace Mediachase.UI.Web.Public
{
	/// <summary>
	/// Summary description for TermsOfUseRUS.
	/// </summary>
	public partial class TermsOfUseRUS : System.Web.UI.Page
	{
		protected ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.Home.Resources.strWorkspace", typeof(TermsOfUseRUS).Assembly);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			UtilHelper.RegisterCssStyleSheet(Page, "~/Styles/IbnFramework/windows.css");
			UtilHelper.RegisterCssStyleSheet(Page, "~/Styles/IbnFramework/Theme.css");
			UtilHelper.RegisterCssStyleSheet(Page, "~/Styles/IbnFramework/tabStyle.css");
			UtilHelper.RegisterCssStyleSheet(Page, "~/Styles/IbnFramework/public.css");

			UtilHelper.RegisterScript(Page, "~/Scripts/browser.js");
			UtilHelper.RegisterScript(Page, "~/Scripts/buttons.js");

			if (!IsPostBack)
			{
				TabStripTab tbt = new TabStripTab();
				tbt.Text = LocRM.GetString("tTrial");
				TabStrip1.Tabs.Add(tbt);
				tbt = new TabStripTab();
				tbt.Text = LocRM.GetString("tCommerce");
				TabStrip1.Tabs.Add(tbt);

				lblTrialVersion.InnerHtml = @"<p>� ��������� ������ �� ����������� ����� ������ ������������ �������� Instant Business Network, ���������� ��� ����������.</p>
								<p>�������� ���������� �� ����� ��������������� �� ���������� � ���������, ����������� ���� � ������� Instant Business Network 4.7.</p>
								<p>�������� ���������� �� ����������� ������ ����������������� ����� ������ ������� � �� ������������� ����������� ������������ ������������� ����� ������ Instant Business Network 4.7</p>
								<p>�� ��������� ����� �������� ����� ������ �� ������� ������� ����������� ������� ���� ���������� ������������ ������ Instant Business Network 4.7.</p>
								<p>�� �������� ������������ ������������ ������ Instant Business Network 4.7 �����������:</p>
								<p>�� ���������:</p>
								<ul><li>+7 (495) 648 61 62 (������)</li><li>+7 (4012) 36 85 98 (�����������)</li></ul>
								<p>�� ����������� �����:<br/>
								<a href='mailto:sales@mediachase.ru'>sales@mediachase.ru</a></p>
								<p>����� ��� ����� ��������:<br/>
								<a href='http://www.pmbox.ru' target='_blank'>www.mediachase.ru</a></p>";

				lblBillableVersion.InnerHtml = @"<p>� ��������� ������ �� ����������� ������������ ������ ������������ �������� Instant Business Network, ���������� ��� ����������.</p>
								<p>������������� ������������ ������ ������� �������� ��� ���������� ������������� ���������� ������������ ���������������� ���������� ���������.</p>
								<p>����� ������������� ���������� �� ������ ��������� � ����� �������� ��� ��������� �� ����������� ���� ���������.</p>
								<p>�� ���� ��������, ��������� � �������������� Instant Business Network 4.7, �����������:</p>
								<p>�� ���������:</p>
								<ul><li>+7 (495) 648 61 62 (������)</li><li>+7 (4012) 36 85 98 (�����������)</li></ul>
								<p>�� ����������� �����:<br/>
								<a href='mailto:sales@mediachase.ru'>sales@mediachase.ru</a><br/>
								<a href='mailto:info@mediachase.ru'>info@mediachase.ru</a></p>
								<p>����� ��� ����� ��������:<br/>
								<a href='http://www.pmbox.ru' target='_blank'>www.mediachase.ru</a></p>";
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
