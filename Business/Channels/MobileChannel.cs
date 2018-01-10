using System.Web;
using EPiServer.Web;

namespace EpiserverSite.Business.Channels
{
    /// <summary>
    /// Defines the 'Mobile' content channel
    /// </summary>
    public class MobileChannel : DisplayChannel
    {
        public const string Tag = "mobile";

        public override string ChannelName
        {
            get
            {
                return Tag;
            }
        }

        public override string ResolutionId
        {
            get
            {
                return typeof(IphoneVerticalResolution).FullName;
            }
        }

        public override bool IsActive(HttpContextBase context)
        {
            return context.Request.Browser.IsMobileDevice;
        }
    }
}
