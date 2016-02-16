<%@ Page Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChatSite._Default" %>

<%@ Import Namespace="RnsBaseWeb.Session" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="<%=ResolveUrl("~/assets/js/chat/chat.css")%>" />
    <script src="<%=ResolveUrl("~/assets/js/chat/chat.js")%>"></script>

    <script src="<%=ResolveUrl("~/Scripts/socket.io-1.4.5.js")%>"></script>
    <script src="<%=ResolveUrl("~/assets/js/default.js")%>"></script>
</asp:Content>

<asp:Content ID="body1" ContentPlaceHolderID="body" runat="server">
    <h1>Welcome</h1>
    <hr />
    <div>Do your business</div>
    
    <input type="hidden" id="uid" value="<%=CSessionMgr.GetUid() %>"/>
    <input type="hidden" id="alias" value="<%=CSessionMgr.GetAccountName() %>" />

    <script>
        var socket = io('http://localhost:3000');
    </script>
</asp:Content>