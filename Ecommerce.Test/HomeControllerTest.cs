using MyEcommerceAdmin.Controllers;

namespace Ecommerce.Test
{
    public class HomeControllerTest
    {
        private HomeController _homeController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _homeController = new HomeController();
        }

        [Test]
        public void Test1()
        {
            //Assign

            //Act
           var result = _homeController.Index();

            //Assert
            Assert.That(result != null);
        }
    }
}