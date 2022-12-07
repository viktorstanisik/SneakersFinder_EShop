using Scrapper_Domain.Models;
using Scrapper_Shared.Enums;

namespace Scrapper_DataAccess.Repositories.SportVision
{
    public interface ISportVisionRepository
    {
        Task SaveEntities(List<ScrappedModel> inputModels, Store store);
    }
}
