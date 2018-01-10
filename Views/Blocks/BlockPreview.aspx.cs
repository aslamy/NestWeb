using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EpiserverSite.Business.WebControls;
using EPiServer.Web;
using EPiServer.Web.WebControls;
using System;
using System.Linq;
using System.Collections.Generic;
using EPiServer;

namespace EpiserverSite.Views.Blocks
{
    /// <summary>
    /// Template used to preview a block using a generic rendering
    /// </summary>
    [TemplateDescriptor(Inherited = true, Tags = new[] { RenderingTags.Preview, RenderingTags.Edit })]
    public partial class BlockPreview : PreviewPage, IRenderTemplate<BlockData>
    {
        private Injected<TemplateResolver> _templateResolver;
        private Injected<DisplayOptions> _displayOptions;

        protected IEnumerable<PreviewArea> PreviewAreas { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PreviewAreas = _displayOptions.Service.Select(o => new PreviewArea()
            {
                AreaName = o.Name,
                AreaTag = o.Tag,
                Supported = _templateResolver.Service.HasTemplate(CurrentData, TemplateTypeCategories.WebFormsPartial, o.Tag)
            });

            previewRepeater.DataSource = PreviewAreas;

            this.DataBind();
        }

        /// <summary>
        /// Sets up a Property control to render contents using temporary property data
        /// </summary>
        /// <param name="propertyControl"></param>
        /// <param name="contents"></param>
        private void SetupPreviewPropertyControl(Property propertyControl, IEnumerable<IContent> contents)
        {
            // Define a content area
            var contentArea = new ContentArea();

            // Add the blocks to preview
            foreach (var content in contents)
            {
                contentArea.Items.Add(new ContentAreaItem { ContentLink = content.ContentLink });
            }

            // Create a temporary property for the content area
            var previewProperty = new PropertyContentArea { Value = contentArea, Name = "PreviewPropertyData", IsLanguageSpecific = true };

            // Render the temporary property using the Property control in the web form
            propertyControl.InnerProperty = previewProperty;
        }

        protected void previewRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            var propertyControl = e.Item.FindControl("propertyControl") as Property;
            var previewArea = (PreviewArea)e.Item.DataItem;

            propertyControl.RenderSettings.Tag = previewArea.AreaTag;
            propertyControl.Visible = previewArea.Supported;

            if (previewArea.Supported)
            {
                SetupPreviewPropertyControl(propertyControl, new[] { CurrentData });
            }
        }

        protected class PreviewArea
        {
            public bool Supported { get; set; }
            public string AreaName { get; set; }
            public string AreaTag { get; set; }
        }
    }
}
