using System;
using System.Globalization;
using System.Net;
using System.Web;
using EPiServer;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.Find.Framework.Statistics;
using EPiServer.Find.Helpers.Text;
using EPiServer.Find.UI;
using EPiServer.Find.UnifiedSearch;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EpiserverSite.Models.Pages;
using EPiServer.Globalization;
using EpiserverSite.Views.Pages;
using EPiServer.Web;
using EPiServer.Find.Cms;

namespace EpiserverSite.Views.Pages
{
    /// <summary>
    /// Presents a page used to search the website using EPiServer Find search features
    /// </summary>
    public partial class FindSearchPageTemplate : SiteTemplatePage<FindSearchPage>
    {
        /// <summary>
        /// Number of page
        /// </summary>
        public int PagingPage { get; set; }

        /// <summary>
        /// Calculate the number of pages required to list results
        /// </summary>
        public int TotalPagingPages
        {
            get
            {
                if (CurrentPage.PageSize > 0)
                {
                    return 1 + (Hits.TotalMatching - 1) / CurrentPage.PageSize;
                }
                return 0;
            }
        }

        /// <summary>
        /// Number of matching hits
        /// </summary>
        public string NumberOfHits { get; set; }

        /// <summary>
        /// User query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Search hits
        /// </summary>
        public UnifiedSearchResults Hits { get; set; }

        /// <summary>
        /// Current active section filter
        /// </summary>
        public string SectionFilter
        {
            get { return HttpContext.Current.Request.QueryString["t"] ?? string.Empty; }
        }

        /// <summary>
        /// Public proxy path mainly used for constructing url's in javascript
        /// </summary>
        public string PublicProxyPath { get; set; }

        /// <summary>
        /// Search tags like language and site
        /// </summary>
        public string Tags
        {
            get { return string.Join(",", ServiceLocator.Current.GetInstance<IStatisticTagsHelper>().GetTags()); }
        }

        /// <summary>
        /// Flag to indicate if both Find serviceUrl and defaultIndex are configured
        /// </summary>
        public bool IsConfigured { get; set; }

        /// <summary>
        /// Flag retrieved from editor settings to determine if it should 
        /// use AND as the operator for multiple search terms
        /// </summary>
        public bool UseAndForMultipleSearchTerms
        {
            get { return CurrentPage.UseAndForMultipleSearchTerms; }
        }
		
		/// <summary>
        /// Height of hit images
        /// </summary>
        public int HitImagesHeight 
		{ 
			get { return CurrentPage.HitImagesHeight; } 
		}

        /// <summary>
        /// When the page is loaded a search is carried out based on the 'q' query string parameter
        /// </summary>
        protected override void OnLoad(EventArgs eventArgs)
        {
            PublicProxyPath = ServiceLocator.Current.GetInstance<IFindUIConfiguration>().AbsolutePublicProxyPath();
            
            //detect if serviceUrl and/or defaultIndex is configured.
            IsConfigured = SearchIndexIsConfigured(EPiServer.Find.Configuration.GetConfiguration());

            if (IsPostBack)
            {
                Query = srchTxt.Text;
                PagingPage = 1;
            }
            else
            {
                Query = Request.Params["q"];
                srchTxt.Text = Query;
                PagingPage = Request.Params["p"].IsNotNullOrEmpty() ? int.Parse(Request.Params["p"]) : 1;
            }

            if (IsConfigured && !string.IsNullOrWhiteSpace(Query))
            {
                var query = BuildQuery();

                //Create a hit specification to determine display based on values entered by an editor on the search page.
                var hitSpec = new HitSpecification
                {
                    HighlightTitle = CurrentPage.HighlightTitles,
                    HighlightExcerpt = CurrentPage.HighlightExcerpts,
					ExcerptLength = CurrentPage.ExcerptLength
                };

                try
                {
                    Hits = query.GetResult(hitSpec);
                }
                catch (WebException wex)
                {
                    IsConfigured = wex.Status != WebExceptionStatus.NameResolutionFailure;
                }

                DisplaySearchResult();                
            }

            // Databind for translations
            SearchButton.DataBind();

            RequireClientResources();
        }

        private ITypeSearch<ISearchContent> BuildQuery()
        {
            var queryFor = SearchClient.Instance.UnifiedSearch().For(Query);

            if (UseAndForMultipleSearchTerms)
            {
                queryFor = queryFor.WithAndAsDefaultOperator();
            }

            var query = queryFor
                .UsingSynonyms()
                .UsingAutoBoost(TimeSpan.FromDays(30))
                //Include a facet whose value we can use to show the total number of hits
                //regardless of section. The filter here is irrelevant but should match *everything*.
                .TermsFacetFor(x => x.SearchSection)
                .FilterFacet("AllSections", x => x.SearchSection.Exists())
                //Fetch the specific paging page.
                .Skip((PagingPage - 1)*CurrentPage.PageSize)
                .Take(CurrentPage.PageSize)
                //Allow editors (from the Find/Optimizations view) to push specific hits to the top 
                //for certain search phrases.
                .ApplyBestBets();

            // obey DNT
            var doNotTrackHeader = System.Web.HttpContext.Current.Request.Headers.Get("DNT");
            // Should Not track when value equals 1
            if (doNotTrackHeader == null || doNotTrackHeader.Equals("0"))
            {
                query = query.Track();
            }

            //If a section filter exists (in the query string) we apply
            //a filter to only show hits from a given section.
            if (!string.IsNullOrWhiteSpace(SectionFilter))
            {
                query = query.FilterHits(x => x.SearchSection.Match(SectionFilter));
            }
            return query;
        }

