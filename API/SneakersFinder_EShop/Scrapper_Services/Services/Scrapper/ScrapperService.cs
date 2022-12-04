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

        public ScrapperService(ISportVisionRepository sportVisionRepository)
        {
            _sportVisionRepository = sportVisionRepository;
        }

        public async Task SportVisionScrapper()
        {
            //int page = 125;
            int page = 0;
            List<SportVisonScrappedModel> items = new();
            using (ChromeDriver driver = new ChromeDriver())
            {
                while (page < 2)
                {
                    driver.Navigate().GoToUrl($"https://www.sportvision.mk/obuvki/page-{page}");
                    var productData = driver.FindElements(By.CssSelector(".item-data")).ToList();

                    if (productData.Count == 0)
                        break;

                    foreach (var prod in productData)
                    {
                        var prices = prod.FindElement(By.CssSelector(".prices-wrapper")).Text.Split("\r\n");

                        items.Add(new SportVisonScrappedModel()
                        {
                            Name = prod.FindElement(By.ClassName("title")).Text,
                            Brand = ConvertBrandToEnum(prod.FindElement(By.CssSelector(".brand > a")).GetAttribute("text")),
                            Link = prod.FindElement(By.CssSelector(".img-wrapper > a")).GetAttribute("href"),
                            PriceWithDiscount = ParsePrice(prices[0]),
                            RegularPrice = prices.Count().Equals(3) ? ParsePrice(prices[2]) : ParsePrice(prices[0]),
                            DiscountPercent = prices.Count().Equals(3) ? ParsePrice(prices[1]) : 0,
                            Store = (int)Store.SportVision
                        });
                    }

                    page++;
                }
                //var itemsSerialized = JsonSerializer.Serialize(items);

            }
                await _sportVisionRepository.SaveEntities(items);
        }

        private int ConvertBrandToEnum(string brand)
        {
            bool isParseValid = Enum.TryParse(brand.ToLower().Replace(" ", ""), true, out Brands enumInt);

            if (isParseValid)
                return (int)enumInt;

            return 0;
        }

        private int ParsePrice(string price)
        {
            bool isParseValid = int.TryParse(price.Replace(".", "").Replace("%", "").Replace("ДЕН", "").Trim(), out int parsedPrice);

            if (isParseValid)
                return parsedPrice;

            return parsedPrice;
        }

    }
}
