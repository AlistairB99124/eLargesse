using eLargesse.Controllers;
using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse.News
{
    public partial class Article : System.Web.UI.Page
    {
        private PostController postController;
        private ClientController clientController;
        protected void Page_Load(object sender, EventArgs e)
        {
            postController = new PostController();
            clientController = new ClientController();

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["articleId"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["articleId"]);

                    eLargesse.Models.Article article = postController.GetArticle(id);
                    eLargesse.Models.Post post = postController.GetPost(article.PostId);

                    Page.Title = post.title;

                    FillPanel(article);
                }
            }
        }

        private void FillPanel(eLargesse.Models.Article a)
        {

            Post post = postController.GetPost(a.PostId);
            Client client = clientController.GetClient(post.author);

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='well'>");
            sb.Append("<p><b>Author:</b>");
            sb.Append(client.FirstName + " " + client.LastName);
            sb.Append("<br/><b>Date Created:</b>");
            sb.Append(post.date_created.ToLongDateString());
            sb.Append("</p>");
            sb.Append("<div class='col-md-5'><img src='../img/Articles/");
            sb.Append(a.FeatureImg);
            sb.Append("' alt='' class='img-thumbnail' style='width:100%;' /></div><div class'col-md-7'>");
            sb.Append(post.content);
            sb.Append("</div>");

            Literal lit = new Literal();
            lit.Text = sb.ToString();

            pnlArticle.Controls.Add(lit);
        }
    }
}