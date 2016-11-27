<%@ Page Title="Checkout Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutError.aspx.cs" Inherits="eLargesse.Checkout.CheckoutError" %>

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
        <h1>Checkout Error</h1>
        <p></p>
        <table id="ErrorTable">
            <tr>
                <td class="field"></td>
                <td><%=Request.QueryString.Get("ErrorCode")%></td>
            </tr>
            <tr>
                <td class="field"></td>
                <td><%=Request.QueryString.Get("Desc")%></td>
            </tr>
            <tr>
                <td class="field"></td>
                <td><%=Request.QueryString.Get("Desc2")%></td>
            </tr>
        </table>
        <p></p>
    </div>
</asp:Content>
