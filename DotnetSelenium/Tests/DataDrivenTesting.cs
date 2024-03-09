using DotnetSelenium.Models;
using DotnetSelenium.Pages;
using FluentAssertions;
using System.Text.Json;

namespace DotnetSelenium.Tests
{
    public class DataDrivenTesting
    {

        private IWebDriver _driver;


        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(LoginWithJsonDataSource))]
        public void TestWithPOM(LoginModel loginModel)
        {
            // *** ARRANGE ***
            LoginPage loginPage = new LoginPage(_driver); // Page Object Model (POM) Initialization.

            // *** ACT ***
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);

            // *** ASSERT ***
            var loggedIn = loginPage.IsLoggedIn();
            Assert.IsTrue(loggedIn.driverTitleHomePage && loggedIn.manageUsers);
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(LoginWithJsonDataSource))]
        public void TestWithPOMwithFluentAssertion(LoginModel loginModel)
        {
            // *** ARRANGE ***
            LoginPage loginPage = new LoginPage(_driver); // Page Object Model (POM) Initialization.

            // *** ACT ***
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);

            // *** ASSERT ***
            var loggedIn = loginPage.IsLoggedIn();
            loggedIn.driverTitleHomePage.Should().BeTrue();
            loggedIn.manageUsers.Should().BeTrue();
        }

        // TODO: TestWithPOMwithJsonData() is failing. Repeat Section 5 to debug failure.
        //       Section 5.19 ~ 13:00 min ts.
        [Test]
        [Category("ddt")]
        public void TestWithPOMwithJsonData()
        {
            // Location of the bin directory - "AppDomain.CurrentDomain.BaseDirectory".
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Login.json");
            var jsonString = File.ReadAllText(jsonFilePath);

            // Deserialize the text to the specific type - from json string to LoginModel.
            var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);

            // Page Object Model (POM) Initialization.
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);
        }

        public static IEnumerable<LoginModel> Login()
        {
            yield return new LoginModel()
            {
                // Valid data.
                UserName = "admin",
                Password = "password"
            };

            yield return new LoginModel()
            {
                // Invalid username.
                UserName = "admin1",
                Password = "password"
            };

            yield return new LoginModel()
            {
                // Invalid password.
                UserName = "admin",
                Password = "password1"
            };
        }

        public static IEnumerable<LoginModel> LoginWithJsonDataSource()
        {
            // Location of the bin directory - "AppDomain.CurrentDomain.BaseDirectory".
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Login.json");
            var jsonString = File.ReadAllText(jsonFilePath);

            // Deserialize the text to the specific type - convert json string to LoginModel.
            var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);

            foreach (var loginData in loginModel)
            {
                yield return loginData;  // Returns "loginData" as a Json Data Source.
            }
        }

        private void ReadJsonFile()
        {
            // Location of the bin directory - "AppDomain.CurrentDomain.BaseDirectory".
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login.json");
            var jsonString = File.ReadAllText(jsonFilePath);

            // Deserialize the text to the specific type - from json string to LoginModel.
            var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Console.WriteLine($"UserName: {loginModel.UserName} Passsword: {loginModel.Password}");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
