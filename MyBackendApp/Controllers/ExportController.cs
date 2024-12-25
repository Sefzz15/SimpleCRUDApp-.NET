using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendApp.Models; 


[ApiController]
[Route("api/[controller]")]
public class ExportController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExportController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportData()
    {
        try
        {
            // Query data from the database
            var dataToExport = await _context.MyTable.ToListAsync();

            if (dataToExport == null || !dataToExport.Any())
            {
                return NotFound("No data found to export.");
            }

            // Log to console (optional)
            Console.WriteLine($"Exported {dataToExport.Count} records.");

            // Return data in the response
            return Ok(dataToExport);
        }
        catch (Exception ex)
        {
            // Log error and return failure message
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
}
