using System;
using InitiativesPlus.Tests.UI.BaseTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace InitiativesPlus.Tests.UI.LoginTests
{
    public class LoginAsPrimaryUsers : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string userNameXpath = "//mat-nav-list/div/div/h2";
        public LoginAsPrimaryUsers()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            _driver = new ChromeDriver();
        }
        public void Dispose()
        {
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void SuperAdminLoginTest()
        {
            new LoginAs().SuperAdmin(_driver);
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                userNameXpath)));
            Assert.Equal("superadmin", element.Text.ToLower());
        }

        [Fact]
        public void UserLoginTest()
        {
            new LoginAs().User(_driver);
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                userNameXpath)));
            Assert.Equal("user", element.Text.ToLower());
        } 
        
        [Fact]
        public void InitiativeLeadLoginTest()
        {
            new LoginAs().InitiativeLead(_driver);
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                userNameXpath)));
            Assert.Equal("initiativelead", element.Text.ToLower());
        } 
        
        [Fact]
        public void EvaluatorLoginTest()
        {
            new LoginAs().Evaluator(_driver);
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(
                userNameXpath)));
            Assert.Equal("evaluator", element.Text.ToLower());
        }
    }
}
