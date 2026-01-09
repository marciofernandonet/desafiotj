using AutoMapper;
using bookcatalog.Data;
using bookcatalog.Dtos.SaleType;
using bookcatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace bookcatalog.Services.SaleTypeService;

public class SaleTypeService : ISaleTypeService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public SaleTypeService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetSaleTypeDto>>> GetAllSaleType()
    {
        ServiceResponse<List<GetSaleTypeDto>> serviceResponse = new();

        try
        {
            var dbSaleTypes = await _context.TipoVenda.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetSaleTypeDto>>(dbSaleTypes);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetSaleTypeDto>> AddSaleType(AddSaleTypeDto saleType)
    {
        ServiceResponse<GetSaleTypeDto> serviceResponse = new();

        try
        {
            var newSaleType = _mapper.Map<SaleType>(saleType);

            await _context.TipoVenda.AddAsync(newSaleType);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetSaleTypeDto>(newSaleType);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }
}