using Scrapper_Domain.Models;

namespace Scrapper_DataAccess.Repositories.SportVision
{
    public interface ISportVisionRepository
    {
        Task SaveEntities(List<ScrappedModel> inputModels);
    }
}
