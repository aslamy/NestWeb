<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ContactBlockControl.ascx.cs" Inherits="EpiserverSite.Views.Blocks.ContactBlockControl" %>

<div class="contact">
    <EPiServer:Property PropertyName="Image" runat="server" />
    <EPiServer:Property PropertyName="Heading" CustomTagName="h3" runat="server" />
    <EPiServer:Property PropertyName="ContactPageLink" runat="server">
        <RenderSettings Tag="ContactBlock"></RenderSettings>
    </EPiServer:Property>
    <asp:PlaceHolder ID="ContactBlockLink" runat="server">
        <a class="btn-blue" href="<%# CurrentBlock.LinkUrl %>" id="Link" runat="server" ><%= CurrentBlock.LinkText %></a>
    </asp:PlaceHolder>
</div>
