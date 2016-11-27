using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class PostController
    {

        public List<Post> SearchPosts(List<string> keywords)
        {
            try
            {
                using(eLargesseEntities de = new eLargesseEntities())
                {
                    List<Post> result = new List<Post>();
                    foreach(string s in keywords)
                    {
                        var a = (from x in de.Posts where x.title.Contains(s)||
                                 x.Client.FirstName.ToLower().Contains(s)||
                                 x.Client.LastName.ToLower().Contains(s)||
                                 x.content.ToLower().Contains(s)
                                 select x).FirstOrDefault();
                        result.Add(a);
                    }
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public Post GetPost(int postId)
        {
            try
            {
                using(eLargesseEntities de = new eLargesseEntities())
                {
                    Post post = de.Posts.Find(postId);
                    return post;
                }
            }
            catch
            {
                return null;
            }
        }
        public Article GetArticle(int articleId)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Article post = de.Articles.Find(articleId);
                    return post;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool InsertPost(Post post)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Posts.Add(post);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertArticle(Article article)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Articles.Add(article);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Post> AllPosts()
        {
            try
            {
                using(eLargesseEntities de = new eLargesseEntities())
                {
                    List<Post> posts = (from x in de.Posts select x).ToList();
                    return posts;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Article> AllArticles()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Article> posts = (from x in de.Articles select x).ToList();
                    return posts;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool AddNewsCategory(ArticleCategory category)
        {
            try
            {
                using(eLargesseEntities de = new eLargesseEntities())
                {
                    de.ArticleCategories.Add(category);
                    de.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<ArticleCategory> GetAllCategories()
        {
            try
            {
                using(eLargesseEntities de = new eLargesseEntities())
                {
                    List<ArticleCategory> arts = (from x in de.ArticleCategories select x).ToList();
                    return arts;
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
