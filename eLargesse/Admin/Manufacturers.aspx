<%@ Page Title="Manufacturers" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Manufacturers.aspx.cs" Inherits="eLargesse.Admin.Manufacturers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <ajaxToolkit:TabContainer  ID="TabContainer1" runat="server" ScrollBars="Both">
        <ajaxToolkit:TabPanel  ID="ViewManTab" runat="server" Width="100%" Height="400px">
            <ContentTemplate>
                <asp:GridView runat="server" ID="grdManu" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id"
                    CssClass="table table-striped table-bordered table-condensed" DataSourceID="ManufacturerDataSource"
                    OnRowEditing="grdManu_RowEditing">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
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
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="AddManuTab" Width="100%" Height="400px">
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
                        <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Name" CssClass="text-danger" ErrorMessage="Name is required." />
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
                        <div class="col-md-offset-2">
                            <asp:Button runat="server" ID="Submit" OnClick="Submit_Click" CssClass="btn btn-default" Text="Submit" />
                        </div>
                    </div>
                    <asp:Label runat="server" ID="SuccessLabel" Visible="false" CssClass="text-success col-md-offset-2"></asp:Label>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>



</asp:Content>
