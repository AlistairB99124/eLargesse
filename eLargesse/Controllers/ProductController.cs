using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class ProductController
    {
        public bool Insert(Product product)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Products.Add(product);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Product product)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                Product p = de.Products.Find(id);
                p.Name = product.Name;
                p.Price = product.Price;
                p.SubCategoryId = product.SubCategoryId;
                p.Description = product.Description;
                p.Image = product.Image;
                p.ManufacturerId = product.ManufacturerId;
                p.LastViewed = product.LastViewed;
                p.Sold = product.Sold;

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
                Product p = de.Products.Find(id);
                de.Products.Attach(p);
                de.Products.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Product product = de.Products.Find(id);
                    if (product.Sold == true)
                    {
                        return null;
                    }
                    else
                    {
                        return product;
                    }
                }
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
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> products = (from x in de.Products select x).ToList();
                    List<Product> existingProducts = new List<Product>();
                    foreach (Product p in products)
                    {
                        if (p.Sold == false)
                        {
                            existingProducts.Add(p);
                        }
                    }

                    return existingProducts;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetAllProductsOrderDate()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> products = (from x in de.Products orderby x.DateCreated descending select x).ToList();
                    List<Product> existingProducts = new List<Product>();
                    foreach(Product p in products)
                    {
                        if (p.Sold == false)
                        {
                            existingProducts.Add(p);
                        }
                    }

                    return existingProducts;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetProductsBySubType(int typeId)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> products = (from x in de.Products where x.SubCategoryId == typeId select x).ToList();
                    List<Product> existingProducts = new List<Product>();
                    foreach (Product p in products)
                    {
                        if (p.Sold == false)
                        {
                            existingProducts.Add(p);
                        }
                    }
                    
                    return existingProducts;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetSortedProductsBySubTypeName(string typeName, int index)
        {
            try
            {
                List<Product> sortedList;
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> prods = (from x in de.Products where x.SubCategory.Name == typeName select x).ToList();
                    List<Product> products = new List<Product>();
                    foreach (Product p in prods)
                    {
                        if (p.Sold == false)
                        {
                            products.Add(p);
                        }
                    }
                    
                    switch (index)
                    {
                        case 0:
                            sortedList = products.OrderBy(o => o.Price).ToList();
                            break;
                        case 1:
                            sortedList = products.OrderBy(o => o.Name).ToList();
                            break;
                        case 2:
                            sortedList = products.OrderBy(o => o.DateCreated).ToList();
                            break;
                        default:
                            sortedList = null;
                            break;
                    }

                    return sortedList;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetProductsByCategory(string typeName)
        {
            try
            {

                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> products = (from x in de.Products where x.SubCategory.Name == typeName select x).ToList();
                    List<Product> existingProducts = new List<Product>();
                    foreach (Product p in products)
                    {
                        if (p.Sold == false)
                        {
                            existingProducts.Add(p);
                        }
                    }

                    return existingProducts;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetSortedProductsByManuName(int manuId, int index)
        {
            try
            {
                List<Product> sortedList;
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> prods = (from x in de.Products where x.ManufacturerId == manuId && x.Sold == false select x).ToList();
                    List<Product> products = new List<Product>();
                    foreach (Product p in prods)
                    {
                        if (p.Sold == false)
                        {
                            products.Add(p);
                        }
                    }
                    switch (index)
                    {
                        case 0:
                            sortedList = products.OrderBy(o => o.Price).ToList();
                            break;
                        case 1:
                            sortedList = products.OrderBy(o => o.Name).ToList();
                            break;
                        case 2:
                            sortedList = products.OrderBy(o => o.DateCreated).ToList();
                            break;
                        default:
                            sortedList = null;
                            break;
                    }

                    return sortedList;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetSortedProductsByPriceRange(decimal minPrice, decimal maxPrice, int index)
        {
            try
            {
                List<Product> sortedList;
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> prods = (from x in de.Products where x.Price >= minPrice && x.Price <= maxPrice && x.Sold == false select x).ToList();
                    List<Product> products = new List<Product>();
                    foreach (Product p in prods)
                    {
                        if (p.Sold == false)
                        {
                            products.Add(p);
                        }
                    }
                    switch (index)
                    {
                        case 0:
                            sortedList = products.OrderBy(o => o.Price).ToList();
                            break;
                        case 1:
                            sortedList = products.OrderBy(o => o.Name).ToList();
                            break;
                        case 2:
                            sortedList = products.OrderBy(o => o.DateCreated).ToList();
                            break;
                        default:
                            sortedList = null;
                            break;
                    }

                    return sortedList;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetProductsBySearchQuery(string query)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Product> products = (from x in de.Products
                                              where x.Name.Contains(query) || x.Manufacturer.Name.Contains(query) || x.SubCategory.Name.Contains(query) || x.SubCategory.Category.Name.Contains(query)
                                              select x
                                              ).ToList();
                    List<Product> existingProducts = new List<Product>();
                    foreach (Product p in products)
                    {
                        if (p.Sold == false)
                        {
                            existingProducts.Add(p);
                        }
                    }

                    return existingProducts;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<int> SearchProducts(List<string> keywords)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<int> ids = new List<int>();
                    foreach (string x in keywords)
                    {
                        int i = (from z in de.Products where z.Name.Contains(x) select z.Id).FirstOrDefault();
                        ids.Add(i);
                    }

                    return ids;
                }
            }
            catch
            {
                return null;
            }
        }

        public void MarkDateSold(int productId, DateTime date, bool result)
        {
            try
            {
                using(eLargesseEntities de = new eLargesseEntities())
                {
                    Product prod = de.Products.Find(productId);
                    prod.DateSold = date;
                    prod.Sold = result;

                    de.SaveChanges();
                }
            }
            catch
            {

            }
        }
    }
}
