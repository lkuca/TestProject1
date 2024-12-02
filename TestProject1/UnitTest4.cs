using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace test.tests
{
    [TestFixture]
    public class UnitTest4
    {
        private IWebDriver driver;
        private readonly string baseUrl = "https://localhost:44331/"; // Replace with your app's base URL
        private const string userEmail = "admin@gmail.com";  // Test user email
        private const string userPassword = "admin";  // Test user password

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\test\test\drivers");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Log in as a user before testing
            driver.Navigate().GoToUrl($"{baseUrl}Account/Login");
            IWebElement emailInput = driver.FindElement(By.Id("Email"));
            IWebElement passwordInput = driver.FindElement(By.Id("Password"));
            emailInput.SendKeys(userEmail);
            passwordInput.SendKeys(userPassword);
            driver.FindElement(By.Id("login-button")).Click();

            // Wait until redirected to the home page
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/"));
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Explicitly call Dispose to satisfy NUnit1032.
                driver = null; // Set the driver to null to prevent reuse.
            }
        }

  
        

        [Test]
        public void Test_MakePayment()
        {
            driver.Navigate().GoToUrl($"{baseUrl}Order/Cart");

            // Click the "Make Payment" button
            IWebElement makePaymentButton = driver.FindElement(By.Id("proceed-payment"));
            makePaymentButton.Click();

            // Wait for the payment process to initiate
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement paymentLinkElement = wait.Until(d => d.FindElement(By.Id("payment-link")));
            string paymentLink = paymentLinkElement.GetAttribute("href");

            Assert.IsTrue(paymentLink.StartsWith("https://"), "Payment link is invalid.");
        }

        [Test]
        public void Test_ClearCart()
        {
            driver.Navigate().GoToUrl($"{baseUrl}Order/Cart");

            // Click the "Clear Cart" button
            IWebElement clearCartButton = driver.FindElement(By.Id("asdf"));
            clearCartButton.Click();

            // Verify the cart is empty
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("Order/Cart"));
            Assert.IsFalse(driver.PageSource.Contains("Cart items"), "Cart was not cleared.");
        }
    }
}
