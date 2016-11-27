using eLargesse.Models;

namespace eLargesse.Controllers
{
    public class ProductViewController
    {
        public bool Insert(ProductView productView)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.ProductViews.Add(productView);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, ProductView productView)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                ProductView p = de.ProductViews.Find(id);
                p.ProductId = productView.Id;

                de.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                ProductView p = de.ProductViews.Find(id);
                de.ProductViews.Attach(p);
                de.ProductViews.Remove(p);
                de.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ProductView GetProductView(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    ProductView productView = de.ProductViews.Find(id);
                    return productView;
                }
            }
            catch
            {
                return null;
            }
        }
    }
    public class T
    {
        public int ProductViewId { get; set; }

        public int Count { get; set; }

    }
}
