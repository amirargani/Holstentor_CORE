using Holstentor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Holstentor.Models.HomeViewModels;
using Holstentor.Data.Class_DbContext;

namespace Holstentor.Models.HomeRepository
{
    public class HomeRep : IDisposable
    {
        private ApplicationDbContext db = null;
        public HomeRep()
        {
            db = new ApplicationDbContext();
        }
        public IList<IndexViewModel> GetIndex(int id = 0) // Inaktiv
        {
            try
            {
                var qindex = db.Tbl_Index.Where(a => a.IDIndex > 0).FirstOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    var qindexlist = db.Tbl_Index.Where(a => a.IDIndex > 0).ToList();
                    if (qindexlist == null)
                        return null;
                    IList<IndexViewModel> lstivm = new List<IndexViewModel>();
                    foreach (var item in qindexlist)
                    {
                        IndexViewModel ivm = new IndexViewModel();
                        ivm.Title = item.Title;
                        ivm.TypedText1 = item.TypedText1;
                        ivm.TypedText2 = item.TypedText2;
                        ivm.TypedText3 = item.TypedText3;
                        ivm.TypedText4 = item.TypedText4;
                        ivm.TypedText5 = item.TypedText5;
                        lstivm.Add(ivm);
                    }

                    return lstivm ?? null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public IndexViewModel GetIndexSelect(string index = null)
        {
            try
            {
                var qindex = db.Tbl_Index.Where(a => a.IDIndex > 0).FirstOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    IndexViewModel ivm = new IndexViewModel();
                    ivm.Title = qindex.Title;
                    ivm.TypedText1 = qindex.TypedText1;
                    ivm.TypedText2 = qindex.TypedText2;
                    ivm.TypedText3 = qindex.TypedText3;
                    ivm.TypedText4 = qindex.TypedText4;
                    ivm.TypedText5 = qindex.TypedText5;
                    return ivm;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IndexEXViewModel GetIndexHeaderSelect(string indexheader = null)
        {
            try
            {
                var qindex = db.Tbl_IndexET.Where(a => a.IDIndex > 0).FirstOrDefault();
                if (qindex == null)
                    return null;
                else
                {
                    IndexEXViewModel ivm = new IndexEXViewModel();
                    ivm.IDIndex = qindex.IDIndex;
                    ivm.Email = qindex.Email;
                    ivm.PhoneNumber = qindex.PhoneNumber;
                    ivm.Street = qindex.Street;
                    ivm.Number = qindex.Number;
                    ivm.PostCode = qindex.PostCode;
                    ivm.City = qindex.City;
                    ivm.Country = qindex.Country;
                    ivm.NameSite = qindex.NameSite;
                    ivm.EmbedLinkGoogleMap = qindex.EmbedLinkGoogleMap;
                    ivm.Description = qindex.Description;
                    return ivm;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<AlbumViewModels> GetAlbum(string album = null)
        {
            try
            {
                var qalbum = db.Tbl_Album.Where(a => a.ID > 0).ToList();
                if (qalbum == null)
                    return null;
                else
                {
                    IList<AlbumViewModels> lstavm = new List<AlbumViewModels>();
                    foreach (var item in qalbum)
                    {
                        AlbumViewModels avm = new AlbumViewModels();
                        avm.NamePic = item.NamePic;
                        avm.Image = item.Image;
                        avm.Date = item.Date;
                        lstavm.Add(avm);
                    }

                    return lstavm ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetUserRole(string username)
        {
            try
            {
                // Select User: Email
                var quser = db.Users.Where(a => a.UserName.Equals(username)).FirstOrDefault();
                var roleid = db.Roles.Where(r => r.Name == "Admin").FirstOrDefault();
                var roleiduser = db.UserRoles.Where(r => r.UserId.Equals(quser.Id)).SingleOrDefault();
                if (quser == null)
                    return null;
                if (roleid.Id == roleiduser.RoleId && quser.Id == roleiduser.UserId && quser.EmailConfirmed == true)
                    return roleid.Name;
                if (quser.Id == roleiduser.UserId && quser.EmailConfirmed == true)
                    return "User";
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ApplicationUser GetUser(string id)
        {
            if (id == null)
                return null;
            var quser = db.Users.Where(a => a.Id == id).SingleOrDefault();
            return quser ?? null;
        }
        public IList<CategoryViewModel> GetCategoryLink(int id = 0)
        {
            try
            {
                var qcategory = db.Tbl_Kategorie.Where(a => a.ID > 0).ToList();
                if (qcategory == null)
                    return null;
                else
                {
                    IList<CategoryViewModel> lstcategory = new List<CategoryViewModel>();
                    foreach (var item in qcategory)
                    {
                        CategoryViewModel acategory = new CategoryViewModel();
                        acategory.ID = item.ID;
                        acategory.TitleCategory = item.TitleCategory;
                        acategory.Description = item.Description;
                        acategory.ActivePassive = item.ActivePassive;
                        acategory.FontName = item.FontName;
                        acategory.Link = /*item.TitleCategory + */ "@" + item.ID;
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
        public IList<SubCategoryViewModel> GetSubCategoryLink(int id = 0)
        {
            try
            {
                var qsubcategory = db.Tbl_Unterkategorie.Where(a => a.ID > 0).ToList();
                if (qsubcategory == null)
                    return null;
                else
                {
                    IList<SubCategoryViewModel> lstsubcategory = new List<SubCategoryViewModel>();
                    foreach (var item in qsubcategory)
                    {
                        SubCategoryViewModel asubcategory = new SubCategoryViewModel();
                        asubcategory.ID = item.ID;
                        asubcategory.CategoryID = item.CategoryID;
                        asubcategory.TitleSubCategory = item.TitleSubCategory;
                        asubcategory.Description = item.Description;
                        asubcategory.ActivePassive = item.ActivePassive;
                        asubcategory.FontName = item.FontName;
                        asubcategory.Subtitle1 = item.Subtitle1;
                        asubcategory.Subtitle2 = item.Subtitle2;
                        asubcategory.Subtitle3 = item.Subtitle3;
                        asubcategory.Link = /*item.TitleSubCategory + */ "@" + item.ID;
                        lstsubcategory.Add(asubcategory);
                    }
                    return lstsubcategory ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<SubCategoryViewModel> GetSubCategory(int id = 0)
        {
            try
            {
                var qsubcategory = db.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(id)).ToList();
                if (qsubcategory == null)
                    return null;
                else
                {
                    IList<SubCategoryViewModel> lstsubcategory = new List<SubCategoryViewModel>();
                    foreach (var item in qsubcategory)
                    {
                        SubCategoryViewModel asubcategory = new SubCategoryViewModel();
                        asubcategory.ID = item.ID;
                        asubcategory.CategoryID = item.CategoryID;
                        asubcategory.TitleSubCategory = item.TitleSubCategory;
                        asubcategory.Description = item.Description;
                        asubcategory.ActivePassive = item.ActivePassive;
                        asubcategory.FontName = item.FontName;
                        asubcategory.Subtitle1 = item.Subtitle1;
                        asubcategory.Subtitle2 = item.Subtitle2;
                        asubcategory.Subtitle3 = item.Subtitle3;
                        lstsubcategory.Add(asubcategory);
                    }
                    return lstsubcategory ?? null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Hochladen_Db GetHochladen(string index = null)
        {
            try
            {
                var qupload = db.Tbl_Hochladen.Where(a => a.ID > 0).FirstOrDefault();
                if (qupload == null)
                    return null;
                else
                {
                    Hochladen_Db up = new Hochladen_Db();
                    up.ID = qupload.ID;
                    up.Video = qupload.Video;
                    up.Logo = qupload.Logo;
                    up.ImageAbout = qupload.ImageAbout;
                    up.ImageGallery = qupload.ImageGallery;
                    up.ImageFooter = qupload.ImageFooter;
                    return up;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        ~HomeRep()
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