<%@ Page Title="Sub-Types" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SubCategories.aspx.cs" Inherits="eLargesse.Admin.SubCategories" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
        <ajaxToolkit:TabPanel runat="server" ID="ViewSubCatTab" HeaderText="View" Width="100%" Height="400px">
            <ContentTemplate>
                <asp:GridView ID="GridView2" Width="100%" runat="server"
                    AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="Id"
                    CssClass="table table-striped table-bordered table-condensed" DataSourceID="SubCategoryDataSource"
                    OnRowEditing="GridView2_RowEditing">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
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
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="AddSubCatTab" HeaderText="Add" Width="100%" Height="400px">
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
                        <asp:Label runat="server" AssociatedControlID="DDLType" CssClass="col-md-2 control-label">Type:</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="DDLType" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="Id" DataSourceID="SqlDataSource1" />



                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:eLargesseConnection %>" SelectCommand="SELECT * FROM [Category]"></asp:SqlDataSource>



                            <asp:RequiredFieldValidator runat="server" ControlToValidate="DDLType" CssClass="text-danger" ErrorMessage="Name is required." />
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
