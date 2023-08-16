using Microsoft.EntityFrameworkCore;

namespace sql_lite_db.Models
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
		public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

	}
}
