using CanteenEnrolment.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Threading;

namespace CanteenEnrolment.API.Data
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student?> GetStudentById(int id);
        Task<Student?> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task DeleteStudent(int id);
        Task<bool> StudentExists(int id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _dbContext;

        public StudentRepository(SchoolContext DbContext)
        {
            this._dbContext = DbContext;
        }

        public async Task<IEnumerable<Student?>> GetStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(s => s != null && s.ID == id);
        }

        public async Task<Student?> AddStudent(Student student)
        {
            var result = await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            var result =  await _dbContext.Students.FirstOrDefaultAsync(s => s.ID == student.ID);

            if (result != null)
            {
                result.FirstMidName = student.FirstMidName;
                result.LastName = student.LastName;
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task DeleteStudent(int id)
        {
            var result = await _dbContext.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (result != null)
            {
                _dbContext.Students.Remove(result);
               await  _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> StudentExists(int id)
        {
            return await _dbContext.Students.AnyAsync(s => s.ID == id);
        }
    }
}