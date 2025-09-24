using Holstentor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.ProfileRepository
{
    public class UserRep : IDisposable
    {
        private ApplicationDbContext db = null;
        public UserRep()
        {
            db = new ApplicationDbContext();
        }
        public ApplicationUser GetUser(string username)
        {
            try
            {
                // Select User: Email
                var quser = db.Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                if (quser == null)
                    return null;
                else
                {
                    ApplicationUser auser = new ApplicationUser();
                    auser.Email = quser.Email;
                    auser.UserName = quser.UserName;
                    auser.Name = quser.Name;
                    auser.NameFamily = quser.NameFamily;
                    auser.PhoneNumber = quser.PhoneNumber;
                    return auser;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool InsertUser(string username)
        {
            try
            {
                var quser = db.Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                if (quser.Name != null && quser.Name != "" &&
                    quser.NameFamily != null && quser.NameFamily != "" &&
                    quser.PhoneNumber != null && quser.PhoneNumber != "")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }

        }
        ~UserRep()
        {
            Dispose();
        }
        public void Dispose()
        {

        }
        public void Dispose(bool Dis)
        {
            if (Dis)
            {
                Dispose();
            }
        }
    }
}