using bookcatalog.Dtos.SaleType;

namespace bookcatalog.Services.SaleTypeService;

public interface ISaleTypeService
{
    Task<ServiceResponse<List<GetSaleTypeDto>>> GetAllSaleType();
    Task<ServiceResponse<GetSaleTypeDto>> AddSaleType(AddSaleTypeDto saleType);
}