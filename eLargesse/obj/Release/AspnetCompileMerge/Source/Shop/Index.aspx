<%@ Page Title="Shop" Language="C#" MasterPageFile="~/SideBar.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="eLargesse.Shop.Index" %>

<%@ MasterType VirtualPath="~/SideBar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
    <div class="single-product-area">
        <div class="row">
            <div class="col-md-12">
                <asp:Panel  ID="pnlProducts" runat="server"></asp:Panel>
                
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <ul class="pagination">
                    <li><asp:LinkButton runat="server" ID="Previous" Text="<<" OnClick="Previous_Click" /></li>
                    <li><asp:LinkButton runat="server" ID="Next" Text=">>" OnClick="Next_Click" /></li>
                </ul>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
</asp:Content>
