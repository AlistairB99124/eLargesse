﻿<%@ Page Title="Types" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="eLargesse.Admin.Categories" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="form-horizontal">
        <div class="col-md-offset-2">
            <h2><%:Page.Title %></h2>
        </div>
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name" CssClass="text-danger" ErrorMessage="Name is required." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2">
                <asp:Button runat="server" ID="Submit" OnClick="Submit_Click" CssClass="btn btn-default" Text="Submit" />
            </div>
        </div>
        <asp:Label runat="server" ID="SuccessLabel" Visible="false" CssClass="text-success col-md-offset-2"></asp:Label>
    </div>
</asp:Content>
