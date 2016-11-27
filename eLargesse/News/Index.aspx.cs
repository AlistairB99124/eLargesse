using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eLargesse.Models;
using eLargesse.Controllers;

namespace eLargesse.News
{
    public partial class Index : System.Web.UI.Page
    {
        private PostController postController;
        private ClientController clientController;
        protected void Page_Load(object sender, EventArgs e)
        {
            postController = new PostController();
            clientController = new ClientController();
            List<eLargesse.Models.Article> allArticles = postController.AllArticles();
            FillPanel(allArticles);
        }

        private void FillPanel(List<eLargesse.Models.Article> posts)
        {
            if (posts != null)
            {
                foreach(eLargesse.Models.Article a in posts)
                {
                    Post post = postController.GetPost(a.PostId);
                    Client client = clientController.GetClient(post.author);

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div class='row'><h4><a href='Article.aspx?articleId=");
                    sb.Append(a.Id);
                    sb.Append("'>");
                    sb.Append(post.title);
                    sb.Append("</a></h4><p>Author:");
                    sb.Append(client.FirstName + " " + client.LastName);
                    sb.Append("</p><p>Date:");
                    sb.Append(post.date_created.ToLongDateString());
                    sb.Append("</p>");
                    sb.Append("<div class='col-md-3'><img alt='' class='img-thumbnail' style='width:100%;' src='../img/Articles/");
                    sb.Append(a.FeatureImg);
                    sb.Append("'></div><div class='col-md-9'><p>");
                    sb.Append(post.content.Substring(0, Math.Min(post.content.Length, 350)));
                    sb.Append("......<a href='Article.aspx?articleId=");
                    sb.Append(a.Id);
                    sb.Append("'>Read More</a></p>");
                    sb.Append("</p></div></div>");

                    Literal lit = new Literal();
                    lit.Text = sb.ToString();

                    pblArticles.Controls.Add(lit);
                }
            }
        }
    }
}