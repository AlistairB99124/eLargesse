using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using eLargesse.Models;
using eLargesse.Controllers;

namespace eLargesse.Account
{
    public partial class Manage : System.Web.UI.Page
    {

        private ClientController clientController;
        private AddressController addressController;

        protected string SuccessMessage
        {
            get;
            private set;
        }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }

        public bool HasPhoneNumber { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool TwoFactorBrowserRemembered { get; private set; }

        public int LoginsCount { get; set; }

        protected void Page_Load()
        {
            clientController = new ClientController();
            addressController = new AddressController();

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(User.Identity.GetUserId()));

            // Enable this after setting up two-factor authentientication
            //PhoneNumber.Text = manager.GetPhoneNumber(User.Identity.GetUserId()) ?? String.Empty;

            TwoFactorEnabled = manager.GetTwoFactorEnabled(User.Identity.GetUserId());

            LoginsCount = manager.GetLogins(User.Identity.GetUserId()).Count;

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            if (!IsPostBack)
            {
                // Determine the sections to render
                if (HasPassword(manager))
                {
                    ChangePassword.Visible = true;
                }
                else
                {
                    CreatePassword.Visible = true;
                    ChangePassword.Visible = false;
                }

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Your password has been changed."
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The account was removed."
                        : message == "AddPhoneNumberSuccess" ? "Phone number has been added"
                        : message == "RemovePhoneNumberSuccess" ? "Phone number was removed"
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }

                Client client = clientController.GetClientByGUID(Context.User.Identity.GetUserId());
                Address presentAddress = addressController.GetAddressByClient(client.ID);
                string guid = Context.User.Identity.GetUserId();

                FillPage(presentAddress, client, manager.GetEmail(guid), manager.GetPhoneNumber(guid));
            }
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.SetPhoneNumber(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return;
            }
            var user = manager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), false);

            Response.Redirect("/Account/Manage");
        }

        //EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), true);

            Response.Redirect("/Account/Manage");
        }

        protected void btnUpdateAddress_Click(object sender, EventArgs e)
        {
            Client client = clientController.GetClientByGUID(Context.User.Identity.GetUserId());
            Address address = CreateAddress(client.ID);
            Address presentAddress = addressController.GetAddressByClient(client.ID);

            if (presentAddress == null)
            {
                addressController.Insert(address);
                Response.Redirect("~/Account/Manage.aspx");
            }
            else
            {
                addressController.Update(presentAddress.Id, address);
                Response.Redirect("~/Account/Manage.aspx");
            }
        }

        private Address CreateAddress(int clientId)
        {
            Address c = new Address()
            {
                Address1 = billing_address_1.Text,
                Address2 = billing_address_2.Text,
                CIty = billing_city.Text,
                Country = billing_country.SelectedValue,
                ClientId = clientId,
                PostalCode = billing_postcode.Text,
                State = billing_state.Text
            };

            return c;
        }

        private void FillPage(Address address, Client client, string email, string cell)
        {
            if (address != null)
            {
                billing_address_1.Text = address.Address1;
                billing_address_2.Text = address.Address2;
                billing_city.Text = address.CIty;
                billing_state.Text = address.State;
                billing_postcode.Text = address.PostalCode;
                billing_country.SelectedValue = address.Country;
            }
            
            billing_first_name.Text = client.FirstName;
            billing_last_name.Text = client.LastName;
            billing_phone.Text = cell;
            billing_email.Text = email;
        }
    }
}