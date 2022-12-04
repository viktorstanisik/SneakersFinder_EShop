using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Services.Services.HTTPRequest
{
    public interface IHTTPRequestService
    {
        Task CallScrapper();
    }
}
