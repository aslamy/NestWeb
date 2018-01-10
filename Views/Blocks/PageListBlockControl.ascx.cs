using System;
using EPiServer.Filters;
using EpiserverSite.Models.Blocks;

namespace EpiserverSite.Views.Blocks
{
    public partial class PageListBlockControl : SiteBlockControlBase<PageListBlock>
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Configure page list based on block properties
            PageList.CategoryFilter = CurrentBlock.CategoryFilter;
            PageList.IncludeIntroduction = CurrentBlock.IncludeIntroduction;
            PageList.IncludePublishDate = CurrentBlock.IncludePublishDate;
            PageList.ListRoot = CurrentBlock.Root;
            PageList.MaxCount = CurrentBlock.Count;
            PageList.PageTypeFilter = CurrentBlock.PageTypeFilter != null ? CurrentBlock.PageTypeFilter.ID : 0;
            PageList.Recursive = CurrentBlock.Recursive;
            PageList.SortOrder = (FilterSortOrder)CurrentBlock.SortOrder;

            // Trigger full page reload when block is updated in edit mode (requires FullRefreshPropertiesMetaData control in Root.Master)
            PageBase.EditHints.Add("IncludePublishDate");
            PageBase.EditHints.Add("IncludeIntroduction");
            PageBase.EditHints.Add("Count");
            PageBase.EditHints.Add("SortOrder");
            PageBase.EditHints.Add("Root");
            PageBase.EditHints.Add("PageTypeFilter");
            PageBase.EditHints.Add("CategoryFilter");
            PageBase.EditHints.Add("Recursive");
        }
    }
}
