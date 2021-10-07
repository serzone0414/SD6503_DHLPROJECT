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
            _driver.Navigate().GoToUrl("https://localhost:5001/");
            Assert.AreEqual("Index - SD6503_DHLPROJECT", _driver.Title);
        }
}



}
