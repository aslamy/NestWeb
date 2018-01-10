using System;
using EpiserverSite.Business.WebControls;
using EpiserverSite.Models.Pages;
using EPiServer.Web;

namespace EpiserverSite.Views.Pages.Partials
{
    /// <summary>
    /// Base class for user controls used to render pages when dropped in a content area
    /// </summary>
    /// <typeparam name="T">Any page type inheriting from SitePageData</typeparam>
    public abstract class PartialPageTemplate<T> : ContentControlBase<SitePageData, T> where T : SitePageData
    {
    }
}
