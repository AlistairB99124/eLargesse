<%@ Page Title="" Language="C#" MasterPageFile="~/SideBar.Master" AutoEventWireup="true" CodeBehind="Example.aspx.cs" Inherits="eLargesse.Example" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <h1>Heading 1</h1>
            <h2>Heading 2</h2>
            <h3>Heading 3</h3>
            <h4>Heading 4</h4>
            <h5>Heading 5</h5>
            <h6>Heading 6</h6>
            <h7>Heading 7</h7>
            <p>Paragraph</p>


        </div>
        <div class="col-md-4">

            <p>
                <asp:Button runat="server" Text="Default" CssClass="btn btn-default" /></p>
            <p>
                <asp:Button runat="server" Text="Success" CssClass="btn btn-success" /></p>
            <p>
                <asp:Button runat="server" Text="Primary" CssClass="btn btn-primary" /></p>
            <p>
                <asp:Button runat="server" Text="Information" CssClass="btn btn-info" /></p>
            <p>
                <asp:Button runat="server" Text="Warning" CssClass="btn btn-warning" /></p>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="img-thumbnail">
                <asp:Image runat="server" ImageUrl="~/img/logo.png" />
            </div>
        </div>
        <div class="col-md-8">
            <h2>Heading</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consequuntur, dolorem, excepturi. Dolore aliquam quibusdam ut quae iure vero exercitationem ratione!</p>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Modi ab molestiae minus reiciendis! Pariatur ab rerum, sapiente ex nostrum laudantium.</p>
        </div>

    </div>
    <div class="row">
        <div class="col-md-6">
            <h4>Unordered List</h4>
            <ul>
                <li>Item 1</li>
                 <li>Item 2</li>
                 <li>Item 3</li>
                 <li>Item 4</li>
                 <li>Item 5</li>
                 <li>Item 6</li>
            </ul>
        </div>
         <div class="col-md-6">
             <h4>Ordered List</h4>
             <ol>
                 <li>Item 1</li>
                 <li>Item 2</li>
                 <li>Item 3</li>
                 <li>Item 4</li>
                 <li>Item 5</li>
                 <li>Item 6</li>
             </ol>
        </div>
    </div>
</asp:Content>
