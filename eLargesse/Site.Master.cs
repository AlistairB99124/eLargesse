using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using eLargesse.Models;
using eLargesse.Controllers;
using System.Linq;
using MailChimp;
using MailChimp.Helper;

namespace eLargesse
{
    public partial class SiteMaster : MasterPage
    {
        #region Fields
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        private ClientController clientController;
        private CartController cartController;
        //private ProductController productController;
        //private ManufacturerController manuController;
        private SubCategoryController subCategoryController;

        #endregion

        #region Properties
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
        #endregion

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

        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                if (Context.User.Identity.GetUserId() == "5db2539a-1462-4bcc-ab19-7d0cc1bc4fe4")
                {
                    linkAdmin.Visible = true;
                }
                cartController = new CartController();
                clientController= new ClientController();
                Client c = clientController.GetClientByGUID(HttpContext.Current.User.Identity.GetUserId());


                PreviewQuantity.Text = cartController.GetAmountOfOrders(c.ID).ToString("F0");
                PreviewTotal.Text = ddlCurreny.SelectedValue + Convert.ToString(ConvertPrice(Convert.ToDecimal(cartController.GetTotalInCart(c.ID)), ddlCurreny.Text).ToString("F2"));
            }
            FillPage();
        }

        #endregion

            #region Methods
        private void FillPanel(IEnumerable<SubCategory> subCategories)
        {
            if (subCategories != null)
            {
                Literal lit = new Literal();
                lit.Text = "<ul>";
                Literal litEnd = new Literal();
                litEnd.Text = "</ul>";

                pnlListCategories.Controls.Add(lit);
                foreach (SubCategory s in subCategories)
                {
                    Panel panel = new Panel();
                    HyperLink link = new HyperLink()
                    {
                        Text = s.Name,
                        NavigateUrl = string.Format("~/Shop/Index.aspx?categoryId={0}", s.Id)
                    };
                    Literal lit1 = new Literal();
                    lit1.Text = "<li>";
                    Literal lit2 = new Literal();
                    lit2.Text = "</li>";

                    panel.Controls.Add(lit1);
                    panel.Controls.Add(link);
                    panel.Controls.Add(lit2);

                    pnlListCategories.Controls.Add(panel);
                }
                pnlListCategories.Controls.Add(litEnd);
            }
        }

        private void FillPage()
        {
            subCategoryController = new SubCategoryController();
            List<SubCategory> subCategories = subCategoryController.GetAllSubCategories();
            IEnumerable<SubCategory> topFive = subCategories.Take(5);

            FillPanel(topFive);
        }

        public decimal ConvertPrice(decimal randValue, string currency)
        {
            decimal result = 0.00M;
            
            switch(currency)
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

        #endregion

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                MailChimpManager mc = new MailChimpManager("98515aa118e77a1d484c336e2f329aab-us13");

                //  Create the email parameter
                EmailParameter email = new EmailParameter()
                {
                    Email = txtMailChimp.Text
                };

                EmailParameter results = mc.Subscribe("5650b1d694", email);
                Response.Redirect("~/Default.aspx");
            }
            catch
            {

            }
            
        }

        protected void SearchPosts_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Replace(" ", "+").ToLower();
            Response.Redirect("~/Search?query=" + query);
        }
    }

}