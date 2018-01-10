using System;
using System.Text;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.ServiceLocation;
using EpiserverSite.Models.Media;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Core;

namespace EpiserverSite.Views.Media
{
    [TemplateDescriptor(Inherited = true, TemplateTypeCategory = TemplateTypeCategories.UserControl)]
    public partial class VideoControl : ContentControlBase<VideoFile>
    {
        protected Injected<UrlResolver> UrlResolver { get; set; }

        /// <summary>
        /// Gets or sets the player configuration options. Serialized to a javascript object in markup.
        /// </summary>
        public object PlayerConfiguration { get; set; }

        /// <summary>
        /// Gets the virtual path to the local video player script
        /// </summary>
        public string ScriptUrl { get; set; }

        /// <summary>
        /// Gets the virtual path to the local video file.
        /// </summary>
        protected string VideoUrl
        {
            get
            {
                return UrlResolver.Service.GetUrl(CurrentData.ContentLink);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ScriptUrl = Page.ResolveUrl("~/Static/jwplayer/jwplayer.js");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // The player configuration is serialized in the markup to prevent special characters from breaking the script.
            PlayerConfiguration = new
            {
                // Path of the media to play
                file = VideoUrl,
                
                // The media type. 
                // Must be provided to prevent the player from trying to read the media stream as a 
                // playlist if it can't find a known file extension in the file property.
                type = "video",
                
                // Path to an initial preview image
                image = ContentReference.IsNullOrEmpty(CurrentData.PreviewImage) ? String.Empty : UrlResolver.Service.GetUrl(CurrentData.PreviewImage),
                
                // Width and height of the player
                width = "100%",
                height = "100%",
                
                modes = new object[] 
                { 
                    new { type = "html5" },
                    new { type = "flash", src = Page.ResolveUrl("~/Static/jwplayer/player.swf") }
                }
            };

            DataBind();
        }
    }
}
