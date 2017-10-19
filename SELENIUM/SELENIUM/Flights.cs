using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SELENIUM
{
    class Flights
    {
        public Flights()
        {
            WebDriverWait wait = new WebDriverWait(TestClassExpedia.driver, TimeSpan.FromMilliseconds(30000));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='flight-listing-container']/ul/li/h3")));
            PageFactory.InitElements(TestClassExpedia.driver, this);

        }

        [FindsBy(How=How.Name,Using ="sort")]
        private IWebElement ComboPrice{ get; set; }
        [FindsBy(How = How.XPath, Using = "//div[@id='flight-listing-container']/ul/li/h3")]
        private IWebElement LowerPrice { get; set; }

        public Boolean IsOrdered()
        {
            var selectedOpt = new SelectElement(ComboPrice).SelectedOption;
            return selectedOpt.Text.Contains("Precio (menor)");

        }

        public void OrderByLowerPrice()
        {
            new SelectElement(ComboPrice).SelectByText("Precio (menor)");
        }
        public Boolean ExistCheapFlight()
        {
            if(!Directory.Exists("c:/cc"))
                Directory.CreateDirectory("c:/cc");
            string localStorageFile = "c:/cc/LastCheapFlyPriceToCancun.txt";
            string lastPrice = string.Empty;
            if (!File.Exists(localStorageFile))
                File.WriteAllText(localStorageFile,"1");
            using (StreamReader reader = new StreamReader(localStorageFile))
            {
                lastPrice = reader.ReadToEnd();
            }
            
            string lowPrice = Regex.Match(LowerPrice.Text, "\\$(.*)").Groups[1].Value.Replace(",", "");
            if (Convert.ToInt32(lowPrice) <= Convert.ToInt32(lastPrice) - 1000)
                return true;
            else
            {
                
                using (StreamWriter sw =  new StreamWriter(localStorageFile))
                {
                    
                    sw.WriteLine(lowPrice);
                }
            }


            return false;
        }


    }
}
