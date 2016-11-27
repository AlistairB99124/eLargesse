using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class ClientController
    {
        public bool Insert(Client client)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                de.Clients.Add(client);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int id, Client client)
        {
            try
            {
                eLargesseEntities de = new eLargesseEntities();
                Client p = de.Clients.Find(id);
                p.FirstName = client.FirstName;
                p.GUID = client.GUID;
                p.LastName = client.LastName;                

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
                Client p = de.Clients.Find(id);
                de.Clients.Attach(p);
                de.Clients.Remove(p);
                de.SaveChanges();

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Client GetClient(int id)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Client client = de.Clients.Find(id);
                    return client;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Client> GetAllClients()
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    List<Client> clients = (from x in de.Clients select x).ToList();
                    return clients;
                }
            }
            catch
            {
                return null;
            }
        }

        public Client GetClientByGUID(string guid)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    Client client = (from x in de.Clients where x.GUID == guid select x).FirstOrDefault();
                    return client;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
