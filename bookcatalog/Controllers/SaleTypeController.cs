using bookcatalog.Dtos.SaleType;
using bookcatalog.Services.SaleTypeService;
using Microsoft.AspNetCore.Mvc;

namespace bookcatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleTypeController : ControllerBase
{
    private readonly ISaleTypeService _saleTypeService;
    public SaleTypeController(ISaleTypeService saleTypeService)
    {
        _saleTypeService = saleTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _saleTypeService.GetAllSaleType());
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddSaleTypeDto saleType)
    {
        return Ok(await _saleTypeService.AddSaleType(saleType));
    }
}