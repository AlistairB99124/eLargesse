<%@ Page Title="Blog" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="eLargesse.Admin.Blog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ScrollBars="Both">
        <ajaxToolkit:TabPanel runat="server" ID="AddBlogTab" HeaderText="Add" Width="100%" Height="400px">
            <ContentTemplate>
                <div class="col-md-9">
                    <h2>New Blog</h2>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtTitle" CssClass="col-md-2 control-label">Title</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtTitle" TextMode="SingleLine" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtContent" CssClass="col-md-2 control-label">Content</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" CssClass="form-control" Width="100%" Height="400px" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-3">
                    
                    <div class="col-md-offset-2">

                        <asp:Button runat="server" Text="Publish" ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                    <p>
                        <asp:DropDownList runat="server" ID="ddlCategories"></asp:DropDownList>
                    </p>
                    <p>
                        <asp:Label runat="server" ID="StatusLabel" ForeColor="Red" />
                    </p>
                    <p>
                        <asp:FileUpload ID="FeatureImageUpload" runat="server" />
                    </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="ViewBlogsTab" HeaderText="View" Width="100%" Height="400px">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlBlogs" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>

</asp:Content>
