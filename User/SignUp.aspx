<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ChatSite.User.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../assets/js/signup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="sign-up-wrapper">
        <% if( ErrorMsg == ChatSite.Constants.S_OK ) { %>
        <h4 class="ui green header">Thank you for your registration.</h4>
        <a class="ui right labeled icon button" href="<%=ResolveUrl("~/User/SignIn.aspx") %>">
            <i class="right arrow icon"></i>
            Go to Login
        </a>
        <% } else { %>
        <h4 class="ui grey header">Fill the sign up form</h4>
        <hr />
        <% if( ErrorMsg != null ) { %>
        <div class="ui error message"><%=ErrorMsg %></div>
        <% } %>
        <form id="frmSignup" class="ui form" action="SignUp.aspx?action=register" method="post">
            <div class="field">
                <label>Account Name</label>
                <div class="ui left icon input">
                    <input name="account_name" type="text" placeholder="Input your account name." value="<%=Request["account_name"] %>">
                    <i class="user icon"></i>
                </div>
            </div>
            <div class="field">
                <label>Email</label>
                <div class="ui left icon input">
                    <input name="email" type="text" placeholder="Input your email." value="<%=Request["email"] %>">
                    <i class="mail icon"></i>
                </div>
            </div>
            <div class="field">
                <label>Password</label>
                <div class="ui left icon input">
                    <input name="pwd" type="password" value="<%=Request["pwd"] %>">
                    <i class="lock icon"></i>
                </div>
            </div>
            <div class="field">
                <label>Retype password</label>
                <div class="ui left icon input">
                    <input name="retype_pwd" type="password">
                    <i class="lock icon"></i>
                </div>
            </div>
            <div class="ui green submit button">Sign Up</div>
        </form>
        <% } %>
    </div>
</asp:Content>
