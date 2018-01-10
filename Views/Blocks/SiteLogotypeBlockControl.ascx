<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SiteLogotypeBlockControl.ascx.cs" Inherits="EpiserverSite.Views.Blocks.SiteLogotypeBlockControl" %>
<%@ Import Namespace="EPiServer.Web" %>

<a href="<%# ContentReference.IsNullOrEmpty(SiteDefinition.Current.StartPage) ? String.Empty : SiteDefinition.Current.StartPage.GetPage().LinkURL %>" title="<%# CurrentBlock.Title %>" id="logo" runat="server">
    <img src="<%# LogotypeUrl %>" alt="<%# CurrentBlock.Title %>" runat="server" />
</a>
