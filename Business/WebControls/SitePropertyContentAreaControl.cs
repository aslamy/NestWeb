using System;
using EPiServer;
using EPiServer.Core;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Web.PropertyControls;
using EPiServer.Web.WebControls;

namespace EpiserverSite.Business.WebControls
{
    /// <summary>
    /// Provides custom rendering of content areas to add custom css classes based on render settings and content type.
    /// </summary>
    public class SitePropertyContentAreaControl : PropertyContentAreaControl
    {
        /// <summary>
        /// Overridden to add custom classes to the blocks elements inside a content area.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // TODO: Try to remove extra container created by PropertyContentAreaControl
            var container = Controls.Count > 0 ? Controls[0] : null;
            if (container == null)
            {
                return;
            }

            foreach (var control in container.Controls)
            {
                var webControl = control as WebControl;
                if (webControl != null)
                {
                    webControl.CssClass = String.Join(" ", GetClasses(webControl));
                    continue;
                }
                var htmlControl = control as HtmlControl;
                if (htmlControl != null)
                {
                    htmlControl.Attributes.Add("class", String.Join(" ", GetClasses(htmlControl)));
                }
            }
        }


        /// <summary>
        /// Assemble a list of css class names for content inside a content area.
        /// </summary>
        /// <param name="control">The control containing the content.</param>
        private IEnumerable<string> GetClasses(Control control)
        {
            var classes = new List<string>() {
                "block",
            };

            var renderer = control as ContentRenderer;
            if (renderer != null)
            {
                var content = renderer.ContentAreaItem.GetContent();
                if (content != null)
                {
                    classes.Add(content.GetOriginalType().Name.ToLowerInvariant());
                    classes.Add(renderer.Tag);
                    classes.Add(GetCssClassForTag(renderer.Tag));
                }
            }

            if (PropertyIsEditableForCurrentLanguage())
            {
                classes.Add("clearfix");
            }

            return classes;
        }

        /// <summary>
        /// Gets a CSS class used for styling based on a tag name (ie a Bootstrap class name)
        /// </summary>
        /// <param name="tagName">Any tag name available, see <see cref="Global.ContentAreaTags"/></param>
        private static string GetCssClassForTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return "";
            }
            switch (tagName.ToLower())
            {
                case "span12":
                    return "full";
                case "span8":
                    return "wide";
                case "span6":
                    return "half";
                default:
                    return string.Empty;
            }
        }
    }
}
