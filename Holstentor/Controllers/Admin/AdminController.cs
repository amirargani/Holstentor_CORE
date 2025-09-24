using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Holstentor.Data;
using Holstentor.Data.Class_DbContext;
using Holstentor.Models;
using Holstentor.Models.AdminDashboardRepository;
using Holstentor.Models.HomeRepository;
using Holstentor.Models.ProfileViewModels; // Add
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity; // Add
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add
using Microsoft.Extensions.Options; // Add

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Holstentor.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly AdminDashboard_Rep _adr = new AdminDashboard_Rep();
        public readonly HomeRep _ivm = new HomeRep();
        private IHostingEnvironment _environment;
        // Add
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _externalCookieScheme;
        private readonly ILogger _logger;
        public AdminController(ApplicationDbContext context, IHostingEnvironment environment,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            ILoggerFactory loggerFactory)
        {
            _environment = environment;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _logger = loggerFactory.CreateLogger<AdminController>();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> DetailsUsers(int page = 1)  // Inaktiv
        //{
        //    var qusers = await _context.Users.ToListAsync();
        //    return View(qusers.Skip(SkipNumber).Take(TakeNumber) ?? null);
        //}
        public IActionResult DetailsBenutzer(int page = 1) // DetailsUsers
        {
            int TakeNumber = 25;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            ViewData["DetailsBenutzerCount"] = _adr.GetUsersSendAll().Count();
            ViewData["DetailsBenutzerTakeNumber"] = TakeNumber;
            ViewData["DetailsBenutzerPage"] = page;
            return View(_adr.GetUsersSendAll().Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsBenutzerShow(string id) //  DetailsUserShow
        {
            return View(_adr.GetDetailsShow(id));
        }
        public IActionResult InaktivBenutzer(string id) // InactiveUser
        {
            if (id == null)
                return RedirectToAction("DetailsBenutzer");
            var applicationUser = _context.Users.Where(m => m.Id == id).SingleOrDefault();
            if (applicationUser == null)
                return RedirectToAction("DetailsBenutzer");
            applicationUser.EmailConfirmed = false;
            _context.Users.Update(applicationUser);
            _context.SaveChanges();
            return RedirectToAction("DetailsBenutzer");
        }
        public IActionResult AktivBenutzer(string id) // ActiveUser
        {
            if (id == null)
                return RedirectToAction("DetailsBenutzer");
            var applicationUser = _context.Users.Where(m => m.Id == id).SingleOrDefault();
            if (applicationUser == null)
                return RedirectToAction("DetailsBenutzer");
            applicationUser.EmailConfirmed = true;
            _context.Users.Update(applicationUser);
            _context.SaveChanges();
            return RedirectToAction("DetailsBenutzer");
        }
        public IActionResult Galerie(int page = 1) // Album
        {
            int TakeNumber = 25;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            var qalbum = _context.Tbl_Album.OrderByDescending(a => a.Date).ToList();
            ViewData["AlbumCount"] = qalbum.Count();
            ViewData["AlbumTakeNumber"] = TakeNumber;
            ViewData["AlbumPage"] = page;
            return View(qalbum.Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        public IActionResult GalerieErstellen() // AlbumCreate
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalerieErstellen([Bind("Date,NamePic")] Album_Db album, IFormFile Image) // AlbumCreate
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                    {
                        var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\album");
                        if (Image.Length <= 5242880) // > 0 - 5 MB
                        {
                            Random rnd = new Random();
                            string FileName = "album-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            Album_Db addalbum = new Album_Db();
                            addalbum.Date = DateTime.Now;
                            addalbum.Image = FileName;
                            addalbum.NamePic = album.NamePic;
                            _context.Add(addalbum);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Galerie");
                        }
                        else
                            return View(album);
                    }
                }
                else
                    return View(album);
            }
            return View(album);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalerieBearbeiten(int? id) // AlbumEdit
        {
            if (id == null)
            {
                return RedirectToAction("Galerie");
            }
            var qalbum = await _context.Tbl_Album.SingleOrDefaultAsync(m => m.ID == id);
            if (qalbum == null)
            {
                return RedirectToAction("Galerie");
            }
            return View(qalbum);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalerieBearbeitenConfirmed(int id, IFormFile Image, [Bind("ID,Date,NamePic")] Album_Db album) // AlbumEditConfirmed
        {
            if (id != album.ID)
            {
                return RedirectToAction("Galerie");
            }
            try
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                    {
                        var qupdate = await _context.Tbl_Album.Where(a => a.ID == album.ID).SingleOrDefaultAsync();
                        if (Image.Length <= 5242880)
                        {
                            var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\album");
                            var qalbumdelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\album\\" + qupdate.Image);
                            if (System.IO.File.Exists(qalbumdelete))
                            {
                                System.IO.File.Delete(qalbumdelete);
                            }
                            Random rnd = new Random();
                            string FileName = "album-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            qupdate.NamePic = album.NamePic;
                            qupdate.Image = FileName;
                            _context.Update(qupdate);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    var qupdate = await _context.Tbl_Album.Where(a => a.ID == album.ID).SingleOrDefaultAsync();
                    qupdate.NamePic = album.NamePic;
                    _context.Update(qupdate);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Galerie");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalerieExists(album.ID))
                {
                    return RedirectToAction("Galerie");
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalerieLöschen(int? id) // AlbumDelete
        {
            if (id == null)
            {
                return RedirectToAction("Galerie");
            }
            var qalbum = await _context.Tbl_Album.SingleOrDefaultAsync(m => m.ID == id);
            if (qalbum == null)
            {
                return RedirectToAction("Galerie");
            }
            return View(qalbum);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalerieLöschenConfirmed(int id) // AlbumDeleteConfirmed
        {
            var qalbum = await _context.Tbl_Album.SingleOrDefaultAsync(m => m.ID == id);
            var qalbumdelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\album\\" + qalbum.Image);
            if (System.IO.File.Exists(qalbumdelete))
            {
                System.IO.File.Delete(qalbumdelete);
            }
            _context.Tbl_Album.Remove(qalbum);
            await _context.SaveChangesAsync();
            return RedirectToAction("Galerie");
        }
        private bool GalerieExists(int id)
        {
            return _context.Tbl_Album.Any(e => e.ID == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Startseite(int? id = null) // Homepage
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var qindex = await _context.Tbl_Index.SingleOrDefaultAsync(m => m.IDIndex == id);
            if (qindex == null)
            {
                return RedirectToAction("Index");
            }
            return View(qindex);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartseiteConfirmed(int? id, Index_Db model) // HomepageConfirmed
        {
            var qindex = await _context.Tbl_Index.SingleOrDefaultAsync(m => m.IDIndex == id);
            if (id != qindex.IDIndex)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var qupdate = await _context.Tbl_Index.Where(a => a.IDIndex == qindex.IDIndex).SingleOrDefaultAsync();
                qupdate.Title = model.Title;
                qupdate.TypedText1 = model.TypedText1;
                qupdate.TypedText2 = model.TypedText2;
                qupdate.TypedText3 = model.TypedText3;
                qupdate.TypedText4 = model.TypedText4;
                qupdate.TypedText5 = model.TypedText5;
                _context.Update(qupdate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StartseiteExists(qindex.IDIndex))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool StartseiteExists(int id)
        {
            return _context.Tbl_Index.Any(e => e.IDIndex == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KopfzeileFusszeileUeber(int? id = null) // HeaderFooterAbout
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var qindex = await _context.Tbl_IndexET.SingleOrDefaultAsync(m => m.IDIndex == id);
            if (qindex == null)
            {
                return RedirectToAction("Index");
            }
            return View(qindex);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KopfzeileFusszeileUeberConfirmed(int? id, IndexET_Db model) // HeaderFooterAboutConfirmed
        {
            var qindex = await _context.Tbl_IndexET.SingleOrDefaultAsync(m => m.IDIndex == id);
            if (id != qindex.IDIndex)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var qupdate = await _context.Tbl_IndexET.Where(a => a.IDIndex == qindex.IDIndex).SingleOrDefaultAsync();
                qupdate.Street = model.Street;
                qupdate.Number = model.Number;
                qupdate.PostCode = model.PostCode;
                qupdate.City = model.City;
                qupdate.Country = model.Country;
                qupdate.Description = model.Description;
                //qupdate.Email = model.Email;
                qupdate.EmbedLinkGoogleMap = model.EmbedLinkGoogleMap;
                qupdate.NameSite = model.NameSite;
                qupdate.PhoneNumber = model.PhoneNumber;
                _context.Update(qupdate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KopfzeileFußzeileUeberExists(qindex.IDIndex))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool KopfzeileFußzeileUeberExists(int id)
        {
            return _context.Tbl_IndexET.Any(e => e.IDIndex == id);
        }
        public IActionResult Kategorie(int page = 1) // Category
        {
            int TakeNumber = 15;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            var qcategory = _context.Tbl_Kategorie.OrderBy(a => a.TitleCategory).ToList();
            ViewData["CategoryCount"] = qcategory.Count();
            ViewData["CategoryTakeNumber"] = TakeNumber;
            ViewData["CategoryPage"] = page;
            return View(qcategory.Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        public IActionResult KategorieInaktiv(int? id) // CategoryInactive
        {
            if (id <= 0)
                return RedirectToAction("Kategorie");
            var acategory = _context.Tbl_Kategorie.Where(m => m.ID == id).SingleOrDefault();
            if (acategory == null)
                return RedirectToAction("Kategorie");
            acategory.ActivePassive = false;
            _context.Tbl_Kategorie.Update(acategory);
            _context.SaveChanges();
            return RedirectToAction("Kategorie");
        }
        public IActionResult KategorieAktiv(int? id) // CategoryActive
        {
            if (id <= 0)
                return RedirectToAction("Kategorie");
            var acategory = _context.Tbl_Kategorie.Where(m => m.ID == id).SingleOrDefault();
            if (acategory == null)
                return RedirectToAction("Kategorie");
            acategory.ActivePassive = true;
            _context.Tbl_Kategorie.Update(acategory);
            _context.SaveChanges();
            return RedirectToAction("Kategorie");
        }
        public IActionResult KategorieErstellen() // CategoryCreate
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KategorieErstellen([Bind("TitleCategory,ActivePassive,FontName,Description")] Kategorie_Db category) // CategoryCreate
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Kategorie");
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KategorieBearbeiten(int? id) // CategoryEdit
        {
            if (id == null)
            {
                return RedirectToAction("Kategorie");
            }
            var qcategory = await _context.Tbl_Kategorie.SingleOrDefaultAsync(m => m.ID == id);
            if (qcategory == null)
            {
                return RedirectToAction("Kategorie");
            }
            return View(qcategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KategorieBearbeitenConfirmed(int id, [Bind("ID,TitleCategory,ActivePassive,FontName,Description")] Kategorie_Db category) // CategoryEditConfirmed
        {
            if (id != category.ID)
            {
                return RedirectToAction("Kategorie");
            }
            try
            {
                var qupdate = await _context.Tbl_Kategorie.Where(a => a.ID == category.ID).SingleOrDefaultAsync();
                qupdate.TitleCategory = category.TitleCategory;
                qupdate.FontName = category.FontName;
                qupdate.Description = category.Description;
                qupdate.ActivePassive = category.ActivePassive;
                _context.Update(qupdate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Kategorie");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorieExists(category.ID))
                {
                    return RedirectToAction("Kategorie");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool KategorieExists(int id)
        {
            return _context.Tbl_Kategorie.Any(e => e.ID == id);
        }
        public IActionResult Unterkategorie(int page = 1) // SubCategory
        {
            int TakeNumber = 25;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            var qsubcategory = _context.Tbl_Unterkategorie.OrderBy(a => a.TitleSubCategory).Include(a => a.Tbl_Kategorie).OrderBy(a => a.Tbl_Kategorie.TitleCategory).ToList();
            ViewData["SubCategoryCount"] = qsubcategory.Count();
            ViewData["SubCategoryTakeNumber"] = TakeNumber;
            ViewData["SubCategoryPage"] = page;
            return View(qsubcategory.Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        public IActionResult UnterkategorieInaktiv(int? id) // SubCategoryInactive
        {
            if (id <= 0)
                return RedirectToAction("Unterkategorie");
            var asubcategory = _context.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
            if (asubcategory == null)
                return RedirectToAction("Unterkategorie");
            asubcategory.ActivePassive = false;
            _context.Tbl_Unterkategorie.Update(asubcategory);
            _context.SaveChanges();
            return RedirectToAction("Unterkategorie");
        }
        public IActionResult UnterkategorieAktiv(int? id) // SubCategoryActive
        {
            if (id <= 0)
                return RedirectToAction("Unterkategorie");
            var asubcategory = _context.Tbl_Unterkategorie.Where(m => m.ID == id).SingleOrDefault();
            if (asubcategory == null)
                return RedirectToAction("Unterkategorie");
            asubcategory.ActivePassive = true;
            _context.Tbl_Unterkategorie.Update(asubcategory);
            _context.SaveChanges();
            return RedirectToAction("Unterkategorie");
        }
        public IActionResult UnterkategorieErstellen() // SubCategoryCreate
        {
            ViewBag.CategoryID = new SelectList(_context.Tbl_Kategorie, "ID", "TitleCategory");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnterkategorieErstellen([Bind("TitleSubCategory,CategoryID,ActivePassive,FontName,Description,Subtitle1,Subtitle2,Subtitle3")] Unterkategorie_Db subcategory) // SubCategoryCreate
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Unterkategorie");
            }
            return View(subcategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnterkategorieBearbeiten(int? id) // SubCategoryEdit
        {
            if (id == null)
            {
                return RedirectToAction("Unterkategorie");
            }
            var qsubcategory = await _context.Tbl_Unterkategorie.SingleOrDefaultAsync(m => m.ID == id);
            if (qsubcategory == null)
            {
                return RedirectToAction("Unterkategorie");
            }
            ViewBag.CategoryID = new SelectList(_context.Tbl_Kategorie, "ID", "TitleCategory");
            //ViewBag.ID = new SelectList(_context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(qsubcategory.CategoryID)), "ID", "TitleSubCategory");
            return View(qsubcategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnterkategorieBearbeitenConfirmed(int id, [Bind("ID,TitleSubCategory,CategoryID,ActivePassive,FontName,Description,Subtitle1,Subtitle2,Subtitle3")] Unterkategorie_Db subcategory)  // SubCategoryEditConfirmed
        {
            if (id != subcategory.ID)
            {
                return RedirectToAction("Unterkategorie");
            }
            try
            {
                var qproducts = await _context.Tbl_Produkte.Where(a => a.SubCategoryID == subcategory.ID).ToListAsync();
                var qupdate = await _context.Tbl_Unterkategorie.Where(a => a.ID == subcategory.ID).SingleOrDefaultAsync();
                foreach (var item in qproducts)
                {
                    if (item.SubCategoryID == subcategory.ID)
                    {
                        item.CategoryID = subcategory.CategoryID;
                        _context.Tbl_Produkte.Update(item);
                        await _context.SaveChangesAsync();
                    }
                }
                qupdate.TitleSubCategory = subcategory.TitleSubCategory;
                qupdate.CategoryID = subcategory.CategoryID;
                qupdate.FontName = subcategory.FontName;
                qupdate.Description = subcategory.Description;
                qupdate.Subtitle1 = subcategory.Subtitle1;
                qupdate.Subtitle2 = subcategory.Subtitle2;
                qupdate.Subtitle3 = subcategory.Subtitle3;
                qupdate.ActivePassive = subcategory.ActivePassive;
                _context.Update(qupdate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Unterkategorie");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnterkategorieExists(subcategory.ID))
                {
                    return RedirectToAction("Unterkategorie");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool UnterkategorieExists(int id)
        {
            return _context.Tbl_Unterkategorie.Any(e => e.ID == id);
        }
        public IActionResult Produkte(int page = 1) //Products
        {
            int TakeNumber = 200;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            var qproducts = _context.Tbl_Produkte.OrderByDescending(a => a.Date).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_User).Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Kategorie).ToList();
            ViewData["ProductsCount"] = qproducts.Count();
            ViewData["ProductsTakeNumber"] = TakeNumber;
            ViewData["ProductsPage"] = page;
            return View(qproducts.Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        public IActionResult ProduktInaktiv(int? id) // ProductInactive
        {
            if (id <= 0)
                return RedirectToAction("Produkte");
            var aproducts = _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
            if (aproducts == null)
                return RedirectToAction("Produkte");
            aproducts.ActiveInactive = false;
            _context.Tbl_Produkte.Update(aproducts);
            _context.SaveChanges();
            return RedirectToAction("Produkte");
        }
        public IActionResult ProduktAktiv(int? id) // ProductActive
        {
            if (id <= 0)
                return RedirectToAction("Produkte");
            var aproducts = _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
            if (aproducts == null)
                return RedirectToAction("Produkte");
            aproducts.ActiveInactive = true;
            _context.Tbl_Produkte.Update(aproducts);
            _context.SaveChanges();
            return RedirectToAction("Produkte");
        }
        public IActionResult ProduktKategorie()
        {
            return View(_ivm.GetCategoryLink());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProduktErstellen(int id = 0) // ProductsCreate
        {
            var qsubcategory = _context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(id)).FirstOrDefault();
            if (qsubcategory == null)
                return RedirectToAction("ProduktKategorie");
            //return View(_ivm.GetSubCategory(id));
            ViewBag.SubCategoryID = new SelectList(_context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(id)), "ID", "TitleSubCategory");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProduktErstellenConfirmed(/*string titlesubcategory,*/ Produkte_Db products) // ProductsCreateConfirmed
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var userid = _context.Users.Where(a => a.UserName == username && a.EmailConfirmed == true).FirstOrDefault();
                //var qsubcategory = _context.Tbl_Unterkategorie.Where(a => a.TitleSubCategory.Equals(titlesubcategory)).FirstOrDefault();
                var qsubcategory = _context.Tbl_Unterkategorie.Where(a => a.ID.Equals(products.SubCategoryID)).FirstOrDefault();
                Produkte_Db pk = new Produkte_Db();
                pk.Title = products.Title;
                pk.CategoryID = qsubcategory.CategoryID;
                pk.SubCategoryID = qsubcategory.ID;
                pk.Price1 = products.Price1;
                pk.Price2 = products.Price2;
                pk.Price3 = products.Price3;
                pk.Description = products.Description;
                pk.ActiveInactive = true;
                pk.Date = DateTime.Now;
                pk.UserID = userid.Id;
                _context.Add(pk);
                if (await _context.SaveChangesAsync() > 0)
                    return RedirectToAction("Produkte");
            }
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProdukteBearbeiten(int? id) // ProductsEdit
        {
            if (id == null)
            {
                return RedirectToAction("Produkte");
            }
            var qproducts = await _context.Tbl_Produkte.SingleOrDefaultAsync(m => m.ID == id);
            if (qproducts == null)
            {
                return RedirectToAction("Produkte");
            }
            //ViewBag.CategoryID = new SelectList(_context.Tbl_Kategorie, "ID", "TitleCategory", qproducts.CategoryID);
            ViewBag.CategoryID = new SelectList(_context.Tbl_Kategorie.Where(a => a.ID.Equals(qproducts.CategoryID)), "ID", "TitleCategory");
            ViewBag.SubCategoryID = new SelectList(_context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(qproducts.CategoryID)), "ID", "TitleSubCategory");
            //ViewBag.CategoryID = _context.Tbl_Kategorie.ToList();
            //ViewData["CategoryID"] = new SelectList(_context.Tbl_Kategorie, "ID", "TitleCategory", id);
            //ViewData["SubCategoryID"] = new SelectList(_context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(qproducts.CategoryID)), "ID", "TitleSubCategory");
            return View(qproducts);
        }
        public JsonResult GetCategoryList(int? id)
        {
            var qsubcategory = _context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(id)).ToList();
            return Json(qsubcategory);
            //List<Unterkategorie_Db> UnterkategorieList = _context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(id)).ToList();
            //return Json(new SelectList(UnterkategorieList/*, JsonRequestBehavior.AllowGet*/));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProdukteBearbeitenConfirmed(int id, [Bind("ID,Title,CategoryID,SubCategoryID,Description,Price1,Price2,Price3,UserID,Date,ActiveInactive")] Produkte_Db products) // ProductsEditConfirmed
        {
            if (id != products.ID)
            {
                return RedirectToAction("Produkte");
            }
            //if (products.SubCategoryID == null)
            //{
            //    ViewBag.CategoryID = new SelectList(_context.Tbl_Kategorie, "ID", "TitleCategory", products.CategoryID);
            //    ViewBag.SubCategoryID = new SelectList(_context.Tbl_Unterkategorie.Where(a => a.CategoryID.Equals(products.SubCategoryID)), "ID", "TitleSubCategory");
            //    return View("ProductsEdit", products);
            //}
            try
            {
                var username = User.Identity.Name;
                var userid = _context.Users.Where(a => a.UserName == username && a.EmailConfirmed == true).FirstOrDefault();
                var qsubcategory = _context.Tbl_Unterkategorie.Where(a => a.ID.Equals(products.SubCategoryID)).FirstOrDefault();
                var qupdate = await _context.Tbl_Produkte.Where(a => a.ID == products.ID).SingleOrDefaultAsync();
                qupdate.Title = products.Title;
                qupdate.CategoryID = qsubcategory.CategoryID;
                qupdate.SubCategoryID = products.SubCategoryID;
                qupdate.Price1 = products.Price1;
                qupdate.Price2 = products.Price2;
                qupdate.Price3 = products.Price3;
                qupdate.Description = products.Description;
                qupdate.ActiveInactive = products.ActiveInactive;
                qupdate.UserID = userid.Id;
                _context.Update(qupdate);
                await _context.SaveChangesAsync();
                ViewData["CategoryID"] = new SelectList(_context.Tbl_Kategorie, "ID", "TitleCategory", products.CategoryID);
                return RedirectToAction("Produkte");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdukteExists(products.ID))
                {
                    return RedirectToAction("Produkte");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool ProdukteExists(int id)
        {
            return _context.Tbl_Produkte.Any(e => e.ID == id);
        }
        public IActionResult Produktgalerie(int page = 1) // Gallery
        {
            int TakeNumber = 200;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            var qgallery = _context.Tbl_Produkte.OrderByDescending(a => a.Date).Include(a => a.Tbl_Galerie).Include(a => a.Tbl_Unterkategorie).Include(a => a.Tbl_Kategorie).ToList();
            ViewData["GalleryCount"] = qgallery.Count();
            ViewData["GalleryTakeNumber"] = TakeNumber;
            ViewData["GalleryPage"] = page;
            return View(qgallery.Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult GalleryCreate(int? id)  // Inaktiv
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Gallery");
        //    }
        //    var qproduct = _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
        //    ViewData["ProductID"] = qproduct.ID;
        //    if (qproduct == null)
        //    {
        //        return RedirectToAction("Gallery");
        //    }
        //    return View(qproduct);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProduktgalerieErstellen(int? id) // GalleryCreate
        {
            if (id == null)
            {
                return RedirectToAction("Produktgalerie");
            }
            var qproduct = _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
            if (qproduct ==null)
                return RedirectToAction("Produktgalerie");
            ViewData["ProductID"] = qproduct.ID;
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ProduktgalerieErstellen(int? id) // GalleryCreate  // Inaktiv
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Produktgalerie");
        //    }
        //    var qproduct = await _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefaultAsync();
        //    ViewData["ProductID"] = qproduct.ID;
        //    if (qproduct == null)
        //    {
        //        return RedirectToAction("Produktgalerie");
        //    }
        //    return View(qproduct);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProduktgalerieErstellenConfirmed(int id, [Bind("Date,NamePic")] Galerie_Db gallery, IFormFile Image) // GalleryCreateConfirmed
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                    {
                        var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\gallery");
                        var qproduct = _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefault();
                        if (Image.Length <= 5242880)
                        {
                            Random rnd = new Random();
                            string FileName = "gallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            Galerie_Db addgallery = new Galerie_Db();
                            addgallery.Date = DateTime.Now;
                            addgallery.Image = FileName;
                            addgallery.NamePic = gallery.NamePic;
                            addgallery.ProductID = qproduct.ID;
                            _context.Add(addgallery);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Produktgalerie");
                        }
                    }
                }
            }
            return RedirectToAction("Produktgalerie");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProduktgalerieBearbeiten(int? id) // GalleryEdit
        {
            if (id == null)
            {
                return RedirectToAction("Produktgalerie");
            }
            var qproduct = await _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefaultAsync();
            var qgallery = await _context.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefaultAsync();
            if (qproduct == null)
            {
                return RedirectToAction("Produktgalerie");
            }
            return View(qgallery);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ProduktgalerieBearbeiten(int? id) // GalleryEdit  // Inaktiv
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Produktgalerie");
        //    }
        //    var qproduct = await _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefaultAsync();
        //    var qgallery = await _context.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefaultAsync();
        //    ViewData["ID"] = qproduct.ID;
        //    ViewData["NamePic"] = qgallery.NamePic;
        //    ViewData["Image"] = qgallery.Image;
        //    if (qproduct == null)
        //    {
        //        return RedirectToAction("Produktgalerie");
        //    }
        //    return View(qproduct);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProduktgalerieBearbeitenConfirmed(int id, IFormFile Image, [Bind("ID,Date,NamePic")] Galerie_Db gallery) // GalleryEditConfirmed
        {
            var qproduct = await _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefaultAsync();
            var qgallery = await _context.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefaultAsync();
            if (id != qgallery.ProductID)
            {
                return RedirectToAction("Produktgalerie");
            }
            try
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/bmp")
                    {
                        var qupdate = await _context.Tbl_Galerie.Where(a => a.ID == qgallery.ID).SingleOrDefaultAsync();
                        if (Image.Length <= 5242880)
                        {
                            var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\gallery");
                            var qgallerydelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\gallery\\" + qupdate.Image);
                            if (System.IO.File.Exists(qgallerydelete))
                            {
                                System.IO.File.Delete(qgallerydelete);
                            }
                            Random rnd = new Random();
                            string FileName = "album-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            qupdate.NamePic = gallery.NamePic;
                            qupdate.Image = FileName;
                            _context.Update(qupdate);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    var qupdate = await _context.Tbl_Galerie.Where(a => a.ID == qgallery.ID).SingleOrDefaultAsync();
                    qupdate.NamePic = gallery.NamePic;
                    _context.Update(qupdate);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Produktgalerie");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduktgalerieExists(gallery.ID))
                {
                    return RedirectToAction("Produktgalerie");
                }
                else
                {
                    throw;
                }
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> GalleryDelete(int? id)  // Inaktiv
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Gallery");
        //    }
        //    var qproduct = await _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefaultAsync();
        //    var qgallery = await _context.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefaultAsync();
        //    ViewData["ID"] = qgallery.ID;
        //    if (qproduct == null)
        //    {
        //        return RedirectToAction("Gallery");
        //    }
        //    return View(qproduct);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProduktgalerieLöschenConfirmed(int id) // GalleryDeleteConfirmed
        {
            var qproduct = await _context.Tbl_Produkte.Where(m => m.ID == id).SingleOrDefaultAsync();
            var qgallery = await _context.Tbl_Galerie.Where(m => m.ProductID == qproduct.ID).SingleOrDefaultAsync();
            var qgallerydelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\gallery\\" + qgallery.Image);
            if (System.IO.File.Exists(qgallerydelete))
            {
                System.IO.File.Delete(qgallerydelete);
            }
            _context.Tbl_Galerie.Remove(qgallery);
            await _context.SaveChangesAsync();
            return RedirectToAction("Produktgalerie");
        }
        private bool ProduktgalerieExists(int id)
        {
            return _context.Tbl_Galerie.Any(e => e.ID == id);
        }
        public IActionResult Nachrichten(int page = 1)
        {
            int TakeNumber = 30;
            int SkipNumber = (TakeNumber * page) - TakeNumber;
            string admin = _ivm.GetUserRole(User.Identity.Name);
            var qadmin = _context.Roles.Where(a => a.Name == admin).FirstOrDefault();
            var quserrecive = _context.UserRoles.Where(a => a.RoleId == qadmin.Id).FirstOrDefault();
            var quser = _context.Users.Where(a => a.Id == quserrecive.UserId).First();
            var qmessage = _context.Tbl_Nachricht.Where(a => a.UserIdRecive == quser.Id).ToList();
            ViewData["MessagesCount"] = qmessage.Count();
            ViewData["MessagesTakeNumber"] = TakeNumber;
            ViewData["MessagesPage"] = page;
            return View(qmessage.Skip(SkipNumber).Take(TakeNumber) ?? null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NachrichtenAntworten(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Nachrichten");
            }
            var qmessage = _context.Tbl_Nachricht.Where(a => a.ID.Equals(id)).FirstOrDefault();
            if (qmessage == null)
            {
                return RedirectToAction("Nachrichten");
            }
            return View(qmessage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NachrichtenAntwortenConfirmed(int id, [Bind("TextAdmin")] Nachricht_Db nachricht)
        {
            try
            {
                var qid = _context.Tbl_Nachricht.Where(a => a.ID == id).First();
                qid.Confirm = true;
                qid.TextAdmin = nachricht.TextAdmin;
                qid.DateUpdate = DateTime.Now;
                _context.Tbl_Nachricht.Update(qid);
                await _context.SaveChangesAsync();
                return RedirectToAction("Nachrichten");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NachrichtenExists(nachricht.ID))
                {
                    return RedirectToAction("Nachrichten");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool NachrichtenExists(int id)
        {
            return _context.Tbl_Nachricht.Any(e => e.ID == id);
        }
        [HttpGet]
        public IActionResult PasswortAendern() // ChangePassword
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswortAendern(ChangePasswordViewModel model) // ChangePassword
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(PasswortAendern), "Admin");
                }
                return RedirectToAction(nameof(PasswortAendern), "Admin");
            }
            return RedirectToAction(nameof(PasswortAendern), "Admin");
        }
        private Task<ApplicationUser> GetCurrentUserAsync() // Add
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VideoBearbeiten(int? id = null) // VideoEdit
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var qvideo = await _context.Tbl_Hochladen.SingleOrDefaultAsync(m => m.ID == id);
            if (qvideo == null)
            {
                return RedirectToAction("Index");
            }
            return View(qvideo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VideoBearbeitenConfirmed(int id, IFormFile Video, [Bind("ID,Video")] Hochladen_Db upload) // VideoEditConfirmed
        {
            if (id != upload.ID)
            {
                return RedirectToAction("Index");
            }
            try
            {
                if (Video != null)
                {
                    if (Video.ContentType == "video/mp4")
                    {
                        var qupdate = await _context.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefaultAsync();
                        if (Video.Length <= 5242880)
                        {
                            var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\video\\");
                            var qvideodelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\video\\" + qupdate.Video);
                            if (System.IO.File.Exists(qvideodelete))
                            {
                                System.IO.File.Delete(qvideodelete);
                            }
                            Random rnd = new Random();
                            string FileName = "video-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Video.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Video.CopyTo(fileStream);
                            }
                            qupdate.Video = FileName;
                            _context.Update(qupdate);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(upload.ID))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FotoUeberunsBearbeiten(int? id = null) // Edit
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var qimage = await _context.Tbl_Hochladen.SingleOrDefaultAsync(m => m.ID == id);
            if (qimage == null)
            {
                return RedirectToAction("Index");
            }
            return View(qimage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FotoUeberunsBearbeitenConfirmed(int id, IFormFile Image, [Bind("ID,ImageAbout")] Hochladen_Db upload) // EditConfirmed
        {
            if (id != upload.ID)
            {
                return RedirectToAction("Index");
            }
            try
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var qupdate = await _context.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefaultAsync();
                        if (Image.Length <= 5242880)
                        {
                            var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\about\\");
                            var qimagedelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\about\\" + qupdate.ImageAbout);
                            if (System.IO.File.Exists(qimagedelete))
                            {
                                System.IO.File.Delete(qimagedelete);
                            }
                            Random rnd = new Random();
                            string FileName = "about-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            qupdate.ImageAbout = FileName;
                            _context.Update(qupdate);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(upload.ID))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FotoGalerieBearbeiten(int? id = null) // Edit
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var qimage = await _context.Tbl_Hochladen.SingleOrDefaultAsync(m => m.ID == id);
            if (qimage == null)
            {
                return RedirectToAction("Index");
            }
            return View(qimage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FotoGalerieBearbeitenConfirmed(int id, IFormFile Image, [Bind("ID,ImageGallery")] Hochladen_Db upload) // EditConfirmed
        {
            if (id != upload.ID)
            {
                return RedirectToAction("Index");
            }
            try
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var qupdate = await _context.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefaultAsync();
                        if (Image.Length <= 5242880)
                        {
                            var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\gallery\\");
                            var qimagedelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\gallery\\" + qupdate.ImageGallery);
                            if (System.IO.File.Exists(qimagedelete))
                            {
                                System.IO.File.Delete(qimagedelete);
                            }
                            Random rnd = new Random();
                            string FileName = "gallery-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            qupdate.ImageGallery = FileName;
                            _context.Update(qupdate);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(upload.ID))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FotoFusszeileBearbeiten(int? id = null) // Edit
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var qimage = await _context.Tbl_Hochladen.SingleOrDefaultAsync(m => m.ID == id);
            if (qimage == null)
            {
                return RedirectToAction("Index");
            }
            return View(qimage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FotoFusszeileBearbeitenConfirmed(int id, IFormFile Image, [Bind("ID,ImageFooter")] Hochladen_Db upload) // EditConfirmed
        {
            if (id != upload.ID)
            {
                return RedirectToAction("Index");
            }
            try
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var qupdate = await _context.Tbl_Hochladen.Where(a => a.ID == upload.ID).SingleOrDefaultAsync();
                        if (Image.Length <= 5242880)
                        {
                            var uploads = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\footer\\");
                            var qimagedelete = Path.Combine(_environment.WebRootPath, "assets\\img\\backgrounds\\upload\\footer\\" + qupdate.ImageFooter);
                            if (System.IO.File.Exists(qimagedelete))
                            {
                                System.IO.File.Delete(qimagedelete);
                            }
                            Random rnd = new Random();
                            string FileName = "footer-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + rnd.Next(1, 10000000).ToString() + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            qupdate.ImageFooter = FileName;
                            _context.Update(qupdate);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(upload.ID))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool UploadExists(int id)
        {
            return _context.Tbl_Hochladen.Any(e => e.ID == id);
        }
    }
}