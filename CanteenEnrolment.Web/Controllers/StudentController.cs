using CanteenEnrolment.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;

namespace CanteenEnrolment.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IHttpClientFactory httpClientFactory, ILogger<StudentController> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _client = httpClientFactory.CreateClient("CanteenAPI");
        }
        // GET: StudentController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("Students");
            var studentList = await response.Content.ReadFromJsonAsync<List<Student>>();
            return View(studentList);
            //return View();
        }

        // GET: StudentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"Students/{id}");
            var studentDetails = await response.Content.ReadFromJsonAsync<Student>();

            if (studentDetails == null)
            {
                return NotFound();
            }
            return View(studentDetails);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student newStudent)
        {
            newStudent.EnrollmentDate = DateTime.Now;
            if (string.IsNullOrEmpty(newStudent.FirstMidName) || string.IsNullOrEmpty(newStudent.LastName))
            {
                ModelState.AddModelError("", "Student First and Last names are required");
                return View(newStudent); // Return to the form with validation errors if necessary.
            }

            var json = JsonConvert.SerializeObject(newStudent);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("Students", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var createdStudent = JsonConvert.DeserializeObject<Student>(result);

                // Redirect back to the home page (assuming you have an 'Index' action in the 'HomeController').
                return RedirectToAction("Index");
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "An error occurred while creating the student.");
                return View(newStudent);
            }
        }

        // GET: StudentController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.GetAsync($"Students/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var studentEdit = JsonConvert.DeserializeObject<Student>(result);
                return View(studentEdit);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student editedStudent)
        {

            if (string.IsNullOrEmpty(editedStudent.FirstMidName) || string.IsNullOrEmpty(editedStudent.LastName))
            {
                ModelState.AddModelError("", "First Name and Last name are required.");
                return View(editedStudent); // Return to the form with validation errors if necessary.
            }

            var json = JsonConvert.SerializeObject(editedStudent);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"Students/{editedStudent.ID}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "An error occurred while editing the blog.");
                return View(editedStudent);
            }
        }


        // GET: StudentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"Students/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
