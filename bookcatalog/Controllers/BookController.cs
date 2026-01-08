using bookcatalog.Dtos.Book;
using bookcatalog.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace bookcatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _bookService.GetAllBooks());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _bookService.GetBook(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBookDto book)
    {
        return Ok(await _bookService.AddBook(book));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBookDto book)
    {
        var response = await _bookService.UpdateBook(book);

        if(response.Data == null)
        {
            return StatusCode(500);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _bookService.DeleteBook(id));
    }
}