using bookcatalog.Dtos.Subject;
using bookcatalog.Services.SubjectService;
using Microsoft.AspNetCore.Mvc;

namespace bookcatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _subjectService.GetAllSubjects());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _subjectService.GetSubject(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddSubjectDto subject)
    {
        return Ok(await _subjectService.AddSubject(subject));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateSubjectDto subject)
    {
        var response = await _subjectService.UpdateSubject(subject);

        if(response.Data == null)
        {
            return StatusCode(500);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _subjectService.DeleteSubject(id));
    }
}