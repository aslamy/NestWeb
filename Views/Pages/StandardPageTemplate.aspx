<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPages/LeftNavigation.Master" AutoEventWireup="false" CodeBehind="StandardPageTemplate.aspx.cs" Inherits="EpiserverSite.Views.Pages.StandardPageTemplate" %>

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
