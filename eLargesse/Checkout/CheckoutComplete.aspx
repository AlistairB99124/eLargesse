<%@ Page Title="Checkout Complete" Language="C#" MasterPageFile="~/Sidebar.Master" AutoEventWireup="true" CodeBehind="CheckoutComplete.aspx.cs" Inherits="eLargesse.Checkout.CheckoutComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
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
        <h1>Checkout Complete</h1>
        <p></p>
        <h3>Payment Transaction ID:</h3>
        <asp:Label ID="TransactionId"
            runat="server"></asp:Label>
        <p></p>
        <h3>Thank You!</h3>
        <p></p>
        <hr />
        <asp:Button ID="Continue" runat="server" Text="Continue Shopping" OnClick="Continue_Click" />
    </div>
</asp:Content>
