using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class CartController:IDisposable
    {
        private eLargesseEntities de = new eLargesseEntities();

        public void Dispose()
        {
            if (de != null)
            {
                de.Dispose();
                de = null;
            }
        }

        public bool Insert(Cart cart)
        {
            try
            {
                //
                de.Carts.Add(cart);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Cart cart)
        {
            try
            {
                
                Cart p = de.Carts.Find(id);
                p.Amount = cart.Amount;
                p.ClientId = cart.ClientId;
                p.DatePurchased = cart.DatePurchased;
                p.IsInCart = cart.IsInCart;
                p.ProductId = cart.ProductId;

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
                
                Cart p = de.Carts.Find(id);
                de.Carts.Attach(p);
                de.Carts.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<Cart> GetOrdersInCart(int clientId)
        {
            List<Cart> orders = (from x in de.Carts
                                 where x.ClientId == clientId
                                 && x.IsInCart
                                 orderby x.DatePurchased descending
                                 select x).ToList();


            return orders;
        }

        public decimal GetAmountOfOrders(int clientId)
        {
            try
            {
                decimal amount = (from x in de.Carts
                                  where x.ClientId == clientId
                                  && x.IsInCart
                                  select x.Amount).Sum();

                return amount;
            }
            catch
            {
                return 0;
            }
        }

        public decimal? GetTotalInCart(int clientId)
        {
            try
            {
                decimal? q = (from x in de.Carts
                              join z in de.Products on x.ProductId equals z.Id
                              where x.ClientId == clientId && x.IsInCart
                              select (z.Price * x.Amount)).Sum();

                return q;
            }
            catch
            {
                return 0;
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            Cart p = de.Carts.Find(id);
            p.Amount = quantity;

            de.SaveChanges();
        }

        public void EmptyCart(List<Cart> carts)
        {
            if (carts != null)
            {
                foreach (Cart cart in carts)
                {
                    Cart oldCart = de.Carts.Find(cart.Id);
                    oldCart.DatePurchased = DateTime.Now;
                    oldCart.IsInCart = false;
                }
                de.SaveChanges();
            }
        }
    }
}
