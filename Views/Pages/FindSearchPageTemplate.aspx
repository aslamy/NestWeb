<%@ Page Language="C#" MasterPageFile="~/Views/MasterPages/TwoPlusOne.master" Codebehind="FindSearchPageTemplate.aspx.cs" Inherits="EpiserverSite.Views.Pages.FindSearchPageTemplate" %>
<%@ Import Namespace="EPiServer.Find" %>
<%@ Import Namespace="EPiServer.Find.Helpers.Text" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%
if (IsConfigured)
{
%>
    <script language="javascript">
        function spellcheck(query, hitsCutoff) {
            $.ajax({
                url: "<%=PublicProxyPath%>" + "/_spellcheck?query=" + encodeURIComponent(query) + "&size=1&hits_cutoff=" + hitsCutoff + "&tags=" + encodeURIComponent("<%=Tags%>"),
                dataType: 'jsonp',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.hits != undefined && data.hits[0] != undefined) {
                        var suggestion = data.hits[0].suggestion;
                        if (suggestion != undefined) {
                            document.getElementById("suggestion").innerHTML = "<%=Translate("/searchpagetemplate/suggestion")%>" + ": " + "<a href='?q=" + suggestion + "'>" + suggestion + "</a>";
                        }
                    }
                }
            });
        }
        function relatedQuery(query, hitsCutoff) {
            $.ajax({
                url: "<%=PublicProxyPath%>" + "/_didyoumean?query=" + encodeURIComponent(query) + "&size=3&hits_cutoff=" + hitsCutoff + "&tags=" + encodeURIComponent("<%=Tags%>"),
                dataType: 'jsonp',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.hits != undefined && data.hits[0] != undefined) {
                        var list = [];
                        for (var hit in data.hits) {
                            list.push(data.hits[hit].suggestion);
                        }
                        document.getElementById("suggestion").innerHTML =
                          "<%=Translate("/searchpagetemplate/peoplealsosearchedfor")%>" + ": " + list.join(", ");
                    }
                }
            });
        }
        $(function () {
            $("#<%=srchTxt.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "<%=PublicProxyPath%>" + "/_autocomplete?prefix=" + encodeURIComponent(request.term) + "&size=5" + "&tags=" + encodeURIComponent("<%=Tags%>"),
                        dataType: "jsonp",
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            response($.map(data.hits, function (item) {
                                return {
                                    label: item.query,
                                    value: item.query
                                };
                            }));
                        }
                    });
                },
                minLength: 2
            });
        });
    </script>
<%
}
%>
    
    <div class="row">
        <div class="span8,search-form">
            <asp:Panel DefaultButton="SearchButton" runat="server">
                <asp:TextBox TabIndex="1" ID="srchTxt" name="srchTxt" runat="server" />
                <asp:Button Text='<%#Translate("/searchpagetemplate/searchbutton")%>' Enabled="<%#IsConfigured%>" CssClass="btn" TabIndex="2" OnClick="SearchClick" ID="SearchButton" runat="server" />
            </asp:Panel>
            <asp:CustomValidator ControlToValidate="srchTxt" Display="Dynamic" ID="SearchKeywordsValidator" ClientIDMode="Static" runat="server" />     
        </div>
    </div>
    
