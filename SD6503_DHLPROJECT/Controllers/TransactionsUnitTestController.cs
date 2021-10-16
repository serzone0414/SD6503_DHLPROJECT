using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD6503_DHLPROJECT.Models;

namespace SD6503_DHLPROJECT.Controllers
{
    public class TransactionsUnitTestController : Controller
    {
        public List<TransactionTable> GetTransactionList()
        {
            return new List<TransactionTable>
            {
                new TransactionTable
                {
                    TransactionNumber = 67890,
                    ToAccount = 998877,
                    FromAccount = 887766,
                    LendAmount = 35,
                    PaybackAmount = 7,
                },
            };
        }

        public IActionResult Index()
        {
            var transactionDetails = from t in GetTransactionList() select t;
            return View(transactionDetails);
        }

        public IActionResult Transaction()
        {
            var transactionDetails = from t in GetTransactionList()
                                     orderby t.TransactionNumber
                                     select t;
            return View(transactionDetails);
        }

        public ActionResult Details(int Id)
        {
            return View("Details");
        }
    }
}
