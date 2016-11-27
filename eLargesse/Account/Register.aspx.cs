using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using eLargesse.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using eLargesse.Controllers;

namespace eLargesse.Account
{
    public partial class Register : Page
    {
        private ClientController clientController;

        protected void Page_Load(object sender, EventArgs e)
        {
            clientController = new ClientController();
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            bool clientResult = false;

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = username.Text, Email = username.Text };
            IdentityResult result = manager.Create(user, txtPassword.Text);

            Client client = CreateClient(user);
            clientResult = clientController.Insert(client);

            if (result.Succeeded)
            {
                if (clientResult)
                {

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    string code = manager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                    //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    Response.Redirect("~/Account/Manage.aspx");

                    //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    
                }
                else
                {
                    lblErrorMessage.Text = "Client Failed";
                }

            }
            else
            {
                lblErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        private Client CreateClient(ApplicationUser user)
        {
            Client client = new Client();

            client.GUID = user.Id;
            client.FirstName = txtFirstName.Text;
            client.LastName = txtLastName.Text;

            return client;
        }


    }
}