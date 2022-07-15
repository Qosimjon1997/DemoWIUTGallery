using DemoWIUTGallery.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWIUTGallery.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Gallery> Galleries { get; set; }
    }
}
