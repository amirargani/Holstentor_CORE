using Holstentor.Data;
using Holstentor.Data.Class_DbContext;
using Holstentor.Models.ProfileViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.AdminDashboardRepository
{
    public class AdminDashboard_Rep : IDisposable
    {
        private ApplicationDbContext db = null;
        public AdminDashboard_Rep()
        {
            db = new ApplicationDbContext();
        }
        public List<ApplicationUser> GetUsers()
        {
            // All Users
            var qusers = db.Users.ToList();
            return qusers;
        }
        public List<ApplicationUser> GetLastUsers()
        {
            var qlastusers = db.Users.Where(a => a.Date >= DateTime.Now.AddDays(-30)).ToList();
            return qlastusers;
        }
        public int GetUsersCount()
        {
            var roleid = db.Roles.Where(r => r.Name == "User").FirstOrDefault();
            var qgetuserscount = db.UserRoles.Where(a => a.RoleId == roleid.Id).Count();
            return qgetuserscount;
        }
        public int GetUsersNachrichtCount()
        {
            // SELECT DISTINCT UserIdSend FROM Tbl_Nachricht;
            // SELECT UserIdSend   
            // FROM Tbl_Nachricht INTERSECT SELECT UserId FROM AspNetUserRoles;
            //var qgetusersnachrichtcount = (from a in db.Tbl_Nachricht select a.UserIdSend).Intersect(from u in db.UserRoles select u.UserId).Count();
            var qgetusersnachrichtcount = db.Tbl_Nachricht.OrderByDescending(a => a.ID).Count();
            return qgetusersnachrichtcount;
        }
        public int GetTotalUsersNachrichtTrueCount(string username)
        {
            var quser = db.Users.Where(a => a.UserName == username).FirstOrDefault().Id;
            var qgetusersnachrichttruecount = db.Tbl_Nachricht.Where(a => a.Confirm == true && a.UserIdRecive == quser).Count();
            return qgetusersnachrichttruecount;
        }
        public int GetTotalUsersNachrichtFalseCount(string username)
        {
            var quser = db.Users.Where(a => a.UserName == username).FirstOrDefault().Id;
            var qgetusersnachrichtfalsecount = db.Tbl_Nachricht.Where(a => a.Confirm == false && a.UserIdRecive == quser).Count();
            return qgetusersnachrichtfalsecount;
        }
        public List<ApplicationUser> GetDetailsShow(string id)
        {
            var quasrid = db.Users.Where(a => a.Id == id).ToList();
            return quasrid;
        }
        public IList<ApplicationUser> GetUsersSend(string users = null)
        {
            try
            {
                //var qusers = db.Users.OrderByDescending(a => a.Date).ToList();
                //var qusers = db.Users.Where(a => a.Date >= DateTime.Now.AddDays(-30)).ToList().Take(10);
                var qusers = db.Users.Where(a => a.Date >= DateTime.Now.AddDays(-30)).ToList();
                var roleid = db.Roles.Where(r => r.Name == "User").FirstOrDefault();
                var qusersroleid = db.UserRoles.Where(a => a.RoleId == roleid.Id).ToList();
                if (qusers == null)
                    return null;
                else
                {
                    IList<ApplicationUser> lstausers = new List<ApplicationUser>();
                    foreach (var item in qusers)
                    {
                        foreach (var itemusersroleid in qusersroleid)
                        {
                            if (item.Id == itemusersroleid.UserId && item.EmailConfirmed == false)
                            {
                                ApplicationUser auser = new ApplicationUser();
                                auser.Name = item.Name;
                                auser.NameFamily = item.NameFamily;
                                auser.Email = item.Email;
                                auser.EmailConfirmed = item.EmailConfirmed;
                                auser.Id = item.Id;
                                auser.Date = item.Date;
                                auser.PhoneNumber = item.PhoneNumber;
                                lstausers.Add(auser);
                            }
                        }
                    }
                    //return lstausers ?? null;
                    return lstausers.Take(10).ToList() ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<ApplicationUser> GetUsersSendAll(string users = null)
        {
            try
            {
                var qusers = db.Users.OrderByDescending(a => a.Date).ToList();
                var roleid = db.Roles.Where(r => r.Name == "User").FirstOrDefault();
                var qusersroleid = db.UserRoles.Where(a => a.RoleId == roleid.Id).ToList();
                if (qusers == null)
                    return null;
                else
                {
                    IList<ApplicationUser> lstausers = new List<ApplicationUser>();
                    foreach (var item in qusers)
                    {
                        foreach (var itemusersroleid in qusersroleid)
                        {
                            if (item.Id == itemusersroleid.UserId)
                            {
                                ApplicationUser auser = new ApplicationUser();
                                auser.Name = item.Name;
                                auser.NameFamily = item.NameFamily;
                                auser.Email = item.Email;
                                auser.EmailConfirmed = item.EmailConfirmed;
                                auser.Id = item.Id;
                                auser.Date = item.Date;
                                auser.PhoneNumber = item.PhoneNumber;
                                lstausers.Add(auser);
                            }
                        }
                    }
                    return lstausers ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ApplicationUser GetUserNameFamily(string id)
        {
            if (id == null)
                return null;
            var quser = db.Users.Where(a => a.Id == id).SingleOrDefault();
            return quser ?? null;
        }
        public IList<Album_Db> GetAlbum(string album = null)
        {
            try
            {
                //var qalbum = db.Tbl_Album.Where(a => a.Date >= DateTime.Now.AddDays(-30)).ToList().Take(10);
                var qalbum = db.Tbl_Album.Where(a => a.ID > 0).ToList();
                if (qalbum == null)
                    return null;
                else
                {
                    IList<Album_Db> lstalbum = new List<Album_Db>();
                    foreach (var item in qalbum)
                    {
                        Album_Db aalbum = new Album_Db();
                        aalbum.Date = item.Date;
                        aalbum.NamePic = item.NamePic;
                        aalbum.Image = item.Image;
                        aalbum.ID = item.ID;
                        lstalbum.Add(aalbum);
                    }
                    return lstalbum ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Index_Db GetIndex(string index = null)
        {
            try
            {
                //var qindex = db.Tbl_Index.SingleOrDefault();
                var qindex = db.Tbl_Index.Where(a => a.IDIndex > 0).FirstOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    Index_Db aindex = new Index_Db();
                    aindex.IDIndex = qindex.IDIndex;
                    aindex.Title = qindex.Title;
                    aindex.TypedText1 = qindex.TypedText1;
                    aindex.TypedText2 = qindex.TypedText2;
                    aindex.TypedText3 = qindex.TypedText3;
                    aindex.TypedText4 = qindex.TypedText4;
                    aindex.TypedText5 = qindex.TypedText5;
                    return aindex;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<Kategorie_Db> GetCategory(string category = null)
        {
            try
            {
                var qcategory = db.Tbl_Kategorie.Where(a => a.ID > 0).ToList();
                if (qcategory == null)
                    return null;
                else
                {
                    IList<Kategorie_Db> lstcategory = new List<Kategorie_Db>();
                    foreach (var item in qcategory)
                    {
                        Kategorie_Db acategory = new Kategorie_Db();
                        acategory.ID = item.ID;
                        acategory.TitleCategory = item.TitleCategory;
                        acategory.Description = item.Description;
                        acategory.ActivePassive = item.ActivePassive;
                        acategory.FontName = item.FontName;
                        lstcategory.Add(acategory);
                    }
                    return lstcategory ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        ~AdminDashboard_Rep()
        {
            Dispose();
        }

        public void Dispose()
        {

        }
    }
}