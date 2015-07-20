using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mediachase.Ibn.Web.UI.WebControls;

namespace Mediachase.Ibn.Web.UI.Shell.CommandHandlers
{
	public class ManagementCenterHandler : ICommandEnableHandler
	{
		#region ICommandEnableHandler Members

		public bool IsEnable(object Sender, object Element)
		{
			if (Mediachase.IBN.Business.Security.IsUserInGroup(Mediachase.IBN.Business.InternalSecureGroups.ProjectManager) ||
								Mediachase.IBN.Business.Security.IsUserInGroup(Mediachase.IBN.Business.InternalSecureGroups.ExecutiveManager) ||
								Mediachase.IBN.Business.Security.IsUserInGroup(Mediachase.IBN.Business.InternalSecureGroups.TimeManager))
				return true;
			else
				return false;
		}

		#endregion
	}
}
