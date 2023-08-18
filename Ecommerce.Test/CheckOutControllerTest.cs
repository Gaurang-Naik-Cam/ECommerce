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
    public class CheckOutControllerTest
    {
        private CheckOutController _checkoutController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _checkoutController = new CheckOutController();
        }

        [Test]
        public void CheckOut()
        {
            MyEcommerceDbContext db = new MyEcommerceDbContext();

            FormCollection CheckoutDetails = new FormCollection();
            CheckoutDetails["FirstName"] = "Test";
            CheckoutDetails["LastName"] = "Customer";
            CheckoutDetails["Email"] = "TestCustomer1@email.com";
            CheckoutDetails["Mobile"] = "2491232712";
            CheckoutDetails["Address"] = "199 Wonderland" ;
            CheckoutDetails["City"] = "Sudbury";
            CheckoutDetails["PostCode"] ="P3A7Y3";

            _checkoutController.PlaceOrder(CheckoutDetails);

            var result = db.Orders.Where(o => o.Customer.Email.Equals("TestCustomer1@email.com")).FirstOrDefault();

            Assert.That(result.Customer.Email, Is.EqualTo("TestCustomer1@email.com"));
        }
    }
}
