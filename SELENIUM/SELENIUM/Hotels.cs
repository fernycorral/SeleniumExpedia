using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SELENIUM
{
    class Hotels
    {
        public Hotels()
        {
            WebDriverWait wait = new WebDriverWait(TestClassExpedia.driver, TimeSpan.FromMilliseconds(30000));
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("article")));
            PageFactory.InitElements(TestClassExpedia.driver, this);
        }

        [FindsBy(How = How.Id, Using = "star5")]
        private IWebElement Stars { get; set; }

        public void Select5StartsHotels()
        {
            Stars.Click();
            System.Threading.Thread.Sleep(6000);
        }

        public int MinorRate()
        {

            IList<IWebElement> Hotels5Stars =
                TestClassExpedia.driver.FindElements(By.TagName("article"));
            string homeWindowName = TestClassExpedia.driver.WindowHandles.Last();
            foreach (IWebElement hotel in Hotels5Stars)
            {
                hotel.FindElement(By.XPath(".//a[@href]")).Click();
              
                string windowName = TestClassExpedia.driver.WindowHandles.Last();
                TestClassExpedia.driver.SwitchTo().Window(windowName);
                
                  WebDriverWait wait = new WebDriverWait(TestClassExpedia.driver, TimeSpan.FromMilliseconds(30000));
                 wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div/h1[@id='hotel-name']")));
                TestClassExpedia.driver.FindElement(By.XPath("//span[contains(text(),'Reseñas de huéspedes')]")).Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("reviews-sort-selector")));
                IWebElement reviews = TestClassExpedia.driver.FindElement(By.Id("reviews-sort-selector"));
                new SelectElement(reviews).SelectByIndex(3);
                IWebElement firstReview = TestClassExpedia.driver.FindElement(By.TagName("article"));
                IList<IWebElement>  rates = firstReview.FindElements(By.XPath("//span[@class='rating']/span"));
                foreach(IWebElement rate in rates)
                {
                    if (Convert.ToInt32(rate.Text) < 4) { }
                        return Convert.ToInt32(rate.Text);
                }
                    TestClassExpedia.driver.Close();
                    TestClassExpedia.driver.SwitchTo().Window(homeWindowName);
                    
                
            }
            return 5;
        }
           
    }
}
