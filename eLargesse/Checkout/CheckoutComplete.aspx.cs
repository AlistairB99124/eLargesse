using eLargesse.Controllers;
using eLargesse.Logic;
using eLargesse.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse.Checkout
{
    public partial class CheckoutComplete : System.Web.UI.Page
    {
        private OrderController orderController;
        private ProductController productController;
        private OrderDetailController orderDetailController;
        private ClientController clientController;
        protected void Page_Load(object sender, EventArgs e)
        {
            orderController = new OrderController();
            productController = new ProductController();
            orderDetailController = new OrderDetailController();
            clientController = new ClientController();

            Client client = clientController.GetClientByGUID(Context.User.Identity.GetUserId());

            Order order = orderController.GetOrder(client.ID);

           
            if (!string.IsNullOrWhiteSpace(Request.QueryString["method"]))
            {
                string method = Request.QueryString["method"];
                if (method == "EFT")
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["orderId"]))
                    {
                        if (!string.IsNullOrWhiteSpace(Request.QueryString["method"]))
                        {
                            int orderId = Convert.ToInt32(Request.QueryString["orderId"]);

                            if (order != null)
                            {
                                List<OrderDetail> orders = orderDetailController.GetAllOrderDetails(orderId);
                                string date = DateTime.Now.ToShortDateString().Replace("/","");
                                string TransactId = date + " " + orderId;
                                TransactionId.Text = TransactId;
                                foreach (OrderDetail orderDetail in orders)
                                {
                                    productController.MarkDateSold(orderDetail.ProductId, DateTime.Now, true);
                                    orderDetailController.Delete(orderDetail.Id);
                                }
                                orderController.MarkEFTPaymentProcessed(orderId, true, TransactId);
                            }
                        }
                    }
                }
                else
                {
                    CompletePayPal(order);
                }

            }
            else
            {
                CompletePayPal(order);
            }

        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


        private void CompletePayPal(Order order)
        {
            if (!IsPostBack)
            {
                // Verify user has completed the checkout process.
                if ((string)Session["userCheckoutCompleted"] != "true")
                {
                    Session["userCheckoutCompleted"] = string.Empty;
                    Response.Redirect("CheckoutError.aspx?" + "Desc=Unvalidated%20Checkout.");
                }
                NVPAPICaller payPalCaller = new NVPAPICaller();
                string retMsg = "";
                string token = "";
                string finalPaymentAmount = "";
                string PayerID = "";
                NVPCodec decoder = new NVPCodec();
                token = Session["token"].ToString();
                PayerID = Session["payerId"].ToString();
                finalPaymentAmount = Session["payment_amt"].ToString();
                bool ret = payPalCaller.DoCheckoutPayment(finalPaymentAmount, token, PayerID, ref decoder, ref retMsg);
                if (ret)
                {
                    // Retrieve PayPal confirmation value.
                    string PaymentConfirmation = decoder["PAYMENTINFO_0_TRANSACTIONID"].ToString();
                    TransactionId.Text = PaymentConfirmation;
                    eLargesseEntities _db = new eLargesseEntities();
                    // Get the current order id.
                    int currentOrderId = -1;
                    if (Session["currentOrderId"].ToString() != string.Empty)
                    {
                        currentOrderId = Convert.ToInt32(Session["currentOrderID"]);
                    }
                    Order myCurrentOrder;
                    if (currentOrderId >= 0)
                    {
                        // Get the order based on order id.

                        myCurrentOrder = _db.Orders.Single(o => o.Id == currentOrderId);
                        // Update the order to reflect payment has been completed.
                        myCurrentOrder.PaymentTransactionId = PaymentConfirmation;
                        // Save to DB.
                        _db.SaveChanges();
                    }
                    List<OrderDetail> orderDetails = orderController.GetOrdersInOrder(order.Id);
                    // Clear shopping cart.
                    using (OrderDetailController usersShoppingCart = new OrderDetailController())
                    {
                        foreach (OrderDetail ord in orderDetails)
                        {
                            productController.MarkDateSold(ord.ProductId, DateTime.Now, true);
                            usersShoppingCart.Delete(ord.Id);
                        }
                    }
                    orderController.MarkPaymentProcessed((int)Session["currentOrderId"], true);
                    orderController.MarkAsShipped((int)Session["currentOrderId"], true);
                    // Clear order id.
                    Session["currentOrderId"] = string.Empty;
                  
                }
                else
                {
                    Response.Redirect("CheckoutError.aspx?" + retMsg);
                }
            }
        }
    }
}