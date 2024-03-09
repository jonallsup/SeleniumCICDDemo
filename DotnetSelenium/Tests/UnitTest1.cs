// ==========================================================================================================================================
//
// Source for "Selenium with C# for Beginners (.NET 8, C# 12)": https://www.udemy.com/course/selenium-dotnet-basics/
//
// ==========================================================================================================================================

using DotnetSelenium.Pages;
using OpenQA.Selenium.Support.UI;

namespace DotnetSelenium.Tests
{
    public class Tests
    {
        private IWebDriver? driver; // '?' declares the field as "nullable".

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IWebElement searchTextBox;

            // Sudo code for setting up Selenium:
            // 1.  Create a new instance of Selenium WebDriver.
            driver = new ChromeDriver();

            // 2.  Navigate to the Url.
            driver.Navigate().GoToUrl("https://www.Google.com");

            // 2a. Maximize the browser window.
            driver.Manage().Window.Maximize();

            // 3.  Find the element.
            searchTextBox = driver.FindElement(By.Name("q"));

            // 4.  Type in the element.
            searchTextBox.SendKeys("Selenium");

            // 5.  Click on the element.
            searchTextBox.SendKeys(Keys.Return);
        }

        [Test]
        public void EaWebsiteTest()
        {
            IWebElement loginLink;
            //IWebElement userNameTextBox;
            //IWebElement passwordTextBox;
            IWebElement loginButton;
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            driver.Manage().Window.Maximize();

            // Find Login link.
            loginLink = driver.FindElement(By.Id("loginLink"));

            // Click Login link.
            loginLink.Click();

            // Explicit Wait with Polling.
            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200),
                Message = "Textbox UserName does not appear during that timeframe."
            };

            var userNameTextBox = driverWait.Until(delta =>
            {
                var element = driver.FindElement(By.Name("UserName"));

                // If element is not Null AND it is displayed, return tr
                if ((element != null && element.Displayed))
                {

                    // If element is not Null AND it is displayed, return tr
                    return (IWebElement?)element;
                }
                else
                {

                    // If element is not Null AND it is displayed, return tr
                    return (IWebElement?)null;
                }
            });


            //// Find UserName text box.
            //userNameTextBox = driver.FindElement(By.Name("UserName"));

            // Enter username text into UserName text box.
            userNameTextBox.SendKeys("admin");

            // Find Password text box.
            var passwordTextBox = driver.FindElement(By.Id("Password"));

            // Enter password text into Password text box.
            passwordTextBox.SendKeys("password");

            // Identify button using Class Name:
            //loginButton = driver.FindElement(By.ClassName("btn")); // Leave comment - example of using Class Name.

            // Identify button using CssSelector:
            loginButton = driver.FindElement(By.CssSelector(".btn"));

            // *** Note: Because this button is of type "submit" because it's under the form tag.
            loginButton.Submit();
        }


        [Test]
        public void TestWithPOM()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            driver.Manage().Window.Maximize();

            // Page Object Model (POM) Initialization.
            LoginPage loginPage = new LoginPage(driver);

            loginPage.ClickLogin();
            loginPage.Login("admin", "password");
        }

        [Test]
        public void EaWebsiteTestReduceSizeCode()
        {

            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            driver.Manage().Window.Maximize();

            // Page Object Model (POM) Initialization.
            LoginPage loginPage = new LoginPage(driver);

            loginPage.ClickLogin();
            loginPage.Login("admin", "password");
        }

        //[Test]
        //public void WorkingWithAdvancedControls()
        //{
        //    driver = new ChromeDriver();
        //    driver.Navigate().GoToUrl("file:///testpage.html");
        //    driver.Manage().Window.Maximize();

        //    SeleniumCustomMethods.SelectDropdownByText(driver, By.Id("dropdown"), "Option3");
        //    SeleniumCustomMethods.MultiSelectElementsByValue(driver, By.Id("multiselect"), ["multi1", "multi2"]);
        //    var getSelectedOptions = SeleniumCustomMethods.GetSelectedElements(driver, By.Id("multiselect"));

        //    //foreach (var selectedOption in getSelectedOptions)
        //    //{
        //    //    Console.WriteLine(selectedOption);
        //    //}
        //    getSelectedOptions.ForEach(Console.WriteLine);
        //}

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
