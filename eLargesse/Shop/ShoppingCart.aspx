<%@ Page Title="Shopping Cart" Language="C#" MasterPageFile="~/SideBar.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="eLargesse.Shop.ShoppingCart" %>
<%@ MasterType VirtualPath="~/SideBar.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
     <div class="woocommerce">
        <div>
            <asp:Panel runat="server" ID="pnlCart" />
        </div>
        <hr />
        <div class="cart-collaterals">
            <div class="cross-sells">
                <h2>You May Be Interested In...</h2>
                <ul class="products">
                    <li class="product">
                        <asp:HyperLink runat="server" ID="linkInterestedOne">
                            <asp:Image runat="server" ID="imgInterestedOne" Width="325" Height="325" AlternateText="T_4_front" CssClass="attachment-shop_catalog wp-post-image" />
                            <h3>
                                <asp:Label runat="server" ID="lblInterestedOne" /></h3>
                            <span class="price"><span class="amount">
                                <asp:Label runat="server" ID="lblInterestedPriceOne" /></span></span>
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" ID="linkInterestedAddOne" CssClass="add_to_cart_button">Select options</asp:HyperLink>
                    </li>
                    <li class="product">
                        <asp:HyperLink runat="server" ID="linkInterestedTwo">
                            <asp:Image runat="server" ID="imgInterestedTwo" Width="325" Height="325" AlternateText="T_4_front" CssClass="attachment-shop_catalog wp-post-image" />
                            <h3>
                                <asp:Label runat="server" ID="lblInterestedTwo" /></h3>
                            <span class="price"><span class="amount">
                                <asp:Label runat="server" ID="lblInterestedPriceTwo" /></span></span>
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" ID="linkInterestedAddTwo" CssClass="add_to_cart_button">Select options</asp:HyperLink>
                    </li>
                </ul>
            </div>

            <div class="cart_totals">
                <h2>Cart Totals</h2>
                <asp:Table runat="server" CellSpacing="0">
                    <asp:TableRow runat="server">
                        <asp:TableHeaderCell runat="server">
                                            Cart Subtotal
                        </asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <span class="amount">
                                <asp:Label runat="server" ID="lblSubtotal"></asp:Label></span>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableHeaderCell runat="server">
                                            Shipping (Estimate)
                        </asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <asp:Label runat="server" ID="lblShipping"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableHeaderCell runat="server">
                                            Tax
                        </asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <asp:Label ID="lblTax" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableHeaderCell runat="server">
                                            Order Total
                        </asp:TableHeaderCell>
                        <asp:TableCell runat="server">
                            <strong><span class="amount">
                                <asp:Label runat="server" ID="lblTotal" /></span></strong>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </div>
        </div>
    </div>
</asp:Content>
