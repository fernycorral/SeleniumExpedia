using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SELENIUM
{
   public class ExpediaATest
    {
        Flights selectedFlights;
        TestClassExpedia expediaTest;
      [SetUp]
      public void Setup()
        {
            TestClassExpedia.driver = new ChromeDriver();
            TestClassExpedia.driver.Url = "https://www.expedia.mx/";
            expediaTest = new TestClassExpedia();

        }

        [Test]
        public void ArePricesOrderedByPrice()
        {
            selectedFlights = expediaTest.DoSearch();
            Assert.IsTrue(selectedFlights.IsOrdered());
        }

        [Test]
        public void TransitWindowShown()
        {
            
            Assert.DoesNotThrow(()=>expediaTest.DoSearchByHotelFly());
        }

        [Test]
        public void AreRatesLessThanFour()
        {
           Hotels hot = expediaTest.DoSearchByHotelFly();
           hot.Select5StartsHotels();
            Assert.Greater(hot.MinorRate(), 4);
        }
        [Test]
        public void NotifyUserCheapFlights()
        {
            selectedFlights = expediaTest.DoSearch(false,"Cancún, México");
            if (!selectedFlights.IsOrdered())
                selectedFlights.OrderByLowerPrice();
            Assert.IsTrue(selectedFlights.ExistCheapFlight(), "A cheap Cancun flight was found(1000 pesos cheaper)");
        }

        [TearDown]
        public void DisposeAll()
        {
            TestClassExpedia.driver.Close();
            TestClassExpedia.driver.Dispose();
               
        }
    }
}
