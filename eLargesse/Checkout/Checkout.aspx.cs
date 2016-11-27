using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLargesse.Controllers;
using eLargesse.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using GoogleMaps.LocationServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Data;
using System.Text;
using eLargesse.Logic;
using System.Net.Mail;
using System.Net;
using PayPal;
using PayPal.PayPalAPIInterfaceService;

namespace eLargesse.Checkout
{
    public partial class Checkout : System.Web.UI.Page
    {
        #region Fields
        private ClientController clientController;
        private CartController cartController;
        private OrderController orderController;
        private OrderDetailController orderDetailController;
        private ProductController productController;
        private AddressController addressController;
        private Client client;
        private GoogleLocationService locationService;
        private Order foundOrder;
        private List<OrderDetail> orderDetails;
        private eLargesse.Models.Address billingAddress;
        private AccountController accountController;

        private const string UserIp = "196.210.8.188";
        private const string Memo = "eLargesse Integration";
        private const string DeviceId = "Test Device";
        private const string checkoutCancel = "http://localhost::58543/Checkout/CheckoutCancel.aspx";
        private const string returnURL = "http://localhost::58543/Checkout/Checkout.aspx";
        private const string ipnURL = "http://localhost::58543/Checkout/CheckoutIPN.aspx";
        private const string currencyCode = "ZAF";
        private const string language = "en_US";

        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            clientController = new ClientController();
            cartController = new CartController();
            orderController = new OrderController();
            orderDetailController = new OrderDetailController();
            productController = new ProductController();
            addressController = new AddressController();
            locationService = new GoogleLocationService();
            accountController = new AccountController();

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["orderId"]))
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["clientId"]))
                    {
                        int orderId = Convert.ToInt32(Request.QueryString["orderId"]);
                        int clientId = Convert.ToInt32(Request.QueryString["clientId"]);

                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        client = clientController.GetClient(clientId);
                        billingAddress = addressController.GetAddressByClient(client.ID);
                        foundOrder = orderController.GetOrder(clientId);
                        orderDetails = orderDetailController.GetAllOrderDetails(orderId);
                        string guid = Context.User.Identity.GetUserId();

                        FillPageBillingAddress(billingAddress, client, manager.GetEmail(guid), manager.GetPhoneNumber(guid));
                        FillPageOrderDetails(orderDetails, foundOrder, client);
                    }

                }

            }
        }

        #endregion

        #region Methods

        private void CreateInvoice(DataTable dt, Order order, decimal subTotal, decimal vat, decimal shipping, decimal total, string reference)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<div style='width:595px;border:1px solid #000;margin:auto;'>");
                    sb.Append("<table style='width:595px;'>");
                    sb.Append("<tr><td style='text-align:right; background-color:#999;' colspan=4><b>INVOICE</b></td></tr>");
                    sb.Append("<tr><td colspan='4'></td></tr>");
                    sb.Append("");
                    sb.Append("<tr><td style='' style='width:20%; text-align:right'><b>Company:</b></td><td style='width:30%;'>eLargesse(Pty)Ltd</td><td style='width:20%; text-align:right'><b>Date:</b></td><td style='width:30%;'>");
                    sb.Append(DateTime.Now.ToShortDateString());
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td></td><td style='text-align:right;'><b>Invoice #:</b></td><td>");
                    sb.Append(order.Id);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td> <td></td><td style='text-align:right;'><b>Client ID:");
                    sb.Append("</b></td><td>");
                    sb.Append(order.CustomerId);
                    sb.Append("</td></tr>");
                    sb.Append("<tr style='height:15px;'><td colspan=4></td></tr>");
                    sb.Append("<tr><td style='text-align:right;'><b>BILL TO</b></td><td>");
                    sb.Append(order.FirstName + " " + order.LastName);
                    sb.Append("</td><td style='text-align:right;'><b>SHIP TO</b></td><td>");
                    sb.Append(order.FirstName + " " + order.LastName);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td></td><td></td><td></td></tr>");
                    sb.Append("<tr><td></td><td>");
                    sb.Append(order.Address);
                    sb.Append("</td><td></td><td>");
                    sb.Append(order.Address);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td>");
                    sb.Append(order.City + ", " + order.State + ", " + order.PostalCode);
                    sb.Append("</td><td></td><td>");
                    sb.Append(order.City + ", " + order.State + ", " + order.PostalCode);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td>");
                    sb.Append(order.Cell);
                    sb.Append("</td><td></td><td>");
                    sb.Append(order.Cell);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<table style='border:1px solid #000; width:595px;'>");
                    sb.Append("<tr>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "Product Description")
                        {
                            sb.Append("<th style='background-color:#d20b0c; color:#fff; width:33.32%;'>");
                            sb.Append(col.ColumnName);
                            sb.Append("</th>");
                        }
                        else
                        {
                            sb.Append("<th style='background-color:#d20b0c; color:#fff; width:16.67%;'>");
                            sb.Append(col.ColumnName);
                            sb.Append("</th>");
                        }

                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }

                    sb.Append("</table>");
                    sb.Append("<table style='width:595px;'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='width:135px;'></td> <td></td>");
                    sb.Append("<td style='text-align:right; width:99.167px;'><b>Sub-Total</b></td>");
                    sb.Append("<td style='border:1px solid #000; width:99.167px;'>");
                    sb.Append(subTotal.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px; text-align:right;'><b>Banking Details:</b></td>");
                    sb.Append("<td></td><td style='width:99.167px; text-align:right;'><b>VAT</b></td>");
                    sb.Append("<td style='width:99.167px; border:1px solid #000;'>");
                    sb.Append(vat.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px;text-align:right;'><b>Bank</b></td>");
                    sb.Append("<td>########</td>");
                    sb.Append("<td style='width:99.167px; text-align:right;'><b>Shipping</b></td>");
                    sb.Append("<td style='width:99.167px; border:1px solid #000;'>");
                    sb.Append(shipping.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px; text-align:right;'><b>Branch #:</b></td>");
                    sb.Append("<td>########</td><td style='width:99.167px; text-align:right;'><b>Total</b></td>");
                    sb.Append("<td style='width:99.167px; border:1px solid #000;'>");
                    sb.Append(total.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px; text-align:right;'><b>Account Number:</b></td>");
                    sb.Append("<td>########</td><td style='width:99.167px;'></td> <td style='width:99.167px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr><td  style='width:135px; text-align:right;'><b>Reference:</b></td><td>");
                    sb.Append(reference);
                    sb.Append("</td><td style='width:99.167px;'></td> <td style='width:99.167px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

                    StringReader sr = new StringReader(sb.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.AddTitle(order.Id.ToString());
                    pdfDoc.AddAuthor("eLaregesse(Pty)Ltd");

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        htmlparser.Close();
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        //var fileName = order.Id.ToString();
                        //var rootPath = "~/Invoices";
                        //var filePath = System.IO.Path.Combine(rootPath, fileName);
                        //System.IO.File.WriteAllBytes(filePath, bytes);

                        MailMessage mm = new MailMessage("alistair.bowendavies@gmail.com", "alistair.bowendavies@gmail.com");
                        mm.Subject = "iTextSharp PDF";
                        mm.Body = "iTextSharp PDF Attachment";
                        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "iTextSharpPDF.pdf"));
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential();
                        NetworkCred.UserName = "alistair.bowendavies@gmail.com";
                        NetworkCred.Password = "HereComesTheMonkey";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                   
                }
            }
        }

        private void CreateShippingInvoice(DataTable dt, Order order, Address shippingAddress, decimal subTotal, decimal vat, decimal shipping, decimal total, string reference)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<div style='width:595px;border:1px solid #000;margin:auto;'>");
                    sb.Append("<table style='width:595px;'>");
                    sb.Append("<tr><td style='text-align:right; background-color:#999;' colspan=4><b>INVOICE</b></td></tr>");
                    sb.Append("<tr><td colspan='4'></td></tr>");
                    sb.Append("");
                    sb.Append("<tr><td style='' style='width:20%; text-align:right'><b>Company:</b></td><td style='width:30%;'>eLargesse(Pty)Ltd</td><td style='width:20%; text-align:right'><b>Date:</b></td><td style='width:30%;'>");
                    sb.Append(DateTime.Now.ToShortDateString());
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td></td><td style='text-align:right;'><b>Invoice #:</b></td><td>");
                    sb.Append(order.Id);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td> <td></td><td style='text-align:right;'><b>Client ID:");
                    sb.Append("</b></td><td>");
                    sb.Append(order.CustomerId);
                    sb.Append("</td></tr>");
                    sb.Append("<tr style='height:15px;'><td colspan=4></td></tr>");
                    sb.Append("<tr><td style='text-align:right;'><b>BILL TO</b></td><td>");
                    sb.Append(order.FirstName + " " + order.LastName);
                    sb.Append("</td><td style='text-align:right;'><b>SHIP TO</b></td><td>");
                    sb.Append(order.FirstName + " " + order.LastName);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td></td><td></td><td></td></tr>");
                    sb.Append("<tr><td></td><td>");
                    sb.Append(order.Address);
                    sb.Append("</td><td></td><td>");
                    sb.Append(shippingAddress.Address1 + " " + shippingAddress.Address2);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td>");
                    sb.Append(order.City + ", " + order.State + ", " + order.PostalCode);
                    sb.Append("</td><td></td><td>");
                    sb.Append(shippingAddress.CIty + ", " + shippingAddress.State + ", " + shippingAddress.PostalCode);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td></td><td>");
                    sb.Append(order.Cell);
                    sb.Append("</td><td></td><td>");
                    sb.Append(order.Cell);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<table style='border:1px solid #000; width:595px;'>");
                    sb.Append("<tr>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "Product Description")
                        {
                            sb.Append("<th style='background-color:#d20b0c; color:#fff; width:33.32%;'>");
                            sb.Append(col.ColumnName);
                            sb.Append("</th>");
                        }
                        else
                        {
                            sb.Append("<th style='background-color:#d20b0c; color:#fff; width:16.67%;'>");
                            sb.Append(col.ColumnName);
                            sb.Append("</th>");
                        }

                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }

                    sb.Append("</table>");
                    sb.Append("<table style='width:595px;'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='width:135px;'></td> <td></td>");
                    sb.Append("<td style='text-align:right; width:99.167px;'><b>Sub-Total</b></td>");
                    sb.Append("<td style='border:1px solid #000; width:99.167px;'>");
                    sb.Append(subTotal.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px; text-align:right;'><b>Banking Details:</b></td>");
                    sb.Append("<td></td><td style='width:99.167px; text-align:right;'><b>VAT</b></td>");
                    sb.Append("<td style='width:99.167px; border:1px solid #000;'>");
                    sb.Append(vat.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px;text-align:right;'><b>Bank</b></td>");
                    sb.Append("<td>########</td>");
                    sb.Append("<td style='width:99.167px; text-align:right;'><b>Shipping</b></td>");
                    sb.Append("<td style='width:99.167px; border:1px solid #000;'>");
                    sb.Append(shipping.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px; text-align:right;'><b>Branch #:</b></td>");
                    sb.Append("<td>########</td><td style='width:99.167px; text-align:right;'><b>Total</b></td>");
                    sb.Append("<td style='width:99.167px; border:1px solid #000;'>");
                    sb.Append(total.ToString("F2"));
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td style='width:135px; text-align:right;'><b>Account Number:</b></td>");
                    sb.Append("<td>########</td><td style='width:99.167px;'></td> <td style='width:99.167px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr><td  style='width:135px; text-align:right;'><b>Reference:</b></td><td>");
                    sb.Append(reference);
                    sb.Append("</td><td style='width:99.167px;'></td> <td style='width:99.167px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

                    StringReader sr = new StringReader(sb.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        htmlparser.Close();
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        MailMessage mm = new MailMessage("alistair.bowendavies@gmail.com", "alistair.bowendavies@gmail.com");
                        mm.Subject = "iTextSharp PDF";
                        mm.Body = "iTextSharp PDF Attachment";
                        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "iTextSharpPDF.pdf"));
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential();
                        NetworkCred.UserName = "alistair.bowendavies@gmail.com";
                        NetworkCred.Password = "HereComesTheMonkey";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }
        }
        //private Order CreateOrder(Client client, eLargesse.Models.Address address, string Cell, string Email, decimal Total)
        //{
        //    Order ord = new Order()
        //    {
        //        CustomerId = client.ID,
        //        FirstName = client.FirstName,
        //        LastName = client.LastName,
        //        Address = address.Address1 + ", " + address.Address2,
        //        City = address.CIty,
        //        State = address.State,
        //        Country = address.Country,
        //        OrderDate = DateTime.Now,
        //        HasBeenShipping = false,
        //        PostalCode = address.PostalCode,
        //        Total = Total,
        //        Cell = Cell,
        //        Email = Email
        //    };

        //    return ord;
        //}

        private void FillPageOrderDetails(List<OrderDetail> orderDetail, Order order, Client client)
        {
            if (orderDetail != null)
            {


                Table table = new Table { CssClass = "shop_table" };

                /*----------------Cart Item Header Row -------------------*/
                TableHeaderRow a = new TableHeaderRow();

                Label a1Header = new Label { Text = "Product" };
                Label a2Header = new Label { Text = "Total" };

                TableCell a1 = new TableCell { CssClass = "product-name" };
                TableCell a2 = new TableCell { CssClass = "product-total" };

                a1.Controls.Add(a1Header);
                a2.Controls.Add(a2Header);

                a.Cells.Add(a1);
                a.Cells.Add(a2);

                table.Rows.Add(a);

                /*-------------------Cart Items Row------------------------*/

                foreach (OrderDetail p in orderDetail)
                {
                    decimal quantity = Convert.ToDecimal(p.Quantity);
                    decimal unitPrice = Convert.ToDecimal(p.UnitPrice);
                    decimal subTotal = quantity * unitPrice;

                    Label productName = new Label { Text = p.ProductName };
                    Label productTotal = new Label { Text = "R " + subTotal.ToString("F2") };

                    TableRow b = new TableRow() { CssClass = "cart_item" };

                    TableCell b1 = new TableCell();
                    TableCell b2 = new TableCell();

                    b1.Controls.Add(productName);
                    b2.Controls.Add(productTotal);

                    b.Cells.Add(b1);
                    b.Cells.Add(b2);

                    table.Rows.Add(b);

                }

                /*----------------Total of SubTotals Row---------------------------*/
                
                decimal totalInCart = Convert.ToDecimal((from x in orderDetail select x.UnitPrice * x.Quantity).Sum());


                TableRow c = new TableRow();
                Label c1Header = new Label { Text = "Sub-Total" };
                Label c2Label = new Label { Text = "R " + totalInCart.ToString("F2") };

                TableHeaderCell c1 = new TableHeaderCell { CssClass = "cart-subtotal" };
                TableCell c2 = new TableCell();

                c1.Controls.Add(c1Header);
                c2.Controls.Add(c2Label);

                c.Cells.Add(c1);
                c.Cells.Add(c2);

                table.Rows.Add(c);

                /*--------------------Shipping Row---------------------------*/
                Address address = addressController.GetAddressByClient(client.ID);
                decimal shipping = GetShippingCost(address);

                TableFooterRow d = new TableFooterRow();
                Label d1Header = new Label { Text = "Shipping and Handling" };
                Label d2Label = new Label { Text = "R " + shipping.ToString("F2") };

                TableHeaderCell d1 = new TableHeaderCell { CssClass = "shipping" };
                TableCell d2 = new TableCell();

                d1.Controls.Add(d1Header);
                d2.Controls.Add(d2Label);

                d.Cells.Add(d1);
                d.Cells.Add(d2);

                table.Rows.Add(d);

                /*--------------------Tax Row-----------------------------*/

                decimal vat = totalInCart * 0.14M;

                TableFooterRow e = new TableFooterRow();
                TableHeaderCell e1 = new TableHeaderCell();
                TableCell e2 = new TableCell();

                Label e1Header = new Label { Text = "VAT" };
                Label e2Label = new Label { Text = "R " + vat.ToString("F2") };


                e1.Controls.Add(e1Header);
                e2.Controls.Add(e2Label);

                e.Cells.Add(e1);
                e.Cells.Add(e2);

                table.Rows.Add(e);

                /*-------------------Total Row---------------------------*/

                TableFooterRow f = new TableFooterRow();
                TableHeaderCell f1 = new TableHeaderCell();
                TableCell f2 = new TableCell();

                Label f1Header = new Label { Text = "Total" };
                Label f2Label = new Label { Text = "R " + order.Total.ToString("F2") };

                f1.Controls.Add(f1Header);
                f2.Controls.Add(f2Label);

                f.Cells.Add(f1);
                f.Cells.Add(f2);

                table.Rows.Add(f);

                /*-----------------Add Table----------------------------*/

                pnlSummary.Controls.Add(table);
            }
        }


        private void FillPageBillingAddress(eLargesse.Models.Address address, Client client, string email, string cell)
        {
            if (address != null)
            {
                billing_address_1.Text = address.Address1;
                billing_address_1.Text = address.Address2;
                billing_city.Text = address.CIty;
                billing_state.Text = address.State;
                billing_postcode.Text = address.PostalCode;
                billing_country.SelectedValue = address.Country;
            }
            if (cell == "")
            {
                billing_phone.Text = "";
            }
            else
            {
                billing_phone.Text = cell;
            }
            billing_first_name.Text = client.FirstName;
            billing_last_name.Text = client.LastName;
            billing_email.Text = email;
        }

        private Address GetShippingAddress(int client)
        {
            if (shipping_address_1 == null)
            {
                return null;
            }
            else
            {
                Address address = new Address()
                {
                    Address1 = shipping_address_1.Text,
                    Address2 = shipping_address_2.Text,
                    CIty = shipping_city.Text,
                    ClientId = client,
                    Country = ddlShippingCountry.SelectedValue,
                    PostalCode = shipping_postcode.Text,
                    State = shipping_state.Text
                };

                return address;
            }
        }

        #region Shipping Cost Calculation
        private decimal GetShippingCost(Address address)
        {
            decimal shippingCost = 0.00M;

            if (address != null)
            {
                AddressData dataClient = new AddressData()
                {
                    Address = address.Address1 + ", " + address.Address2,
                    City = address.CIty,
                    Country = address.Country,
                    State = address.State,
                    Zip = address.PostalCode
                };

                GoogleLocationService service = new GoogleLocationService();
                var point = service.GetLatLongFromAddress(dataClient);
                var clientLong = point.Longitude;
                var clientLat = point.Latitude;
                var eLargesseLat = -34.039970;
                var eLargesseLong = 18.433736;

                double distance = Distance(clientLat, clientLong, eLargesseLat, eLargesseLong, 'K');

                if (distance <= 50)
                {
                    shippingCost = 50.00M;
                }
                else if (distance > 50 && distance <= 100)
                {
                    shippingCost = 100.00M;
                }
                else if (distance > 100 && distance <= 200)
                {
                    shippingCost = 150.00M;
                }
                else if (distance > 200 && distance <= 400)
                {
                    shippingCost = 250.00M;
                }
                else if (distance > 400 && distance <= 700)
                {
                    shippingCost = 400.00M;
                }
                else
                {
                    shippingCost = 600.00M;
                }
            }
            return shippingCost;
        }

        private double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        #endregion

        #endregion

        #region Click Events

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }

        protected void place_order_Click(object sender, EventArgs e)
        {
            int ordId = Convert.ToInt32(Request.QueryString["orderId"]);
            int cliId = Convert.ToInt32(Request.QueryString["clientId"]);

            Address address = addressController.GetAddressByClient(cliId);

            foundOrder = orderController.GetOrder(cliId);
            orderDetails = orderDetailController.GetAllOrderDetails(ordId);

            //Get Totals for Payment
            decimal shipping = GetShippingCost(address);
            decimal subtotal = Convert.ToDecimal((from x in orderDetails select x.Quantity * x.UnitPrice).Sum());
            decimal vat = 0.14M * subtotal;
            decimal total = subtotal + shipping;

            if (RadioButtonList1.SelectedValue == "EFT")
            {

                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[5]
                {
                new DataColumn("Product ID"),
                new DataColumn("Product Description"),
                new DataColumn("Quantity"),
                new DataColumn("Unit Price"),
                new DataColumn("Total")
                });
                foreach (OrderDetail ordD in orderDetails)
                {
                    decimal subTotal = Convert.ToDecimal(ordD.Quantity * ordD.UnitPrice);
                    dt.Rows.Add(ordD.ProductId, ordD.ProductName, ordD.Quantity, ordD.UnitPrice, subTotal);
                }
                Address add = GetShippingAddress(cliId);
                if (ShipDiffAddressCheckbox.Checked)
                {
                    if (add.Address1 == "")
                    {
                        CreateInvoice(dt, foundOrder, subtotal, vat, shipping, total, foundOrder.Id.ToString());
                    }
                    else
                    {
                        CreateShippingInvoice(dt, foundOrder, add, subtotal, vat, shipping, total, foundOrder.Id.ToString());
                    }
                }
                else
                {
                    if (add.Address1 == "")
                    {
                        CreateInvoice(dt, foundOrder, subtotal, vat, shipping, total, foundOrder.Id.ToString());
                    }
                    else
                    {
                        CreateShippingInvoice(dt, foundOrder, add, subtotal, vat, shipping, total, foundOrder.Id.ToString());
                    }
                }
                        

                Response.Redirect("~/Checkout/CheckoutComplete.aspx?orderId=" + foundOrder.Id + "&method=EFT");
            }
            else if (RadioButtonList1.SelectedValue == "PayPal")
            {
                Session["payment_amt"] = orderDetailController.GetTotalInCart(cliId);
                Response.Redirect("~/Checkout/CheckoutStart.aspx");
            }
        }

        protected void updateProfile_Click(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Client client = clientController.GetClientByGUID(Context.User.Identity.GetUserId());
                Order order = orderController.GetOrder(client.ID);
                Order updateOrder = new Order()
                {
                    Address = billing_address_1.Text + " " + billing_address_2.Text,
                    Cell = billing_phone.Text,
                    City = billing_city.Text,
                    Country = billing_country.SelectedValue,
                    CustomerId = client.ID,
                    Email = billing_email.Text,
                    FirstName = billing_first_name.Text,
                    LastName = billing_last_name.Text,
                    HasBeenShipping = false,
                    OrderDate = order.OrderDate,
                    PaymentProcessed = false,
                    PaymentTransactionId = null,
                    PostalCode = billing_postcode.Text,
                    State = billing_state.Text,
                    Total = order.Total
                };
                Address address = new Address()
                {
                    Address1 = billing_address_1.Text,
                    Address2 = billing_address_2.Text,
                    CIty = billing_city.Text,
                    Country = billing_country.SelectedValue,
                    PostalCode = billing_postcode.Text,
                    State = billing_state.Text,
                    ClientId = client.ID
                };
                accountController.UpdateCell(Context.User.Identity.GetUserId(), billing_phone.Text);
                addressController.Insert(address);

                bool result = orderController.UpdateOrder(order.Id, updateOrder);
                if (result)
                {
                    UpdateResultLabel.Text = "Successfully Updated";
                    UpdateResultLabel.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    UpdateResultLabel.Text = "Error while updating, please make sure all field data types were correct!";
                    UpdateResultLabel.ForeColor = System.Drawing.Color.Red;
                }
            }

        }

        #endregion

    }
}