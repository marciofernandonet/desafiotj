using bookcatalog.Data;
using bookcatalog.Dtos.Report;
using bookcatalog.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace bookcatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly DataContext _context;

    public ReportController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet("author")]
    public async Task<IActionResult> Hello()
    {
        var data = await _context
        .Set<ReportView>()
        .AsNoTracking()
        .ToListAsync();

        var report = new AuthorReport(data);
        var pdf = report.GeneratePdf();

        return File(pdf, "application/pdf", "Relatorio.pdf");
    }
}