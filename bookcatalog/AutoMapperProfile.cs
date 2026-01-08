using AutoMapper;
using bookcatalog.Dtos.Author;
using bookcatalog.Dtos.Book;
using bookcatalog.Dtos.Subject;
using bookcatalog.Models;

namespace bookcatalog;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Book, GetBookDto>();
        CreateMap<AddBookDto, Book>()
            .ForMember(dest => dest.LivroAutor, opt => opt.Ignore())
            .ForMember(dest => dest.LivroAssunto, opt => opt.Ignore());

        CreateMap<Author, GetAuthorDto>();
        CreateMap<AddAuthorDto, Author>();

        CreateMap<Subject, GetSubjectDto>();
        CreateMap<AddSubjectDto, Subject>();
    } 
}