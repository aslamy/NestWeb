<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/TwoPlusOne.Master" AutoEventWireup="false" CodeBehind="ProductPageTemplate.aspx.cs" Inherits="EpiserverSite.Views.Pages.ProductPageTemplate" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <EPiServer:Property CustomTagName="h1" PropertyName="PageName" runat="server" />
    
    <EPiServer:Property CustomTagName="p" CssClass="lead" PropertyName="MetaDescription" runat="server" />
    
    <div class="row">
        <EPiServer:Property PropertyName="MainBody" CustomTagName="div" CssClass="span8 clearfix" runat="server" />
    </div>
    
    <EPiServer:Property PropertyName="MainContentArea" CssClass="row" runat="server">
        <RenderSettings Tag="span4" />
    </EPiServer:Property>

</asp:Content>

<asp:Content ContentPlaceHolderID="RelatedContent" runat="server">
                
    <div>
        <EPiServer:Property PropertyName="PageImage" runat="server" />
    </div>

    <div class="block highlightBox" id="UniqueSellingPointsContainer" runat="server">
        <EPiServer:Property PropertyName="PageName" Runat="server" CustomTagName="h2" />
        <EPiServer:Property PropertyName="UniqueSellingPoints" runat="server" CustomTagName="div" />
    </div>

    <EPiServer:Property PropertyName="RelatedContentArea" CssClass="row" runat="server">
        <RenderSettings Tag="span4" />
    </EPiServer:Property>

</asp:Content>
