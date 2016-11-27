<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="eLargesse.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <section>
        <div class="map map_height1">
            <div id="google-map" class="map_model" data-zoom="14" data-x="18.4406712" data-y="-33.9205086"></div>
            <ul class="map_locations">
                <li data-x="18.4406712" data-y="-33.9205086" data-basic="~/img/gmap_marker.png" data-active="~/img/gmap_marker_active.png">
                    <div class="location">
                        <h3 class="txt-clr1">flooring
                        <small>RETAILER
                        </small>
                        </h3>
                        <address>
                            <dl>
                                <dt>Free phone: </dt>
                                <dd class="phone"><a href="callto:#">800-2345-6789</a></dd>
                            </dl>
                            <dl>
                                <dt>Address: </dt>
                                <dd>4578 Marmora Road,Glasgow D04 89GR</dd>
                            </dl>
                            <dl>
                                <dt>Hours: </dt>
                                <dd>6am-4pm PST M-Th; &nbsp;&nbsp;  6am-3pm PST Fri</dd>
                            </dl>
                            <dl>
                                <dt>E-mail: </dt>
                                <dd><a href="mailto:#">info@demolink.org</a></dd>
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
                                <input type="text" name="name" placeholder="Name:" value="" />
                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*This is not a valid name.</span>
                            </label>

                            <label class="phone">
                                <input type="text" name="phone" placeholder="Phone:" value="" />

                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*This is not a valid phone.</span>
                            </label>
                            <label class="email">
                                <input type="text" name="email" placeholder="Email:" value="" />

                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*This is not a valid email.</span>
                            </label>

                            <label class="message">
                                <textarea name="message" placeholder="Message"></textarea>

                                <span class="empty-message">*This field is required.</span>
                                <span class="error-message">*The message is too short.</span>
                            </label>

                            <!--  <label class="recaptcha"> <span class="empty-message">*This field is required.</span> </label> -->

                            <div class="btn-wr text-primary">
                                <asp:Button runat="server" ID="btnContact" CssClass="button alt" Text="Submit"></asp:Button>
                            </div>
                        </fieldset>
                        <div class="modal fade response-message">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                            &times;
                                        </button>
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
