<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="eLargesse.Account.Wishlist" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="container">
            <div class="col-md-4">
                <ul class="list-group">
                    <li class="list-group-item">
                        <div class="input-group">
                            <span class="input-group-btn">
                                <asp:LinkButton runat="server" CssClass="btn btn-default"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Search for..." />
                        </div>
                        <!-- /input-group -->
                        </li>
                    <li class="list-group-item">
                        <asp:LinkButton ID="CreateList" OnClick="CreateList_Click" runat="server" Width="100%" CssClass="btn btn-default">Add List</asp:LinkButton>
                    </li>
                    <%--<li class="list-group-item">
                        <asp:HyperLink ID="HyperLink12" runat="server">HyperLink</asp:HyperLink>
                    </li>--%>
                </ul>
                   
            </div>
            <div class="col-md-8">
                <div class="maincontent-area">
                    <div class="">
                        <div class="row">
                            <div class="col-md-12">
                                <div>
                                    <h4>New List</h4>
                                    <div class="latest-product">
                                        <asp:Panel ID="pnlWishlistOuter" runat="server" class="product-carousel">
                                        </asp:Panel>
                                    </div>
                                  
                  
                                </div>
                                <hr />
                                <div>
                                    <h4>Audio</h4>
                                    <div class="latest-product">
                                        <asp:Panel ID="pnlCategoryOne" runat="server" class="product-carousel">
                                        </asp:Panel>
                                    </div>
                                </div>
                                <hr />
                                <div>
                                    <h4>TVs</h4>
                                    <div class="latest-product">
                                        <asp:Panel ID="pnlCategoryTwo" runat="server" class="product-carousel">
                                        </asp:Panel>
                                    </div>
                                </div>
                                <hr />
                                <div>
                                    <h4>Garden Tools</h4>
                                    <div class="latest-product">
                                        <asp:Panel ID="pnlCategoryThree" runat="server" class="product-carousel">
                                        </asp:Panel>
                                    </div>
                                </div>
                                <hr />
                                <div>
                                    <div class="latest-product">
                                        <asp:Panel ID="pnlCategoryFour" runat="server" class="product-carousel">
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
