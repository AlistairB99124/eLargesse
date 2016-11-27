using eLargesse.Controllers;
using eLargesse.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        private ProductController controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            controller = new ProductController();
            if (!IsPostBack)
            {
                GetImages();

                if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    FillPage(id);
                    Submit.Text = "Update";
                }
                else
                {
                    Submit.Text = "Insert";
                }
            }
        }
        protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Get selected row
            GridViewRow row = grdProducts.Rows[e.NewEditIndex];

            //Get id of selected product
            int rowId = Convert.ToInt32(row.Cells[1].Text);

            //Redirect user to ManageProducts along with the selected rowId
            Response.Redirect("~/Admin/Products?id=" + rowId);
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
           
            Product p = CreateProduct();

            if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                bool UpdateProduct = controller.Update(id, p);
                if (UpdateProduct)
                {
                    SuccessLabel.Text = "Product Successfully Updated";
                    SuccessLabel.Visible = true;
                }
            }
            else
            {
                bool InsertProduct = controller.Insert(p);
                if (controller.Insert(p))
                {
                    SuccessLabel.Text = "Su";
                    SuccessLabel.Visible = true;
                }
                
            }

        }
        private void FillPage(int id)
        {
            //Get selected product from DB
            
            Product product = controller.GetProduct(id);

            //Fill TextBoxes
            Name.Text = product.Name;
            Price.Text = product.Price.ToString();
            Description.Text = product.Description;

            //Set DropDownLists values
            Image.SelectedValue = product.Image;
            Type.SelectedValue = product.SubCategoryId.ToString();
        }

        private void GetImages()
        {
            try
            {
                //Get all filepaths
                string[] images = Directory.GetFiles(MapPath("~/img/Products/"));

                //Get all filenames and add them to an arraylist
                ArrayList imageList = new ArrayList();
                foreach (string i in images)
                {
                    string imageName = i.Substring(i.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                    imageList.Add(imageName);
                }

                //Set the arrayList as the dropdoplist's datasource and refresh
                Image.DataSource = imageList;
                Image.AppendDataBoundItems = true;
                Image.DataBind();
            }
            catch (Exception e)
            {
                SuccessLabel.ForeColor = System.Drawing.Color.Red;
                SuccessLabel.Text = e.ToString();
            }
        }



        private Product CreateProduct()
        {
            Product p = new Product();

            p.Name = Name.Text;
            p.DateCreated = DateTime.Now;
            p.DateSold = null;
            p.Price = Convert.ToDecimal(Price.Text);
            p.SubCategoryId = Convert.ToInt32(Type.SelectedValue);
            p.Description = Description.Text;
            p.Image = Image.SelectedValue;
            p.ManufacturerId = Convert.ToInt32(ddlManufacturer.SelectedValue);
            p.Sold = false;

            return p;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string fileName;
            string path;

            var contentType = imgFileUpload.PostedFile.ContentType;
            var contentLength = imgFileUpload.PostedFile.ContentLength;

            if (imgFileUpload.HasFile)
            {
                try
                {
                    if (contentType == "image/jpeg" || contentType == "image/png")
                    {
                        if (contentLength < 2048000)
                        {
                            fileName = Path.GetFileName(imgFileUpload.PostedFile.FileName);
                            path = "~/img/Products/" + fileName;
                            imgFileUpload.PostedFile.SaveAs(Server.MapPath(path));
                            UploadStatus.Text = "Upload status: File uploaded!";

                        }
                        else
                        {
                            UploadStatus.Text = "Upload status: The file has to be less than 2MB";
                        }
                    }
                    else
                    {
                        UploadStatus.Text = "Upload status: Only JPEG or PNG files are accepted";
                    }

                }
                catch (Exception ex)
                {
                    UploadStatus.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
    }
}