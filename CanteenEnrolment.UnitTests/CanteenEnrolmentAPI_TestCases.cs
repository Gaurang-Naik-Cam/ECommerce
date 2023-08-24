using AutoFixture;
using CanteenEnrolment.API.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using CanteenEnrolment.API;
using CanteenEnrolment.API.Controllers;
using CanteenEnrolment.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace CanteenEnrolment.UnitTests
{
    [TestClass]
    public class APIUnitTests
    {
        private Mock<IStudentRepository> _repo;
        private Fixture _fixture;
        private StudentsController _controller;

        public APIUnitTests()
        {
            _fixture = new Fixture();
            _repo = new Mock<IStudentRepository>();
        }


        [TestMethod]
        public async Task GetStudentList_Returns_NotFoundWhenNoStudentExist()
        {
            //Arrange
            _repo.Setup(repo => repo.GetStudents()).ReturnsAsync(new List<Student>());
            _controller = new StudentsController(_repo.Object);


            //Action
            var actionResult = await _controller.GetStudents();

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetStudentList_Throws_Exception()
        {
            //Arrange
            _repo.Setup((repo) => repo.GetStudents()).Throws(new Exception());

            _controller = new StudentsController(_repo.Object);

            //Act
            var result = await _controller.GetStudents();

            //Assert


            Assert.IsNotNull(result);
            var statusCodeResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);

        }

        [TestMethod]
        public async Task GetStudents_Returns_OkWithCorrectNumberOfStudents()
        {
            //Arrange
            var studentList = _fixture.CreateMany<Student>(3).ToList();
            _repo.Setup(repo => repo.GetStudents()).ReturnsAsync(studentList);

            _controller = new  StudentsController(_repo.Object);

            //Action
            var actionResult = await _controller.GetStudents();

            var result = actionResult.Result as OkObjectResult;
            var returnStudents = result?.Value as IEnumerable<Student>;
            Assert.IsNotNull(returnStudents);
            Assert.AreEqual(studentList.Count, returnStudents.Count());
        }

        //GetBlog Test cases
        [TestMethod]
        public async Task GetStudent_Returns_NotFound_WhenStudentDoesNotExist()
        {
            //Arrange
            var nonExisttentId = 1000;
            _repo.Setup(repo => repo.GetStudentById(nonExisttentId)).ReturnsAsync((Student)null);

            _controller = new StudentsController(_repo.Object);

            //Act
            var actionResult = await _controller.GetStudent(nonExisttentId);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetStudent_Returns_OkWithCorrectStudent()
        {
            //Arrange
            var studentId = 7;
            var student = new Student { ID = studentId, FirstMidName = "Norman", LastName = "Laura" };
            _repo.Setup(repo => repo.GetStudentById(studentId)).ReturnsAsync(student);

            _controller = new StudentsController(_repo.Object);

            //Act
            var actionResult = await _controller.GetStudent(studentId);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var result = actionResult.Result as OkObjectResult;
            var returnedStudent = result?.Value as Student;
            Assert.IsNotNull(returnedStudent);
            Assert.AreEqual(student.ID, returnedStudent.ID);
            Assert.AreEqual(student.FirstMidName, returnedStudent.FirstMidName);
            Assert.AreEqual(student.LastName, returnedStudent.LastName);

        }

    }
}
