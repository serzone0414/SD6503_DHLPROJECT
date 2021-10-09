using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;


using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1
{
    [TestClass]
    public class AutomatedUi
    {



        private readonly IWebDriver _driver;
        public AutomatedUi()
        {
            _driver = new ChromeDriver();

        }


        [TestMethod]

        public void launchbrowser()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            Assert.AreEqual("Home Page - SD6503_DHLPROJECT", _driver.Title);
        }

        [TestMethod]

        public void LoginTestValid()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

        }

        [TestMethod]

        public void LoginTestInvalidUsername()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("hamishssss");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

        }

        [TestMethod]

        public void LoginTestInvalidPassWord()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("12345");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

        }


        [TestMethod]

        public void LoginTestNoInput()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

        }


        [TestMethod]
        public void RegisterTest()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Register");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("name")).SendKeys("AutoTestUser");
            _driver.FindElement(By.Id("name")).SendKeys(Keys.Enter);

        }

        [TestMethod]
        public void RegisterTestUserConfirm()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
        }

        [TestMethod]
        public void RegisterTestNoData()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Register");
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);


        }

        [TestMethod]
        public void RegisterTestPreExisingUser()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Register");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);


        }

        [TestMethod]
        public void RegisterTestInvalidUserName()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Register");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("3unct!onT3st");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.FindElement(By.Id("name")).SendKeys("3unct!onT3st");
            _driver.FindElement(By.Id("name")).SendKeys(Keys.Enter);

        }

        [TestMethod]

        public void AddFundsTest()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/AddBalance");
            _driver.FindElement(By.Id("balance")).SendKeys("1000");
            _driver.FindElement(By.Id("balance")).SendKeys(Keys.Enter);

        }

        public void AddFundsTestInvalidNumber()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/AddBalance");
            _driver.FindElement(By.Id("balance")).SendKeys("100000");
            _driver.FindElement(By.Id("balance")).SendKeys(Keys.Enter);

        }

        public void AddFundsTestInvalidString()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/AddBalance");
            _driver.FindElement(By.Id("balance")).SendKeys("Infinite");
            _driver.FindElement(By.Id("balance")).SendKeys(Keys.Enter);

        }

        [TestMethod]

        public void LendFundsTestValid()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Lend");
            var toAccount = _driver.FindElement(By.Id("ToAccount"));
            var toAccountSelect = new SelectElement(toAccount);
            toAccountSelect.SelectByText("Dawoo Jeong");
            var lendAmount = _driver.FindElement(By.Id("LendAmount"));
            lendAmount.SendKeys("10");
            lendAmount.SendKeys(Keys.Enter);
        }

        public void LendFundsTestInvalidNumberTooLarge()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Lend");
            var toAccount = _driver.FindElement(By.Id("ToAccount"));
            var toAccountSelect = new SelectElement(toAccount);
            toAccountSelect.SelectByText("Dawoo Jeong");
            var lendAmount = _driver.FindElement(By.Id("LendAmount"));
            lendAmount.SendKeys("1000000");
            lendAmount.SendKeys(Keys.Enter);

        }

        public void LendFundsTestInvalidString()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Lend");
            var toAccount = _driver.FindElement(By.Id("ToAccount"));
            var toAccountSelect = new SelectElement(toAccount);
            toAccountSelect.SelectByText("Dawoo Jeong");
            var lendAmount = _driver.FindElement(By.Id("LendAmount"));
            lendAmount.SendKeys("Fifty");
            lendAmount.SendKeys(Keys.Enter);

        }


        [TestMethod]

        public void PayBackFundsTestValid()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("Hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/PayBack");
            var toAccountPayback = _driver.FindElement(By.Id("ToAccount"));
            var toAccountPaybackSelect = new SelectElement(toAccountPayback);
            toAccountPaybackSelect.SelectByText("Janetta Canary");
            _driver.FindElement(By.Id("PaybackAmount")).SendKeys("10");
            _driver.FindElement(By.Id("PaybackAmount")).SendKeys(Keys.Enter);
        }
        [TestMethod]
        public void PayBackFundsTestInvalidInputToLarge()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("Hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/PayBack");
            var toAccountPayback = _driver.FindElement(By.Id("ToAccount"));
            var toAccountPaybackSelect = new SelectElement(toAccountPayback);
            toAccountPaybackSelect.SelectByText("Janetta Canary");
            _driver.FindElement(By.Id("PaybackAmount")).SendKeys("100000000");
            _driver.FindElement(By.Id("PaybackAmount")).SendKeys(Keys.Enter);
        }
        [TestMethod]
        public void PayBackFundsTestInvalidString()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("Hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/PayBack");
            var toAccountPayback = _driver.FindElement(By.Id("ToAccount"));
            var toAccountPaybackSelect = new SelectElement(toAccountPayback);
            toAccountPaybackSelect.SelectByText("Janetta Canary");
            _driver.FindElement(By.Id("PaybackAmount")).SendKeys("Ten");
            _driver.FindElement(By.Id("PaybackAmount")).SendKeys(Keys.Enter);


        }

        [TestMethod]

        public void SearchTestWithData()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);

            _driver.FindElement(By.Name("Username")).SendKeys("hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            _driver.FindElement(By.Id("search")).SendKeys("m");
            _driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);


        }

        public void SearchTestReturnToFullList()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);

            _driver.FindElement(By.Name("Username")).SendKeys("hamish");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            _driver.FindElement(By.Id("search")).SendKeys("m");
            _driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            _driver.FindElement(By.Id("search")).SendKeys(Keys.Control + "a");
            _driver.FindElement(By.Id("search")).SendKeys(Keys.Delete);
            _driver.FindElement(By.Id("search")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);


        }










    }
}
