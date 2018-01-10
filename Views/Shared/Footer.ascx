<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Footer.ascx.cs" Inherits="EpiserverSite.Views.Shared.Footer" EnableViewState="false" %>

<div class="row">
    <div class="span12 footer">
        <hr />
        <div class="row">
            <div class="span3">
                <h3>
                    <EPiServer:Translate Text="/footer/products" runat="server" />
                </h3>
                <EPiServer:Property PageLink="<%# StartPageReference.ToPageReference() %>" PropertyName="ProductPageLinks" runat="server" />
            </div>
            <div class="span3">
                <h3>
                    <EPiServer:Translate Text="/footer/company" runat="server" />
                </h3>
                <EPiServer:Property PageLink="<%# StartPageReference.ToPageReference() %>" PropertyName="CompanyInformationPageLinks" runat="server" />
            </div>
            <div class="span3">
                <h3>
                    <EPiServer:Translate Text="/footer/news" runat="server" />
                </h3>
                <EPiServer:Property PageLink="<%# StartPageReference.ToPageReference() %>" PropertyName="NewsPageLinks" runat="server" />
            </div>
            <div class="span3">
                <h3>
                    <EPiServer:Translate Text="/footer/customerzone" runat="server" />
                </h3>
                <EPiServer:Property PageLink="<%# StartPageReference.ToPageReference() %>" PropertyName="CustomerZonePageLinks" runat="server" />
                <ul>
                    <li>
                        <asp:LoginView runat="server">
                            <AnonymousTemplate>
                                <a href="<%# FormsAuthentication.LoginUrl %>?ReturnUrl=<%= Server.UrlEncode(GetCurrentPageUrl()) %>"><%# Translate("/footer/login") %></a>
                            </AnonymousTemplate>
                            <LoggedInTemplate>
                                <asp:LinkButton Text='<%# Translate("/footer/logout") %>' OnClick="Logout" runat="server" />
                            </LoggedInTemplate>
                        </asp:LoginView>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</div>
