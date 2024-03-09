using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotnetSelenium.Driver;
using DotnetSelenium.Pages;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DotnetSelenium.Tests
{
    [TestFixture(Author = "JAllsup", Description = "NUnit Tests Demo. Passing data via TestFixture attribute via Default Constructor.")]
    [TestFixture("admin", "password", DriverType.Firefox)]

    public class NUnitTestsDemo
    {
        // Private member fields begin with an '_'.
        private IWebDriver _driver;
        private readonly string userName;
        private readonly string password;
        private readonly DriverType driverType;
        private ExtentReports _extentReports;
        private ExtentTest _extentTest;
        private ExtentTest _testNode;

        // Default Constructor.
        public NUnitTestsDemo(string userName, string password, DriverType driverType)
        {
            this.userName = userName;
            this.password = password;
            this.driverType = driverType;
        }

        [SetUp]
        public void SetUp()
        {
            SetupExtentReports();
            _driver = GetDriverType(driverType);
            _testNode = _extentTest.CreateNode("Setup and Teardown Info").Pass("Browser launched.");
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            _driver.Manage().Window.Maximize();
        }

        private IWebDriver GetDriverType(DriverType driverType)
        {
            // Evolution of Switch statements:

            // If, Else If, Else Expression:
            //if (driverType == DriverType.Chrome)
            //{
            //    _driver = new ChromeDriver();
            //}
            //else if (driverType == DriverType.Firefox)
            //{
            //    _driver = new FirefoxDriver();
            //}
            //else if (driverType == DriverType.Edge)
            //{
            //    _driver = new EdgeDriver();
            //}
            //return _driver;

            // Old Switch Expression:
            //switch (driverType)
            //{
            //    case DriverType.Chrome:
            //        _driver = new ChromeDriver();
            //        break;
            //    case DriverType.Firefox:
            //        _driver = new FirefoxDriver();
            //        break;
            //    case DriverType.Edge:
            //        _driver = new EdgeDriver();
            //        break;
            //}
            //return _driver;

            // Enhanced Switch Expression:
            return _driver = driverType switch
            {
                DriverType.Chrome => new ChromeDriver(),
                DriverType.Firefox => new FirefoxDriver(),
                DriverType.Edge => new EdgeDriver(),
                _ => _driver
            };
        }

        [Test]
        [Category("Smoke")]
        public void TestWithPOM()
        {

            // Page Object Model (POM) Initialization.
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLogin();
            _extentTest.Log(Status.Pass, "Click Login.");
            loginPage.Login(userName, password);
            _extentTest.Log(Status.Pass, "UserName and Password entered on Login.");

            var loggedIn = loginPage.IsLoggedIn();
            Assert.IsTrue(loggedIn.driverTitleHomePage && loggedIn.manageUsers);
            _extentTest.Log(Status.Pass, "Assertion Successful.");
        }

        private void SetupExtentReports()
        {
            _extentReports = new ExtentReports();
            var spark = new ExtentSparkReporter("TestReport.html"); // Not specifying a filepath, the report will be generated in the "bin" dirctory.

            _extentReports.AttachReporter(spark);
            _extentReports.AddSystemInfo("OS", "Windows 10");
            _extentReports.AddSystemInfo("Browser", driverType.ToString());
            _extentTest = _extentReports.CreateTest("Login Test with POM").Log(Status.Info, "Extent report initialized.");
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
            _testNode.Pass("Browser quit.");
            _driver.Dispose();
            _extentReports.Flush();
        }
    }
}
