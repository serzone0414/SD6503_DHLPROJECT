using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SD6503_DHLPROJECT.Models;
using SD6503_DHLPROJECT.Controllers;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

                LoginAccount loginAccount = _context.LoginAccounts.Where(u => u.Username == user.Username).FirstOrDefault();
                AccountDetail accountDetail = new AccountDetail();
                accountDetail.Identifier = loginAccount.Identifier;
                accountDetail.Name = "New User";
                accountDetail.Balance = 0;
                _context.AccountDetails.Add(accountDetail);
                _context.SaveChanges();

                ModelState.Clear();
                Login(user);
                return RedirectToAction("LoggedIn");
            }
            else 
            {
                ModelState.AddModelError("", "Username and password are required");
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


        public ActionResult LoggedIn(string searchBy, string search)
        {
            if (HttpContext.Session.GetString("Identifier") != null)
            {
                //Get Account details
                var accountDetail = _context.AccountDetails.Where(u => u.Identifier == Int32.Parse(HttpContext.Session.GetString("Identifier"))).FirstOrDefault();
                HttpContext.Session.SetString("Username", accountDetail.Name);
                HttpContext.Session.SetString("Balance", accountDetail.Balance.ToString());
                HttpContext.Session.SetString("AccountNumber", accountDetail.AccountNumber.ToString());
                if (accountDetail.Name == "New User")
                {
                    return RedirectToAction("NewUser");
                }
                ViewBag.Username = HttpContext.Session.GetString("Username");
                ViewBag.Balance = HttpContext.Session.GetString("Balance");
                ViewBag.Identifier = accountDetail.AccountNumber.ToString();

                List<AccountDetail> accountsList = new List<AccountDetail>();
                accountsList = _context.AccountDetails.ToList();
                ViewBag.accountsList = accountsList;

                //Get Transaction details
                if (search == null)
                {
                    IList<TransactionTable> relatedTransactionList = new List<TransactionTable>();
                    return View(_context.TransactionTables.Where(u => u.FromAccount == accountDetail.AccountNumber || u.ToAccount == accountDetail.AccountNumber).ToList());
                }
                else
                {
                    if (searchBy == "Name")
                    {
                        var searchingAccount = _context.AccountDetails.Where(u => u.Name.StartsWith(search)).FirstOrDefault();
                        if (searchingAccount != null)
                        {
                            int searchingAccountNumber = searchingAccount.AccountNumber;
                            IList<TransactionTable> relatedTransactionList = new List<TransactionTable>();
                            var returningList = _context.TransactionTables.Where(u => u.FromAccount == accountDetail.AccountNumber || u.ToAccount == accountDetail.AccountNumber).Where(u => u.FromAccount == searchingAccountNumber || u.ToAccount == searchingAccountNumber).ToList();
                            return View(returningList);
                        }
                        else
                            return View(_context.TransactionTables.Where(u => u.FromAccount == 000000).ToList());
                    }
                    else 
                    {
                        int searchingAccountNumber;
                        bool isNumber = int.TryParse(search, out searchingAccountNumber);
                        if (isNumber)
                        {
                            IList<TransactionTable> relatedTransactionList = new List<TransactionTable>();
                            var returningList = _context.TransactionTables.Where(u => u.FromAccount == accountDetail.AccountNumber || u.ToAccount == accountDetail.AccountNumber).Where(u => u.FromAccount == searchingAccountNumber || u.ToAccount == searchingAccountNumber).ToList();
                            return View(returningList);
                        }
                        else
                        {
                            return View(_context.TransactionTables.Where(u => u.FromAccount == 000000).ToList());
                        }

                    }
                } 
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


        public IActionResult Lend()
        {
            if (HttpContext.Session.GetString("AccountNumber") != null)
            {
                var accountDetail = _context.AccountDetails.Where(u => u.Identifier == Int32.Parse(HttpContext.Session.GetString("Identifier"))).FirstOrDefault();
                ViewData["FromAccount"] = new SelectList(_context.AccountDetails.Where(u => u.AccountNumber == accountDetail.AccountNumber), "AccountNumber", "Name");
                return View();
            }
            else
                return RedirectToAction("Index");

        }

        // POST: Home/Lend

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Lend([Bind("TransactionNumber,ToAccount,FromAccount,LendAmount,0")] TransactionTable transactionTable)
        {
            if (ModelState.IsValid)
            {
                var fromAccount = _context.Set<AccountDetail>().SingleOrDefault(o => o.AccountNumber == transactionTable.FromAccount);
                fromAccount.Balance = fromAccount.Balance - transactionTable.LendAmount;
                var toAccount = _context.Set<AccountDetail>().SingleOrDefault(o => o.AccountNumber == transactionTable.ToAccount);
                toAccount.Balance = toAccount.Balance + transactionTable.LendAmount;

                var existTransaction = _context.Set<TransactionTable>().Where(o => o.FromAccount == transactionTable.FromAccount && o.ToAccount == transactionTable.ToAccount).FirstOrDefault();
                //var existTransaction = _context.Set<TransactionTable>().SingleOrDefault(o => o.FromAccount == transactionTable.FromAccount && o.ToAccount == transactionTable.ToAccount);
                if (existTransaction != null)
                {
                    existTransaction.LendAmount = existTransaction.LendAmount + transactionTable.LendAmount;
                }
                else
                {
                    _context.Add(transactionTable);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("LoggedIn");
            }
            ViewData["FromAccount"] = new SelectList(_context.AccountDetails, "AccountNumber", "Name", transactionTable.FromAccount);
            return View(transactionTable);
        }

        public IActionResult PayBack()
        {
            if (HttpContext.Session.GetString("AccountNumber") != null)
            {
                var fromAccountDetail = _context.AccountDetails.Where(u => u.Identifier == Int32.Parse(HttpContext.Session.GetString("Identifier"))).FirstOrDefault();
                ViewData["FromAccount"] = new SelectList(_context.AccountDetails.Where(u => u.AccountNumber == fromAccountDetail.AccountNumber), "AccountNumber", "Name");


                List<TransactionTable> ToAccountTransactionList = _context.TransactionTables.Where(u => u.ToAccount == fromAccountDetail.AccountNumber ).ToList();
                List<AccountDetail> relatedAccountList = new List<AccountDetail>();
                foreach (var transaction in ToAccountTransactionList)
                {
                    var realtedAccountNumber = _context.AccountDetails.Where(u => u.AccountNumber == transaction.FromAccount).FirstOrDefault();
                    if (realtedAccountNumber != null)
                    {
                        relatedAccountList.Add(realtedAccountNumber);
                    }
                }
                ViewData["ToAccount"] = new SelectList(relatedAccountList, "AccountNumber", "Name");


                return View();
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Home/PayBack
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayBack([Bind("TransactionNumber,ToAccount,FromAccount,0,PaybackAmount")] TransactionTable transactionTable)
        {
            if (ModelState.IsValid)
            {
                var fromAccount = _context.Set<AccountDetail>().SingleOrDefault(o => o.AccountNumber == transactionTable.FromAccount);
                fromAccount.Balance = fromAccount.Balance - transactionTable.PaybackAmount;
                var toAccount = _context.Set<AccountDetail>().SingleOrDefault(o => o.AccountNumber == transactionTable.ToAccount);
                toAccount.Balance = toAccount.Balance + transactionTable.PaybackAmount;

                var existTransaction = _context.Set<TransactionTable>().Where(o => o.ToAccount == transactionTable.FromAccount && o.FromAccount == transactionTable.ToAccount).FirstOrDefault();
                //var existTransaction = _context.Set<TransactionTable>().SingleOrDefault(o => o.FromAccount == transactionTable.FromAccount && o.ToAccount == transactionTable.ToAccount);
                if (existTransaction != null)
                {
                    existTransaction.PaybackAmount = existTransaction.PaybackAmount + transactionTable.PaybackAmount;
                }
                else
                {
                    _context.Add(transactionTable);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("LoggedIn");
            }
            ViewData["FromAccount"] = new SelectList(_context.AccountDetails, "AccountNumber", "Name", transactionTable.FromAccount);
            return View(transactionTable);
        }

        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewUser(string name)
        {
            AccountDetail accountDetail = _context.AccountDetails.Where(u => u.AccountNumber == Int32.Parse(HttpContext.Session.GetString("AccountNumber"))).FirstOrDefault();
            accountDetail.Name = name;
            _context.Update(accountDetail);
            _context.SaveChanges();

            return RedirectToAction("LoggedIn");
        }

        public IActionResult AddBalance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBalance(string balance)
        {
            if (balance != null)
            {
                Double addedBalance;
                bool isValidNumber = Double.TryParse(balance,out addedBalance);
       
                if (isValidNumber)
                {
                    if (addedBalance >= 0 && addedBalance < 99999)
                    {
                        AccountDetail accountDetail = _context.AccountDetails.Where(u => u.AccountNumber == Int32.Parse(HttpContext.Session.GetString("AccountNumber"))).FirstOrDefault();
                        accountDetail.Balance = accountDetail.Balance + addedBalance;
                        _context.Update(accountDetail);
                        _context.SaveChanges();
                        return RedirectToAction("LoggedIn");

                    }
                    else
                        return View();
                }

            }
            return View();
        }
    }
}
