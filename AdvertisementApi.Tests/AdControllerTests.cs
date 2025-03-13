using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using advertisement_API.Controllers;

namespace AdvertisementApi.Tests
{
    [TestClass]
    public class AdPlatformControllerTests
    {
        private AdPlatformController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new AdPlatformController();
        }

        [TestMethod]
        public void LoadAdPlatforms_ValidFilePath_ReturnsOk()
        {
            string validFilePath = ""; // Подставьте верный путь
            var result = _controller.LoadAdPlatforms(validFilePath) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Data loaded", result.Value);
        }

        [TestMethod]
        public void LoadAdPlatforms_InvalidFilePath_ReturnsBadRequest()
        {
            string invalidFilePath = ""; // Подставьте неверный путь
            var result = _controller.LoadAdPlatforms(invalidFilePath) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.ToString().StartsWith("Error:"));
        }

        [TestMethod]
        public void SearchPlatforms_ValidLocation_ReturnsOk()
        {
            string location = "/ru/svrd/revda"; // Подставьте местоположение
            var result = _controller.SearchPlatforms(location) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<AdPlatform>));
        }

        [TestMethod]
        public void SearchPlatforms_InvalidLocation_ReturnsOk()
        {
            string location = "ru"; // Подставьте неверное местоположение
            var result = _controller.SearchPlatforms(location) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<AdPlatform>));
        }
    }
}