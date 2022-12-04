using EShop_Client_Services.Services.HTTPRequest;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop_Client_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        private IHTTPRequestService _httpService;

        public HangfireController(IHTTPRequestService httpService)
        {
            _httpService = httpService;
        }

        [HttpPost("sendmail")]
        public IActionResult HangFireMailInvoice()
        {

                RecurringJob.AddOrUpdate(() => _httpService.CallScrapper(), Cron.Daily());
            
                return Ok($"RecurringJob scheduled (minutely)");
        }
    }
}
