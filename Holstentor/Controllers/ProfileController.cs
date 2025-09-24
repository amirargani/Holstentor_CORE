using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Holstentor.Data;
using Holstentor.Data.Class_DbContext;
using Holstentor.Models;
using Holstentor.Models.ProfileRepository;
using Holstentor.Models.ProfileViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // Add
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add
using Microsoft.Extensions.Options; // Add

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Holstentor.Controllers
{
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        // Add
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _externalCookieScheme;
        private readonly ILogger _logger;

        // GET: /<controller>/
        private ApplicationDbContext db = null;
        private UserRep auserrep = null;
        public ProfileController( // Add
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _logger = loggerFactory.CreateLogger<ProfileController>();
            db = new ApplicationDbContext();
            auserrep = new UserRep();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Nachricht() // Message
        {
            if (auserrep.InsertUser(User.Identity.Name) == false)
                return RedirectToAction("Index", "Profile");
            else
                return View();
        }

        //public IActionResult MessageReply() // Inaktiv
        //{
        //    if (auserrep.InsertUser(User.Identity.Name) == false)
        //        return RedirectToAction("Index", "Profile");
        //    else
        //        return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NachrichtGesendet(MessageViewModel model) // MessageSend
        {
            try
            {
                var qadmin = db.Roles.Where(a => a.Name == "Admin").FirstOrDefault();
                var quserrecive = db.UserRoles.Where(a => a.RoleId == qadmin.Id).FirstOrDefault();
                var qusersend = db.Users.Where(a => a.UserName == model.UserIdSend).FirstOrDefault();
                Nachricht_Db msg = new Nachricht_Db();
                msg.Confirm = false;
                msg.Date = DateTime.Now;
                msg.Text = model.Text;
                msg.TextAdmin = "-";
                msg.UserIdRecive = quserrecive.UserId;
                msg.UserIdSend = qusersend.Id;
                db.Tbl_Nachricht.Add(msg);
                db.SaveChanges();
                return RedirectToAction(nameof(Nachricht));
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Nachricht));
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MeinProfil(UserViewModel model) // MyProfile
        {
            var username = User.Identity.Name;
            var quser = db.Users.Where(a => a.UserName == username).FirstOrDefault();
            quser.Name = model.Name;
            quser.NameFamily = model.NameFamily;
            quser.PhoneNumber = model.PhoneNumber;
            db.Users.Update(quser);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MeinProfilBearbeiten(UserViewModel model) // EditMyProfile
        {
            // Select User: Email
            var username = User.Identity.Name;
            var quser = db.Users.Where(a => a.UserName == username).FirstOrDefault();
            quser.Name = model.Name;
            quser.NameFamily = model.NameFamily;
            quser.PhoneNumber = model.PhoneNumber;
            db.Users.Update(quser);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
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
                    return RedirectToAction(nameof(PasswortAendern), "Profile");
                }
                return RedirectToAction(nameof(PasswortAendern), "Profile");
            }
            return RedirectToAction(nameof(PasswortAendern), "Profile");
        }
        private Task<ApplicationUser> GetCurrentUserAsync() // Add
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
