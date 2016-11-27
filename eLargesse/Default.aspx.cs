using eLargesse.Controllers;
using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace eLargesse
{
    public partial class _Default : Page
    {

        private ClientController clientController;
        private CartController cartController;
        private ProductController productController;
        private ManufacturerController manuController;

        protected void Page_Load(object sender, EventArgs e)
        {
            clientController = new ClientController();
            cartController = new CartController();
            productController = new ProductController();
            manuController = new ManufacturerController();

            FillPage();
        }


        private void FillProductPanelPanel(List<Product> products, Panel panel)
        {
            DropDownList ddlCurrency = Master.DDLCurrency;
            

            if (products != null)
            {
                //Create a new panel with an ImageButton and 2 Labels for each Product
                foreach (Product p in products)
                {
                    decimal randValue = Convert.ToDecimal(p.Price);
                    decimal productPrice = Master.ConvertPrice(randValue, ddlCurrency.Text);
                    Panel productPanel = new Panel();
                    productPanel.Attributes["class"] = "single-product";
                    ImageButton imageButton = new ImageButton()
                    {
                        ImageUrl = "~/img/Products/" + p.Image,
                        Width = new Unit("100%")
                    };

                    Label linkName = new Label()
                    {
                        Text = string.Format("<H4>{0}</H4>", p.Name)
                    };

                    linkName.Attributes["style"] = "text-align:center;";

                    Label lblPrice = new Label()
                    {
                        Text = string.Format("<ins>{0}</ins>", ddlCurrency.SelectedValue + " " + productPrice.ToString("F2")),
                        CssClass = "product-carousel-price"
                    };
                    lblPrice.Attributes["style"] = "text-align:center;";

                    LinkButton hoverlink1 = new LinkButton()
                    {
                        CssClass = "add-to-cart-link",
                        Text = "<i class='fa fa-shopping-cart'></i>ADD TO CART",
                        ID = "add" + p.Id
                    };
                    hoverlink1.Click += AddToCart_Click;

                    HyperLink hoverlink2 = new HyperLink()
                    {
                        CssClass = "view-details-link",
                        Text = "<i class='fa fa-link'></i>Details",
                        NavigateUrl = "~/Shop/Product.aspx?id=" + p.Id
                    };

                   


                    Literal lit1 = new Literal();
                    Literal lit2 = new Literal();
                    Literal lit3 = new Literal();
                    Literal lit4 = new Literal();
                    Literal lit5 = new Literal();
                    Literal lit6 = new Literal();


                    lit1.Text = "<div class='product-hover'>";
                    lit2.Text = "</div>";
                    lit3.Text = "<div class='product-f-image'>";
                    lit4.Text = "</div>";
                    lit5.Text = "<div class='product-carousel-price'";
                    lit6.Text = "</div>";

                    //Add child controls to Panel

                    productPanel.Controls.Add(lit3);
                    productPanel.Controls.Add(imageButton);
                    productPanel.Controls.Add(lit1);
                    productPanel.Controls.Add(hoverlink1);
                    productPanel.Controls.Add(hoverlink2);
                    productPanel.Controls.Add(lit4);
                    productPanel.Controls.Add(lit2);
                    productPanel.Controls.Add(linkName);
                    productPanel.Controls.Add(lit5);
                    productPanel.Controls.Add(lblPrice);
                    productPanel.Controls.Add(lit6);


                    //Add deynamic Panels to static Parent panel
                    panel.Controls.Add(productPanel);


                }
            }
        }

        private void AddToCart_Click(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                string guid = Context.User.Identity.GetUserId();
                Client client = clientController.GetClientByGUID(guid);
                int clientId = client.ID;
                LinkButton selectedLink = (LinkButton)sender;
                string link = selectedLink.ID.Replace("add", "");
                int productId = Convert.ToInt32(link);

                if (client != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    int amount = 1;

                    eLargesse.Models.Cart cart = new eLargesse.Models.Cart
                    {
                        Amount = amount,
                        ClientId = clientId,
                        DatePurchased = DateTime.Now,
                        IsInCart = true,
                        ProductId = productId
                    };

                    cartController.Insert(cart);
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
          
        }

        private void FillPage()
        {
            //Get a list of all products in DB
            List<Product> products = productController.GetAllProductsOrderDate();

            //Get a list of all manufacturers in DB
            List<Manufacturer> manufacturers = manuController.GetAllManufacturers();


            //// Get Most Viewed Products
            eLargesseEntities de = new eLargesseEntities();
            var q = (from x in de.ProductViews
                     group x by x.ProductId into xgroup
                     let count = xgroup.Count()
                     orderby count descending
                     select new { Count = count, XVersion = xgroup.Key }).ToList();
            List<Product> mostViewed = new List<Product>();
            if (q != null)
            {
                foreach (var t in q)
                {
                    Product p = productController.GetProduct(t.XVersion);
                    if (p != null)
                    {
                        mostViewed.Add(p);
                    }
                }
            }
            IEnumerable<Product> topFour = mostViewed.Take(4);

            //// Get Last Viewed Products
            List<Product> lastViewed = (from x in de.Products where x.Sold == false orderby x.LastViewed descending select x).ToList();
            IEnumerable<Product> lastViewedTake4 = lastViewed.Take(4);

            //Make sure products exist in the database
            FillProductPanelPanel(products, Panel1);
            FillManufacturerPanel(manufacturers, Panel2);
            FillMostViewedProductPanel(topFour, Panel3);
            FillMostViewedProductPanel(lastViewedTake4, Panel4);
        }

        private void FillManufacturerPanel(List<Manufacturer> manufacturers, Panel panel)
        {
            if (manufacturers != null)
            {
                //Create a new panel with an ImageButton and 2 Labels for each Product
                foreach (Manufacturer p in manufacturers)
                {
                    Panel productPanel = new Panel();
                    productPanel.Attributes["class"] = "img-thumbnail";
                    Image image = new Image()
                    {
                        ImageUrl = "~/img/Brands/" + p.Logo,
                        Height = new Unit("100%")
                    };

                   
                    //Add child controls to Panel

                    productPanel.Controls.Add(image);
                 
                    //Add deynamic Panels to static Parent panel
                    panel.Controls.Add(productPanel);


                }
            }
        }
        private void FillMostViewedProductPanel(IEnumerable<Product> products, Panel panel)
        {
            DropDownList ddlCurrency = Master.DDLCurrency;


            if (products != null)
            {
                //Create a new panel with an ImageButton and 2 Labels for each Product
                foreach (Product p in products)
                {
                    decimal randValue = Convert.ToDecimal(p.Price);
                    decimal productPrice = Master.ConvertPrice(randValue, ddlCurrency.Text);

                    Panel productPanel = new Panel();
                    productPanel.Attributes["class"] = "single-wid-product";
                    Image imageButton = new Image()
                    {
                        ImageUrl = "~/img/Products/" + p.Image,
                        CssClass = "product-thumb"
                    };

                    HyperLink hoverlink2 = new HyperLink()
                    {
                        CssClass = "view-details-link",
                        Text = p.Name,
                        NavigateUrl = "~/Shop/Product.aspx?id=" + p.Id
                    };

                    Label lblPrice = new Label()
                    {
                        Text = string.Format("<ins>{0}</ins>", ddlCurrency.SelectedValue + " " + productPrice.ToString("F2")),
                        CssClass = "product-carousel-price"
                    };
                    //lblPrice.Attributes["style"] = "text-align:center;";

                    Literal lit1 = new Literal();
                    Literal lit2 = new Literal();
                    Literal lit3 = new Literal();
                    Literal lit4 = new Literal();
                    Literal lit5 = new Literal();
                    Literal lit6 = new Literal();
                    Literal lit7a = new Literal();
                    Literal lit7b = new Literal();
                    Literal lit8a = new Literal();
                    Literal lit8b = new Literal();


                    lit1.Text = "<a runat='server' href='~/Shop/Product.aspx?id=" + p.Id + "'>";
                    lit2.Text = "</a>";
                    lit3.Text = "<h2>";
                    lit4.Text = "</h2>";
                    lit5.Text = "<div class='product-wid-price'";
                    lit6.Text = "</div>";
                    lit7a.Text = "<div class='row'>";
                    lit7b.Text = "</div>";

                    //Add child controls to Panel
                    productPanel.Controls.Add(lit7a);
                    productPanel.Controls.Add(lit1);
                    productPanel.Controls.Add(imageButton);
                    productPanel.Controls.Add(lit2);
                    productPanel.Controls.Add(lit7b);
                    productPanel.Controls.Add(lit8a);
                    productPanel.Controls.Add(lit3);
                    productPanel.Controls.Add(hoverlink2);
                    productPanel.Controls.Add(lit4);
                    productPanel.Controls.Add(lit5);
                    productPanel.Controls.Add(lblPrice);
                    productPanel.Controls.Add(lit6);
                    productPanel.Controls.Add(lit8b);

                    //Add deynamic Panels to static Parent panel
                    panel.Controls.Add(productPanel);
                }
            }
        }
    }
}