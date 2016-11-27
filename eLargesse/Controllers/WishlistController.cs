using eLargesse.Models;
using System.Collections.Generic;
using System.Linq;

namespace eLargesse.Controllers
{
    public class WishlistController
    {
        public bool Insert(Wishlist wishlist)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Wishlists.Add(wishlist);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Wishlist wishlist)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                Wishlist p = de.Wishlists.Find(id);
                p.CustomerId = wishlist.CustomerId;
                p.IsCurrent = wishlist.IsCurrent;
                p.Shared = wishlist.Shared;

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
                Wishlist p = de.Wishlists.Find(id);
                de.Wishlists.Attach(p);
                de.Wishlists.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Wishlist GetWishlist(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Wishlist wishlist = de.Wishlists.Find(id);
                    return wishlist;
                }
            }
            catch
            {
                return null;
            }
        }

        public Wishlist GetWishListByClient(int clientId)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Wishlist wishlist = (from x in de.Wishlists where x.CustomerId == clientId && x.IsCurrent select x).FirstOrDefault();
                    return wishlist;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Wishlist> GetAllWishlists()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Wishlist> wishlists = (from x in de.Wishlists select x).ToList();
                    return wishlists;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
