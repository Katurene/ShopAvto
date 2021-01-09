using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopAvto.Data.Models;

namespace ShopAvto.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
