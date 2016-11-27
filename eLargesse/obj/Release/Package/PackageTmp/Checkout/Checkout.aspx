<%@ Page Title="" Language="C#" MasterPageFile="~/Sidebar.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="eLargesse.Checkout.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
     <div class="woocommerce">
        <div class="woocommerce-info">
            Returning customer? <a class="showlogin" data-toggle="collapse" href="#login-form-wrap" aria-expanded="false" aria-controls="login-form-wrap">Click here to login</a>
        </div>
        <div id="login-form-wrap" class="login collapse">
            <p>If you have shopped with us before, please enter your details in the boxes below. If you are a new customer please proceed to the Billing &amp; Shipping section.</p>
            <p class="form-row form-row-first">
                <asp:Label runat="server" AssociatedControlID="username">
                                    Username or email <span class="required">*</span>
                </asp:Label>
                <asp:TextBox runat="server" ID="username" CssClass="input-text" />
            </p>
            <p class="form-row form-row-last">
                <asp:Label runat="server" AssociatedControlID="password">
                                    Password <span class="required">*</span>
                </asp:Label>
                <asp:TextBox runat="server" ID="password" CssClass="input-text" />
            </p>
            <div class="clear"></div>


            <p class="form-row">
                <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="button" OnClick="btnLogin_Click" />
                <asp:Label runat="server" CssClass="inline" AssociatedControlID="rememberme">
                    <asp:CheckBox runat="server" Text="forever" ID="rememberme" />
                    Remember me </asp:Label>
            </p>
            <p class="lost_password">
                <asp:HyperLink runat="server" ID="LostPAssword">Lost your password?</asp:HyperLink>
            </p>

            <div class="clear"></div>
        </div>

        <div class="woocommerce-info">
            Have a coupon? <a class="showcoupon" data-toggle="collapse" href="#coupon-collapse-wrap" aria-expanded="false" aria-controls="coupon-collapse-wrap">Click here to enter your code</a>
        </div>

        <div id="coupon-collapse-wrap" method="post" class="checkout_coupon collapse">

            <p class="form-row form-row-first">
                <input type="text" value="" id="coupon_code" placeholder="Coupon code" class="input-text" name="coupon_code">
            </p>

            <p class="form-row form-row-last">
                <input type="submit" value="Apply Coupon" name="apply_coupon" class="button">
            </p>

            <div class="clear"></div>
        </div>
        <div class="checkout">
            <div id="customer_details" class="col2-set">
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
                    </div>
                </div>
                <div class="col-2">
                    <div class="woocommerce-shipping-fields">
                        <h3 id="ship-to-different-address">
                            <asp:Label runat="server" CssClass="checkbox" AssociatedControlID="ShipDiffAddressCheckbox">Ship to a different address?</asp:Label>
                            <asp:CheckBox runat="server" CssClass="input-checkbox" ID="ShipDiffAddressCheckbox" />
                        </h3>
                        <div class="shipping_address" style="display: block;">
                            <p id="shipping_country_field" class="form-row form-row-wide address-field update_totals_on_change validate-required woocommerce-validated">
                                <asp:Label runat="server" CssClass="" AssociatedControlID="ddlShippingCountry">Country<abbr title="required" class="required">*</abbr>
                                </asp:Label>
                                <asp:DropDownList ID="ddlShippingCountry" runat="server" 
                                    CssClass="country_to_state country_select" 
                                    DataTextField="Name" DataValueField="Id" DataSourceID="CountryDataSource" />
                            </p>
                            <p id="shipping_first_name_field" class="form-row form-row-first validate-required">
                                <asp:Label runat="server" class="" AssociatedControlID="shipping_first_name">
                                                        First Name
                                                    <abbr title="required" class="required">*</abbr>
                                </asp:Label>
                                <asp:TextBox runat="server" placeholder="" ID="shipping_first_name" CssClass="input-text" />
                            </p>

                            <p id="shipping_last_name_field" class="form-row form-row-last validate-required">
                                <asp:Label runat="server" CssClass="" AssociatedControlID="shipping_last_name">
                                                        Last Name
                                                    <abbr title="required" class="required">*</abbr>
                                </asp:Label>
                                <asp:TextBox runat="server" placeholder="" ID="shipping_last_name" CssClass="input-text" />
                            </p>
                            <div class="clear"></div>

                            <p id="shipping_company_field" class="form-row form-row-wide">
                                <asp:Label runat="server" CssClass="" AssociatedControlID="shipping_company">Company Name</asp:Label>
                                <asp:TextBox runat="server" placeholder="" ID="shipping_company" CssClass="input-text " />
                            </p>

                            <p id="shipping_address_1_field" class="form-row form-row-wide address-field validate-required">
                                <asp:Label runat="server" CssClass="" AssociatedControlID="shipping_address_1">
                                                        Address
                                                    <abbr title="required" class="required">*</abbr>
                                </asp:Label>
                                <asp:TextBox runat="server" placeholder="Street address" ID="shipping_address_1" CssClass="input-text " />
                            </p>
                            <p id="shipping_address_2_field" class="form-row form-row-wide address-field">
                                <asp:TextBox runat="server" placeholder="Apartment, suite, unit etc. (optional)" ID="shipping_address_2" CssClass="input-text " />
                            </p>
                            <p id="shipping_city_field" class="form-row form-row-wide address-field validate-required" data-o_class="form-row form-row-wide address-field validate-required">
                                <label class="" for="shipping_city">Town / City<abbr title="required" class="required">*</abbr></label>
                                <asp:TextBox runat="server" placeholder="Town / City" ID="shipping_city" CssClass="input-text " />
                            </p>
                            <p id="shipping_state_field" class="form-row form-row-first address-field validate-state" data-o_class="form-row form-row-first address-field validate-state">
                                <asp:Label runat="server" class="" AssociatedControlID="shipping_state">County</asp:Label>
                                <asp:TextBox runat="server" ID="shipping_state" placeholder="State / County" CssClass="input-text " />
                            </p>
                            <p id="shipping_postcode_field" class="form-row form-row-last address-field validate-required validate-postcode" data-o_class="form-row form-row-last address-field validate-required validate-postcode">
                                <asp:Label runat="server" CssClass="" AssociatedControlID="shipping_postcode">
                                                        Postcode
                                                    <abbr title="required" class="required">*</abbr>
                                </asp:Label>
                                <asp:TextBox runat="server" placeholder="Postcode / Zip" ID="shipping_postcode" CssClass="input-text " />
                            </p>
                            <div class="clear"></div>
                        </div>
                        <p id="order_comments_field" class="form-row notes">
                            <label class="" for="order_comments">Order Notes</label>
                            <textarea cols="5" rows="2" placeholder="Notes about your order, e.g. special notes for delivery." id="order_comments" class="input-text " name="order_comments"></textarea>
                        </p>
                    </div>
                </div>
            </div>
            <h3 id="order_review_heading">Your order</h3>

            <div id="order_review" style="position: relative;">
                <asp:Panel ID="pnlSummary" runat="server"></asp:Panel>
                <div id="payment">
                 
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="payment_methods methods" RepeatLayout="Table" Width="30%" CellPadding="20" CellSpacing="20" Height="200px" Font-Size="Medium" Font-Bold="True">
                        <asp:ListItem Selected="True" Value="EFT" Text="Direct Bank Transfer" />                       
                      
                        <asp:ListItem Value="PayPal" Text="Paypal" />
                       
                    </asp:RadioButtonList>
                     <img alt="PayPal Acceptance Mark" src="https://www.paypalobjects.com/webstatic/mktg/Logo/AM_mc_vs_ms_ae_UK.png" /><a title="What is PayPal?" onclick="javascript:window.open('https://www.paypal.com/gb/webapps/mpp/paypal-popup','WIPaypal','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=1060, height=700'); return false;" class="about_paypal" href="https://www.paypal.com/gb/webapps/mpp/paypal-popup">What is PayPal?</a>

                            <div style="display: none;" class="payment_box payment_method_paypal">
                                <p>Pay via PayPal; you can pay with your credit card if you don’t have a PayPal account.</p>
                            </div>

                    <div class="form-row place-order">

                        <asp:Button runat="server" ID="place_order" Text="Continue to Payment" CssClass="button alt" OnClick="place_order_Click" />
                    </div>

                    <div class="clear"></div>
                </div>
            </div>
        </div>
         <div class="row">
             <div class="col-md-12">
                 <h2>Test Code</h2>
                 <p>
                     <asp:TextBox runat="server" ID="txtTest" Width="100%" TextMode="MultiLine" Height="500px" />
                 </p>
             </div>
         </div>
    </div>
</asp:Content>
