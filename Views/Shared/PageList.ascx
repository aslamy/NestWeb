<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PageList.ascx.cs" Inherits="EpiserverSite.Views.Shared.PageList" %>

<EPiServer:PageList OnFilter="FilterPageList" AutoBind="False" ID="List" runat="server">

    <ItemTemplate>
        <div class="listResult <%# Container.CurrentContent is ICategorizable ? string.Join(" ", ((ICategorizable)Container.CurrentContent).GetThemeCssClassNames()) : string.Empty %>">
            <h3>
                <EPiServer:Property PropertyName="PageLink" Visible="<%# Container.CurrentPage.IsVisibleOnSite() %>" runat="server" />
                <EPiServer:Property PropertyName="PageName" Runat="server" Visible="<%# !Container.CurrentPage.IsVisibleOnSite() %>" />
            </h3>
            <p class="date" clientidmode="Static" Visible="<%# IncludePublishDate && Container.CurrentPage != null && Container.CurrentPage.StartPublish.HasValue %>" runat="server">
                <%# Container.CurrentPage.StartPublish.Value.ToString("d MMMM yyyy") %>
            </p>
            <p clientidmode="Static" Visible="<%# IncludeIntroduction %>" runat="server"><%# Container.CurrentContent is SitePageData ? Server.HtmlEncode(((SitePageData)Container.CurrentContent).TeaserText) : String.Empty %></p>
            <hr />
        </div>
    </ItemTemplate>

</EPiServer:PageList>
