using In_Memory_Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace In_Memory_Db.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public EmployeeController(ApplicationDbContext context)
		{
			_context = context;
		}
		[Authorize]
		[HttpGet]
		[Route("employee-list")]
		public async Task<IActionResult> GetAllEmployees()
		{
			List<EmployeeDetail> listofEmployees = await _context.EmployeeDetails.ToListAsync();
			return Ok(listofEmployees);

		}

		[Authorize]
		[HttpGet("get-details/{id}")]
		//[Route("employee-details")]
		public async Task<IActionResult> GetEmployees(int id)
		{

			EmployeeDetail empDetail = await _context.EmployeeDetails.FirstOrDefaultAsync(u => u.Id == id);
			if (empDetail.Id != null)
			{
				return Ok(empDetail);
			}
			return NotFound();

		}
		[Authorize]
		[HttpPost]
		[Route("add-employee")]
		public async Task<IActionResult> AddEmployeeDetails(EmployeeDetail employeeDetail)
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
			await _context.EmployeeDetails.AddAsync(empDetail);
			_context.SaveChanges();
			return Ok(empDetail);


		}
		[Authorize]
		[HttpDelete("delete-employee-details/{id}")]
		public async Task<IActionResult> RemoveEmployeeDetails(int id)
		{

			EmployeeDetail empDetail = await _context.EmployeeDetails.FirstOrDefaultAsync(u => u.Id == id);
			if (empDetail.Id != null)
			{
				_context.EmployeeDetails.Remove(empDetail);
				await _context.SaveChangesAsync();
				return Ok();
			}
			return NotFound();


		}
		[Authorize]
		[HttpPut("update-employee-details/{id}")]
		public async Task<IActionResult> UpdateEmployeeDetails(int id, [FromBody] EmployeeDetail employeeDetail)
		{

			if (employeeDetail == null || id != employeeDetail.Id)
			{
				return BadRequest();
			}

			EmployeeDetail empDetail = await _context.EmployeeDetails.FirstOrDefaultAsync(u => u.Id == id);
			if (empDetail == null)
			{
				return NotFound();
			}
			empDetail.Name = employeeDetail.Name;
			empDetail.Address = employeeDetail.Address;
			empDetail.Mobile = employeeDetail.Mobile;
			empDetail.CreatedAt = empDetail.CreatedAt;
			_context.EmployeeDetails.Update(empDetail);
			await _context.SaveChangesAsync();
			return Ok();



		}

	}
}
