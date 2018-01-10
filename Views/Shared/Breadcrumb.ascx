<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Breadcrumb.ascx.cs" Inherits="EpiserverSite.Views.Shared.Breadcrumb" %>

<EPiServer:PageList ID="BreadcrumbList" runat="server">
    
    <HeaderTemplate>
        <div class="row visible-desktop">
            <div class="span12">
                <ul class="alloyBreadcrumb">
    </HeaderTemplate>

    <FooterTemplate>
                    <EPiServer:Property CustomTagName="li" CssClass="active" PropertyName="PageName" Runat="server" />
                </ul>
            </div>
        </div> 
    </FooterTemplate>
    
    <ItemTemplate>
        <li>
            <%-- If a page doesn't have a template we don't link it --%>
            <EPiServer:Property Visible="<%# Container.CurrentPage.IsVisibleOnSite() %>" PropertyName="PageLink" runat="server" />
            <EPiServer:Property PropertyName="PageName" Runat="server" Visible="<%# !Container.CurrentPage.IsVisibleOnSite() %>" />
            <span class="divider">/</span>
        </li>
    </ItemTemplate>

</EPiServer:PageList>
