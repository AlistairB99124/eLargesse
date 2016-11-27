<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="eLargesse.Admin.Products" %>
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
            <asp:Label runat="server" AssociatedControlID="Type" CssClass="col-md-2 control-label">Name:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name" CssClass="text-danger" ErrorMessage="Name is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Type" CssClass="col-md-2 control-label">Type:</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Type" runat="server" DataTextField="Name" DataValueField="Id" CssClass="form-control" DataSourceID="SqlDataSource1"></asp:DropDownList>
              
              
              
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:eLargesseConnection %>" SelectCommand="SELECT * FROM [SubType]"></asp:SqlDataSource>
              
              
              
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Type" CssClass="text-danger" ErrorMessage="Type is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlManufacturer" CssClass="col-md-2 control-label">Manufacturer:</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="Id" DataSourceID="SqlDataSource2"></asp:DropDownList>
              
               
              
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:eLargesseConnection %>" SelectCommand="SELECT * FROM [Manufacturer]"></asp:SqlDataSource>
              
               
              
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlManufacturer" CssClass="text-danger" ErrorMessage="Manufacturer is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Price" CssClass="col-md-2 control-label">Price:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Price" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Price" CssClass="text-danger" ErrorMessage="Price is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Image" CssClass="col-md-2 control-label">or Upload Image:</asp:Label>
            <div class="col-md-10">
                <div style="max-width:280px;">
                    <asp:Table runat="server" CssClass="table" Width="100%">
                        <asp:TableRow runat="server" CssClass="table" Width="100%">
                            <asp:TableCell runat="server" Width="75%">
                                <asp:FileUpload ID="imgFileUpload" runat="server" CssClass="form-control" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" Width="25%">
                                <asp:Button runat="server" ID="UploadButton" Text="Upload" CssClass="btn btn-default" OnClick="UploadButton_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Label ID="UploadStatus" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Image" CssClass="col-md-2 control-label">Image:</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Image" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Image" CssClass="text-danger" ErrorMessage="Image is required." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Height="140px" Width="323px" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Description" CssClass="text-danger" ErrorMessage="Description is required." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2">
                <asp:Button runat="server" ID="Submit" OnClick="Submit_Click" CssClass="btn btn-default" />
            </div>
        </div>
        <asp:Label runat="server" ID="SuccessLabel" Visible="false" CssClass="text-success col-md-offset-2"></asp:Label>
    </div>
</asp:Content>
