using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

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

        public void LendFundsValid()
        {
            _driver.Navigate().GoToUrl("http://localhost:5001/");
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Name("Username")).SendKeys("AutoTest");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.Id("Password")).SendKeys(Keys.Enter);
            _driver.Navigate().GoToUrl("http://localhost:5001/Home/Lend");
            _driver.FindElement(By.Id("ToAccount")).Click();

        }






    }
}
