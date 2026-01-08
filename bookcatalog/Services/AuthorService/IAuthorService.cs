using bookcatalog.Dtos.Author;

namespace bookcatalog.Services.AuthorService;

public interface IAuthorService
{
    Task<ServiceResponse<List<GetAuthorDto>>> GetAllAuthors();
    Task<ServiceResponse<GetAuthorDto>> GetAuthor(int id);
    Task<ServiceResponse<GetAuthorDto>> AddAuthor(AddAuthorDto author);
    Task<ServiceResponse<GetAuthorDto>> UpdateAuthor(UpdateAuthorDto author);
    Task<ServiceResponse<GetAuthorDto>> DeleteAuthor(int id);
}