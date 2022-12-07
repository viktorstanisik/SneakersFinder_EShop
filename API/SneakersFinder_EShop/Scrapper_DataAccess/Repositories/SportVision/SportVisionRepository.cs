using Scrapper_Domain;
using Scrapper_Domain.Models;
using Scrapper_Shared.Enums;
using System.Linq;

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

        public async Task SaveEntities(List<ScrappedModel> models, Store store)
        {
            try
            {
                List<SportVisonDbModel> productsFromScrapper = new();
                models.ForEach(x => productsFromScrapper.Add(new SportVisonDbModel
                {
                    PriceWithDiscount = x.PriceWithDiscount,
                    RegularPrice = x.RegularPrice,
                    DiscountPercent = x.DiscountPercent,
                    Store = x.Store,
                    Brand = x.Brand,
                    Link = x.Link,
                    Name = x.Name
                }));


                // Get all products for given store
                var productsFromDb = _scrapperDbContext.SportVisonDbModel.Where(x => x.Store == (int)store);

                // Product that exist in db, but does not exist in code - Product does not exist in store anymore
                // Ne e funkcionalno zaradi toa sto nemoze da se izvrsi Except na IQueryable
                // Da se najde nacin kako da se sporedat 2te listi i da se izvlecat produktite sto gi ima vo baza, a ne gi nasol skraperot
                //var productThatDoesNotExistAnymore = productsFromDb.Where(x => !productsFromScrapper.Contains(x));
                //var productThatDoesNotExistAnymore = productsFromDb.ToList().Except(productsFromScrapper);

                foreach (var product in productsFromDb)
                {
                    // If product exists update it and remove if from models that come from code
                    // If product does not exist leave it to get inserted
                    var model = models.FirstOrDefault(x => x.Link == product.Link);

                    if (model != null)
                    {
                        models.Remove(model);
                        product.PriceWithDiscount = model.PriceWithDiscount;
                        product.RegularPrice = model.RegularPrice;
                        product.DiscountPercent = model.DiscountPercent;
                        product.Store = model.Store;
                        product.Brand = model.Brand;
                        product.Link = model.Link;
                        product.Name = model.Name;
                    }
                }

                //productsFromDb.ToList().RemoveRange(productsFromScrapper);
                _scrapperDbContext.SportVisonDbModel.UpdateRange(productsFromDb);
                _scrapperDbContext.SportVisonDbModel.AddRange(productsFromScrapper);


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
