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
    public class TransactionTablesController : Controller
    {
        private readonly DhlDatabaseContext _context;

        public TransactionTablesController(DhlDatabaseContext context)
        {
            _context = context;
        }

        // GET: TransactionTables
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                var dhlDatabaseContext = _context.TransactionTables.Include(t => t.FromAccountNavigation);
                return View(await dhlDatabaseContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: TransactionTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var transactionTable = await _context.TransactionTables
                    .Include(t => t.FromAccountNavigation)
                    .FirstOrDefaultAsync(m => m.TransactionNumber == id);
                if (transactionTable == null)
                {
                    return NotFound();
                }

                return View(transactionTable);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }          
        }

        // GET: TransactionTables/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                ViewData["FromAccount"] = new SelectList(_context.AccountDetails, "AccountNumber", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: TransactionTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionNumber,ToAccount,FromAccount,LendAmount,PaybackAmount")] TransactionTable transactionTable)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (ModelState.IsValid)
                {
                    _context.Add(transactionTable);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["FromAccount"] = new SelectList(_context.AccountDetails, "AccountNumber", "Name", transactionTable.FromAccount);
                return View(transactionTable);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
     
        }

        // GET: TransactionTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var transactionTable = await _context.TransactionTables.FindAsync(id);
                if (transactionTable == null)
                {
                    return NotFound();
                }
                ViewData["FromAccount"] = new SelectList(_context.AccountDetails, "AccountNumber", "Name", transactionTable.FromAccount);
                return View(transactionTable);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // POST: TransactionTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionNumber,ToAccount,FromAccount,LendAmount,PaybackAmount")] TransactionTable transactionTable)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                if (id != transactionTable.TransactionNumber)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(transactionTable);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionTableExists(transactionTable.TransactionNumber))
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
                ViewData["FromAccount"] = new SelectList(_context.AccountDetails, "AccountNumber", "Name", transactionTable.FromAccount);
                return View(transactionTable);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        // GET: TransactionTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {

                var transactionTable = await _context.TransactionTables
                    .Include(t => t.FromAccountNavigation)
                    .FirstOrDefaultAsync(m => m.TransactionNumber == id);
                if (transactionTable == null)
                {
                    return NotFound();
                }

                return View(transactionTable);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

        }

        // POST: TransactionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                var transactionTable = await _context.TransactionTables.FindAsync(id);
                _context.TransactionTables.Remove(transactionTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        private bool TransactionTableExists(int id)
        {
            if (HttpContext.Session.GetString("Identifier") != null && HttpContext.Session.GetString("IsAdmin") == "True")
            {
                return _context.TransactionTables.Any(e => e.TransactionNumber == id);
            }
            else
            {
                return false;
            }

        }
    }
}
