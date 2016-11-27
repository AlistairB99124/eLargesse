using eLargesse.Controllers;
using eLargesse.Logic;
using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse.Shop
{
    public partial class Index : System.Web.UI.Page
    {
        private ProductController productController;
        protected void Page_Load(object sender, EventArgs e)
        {
            productController = new ProductController();

            if (!string.IsNullOrWhiteSpace(Request.QueryString["query"]))
            {
                // Get query from URL
                string query = Convert.ToString(Request.QueryString["query"]);

                // Create an array to store each word in the query
                string[] splits = query.Split(' ');

                // Store words of the query in a list of keywords
                List<string> keywords = splits.ToList();

                // Get a list of product IDs from the database
                List<int> ids = productController.SearchProducts(keywords);

                //Get All product objects from the database
                List<eLargesse.Models.Product> allProducts = productController.GetAllProducts();

                //Create a new list of products that will store the search results
                List<eLargesse.Models.Product> searchResults = new List<eLargesse.Models.Product>();

                //Loop through each product id
                foreach (int i in ids)
                {
                    //Loop through All products
                    foreach (eLargesse.Models.Product product in allProducts)
                    {
                        //check if id and productId match
                        if (i == product.Id)
                        {
                            // Check the search results is not already null
                            if (searchResults.Count == 0)
                            {
                                // iterate through search results to avoid duplicates
                                for (int x = 0; x < searchResults.Count; x++)
                                {
                                    // iF search ID is not null, then add
                                    if (searchResults[x].Id != i)
                                    {
                                        searchResults.Add(product);
                                    }
                                }
                                //If search results is null, then add
                                searchResults.Add(product);
                            }

                        }

                    }
                }
                FillPage(searchResults);
            }
            else if (!string.IsNullOrWhiteSpace(Request.QueryString["categoryId"]))
            {
                int catID = Convert.ToInt32(Request.QueryString["categoryId"]);                
                List<eLargesse.Models.Product> products = productController.GetProductsBySubType(catID);
                FillPanel(products);
            }
            else
            {
                
                List<eLargesse.Models.Product> products = productController.GetAllProducts();

                FillPage(products);
            }
        }

        private void FillPanel(List<eLargesse.Models.Product> products)
        {
            DropDownList ddlCurrency = Master.DDLCurrency;

            if (products != null)
            {
                //Create a new panel with an ImageButton and 2 Labels for each Product
                foreach (eLargesse.Models.Product p in products)
                {
                    decimal randValue = Convert.ToDecimal(p.Price);
                    decimal productPrice = Master.ConvertPrice(randValue, ddlCurrency.Text);
                    Panel productPanel = new Panel();
                    ImageButton imageButton = new ImageButton()
                    {
                        ImageUrl = "~/img/Products/" + p.Image,
                        CssClass = "productImage",
                        PostBackUrl = string.Format("~/Shop/Product.aspx?id={0}", p.Id)
                    };
                  
                    HyperLink linkName = new HyperLink()
                    {
                        Text = string.Format("<H4>{0}</H4>", p.Name),
                        NavigateUrl = string.Format("~/Shop/Product.aspx?id={0}", p.Id)
                    };

                    Label lblPrice = new Label()
                    {
                        Text = string.Format("<ins>{0}</ins>", ddlCurrency.SelectedValue + " " + productPrice.ToString("F2")),
                        CssClass = "product-carousel-price"
                    };

                    //Add child controls to Panel
                    productPanel.Controls.Add(imageButton);
                    productPanel.Controls.Add(linkName);
                    productPanel.Controls.Add(lblPrice);

                    //Add deynamic Panels to static Parent panel
                    pnlProducts.Controls.Add(productPanel);


                }
            }
            else
            {
                //No products found
                pnlProducts.Controls.Add(new Literal { Text = "No Products Found!" });
            }


        }
        private void FillPage(List<eLargesse.Models.Product> products)
        {
            //Get a list of all products in DB


            //Make sure products exist in the database
            FillPanel(products);
        }

        protected void Previous_Click(object sender, EventArgs e)
        {

        }

        protected void Next_Click(object sender, EventArgs e)
        {

        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            pnlProducts.Controls.Clear();
            FilterProductsbyManufacturers();
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            pnlProducts.Controls.Clear();
            FilterProducts();
        }


        #region TreenodePopulate
        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                switch (e.Node.Depth)
                {
                    case 0:
                        PopulateCategories(e.Node);
                        break;
                    case 1:
                        PopulateSubCategories(e.Node);
                        break;
                }
            }
        }
        protected void TreeView2_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                switch (e.Node.Depth)
                {
                    case 0:
                        PopulateManufacturers(e.Node);
                        break;
                }
            }
        }
        #endregion

        #region Database Tree Populations

        private void PopulateManufacturers(TreeNode node)
        {
            SqlCommand sqlQuery = new SqlCommand("Select Name, Id From Manufacturer");
            DataSet resultSet;
            resultSet = DatabaseHelpers.RunQuery(sqlQuery);
            if (resultSet.Tables.Count > 0)
            {
                foreach (DataRow row in resultSet.Tables[0].Rows)
                {
                    TreeNode NewNode = new
                        TreeNode(row["Name"].ToString(),
                        row["Id"].ToString());
                    NewNode.PopulateOnDemand = true;
                    NewNode.SelectAction = TreeNodeSelectAction.Select;
                    node.ChildNodes.Add(NewNode);
                }
            }
        }

        private void PopulateSubCategories(TreeNode node)
        {
            SqlCommand sqlQuery = new SqlCommand();
            sqlQuery.CommandText = "Select Name From SubCategory " +
                " Where CategoryId = @typeid";
            sqlQuery.Parameters.Add("@typeid", SqlDbType.Int).Value =
                node.Value;
            DataSet ResultSet = DatabaseHelpers.RunQuery(sqlQuery);
            if (ResultSet.Tables.Count > 0)
            {
                foreach (DataRow row in ResultSet.Tables[0].Rows)
                {
                    TreeNode NewNode = new
                        TreeNode(row["Name"].ToString());
                    NewNode.PopulateOnDemand = false;
                    NewNode.SelectAction = TreeNodeSelectAction.Select;
                    node.ChildNodes.Add(NewNode);
                }
            }
        }

        private void PopulateCategories(TreeNode node)
        {
            SqlCommand sqlQuery = new SqlCommand("Select Name, Id From Category");
            DataSet resultSet;
            resultSet = DatabaseHelpers.RunQuery(sqlQuery);
            if (resultSet.Tables.Count > 0)
            {
                foreach (DataRow row in resultSet.Tables[0].Rows)
                {
                    TreeNode NewNode = new
                        TreeNode(row["Name"].ToString(),
                        row["Id"].ToString());
                    NewNode.PopulateOnDemand = true;
                    NewNode.SelectAction = TreeNodeSelectAction.Expand;
                    node.ChildNodes.Add(NewNode);
                }
            }
        }

        #endregion

        #region Filters
      

        private void FillPage()
        {
            //Get a list of all products in DB
            List<eLargesse.Models.Product> products = productController.GetAllProducts();

            //Make sure products exist in the database
            FillPanel(products);
        }

        private void FilterProductsByPriceRange()
        {
            decimal minPrice = Convert.ToDecimal(txtMinPrice.Text);
            decimal maxPrice = Convert.ToDecimal(txtMaxPrice.Text);
            int index = ddlSortBy.SelectedIndex;
            
            List<eLargesse.Models.Product> products = productController.GetSortedProductsByPriceRange(minPrice, maxPrice, index);
            FillPanel(products);
        }

        protected void btnFilterPrice_Click(object sender, EventArgs e)
        {
            pnlProducts.Controls.Clear();
            FilterProductsByPriceRange();
        }

        private void FilterProductsbyManufacturers()
        {
            int index = ddlSortBy.SelectedIndex;
            int manuID = Convert.ToInt32(TreeView2.SelectedValue);
            List<eLargesse.Models.Product> products = productController.GetSortedProductsByManuName(manuID, index);
            FillPanel(products);
        }

        private void FilterProducts()
        {
            int index = ddlSortBy.SelectedIndex;
            string typeName = Convert.ToString(TreeView1.SelectedValue);
            List<eLargesse.Models.Product> products = productController.GetSortedProductsBySubTypeName(typeName, index);

            FillPanel(products);
        }

        #endregion
    }
}