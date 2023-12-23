using img_api.DTOs.Student;
using img_api.Models;

namespace img_api.Interfaces
{
    public interface IStudentRepository
    {
        Task<int> Save(StudentDTO studentDto);
        Task<List<StudentDTO>?> GetAllStudentsAsync();
        Task<StudentDTO?> GetStudentByIdAsync(int? id);
        Task<int> StudentUpdateAsync(StudentDTO studentDto);
        Task<int?> DeleteStudentAsync(int? id);
    }
}
