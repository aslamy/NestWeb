<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ContactPagePartialTemplateWide.ascx.cs" Inherits="EpiserverSite.Views.Pages.Partials.ContactPagePartialTemplateWide" %>

<div class="border innerblock">
    
    <div class="media">
        <div class="mediaImg">
            <EPiServer:Property PropertyName="PageImage" runat="server" />
        </div>
        <div class="mediaText">
            <EPiServer:Property PropertyName="PageName" CustomTagName="h2" runat="server" />
            <EPiServer:Property PropertyName="TeaserText" CustomTagName="p" runat="server" />
            <p>
                <%= Translate("/contact/email") %>: <a href="mailto:<%= HttpUtility.HtmlAttributeEncode(CurrentData.Email) %>"><%= Server.HtmlEncode(CurrentData.Email) %></a><br />
                <%= Translate("/contact/phone") %>: <%= Server.HtmlEncode(CurrentData.Phone) %>
            </p>
        </div>
    </div>

</div>
