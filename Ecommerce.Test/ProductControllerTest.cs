using MyEcommerceAdmin.Controllers;
using MyEcommerceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Test
{
    public class ProductControllerTest
    {
        private ProductController _productController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _productController = new ProductController();
        }

        [Test]
        public void Crate()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();
            ProductVM productVM = new ProductVM();
            productVM.ProductID = 1;
            productVM.Name = "TestProduct";
            productVM.UnitPrice = (decimal)12.99;
            productVM.OldPrice = (decimal)9.99;
            productVM.SupplierID = 2;
            var result = _productController.Create(productVM);

            var actualResult = db.Products.Where(p => p.Name.Equals("TestProduct")).FirstOrDefault();

            Assert.That(actualResult.Name, Is.EqualTo(productVM.Name));
        }

        [Test]
        public void Edit()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();
            ProductVM productVM = new ProductVM();
            productVM.ProductID = 999;
            productVM.Name = "TestProduct1";
            productVM.UnitPrice = (decimal)12.99;
            productVM.OldPrice = (decimal)9.99;
            productVM.SupplierID = 2;
            var result = _productController.Create(productVM);

            ProductVM productVM1 = new ProductVM();
            productVM1.SupplierID = 4;
            productVM1.Name = "TestProduct5";
            _productController.Edit(productVM1);

            var actualResult = db.Products.Where(p => p.Name.Equals("TestProduct5")).FirstOrDefault();


            Assert.That(actualResult.Name, Is.EqualTo(productVM1.Name));
        }
    }
}
