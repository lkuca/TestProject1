using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace test.tests
{
    public class UnitTest1 : IDisposable
    {
        private IWebDriver driver;
        private readonly string baseUrl = "https://localhost:44331/";

        [SetUp]
        public void Setup()
        {
            try
            {
                var driverService = ChromeDriverService.CreateDefaultService(@"C:\Users\opilane\source\repos\test\test\drivers");
                driverService.HideCommandPromptWindow = true; // Hides the command prompt window
                var options = new ChromeOptions();
                driver = new ChromeDriver(driverService, options);

                // Maximize the browser window
                driver.Manage().Window.Maximize();

                // Set implicit wait for element finding
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Driver initialization failed: {ex.Message}");
            }
        }

        [TearDown]
        public void Teardown()
        {
            Dispose();
        }

        public void Dispose()
        {
            // Dispose the driver properly if initialized
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        [Test]
        public void TestRegistration()
        {
            try
            {
                var fullUrl = $"{baseUrl}Account/Register";
                Console.WriteLine($"Navigating to: {fullUrl}");

                driver.Navigate().GoToUrl(fullUrl);

                driver.FindElement(By.Id("FirstName")).SendKeys("John");
                driver.FindElement(By.Id("LastName")).SendKeys("Doe");
                driver.FindElement(By.Id("Gender")).SendKeys("Male");
                driver.FindElement(By.Id("Email")).SendKeys("johndoe@example.com");
                driver.FindElement(By.Id("Password")).SendKeys("password123");
                


                IWebElement submitButton = driver.FindElement(By.Id("register-button"));
                submitButton.Click();

                
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Element not found: {ex.Message}");
            }
            catch (WebDriverException ex)
            {
                Assert.Fail($"WebDriver error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected error: {ex.Message}");
            }
        }
    }
}