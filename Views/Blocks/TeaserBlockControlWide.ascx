<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TeaserBlockControlWide.ascx.cs" Inherits="EpiserverSite.Views.Blocks.TeaserBlockControlWide" %>
<%@ Import Namespace="EPiServer" %>

<div class="border innerblock">
       
    <Alloy:TeaserLink ContentLink="<%# CurrentBlock.Link %>" ToolTip="<%# CurrentBlock.Heading %>" runat="server">
        <div class="media">
            <div class="mediaImg">
                <EPiServer:Property PropertyName="Image" runat="server"/>
            </div>
            <div class="mediaText">
                <EPiServer:Property PropertyName="Heading" CustomTagName="h2" runat="server"/>
                <EPiServer:Property PropertyName="Text" CustomTagName="p" runat="server"/>
            </div>
        </div>
    </Alloy:TeaserLink>

</div>
