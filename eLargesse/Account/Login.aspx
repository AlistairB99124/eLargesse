<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eLargesse.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="product-big-title-area">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="product-bit-title text-center">
                        <h2><%:Page.Title %></h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="single-product-area">
        <div class="zigzag-bottom"></div>
        <div class="container">
            <div class="col-md-6">
                <div id="login-form-wrap">
                    <p class="form-row form-row-first">
                        <asp:Label runat="server" AssociatedControlID="username">
                                    Username or email <span class="required">*</span>
                        </asp:Label>
                        <asp:TextBox runat="server" ID="username" CssClass="form-control" />
                    </p>
                    <p class="form-row form-row-last">
                        <asp:Label runat="server" AssociatedControlID="password">
                                    Password <span class="required">*</span>
                        </asp:Label>
                        <asp:TextBox runat="server" ID="password"  CssClass="form-control" TextMode="Password" />
                    </p>
                    <asp:Label ID="lblErrorMessage" runat="server" Text="Label"></asp:Label>
                    <div class="clear"></div>
                    <p class="form-row">
                        <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-default" OnClick="LogIn" />
                        <asp:Label runat="server" CssClass="inline" AssociatedControlID="rememberme">
                            <asp:CheckBox runat="server" ID="rememberme" />
                            Remember me </asp:Label>
                    </p>
                    <p class="lost_password">
                        <asp:HyperLink runat="server" ID="LostPassword">Lost your password?</asp:HyperLink>
                    </p>

                    <div class="clear"></div>
                </div>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" NavigateUrl="~/Account/Register.aspx">Register as a new user</asp:HyperLink>
                </p>
            </div>
            <div class="col-md-4">
                <section id="socialLoginForm">
                    <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
                </section>
            </div>
        </div>
    </div>


</asp:Content>
