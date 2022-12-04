using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrapperServices.Services.Scrapper;

namespace Scrapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapperController : ControllerBase
    {
        private IScrapperService _scrapperService;

        public ScrapperController(IScrapperService scrapperService)
        {
            _scrapperService = scrapperService;
        }

        [HttpGet("sport-vision")]
        public async Task<IActionResult> ScrapSportVision()
        {
            try
            {
                await _scrapperService.SportVisionScrapper();
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
