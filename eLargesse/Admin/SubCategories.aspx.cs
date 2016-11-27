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
    public partial class SubCategories : System.Web.UI.Page
    {
        private SubCategoryController subCatController;
        protected void Page_Load(object sender, EventArgs e)
        {
            subCatController = new SubCategoryController();
        }
        public SubCategory CreateSubType()
        {
            SubCategory st = new SubCategory();
            st.Name = Name.Text;
            st.CategoryId = Convert.ToInt32(DDLType.SelectedValue);
            return st;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            SubCategory st = CreateSubType();

            if (subCatController.Insert(st))
            {
                SuccessLabel.Text = "Success";
                SuccessLabel.Visible = true;
            }
            
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = GridView2.Rows[e.NewEditIndex];

            //Get id of selected product
            int rowId = Convert.ToInt32(row.Cells[1].Text);

            //Redirect user to ManageProducts along with the selected rowId
            Response.Redirect("~/Management/SubCategories.aspx?id=" + rowId);
        }

    }
}