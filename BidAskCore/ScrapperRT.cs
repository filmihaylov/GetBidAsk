using BidAskCore.DTOs;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore
{
    public class ScrapperRT
    {
        private string URL = "https://www.reuters.com/finance/currencies/quote?srcCurr=EUR&destCurr=USD";
        private CurrencyDto currency = new CurrencyDto();
        private ScrapingBrowser Browser = new ScrapingBrowser();


        public CurrencyDto ExtractEURUSDData()
        {
            WebPage page = this.GetPage();
            this.currency.EUR = this.GetEUR(page);
            this.currency.USD = this.GetUSD(page);

            return this.currency;
        }

        private WebPage GetPage()
        {
            WebPage PageResult = this.Browser.NavigateToPage(new Uri(this.URL));
            return PageResult;
        }

        private decimal? GetEUR(WebPage PageResult)
        {
            IEnumerable<HtmlNode> nodes = PageResult.Html.CssSelect(".bidAsk div");

            foreach(HtmlNode node in nodes)
            {
                HtmlNode child = node.PreviousSibling;

                if (child.InnerText.Contains("Bid"))
                {
                    return decimal.Parse(node.InnerText.Trim());
                }
            }

            return null;
        }

        private decimal? GetUSD(WebPage PageResult)
        {
            IEnumerable<HtmlNode> nodes = PageResult.Html.CssSelect(".bidAsk div");

            foreach (HtmlNode node in nodes)
            {
                HtmlNode child = node.PreviousSibling;

                if (child.InnerText.Contains("Offer"))
                {
                    return decimal.Parse(node.InnerText.Trim());
                }
            }

            return null;
        }
    }
}
