using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class CategoryController
    {
        public bool Insert(Category category)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Categories.Add(category);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Category category)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                Category p = de.Categories.Find(id);
                p.Name = category.Name;             

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
                Category p = de.Categories.Find(id);
                de.Categories.Attach(p);
                de.Categories.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Category GetCategory(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Category category = de.Categories.Find(id);
                    return category;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Category> GetAllCategorys()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Category> categorys = (from x in de.Categories select x).ToList();
                    return categorys;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
