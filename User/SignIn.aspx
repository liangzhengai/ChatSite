<%@ Page Title="Sign In" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="ChatSite.User.SignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="sign-in-wrapper">
        <% if( ErrorMsg != null ) { %>
            <div class="ui error message"><%=ErrorMsg %></div>
        <% } %>
        <div class="ui two column middle aligned very relaxed stackable grid" style="position:relative;">
            <div class="column">
                <form class="ui form" action="SignIn.aspx?action=signin" method="post">
                    <div class="field">
                        <label>Username</label>
                        <div class="ui left icon input">
                            <input name="account_name" type="text" placeholder="Input account name or email.">
                            <i class="user icon"></i>
                        </div>
                    </div>
                    <div class="field">
                        <label>Password</label>
                        <div class="ui left icon input">
                            <input name="pwd" type="password">
                            <i class="lock icon"></i>
                        </div>
                    </div>
                    <button class="ui blue submit button">Sign In</button>
                </form>
            </div>
            <div class="ui vertical divider">
                Or
            </div>
            <div class="center aligned column">
                <a href="SignUp.aspx" class="ui big green labeled icon button">
                    <i class="signup icon"></i>
                    Sign Up
                </a>
            </div>
        </div>
    </div>    
</asp:Content>
