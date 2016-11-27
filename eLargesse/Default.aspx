<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eLargesse._Default" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="slider-area">
        <div class="zigzag-bottom"></div>
        <div id="slide-list" class="carousel carousel-fade slide" data-ride="carousel">

            <div class="slide-bulletz">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <ol class="carousel-indicators slide-indicators">
                                <li data-target="#slide-list" data-slide-to="0" class="active"></li>
                                <li data-target="#slide-list" data-slide-to="1"></li>
                                <li data-target="#slide-list" data-slide-to="2"></li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="carousel-inner" role="listbox">
                <div class="item active">
                    <div class="single-slide">
                        <div class="slide-bg slide-one"></div>
                        <div class="slide-text-wrapper">
                            <div class="slide-text">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6 col-md-offset-6">
                                            <div class="slide-content">
                                                <h2>Community Engagement!</h2>
                                                <p>Ever felt the you could do more in community but needed guidance. Let eLargesse be that guide and show you the best way to give your money and time!</p>
                                                <p>From purchasing our charity products, to getting invloved in our projects. Go buidling homes in Kraifontein or knitt jerseys for the winter. We organise everything!</p>
                                                <asp:Hyperlink runat="server" CssClass="readmore">Learn more</asp:Hyperlink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="single-slide">
                        <div class="slide-bg slide-two"></div>
                        <div class="slide-text-wrapper">
                            <div class="slide-text">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6 col-md-offset-6">
                                            <div class="slide-content">
                                                <h2>We Have The Vison!</h2>
                                                <p>Our mission is to support and provide a platform for charities to
                                                    raise funds for their chosen causes. As well as encourage the
                                                    community and customers to engaged in community work.</p>
                                                <asp:Hyperlink runat="server" NavigateUrl="~/About.aspx#about-us" CssClass="readmore">Learn more</asp:Hyperlink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div class="single-slide">
                        <div class="slide-bg slide-three"></div>
                        <div class="slide-text-wrapper">
                            <div class="slide-text">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6 col-md-offset-6">
                                            <div class="slide-content">
                                                <h2>News</h2>
                                                <p>Our volunteers are active all over the Western Cape.</p>
                                                <p>A news blog keeps all our members up to date on all the wonderful things we get up to.</p>
                                                <asp:Hyperlink runat="server" NavigateUrl="~/News/Index.aspx" class="readmore">Learn more</asp:Hyperlink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <!-- End slider area -->

    <div class="promo-area">
        <div class="zigzag-bottom"></div>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-6">
                    <div class="single-promo">
                        <i class="fa fa-refresh"></i>
                        <p>30 Days return</p>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="single-promo">
                        <i class="fa fa-truck"></i>
                        <p>Affordable shipping</p>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="single-promo">
                        <i class="fa fa-lock"></i>
                        <p>Secure payments</p>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="single-promo">
                        <i class="fa fa-gift"></i>
                        <p>Quality products</p>
                    </div>
                </div>
            </div>
         
        </div>
    </div>
    <!-- End promo area -->

    <div class="maincontent-area">
        <div class="zigzag-bottom"></div>
        <div class="container">
            <div class="row">
                <asp:Label ID="lblResult" runat="server" Text="" Visible="false"></asp:Label>
                <div class="col-md-12">
                    <div class="latest-product">
                        <h2 class="section-title">Latest Products</h2>
                      
                            <asp:Panel ID="Panel1" runat="server" CssClass="product-carousel"></asp:Panel>
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End main content area -->

    <div class="brands-area">
        <div class="zigzag-bottom"></div>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="brand-wrapper">
                        <h2 class="section-title">Top Brands</h2>
                        <asp:Panel ID="Panel2" runat="server" CssClass="brand-list" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End brands area -->

    <div class="product-widget-area">
        <div class="zigzag-bottom"></div>
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="single-product-widget">
                        <h2 class="product-wid-title">Top Sellers</h2>
                        <asp:Hyperlink runat="server" class="wid-view-more">View All</asp:Hyperlink>
                        <asp:Panel ID="Panel3" runat="server"></asp:Panel>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="single-product-widget">
                        <h2 class="product-wid-title">Last Viewed</h2>
                        <asp:Hyperlink runat="server" class="wid-view-more">View All</asp:Hyperlink>
                        <asp:Panel ID="Panel4" runat="server"></asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End product widget area -->
</asp:Content>