        private void DisplaySearchResult()
        {
            if (Hits != null)
            {
                var numberOfHits = Hits.TotalMatching;
                NumberOfHits = numberOfHits == 0
                    ? Translate("/searchpagetemplate/zero")
                    : numberOfHits.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Handle click of search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchClick(object sender, EventArgs e)
        {
            Response.Redirect(UriSupport.AddQueryString(CurrentPage.LinkURL, "q", Server.UrlEncode(srchTxt.Text.Trim())));
        }

        /// <summary>
        /// Returns url for specified section facet
        /// </summary>
        /// <param name="groupName">Name of section</param>
        /// <returns>String url for specified section facet</returns>
        public string GetSectionGroupUrl(string groupName)
        {
            return UriSupport.AddQueryString(RemoveQueryStringByKey(HttpContext.Current.Request.Url.AbsoluteUri,"p"), "t", HttpContext.Current.Server.UrlEncode(groupName));
        }

        /// <summary>
        /// Create URL for a specific paging page.
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <returns>String url for specific page</returns>
        public string GetPagingUrl(int pageNumber)
        {
            return UriSupport.AddQueryString(HttpContext.Current.Request.RawUrl, "p", pageNumber.ToString());
        }

        /// <summary>
        /// Retrieve the paging section from the query string parameter "ps".
        /// If no parameter exists, default to the first paging section.
        /// </summary>
        public int PagingSection
        {
            get
            {
                return 1 + (PagingPage - 1) / PagingSectionSize;
            }
        }

        public int PagingSectionSize
        {
            get { return 10; }
        }

        /// <summary>
        /// Calculate the number of paging sections required to list page links
        /// </summary>
        public int TotalPagingSections
        {
            get
            {
                return 1 + (TotalPagingPages - 1) / PagingSectionSize;
            }
        }

        /// <summary>
        /// Number of first page in current paging section
        /// </summary>
        public int PagingSectionFirstPage
        {
            get { return 1 + (PagingSection - 1) * PagingSectionSize; }
        }

        /// <summary>
        /// Number of last page in current paging section
        /// </summary>
        public int PagingSectionLastPage
        {
            get { return Math.Min((PagingSection * PagingSectionSize), TotalPagingPages); }
        }

        /// <summary>
        /// Create URL for the next paging section.
        /// </summary>
        /// <returns>Url for next paging section</returns>
        public string GetNextPagingSectionUrl()
        {
            return UriSupport.AddQueryString(HttpContext.Current.Request.RawUrl, "p", ((PagingSection * PagingSectionSize) + 1).ToString());
        }

        /// <summary>
        /// Create URL for the previous paging section.
        /// </summary>
        /// <returns>Url for previous paging section</returns>
        public string GetPreviousPagingSectionUrl()
        {
            return UriSupport.AddQueryString(HttpContext.Current.Request.RawUrl, "p", ((PagingSection - 1) * PagingSectionSize).ToString());
        }

        /// <summary>
        /// Removes specified query string from url
        /// </summary>
        /// <param name="url">Url from which to remove query string</param>
        /// <param name="key">Key of query string to remove</param>
        /// <returns>New url that excludes the specified query string</returns>
        private string RemoveQueryStringByKey(string url, string key)
        {                   
            var uri = new Uri(url);
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);
            newQueryString.Remove(key);
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }

        /// <summary>
        /// Checks if service url and index are configured
        /// </summary>
        /// <param name="configuration">Find configuration</param>
        /// <returns>True if configured, false otherwise</returns>
        private bool SearchIndexIsConfigured(EPiServer.Find.Configuration configuration)
        {
            return (!configuration.ServiceUrl.IsNullOrEmpty()
                    && !configuration.ServiceUrl.Contains("YOUR_URI")
                    && !configuration.DefaultIndex.IsNullOrEmpty()
                    && !configuration.DefaultIndex.Equals("YOUR_INDEX"));
        }

        /// <summary>
        /// Requires the client resources used in the view.
        /// </summary>
        private void RequireClientResources()
        {
            var requiredClientResourceList = ServiceLocator.Current.GetInstance<IRequiredClientResourceList>();
            // jQuery.UI is used in autocomplete example. 
            // Add jQuery.UI files to existing client resource bundles or load it from CDN or use any other alternative library.
            // We use local resources for demo purposes without Internet connection.
            requiredClientResourceList.RequireStyle(VirtualPathUtilityEx.ToAbsolute("~/Static/css/jquery-ui.css"));
            requiredClientResourceList.RequireScript(VirtualPathUtilityEx.ToAbsolute("~/Static/js/jquery-ui.js")).AtFooter();
        }
    }
}
