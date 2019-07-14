using BidAskCore.DTOs;
using BidAskCore.Exceptions;
using BidAskCore.Scrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore
{
    public class ScrapperF1 : IScrapper
    {
        private string URL = "https://1forge.com/forex-data-api/currency-pair-list";
        private CurrencyDto currency = new CurrencyDto();

        public CurrencyDto ExtractEURUSDData()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            using (ChromeDriver driver = new ChromeDriver(chromeOptions))
            {
                
                driver.Navigate().GoToUrl(this.URL);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                try
                {
                    this.currency.EUR = this.GetEUR(driver);
                    this.currency.USD = this.GetUSD(driver);
                }
                catch(Exception ex)
                {
                    // log ex
                    throw new FailedScrappingException("F1");
                }
            }

            return this.currency;
        }

        private decimal? GetEUR(ChromeDriver driver)
        {
            IWebElement element = driver.FindElementByXPath("//tr[@class='base-EUR quote-USD']//span[@class='bid transition']");
            decimal? eur = decimal.Parse(element.Text.Trim());
            return eur;
        }

        private decimal? GetUSD(ChromeDriver driver)
        {
            IWebElement element = driver.FindElementByXPath("//tr[@class='base-EUR quote-USD']//span[@class='ask transition']");
            decimal? usd = decimal.Parse(element.Text.Trim());
            return usd;
        }

    }
}
