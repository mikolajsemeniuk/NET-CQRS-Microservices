using Microsoft.EntityFrameworkCore;

namespace Write.Data
{
    public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}
	}
}