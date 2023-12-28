using img_api.DTOs.Student;
using img_api.Interfaces;
using img_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace img_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }



        //GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudent()
        {
            var students = await _studentRepository.GetAllStudentsAsync();

            if (students != null)
            {
                return students;
            }
            else { return Problem("Entity set 'ApplicationDbContext.Students'  is null."); }
        }



        //GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var DesiredStudentDto = await _studentRepository.GetStudentByIdAsync(id);

            if (DesiredStudentDto == null)
            {
                return NotFound();
            }

            return DesiredStudentDto;
        }



        //POST: api/Student
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> PostStudent([FromForm]StudentDTO studentDto)
        {
            var locatedID = await _studentRepository.Save(studentDto);

            if (locatedID != 0)
            {
                return CreatedAtAction("GetStudent", new { id = studentDto.Id }, studentDto);
            }
            else
            {
                //return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
                return Problem();
            }
        }



        //PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(StudentDTO studentDto)
        {
            int locatedID = await _studentRepository.StudentUpdateAsync(studentDto);

            if (locatedID == 0)
            {
                return NotFound();
            }

            return Ok();
        }



        //DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentRepository.DeleteStudentAsync(id);
            if (success == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }

            return Ok();
        }
    }
}
