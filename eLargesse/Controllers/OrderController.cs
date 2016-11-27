using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class OrderController : IDisposable
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

        public bool InsertOrder(Order order)
        {
            try
            {
                de.Orders.Add(order);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateOrder(int id, Order order)
        {
            try
            {
                Order p = de.Orders.Find(id);
                p.OrderDate = order.OrderDate;
                p.CustomerId = order.CustomerId;
                p.FirstName = order.FirstName;
                p.LastName = order.LastName;
                p.Address = order.Address;
                p.City = order.City;
                p.State = order.State;
                p.PostalCode = order.PostalCode;
                p.Country = order.Country;
                p.Cell = order.Cell;
                p.Email = order.Email;
                p.Total = order.Total;
                p.PaymentTransactionId = order.PaymentTransactionId;
                p.HasBeenShipping = order.HasBeenShipping;
                p.PaymentProcessed = order.PaymentProcessed;

                de.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                Order p = de.Orders.Find(id);
                de.Orders.Attach(p);
                de.Orders.Remove(p);
                de.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Order GetOrderbyOrderId(int orderId)
        {
            try
            {
                using (eLargesseEntities db = new eLargesseEntities())
                {
                    Order order = db.Orders.Find(orderId);
                    return order;
                }
            }
            catch
            {
                return null;
            }
        }
        public Order GetOrder(int clientId)
        {
            try
            {
                Order order = (from x in de.Orders where x.CustomerId == clientId && x.PaymentProcessed == false select x).FirstOrDefault();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public List<Product> GetAllProducts()
        {
            try
            {
                using (eLargesseEntities db = new eLargesseEntities())
                {
                    List<Product> products = (from x in db.Products select x).ToList();
                    return products;
                }
            }
            catch
            {
                return null;
            }
        }

        public void MarkPaymentProcessed(int orderId, bool result)
        {
            try
            {
                using (eLargesseEntities db = new eLargesseEntities())
                {
                    Order ord = db.Orders.Find(orderId);
                    ord.PaymentProcessed = result;
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public void MarkEFTPaymentProcessed(int orderId, bool result, string transactionID)
        {
            try
            {
                eLargesseEntities db = new eLargesseEntities();
                Order ord = db.Orders.Find(orderId);
                ord.PaymentProcessed = result;
                ord.PaymentTransactionId = transactionID;
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public void MarkAsShipped(int orderId, bool result)
        {
            try
            {
                Order ord = de.Orders.Find(orderId);
                ord.HasBeenShipping = result;
                de.SaveChanges();
            }
            catch
            {

            }
        }

        public List<OrderDetail> GetOrdersInOrder(int orderId)
        {

            List<OrderDetail> orders = (from x in de.OrderDetails
                                        where x.OrderId == orderId
                                        orderby x.ProductName descending
                                        select x).ToList();
            return orders;
        }

    }
}
