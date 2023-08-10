using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		[HttpGet]
		[Route("employee-list")]
		public async Task<IActionResult> GetAllEmployees()
		{
			using (CrudDbContext db = new())
			{
				List<EmployeeDetail> listofEmployees = await db.EmployeeDetails.ToListAsync();
				return Ok(listofEmployees);
			}

		}


		[HttpGet("{id:int}")]
		//[Route("employee-details")]
		public async Task<IActionResult> GetEmployees(int id)
		{
			using (CrudDbContext db = new())
			{
				EmployeeDetail empDetail = await db.EmployeeDetails.FirstOrDefaultAsync(u => u.Id == id);
				if (empDetail.Id != null)
				{
					return Ok(empDetail);
				}
				return NotFound();
			}

		}
		[HttpPost]
		[Route("add-employee")]
		public async Task<IActionResult> AddEmployeeDetails(EmployeeDetail employeeDetail)
		{
			using (CrudDbContext db = new())
			{
				if (employeeDetail.Id != 0)
				{
					return BadRequest();
				}
				EmployeeDetail empDetail = new()
				{
					Name = employeeDetail.Name,
					Address = employeeDetail.Address,
					Mobile = employeeDetail.Mobile,
					CreatedAt = employeeDetail.CreatedAt
				};
				await db.EmployeeDetails.AddAsync(empDetail);
				db.SaveChanges();
				return Ok(empDetail);
			}

		}

		[HttpDelete("delete-employee-details/{id}")]
		public async Task<IActionResult> RemoveEmployeeDetails(int id)
		{
			using (CrudDbContext db = new())
			{
				EmployeeDetail empDetail = await db.EmployeeDetails.FirstOrDefaultAsync(u => u.Id == id);
				if (empDetail.Id != null)
				{
					db.EmployeeDetails.Remove(empDetail);
					await db.SaveChangesAsync();
					return Ok();
				}
				return NotFound();
			}

		}

		[HttpPut("update-employee-details/{id}")]
		public async Task<IActionResult> UpdateEmployeeDetails(int id, [FromBody] EmployeeDetail employeeDetail)
		{

			if (employeeDetail == null || id != employeeDetail.Id)
			{
				return BadRequest();
			}
			using (CrudDbContext db = new())
			{
				EmployeeDetail empDetail = await db.EmployeeDetails.FirstOrDefaultAsync(u => u.Id == id);
				if (empDetail == null)
				{
					return NotFound();
				}
					empDetail.Name = employeeDetail.Name;
					empDetail.Address = employeeDetail.Address;
					empDetail.Mobile= employeeDetail.Mobile;
					empDetail.CreatedAt = empDetail.CreatedAt;
					db.EmployeeDetails.Update(empDetail);
				await db.SaveChangesAsync();
				return Ok();

			}

		}

	}

}
