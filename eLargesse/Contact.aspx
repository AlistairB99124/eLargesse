<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="eLargesse.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <section>, 
        <div class="map map_height1">
            <div id="google-map" class="map_model" data-zoom="14" data-x="18.433803" data-y="-34.039953"></div>
            <ul class="map_locations">
                <li data-x="18.433803" data-y="-34.039953" data-basic="img/gmap_marker.png" data-active="img/gmap_marker_active.png">
                    <div class="location">
                        <h3 class="txt-clr1"><small>e</small>
                            Largesse
                        </h3>
                        <address>
                            <dl>
                                <dt>Free phone: </dt>
                                <dd class="phone"><a href="callto:#">+27-79-364-2185</a></dd>
                            </dl>
                            <dl>
                                <dt>Address: </dt>
                                <dd>1 Airlie Lane, Constantia, Cape Town, 7806</dd>
                            </dl>
                            <dl>
                                <dt>Hours: </dt>
                                <dd>6am-4pm PST M-Th; &nbsp;&nbsp;  6am-3pm PST Fri</dd>
                            </dl>
                            <dl>
                                <dt>E-mail: </dt>
                                <dd><a href="mailto:#">Admin@elargesse.co.za</a></dd>
                            </dl>
                        </address>
                    </div>
                </li>
            </ul>
        </div>
    </section>
    <div style="width: 100%; height: 20px; clear: both;"></div>
    <div class="container">
        <div class="col-md-6">
            <address>
                <strong><small>e</small>Largesse</> (Pty) Ltd</strong><br />
                1 Airlie Lane<br />
                Constantia<br />
                Cape Town, 7806<br />
                South Africa<br />
                <abbr title="Phone">P:</abbr>
                <a href="callto:+27793642185">+27-79-364-2185</a>
            </address>

            <address>
                <strong>Information:</strong>   <a href="mailto:info@elargesse.co.za">info@elargesse.co.za</a><br />
                <strong>Queries:</strong> <a href="mailto:secretary@elargesse.co.za">secretary@elargesse.co.za</a>
            </address>
        </div>
        <div class="col-md-6">
            <section class="well well4">
                <div>
                    <h2>contact
            <small>form
            </small>
                    </h2>
                    <div id="contact-form" class='contact-form'>
                        <div class="contact-form-loader"></div>
                        <fieldset>
                            <label class="name">
                                <asp:TextBox runat="server" ID="Name" placeholder="Name" />
                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*This is not a valid name.</span>
                            </label>

                            <label class="phone">
                                <asp:TextBox runat="server" ID="Phone" placeholder="Phone:" />
                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*This is not a valid phone.</span>
                            </label>
                            <label class="email">
                                <asp:TextBox runat="server" ID="Email" placeholder="Email:"/>
                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*This is not a valid email.</span>
                            </label>

                            <label class="message">
                                <asp:TextBox runat="server" ID="Message" placeholder="Your message....." TextMode="MultiLine" />
                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*The message is too short.</span>
                            </label>

                            <!--  <label class="recaptcha"> <span class="empty-message">*This field is required.</span> </label> -->

                            <div class="btn-wr text-primary">
                                <asp:Button runat="server" ID="btnContact" CssClass="btn btn-primary" Height="45px" Text="Submit" OnClick="btnContact_Click"></asp:Button>
                            </div>
                        </fieldset>
                        <div class="modal fade response-message">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:button runat="server" CssClass="close" ID="btnSendEmail" OnClick="btnSendEmail_Click" Text="Submit" />
                                        <h4 class="modal-title">Modal title</h4>
                                    </div>
                                    <div class="modal-body">
                                        You message has been sent! We will be in touch soon.
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>

    </div>
        </div>
    <div style="clear: both;"></div>
</asp:Content>
