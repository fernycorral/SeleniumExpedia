using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELENIUM
{

                
    class TestClassExpedia
    {
        public static IWebDriver driver;

        public TestClassExpedia()
        {
          PageFactory.InitElements(driver, this);
        }

        //ids changed often in the site I let few below

        // [FindsBy(How = How.XPath,Using = "//li/a[@data-tab='flight']")]
        // [FindsBy(How = How.XPath, Using = "//li/button[@id='tab-flight-tab-hp']")]
        //[FindsBy(How = How.XPath, Using = "//li/a[@id='tab-flight-tab']")]
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Vuelos')]")]
        private IWebElement FlyButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Vuelo y hotel')]")]
        private IWebElement FlyHotelButton { get; set; }
        //[FindsBy(How = How.Id,Using= "flight-origin")]
        //[FindsBy(How = How.Id, Using = "flight-origin-hp-flight")]
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Origen')]/following-sibling::input[@placeholder='Ciudad o aeropuerto']")]        
        private IWebElement OrigenTxtBox { get; set; }
        //[FindsBy(How = How.Id, Using = "flight-destination")]
        //[FindsBy(How = How.Id, Using = "flight-destination-hp-flight")]
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Destino')]/following-sibling::input[@placeholder='Ciudad o aeropuerto']")]
        private IWebElement DestTxtBox { get; set; }
        //[FindsBy(How = How.Id, Using = "flight-departing")]
        //[FindsBy(How = How.Id, Using = "flight-departing-hp-flight")]
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Salida')]/following-sibling::input")]
        private IWebElement DateDep { get; set; }
        //[FindsBy(How = How.Id, Using = "flight-returning")]
        //[FindsBy(How = How.Id, Using = "flight-returning-hp-flight")]
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Regreso')]/following-sibling::input")]
        private IWebElement DateRet { get; set; }
        [FindsBy(How=How.XPath,Using = "//td/button[@data-day='22' and @data-month='10']")]
        private IWebElement DepDate { get; set; }
        [FindsBy(How=How.XPath,Using = "//td/button[@data-day='24' and @data-month='10']")]
        private IWebElement RetDate { get; set; }
        [FindsBy(How = How.XPath, Using = "//label[.//button/span[contains(text(),'Buscar')]]")]
        //[FindsBy(How=How.Id,Using = "search-button")]
        private IWebElement SubmitBton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Salida')]/following-sibling::input[@placeholder='Ciudad o aeropuerto']")]
        private IWebElement FlyHotelOrigin { get; set; }
        //[FindsBy(How = How.Id, Using = "flight-destination")]
        //[FindsBy(How = How.Id, Using = "flight-destination-hp-flight")]
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Llegada')]/following-sibling::input[@placeholder='Ciudad o aeropuerto']")]
        private IWebElement FlyHotelDest { get; set; }






        public Flights DoSearch(Boolean check2Scales= true,string dest = "Barcelona, España (BCN-Todos los aeropuertos)")
        {
            FlyButton.Click();
            OrigenTxtBox.SendKeys("Chihuahua, Chihuahua, México (CUU-A. Internacional General Roberto Fierro Villalobos)");
            DestTxtBox.SendKeys(dest);
            DateDep.Click(); //some times this node isn't visible it is required to clean and build to run propertly I didn't figure it out why   
            DepDate.Click();
            DateRet.Click();
            RetDate.Click();
            SubmitBton.Click();
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromMilliseconds(30000));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("stopFilter_stops-2")));
            if(check2Scales)
                driver.FindElement(By.Id("stopFilter_stops-2")).Click();

            return new Flights();

        }

        public Hotels DoSearchByHotelFly()
        {
            FlyButton.Click();
            OrigenTxtBox.SendKeys("Chihuahua, Chihuahua, México (CUU-A. Internacional General Roberto Fierro Villalobos)");
            DestTxtBox.SendKeys("Barcelona, España (BCN-Todos los aeropuertos)");
           
            DateDep.Click();
            DepDate.Click();
            DateRet.Click();
            RetDate.Click();
            FlyHotelButton.Click();
            SubmitBton.Click();
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(30000));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='imgLoading']")));
            }catch(TimeoutException tex)
            {
                throw new Exception();
            }

            return new Hotels();
        
        }
    }


   

}
