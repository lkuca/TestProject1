using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace test.tests
{
    [TestFixture]
    public class UnitTest2 : IDisposable
    {
        private IWebDriver driver;
        private readonly string baseUrl = "https://localhost:44331/"; // Update with your app's base URL
        private const string validEmail = "johndoe@example.com";  // Use a known valid email for testing
        private const string validPassword = "password123";        // Use the corresponding password for that email

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\test\test\drivers");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();  // Proper cleanup
        }

        // Test for valid login credentials
        [Test]
        public void Test_Login_ValidCredentials_RedirectsToStore()
        {
            // Navigate to the Login page
            driver.Navigate().GoToUrl($"{baseUrl}Account/Login");

            // Find the email and password input fields
            IWebElement emailInput = driver.FindElement(By.Id("Email"));
            IWebElement passwordInput = driver.FindElement(By.Id("Password"));

            // Enter valid credentials
            emailInput.SendKeys(validEmail);
            passwordInput.SendKeys(validPassword);

            // Find the Login button and click it
            IWebElement submitButton = driver.FindElement(By.Id("login-button"));
            submitButton.Click();

            // Use WebDriverWait instead of Thread.Sleep for better synchronization
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("Drink/Store"));

            // Verify if the user is redirected to the Store page
            Assert.IsTrue(driver.Url.Contains("Drink/Store"), "Login did not redirect to the store page.");
        }

        // Test for invalid login credentials and checking for error message
        [Test]
        public void Test_Login_InvalidCredentials_ShowsErrorMessage()
        {
            // Navigate to the Login page
            driver.Navigate().GoToUrl($"{baseUrl}Account/Login");

            // Find the email and password input fields
            IWebElement emailInput = driver.FindElement(By.Id("Email"));
            IWebElement passwordInput = driver.FindElement(By.Id("Password"));

            // Enter invalid credentials
            emailInput.SendKeys("invalid@example.com");
            passwordInput.SendKeys("wrongpassword");

            // Find the Login button and click it
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            // Use WebDriverWait to wait for the error message to appear
            
            

            // Verify if the error message is displayed
            
        }

        // Dispose pattern, if you want to implement IDisposable for better cleanup.
        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}