// ==========================================================================================================================================
//
// Source for "Selenium with C# for Beginners (.NET 8, C# 12)": https://www.udemy.com/course/selenium-dotnet-basics/
//
// ==========================================================================================================================================
using DotnetSelenium.Driver;
using DotnetSelenium.Pages;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace DotnetSelenium.Tests
{
    [TestFixture(Author = "JAllsup", Description = "SeleniumGridTest. Passing data via TestFixture attribute via Default Constructor.")]
    [TestFixture("admin", "password", DriverType.Firefox)]

    public class SeleniumGridTest
    {
        // Private member fields begin with an '_'.
        private IWebDriver _driver;
        private readonly string userName;
        private readonly string password;
        private readonly DriverType driverType;

        // Default Constructor.
        public SeleniumGridTest(string userName, string password, DriverType driverType)
        {
            this.userName = userName;
            this.password = password;
            this.driverType = driverType;
        }

        [SetUp]
        public void SetUp()
        {
            //_driver = GetDriverType(driverType); 

            // Instead of calling GetDriverType, use the below code to communicate with Selenium Grid:
            _driver = new RemoteWebDriver(new Uri("http://localhost:4444"), new EdgeOptions());

            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("Smoke")]
        public void TestWithPOM()
        {

            // Page Object Model (POM) Initialization.
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLogin();
            loginPage.Login(userName, password);
        }

        [Test]
        [TestCase("Chrome", "30")] // Testcase data.
        public void TestBrowserVersion(string browser, string version)
        {
            Console.WriteLine($"The browser is {browser} with version {version}");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
