<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ContactPagePartialTemplate.ascx.cs" Inherits="EpiserverSite.Views.Pages.Partials.ContactPagePartialTemplate" %>

<div class="border innerblock">
    
    <EPiServer:Property PropertyName="PageImage" runat="server" />
    <EPiServer:Property PropertyName="PageName" CustomTagName="h2" runat="server" />
    <EPiServer:Property PropertyName="TeaserText" CustomTagName="p" runat="server" />
    <p>
        <%= Translate("/contact/email") %>: <a href="mailto:<%= HttpUtility.HtmlAttributeEncode(CurrentData.Email) %>"><%= Server.HtmlEncode(CurrentData.Email) %></a><br />
        <%= Translate("/contact/phone") %>: <%= Server.HtmlEncode(CurrentData.Phone) %>
    </p>

</div>
