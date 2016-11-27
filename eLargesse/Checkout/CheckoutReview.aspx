<%@ Page Title="Checkout Review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutReview.aspx.cs" Inherits="eLargesse.Checkout.CheckoutReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="container">
        <h1>Order Review</h1>
        <p></p>
        <h3 style="padding-left: 33px">Products:</h3>
        <asp:GridView ID="OrderItemList" runat="server" AutoGenerateColumns="False" GridLines="Both" CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText=" Product ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="UnitPrice" HeaderText="Price(each)" DataFormatString="{0:c}" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            </Columns>
        </asp:GridView>
        <asp:DetailsView ID="ShipInfo" runat="server" AutoGenerateRows="false" GridLines="None" CellPadding="10" BorderStyle="None" CommandRowStyleBorderStyle="None">
            <Fields>
                <asp:TemplateField>
                    <ItemTemplate>
                        <h3>Shipping Address:</h3>
                        <br />
                        <asp:Label ID="FirstName" runat="server" Text='<%#:Eval("FirstName") %>'></asp:Label>
                        <asp:Label ID="LastName" runat="server" Text='<%#:Eval("LastName") %>'></asp:Label>
                        <br />
                        <asp:Label ID="Address" runat="server" Text='<%#:Eval("Address") %>'></asp:Label>
                        <br />
                        <asp:Label ID="City" runat="server" Text='<%#: Eval("City")%>'></asp:Label>
                        <asp:Label ID="State" runat="server" Text='<%#: Eval("State")%>'></asp:Label>
                        <asp:Label ID="PostalCode" runat="server" Text='<%#:Eval("PostalCode") %>'></asp:Label>
                        <p></p>
                        <h3>Order Total:</h3>
                        <br />
                        <asp:Label ID="Total" runat="server" Text='<%#: Eval("Total","{0:C}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
        <p></p>
        <hr />
        <asp:Button ID="CheckoutConfirm" runat="server" Text="Complete Order" OnClick="CheckoutConfirm_Click" />
    </div>
</asp:Content>
