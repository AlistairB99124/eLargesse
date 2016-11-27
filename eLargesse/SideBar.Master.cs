using eLargesse.Controllers;
using eLargesse.Logic;
using eLargesse.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse
{
    public partial class SIdeBar : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        private ProductController productController;
        private CartController cartController;
        private ClientController clientController;

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

       
        protected void Page_PreRender(object sender, EventArgs e)
        {
            productController = new ProductController();
            cartController = new CartController();
            clientController = new ClientController();

            FillRecentPosts();
            if (Context.User.Identity.IsAuthenticated)
            {
                if (Context.User.Identity.GetUserId() == "5db2539a-1462-4bcc-ab19-7d0cc1bc4fe4")
                {
                    linkAdmin.Visible = true;
                }

                Client c = clientController.GetClientByGUID(HttpContext.Current.User.Identity.GetUserId());


                PreviewQuantity.Text = cartController.GetAmountOfOrders(c.ID).ToString("F0");
                PreviewTotal.Text = ddlCurreny.SelectedValue + Convert.ToString(ConvertPrice(Convert.ToDecimal(cartController.GetTotalInCart(c.ID)), ddlCurreny.Text).ToString("F2"));
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        public decimal ConvertPrice(decimal randValue, string currency)
        {
            decimal result = 0.00M;

            switch (currency)
            {
                case "R":
                    result = 1 * randValue;
                    break;
                case "$":
                    result = 0.06938M * randValue;
                    break;
                case "£":
                    result = 0.04759M * randValue;
                    break;
                default:
                    result = 0.00M;
                    break;
            }
            return result;
        }
        public DropDownList DDLCurrency
        {
            get { return ddlCurreny; }
            set { ddlCurreny = value; }
        }
        public DropDownList DDLLanguage
        {
            get { return ddlLanguage; }
            set { ddlLanguage = value; }
        }
        private void FillRecentPosts()
        {
            List<Product> products = productController.GetAllProductsOrderDate();
            var list = new List<ListItem>();

            foreach (Product p in products)
            {
                list.Add(new ListItem(p.Name, "~/Shop/Product.aspx?id=" + p.Id));
            }

            BulletedList1.DataSource = list.Take(4);
            BulletedList1.DataTextField = "text";
            BulletedList1.DataValueField = "value";
            BulletedList1.DisplayMode = BulletedListDisplayMode.HyperLink;
            BulletedList1.DataBind();
        }

        protected void btnSearchProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shop/Index.aspx?query=" + txtSearchProducts.Text.Replace(" ", "+"));
        }

        protected void SearchPosts_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Replace(" ", "+").ToLower();
            Response.Redirect("~/Search?query=" + query);
        }


        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
      
    }
}