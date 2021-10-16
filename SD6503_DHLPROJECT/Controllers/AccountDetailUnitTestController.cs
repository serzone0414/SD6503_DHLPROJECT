using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD6503_DHLPROJECT.Models;

namespace SD6503_DHLPROJECT.Controllers
{
    public class AccountDetailUnitTestController : Controller
    {
        public List<AccountDetail> GetAccountDetailList()
        {
            return new List<AccountDetail>
            {
                new AccountDetail
                {
                    Identifier = 778899,
                    AccountNumber = 998877,
                    Name = "Bob Smith",
                    Balance = 555,
                },
                new AccountDetail
                {
                    Identifier = 667788,
                    AccountNumber = 887766,
                    Name = "Sue Grey",
                    Balance = 666,
                },
            };
        }

        public IActionResult Index()
        {
            var accountDetails = from a in GetAccountDetailList() select a;
            return View(accountDetails);
        }

        public IActionResult Account()
        {
            var accountDetails = from a in GetAccountDetailList()
                                 orderby a.Name
                                 select a;
            return View(accountDetails);
        }

        public ActionResult Details(int Id)
        {
            return View("Details");
        }
    }
}
