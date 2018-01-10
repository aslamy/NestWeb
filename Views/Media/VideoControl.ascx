<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="VideoControl.ascx.cs" Inherits="EpiserverSite.Views.Media.VideoControl" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<div class="embed" id="embed" runat="server">
    <div style="position: absolute; width: 100%; height: 100%">
        <div id="container" runat="server"></div>
    </div>

    <script type="text/javascript" src="<%# ScriptUrl %>"></script>
    <script type="text/javascript">
        <%--
            To prevent special characters in paths from breaking the script and 
            open up for script injection we use the serializer to escape the config.
        --%>
        jwplayer('<%= container.ClientID %>').setup(
            <%# new JavaScriptSerializer().Serialize(PlayerConfiguration) %>
        );
        jwplayer('<%= container.ClientID %>').play();
        jwplayer('<%= container.ClientID %>').pause();
    </script>
</div>
