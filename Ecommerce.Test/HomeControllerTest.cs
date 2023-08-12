using MyEcommerceAdmin.Controllers;
using System.Data.SqlClient;


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
        public void Index()
        {
            //Assign
            //Since there is no input param for Index method. Param is not assigned.

            //Act
           var result = _homeController.Index();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}