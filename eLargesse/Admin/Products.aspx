<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="eLargesse.Admin.Products" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ScrollBars="Both">
        <ajaxToolkit:TabPanel runat="server" ID="ViewProductsTab" HeaderText="View" Width="100%" Height="400px">
            <ContentTemplate>
                <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-condensed" OnRowEditing="grdProducts_RowEditing" DataSourceID="ProductDataSource">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
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
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="AddProductsTab" HeaderText="Add" Width="100%" Height="400px">
            <ContentTemplate>
                <div class="form-horizontal">
                    <div class="col-md-offset-2">
                        <h2><%:Page.Title %></h2>
                    </div>
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Type" CssClass="col-md-2 control-label">Name:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Name" CssClass="text-danger" ErrorMessage="Name is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Type" CssClass="col-md-2 control-label">Type:</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="Type" runat="server" DataTextField="Name" DataValueField="Id" CssClass="form-control" DataSourceID="SqlDataSource1"></asp:DropDownList>



                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:eLargesseConnection %>" SelectCommand="SELECT * FROM [SubCategory]"></asp:SqlDataSource>



                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Type" CssClass="text-danger" ErrorMessage="Type is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlManufacturer" CssClass="col-md-2 control-label">Manufacturer:</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="Id" DataSourceID="SqlDataSource2"></asp:DropDownList>



                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:eLargesseConnection %>" SelectCommand="SELECT * FROM [Manufacturer]"></asp:SqlDataSource>



                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlManufacturer" CssClass="text-danger" ErrorMessage="Manufacturer is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Price" CssClass="col-md-2 control-label">Price:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Price" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Price" CssClass="text-danger" ErrorMessage="Price is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Image" CssClass="col-md-2 control-label">or Upload Image:</asp:Label>
                        <div class="col-md-10">
                            <div style="max-width: 280px;">
                                <asp:Table runat="server" CssClass="table" Width="100%">
                                    <asp:TableRow runat="server" CssClass="table" Width="100%">
                                        <asp:TableCell runat="server" Width="75%">
                                            <asp:FileUpload ID="imgFileUpload" runat="server" CssClass="form-control" />
                                        </asp:TableCell>
                                        <asp:TableCell runat="server" Width="25%">
                                            <asp:Button runat="server" ID="UploadButton" Text="Upload" CssClass="btn btn-default" OnClick="UploadButton_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <asp:Label ID="UploadStatus" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Image" CssClass="col-md-2 control-label">Image:</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="Image" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Image" CssClass="text-danger" ErrorMessage="Image is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Height="140px" Width="323px" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Description" CssClass="text-danger" ErrorMessage="Description is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2">
                            <asp:Button runat="server" ID="Submit" OnClick="Submit_Click" CssClass="btn btn-default" />
                        </div>
                    </div>
                    <asp:Label runat="server" ID="SuccessLabel" Visible="false" CssClass="text-success col-md-offset-2"></asp:Label>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
