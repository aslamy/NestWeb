<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SubNavigation.ascx.cs" Inherits="EpiserverSite.Views.Shared.SubNavigation" %>

<EPiServer:MenuList PageLink="<%# TopPageLink %>" NumberOfLevels="1" runat="server">
    
    <HeaderTemplate>
        <div id="alloyDrop" class="accordion">
            <div class="accordion-group">
    </HeaderTemplate>
    
    <FooterTemplate>
            </div>
        </div>
    </FooterTemplate>
    
    <ItemTemplate>
        <div class="accordion-heading">
            <a class="accordion-toggle" href="<%# Container.CurrentPage.LinkURL %>" data-parent="#alloyDrop"><%# HttpContext.Current.Server.HtmlEncode(Container.CurrentPage.PageName) %> <i class='<%# HasChildren(Container.CurrentPage) ? "icon-chevron-down pull-right" : "pull-right" %>'></i></a>
        </div>
    </ItemTemplate>
    
    <SelectedTemplate>
        <div class="accordion-heading">
            <a class='<%# Container.CurrentPage.PageLink.CompareToIgnoreWorkID(CurrentPage.PageLink) ? "accordion-toggle active" : "accordion-toggle" %>' href="<%# Container.CurrentPage.LinkURL %>" data-parent="#alloyDrop"><%# HttpContext.Current.Server.HtmlEncode(Container.CurrentPage.PageName) %> <i class='<%# HasChildren(Container.CurrentPage) ? "icon-chevron-down pull-right" : "pull-right" %>'></i></a>
        </div>
        
        <asp:PlaceHolder runat="server" Visible="<%#HasChildren(Container.CurrentPage) %>">
            <EPiServer:PageTree PageLink="<%# Container.CurrentPage.PageLink %>" runat="server">
            
                    <HeaderTemplate>
                        <div id="collapse-<%# Container.CurrentPage.PageLink.ID %>" class="accordion-body collapse <%# Container.CurrentPage.PageLink.CompareToIgnoreWorkID(CurrentPage.PageLink) || CurrentPage.IsDescendantOf(Container.CurrentPage)  ? "in" : "" %>">
                            <ul>
                    </HeaderTemplate>
            
                    <FooterTemplate>
                            </ul>
                        </div>
                    </FooterTemplate>

                    <ItemTemplate>
                        <li><EPiServer:Property PropertyName="PageLink" runat="server" /></li>
                    </ItemTemplate>
                
                    <SelectedItemTemplate>
                        <li class="active"><EPiServer:Property PropertyName="PageLink" runat="server" /></li>
                    </SelectedItemTemplate>
            
                    <IndentTemplate>
                        <asp:PlaceHolder Visible="<%# Container.Indent>1 %>" runat="server">
                            <div style="height: 0px;" id="subCollapse-<%# Container.CurrentPage.ParentLink.ID %>" class="accordion-body collapse">
                                <ol>
                        </asp:PlaceHolder>
                    </IndentTemplate>
            
                    <UnindentTemplate>
                        <asp:PlaceHolder Visible="<%# Container.Indent>1 %>" runat="server">
                                </ol>
                            </div>
                        </asp:PlaceHolder>
                    </UnindentTemplate>

                </EPiServer:PageTree>
        </asp:PlaceHolder>

    </SelectedTemplate>

</EPiServer:MenuList>
