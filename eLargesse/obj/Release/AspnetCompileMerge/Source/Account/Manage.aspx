<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="eLargesse.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2><%: Title %>.</h2>

        <div>
            <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
                <p class="text-success"><%: SuccessMessage %></p>
            </asp:PlaceHolder>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-horizontal">
                    <h4>Change your account settings</h4>
                    <hr />
                    <dl class="dl-horizontal">
                        <dt>Password:</dt>
                        <dd>
                            <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
                            <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                        </dd>
                        <dt>External Logins:</dt>
                        <dd><%: LoginsCount %>
                            <asp:HyperLink NavigateUrl="/Account/ManageLogins" Text="[Manage]" runat="server" />

                        </dd>
                        <%--
                        Phone Numbers can used as a second factor of verification in a two-factor authentication system.
                        See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                        for details on setting up this ASP.NET application to support two-factor authentication using SMS.
                        Uncomment the following blocks after you have set up two-factor authentication
                        --%>
                        <%--
                    <dt>Phone Number:</dt>
                    <% if (HasPhoneNumber)
                       { %>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[Add]" />
                    </dd>
                    <% }
                       else
                       { %>
                    <dd>
                        <asp:Label Text="" ID="PhoneNumber" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[Change]" /> &nbsp;|&nbsp;
                        <asp:LinkButton Text="[Remove]" OnClick="RemovePhone_Click" runat="server" />
                    </dd>
                    <% } %>
                        --%>

                        <dt>Two-Factor Authentication:</dt>
                        <dd>
                            <p>
                                There are no two-factor authentication providers configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                                for details on setting up this ASP.NET application to support two-factor authentication.
                            </p>
                            <% if (TwoFactorEnabled)
                                { %>
                            <%--
                        Enabled
                        <asp:LinkButton Text="[Disable]" runat="server" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
                            --%>
                            <% }
                                else
                                { %>
                            <%--
                        Disabled
                        <asp:LinkButton Text="[Enable]" CommandArgument="true" OnClick="TwoFactorEnable_Click" runat="server" />
                            --%>
                            <% } %>
                        </dd>
                    </dl>
                </div>
            </div>
            <div id="customer_details" class="col-md-6">
                 <div class="col-1">
                    <div class="woocommerce-billing-fields">
                        <h3>Billing Details</h3>
                        <p id="billing_country_field" class="form-row form-row-wide address-field update_totals_on_change validate-required woocommerce-validated">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_country">
                                                Country
                                                <abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:DropDownList ID="billing_country" runat="server" CssClass="country_to_state country_select" DataTextField="Name" DataValueField="Name" DataSourceID="CountryDataSource">
                                <asp:ListItem Selected="True" Value="null">Select a Country</asp:ListItem>

                            </asp:DropDownList>
                            
                            <asp:SqlDataSource ID="CountryDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLargesse.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Country]"></asp:SqlDataSource>
                            
                        </p>

                        <p id="billing_first_name_field" class="form-row form-row-first validate-required">
                            <asp:Label CssClass="" runat="server" AssociatedControlID="billing_first_name">
                                                First Name
                                                <abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="" ID="billing_first_name" />
                        </p>

                        <p id="billing_last_name_field" class="form-row form-row-last validate-required">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_last_name">
                                                Last Name
                                                <abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="" ID="billing_last_name" CssClass="input-text" />
                        </p>
                        <div class="clear"></div>

                        <p id="billing_company_field" class="form-row form-row-wide">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_company">Company Name</asp:Label>
                            <asp:TextBox runat="server" placeholder="" ID="billing_company" CssClass="input-text" />
                        </p>
                        <p id="billing_address_1_field" class="form-row form-row-wide address-field validate-required">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_address_1">Address<abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="Street address" ID="billing_address_1" CssClass="input-text" />
                        </p>
                        <p id="billing_address_2_field" class="form-row form-row-wide address-field">
                            <asp:TextBox runat="server" placeholder="Apartment, suite, unit etc. (optional)" ID="billing_address_2" CssClass="input-text" />
                        </p>
                        <p id="billing_city_field" class="form-row form-row-wide address-field validate-required" data-o_class="form-row form-row-wide address-field validate-required">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_city">Town / City<abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="Town / City" ID="billing_city" CssClass="input-text" />
                        </p>
                        <p id="billing_state_field" class="form-row form-row-first address-field validate-state" data-o_class="form-row form-row-first address-field validate-state">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_state">County</asp:Label>
                            <asp:TextBox runat="server" ID="billing_state" placeholder="State / County" CssClass="input-text" />
                        </p>
                        <p id="billing_postcode_field" class="form-row form-row-last address-field validate-required validate-postcode" data-o_class="form-row form-row-last address-field validate-required validate-postcode">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_postcode">Postcode<abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="Postcode / Zip" ID="billing_postcode" CssClass="input-text" />
                        </p>
                        <div class="clear"></div>
                        <p id="billing_email_field" class="form-row form-row-first validate-required validate-email">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_email">
                                                Email Address
                                                <abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="" ID="billing_email" CssClass="input-text" />
                        </p>
                        <p id="billing_phone_field" class="form-row form-row-last validate-required validate-phone">
                            <asp:Label runat="server" CssClass="" AssociatedControlID="billing_phone">
                                                Phone
                                                <abbr title="required" class="required">*</abbr>
                            </asp:Label>
                            <asp:TextBox runat="server" placeholder="" ID="billing_phone" CssClass="input-text" />
                        </p>
                        <div class="clear"></div>
                        <div class="create-account">
                            <p>Update your billing address information</p>
                            <p id="account_password_field" class="form-row validate-required">
                               
                                <asp:Button runat="server" Text="Update" placeholder="Password" ID="btnUpdateAddress" CssClass="btn btn-primary" OnClick="btnUpdateAddress_Click" />
                            </p>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
