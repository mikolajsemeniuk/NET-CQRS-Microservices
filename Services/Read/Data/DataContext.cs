using Microsoft.EntityFrameworkCore;
using Read.Models;

namespace Read.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
    }
}