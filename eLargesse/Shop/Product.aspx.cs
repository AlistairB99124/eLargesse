using System;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using eLargesse.Models;
using eLargesse.Controllers;

namespace eLargesse.Shop
{
    public partial class Product : System.Web.UI.Page
    {
        private ProductController productController;
        private ProductViewController productViewController;
        private ClientController clientController;
        private CartController cartController;

        protected void Page_Load(object sender, EventArgs e)
        {
            productController = new ProductController();
            productViewController = new ProductViewController();
            clientController = new ClientController();
            cartController = new CartController();

            FillPage();
        }

        private void FillPage()
        {
            DropDownList ddlCurrency = Master.DDLCurrency;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                eLargesse.Models.Product p = productController.GetProduct(id);
                decimal randValue = Convert.ToDecimal(p.Price);
                decimal productPrice = Master.ConvertPrice(randValue, ddlCurrency.Text);

                //Fill page with data
                lblProductPrice.Text = "Price per unit: <br/>" + ddlCurrency.SelectedValue + " " + productPrice.ToString("F2");
                lblProductName.Text = p.Name;
                lblDescription.Text = p.Description;
                //itemNumber.Text = id.ToString();
                imgProductMain.ImageUrl = "~/img/Products/" + p.Image;

                //Fill Amount dropdownlist with numbers 1 - 20
                int[] amount = Enumerable.Range(1, 20).ToArray();
                ddlQuantity.DataSource = amount;
                ddlQuantity.AppendDataBoundItems = true;
                ddlQuantity.DataBind();

                // Add product to Tally Count
                ProductView view = new ProductView() { ProductId = id };
                productViewController.Insert(view);

                // Update Views in Product Table
                Models.Product pp = new Models.Product()
                {
                    Description = p.Description,
                    DateCreated = p.DateCreated,
                    DateSold = p.DateSold,
                    Image = p.Image,
                    LastViewed = DateTime.Now,
                    ManufacturerId = p.ManufacturerId,
                    Id = p.Id,
                    Price = p.Price,
                    SubCategoryId = p.SubCategoryId,
                    Name = p.Name,
                    Sold = false,
                };
                productController.Update(p.Id, pp);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    string guid = Context.User.Identity.GetUserId();
                    Client client = clientController.GetClientByGUID(guid);
                    int clientId = client.ID;

                    if (client != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        int amount = Convert.ToInt32(ddlQuantity.SelectedValue);

                        eLargesse.Models.Cart cart = new eLargesse.Models.Cart
                        {
                            Amount = amount,
                            ClientId = clientId,
                            DatePurchased = DateTime.Now,
                            IsInCart = true,
                            ProductId = id
                        };

                        result = cartController.Insert(cart);
                        if (result)
                        {
                            Response.Redirect("~/Shop/Product.aspx?id=" + id);
                        }
                    }
                    else
                    {
                        lblResult.Text = "Please login to order items";
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
               
            }
        }
    }
}