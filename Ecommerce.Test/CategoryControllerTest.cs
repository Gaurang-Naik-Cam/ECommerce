using MyEcommerceAdmin.Controllers;
using MyEcommerceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Test
{
    public class CategoryControllerTest
    {
        private CategoryController _categoryController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _categoryController = new CategoryController();
        }

        [Test]
        public void Create()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();

            Category testCategory = new Category();
            testCategory.Name = "TestCategory";
            testCategory.isActive = true;
            testCategory.Description = "This is a test category. Please ignore!";
            testCategory.Products.Add(new Product() { Name = "TestProduct", UnitPrice = 199, ProductAvailable = false, UnitInStock = 0 });
            _categoryController.Create(testCategory);

            var result = db.Categories.Where<Category>(c => c.Name.Equals("TestCategory")).FirstOrDefault();

            Assert.That(testCategory, Is.EqualTo(result));

        }
    }
}
