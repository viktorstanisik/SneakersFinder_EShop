using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapperServices.Services.Scrapper
{
    public interface IScrapperService
    {
        public Task SportVisionScrapper();
        public Task SportRealityScrapper();
        public Task BuzzScrapper();

    }
}
