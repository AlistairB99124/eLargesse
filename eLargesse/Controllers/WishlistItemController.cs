using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class WishlistItemController
    {
        public bool Insert(WishlistItem wishlistItem)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.WishlistItems.Add(wishlistItem);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, WishlistItem wishlistItem)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                WishlistItem p = de.WishlistItems.Find(id);
                p.WishlistId = wishlistItem.WishlistId;
                p.ProductId = wishlistItem.ProductId;
                p.Created = wishlistItem.Created;
                p.Description = wishlistItem.Description;

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
                WishlistItem p = de.WishlistItems.Find(id);
                de.WishlistItems.Attach(p);
                de.WishlistItems.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public WishlistItem GetWishlistItem(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    WishlistItem wishlistItem = de.WishlistItems.Find(id);
                    return wishlistItem;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<WishlistItem> GetAllWishlistItems(Wishlist wishlist)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<WishlistItem> wishlistItems = (from x in de.WishlistItems where x.WishlistId == wishlist.Id select x).ToList();
                    return wishlistItems;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetProductsInList(List<WishlistItem> wishlist)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> products = new List<Product>();
                    foreach (WishlistItem w in wishlist)
                    {
                        Product product = (from x in de.Products where x.Id == w.ProductId select x).FirstOrDefault();
                        products.Add(product);
                    }
                    return products;

                }
            }
            catch
            {
                return null;
            }
        }
    }
}
