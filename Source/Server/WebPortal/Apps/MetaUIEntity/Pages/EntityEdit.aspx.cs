﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Mediachase.Ibn.Web.UI.MetaUIEntity.Pages
{
	public partial class EntityEdit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(Request["ObjectId"]))
				pT.Title = GetGlobalResourceObject("IbnFramework.Common", "Editing").ToString();
			else
				pT.Title = GetGlobalResourceObject("IbnFramework.Common", "Creating").ToString();
		}
	}
}
