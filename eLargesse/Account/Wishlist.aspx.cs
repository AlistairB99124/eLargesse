using eLargesse.Controllers;
using eLargesse.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;

namespace eLargesse.Account
{
    public partial class Wishlist : System.Web.UI.Page
    {
        private ProductController productController;
        private ClientController clientController;
        private WishlistController wishlistController;
        private WishlistItemController wishlistItemController;

        protected void Page_Load(object sender, EventArgs e)
        {
            productController = new ProductController();
            clientController = new ClientController();
            wishlistController = new WishlistController();
            wishlistItemController = new WishlistItemController();

            if (Context.User.Identity.IsAuthenticated)
            {
                
                List<Product> products = null;
                List<Product> audio = productController.GetProductsByCategory("Audio");
                List<Product> tv = productController.GetProductsByCategory("TV");
                List<Product> garden = productController.GetProductsByCategory("Garden");
                FillPanels(products, pnlWishlistOuter);
                FillPanels(audio, pnlCategoryOne);
                FillPanels(tv, pnlCategoryTwo);
                FillPanels(garden, pnlCategoryThree);

                eLargesse.Models.Wishlist wishlist = wishlistController.GetWishListByClient(clientController.GetClientByGUID(User.Identity.GetUserId()).ID);
                
                List<WishlistItem> items = wishlistItemController.GetAllWishlistItems(wishlist);
                List<Product> productsInList = wishlistItemController.GetProductsInList(items);
                FillPanels(productsInList, pnlWishlistOuter);
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
           

        }

        private void FillPanels(List<Product> products, Panel panel)
        {
            if (products != null)
            {
                //Create a new panel with an ImageButton and 2 Labels for each Product
                foreach (Product p in products)
                {
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
                        Text = string.Format("<ins>{0}</ins>", "R" + p.Price),
                        CssClass = "product-carousel-price"
                    };
                    lblPrice.Attributes["style"] = "text-align:center;";


                    HyperLink hoverlink2 = new HyperLink()
                    {
                        CssClass = "view-details-link",
                        Text = "<i class='fa fa-link'></i>Details",
                        NavigateUrl = "~/Shop/Product.aspx?id=" + p.Id
                    };

                    LinkButton btnAddToList = new LinkButton();
                    LinkButton btnDeleteFromList = new LinkButton();

                    if (panel != pnlWishlistOuter)
                    {
                        btnAddToList.ID = "add" + p.Id;
                        btnAddToList.Text = "<span class='glyphicon glyphicon-heart'></span>";
                        btnAddToList.CssClass = "btn";
                        btnAddToList.Attributes["style"] = "float:right;";

                        btnAddToList.Click += Add_ToList;
                    }
                    else
                    {
                        btnDeleteFromList.ID = "del" + p.Id;
                        btnDeleteFromList.Text = "<span class='glyphicon glyphicon-trash'></span>";
                        btnDeleteFromList.CssClass = "btn";

                        btnDeleteFromList.Attributes["style"] = "float:right;";
                        btnDeleteFromList.Click += Del_FromList;
                    }


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
                    productPanel.Controls.Add(hoverlink2);
                    productPanel.Controls.Add(lit4);
                    productPanel.Controls.Add(lit2);
                    productPanel.Controls.Add(linkName);
                    productPanel.Controls.Add(lit5);
                    productPanel.Controls.Add(lblPrice);
                    productPanel.Controls.Add(lit6);
                    if (panel != pnlWishlistOuter)
                    {
                        productPanel.Controls.Add(btnAddToList);
                    }
                    else
                    {
                        productPanel.Controls.Add(btnDeleteFromList);
                    }



                    //Add deynamic Panels to static Parent panel
                    panel.Controls.Add(productPanel);


                }
            }
            else
            {
                //No products found
                
            }
        }

        private void Del_FromList(object sender, EventArgs e)
        {
            LinkButton selectLink = (LinkButton)sender;
            string link = selectLink.ID.Replace("del", "");
            int productId = Convert.ToInt32(link);
            WishlistItem w = wishlistItemController.GetWishlistItem(productId);
            wishlistItemController.Delete(w.Id);

            Response.Redirect("~/Account/Wishlist.aspx");
        }

        private void Add_ToList(object sender, EventArgs e)
        {
            
            LinkButton selectLink = (LinkButton)sender;
            string link = selectLink.ID.Replace("add", "");
            int productId = Convert.ToInt32(link);
          
            Models.Wishlist wishlist = wishlistController.GetWishListByClient(clientController.GetClientByGUID(User.Identity.GetUserId()).ID);
            if(wishlist == null)
            {
                Models.Wishlist newWishlist = new Models.Wishlist()
                {
                    CustomerId = clientController.GetClientByGUID(User.Identity.GetUserId()).ID,
                    IsCurrent = true
                };

                wishlistController.Insert(wishlist);
                WishlistItem aWishItem = new WishlistItem() { ProductId = productId, WishlistId = newWishlist.Id, Created = DateTime.Now };
                wishlistItemController.Insert(aWishItem);

                Response.Redirect("~/Account/Wishlist.aspx");
            }
            else
            {
                WishlistItem wishItem = new WishlistItem() { ProductId = productId, WishlistId = wishlist.Id, Created = DateTime.Now };
                wishlistItemController.Insert(wishItem);

                Response.Redirect("~/Account/Wishlist.aspx");
            }
           
        }



        protected void CreateList_Click(object sender, EventArgs e)
        {

            Models.Wishlist wishlist = new Models.Wishlist()
            {
                CustomerId = clientController.GetClientByGUID(User.Identity.GetUserId()).ID,
                IsCurrent = true
            };

            wishlistController.Insert(wishlist);
        }
        
    }
}