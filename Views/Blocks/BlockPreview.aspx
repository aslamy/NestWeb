<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="BlockPreview.aspx.cs" MasterPageFile="~/Views/MasterPages/Site.master" Inherits="EpiserverSite.Views.Blocks.BlockPreview" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    
    <asp:Repeater ID="previewRepeater" OnItemDataBound="previewRepeater_ItemDataBound" runat="server">
        <ItemTemplate>
            <Alloy:TemplateHint ID="templateHint" runat="server" Visible="<%# PreviewAreas.Any(a => a.Supported) %>">
                <%# 
                    (bool)DataBinder.Eval(Container.DataItem, "Supported") ? 
                    string.Format(Translate("/preview/heading"), HttpUtility.HtmlEncode(CurrentData.Name), DataBinder.Eval(Container.DataItem, "AreaName")) :
                    string.Format(Translate("/preview/norenderer"), HttpUtility.HtmlEncode(CurrentData.Name), DataBinder.Eval(Container.DataItem, "AreaName"))
                %>

            </Alloy:TemplateHint>
            <EPiServer:Property ID="propertyControl" CssClass="row preview" runat="server">
                <RenderSettings EnableEditFeaturesForChildren="true" />
            </EPiServer:Property>
        </ItemTemplate>
    </asp:Repeater>
    <Alloy:TemplateHint ID="noRendererAtAllTemplateHint" runat="server" Visible="<%# PreviewAreas.All(a => !a.Supported) %>" >
        <%=
            string.Format(Translate("/preview/norendereratall"), HttpUtility.HtmlEncode(CurrentData.Name))
        %>
    </Alloy:TemplateHint>
</asp:Content>
