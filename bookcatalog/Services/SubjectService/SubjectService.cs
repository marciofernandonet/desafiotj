using AutoMapper;
using bookcatalog.Data;
using bookcatalog.Dtos.Subject;
using bookcatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace bookcatalog.Services.SubjectService;

public class SubjectService : ISubjectService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    public SubjectService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetSubjectDto>>> GetAllSubjects()
    {
        ServiceResponse<List<GetSubjectDto>> serviceResponse = new();

        try
        {
            List<Subject> dbSubjects = await _context.Assunto.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetSubjectDto>>(dbSubjects);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetSubjectDto>> GetSubject(int id)
    {
        ServiceResponse<GetSubjectDto> serviceResponse = new();

        try
        {
            Subject dbSubject = await _context.Assunto.FirstAsync(x => x.Id == id);
            
            if(dbSubject != null)
            {
                serviceResponse.Data = _mapper.Map<GetSubjectDto>(dbSubject);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Subject not found.";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetSubjectDto>> AddSubject(AddSubjectDto subject)
    {
        ServiceResponse<GetSubjectDto> serviceResponse = new();

        try
        {
            Subject newSubject = _mapper.Map<Subject>(subject);

            await _context.Assunto.AddAsync(newSubject);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetSubjectDto>(newSubject);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetSubjectDto>> UpdateSubject(UpdateSubjectDto subject)
    {
        ServiceResponse<GetSubjectDto> serviceResponse = new();

        try
        {
            Subject dbSubject = await _context.Assunto.FirstAsync(x => x.Id == subject.Id);
            dbSubject.Descricao = subject.Descricao;

            _context.Assunto.Update(dbSubject);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetSubjectDto>(dbSubject);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetSubjectDto>> DeleteSubject(int id)
    {
        ServiceResponse<GetSubjectDto> serviceResponse = new();

        try
        {
            Subject dbSubject = await _context.Assunto.FirstAsync(x => x.Id == id);
            
            if(dbSubject != null)
            {
                _context.Assunto.Remove(dbSubject);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetSubjectDto>(dbSubject);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Subject not found.";
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