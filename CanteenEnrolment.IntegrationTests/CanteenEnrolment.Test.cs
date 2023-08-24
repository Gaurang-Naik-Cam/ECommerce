using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CanteenEnrolment.IntegrationTests
{
    [TestClass]
    public class TheBlogEngineTests
    {
        private IWebDriver? _webDriver;
        private const string BaseUrl = "https://localhost:7280";

        [TestInitialize]
        public void SetUp()
        {
            //setup
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
        }


        [TestMethod]
        public void TitleShouldContainHomeInIt()
        {
            _webDriver?.Navigate().GoToUrl(BaseUrl);
            Assert.IsTrue(_webDriver?.Title.Contains("Home"));
        }

        [TestMethod]
        public void ReadMoreLink_ShouldNavigateToStudentDetailsPage()
        {
            // Arrange
            _webDriver?.Navigate().GoToUrl(BaseUrl);

            // Act
            var readMoreLink = _webDriver?.FindElement(By.ClassName("mb-3"));
            readMoreLink?.Click();

            // Assert
            Assert.IsTrue(_webDriver?.Url.Contains("/Student/Details/"));
        }

        [TestMethod]
        public void HomePage_ShouldContainStudents()
        {
            // Arrange
            _webDriver?.Navigate().GoToUrl(BaseUrl);

            // Act
            var blogCards = _webDriver?.FindElements(By.ClassName("table"));

            // Assert
            Assert.IsTrue(blogCards is { Count: > 0 });
        }

        [TestCleanup]
        public void TearDown()
        {
            //tear down
            _webDriver?.Quit();
        }
    }
}