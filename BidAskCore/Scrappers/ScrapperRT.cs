using BidAskCore.DTOs;
using BidAskCore.Exceptions;
using BidAskCore.Scrappers;
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
    public class ScrapperRT : IScrapper
    {
        private string URL = "https://www.reuters.com/finance/currencies/quote?srcCurr=EUR&destCurr=USD";
        private CurrencyDto currency = new CurrencyDto();
        private ScrapingBrowser Browser = new ScrapingBrowser();


        public CurrencyDto ExtractEURUSDData()
        {
            IEnumerable<HtmlNode> nodes = this.GetNodes();
            try
            {
                this.currency.EUR = this.GetEUR(nodes);
                this.currency.USD = this.GetUSD(nodes);
            }
            catch(Exception ex)
            {
                //log ex
                throw new FailedScrappingException("RT");
            }

            return this.currency;
        }

        private IEnumerable<HtmlNode> GetNodes()
        {
            WebPage PageResult = this.Browser.NavigateToPage(new Uri(this.URL));
            IEnumerable<HtmlNode> nodes = PageResult.Html.CssSelect(".bidAsk div");
            return nodes;
        }

        private decimal? GetEUR(IEnumerable<HtmlNode> nodes)
        {
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

        private decimal? GetUSD(IEnumerable<HtmlNode> nodes)
        {
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
