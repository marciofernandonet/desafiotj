using bookcatalog.Dtos.Book;

namespace bookcatalog.Services.BookService;

public interface IBookService
{
    Task<ServiceResponse<List<GetBookDto>>> GetAllBooks();
    Task<ServiceResponse<GetBookDto>> GetBook(int id);
    Task<ServiceResponse<GetBookDto>> AddBook(AddBookDto book);
    Task<ServiceResponse<GetBookDto>> UpdateBook(UpdateBookDto book);
    Task<ServiceResponse<GetBookDto>> DeleteBook(int id);
}