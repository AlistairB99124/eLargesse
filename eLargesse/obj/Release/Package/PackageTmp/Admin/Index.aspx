<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="eLargesse.Admin.Index" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div>
            <h2><%:Page.Title %></h2>
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" Text="Add New Product" PostBackUrl="~/Admin/Products.aspx"></asp:LinkButton>
            <br />
            <hr />
            <div>
                <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-condensed" OnRowEditing="grdProducts_RowEditing" DataSourceID="ProductDataSource" >
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" SortExpression="SubCategoryId" />
                        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                        <asp:BoundField DataField="DateSold" HeaderText="DateSold" SortExpression="DateSold" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" />
                        <asp:BoundField DataField="ManufacturerId" HeaderText="ManufacturerId" SortExpression="ManufacturerId" />
                        <asp:BoundField DataField="LastViewed" HeaderText="LastViewed" SortExpression="LastViewed" />
                    </Columns>
                   
                </asp:GridView>
                <asp:SqlDataSource ID="ProductDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLargesse.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" DeleteCommand="DELETE FROM [Product] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Product] ([SubCategoryId], [DateCreated], [DateSold], [Name], [Price], [Description], [Image], [ManufacturerId], [LastViewed]) VALUES (@SubCategoryId, @DateCreated, @DateSold, @Name, @Price, @Description, @Image, @ManufacturerId, @LastViewed)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Product]" UpdateCommand="UPDATE [Product] SET [SubCategoryId] = @SubCategoryId, [DateCreated] = @DateCreated, [DateSold] = @DateSold, [Name] = @Name, [Price] = @Price, [Description] = @Description, [Image] = @Image, [ManufacturerId] = @ManufacturerId, [LastViewed] = @LastViewed WHERE [Id] = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SubCategoryId" Type="Int32" />
                        <asp:Parameter Name="DateCreated" Type="DateTime" />
                        <asp:Parameter Name="DateSold" Type="DateTime" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Price" Type="Decimal" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="Image" Type="String" />
                        <asp:Parameter Name="ManufacturerId" Type="Int32" />
                        <asp:Parameter Name="LastViewed" Type="DateTime" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SubCategoryId" Type="Int32" />
                        <asp:Parameter Name="DateCreated" Type="DateTime" />
                        <asp:Parameter Name="DateSold" Type="DateTime" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Price" Type="Decimal" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="Image" Type="String" />
                        <asp:Parameter Name="ManufacturerId" Type="Int32" />
                        <asp:Parameter Name="LastViewed" Type="DateTime" />
                        <asp:Parameter Name="Id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    <div class="row">
        <div class="col-md-6">
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Admin/Types.aspx">Add New Product Type</asp:LinkButton>
            <br/>
            <hr />
            <div>
                <asp:GridView ID="GridView1" Width="100%" 
                    runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="Id" 
                    CssClass="table table-striped table-bordered table-condensed" DataSourceID="CategoryDataSource"
                     OnRowEditing="GridView1_RowEditing" >
                    <Columns>
                     
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                     
                    </Columns>
                </asp:GridView>
               
                <asp:SqlDataSource ID="CategoryDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLargesse.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" DeleteCommand="DELETE FROM [Category] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Category] ([Name]) VALUES (@Name)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Category]" UpdateCommand="UPDATE [Category] SET [Name] = @Name WHERE [Id] = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Name" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
               
            </div>
            <div class="col-md-6">
                 <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Admin/SubTypes.aspx">Add New Product Sub Type</asp:LinkButton>
            <br/>
            <hr />
                <asp:GridView ID="GridView2" Width="100%" runat="server" 
                    AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" DataKeyNames="Id" 
                    CssClass="table table-striped table-bordered table-condensed" DataSourceID="SubCategoryDataSource"
                     OnRowEditing="GridView2_RowEditing">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" SortExpression="CategoryId" />
                    </Columns>
                 
                </asp:GridView>
              
                 <asp:SqlDataSource ID="SubCategoryDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLargesse.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" DeleteCommand="DELETE FROM [SubCategory] WHERE [Id] = @Id" InsertCommand="INSERT INTO [SubCategory] ([Name], [CategoryId]) VALUES (@Name, @CategoryId)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [SubCategory]" UpdateCommand="UPDATE [SubCategory] SET [Name] = @Name, [CategoryId] = @CategoryId WHERE [Id] = @Id">
                     <DeleteParameters>
                         <asp:Parameter Name="Id" Type="Int32" />
                     </DeleteParameters>
                     <InsertParameters>
                         <asp:Parameter Name="Name" Type="String" />
                         <asp:Parameter Name="CategoryId" Type="Int32" />
                     </InsertParameters>
                     <UpdateParameters>
                         <asp:Parameter Name="Name" Type="String" />
                         <asp:Parameter Name="CategoryId" Type="Int32" />
                         <asp:Parameter Name="Id" Type="Int32" />
                     </UpdateParameters>
                 </asp:SqlDataSource>
              
            </div>
        </div>

    </div>
          <div class="row">
               <div class="col-md-6">
            <asp:LinkButton ID="btnAddManufacturer" Width="100%" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Admin/Manufacturers.aspx">Add New Manufacturer</asp:LinkButton>
            <br/>
            <hr />
                   <asp:GridView runat="server" ID="grdManu" AllowPaging="True" 
                       AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" 
                       CssClass="table table-striped table-bordered table-condensed" DataSourceID="ManufacturerDataSource"
                        OnRowEditing="grdManu_RowEditing" >
                       <Columns>
                           <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                           <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                           <asp:BoundField DataField="Logo" HeaderText="Logo" SortExpression="Logo" />
                       </Columns>
                    
                   </asp:GridView>
                   <asp:SqlDataSource ID="ManufacturerDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLargesse.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" DeleteCommand="DELETE FROM [Manufacturer] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Manufacturer] ([Name], [Logo]) VALUES (@Name, @Logo)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Manufacturer]" UpdateCommand="UPDATE [Manufacturer] SET [Name] = @Name, [Logo] = @Logo WHERE [Id] = @Id">
                       <DeleteParameters>
                           <asp:Parameter Name="Id" Type="Int32" />
                       </DeleteParameters>
                       <InsertParameters>
                           <asp:Parameter Name="Name" Type="String" />
                           <asp:Parameter Name="Logo" Type="String" />
                       </InsertParameters>
                       <UpdateParameters>
                           <asp:Parameter Name="Name" Type="String" />
                           <asp:Parameter Name="Logo" Type="String" />
                           <asp:Parameter Name="Id" Type="Int32" />
                       </UpdateParameters>
                   </asp:SqlDataSource>
                   </div>
          </div>
          </div>
</asp:Content>
