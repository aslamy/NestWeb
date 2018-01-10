using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EpiserverSite.Business;
using EpiserverSite.Views.Blocks;

namespace EpiserverSite.Business.WebControls
{
    /// <summary>
    /// A web control that renders a hyperlink to a content item when not in block preview mode.
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class TeaserLink : WebControl
    {
        /// <summary>
        /// Gets or sets the link to the content item.
        /// </summary>
        public virtual PageReference ContentLink { get; set; }

        /// <remarks>Overriden to return an A tag so that a hyperlink is rendered.</remarks>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.A; }
        }

        /// <remarks>Overriden to add the href attribute to the beginning tag.</remarks>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Href, ContentLink.GetPage().LinkURL);
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer but only if there is a content link available
        /// or if the current page is not the block preview page.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (Page is BlockPreview || ContentReference.IsNullOrEmpty(ContentLink))
            {
                return;
            }

            base.RenderBeginTag(writer);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer but only if there is a content link available
        /// or if the current page is not the block preview page.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (Page is BlockPreview || ContentReference.IsNullOrEmpty(ContentLink))
            {
                return;
            }

            base.RenderEndTag(writer);
        }
    }
}
