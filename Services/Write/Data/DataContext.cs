using Microsoft.EntityFrameworkCore;
using Write.Models;

namespace Write.Data
{
    public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}
        public DbSet<Article> Articles { get; set; }
	}
}