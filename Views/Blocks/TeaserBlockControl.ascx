<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TeaserBlockControl.ascx.cs" Inherits="EpiserverSite.Views.Blocks.TeaserBlockControl" %>
<%@ Import Namespace="EPiServer" %>

<div class="border innerblock">
    
    <Alloy:TeaserLink ContentLink="<%# CurrentBlock.Link %>" ToolTip="<%# CurrentBlock.Heading %>" runat="server">
        <EPiServer:Property PropertyName="Heading" CustomTagName="h2" runat="server"/>
        <EPiServer:Property PropertyName="Text" CustomTagName="p" runat="server"/>
        <EPiServer:Property PropertyName="Image" runat="server"/>
    </Alloy:TeaserLink>

</div>
