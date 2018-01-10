using System;
using EpiserverSite.Business;
using EpiserverSite.Models.Pages;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;

namespace EpiserverSite.Views.Properties
{
    [TemplateDescriptor(TagString = "ContactBlock")]
    public partial class ContactDetailsByPageReference : PropertyControlBase<PageReference>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ContentReference.IsNullOrEmpty(CurrentData))
            {
                Contact = CurrentData.GetPage() as ContactPage;
                DataBind();
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
    }
}
