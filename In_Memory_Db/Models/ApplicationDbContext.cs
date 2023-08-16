using Microsoft.EntityFrameworkCore;

namespace In_Memory_Db.Models
{
	public class ApplicationDbContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName:"InMemoryDb");
		}
		public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
	}
}
