<%@ Page Title="Shop" Language="C#" MasterPageFile="~/SideBar.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="eLargesse.Shop.Index" %>

<%@ MasterType VirtualPath="~/SideBar.Master" %>

<asp:Content ID="SideFilters" ContentPlaceHolderID="FilterContent" runat="server">
    <asp:TreeView ID="TreeView1" runat="server" MaxDataBindDepth="2" OnTreeNodePopulate="TreeView1_TreeNodePopulate" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
        <LeafNodeStyle/>
        <Nodes>
            <asp:TreeNode PopulateOnDemand="True" Text="Filter by Product Type" Value="Filter by Product Type"></asp:TreeNode>
        </Nodes>

        <ParentNodeStyle/>
        <RootNodeStyle/>
        <SelectedNodeStyle/>

    </asp:TreeView>
    <br />
    <hr />
    <asp:TreeView ID="TreeView2" runat="server" MaxDataBindDepth="2" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged" OnTreeNodePopulate="TreeView2_TreeNodePopulate">
        <LeafNodeStyle />
        <Nodes>
            <asp:TreeNode PopulateOnDemand="true" Text="Filter by Manufacturer" Value="Filter by Manufacturer"></asp:TreeNode>
        </Nodes>
    </asp:TreeView>
    <br />
    <hr />

    <asp:Table ID="tblPriceFilter" runat="server" Width="100%" CssClass="table-condensed" CellPadding="0" CellSpacing="0" HorizontalAlign="Center">
        <asp:TableRow runat="server" Width="100%" VerticalAlign="Middle">
            <asp:TableCell runat="server" Width="16%" VerticalAlign="Middle">
                <asp:Label ID="lblPriceFilter" runat="server" Text="Price:" CssClass="control-label"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" Width="35%">
                <asp:TextBox ID="txtMinPrice" runat="server" Width="100%"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center" Width="2%">
                    -
            </asp:TableCell>
            <asp:TableCell runat="server" Width="35%">
                <asp:TextBox ID="txtMaxPrice" runat="server" Width="100%"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell runat="server" Width="12%">
                <asp:Button ID="btnFilterPrice" runat="server" Text="Filter" CssClass="btn btn-default" OnClick="btnFilterPrice_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
    <div class="single-product-area">
        <div class="row">
            <div class="col-md-12">
                <p>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSortBy" AutoPostBack="true" Width="150px">
                        <asp:ListItem Text="Price" Value="Price" />
                        <asp:ListItem Text="Name" Value="Name" />
                        <asp:ListItem Selected="True" Text="Date" Value="Date" />
                    </asp:DropDownList>
                </p>
                <asp:Panel ID="pnlProducts" runat="server"></asp:Panel>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
</asp:Content>
