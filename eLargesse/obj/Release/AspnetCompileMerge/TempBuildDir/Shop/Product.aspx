<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/SideBar.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="eLargesse.Shop.Product" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ MasterType VirtualPath="~/SideBar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
    <asp:Table ID="tableProduct" runat="server" Width="100%">
        <asp:TableRow runat="server" CssClass="row">
            <asp:TableCell runat="server" CssClass="col-sm-6">
                <div class="product-images">
                    <div class="product-image">
                        <asp:Image ID="imgProductMain" runat="server" Width="100%" />
                    </div>
                    <div class="product-gallery">
                        <asp:Image ID="imgProductGalleryOne" runat="server" />
                        <asp:Image ID="imgProductGalleryTwo" runat="server" />
                        <asp:Image ID="imgProductGalleryThree" runat="server" />
                        <asp:Image ID="imgProductGalleryFour" runat="server" />
                    </div>
                </div>
            </asp:TableCell>
            <asp:TableCell runat="server" CssClass="col-sm-6">
                <div class="product-inner">
                    <h2 class="product-name">
                        <asp:Label ID="lblProductName" runat="server" /></h2>
                    <div class="product-inner-price">
                        <ins>
                            <asp:Label runat="server" ID="lblProductPrice" /></ins>
                    </div>

                    <div class="cart">
                        <div class="quantity">
                            <asp:DropDownList runat="server" ID="ddlQuantity" CssClass="input-text qty text" />
                        </div>
                        <asp:Button runat="server" ID="btnSubmit" CssClass="add_to_cart_button" Text="ADD TO CART" OnClick="btnSubmit_Click"  />
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                    </div>

                    <div class="product-inner-category">
                        <p>Category: <a href="#">Summer</a>. Tags: <a href="#">awesome</a>, <a href="#">best</a>, <a href="#">sale</a>, <a href="#">shoes</a>. </p>
                    </div>

                    <div role="tabpanel">
                        <ul class="product-tab" role="tablist">
                            <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Description</a></li>
                            <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Reviews</a></li>
                        </ul>
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane fade in active" id="home">
                                <h2>Product Description</h2>
                                <p>
                                    <asp:Label runat="server" ID="lblDescription" />
                                </p>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="profile">
                                <h2>Reviews</h2>
                                <div class="submit-review">
                                    <p>
                                        <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName">Name</asp:Label><asp:TextBox runat="server" ID="txtName" />
                                    </p>
                                    <p>
                                        <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail">Email</asp:Label><asp:TextBox runat="server" ID="txtEmail" />
                                    </p>
                                    <div class="rating-chooser">
                                        <p>Your rating</p>

                                        <div class="rating-wrap-post">
                                            <%--<asp:ScriptManager ID="asm" runat="server" />--%>
                                            <div>
                                            <%--    <ajaxToolkit:Rating ID="Rating1" runat="server"
                                                    CurrentRating="0" MaxRating="5"
                                                    EmptyStarCssClass="glyphicon glyphicon-star-empty"
                                                    FilledStarCssClass="glyphicon glyphicon-star"
                                                    StarCssClass="glyphicon glyphicon-star"
                                                    WaitingStarCssClass="glyphicon glyphicon-star" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <p>
                                        <asp:Label runat="server" ID="lblYourReview" AssociatedControlID="txtYourReview">Your Review</asp:Label><asp:TextBox runat="server" ID="txtYourReview" TextMode="MultiLine" />
                                    </p>
                                    <p>
                                        <asp:Button runat="server" ID="btnReviewSubmit" Text="SUBMIT" />
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div class="related-products-wrapper">
        <h2 class="related-products-title">Related Products</h2>
        <div class="related-products-carousel owl-carousel owl-theme owl-loaded owl-responsive-1200">
            <div class="owl-stage-outer">
            </div>
            <div class="owl-controls">
                <div class="owl-controls">
                    <div class="owl-nav">
                        <div style="" class="owl-prev">prev</div>
                        <div style="" class="owl-next">next</div>
                    </div>
                    <div class="owl-dots" style="">
                        <div class="owl-dot active"><span></span></div>
                        <div class="owl-dot"><span></span></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
