using CosmeticsShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WEB_153502_Sidorova.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cosmetics> CosmeticsSet{ get; set;}
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
    }
}
