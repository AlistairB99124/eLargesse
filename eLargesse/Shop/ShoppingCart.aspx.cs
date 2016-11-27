using eLargesse.Controllers;
using eLargesse.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleMaps.LocationServices;

namespace eLargesse.Shop
{
    public partial class ShoppingCart : Page
    {

        #region Fields
        private ClientController clientController;
        private CartController cartController;
        private OrderController orderController;
        private OrderDetailController orderDetailController;
        private ProductController productController;
        private AddressController addressController;
        private Address address;
        private Client client;
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

            if (Context.User.Identity.IsAuthenticated)
            {
                client = clientController.GetClientByGUID(Context.User.Identity.GetUserId());
                address = addressController.GetAddressByClient(client.ID);
                GetPurchasesInCart(client.ID);
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
        #endregion

        #region Click Events

        private void Delete_Me(object sender, EventArgs e)
        {
            Button selectedLink = (Button)sender;
            string link = selectedLink.ID.Replace("del", "");
            int cartID = Convert.ToInt32(link);

            cartController.Delete(cartID);

            Response.Redirect("~/Shop/ShoppingCart.aspx");
        }

        private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList selectedList = (DropDownList)sender;
            int quantity = Convert.ToInt32(selectedList.SelectedValue);
            int cartId = Convert.ToInt32(selectedList.ID);

            cartController.UpdateQuantity(cartId, quantity);

            Response.Redirect("~/Shop/ShoppingCart.aspx");
        }

        private void Delete_Item(object sender, EventArgs e)
        {
            LinkButton selectedLink = (LinkButton)sender;
            string link = selectedLink.ID.Replace("del", "");
            int cartID = Convert.ToInt32(link);

            cartController.Delete(cartID);

            Response.Redirect("~/Shop/ShoppingCart.aspx");
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string guid = Context.User.Identity.GetUserId();
            Order order = CreateOrder(client, address, manager.GetPhoneNumber(guid), manager.GetEmail(guid));
            orderController.InsertOrder(order);

            Order foundOrder = orderController.GetOrder(client.ID);
            List<Cart> carts = cartController.GetOrdersInCart(client.ID);

            /* Fill Order */
            foreach (Cart c in carts)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = foundOrder.Id,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Quantity = Convert.ToInt32(c.Amount),
                    UnitPrice = c.Product.Price,
                    Discount = null
                };

                orderDetailController.Insert(orderDetail);

            }
            /* Empty Cart */
            cartController.EmptyCart(carts);

            /* Redirect to Checkout*/
            Response.Redirect("~/Checkout/Checkout.aspx?orderId=" + foundOrder.Id + "&clientId=" + foundOrder.CustomerId);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnCoupon_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        private decimal GetTotalInCart(int clientId)
        {
            DropDownList ddlCurrency = Master.DDLCurrency;
            Address address = addressController.GetAddressByClient(clientId);
            decimal shippingRand = GetShippingCost(address);
            decimal shippingConverted = Master.ConvertPrice(shippingRand, ddlCurrency.Text);

            decimal subTotal = Convert.ToDecimal(cartController.GetTotalInCart(clientId));
            decimal vat = subTotal * 0.14M;
            decimal totalAmount = subTotal + shippingConverted;

            return totalAmount;
        }

