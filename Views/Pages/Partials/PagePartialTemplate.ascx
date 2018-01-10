<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PagePartialTemplate.ascx.cs" Inherits="EpiserverSite.Views.Pages.Partials.PagePartialTemplate" %>

<div class="border innerblock">
     
    <%-- We don't link the block if the underlying page is a container page --%>
    <asp:PlaceHolder Visible="<%# CurrentData.HasTemplate() %>" runat="server">
        <a href="<%= CurrentData.LinkURL %>" title="<%= HttpUtility.HtmlAttributeEncode(CurrentData.PageName) %>">
    </asp:PlaceHolder>
        
    <EPiServer:Property PropertyName="PageName" CustomTagName="h2" runat="server" />
    <p id="TeaserTextStandard" runat="server"><%= Server.HtmlEncode(CurrentData.TeaserText) %></p>
    <EPiServer:Property PropertyName="PageImage" runat="server" />
    
    <asp:PlaceHolder Visible="<%# CurrentData.HasTemplate() %>" runat="server">
        </a>
    </asp:PlaceHolder>

</div>
