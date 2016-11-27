using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class AddressController
    {

        public bool Insert(Address address)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Addresses.Add(address);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Address address)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                Address p = de.Addresses.Find(id);
                p.Address1 = address.Address1;
                p.Address2 = address.Address2;
                p.CIty = address.CIty;
                p.State = address.State;
                p.PostalCode = address.PostalCode;
                p.Country = address.Country;
                p.ClientId = address.ClientId;

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
                Address p = de.Addresses.Find(id);
                de.Addresses.Attach(p);
                de.Addresses.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Address GetAddress(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Address address = de.Addresses.Find(id);
                    return address;
                }
            }
            catch
            {
                return null;
            }
        }

        public Address GetAddressByClient(int clientId)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Address address = (from x in de.Addresses where x.ClientId == clientId select x).FirstOrDefault();
                    return address;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Address> GetAllAddresss()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Address> addresss = (from x in de.Addresses select x).ToList();
                    return addresss;
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
