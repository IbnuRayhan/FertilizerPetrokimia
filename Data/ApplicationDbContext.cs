using FertilizerPetrokimia.Models;
namespace FertilizerPetrokimia.Data
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tambahkan DbSet untuk entitas Anda di sini
        public DbSet<Product> Products { get; set; }

        internal async Task FindAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
