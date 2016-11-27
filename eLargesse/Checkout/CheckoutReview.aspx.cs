using eLargesse.Controllers;
using eLargesse.Models;
using eLargesse.Logic;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse.Checkout
{
    public partial class CheckoutReview : System.Web.UI.Page
    {
        private ClientController clientController;
        private CartController cartController;

        protected void Page_Load(object sender, EventArgs e)
        {
            clientController = new ClientController();
            cartController = new CartController();

            if (!IsPostBack)
            {
                string userId = User.Identity.GetUserId();
                Client client = clientController.GetClientByGUID(userId);
                int customerId = client.ID;

                NVPAPICaller payPalCaller = new NVPAPICaller();
                string retMsg = "";
                string token = "";
                string PayerID = "";
                NVPCodec decoder = new NVPCodec();
                token = Session["token"].ToString();
                bool ret = payPalCaller.GetCheckoutDetails(token, ref PayerID, ref decoder, ref retMsg);
                if (ret)
                {
                    
                    Session["payerId"] = PayerID;
                    string amt = decoder["AMT"].Replace(".", ",");
                    var myOrder = new Order();
                    myOrder.OrderDate = Convert.ToDateTime(decoder["TIMESTAMP"].ToString());
                    myOrder.CustomerId = customerId;
                    myOrder.FirstName = decoder["FIRSTNAME"].ToString();
                    myOrder.LastName = decoder["LASTNAME"].ToString();
                    myOrder.Address = decoder["SHIPTOSTREET"].ToString();
                    myOrder.City = decoder["SHIPTOCITY"].ToString();
                    myOrder.State = decoder["SHIPTOSTATE"].ToString();
                    myOrder.PostalCode = decoder["SHIPTOZIP"].ToString();
                    myOrder.Country = decoder["SHIPTOCOUNTRYCODE"].ToString();
                    myOrder.Email = decoder["EMAIL"].ToString();
                    myOrder.Total = Convert.ToDecimal(amt);
                    // Verify total payment amount as set on CheckoutStart.aspx.
                    try
                    {
                        decimal x = Convert.ToDecimal(Session["payment_amt"]);
                        decimal y = 0.06728M;
                        decimal z = (x * y);
                        string q = z.ToString("F2");
                        decimal paymentAmountOnCheckout = Convert.ToDecimal(q);
                        string paymentAmountFromPayPalstring = decoder["AMT"].Replace(".", ",");
                        decimal paymentAmoutFromPayPal = Convert.ToDecimal(paymentAmountFromPayPalstring);
                        if (paymentAmountOnCheckout != paymentAmoutFromPayPal)
                        {
                            Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                    }
                    OrderController ordController = new OrderController();
                    Order theOrder = ordController.GetOrder(customerId);
                    List<OrderDetail> theOrderDetails = ordController.GetOrdersInOrder(theOrder.Id);
                    // Set OrderId.
                    Session["currentOrderId"] = theOrder.Id;
                    // Display Order information.
                    List<Order> orderList = new List<Order>();
                    orderList.Add(theOrder);
                    ShipInfo.DataSource = orderList;
                    ShipInfo.DataBind();
                    // Display OrderDetails.
                    OrderItemList.DataSource = theOrderDetails;
                    OrderItemList.DataBind();

                   
                }
                else
                {
                    Response.Redirect("CheckoutError.aspx?" + retMsg);
                }
            }
        }
        protected void CheckoutConfirm_Click(object sender, EventArgs e)
        {
            Session["userCheckoutCompleted"] = "true";
            Response.Redirect("~/Checkout/CheckoutComplete.aspx");
        }
    }
}