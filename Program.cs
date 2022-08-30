using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace CountJobs
{
    class Program
    {
        private const string ChromeDriverDirectory = @"C:\Users\Acer\Desktop\";     

        class SearchParameters
        {
            private string Dropdown { get; set; }
            private string Filter { get; set; }           

            public SearchParameters(string dropdown, string filter)
            {
                this.Dropdown = dropdown;
                this.Filter = filter;
            }

            public string GetDropdown()
            {
                return this.Dropdown;
            }

            public string GetFilter()
            {
                return this.Filter;
            }
        }  
        
        public static bool CheckResult(int count, int expectedCount)
        {
            if (count == expectedCount)
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            IWebDriver driver = new ChromeDriver(ChromeDriverDirectory, options);
            driver.Url = "https://cz.careers.veeam.com/vacancies";

            var searchDepartment = new SearchParameters("All departments", "Research & Development");
            var searchLanguage = new SearchParameters("All languages", "English");

            driver.FindElement(By.XPath($"//button[normalize-space() = '{searchDepartment.GetDropdown()}']")).Click();            
            driver.FindElement(By.XPath($"//a[normalize-space() = '{searchDepartment.GetFilter()}']")).Click();         

            driver.FindElement(By.XPath($"//button[normalize-space() = '{searchLanguage.GetDropdown()}']")).Click();           
            driver.FindElement(By.XPath($"//label[normalize-space() = '{searchLanguage.GetFilter()}']")).Click();
            
            var count = driver.FindElements(By.CssSelector("a[class='card card-sm card-no-hover']")).Count;
            driver.Close();

            var expectedCount = 11;            
            var checkResult = CheckResult(count, expectedCount);
            Console.WriteLine("\nExpected result: " + checkResult);
        }
    }
}
