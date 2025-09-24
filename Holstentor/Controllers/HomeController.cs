using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Holstentor.Data;
using Holstentor.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Holstentor.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = null;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ueberuns()
        {
            return View();
        }

        public IActionResult Kontakt()
        {
            return View();
        }
        public IActionResult Galerie()
        {
            return View();
        }
        public IActionResult Impressum()
        {
            return View();
        }
        public IActionResult Datenschutz()
        {
            return View();
        }
        public IActionResult Menue(string id = null)
        {
            var qproducts = db.Tbl_Produkte.OrderBy(a => a.Tbl_Kategorie.TitleCategory).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_User).Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Kategorie).ToList();
            if (qproducts == null)
                return View(null);
            List<VmCategoryProductViewModel> lstcategoryproducts = new List<VmCategoryProductViewModel>();
            foreach (var item in qproducts)
            {
                VmCategoryProductViewModel vm = new VmCategoryProductViewModel();
                vm.IDProduct = item.ID;
                vm.TitleProduct = item.Title;
                vm.DescriptionProduct = item.Description;
                vm.PriceProduct1 = item.Price1;
                vm.PriceProduct2 = item.Price2;
                vm.PriceProduct3 = item.Price3;
                vm.DateProduct = item.Date;
                vm.IDCategory = item.CategoryID;
                vm.TitleCategory = item.Tbl_Kategorie.TitleCategory;
                vm.DescriptionCategory = item.Tbl_Kategorie.Description;
                vm.IDSubCategory = item.SubCategoryID;
                vm.TitleSubCategory = item.Tbl_Unterkategorie.TitleSubCategory;
                vm.DescriptionSubCategory = item.Tbl_Unterkategorie.Description;
                vm.SubtitleSubCategory1 = item.Tbl_Unterkategorie.Subtitle1;
                vm.SubtitleSubCategory2 = item.Tbl_Unterkategorie.Subtitle2;
                vm.SubtitleSubCategory3 = item.Tbl_Unterkategorie.Subtitle3;
                var qgalerie = item.Tbl_Galerie.Where(a => a.ProductID == item.ID).FirstOrDefault();
                if (qgalerie != null)
                {
                    vm.ImageGallery = "..\\assets\\img\\backgrounds\\gallery\\" + qgalerie.Image;
                    vm.NamePicGallery = qgalerie.NamePic;
                }
                else
                {
                    vm.ImageGallery = "..\\assets\\img\\backgrounds\\gallery\\default.jpg";
                }
                lstcategoryproducts.Add(vm);
            }
            return View(lstcategoryproducts ?? null);
            //var qprodukte = db.Tbl_Produkte.OrderByDescending(a => a.Date).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_User).Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Kategorie).ToList();
            //return View(qprodukte ?? null);
        }
        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MessageUser(string name, string email, string message)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.Body = "<b>" + "Gastbenutzer: " + name + "</b>" + "<br />" + "<b>" + "E-Mail-Adresse: " + email + "</b>" + "<br />" + "<b>" + "Nachricht: " + "<br />" + message.ToString().Replace("\n", "<br />") + "</b>";
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.From = new MailAddress("restaurantamholstentor@gmail.com", "Gastbenutzer", Encoding.UTF8);
                msg.Priority = MailPriority.Normal;
                msg.Sender = msg.From;
                msg.Subject = "Gastbenutzer: " + email;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.To.Add(new MailAddress("restaurantamholstentor@gmail.com", "restaurantamholstentor@gmail.com", Encoding.UTF8));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("restaurantamholstentor@gmail.com", "+H0123456789");

                smtp.Send(msg);
                return RedirectToAction(nameof(Kontakt));

            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Kontakt));

            }
        }
        [HttpGet]
        public IActionResult Kategoriesuche(string id) // CategorySearch
        {
            string categoryid = "";
            string titlecategory = "";
            List<string> lstcategory = new List<string>();
            var qcategory = id.Trim().Split('@');
            foreach (var item in qcategory)
            {
                string name = item.Trim();
                lstcategory.Add(name);
            }
            categoryid = lstcategory.LastOrDefault();
            titlecategory = lstcategory.FirstOrDefault();
            int? codecategoryid = Convert.ToInt32(categoryid);
            var qcategoryselect = db.Tbl_Kategorie.Where(a => a.ID.Equals(codecategoryid)).FirstOrDefault();
            if (qcategoryselect == null || qcategory == null)
                return RedirectToAction(nameof(Error));
            var qproducts = db.Tbl_Produkte.Where(a => a.CategoryID.Equals(qcategoryselect.ID)).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_Galerie).OrderByDescending(a => a.Date);
            if (qproducts == null)
                return View(null);
            List<VmCategoryProductViewModel> lstcategoryproducts = new List<VmCategoryProductViewModel>();
            foreach (var item in qproducts)
            {
                VmCategoryProductViewModel vm = new VmCategoryProductViewModel();
                vm.IDProduct = item.ID;
                vm.TitleProduct = item.Title;
                vm.DescriptionProduct = item.Description;
                vm.PriceProduct1 = item.Price1;
                vm.PriceProduct2 = item.Price2;
                vm.PriceProduct3 = item.Price3;
                vm.DateProduct = item.Date;
                vm.IDCategory = item.CategoryID;
                vm.TitleCategory = item.Tbl_Kategorie.TitleCategory;
                vm.DescriptionCategory = item.Tbl_Kategorie.Description;
                vm.IDSubCategory = item.SubCategoryID;
                vm.TitleSubCategory = item.Tbl_Unterkategorie.TitleSubCategory;
                vm.DescriptionSubCategory = item.Tbl_Unterkategorie.Description;
                vm.SubtitleSubCategory1 = item.Tbl_Unterkategorie.Subtitle1;
                vm.SubtitleSubCategory2 = item.Tbl_Unterkategorie.Subtitle2;
                vm.SubtitleSubCategory3 = item.Tbl_Unterkategorie.Subtitle3;
                var qgalerie = item.Tbl_Galerie.Where(a => a.ProductID == item.ID).FirstOrDefault();
                if (qgalerie != null)
                {
                    vm.ImageGallery = "..\\assets\\img\\backgrounds\\gallery\\" + qgalerie.Image;
                    vm.NamePicGallery = qgalerie.NamePic;
                }
                else
                {
                    vm.ImageGallery = "..\\assets\\img\\backgrounds\\gallery\\default.jpg";
                }
                lstcategoryproducts.Add(vm);
            }
            return View(lstcategoryproducts ?? null);
        }
        [HttpGet]
        public IActionResult Unterkategoriesuche(string id) // SubCategorySearch
        {
            string subcategoryid = "";
            string titlesubcategory = "";
            List<string> lstsubcategory = new List<string>();
            var qsubcategory = id.Trim().Split('@');
            foreach (var item in qsubcategory)
            {
                string name = item.Trim();
                lstsubcategory.Add(name);
            }
            subcategoryid = lstsubcategory.LastOrDefault();
            titlesubcategory = lstsubcategory.FirstOrDefault();
            int? codesubcategoryid = Convert.ToInt32(subcategoryid);
            var qsubcategoryselect = db.Tbl_Unterkategorie.Where(a => /*a.TitleSubCategory.Equals(titlesubcategory) &&*/ a.ID.Equals(codesubcategoryid)).FirstOrDefault();
            var qcategoryselect = db.Tbl_Kategorie.Where(a => a.ID.Equals(qsubcategoryselect.CategoryID)).FirstOrDefault();
            if (qsubcategoryselect == null || qsubcategory == null)
                return RedirectToAction(nameof(Error));
            var qproducts = db.Tbl_Produkte.Where(a => a.CategoryID.Equals(qcategoryselect.ID) && a.SubCategoryID.Equals(qsubcategoryselect.ID) && a.ActiveInactive == true).Include(a => a.Tbl_Galerie).OrderByDescending(a => a.Date);
            if (qproducts == null)
                return View(null);
            List<VmCategoryProductViewModel> lstsubcategoryproducts = new List<VmCategoryProductViewModel>();
            foreach (var item in qproducts)
            {
                VmCategoryProductViewModel vm = new VmCategoryProductViewModel();
                vm.IDProduct = item.ID;
                vm.TitleProduct = item.Title;
                vm.DescriptionProduct = item.Description;
                vm.PriceProduct1 = item.Price1;
                vm.PriceProduct2 = item.Price2;
                vm.PriceProduct3 = item.Price3;
                vm.DateProduct = item.Date;
                vm.IDCategory = item.CategoryID;
                vm.TitleCategory = item.Tbl_Kategorie.TitleCategory;
                vm.DescriptionCategory = item.Tbl_Kategorie.Description;
                vm.IDSubCategory = item.SubCategoryID;
                vm.TitleSubCategory = item.Tbl_Unterkategorie.TitleSubCategory;
                vm.DescriptionSubCategory = item.Tbl_Unterkategorie.Description;
                vm.SubtitleSubCategory1 = item.Tbl_Unterkategorie.Subtitle1;
                vm.SubtitleSubCategory2 = item.Tbl_Unterkategorie.Subtitle2;
                vm.SubtitleSubCategory3 = item.Tbl_Unterkategorie.Subtitle3;
                var qgalerie = item.Tbl_Galerie.Where(a => a.ProductID == item.ID).FirstOrDefault();
                if (qgalerie != null)
                {
                    vm.ImageGallery = "..\\assets\\img\\backgrounds\\gallery\\" + qgalerie.Image;
                    vm.NamePicGallery = qgalerie.NamePic;
                }
                else
                {
                    vm.ImageGallery = "..\\assets\\img\\backgrounds\\gallery\\default.jpg";
                }
                lstsubcategoryproducts.Add(vm);
            }
            return View(lstsubcategoryproducts ?? null);
        }
    }
}