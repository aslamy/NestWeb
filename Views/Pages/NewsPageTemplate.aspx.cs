using System;
using EPiServer.Core;
using EpiserverSite.Models.Pages;

namespace EpiserverSite.Views.Pages
{
    public partial class NewsPageTemplate : SiteTemplatePage<NewsPage>
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            NoListRootMessage.DataBind();
        }
    }
}
