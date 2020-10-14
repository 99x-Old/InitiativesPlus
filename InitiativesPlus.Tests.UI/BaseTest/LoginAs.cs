using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace InitiativesPlus.Tests.UI.BaseTest
{
    public class LoginAs
    {
        private readonly string userNameXpath = "/html/body/app-root/app-home/html/body/main/div/div/div/div[2]/div/div[2]/form/div[1]/input";
        private readonly string passwordXpath = "/html/body/app-root/app-home/html/body/main/div/div/div/div[2]/div/div[2]/form/div[2]/input";

        public void SuperAdmin(IWebDriver driver)
        {
            driver.Navigate()
                .GoToUrl("http://initiatives-plus-spa.azurewebsites.net");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath(userNameXpath))
                .SendKeys("superadmin");
            driver.FindElement(By.XPath(passwordXpath))
                .SendKeys("password");
            driver.FindElement(By.Id("login"))
                .Click();

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("/dashboard"));
        }        
        
        public void User(IWebDriver driver)
        {
            driver.Navigate()
                .GoToUrl("http://initiatives-plus-spa.azurewebsites.net");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath(userNameXpath))
                .SendKeys("user");
            driver.FindElement(By.XPath(passwordXpath))
                .SendKeys("password");
            driver.FindElement(By.Id("login"))
                .Click();

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("/dashboard"));
        }
        
        public void InitiativeLead(IWebDriver driver)
        {
            driver.Navigate()
                .GoToUrl("http://initiatives-plus-spa.azurewebsites.net");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath(userNameXpath))
                .SendKeys("initiativelead");
            driver.FindElement(By.XPath(passwordXpath))
                .SendKeys("password");
            driver.FindElement(By.Id("login"))
                .Click();

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("/dashboard"));
        }
        
        public void Evaluator(IWebDriver driver)
        {
            driver.Navigate()
                .GoToUrl("http://initiatives-plus-spa.azurewebsites.net");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath(userNameXpath))
                .SendKeys("evaluator");
            driver.FindElement(By.XPath(passwordXpath))
                .SendKeys("password");
            driver.FindElement(By.Id("login"))
                .Click();

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("/dashboard"));
        }
    }
}
