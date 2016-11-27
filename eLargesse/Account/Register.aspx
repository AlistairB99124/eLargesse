<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="eLargesse.Account.Register" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
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
    <div>
        <div class="container">
            <div class="row" style="min-height:50px;">
                <div class="col-md-12">
                    <h5>Please fill in all the required feels</h5>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 well" style="margin:10px 5px 10px 10px;">
                    <h3>Login Details</h3>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-4 control-label">First Name:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" TextMode="SingleLine" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-4 control-label">Last Name:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" TextMode="SingleLine" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="username" CssClass="col-md-4 control-label">Username or email:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="username" CssClass="form-control" TextMode="Email" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-4 control-label">Password:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-4 control-label">Confirm Password:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="form-control" TextMode="Password" Width="100%" />
                            </div>
                        </div>

                        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                        <div class="form-group">
                            <div class="col-md-offset-4">
                                <asp:Button runat="server" ID="btnRegister" Text="REGISTER" CssClass="btn btn-default" TextMode="Password" OnClick="btnRegister_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="col-md-4">
                <section id="socialLoginForm">
                    <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
                </section>
            </div>
              <%--  <div class="col-md-5 well" style="margin:10px 10px 10px 5px;">
                    <h3>Optional: Billing Address</h3>
                    <p>You may set your billing address at checkout</p>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtAddress1" CssClass="col-md-4 control-label">Address Line 1:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtAddress1" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtAddress2" CssClass="col-md-4 control-label">Address Line 2:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtAddress2" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtCity" CssClass="col-md-4 control-label">City:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCity" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtState" CssClass="col-md-4 control-label">Province:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtState" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPostal" CssClass="col-md-4 control-label">Postal Code:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtPostal" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtCountry" CssClass="col-md-4 control-label">Country:</asp:Label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtCountry" TextMode="Password" Width="100%" />
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
        <div style="clear: both;"></div>
    </div>
</asp:Content>
