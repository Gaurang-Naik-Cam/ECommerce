using MyEcommerceAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Test
{
    public class ReportControllerTest
    {

        private ReportsController _reportController { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _reportController = new ReportsController();
        }

        [Test]
        public void StocksReport()
        {
           var result = _reportController.StocksReport();
            Assert.IsTrue(result != null);
        }

        public void CustomersReport()
        {
            var result = _reportController.CustomersReport();
            Assert.IsTrue(result != null);
        }

        public void SalesReport()
        {
            var result = _reportController.SalesReport();
            Assert.IsTrue(result != null);
        }

    }
}
