using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EpiserverSite.Models.Pages;
using EPiServer.Web;

namespace EpiserverSite.Views.Pages.Partials
{
    /// <summary>
    /// Renders a product page teaser when a product page is dropped in a content area
    /// </summary>
    [TemplateDescriptor(Inherited = true, TemplateTypeCategory = TemplateTypeCategories.UserControl, 
        Tags = new[] { Global.ContentAreaTags.TwoThirdsWidth, Global.ContentAreaTags.FullWidth })]
    public partial class PagePartialTemplateWide : PartialPageTemplate<SitePageData>
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            DataBind();
        }
    }
}
