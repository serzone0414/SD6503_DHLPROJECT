using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SD6503_DHLPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SD6503_DHLPROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DhlDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DhlDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(LoginAccount user)
        {
            if (ModelState.IsValid)
            {
                _context.LoginAccounts.Add(user);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = user.Username + " is successfully registered.";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(LoginAccount user)
        {
            var account = _context.LoginAccounts.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (account != null)
            {
                HttpContext.Session.SetString("Identifier", account.Identifier.ToString());
                HttpContext.Session.SetString("Username", account.Username.ToString());
                HttpContext.Session.SetString("IsAdmin", account.IsAdmin.ToString());
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            return View();
        }


        public ActionResult LoggedIn()
        {
            if (HttpContext.Session.GetString("Identifier") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("Username");
                ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
