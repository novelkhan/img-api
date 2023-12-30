using img_api.Data;
using img_api.DTOs.Student;
using img_api.Interfaces;
using img_api.Models;
using Microsoft.EntityFrameworkCore;

namespace img_api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<List<StudentDTO>?> GetAllStudentsAsync()
        public async Task<List<ResponseDTO>?> GetAllStudentsAsync()
        {

            //#pragma warning disable CS8602 // Dereference of a possibly null reference.
            //            return await _context.Students?
            //                  .Select(student => new StudentDTO()
            //                  {
            //                      Id = student.StudentId,
            //                      Name = student.Name,
            //                      Roll = student.Roll,
            //                      Image = BytesArrayToIFormFile(student.Photo)
            //                  }).ToListAsync();
            //#pragma warning restore CS8602 // Dereference of a possibly null reference.


            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await _context.Students?.Select(student => new ResponseDTO()
                                            {
                                                Id = student.StudentId,
                                                Name = student.Name,
                                                Roll = student.Roll,
                                                Image = student.Photo
                                            }).ToListAsync();
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public async Task<int> Save(StudentDTO studentDto)
        {

            if (studentDto.Image.Length > 0)
            {
                Student student = new Student()
                {
                    Name = studentDto.Name,
                    Roll = studentDto.Roll,
                    Photo = IFormFileToBytesArray(studentDto.Image)
                };


                try
                {
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return 0;
                }
                return student.StudentId;
            }


            return 0;
        }




        //public async Task<StudentDTO?> GetStudentByIdAsync(int? id)
        public async Task<ResponseDTO?> GetStudentByIdAsync(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return null;
            }

            //return await _context.Students.Where(x => x.StudentId == id)
            //    .Select(student => new StudentDTO()
            //    {
            //        Id = student.StudentId,
            //        Name = student.Name,
            //        Roll = student.Roll,
            //        Image = BytesArrayToIFormFile(student.Photo)
            //    }).FirstOrDefaultAsync();


            return await _context.Students.Where(x => x.StudentId == id)
                .Select(student => new ResponseDTO()
                {
                    Id = student.StudentId,
                    Name = student.Name,
                    Roll = student.Roll,
                    Image = student.Photo
                }).FirstOrDefaultAsync();
        }




        public async Task<int> StudentUpdateAsync(StudentDTO studentDto)
        {
            Student student = new Student()
            {
                StudentId = studentDto.Id,
                Name = studentDto.Name,
                Roll = studentDto.Roll,
            };

            if (studentDto.Image != null)
            {
                student.Photo = IFormFileToBytesArray(studentDto.Image);
            }
            else
            {
                student.Photo = (await _context.Students.AsNoTracking().SingleOrDefaultAsync(i => i.StudentId == studentDto.Id)).Photo;
            }


            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return 0;
            }
            return student.StudentId;
        }





        public async Task<int?> DeleteStudentAsync(int? id)
        {
            if (_context.Students == null)
            {
                return null;
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                try
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                }
                catch (Exception) { return null; }
            }



            return 1;
        }




        public static byte[] IFormFileToBytesArray(IFormFile ImageIFormFile)
        {
            var ms = new MemoryStream();
            ImageIFormFile.CopyTo(ms);
            var BytesPhoto = ms.ToArray();

            return BytesPhoto;
        }


        public static IFormFile BytesArrayToIFormFile(byte[] BytesPhoto)
        {
            var stream = new MemoryStream(BytesPhoto);
            IFormFile ImageIFormFile = new FormFile(stream, 0, (BytesPhoto).Length, "name", "fileName");
            return ImageIFormFile;
        }

    }
}
