using DotnetSelenium.Extensions;

namespace DotnetSelenium.Pages
{
    public class LoginPage(IWebDriver driver) //Primary Constructor starting with C#12.
    {
        // ************************ Default Constructor - Prior to C#12 ************************
        //private readonly IWebDriver driver; // Private member fields begin with an '_'.
        //
        //public LoginPage(IWebDriver driver)
        //{
        //    this.driver = driver;
        //}
        // *************************************************************************************

        #region Page Locators:

        IWebElement LoginLink => driver.FindElement(By.Id("loginLink"));
        IWebElement UserNameTextBox => driver.FindElement(By.Id("UserName"));
        IWebElement PasswordTextBox => driver.FindElement(By.Id("Password"));
        IWebElement LoginButton => driver.FindElement(By.CssSelector(".btn"));
        IWebElement EmployeeDetailsLink => driver.FindElement(By.LinkText("Employee Details"));
        IWebElement ManageUsersLink => driver.FindElement(By.LinkText("Manage Users"));
        IWebElement LogoffLink => driver.FindElement(By.LinkText("Log off"));
        #endregion

        #region Page Methods:
        /// <summary>
        /// Login: Performs login using parameters from the arguments list.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Login(string username, string password)
        {
            UserNameTextBox.EnterText(username);
            PasswordTextBox.EnterText(password);
            LoginButton.SubmitElement();
        }

        /// <summary>
        /// ClickLogin: Clicks the Login link locator.
        /// </summary>
        public void ClickLogin()
        {
            LoginLink.ClickElement();
        }

        public (bool driverTitleHomePage, bool manageUsers) IsLoggedIn() // Parameterize return types using Tuples.
        {
            bool isLoggedIn = false;
            string homepageTitle = driver.Title;

            if (homepageTitle.Contains("Home")) { isLoggedIn = true; }

            //return isLoggedIn;
            return (homepageTitle.Contains("Home"), ManageUsersLink.Displayed);
        }
        #endregion
    }
}
