using DeepEqual.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Scrapper_Domain;
using Scrapper_Domain.Models;
using Scrapper_Shared.Enums;
using System.Xml.Linq;

namespace Scrapper_DataAccess.Repositories.SportVision
{
    public class SportVisionRepository : ISportVisionRepository
    {
        private readonly ScrapperDbContext _scrapperDbContext;
        //private readonly ILogger _logger;
        public SportVisionRepository(ScrapperDbContext scrapperDbContext/* ILogger logger*/)
        {
            _scrapperDbContext = scrapperDbContext;
            //_logger = logger;
        }

        public async Task SaveEntities(List<ScrappedModel> models)
        {


            foreach (var item in models)
            {
                try
                {
                    var model = _scrapperDbContext.SportVisonDbModel.FirstOrDefault(x => x.Link == item.Link);
                    
                    if(model != null)
                    {
                        model.PriceWithDiscount = item.PriceWithDiscount;
                        model.RegularPrice = item.RegularPrice;
                        model.DiscountPercent = item.DiscountPercent;
                        model.Store = item.Store;
                        model.Brand = item.Brand;
                        model.Link = item.Link;
                        model.Name = item.Name;

                        _scrapperDbContext.SportVisonDbModel.Update(model);
                    }
                    else
                    {
                        await _scrapperDbContext.SportVisonDbModel.AddAsync(new SportVisonDbModel
                        {
                            PriceWithDiscount = item.PriceWithDiscount,
                            RegularPrice = item.RegularPrice,
                            DiscountPercent = item.DiscountPercent,
                            Store = item.Store,
                            Brand = item.Brand,
                            Link = item.Link,
                            Name = item.Name
                        });
                    }

                    //KOGA KE VLEZE AKO IMA VEKE TAKVA STAVKA DA NE PRAVI ADD DA PRAI UPDATE
                    //LOGGER
                    await _scrapperDbContext.SaveChangesAsync();

                }
                catch (Exception)
                {
                    //_logger.LogInformation()
                    throw;
                }

            }

        }

        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }
    }
}