        private void GetPurchasesInCart(int userID)
        {
            Client aClient = clientController.GetClientByGUID(Context.User.Identity.GetUserId());
            Address aAddress = addressController.GetAddressByClient(aClient.ID);

            DropDownList ddlCurrency = Master.DDLCurrency;
            decimal shippingRand = GetShippingCost(aAddress);
            decimal shippingConverted = Master.ConvertPrice(shippingRand, ddlCurrency.Text);
            decimal totalbeforeShippingAmount = 0;
            List<eLargesse.Models.Cart> purchaseList = cartController.GetOrdersInCart(userID);
            CreateShopTable(purchaseList, out totalbeforeShippingAmount);

            decimal subTotal = Convert.ToInt32(cartController.GetTotalInCart(aClient.ID));
            decimal vat = subTotal * 0.14M;
            decimal totalAmount = subTotal + shippingConverted;

            lblShipping.Text = ddlCurrency.SelectedValue + " " + shippingConverted.ToString("F2");
            lblSubtotal.Text = ddlCurrency.SelectedValue + " " + subTotal.ToString("F2");
            lblTax.Text = ddlCurrency.SelectedValue + " " + vat.ToString("F2");
            lblTotal.Text = ddlCurrency.SelectedValue + " " + totalAmount.ToString("F2");

        }

        private void CreateShopTable(List<eLargesse.Models.Cart> purchaseList, out decimal subTotal)
        {
            DropDownList ddlCurrency = Master.DDLCurrency;


            subTotal = new decimal();
            Table table = new Table { CssClass = "shop_table cart" };
            TableHeaderRow a = new TableHeaderRow();
            TableRow c = new TableRow { Width = new Unit("100%") };


            TableHeaderCell a1 = new TableHeaderCell { CssClass = "product-remove", Text = "&nbsp;" };
            TableHeaderCell a2 = new TableHeaderCell { CssClass = "product-thumbnail", Text = "&nbsp;" };
            TableHeaderCell a3 = new TableHeaderCell { CssClass = "product-name", Text = "Product" };
            TableHeaderCell a4 = new TableHeaderCell { CssClass = "product-price", Text = "Price" };
            TableHeaderCell a5 = new TableHeaderCell { CssClass = "product-quantity", Text = "Quantity" };
            TableHeaderCell a6 = new TableHeaderCell { CssClass = "product-subtotal", Text = "Total" };

            a.Cells.Add(a1);
            a.Cells.Add(a2);
            a.Cells.Add(a3);
            a.Cells.Add(a4);
            a.Cells.Add(a5);
            a.Cells.Add(a6);

            table.Rows.Add(a);

            foreach (eLargesse.Models.Cart cart in purchaseList)
            {
                eLargesse.Models.Product product = productController.GetProduct(cart.ProductId);
                decimal randValue = Convert.ToDecimal(product.Price);
                decimal productPrice = Master.ConvertPrice(randValue, ddlCurrency.Text);

                Button btnDelete = new Button { Text = "X", ID = "del" + cart.Id };
                btnDelete.Click += Delete_Me;

                ImageButton btnImage = new ImageButton
                {
                    ImageUrl = string.Format("~/img/Products/{0}", product.Image),
                    PostBackUrl = string.Format("~/Shop/Product.aspx?id={0}",
                    product.Id),
                    Width = 200
                };

                int[] amount = Enumerable.Range(1, 20).ToArray();

                DropDownList ddlAmount = new DropDownList
                {
                    DataSource = amount,
                    AppendDataBoundItems = true,
                    AutoPostBack = true,
                    ID = cart.Id.ToString()
                };

                ddlAmount.DataBind();
                ddlAmount.SelectedValue = cart.Amount.ToString();
                ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged;

                TableRow b = new TableRow { CssClass = "cart_item" };


                TableCell b1 = new TableCell { CssClass = "product-remove" };
                TableCell b2 = new TableCell { CssClass = "product-thumbnail" };
                TableCell b3 = new TableCell { CssClass = "product-name", Text = string.Format("<h4>{0}</h4>", product.Name) };
                TableCell b4 = new TableCell { CssClass = "product-price", Text = string.Format("<h6>{0}</h6>", ddlCurrency.SelectedValue + " " + productPrice.ToString("F2")) };
                TableCell b5 = new TableCell { CssClass = "product-quantity" };
                TableCell b6 = new TableCell { CssClass = "product-subtotal", Text = string.Format("<h6>{0}</h6>", ddlCurrency.SelectedValue + " " + (cart.Amount * productPrice).ToString("F2")) };

                b1.Controls.Add(btnDelete);
                b2.Controls.Add(btnImage);
                b5.Controls.Add(ddlAmount);

                b.Cells.Add(b1);
                b.Cells.Add(b2);
                b.Cells.Add(b3);
                b.Cells.Add(b4);
                b.Cells.Add(b5);
                b.Cells.Add(b6);

                table.Rows.Add(b);

                subTotal += (Convert.ToDecimal(cart.Amount) * Convert.ToDecimal(productPrice));
            }
          

            Button btnUpdate = new Button
            {
                Text = "UPDATE CART",
                CssClass = "btn-primary btn"
            };

            btnUpdate.Click += btnUpdate_Click;

            LinkButton btnPayPal = new LinkButton
            {
                CssClass = "btn btn-success",
                Text = "CHECKOUT"
            };
            btnPayPal.Click += Submit_Click;

            TableCell c1 = new TableCell { Width = new Unit("30%") };
            TableCell c2 = new TableCell { Width = new Unit("30%") };
            TableCell c3 = new TableCell { Width = new Unit("20%") };
            TableCell c4 = new TableCell { Width = new Unit("20%") };

          
            c3.Controls.Add(btnUpdate);
            c4.Controls.Add(btnPayPal);

            c.Cells.Add(c1);
            c.Cells.Add(c2);
            c.Cells.Add(c3);
            c.Cells.Add(c4);

            Table tableFooter = new Table()
            {
                 Width=new Unit("100%")
            };

            tableFooter.Rows.Add(c);

            pnlCart.Controls.Add(table);
            pnlCart.Controls.Add(tableFooter);

            Session[User.Identity.GetUserId()] = purchaseList;

        }

