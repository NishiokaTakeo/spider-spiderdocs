<%@ Page Title="Login" Language="C#" MasterPageFile="~/mvc/view/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SpiderDocs_ClientControl.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="font-size: 1.5em; color: rgb(102, 102, 102); font-variant: small-caps; text-transform: none; font-weight: 600; margin-bottom: 0px; font-family: 'Helvetica Neue', 'Lucida Grande', 'Segoe UI', Arial, Helvetica, Verdana, sans-serif; font-style: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); ">
        Log In</h2>
    <p style="margin-bottom: 10px; line-height: 1.6em; color: rgb(105, 105, 105); font-family: 'Helvetica Neue', 'Lucida Grande', 'Segoe UI', Arial, Helvetica, Verdana, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); ">
        Please enter your username and password.<span class="Apple-converted-space">&nbsp;</span></p>
    <br />
    Username:<br />
    <asp:TextBox ID="txtUserName" runat="server" CssClass="textEntry" Height="22px" 
        Width="218px"></asp:TextBox>
    <br />
    <br />
    Password:<br />
    <asp:TextBox ID="txtPassword" runat="server" CssClass="passwordEntry" 
        Height="21px" Width="218px" TextMode="Password"></asp:TextBox>
    <br />
    <br />
<asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" 
    Text="Your login attempt was not successful. Please try again." 
        Visible="False"></asp:Label>
<br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" onclick="Button1_Click" 
        Text="Log In" CssClass="accountInfo" />
</asp:Content>
