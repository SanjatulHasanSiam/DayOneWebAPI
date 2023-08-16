using System.ComponentModel.DataAnnotations;

namespace sql_lite_db.Models
{
	public class EmployeeDetail
	{
		[Key]
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? Address { get; set; }

		public string? Mobile { get; set; }

		public DateTime? CreatedAt { get; set; }
	}
}
