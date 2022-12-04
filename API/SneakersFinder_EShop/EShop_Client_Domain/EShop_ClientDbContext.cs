using EShop_Client_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Domain
{
    public class EShop_ClientDbContext : DbContext
    {
        public EShop_ClientDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> User { get; set; }
    }
}
