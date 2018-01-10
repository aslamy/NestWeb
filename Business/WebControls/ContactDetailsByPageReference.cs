using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EpiserverSite.Models.Pages;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.PropertyControls;
using EPiServer.Logging;

namespace EpiserverSite.Business.WebControls
{
    /// <summary>
    /// Used to allow a Property control to render contact details based on a PageReference property which specifies a ContactPage
    /// </summary>
    [TemplateDescriptor(TagString = Global.SiteUIHints.Contact)]
    public class ContactDetailsByPageReference : PropertyDataControl, IRenderTemplate<PageReference>
    {
        private static readonly ILogger _logger = LogManager.GetLogger();

        private void RetrieveContactDetails()
        {
            if (PropertyData == null)
            {
                return;
            }

            if (ContentReference.IsNullOrEmpty(PropertyData.Value as ContentReference))
            {
                _logger.Warning("Property data was empty, a PageReference value is required to retrieve contact details");
            }
            else
            {
                Contact = ((ContentReference)PropertyData.Value).GetPage() as ContactPage;
            }
        }

        /// <summary>
        /// Gets the contact page linked to this block
        /// </summary>
        protected ContactPage Contact { get; private set; }

        protected string Email { get { return Contact != null ? Contact.Email : "N/A"; } }
        protected string ImageUrl { get { return Contact != null ? Contact.Image.ToString() : ""; } }
        protected string ContactName { get { return Contact != null ? Contact.PageName : "No contact selected"; } }
        protected string Phone { get { return Contact != null ? Contact.Phone : "N/A"; } }

        public override void CreateDefaultControls()
        {
            RetrieveContactDetails();

            var localizationService = ServiceLocator.Current.GetInstance<LocalizationService>();

            var emailLabel = localizationService.GetString("/contact/email");

            var phoneLabel = localizationService.GetString("/contact/phone");

            var p = new HtmlGenericControl("p");

            p.Controls.Add(new Literal { Text = HttpContext.Current.Server.HtmlEncode(ContactName) });

            p.Controls.Add(new Literal { Text = string.Format("<br>{0}: {1}", phoneLabel, HttpContext.Current.Server.HtmlEncode(Phone)) });

            p.Controls.Add(new Literal { Text = string.Format("<br>{0}: ", HttpContext.Current.Server.HtmlEncode(emailLabel)) });

            p.Controls.Add(new HtmlAnchor { HRef = string.Concat("mailto:", HttpUtility.HtmlAttributeEncode(Email)), InnerText = HttpContext.Current.Server.HtmlEncode(Email)});

            Controls.Add(p);
        }

        public override void ApplyEditChanges()
        {
            // No action required, we use this control for rendering only
        }
        
        public override void CreateEditControls()
        {
            // No action required, we use this control for rendering only
        }
    }
}
