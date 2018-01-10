using System;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.ServiceLocation;
using EpiserverSite.Models.Media;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace EpiserverSite.Views.Media
{
    [TemplateDescriptor(Inherited = true, TemplateTypeCategory = TemplateTypeCategories.UserControl)]
    public partial class ImageControl : ContentControlBase<ImageFile>
    {
        protected Injected<UrlResolver> UrlResolver { get; set; }

        /// <summary>
        /// Gets the virtual path to the local image file.
        /// </summary>
        protected string ImageUrl
        {
            get
            {
                return UrlResolver.Service.GetUrl(CurrentData.ContentLink);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataBind();
        }
    }
}
