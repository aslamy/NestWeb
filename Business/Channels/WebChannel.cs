using System.Web;
using EPiServer.Web;

namespace EpiserverSite.Business.Channels
{
    /// <summary>
    /// Defines the 'Web' content channel
    /// </summary>
    public class WebChannel : DisplayChannel
    {
        public const string Tag = "web";

        public override string ChannelName
        {
            get
            {
                return Tag;
            }
        }

        public override bool IsActive(HttpContextBase context)
        {
            return !context.Request.Browser.IsMobileDevice;
        }

    }
}
