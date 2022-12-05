using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrapper_DataAccess.Repositories.SportVision;
using Scrapper_Domain.Models;
using Scrapper_Shared.Enums;
using System.Text.Json;

namespace ScrapperServices.Services.Scrapper
{
    public class ScrapperService : IScrapperService
    {
        private readonly ISportVisionRepository _sportVisionRepository;
        private readonly IConfiguration _configuration;
        public ScrapperService(ISportVisionRepository sportVisionRepository, IConfiguration configuration)
        {
            _sportVisionRepository = sportVisionRepository;
            _configuration = configuration;
        }

        public async Task SportRealityScrapper()
        {
            var models = ScrapeMain(_configuration["StoresUrl:SportReality"], Store.SportReality);
            if (models.Count > 0)
            {
                await _sportVisionRepository.SaveEntities(models);
            }
        }

        public async Task SportVisionScrapper()
        {

            var models = ScrapeMain(_configuration["StoresUrl:SportVison"], Store.SportVision);
            if (models.Count > 0)
            {
                await _sportVisionRepository.SaveEntities(models);
            }
        }

        public async Task BuzzScrapper()
        {

            var models = ScrapeMain(_configuration["StoresUrl:Buzz"], Store.Buzz);
            if (models.Count > 0)
            {
                await _sportVisionRepository.SaveEntities(models);
            }
        }


        private List<ScrappedModel> ScrapeMain(string url, Store store)
        {
            int page = 0;
            List<ScrappedModel> items = new();
            using (ChromeDriver driver = new ChromeDriver())
            {
                while (page < 2)
                {
                    driver.Navigate().GoToUrl($"{url}/page-{page}");
                    var productData = driver.FindElements(By.CssSelector(".item-data")).ToList();

                    if (productData.Count == 0)
                        break;

                    foreach (var prod in productData)
                    {
                        var prices = ParsePrices(prod.FindElement(By.CssSelector(".prices-wrapper")).Text.Split("\r\n").ToList());

                        items.Add(new ScrappedModel()
                        {
                            Name = prod.FindElement(By.ClassName("title")).Text,
                            Brand = ConvertBrandToEnum(prod.FindElement(By.CssSelector(".brand > a")).GetAttribute("text")),
                            Link = prod.FindElement(By.CssSelector(".img-wrapper > a")).GetAttribute("href"),
                            PriceWithDiscount = prices.PriceWithDiscount,
                            RegularPrice = prices.RegularPrice,
                            DiscountPercent = prices.Discount,
                            Store = (int)store
                        });
                    }

                    page++;
                }
            }
           return items;
        }


        private int ConvertBrandToEnum(string brand)
        {
            bool isParseValid = Enum.TryParse(brand.ToLower().Replace(" ", ""), true, out Brands enumInt);

            if (isParseValid)
                return (int)enumInt;

            return 0;
        }

        private ParsePriceHelperModel ParsePrices(List<string> nonParsedPrices)
        {
            ParsePriceHelperModel helperModel = new ParsePriceHelperModel();
            List<int> parsedPrices = new();
            foreach (string price in nonParsedPrices)
            {
                bool isParseValid = int.TryParse(price.Replace(".", "").Replace("%", "").Replace("ДЕН", "").Replace("MKD", "").Trim(), out int parsedPrice);
                if (isParseValid)
                    parsedPrices.Add(parsedPrice);
            }
            parsedPrices.Sort((a, b) => b.CompareTo(a));

            if (parsedPrices.Count().Equals(3) || parsedPrices.Count().Equals(2))
            {
                helperModel.RegularPrice = parsedPrices[0];
                helperModel.PriceWithDiscount = parsedPrices[1];
            }
            else
            {
                helperModel.RegularPrice = parsedPrices[0];
                helperModel.PriceWithDiscount = parsedPrices[0];
            }

            if (helperModel.RegularPrice != helperModel.PriceWithDiscount)
                helperModel.Discount = FindDiscount(helperModel.RegularPrice, helperModel.PriceWithDiscount);

            return helperModel;
        }

        private int FindDiscount(int regularPrice, int priceWithDiscount)
        {
            decimal res = ((decimal)regularPrice - (decimal)priceWithDiscount) / (decimal)regularPrice;
            return (int)(res * 100);
        }

        private class ParsePriceHelperModel
        {
            public int RegularPrice { get; set; }
            public int PriceWithDiscount { get; set; }
            public int Discount { get; set; } = 0;
        }
    }
}
