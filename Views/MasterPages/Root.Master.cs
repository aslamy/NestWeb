using System.Web.UI;
namespace EpiserverSite.Views.MasterPages
{
    /// <summary>
    /// Defines the most basic parts of the site's HTML, such as doc type and linked resources
    /// </summary>
    public partial class Root : SiteMasterPage
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            Bundles.Controls.Add(new LiteralControl(System.Web.Optimization.Scripts.Render("~/bundles/js").ToHtmlString()));
            Bundles.Controls.Add(new LiteralControl(System.Web.Optimization.Styles.Render("~/bundles/css").ToHtmlString()));
        }
    }
}
