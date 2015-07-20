using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Resources;

namespace Mediachase.UI.Web.Incidents
{
  public partial class ArticleView : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ApplyLocalization();
    }

    private void ApplyLocalization()
    {
      ResourceManager LocRM = new ResourceManager("Mediachase.UI.Web.App_GlobalResources.Common.Resources.strPageTitles", typeof(ArticleView).Assembly);
      pT.Title = LocRM.GetString("tArticleView");
    }
  } 
}
