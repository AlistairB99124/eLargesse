using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class ManufacturerController
    {
        public bool Insert(Manufacturer manufacturer)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Manufacturers.Add(manufacturer);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Manufacturer manufacturer)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                Manufacturer p = de.Manufacturers.Find(id);
                p.Name = manufacturer.Name;
                p.Logo = manufacturer.Logo;

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
                Manufacturer p = de.Manufacturers.Find(id);
                de.Manufacturers.Attach(p);
                de.Manufacturers.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Manufacturer GetManufacturer(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Manufacturer manufacturer = de.Manufacturers.Find(id);
                    return manufacturer;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Manufacturer> manufacturers = (from x in de.Manufacturers select x).ToList();
                    return manufacturers;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
