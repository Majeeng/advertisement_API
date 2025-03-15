using Microsoft.Extensions.Logging;
using advertisement_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Moq;

namespace AdvertisementApiTests
{
    [TestClass]
    public sealed class Ad_Controller_Tests
    {
        private AdPlatformController _controller;
        private Mock<ILogger<AdPlatformController>> _loggerMock;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<AdPlatformController>>();
            _controller = new AdPlatformController(_loggerMock.Object);
        }

        [TestMethod]
        public void LoadAdPlatforms_ValidFilePath_ReturnsOk()
        {
            string validFilePath = "/AdPlatforms.txt"; // Подставьте верный путь
            var result = _controller.LoadAdPlatforms(validFilePath) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Data loaded", result.Value);
        }

        [TestMethod]
        public void LoadAdPlatforms_InvalidFilePath_ReturnsBadRequest()
        {
            string invalidFilePath = "tforms.txt"; // Подставьте неверный путь
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