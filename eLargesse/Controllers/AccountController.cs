using eLargesse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Controllers
{
    public class AccountController
    {
        public bool UpdateCell(string GUID, string Cell)
        {
            try
            {
                using (eLargesseEntities de = new eLargesseEntities())
                {
                    AspNetUser user = de.AspNetUsers.Find(GUID);
                    user.PhoneNumber = Cell;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
