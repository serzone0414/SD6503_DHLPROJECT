using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD6503_DHLPROJECT.Models;
using Microsoft.AspNetCore.Http;

namespace SD6503_DHLPROJECT.Controllers
{
    public class LoginAccountsController : Controller
    {
        private readonly DhlDatabaseContext _context;

        public LoginAccountsController(DhlDatabaseContext context)
        {
            _context = context;
        }

        // GET: LoginAccounts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                return View(await _context.LoginAccounts.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: LoginAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var loginAccount = await _context.LoginAccounts
                    .FirstOrDefaultAsync(m => m.Identifier == id);
                if (loginAccount == null)
                {
                    return NotFound();
                }

                return View(loginAccount);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: LoginAccounts/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: LoginAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,IsAdmin,Identifier")] LoginAccount loginAccount)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(loginAccount);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(loginAccount);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: LoginAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var loginAccount = await _context.LoginAccounts.FindAsync(id);
                if (loginAccount == null)
                {
                    return NotFound();
                }
                return View(loginAccount);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        // POST: LoginAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Username,Password,IsAdmin,Identifier")] LoginAccount loginAccount)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id != loginAccount.Identifier)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(loginAccount);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LoginAccountExists(loginAccount.Identifier))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(loginAccount);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }           
        }

        // GET: LoginAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var loginAccount = await _context.LoginAccounts
                    .FirstOrDefaultAsync(m => m.Identifier == id);
                if (loginAccount == null)
                {
                    return NotFound();
                }

                return View(loginAccount);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: LoginAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                var loginAccount = await _context.LoginAccounts.FindAsync(id);
                _context.LoginAccounts.Remove(loginAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool LoginAccountExists(int id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                return _context.LoginAccounts.Any(e => e.Identifier == id);
            }
            else
            {
                return false;
            }

        }
    }
}
