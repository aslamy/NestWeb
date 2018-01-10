using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using EpiserverSite.Models.Pages;
using EPiServer;
using EPiServer.Web;

namespace EpiserverSite.Business
{
     /// <summary>
    /// Used for common content actions
    /// </summary>   
    public sealed class SiteDataFactory
    {
        private static SiteDataFactory _instance;
        private readonly IContentLoader _contentLoader;
        private readonly IPageCriteriaQueryService _pageCriteriaService;
        private readonly IContentProviderManager _contentProviderManager;

        public SiteDataFactory(IContentLoader contentLoader, IPageCriteriaQueryService pageCriteriaService, IContentProviderManager contentProviderManager)
        {
            _contentLoader = contentLoader;
            _pageCriteriaService = pageCriteriaService;
            _contentProviderManager = contentProviderManager;
        }

        public static SiteDataFactory Instance
        {
            get
            {
                return _instance ?? ( _instance = ServiceLocator.Current.GetInstance<SiteDataFactory>() );
            }
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// Returns all contact pages beneath the main contacts container
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContactPage> GetContactPages()
        {
            var contactsRootPageLink = _contentLoader.Get<StartPage>(SiteDefinition.Current.StartPage).ContactsPageLink;

            if (ContentReference.IsNullOrEmpty(contactsRootPageLink))
            {
                throw new ConfigurationErrorsException("No contact page root specified in site settings, unable to retrieve contact pages");
            }

            return _contentLoader.GetChildren<ContactPage>(contactsRootPageLink).OrderBy(p => p.PageName);
        }

        /// <summary>
        /// Returns pages of a specific page type
        /// </summary>
        /// <typeparam name="TPageData">The page type to filter by</typeparam>
        /// <param name="pageLink"></param>
        /// <param name="maxCount"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public IEnumerable<TPageData> FindPagesByPageType<TPageData>(PageReference pageLink, int maxCount, bool recursive) where TPageData : SitePageData
        {
            if (ContentReference.IsNullOrEmpty(pageLink))
            {
                throw new ArgumentNullException("pageLink", "No page link specified, unable to find pages");
            }

            if (maxCount < 1)
            {
                throw new ArgumentOutOfRangeException("maxCount", "A positive integer is required");
            }

            var pages = recursive
                        ? FindPagesByPageTypeRecursively<TPageData>(pageLink)
                        : _contentLoader.GetChildren<TPageData>(pageLink);

            return pages.Take(maxCount).ToArray(); // We return it as an array to allow databinding to a PageList control's DataSource property
        }

        /// <summary>
        /// Returns pages of a specific page type
        /// </summary>
        /// <param name="pageLink"></param>
        /// <param name="maxCount"></param>
        /// <param name="recursive"></param>
        /// <param name="pageTypeId">ID of the page type to filter by</param>
        /// <returns></returns>
        public IEnumerable<PageData> FindPagesByPageType(PageReference pageLink, int maxCount, bool recursive, int pageTypeId)
        {
            if (ContentReference.IsNullOrEmpty(pageLink))
            {
                throw new ArgumentNullException("pageLink", "No page link specified, unable to find pages");
            }

            if (maxCount < 1)
            {
                throw new ArgumentOutOfRangeException("maxCount", "A positive integer is required");
            }

            var pages = recursive
                        ? FindPagesByPageTypeRecursively(pageLink, pageTypeId)
                        : _contentLoader.GetChildren<PageData>(pageLink);

            return pages.Take(maxCount).ToArray(); // We return it as an array to allow databinding to a PageList control's DataSource property
        }

        // Type specified through generic type parameter
        private IEnumerable<TPageData> FindPagesByPageTypeRecursively<TPageData>(PageReference pageLink) where TPageData : SitePageData
        {
            var pageTypeId = typeof(TPageData).GetPageTypeId();

            var pages = FindPagesByPageTypeRecursively(pageLink, pageTypeId);

            return pages.Select(p => (TPageData)p);
        }

        // Type specified through page type ID
        private IEnumerable<PageData> FindPagesByPageTypeRecursively(PageReference pageLink, int pageTypeId)
        {
            var criteria = new PropertyCriteriaCollection
                               {
                                    new PropertyCriteria
                                    {
                                        Name = "PageTypeID",
                                        Type = PropertyDataType.PageType,
                                        Condition = CompareCondition.Equal,
                                        Value = pageTypeId.ToString()
                                    }
                               };

            // Include content providers serving content beneath the page link specified for the search
            
            if(_contentProviderManager.ProviderMap.CustomProvidersExist)
            {
                var contentProvider = _contentProviderManager.ProviderMap.GetProvider(pageLink);

                if (contentProvider.HasCapability(ContentProviderCapabilities.Search))
                {
                    criteria.Add(new PropertyCriteria
                                     {
                                         Name = "EPI:MultipleSearch",
                                         Value = contentProvider.ProviderKey
                                     });
                }
            }

            return _pageCriteriaService.FindPagesWithCriteria(pageLink, criteria);
        }
    }
}
