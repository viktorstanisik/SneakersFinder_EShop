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

        [HttpGet("scrape")]
        public async Task<IActionResult> ScrapSportVisionGroupation()
        {
            try
            {
                await _scrapperService.SportVisionScrapper();
                await _scrapperService.SportRealityScrapper();
                await _scrapperService.BuzzScrapper();

                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        //[HttpGet("sport-reality")]
        //public async Task<IActionResult> ScrapSportReality()
        //{
        //    try
        //    {
        //        await _scrapperService.SportRealityScrapper();
        //        return Ok();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
