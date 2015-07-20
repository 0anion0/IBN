﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediachase.Ibn.Web.UI.Apps.WidgetEngine.Pages
{
	public partial class CustomPageDesign : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			pT.Title = GetGlobalResourceObject("IbnFramework.Profile", "PageCustomization").ToString();
		}
	}
}
