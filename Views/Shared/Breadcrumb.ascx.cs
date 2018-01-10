using System;
using System.Linq;
using EPiServer;
using EPiServer.Web;
using EPiServer.ServiceLocation;

namespace EpiserverSite.Views.Shared
{
    public partial class Breadcrumb : SiteUserControlBase
    {
        internal Injected<IContentLoader> ContentLoader { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Include all pages but the root page
            BreadcrumbList.DataSource = ContentLoader.Service.GetAncestors(CurrentPage.ContentLink)
                                                            .Where(p => !p.ContentLink.CompareToIgnoreWorkID(SiteDefinition.Current.RootPage))
                                                            .Reverse();

            BreadcrumbList.DataBind();
        }
    }
}
