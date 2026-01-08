using AutoMapper;
using bookcatalog.Data;
using bookcatalog.Dtos.Book;
using bookcatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace bookcatalog.Services.BookService;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public BookService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetBookDto>>> GetAllBooks()
    {
        ServiceResponse<List<GetBookDto>> serviceResponse = new();

        try
        {
            List<Book> dbBooks = await _context.Livro.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetBookDto>>(dbBooks);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetBookDto>> GetBook(int id)
    {
        ServiceResponse<GetBookDto> serviceResponse = new();

        try
        {
            Book dbBook = await _context.Livro
                                        .Include(x => x.LivroAutor)
                                        .Include(x => x.LivroAssunto)
                                        .FirstAsync(x => x.Id == id);
            
            if(dbBook != null)
            {
                serviceResponse.Data = _mapper.Map<GetBookDto>(dbBook);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book not found.";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetBookDto>> AddBook(AddBookDto book)
    {
        ServiceResponse<GetBookDto> serviceResponse = new();

        try
        {
            var newBook = new Book
            {
                Titulo = book.Titulo,
                Editora = book.Editora,
                Edicao = book.Edicao,
                AnoPublicacao = book.AnoPublicacao,
                LivroAutor = [],
                LivroAssunto = []
            };

            foreach (var autorId in book.AutoresIds)
            {
                newBook.LivroAutor.Add(new BookAuthor
                {
                    AutorId = autorId
                });
            }

            foreach (var assuntoId in book.AssuntosIds)
            {
                newBook.LivroAssunto.Add(new BookSubject
                {
                    AssuntoId = assuntoId
                });
            }

            _context.Livro.Add(newBook);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetBookDto>(newBook);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetBookDto>> UpdateBook(UpdateBookDto book)
    {
        ServiceResponse<GetBookDto> serviceResponse = new();

        try
        {
            Book dbBook = await _context.Livro.FirstAsync(x => x.Id == book.Id);
            dbBook.Titulo = book.Titulo;
            dbBook.Editora = book.Editora;
            dbBook.Edicao = book.Edicao;
            dbBook.AnoPublicacao = book.AnoPublicacao;

            _context.Livro.Update(dbBook);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetBookDto>(dbBook);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetBookDto>> DeleteBook(int id)
    {
        ServiceResponse<GetBookDto> serviceResponse = new();

        try
        {
            Book dbBook = await _context.Livro.FirstAsync(x => x.Id == id);
            
            if(dbBook != null)
            {
                _context.Livro.Remove(dbBook);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetBookDto>(dbBook);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Book not found.";
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