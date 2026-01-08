using AutoMapper;
using bookcatalog.Data;
using bookcatalog.Dtos.Author;
using bookcatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace bookcatalog.Services.AuthorService;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    
    public AuthorService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetAuthorDto>>> GetAllAuthors()
    {
        ServiceResponse<List<GetAuthorDto>> serviceResponse = new();

        try
        {
            List<Author> dbAuthors = await _context.Autor.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetAuthorDto>>(dbAuthors);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;        
    }

    public async Task<ServiceResponse<GetAuthorDto>> GetAuthor(int id)
    {
        ServiceResponse<GetAuthorDto> serviceResponse = new();

        try
        {
            Author dbAuthor = await _context.Autor.FirstAsync(x => x.Id == id);
            
            if(dbAuthor != null)
            {
                serviceResponse.Data = _mapper.Map<GetAuthorDto>(dbAuthor);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Author not found.";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetAuthorDto>> AddAuthor(AddAuthorDto author)
    {
        ServiceResponse<GetAuthorDto> serviceResponse = new();

        try
        {
            Author newAuthor = _mapper.Map<Author>(author);

            await _context.Autor.AddAsync(newAuthor);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetAuthorDto>(newAuthor);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(UpdateAuthorDto author)
    {
        ServiceResponse<GetAuthorDto> serviceResponse = new();

        try
        {
            Author dbAuthor = await _context.Autor.FirstAsync(x => x.Id == author.Id);
            dbAuthor.Nome = author.Nome;

            _context.Autor.Update(dbAuthor);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetAuthorDto>(dbAuthor);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetAuthorDto>> DeleteAuthor(int id)
    {
        ServiceResponse<GetAuthorDto> serviceResponse = new();

        try
        {
            Author dbAuthor = await _context.Autor.FirstAsync(x => x.Id == id);
            
            if(dbAuthor != null)
            {
                _context.Autor.Remove(dbAuthor);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetAuthorDto>(dbAuthor);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Author not found.";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }
}