using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CanteenEnrolment.API.Data;
using CanteenEnrolment.Common;
using System.Reflection.Metadata;

namespace CanteenEnrolment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repo;
        //private readonly SchoolContext _context;

        //public StudentsController(SchoolContext context)
        //{
        //    _context = context;
        //}

        public StudentsController(IStudentRepository Repo)
        {
            this._repo = Repo;
        }


        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                var result = await _repo.GetStudents();
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(); //Return NotFoundResult if no blogs are found
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await _repo.GetStudentById(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> PutStudent(int id, Student student)
        {
            try
            {
                if (id != student.ID)
                {
                    return BadRequest("Student ID mismatch.");
                }

                var studentExists = await _repo.StudentExists(id);
                if (!studentExists)
                {
                    return NotFound($"Student with Id {id} not found");
                }
                return await _repo.UpdateStudent(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating student data");
            }
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return Problem("Entity set 'StudentDbContext.Students' is null.");
                }

                var createdBlog = await _repo.AddStudent(student);

                return CreatedAtAction("GetStudents", new { id = student.ID }, student);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating  new employee.");
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var studentExists = await _repo.StudentExists(id);
                if (!studentExists)
                    return NotFound($"Student with Id {id} not found");
                await _repo.DeleteStudent(id);
                return NoContent();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Student");
            }
        }

        //private bool StudentExists(int id)
        //{
        //    return (_context.Students?.Any(e => e.ID == id)).GetValueOrDefault();
        //}
    }
}
