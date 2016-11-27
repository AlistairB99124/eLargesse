using eLargesse.Models;
using System.Collections.Generic;
using System.Linq;

namespace eLargesse.Controllers
{
    public class SubCategoryController
    {
        public bool Insert(SubCategory subCategory)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.SubCategories.Add(subCategory);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, SubCategory subCategory)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                SubCategory p = de.SubCategories.Find(id);
                p.Name = subCategory.Name;
                p.CategoryId = subCategory.CategoryId;
               
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
                SubCategory p = de.SubCategories.Find(id);
                de.SubCategories.Attach(p);
                de.SubCategories.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public SubCategory GetSubCategory(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    SubCategory subCategory = de.SubCategories.Find(id);
                    return subCategory;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SubCategory> GetAllSubCategories()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<SubCategory> subCategorys = (from x in de.SubCategories select x).ToList();
                    return subCategorys;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
