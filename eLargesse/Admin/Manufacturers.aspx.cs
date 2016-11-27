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
    public partial class Manufacturers : System.Web.UI.Page
    {
        private ManufacturerController manuController;
        protected void Page_Load(object sender, EventArgs e)
        {
            manuController = new ManufacturerController();

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

        private void FillPage(int id)
        {
            //Get selected product from DB
           
            Manufacturer manufacturer = manuController.GetManufacturer(id);

            //Fill TextBoxes
            Name.Text = manufacturer.Name;

            //Set DropDownLists values
            Image.SelectedValue = manufacturer.Logo;
          
        }

        private void GetImages()
        {
            try
            {
                //Get all filepaths
                string[] images = Directory.GetFiles(MapPath("~/img/Brands/"));

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

        protected void Submit_Click(object sender, EventArgs e)
        {
            
            Manufacturer pt = CreateManufacturer();

            if (manuController.Insert(pt))
            {
                SuccessLabel.Text = "success";
                SuccessLabel.Visible = true;
            }
        }

        private Manufacturer CreateManufacturer()
        {
            Manufacturer p = new Manufacturer();
            p.Name = Name.Text;
            p.Logo = Image.SelectedValue;

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
                            path = "~/img/Brands/" + fileName;
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
        protected void grdManu_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = grdManu.Rows[e.NewEditIndex];

            //Get id of selected product
            int rowId = Convert.ToInt32(row.Cells[1].Text);

            //Redirect user to ManageProducts along with the selected rowId
            Response.Redirect("~/Management/Manufacturers.aspx?id=" + rowId);
        }
    }
}