using OpenQA.Selenium.Support.UI;

namespace DotnetSelenium.Extensions
{
    public static class SeleniumCustomMethods
    {
        /// <summary>
        /// Click on the locator element.
        /// </summary>
        /// <param name="locator"></param>
        public static void ClickElement(this IWebElement locator)
        {
            locator.Click();
        }

        /// <summary>
        /// Click on the locator element via the Submit method.
        /// </summary>
        /// <param name="locator"></param>
        public static void SubmitElement(this IWebElement locator)
        {
            locator.Submit();
        }

        /// <summary>
        /// Enter text in the locator element via the SendKeys method based on the aurguments list.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="text"></param>

        public static void EnterText(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }

        /// <summary>
        /// Select dropdown by text based on the locator element and text on the aurguments list.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="text"></param>

        public static void SelectDropdownByText(this IWebElement locator, string text)
        {
            SelectElement selectElement = new SelectElement(locator);
            selectElement.SelectByText(text);
        }

        /// <summary>
        /// Select dropdown by Value based on the locator element and text on the aurguments list.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="text"></param>

        public static void SelectDropdownByValue(this IWebElement locator, string value)
        {


            //IWebElement dropdown = driver.FindElement(locator);
            SelectElement selectElement = new SelectElement(locator);
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Multi Select Elements by Value based on the aurguments list.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="values"></param>
        public static void MultiSelectElementsByValue(this IWebElement locator, string[] values)
        {
            SelectElement multiSelectElement = new SelectElement(locator);

            foreach (var value in values)
            {
                multiSelectElement.SelectByValue(value);
            }
        }

        /// <summary>
        /// Get Selected Elements by multi-selecting elements based on the aurguments list.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static List<string> GetSelectedElements(this IWebElement locator)
        {
            List<string> options = new List<string>();
            SelectElement multiSelectElement = new SelectElement(locator);
            IList<IWebElement> selectedOption = multiSelectElement.AllSelectedOptions;

            foreach (var option in selectedOption)
            {
                options.Add(option.Text);
            }

            return options;
        }
    }
}


