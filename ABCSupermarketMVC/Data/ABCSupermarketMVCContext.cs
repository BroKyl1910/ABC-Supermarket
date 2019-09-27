using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ABCSupermarketMVC.Models
{
    public class ABCSupermarketMVCContext : DbContext
    {
        public ABCSupermarketMVCContext (DbContextOptions<ABCSupermarketMVCContext> options)
            : base(options)
        {
        }

        public DbSet<ABCSupermarketMVC.Models.Product> Product { get; set; }
    }
}
