using Microsoft.EntityFrameworkCore;
using SPP.Back.Model;

namespace SPP.Back.Data
{
	public class ApplicationDbContext : DbContext
	{
		//public DbSet<Test> Tests { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{ }
	}
}