        private Order CreateOrder(Client client, Address address, string Cell, string Email)
        {
            string totalBefore = lblTotal.Text.Replace("R ", "");
            decimal totalAfter = Convert.ToDecimal(totalBefore);

            Address aAddress = addressController.GetAddressByClient(client.ID);
            Order ord;

            if (aAddress != null && Cell != null)
            {
                ord = new Order()
                {
                    CustomerId = client.ID,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Address = address.Address1 + ", " + address.Address2,
                    City = address.CIty,
                    State = address.State,
                    Country = address.Country,
                    OrderDate = DateTime.Now,
                    HasBeenShipping = false,
                    PostalCode = address.PostalCode,
                    Total = totalAfter,
                    Cell = Cell,
                    Email = Email,
                    PaymentProcessed = false
                };
            }
            else if (aAddress != null && Cell == null)
            {
                ord = new Order()
                {
                    CustomerId = client.ID,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Address = address.Address1 + ", " + address.Address2,
                    City = address.CIty,
                    State = address.State,
                    Country = address.Country,
                    OrderDate = DateTime.Now,
                    HasBeenShipping = false,
                    PostalCode = address.PostalCode,
                    Total = totalAfter,
                    Cell = " ",
                    Email = Email,
                    PaymentProcessed = false
                };
            }
            else if (aAddress == null && Cell != null)
            {
                ord = new Order()
                {
                    CustomerId = client.ID,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Address = " ",
                    City = " ",
                    State = " ",
                    Country = " ",
                    OrderDate = DateTime.Now,
                    HasBeenShipping = false,
                    PostalCode = " ",
                    Total = totalAfter,
                    Cell = Cell,
                    Email = Email,
                    PaymentProcessed = false
                };
            }
            else
            {
                ord = new Order()
                {
                    CustomerId = client.ID,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Address = " ",
                    City = " ",
                    State = " ",
                    Country = " ",
                    OrderDate = DateTime.Now,
                    HasBeenShipping = false,
                    PostalCode = " ",
                    Total = totalAfter,
                    Cell = " ",
                    Email = Email,
                    PaymentProcessed = false
                };
            }


            return ord;
        }

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

    }
}