<%
if(Hits != null)
{
%>
    <div class="row">
        <div class="span8 grayHead">
            <h2><%=Translate("/searchpagetemplate/result")%></h2>
            <p>
                <%=Translate("/searchpagetemplate/searchfor")%> <i><%=Query%></i>
                <%=Translate("/searchpagetemplate/resultedin")%>
                <%if(Query.IsNotNullOrEmpty())
                {
                %>
                    <%=NumberOfHits%>
                     <%=Translate("/searchpagetemplate/hits")%>

                    <script type="text/javascript">relatedQuery($("#<%=srchTxt.ClientID%>").val(), 3)</script>
                <%
                }
                int nrOfHits = 0;
                if(!int.TryParse(NumberOfHits, out nrOfHits) || nrOfHits <= 0)
                {
                %>
                    <script type="text/javascript">spellcheck($("#<%=srchTxt.ClientID%>").val(), 3)</script>
                <%
                }
                %>                                 

                 <div id="suggestion" style="margin: 1%;"></div>
                
            </p>
        </div>
    
        <%--Display search results here--%>
        <div class="span6 SearchResults">
            <%
            foreach (var hit in Hits.Hits)
            {
            %>
                <div class="listResult">
                    <h3><a href="<%=hit.Document.Url%>"><%=hit.Document.Title%></a></h3>
                    <p>
                        <%
                            if (hit.Document.ImageUri != null)
                            {
                        %>
                                <img src="<%=hit.Document.ImageUri.ToString()%>" height="<%=HitImagesHeight%>"/>
                        <%
                            } 
                        %>
                        <%=hit.Document.Excerpt%>
                    </p>
                    <hr />
                </div>
            <%
            }
            %>
        </div>
        
        <%
        if (Hits.TotalMatching > 0)
        {
        %>
            <%--Sidebar in which facets/filter are shown--%>
            <div class="span2">
                <div class="well">
                    <h2>Sections</h2>
                    <ul class="nav nav-list">

                        <%--Link for clearing section filter--%>
                        <li <%=string.IsNullOrWhiteSpace(SectionFilter) ? "class=\"active\"" : ""%>>
                            <a href="<%=GetSectionGroupUrl("")%>"> All (<%=Hits.FilterFacet("AllSections").Count%>)
                            </a>
                        </li>
                        <%--Display number of hits per section with link for filtering by section--%>
                        <%
                        foreach(var sectionGroup in Hits.TermsFacetFor(x => x.SearchSection))
                        {
                        %>
                            <li <%=SectionFilter == sectionGroup.Term ? "class=\"active\"" : ""%>>
                                <a href= "<%=GetSectionGroupUrl(sectionGroup.Term)%>">
                                    <%=sectionGroup.Term%> (<%=sectionGroup.Count%>)
                                </a>
                            </li>
                        <%
                        }
                        %>
                    </ul>
                </div>
            </div>
        <%
        }
        %>
            <%--Display paging controls--%>
            <div class="span8 pagination pagination-centered" >
                <ul>
                    <%--Link to the previous paging page--%>
                    <%
                    if (PagingPage == 1)
                    {
                    %>
                        <li class="disabled">
                            <a>&laquo; </a>
                        </li>
                    <%
                    }
                    else
                    {
                    %>
                        <li>
                            <a href="<%=GetPagingUrl(PagingPage - 1)%>">&laquo; </a>
                        </li>
                    <%
                    }
                    %>
                    
                    <%--Link to the previous paging section--%>
                    <%
                    if (PagingSection > 1)
                    {
                    %>
                        <li>
                            <a href="<%=GetPreviousPagingSectionUrl()%>">...</a>
                        </li>
                    <%
                    }
                    %>
                    
                    <%--Display links for each specific paging page--%>
                    <%
                    for (int pageNumber = PagingSectionFirstPage; pageNumber <= PagingSectionLastPage; pageNumber++)
                    {
                        if(PagingPage == pageNumber)
                        {
                    %>
                            <li class="active">
                                <a> <%=pageNumber%> </a>
                            </li>
                    <%
                        }
                        else
                        {
                    %>
                            <li>
                                <a href="<%=GetPagingUrl(pageNumber)%>"> <%=pageNumber%> </a>
                            </li>
                    <%
                        }
                    }
                    %>
                    
                    <%--Link to the next paging section--%>
                    <%
                    if (PagingSection < TotalPagingSections)
                    {
                    %>
                        <li>
                            <a href="<%=GetNextPagingSectionUrl()%>">...</a>
                        </li>
                    <%
                    }
                    %>
                              
                    <%--Link to the next paging page--%>
                    <%
                    if (PagingPage == TotalPagingPages)
                    {
                    %>
                        <li class="disabled">
                            <a>&raquo;</a>
                        </li>
                    <%
                    }
                    else
                    {
                    %>
                        <li>
                            <a href="<% =GetPagingUrl(PagingPage+1)%>">&raquo;</a>
                        </li>
                    <%
                    }
                    %>
                </ul>
            </div>
    </div>
<%
}

if (!IsConfigured)
{
%>
    <br />
    <p class="alert alert-info"><%=Translate("/searchpagetemplate/notconfigured")%></p>    
<%
}
%>
</asp:Content>
