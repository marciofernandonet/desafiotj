using bookcatalog.Dtos.Subject;

namespace bookcatalog.Services.SubjectService;

public interface ISubjectService
{
    Task<ServiceResponse<List<GetSubjectDto>>> GetAllSubjects();
    Task<ServiceResponse<GetSubjectDto>> GetSubject(int id);
    Task<ServiceResponse<GetSubjectDto>> AddSubject(AddSubjectDto subject);
    Task<ServiceResponse<GetSubjectDto>> UpdateSubject(UpdateSubjectDto subject);
    Task<ServiceResponse<GetSubjectDto>> DeleteSubject(int id);
}