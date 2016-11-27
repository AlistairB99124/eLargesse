<%@ Page Title="News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="eLargesse.News.Index" %>

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
        <div class="row">
            <div class="col-md-3">

            </div>
            <div class="col-md-6">
                <asp:Panel ID="pblArticles" runat="server"></asp:Panel>
            </div>
            <div class="col-md-3">
                <asp:Panel ID="pnlTopStories" runat="server"></asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
