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
    public class AccountDetailsController : Controller
    {
        private readonly DhlDatabaseContext _context;

        public AccountDetailsController(DhlDatabaseContext context)
        {
            _context = context;
        }

        // GET: AccountDetails
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                var dhlDatabaseContext = _context.AccountDetails.Include(a => a.IdentifierNavigation);
                return View(await dhlDatabaseContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
              
        }

        // GET: AccountDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accountDetail = await _context.AccountDetails
                    .Include(a => a.IdentifierNavigation)
                    .FirstOrDefaultAsync(m => m.AccountNumber == id);
                if (accountDetail == null)
                {
                    return NotFound();
                }

                return View(accountDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }       
        }

        // GET: AccountDetails/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                ViewData["Identifier"] = new SelectList(_context.LoginAccounts, "Identifier", "Password");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: AccountDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,Name,Balance,Identifier")] AccountDetail accountDetail)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(accountDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Identifier"] = new SelectList(_context.LoginAccounts, "Identifier", "Password", accountDetail.Identifier);
                return View(accountDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: AccountDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accountDetail = await _context.AccountDetails.FindAsync(id);
                if (accountDetail == null)
                {
                    return NotFound();
                }
                ViewData["Identifier"] = new SelectList(_context.LoginAccounts, "Identifier", "Password", accountDetail.Identifier);
                return View(accountDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
        }

        // POST: AccountDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountNumber,Name,Balance,Identifier")] AccountDetail accountDetail)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id != accountDetail.AccountNumber)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(accountDetail);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AccountDetailExists(accountDetail.AccountNumber))
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
                ViewData["Identifier"] = new SelectList(_context.LoginAccounts, "Identifier", "Password", accountDetail.Identifier);
                return View(accountDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // GET: AccountDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accountDetail = await _context.AccountDetails
                    .Include(a => a.IdentifierNavigation)
                    .FirstOrDefaultAsync(m => m.AccountNumber == id);
                if (accountDetail == null)
                {
                    return NotFound();
                }

                return View(accountDetail);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        // POST: AccountDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                var accountDetail = await _context.AccountDetails.FindAsync(id);
                _context.AccountDetails.Remove(accountDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        private bool AccountDetailExists(int id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                return _context.AccountDetails.Any(e => e.AccountNumber == id);
            }
            else
            {
                return false;
            }
     
        }

    }
}
