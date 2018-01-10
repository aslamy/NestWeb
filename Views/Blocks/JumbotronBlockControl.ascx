<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="JumbotronBlockControl.ascx.cs" Inherits="EpiserverSite.Views.Blocks.JumbotronBlockControl" %>

<div class="row">
    <div class="span4 hidden-tablet hidden-phone">
        <EPiServer:Property PropertyName="Image" runat="server" />
    </div>

    <div class="span8">
        <EPiServer:Property PropertyName="Heading" CustomTagName="h1" CssClass="jumbotron" DisplayMissingMessage="True" runat="server" />
        <EPiServer:Property PropertyName="SubHeading" CustomTagName="p" CssClass="subHeader" DisplayMissingMessage="True" runat="server" />
        <a class="btn-blue pull-right" href="<%# CurrentBlock.ButtonLink %>" id="jumboLink" runat="server"><%: CurrentBlock.ButtonText %></a>
    </div>
</div>
