using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Scrapper_Domain;
using Scrapper_Domain.Models;

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
                    SportVisonDbModel svModel = new()
                    {
                        PriceWithDiscount = item.PriceWithDiscount,
                        RegularPrice = item.RegularPrice,
                        DiscountPercent = item.DiscountPercent,
                        Store = item.Store,
                        Brand = item.Brand,
                        Link = item.Link,
                        Name = item.Name
                    };

                    //KOGA KE VLEZE AKO IMA VEKE TAKVA STAVKA DA NE PRAVI ADD DA PRAI UPDATE
                    //LOGGER
                    await _scrapperDbContext.AddAsync(svModel);
                    await _scrapperDbContext.SaveChangesAsync();

                }
                catch (Exception)
                {
                    //_logger.LogInformation()
                    throw;
                }

            }

        }
    }
}
