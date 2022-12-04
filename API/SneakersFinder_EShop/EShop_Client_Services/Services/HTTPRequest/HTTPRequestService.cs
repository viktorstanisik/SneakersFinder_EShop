using EShop_Client_Shared.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Services.Services.HTTPRequest
{
    public class HTTPRequestService : IHTTPRequestService
    {
        public async Task CallScrapper()
        {
            string json;

            using (var client = new HttpClient())
            {
                var url = $"https://localhost:7016/api/Scrapper/sport-vision";

                var request = new RestRequest("/resource/", Method.Get);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = new Uri(url);

                var result = await client.GetAsync(endpoint);
                json = await result.Content.ReadAsStringAsync();
            }

        }
    }
}
