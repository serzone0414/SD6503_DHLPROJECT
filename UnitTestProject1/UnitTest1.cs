using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD6503_DHLPROJECT.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginAccountsUnitTest()
        {
            LoginAccountUnitTestController controller = new LoginAccountUnitTestController();
            IActionResult result = controller.Index() as IActionResult;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void AccountDetailsUnitTest()
        {
            AccountDetailUnitTestController controller = new AccountDetailUnitTestController();
            IActionResult result = controller.Index() as IActionResult;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TransactionUnitTest()
        {
            TransactionsUnitTestController controller = new TransactionsUnitTestController();
            IActionResult result = controller.Index() as IActionResult;
            Assert.IsNotNull(result);
        }
    }
}
