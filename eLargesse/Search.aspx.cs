using eLargesse.Controllers;
using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse
{
    public partial class Search : System.Web.UI.Page
    {
        private PostController controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            controller = new PostController();

            if (!string.IsNullOrWhiteSpace(Request.QueryString["query"]))
            {
                // Get query from URL
                string query = Convert.ToString(Request.QueryString["query"]);

                // Create an array to store each word in the query
                string[] splits = query.Split(' ');

                // Store words of the query in a list of keywords
                List<string> keywords = splits.ToList();

                List<Post> searchResults = controller.SearchPosts(keywords);

                FillPanel(searchResults);
            }

          
        }

        private void FillPanel(List<Post> posts)
        {
            if (posts != null)
            {
                foreach(Post p in posts)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<h2><a href='./News/Article?ArticleId=");
                    sb.Append(p.Id);
                    sb.Append("'>");
                    sb.Append(p.title);
                    sb.Append("</a></h2><p style='fore-color:#008000;'>");
                    sb.Append(p.url);
                    sb.Append("</p><p>");
                    sb.Append(p.content);
                    sb.Append("</p><p>");

                    Literal lit = new Literal();
                    lit.Text = sb.ToString();

                    pnlResults.Controls.Add(lit);

                }
            }
        }
    }
}