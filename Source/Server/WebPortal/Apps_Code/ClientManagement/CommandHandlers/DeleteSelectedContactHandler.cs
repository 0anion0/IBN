﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Mediachase.Ibn.Core.Business;
using Mediachase.Ibn.Data;
using Mediachase.Ibn.Web.UI.WebControls;
using Mediachase.UI.Web.Apps.MetaUI.Grid;
using Mediachase.Ibn.Clients;

namespace Mediachase.Ibn.Web.UI.ClientManagement.CommandHandlers
{
	public class DeleteSelectedContactHandler : ICommand
	{
		#region ICommand Members

		public void Invoke(object Sender, object Element)
		{
			if (Element is CommandParameters)
			{
				CommandParameters cp = (CommandParameters)Element;
				string[] selectedElements = EntityGrid.GetCheckedCollection(((CommandManager)Sender).Page, cp.CommandArguments["GridId"]);

				if (selectedElements != null)
				{
					string className = ContactEntity.GetAssignedMetaClassName();
					int errorCount = 0;

					foreach (string elem in selectedElements)
					{
						string id = elem.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[0];
						PrimaryKeyId key = PrimaryKeyId.Parse(id);

						using (TransactionScope tran = DataContext.Current.BeginTransaction())
						{
							bool wasError = false;
							try
							{
								BusinessManager.Delete(className, key);
							}
							catch (Exception ex)
							{
								wasError = true;
								CHelper.GenerateErrorReport(ex);
								errorCount++;
							}

							if (!wasError)
								tran.Commit();
						}
					}

					if (errorCount > 0)
						((CommandManager)Sender).InfoMessage = CHelper.GetResFileString("{IbnFramework.Common:NotAllSelectedItemsWereProcessed}");

					CHelper.RequireBindGrid();
				}
			}
		}

		#endregion
	}
}
