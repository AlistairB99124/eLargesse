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
    public partial class Categories : System.Web.UI.Page
    {
        private CategoryController categoryController;
        protected void Page_Load(object sender, EventArgs e)
        {
            categoryController = new CategoryController();
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
           
            Category pt = CreateProductType();

            if(categoryController.Insert(pt))
            {
                SuccessLabel.Text = "Success";
                SuccessLabel.Visible = true;
            }
           
        }

        private Category CreateProductType()
        {
            Category p = new Category();
            p.Name = Name.Text;

            return p;
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.NewEditIndex];

            //Get id of selected product
            int rowId = Convert.ToInt32(row.Cells[1].Text);

            //Redirect user to ManageProducts along with the selected rowId
            Response.Redirect("~/Management/Categories.aspx?id=" + rowId);
        }
    }
}