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
    public partial class CheckoutStart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientController clientController = new ClientController();
            NVPAPICaller payPalCaller = new NVPAPICaller();
            string retMsg = "";
            string token = "";

            if (Session["payment_amt"] != null)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    Client client = clientController.GetClientByGUID(User.Identity.GetUserId());
                    string total = Session["payment_amt"].ToString();                    
                   
                    bool ret = payPalCaller.ShortcutExpressCheckout(total, ref token, ref retMsg, client.ID);
                    if (ret)
                    {
                        Session["token"] = token;
                        Response.Redirect(retMsg);
                    }
                    else
                    {
                        Response.Redirect("CheckoutError.aspx?" + retMsg);
                    }
                }

            }
            else
            {
                Response.Redirect("CheckoutError.aspx?ErrorCode=AmtMissing");
            }
        }
    }
}