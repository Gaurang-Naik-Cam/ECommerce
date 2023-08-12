using MyEcommerceAdmin.Controllers;
using MyEcommerceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ecommerce.Test
{
    public class AccountControllerTest
    {
        private AccountController _accountController { get; set; } = null;

        [SetUp]

        public void Setup()
        {
            _accountController = new AccountController();
        }

        [Test]
        public void Index()
        {
            //Assign
            //Since there is no input param for Index method. Param is not assigned.

            //Act
            var result = _accountController.Index();

            //Assert
            Assert.IsTrue(result != null);
        }


        public void Register()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();
            
            Customer testCustomer = new Customer();
            testCustomer.Email = "TestCustomer1@email.com";
            testCustomer.First_Name = "Gaurang";
            testCustomer.Last_Name = "Naik";
            testCustomer.UserName = "TestCustomer1";
            testCustomer.Password = "test1@123";
            testCustomer.City = "Sudbury";
            testCustomer.Country = "Canada";
            testCustomer.Address = "199 Wonderland";
            
            _accountController.Register(testCustomer);

            var result = db.Customers.Where(c => c.UserName.Equals(testCustomer.UserName)).FirstOrDefault();

            Assert.That(result, Is.EqualTo(testCustomer));
        }

        public void Login()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();

            Customer testCustomer = new Customer();
            testCustomer.Email = "TestCustomer2@email.com";
            testCustomer.First_Name = "Gaurang";
            testCustomer.Last_Name = "Naik";
            testCustomer.UserName = "TestCustomer2";
            testCustomer.Password = "test2@123";
            testCustomer.City = "Sudbury";
            testCustomer.Country = "Canada";
            testCustomer.Address = "199 Wonderland";

            _accountController.Register(testCustomer);

            FormCollection formCollection = new FormCollection();
            formCollection["UserName"] = testCustomer.UserName;
            formCollection["Password"] = testCustomer.Password;

            var result = _accountController.Login(formCollection);

            Assert.NotNull(result);
            
        }
    }
}
