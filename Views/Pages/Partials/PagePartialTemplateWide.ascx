<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PagePartialTemplateWide.ascx.cs" Inherits="EpiserverSite.Views.Pages.Partials.PagePartialTemplateWide" %>

<div class="border innerblock">
     
    <%-- We don't link the block if the underlying page is a container page --%>
    <asp:PlaceHolder Visible="<%# CurrentData.HasTemplate() %>" runat="server">
        <a href="<%= CurrentData.LinkURL %>" title="<%= HttpUtility.HtmlAttributeEncode(CurrentData.PageName) %>">
    </asp:PlaceHolder>

    <div class="media">
        <div class="mediaImg">
            <EPiServer:Property PropertyName="PageImage" runat="server" />
        </div>
        <EPiServer:Property PropertyName="PageName" CustomTagName="h2" runat="server" />
        <p id="TeaserTextWide" runat="server"><%= Server.HtmlEncode(CurrentData.TeaserText) %></p><%-- Access page property directly to make use of custom property getter --%>
    </div>
    
    <asp:PlaceHolder Visible="<%# CurrentData.HasTemplate() %>" runat="server">
        </a>
    </asp:PlaceHolder>

</div>
