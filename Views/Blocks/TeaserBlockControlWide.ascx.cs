using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EpiserverSite.Models.Blocks;

namespace EpiserverSite.Views.Blocks
{
    [TemplateDescriptor(Inherited = true, TemplateTypeCategory = TemplateTypeCategories.UserControl, Tags = new[] { Global.ContentAreaTags.FullWidth, Global.ContentAreaTags.TwoThirdsWidth })]
    public partial class TeaserBlockControlWide : SiteBlockControlBase<TeaserBlock>
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            DataBind(); // Since we use data binding expressions for the Property controls' Visible property
        }
    }
}
