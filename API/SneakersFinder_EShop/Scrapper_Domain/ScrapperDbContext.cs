using Microsoft.EntityFrameworkCore;
using Scrapper_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper_Domain
{
    public class ScrapperDbContext : DbContext
    {
        public ScrapperDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<SportVisonDbModel> SportVisonDbModel { get; set; }

     
    }
}
