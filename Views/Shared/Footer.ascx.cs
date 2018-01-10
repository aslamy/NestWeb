using System;
using System.Web.Security;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace EpiserverSite.Views.Shared
{
    public partial class Footer : SiteUserControlBase
    {
        private ContentReference _startPageReference;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBind();
        }

        /// <summary>
        /// Gets the start page reference
        /// </summary>
        protected ContentReference StartPageReference
        {
            get
            {
                if (ContentReference.IsNullOrEmpty(_startPageReference)) {
                    _startPageReference = CurrentPage.ContentLink.CompareToIgnoreWorkID(SiteDefinition.Current.StartPage) ? CurrentPage.ContentLink : SiteDefinition.Current.StartPage;
                }
                return _startPageReference;
            }
        }

        /// <summary>
        /// Gets the resolved URL for the current page.
        /// </summary>
        protected string GetCurrentPageUrl()
        {
            return Locate.Advanced.GetInstance<UrlResolver>().GetUrl(CurrentPage.ContentLink);
        }
    
        protected void Logout(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect(CurrentPage.LinkURL); // To have the page reload without the authentication cookie, causing the "Log in" link to re-appear
        }
    }
}
