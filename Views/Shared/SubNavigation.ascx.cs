using System.Linq;
using EPiServer.Core;
using EPiServer.Filters;
using EpiserverSite.Business;
using System.Collections.Concurrent;
using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Globalization;

namespace EpiserverSite.Views.Shared
{
    /// <summary>
    /// Sub navigation menu, mainly for standard pages with a menu on the left-hand side
    /// </summary>
    public partial class SubNavigation : SiteUserControlBase
    {
        private ConcurrentDictionary<PageReference, bool> _hasChildrenlookup = new ConcurrentDictionary<PageReference, bool>();
        private IContentFilter _treeFilter = new FilterContentForVisitor();

        public SubNavigation()
        {}

        internal Injected<IContentLoader> ContentLoader { get; set; }

        protected PageReference TopPageLink
        {
            get { return CurrentPage.ContentLink.GetTopPage().ToPageReference(); }
        }

        /// <summary>
        /// Checks if the specified page has any children that should be visible in the menu
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected bool HasChildren(PageData page)
        {
            //Use lookup to avoid multiple expensive calls to this method fropm the markup
            return _hasChildrenlookup.GetOrAdd(page.PageLink, (pageLink) =>
                {
                    return ContentLoader.Service.GetChildren<PageData>(pageLink, ContentLanguage.PreferredCulture, 0, 10)
                            .Any(p => !_treeFilter.ShouldFilter(p) && p.VisibleInMenu);
                });
        }
    }
}
