using IMS_Project.Controllers;
using MyEcommerceAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Test
{
    public class DashboardControllerTest
    {
        private DashboardController _dashBoardController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _dashBoardController = new DashboardController();
        }

        [Test]
        public void Index()
        {
            var result = _dashBoardController.Index();
            Assert.IsNotNull(result);
        }
    }
}
