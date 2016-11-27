using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class OrderDetailController:IDisposable
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
        public bool Insert(OrderDetail orderItem)
        {
            try
            {                
                de.OrderDetails.Add(orderItem);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, OrderDetail orderItem)
        {
            try
            {                
                OrderDetail p = de.OrderDetails.Find(id);
                p.OrderId = orderItem.OrderId;
                p.ProductId = orderItem.ProductId;
                p.Quantity = orderItem.Quantity;
                p.Discount = orderItem.Discount;
              

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
                OrderDetail p = de.OrderDetails.Find(id);
                de.OrderDetails.Attach(p);
                de.OrderDetails.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public OrderDetail GetOrderDetail(int id)
        {
            try
            {
                using (eLargesseEntities db = new eLargesseEntities())
                {
                    OrderDetail orderItem = db.OrderDetails.Find(id);
                    return orderItem;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<OrderDetail> GetAllOrderDetails(int OrderId)
        {
            try
            {
                using (eLargesseEntities db = new eLargesseEntities())
                {
                    List<OrderDetail> orderItems = (from x in db.OrderDetails where x.OrderId == OrderId select x).ToList();
                    return orderItems;
                }
            }
            catch
            {
                return null;
            }
        }

        public void EmptyCart(List<OrderDetail> orderDetails)
        {
            if (orderDetails != null)
            {
                using(eLargesseEntities db = new eLargesseEntities())
                {
                    foreach (OrderDetail cart in orderDetails)
                    {
                        db.OrderDetails.Attach(cart);
                        db.OrderDetails.Remove(cart);
                    }
                    db.SaveChanges();
                }
                
            }
        }

        public decimal? GetTotalInCart(int clientId)
        {
            try
            {
                decimal? q = (from x in de.OrderDetails select x.Quantity * x.UnitPrice).Sum();
                return q;
            }
            catch
            {
                return 0;
            }
        }
    }
}
