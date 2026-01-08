using bookcatalog.Dtos.Author;
using bookcatalog.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace bookcatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    
    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _authorService.GetAllAuthors());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _authorService.GetAuthor(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddAuthorDto author)
    {
        return Ok(await _authorService.AddAuthor(author));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateAuthorDto author)
    {
        var response = await _authorService.UpdateAuthor(author);

        if(response.Data == null)
        {
            return StatusCode(500);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _authorService.DeleteAuthor(id));
    }
}