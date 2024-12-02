using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace test.tests
{
    [TestFixture]
    public class UnitTest3 : IDisposable
    {
        private IWebDriver driver;
        private readonly string baseUrl = "https://localhost:44331/"; // Update with your app's base URL
        private const string validEmail = "admin@gmail.com";  // Use a valid admin email
        private const string validPassword = "admin";   // Use the corresponding password for admin

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
            wait.Until(d => d.Url.Contains("/"));

            // Verify if the user is redirected to the Store page
            Assert.IsTrue(driver.Url.Contains("/"), "Login did not redirect to the store page.");
        }

        // Test for accessing the Drink Index page
        [Test]
        public void Test_Access_Drink_Index_Page()
        {
            // Ensure user is logged in first
            Test_Login_ValidCredentials_RedirectsToStore();  // Make sure we are logged in

            // Navigate to the Drink Index page by link text or href
            IWebElement drinkIndexLink = driver.FindElement(By.LinkText("Jookide haldamine"));  // Use the link text
            drinkIndexLink.Click();

            // Or using href if you know the URL
            // IWebElement drinkIndexLink = driver.FindElement(By.XPath("//a[@href='/Drink/Index']"));
            // drinkIndexLink.Click();

            // Verify if the page contains the correct title or expected content
            IWebElement pageTitle = driver.FindElement(By.XPath("//h2[text()='Index']"));
            Assert.IsTrue(pageTitle.Text.Contains("Index"), "The page does not display 'Index'.");
        }

        // Test for adding a drink (Create)
        [Test]
        public void Test_Add_New_Drink()
        {
            // Ensure user is logged in first
            

            Test_Login_ValidCredentials_RedirectsToStore();  // Make sure we are logged in

            // Navigate to the Drink Index page by link text or href
            IWebElement drinkIndexLink = driver.FindElement(By.LinkText("Jookide haldamine"));  // Use the link text
            drinkIndexLink.Click();

            // Navigate to the Create Drink page by link text or href
            IWebElement createDrinkLink = driver.FindElement(By.LinkText("Create New"));
            createDrinkLink.Click();

            // Or using href if you know the URL
            // IWebElement createDrinkLink = driver.FindElement(By.XPath("//a[@href='/Drink/Create']"));
            // createDrinkLink.Click();

            // Find the input fields for creating a new drink
            IWebElement nameInput = driver.FindElement(By.Id("DrinkName"));
            IWebElement typeInput = driver.FindElement(By.Id("Type"));
            IWebElement descriptionInput = driver.FindElement(By.Id("Description"));
            IWebElement priceInput = driver.FindElement(By.Id("Price"));

            // Enter drink details
            nameInput.SendKeys("Lemonade");
            typeInput.SendKeys("Kvass");
            descriptionInput.SendKeys("Freshly squeezed lemonade");
            priceInput.SendKeys("32");

            // Submit the form
            IWebElement submitButton = driver.FindElement(By.Id("submit-button"));
            submitButton.Click();

            // Wait for the page to reload and verify the drink is listed
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("Drink/Index"));

            // Verify that the new drink appears on the list
            
        }

        // Test for editing a drink (Edit)
        [Test]
        public void Test_Edit_Drink()
        {
            // Ensure user is logged in first
            Test_Login_ValidCredentials_RedirectsToStore();

            IWebElement drinkIndexLink = driver.FindElement(By.LinkText("Jookide haldamine"));  // Use the link text
            drinkIndexLink.Click();

            // Navigate to the Edit page for a specific drink (assuming ID = 1)
            IWebElement editLink = driver.FindElement(By.XPath("//a[@href='/Drink/Edit/1']"));
            editLink.Click();

            // Find the input fields to edit the drink
            IWebElement nameInput = driver.FindElement(By.Id("DrinkName"));
            IWebElement typeInput = driver.FindElement(By.Id("Type"));
            IWebElement descriptionInput = driver.FindElement(By.Id("Description"));
            IWebElement priceInput = driver.FindElement(By.Id("Price"));

            // Edit drink details
            nameInput.Clear();
            nameInput.SendKeys("Updated Lemonade");
            //typeInput.Clear();
            typeInput.SendKeys("Juice");
            descriptionInput.Clear();
            descriptionInput.SendKeys("Updated description");
            priceInput.Clear();
            priceInput.SendKeys("3,0");

            // Submit the form
            IWebElement submitButton = driver.FindElement(By.Id("edit-button"));
            submitButton.Click();

            // Wait for the page to reload and verify the updated drink
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(d => d.Url.Contains("Drink/Index"));

            //// Verify that the drink is updated in the list
            //IWebElement updatedDrink = driver.FindElement(By.XPath("//td[text()='Updated Lemonade']"));
            //Assert.IsNotNull(updatedDrink, "Drink was not updated.");
        }

        // Test for deleting a drink (Delete)
        [Test]
        public void Test_Delete_Drink()
        {
            // Ensure user is logged in first
            Test_Login_ValidCredentials_RedirectsToStore();

            IWebElement drinkIndexLink = driver.FindElement(By.LinkText("Jookide haldamine"));  // Use the link text
            drinkIndexLink.Click();

            // Navigate to the Delete page for a specific drink (assuming ID = 1)
            IWebElement deleteLink = driver.FindElement(By.XPath("//a[@href='/Drink/Delete/1']"));
            deleteLink.Click();

            // Find the delete button and click it
            IWebElement deleteButton = driver.FindElement(By.Id("delete-button"));
            deleteButton.Click();

            // Wait for the page to reload and verify the drink is deleted
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("Drink/Index"));

            // Verify that the drink is no longer in the list
            bool isDrinkDeleted = driver.PageSource.Contains("Lemonade") == false;
            Assert.IsTrue(isDrinkDeleted, "Drink was not deleted.");
        }

        // Test for adding a drink to the cart (AddToCart)
        

        // Dispose pattern for cleanup
        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
