using MyEcommerceAdmin.Controllers;
using MyEcommerceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Test
{
    public class CustomerControllerTest
    {
        private CustomerController _customerController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void Crate()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();
            CustomerVM customerVM = new CustomerVM();
            customerVM.First_Name = "Test";
            customerVM.Last_Name = "Customer";
            customerVM.UserName = "TestCustomer3";
            customerVM.Email = "TestCustomer3@email.com";
            customerVM.Address = "199 Wonderland";
            customerVM.City = "Sudbury";
            customerVM.Country = "Canada";
            customerVM.Gender = "M";
            customerVM.UserName = "TestCustomer3";
            customerVM.Password = "Test@123";

           var result = _customerController.Create(customerVM);

           var actualResult = db.Customers.Where(c => c.Email.Equals("TestCustomer3@email.com")).FirstOrDefault();

            Assert.That(actualResult.Email, Is.EqualTo(customerVM.Email));
        }
    }
}
