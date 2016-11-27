using eLargesse.Controllers;
using eLargesse.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLargesse.Admin
{
    public partial class Blog : System.Web.UI.Page
    {
        private PostController postController;
        private ClientController clientController;
        protected void Page_Load(object sender, EventArgs e)
        {
            postController = new PostController();
            clientController = new ClientController();
            FillDropDown();
            List<Article> articles = postController.AllArticles();
            FillPanel(articles);
        }

        
        private void FillPanel(List<Article> articles)
        {
            if(articles != null)
            {
                foreach(Article a in articles)
                {
                    StringBuilder sb = new StringBuilder();

                    Post post = postController.GetPost(a.PostId);
                    Client client = clientController.GetClient(post.author);
                    
                    sb.Append("<div class='well'><h3>");
                    sb.Append(post.title);
                    sb.Append("</h3><img src='../img/Articles/" + a.FeatureImg + "' alt='img'/><p>");
                    sb.Append(client.FirstName + " " + client.LastName);
                    sb.Append("</p><p>");
                    sb.Append(post.content);
                    sb.Append("</p></div>");

                    Literal lit = new Literal();
                    lit.Text = sb.ToString();

                    pnlBlogs.Controls.Add(lit);

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Client cleint = clientController.GetClientByGUID(Context.User.Identity.GetUserId());
            Post post = CreatePost(cleint);
            postController.InsertPost(post);

            string fileName;
            string path;

            var contentType = FeatureImageUpload.PostedFile.ContentType;
            var contentLength = FeatureImageUpload.PostedFile.ContentLength;

            if (FeatureImageUpload.HasFile)
            {
                try
                {
                    if (contentType == "image/jpeg" || contentType == "image/png")
                    {
                        if (contentLength < 2048000)
                        {
                            fileName = Path.GetFileName(FeatureImageUpload.PostedFile.FileName);
                            path = "~/img/Articles/" + fileName;
                            FeatureImageUpload.PostedFile.SaveAs(Server.MapPath(path));

                            Article article = CreateArticle(post, fileName);
                            postController.InsertArticle(article);

                            Response.Redirect("~/Admin/Blog");
                        }
                        else
                        {
                            StatusLabel.Text = "Upload status: The file has to be less than 2MB";
                        }
                    }
                    else
                    {
                        StatusLabel.Text = "Upload status: Only JPEG or PNG files are accepted";
                    }

                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }


        }

        private void FillDropDown()
        {
            List<ArticleCategory> categories = postController.GetAllCategories();
            ddlCategories.DataSource = categories;
            ddlCategories.DataBind();
        }

        private Post CreatePost(Client client)
        {
            Post post = new Post()
            {
                author = client.ID,
                content = txtContent.Text,
                title = txtTitle.Text,
                date_created = DateTime.Now,
                status = 2,
                url = "~/News/Article#Blog_" + DateTime.Now,
                Comments = null
            };
          
            return post;

        }
        private Article CreateArticle(Post post, string ImageUploaded)
        {
            Article article = new Article()
            {
                Category = ddlCategories.SelectedValue,
                Comment = null,
                CommentId = null,
                FeatureImg = ImageUploaded,
                PostId = post.Id
            };
            return article;
        }
    }
}