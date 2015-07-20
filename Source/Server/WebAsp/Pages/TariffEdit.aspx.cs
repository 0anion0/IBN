﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;

namespace Mediachase.Ibn.WebAsp.Pages
{
	public partial class TariffEdit : System.Web.UI.Page
	{
		protected ResourceManager LocRM = new ResourceManager("Mediachase.Ibn.WebAsp.App_GlobalResources.Resources.Tariffs", Assembly.GetExecutingAssembly());
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["TariffId"] != null)
				pageTemplate.Title = LocRM.GetString("TariffEdit");
			else
				pageTemplate.Title = LocRM.GetString("TariffCreate");
		}
	}
}
