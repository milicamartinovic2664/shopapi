using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ShopApi.Controllers;
using ShopApi.Models;
using ShopApi.Models.DBContext;
using ShopApi.Services;
using ShopApi.Services.Interfaces;

namespace ShopTest
{
    [TestClass]
    public class ShopTest
    {
        private readonly Mock<IJewleryItemService> serviceMock;
        private readonly JewleryItemsController controller;

        public ShopTest(ILogger logger)
        {
            serviceMock = new Mock<IJewleryItemService>();

            controller = new JewleryItemsController(serviceMock.Object, logger);
        }

        [TestMethod()]
        public async Task GetJewleryItems_NotEmptyResults_ReturnsIEnumerable()
        {
            serviceMock.Setup(p => p.GetJewleryItems())
                .ReturnsAsync(GetJewleryItems());

            var result = await controller.GetJewleryItems();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;

            Assert.IsInstanceOfType(list?.Value, typeof(IEnumerable<JewleryItem>));

            var listJewleryItems = list?.Value as IEnumerable<JewleryItem>;

            Assert.AreEqual(2, listJewleryItems?.Count() ?? 0);
        }

        [TestMethod()]
        public async Task GetJewleryItem_EmptyResults_ReturnsNotFound()
        {
            serviceMock.Setup(p => 
                p.GetJewleryItems())
                    .ReturnsAsync(Enumerable.Empty<JewleryItem>);

            var result = await controller.GetJewleryItems();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

            var resultValue = result.Result as NotFoundResult;

            Assert.AreEqual(404, resultValue?.StatusCode ?? 0);
        }

        [TestMethod()]
        public async Task GetJewleryItemById_OkResult_ReturnsJewleryItem()
        {
            long jewleryItemId = 0;

            serviceMock.Setup(
                p => p.GetJewleryItemById(jewleryItemId))
                    .ReturnsAsync(Mock.Of<JewleryItem>());

            var result = await controller.GetJewleryItem(jewleryItemId);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var resultValue = result.Result as OkObjectResult;

            Assert.IsInstanceOfType(resultValue?.Value, typeof(JewleryItem));

            var item = resultValue?.Value as JewleryItem;

            Assert.AreEqual(jewleryItemId, item?.Id);
        }

        [TestMethod()]
        public async Task GetJewleryItemById_EmptyResult_ReturnsNotFound()
        {
            long jewleryItemId = 3;

            serviceMock.Setup(
                p => p.GetJewleryItemById(jewleryItemId))
                    .ReturnsAsync(() => null);

            var result = await controller.GetJewleryItem(jewleryItemId);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

            var resultValue = result.Result as NotFoundResult;

            Assert.AreEqual(404, resultValue?.StatusCode ?? 0);
        }

        [TestMethod()]
        public async Task CreateJewleryItem_Success_CreatedAtActionResult()
        {
            var jewleryItem = Mock.Of<JewleryItem>();

            serviceMock.Setup(
                p => p.CreateJewleryItem(jewleryItem))
                    .Returns(Task.CompletedTask);

            var result = await controller.PostJewleryItem(jewleryItem);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod()]
        public async Task PutJewleryItem_Success_OkObjectResult()
        {
            long itemId = 0;

            var jewleryItem = Mock.Of<JewleryItem>();

            serviceMock.Setup(
                p => p.UpdateJewleryItem(itemId, jewleryItem))
                    .ReturnsAsync(jewleryItem);

            var result = await controller.PutJewleryItem(itemId, jewleryItem);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod()]
        public async Task PutJewleryItem_Fail_BadRequestResult()
        {
            long itemId = 1;

            var jewleryItem = Mock.Of<JewleryItem>();

            serviceMock.Setup(
                p => p.UpdateJewleryItem(itemId, jewleryItem))
                    .ReturnsAsync(jewleryItem);

            var result = await controller.PutJewleryItem(2, jewleryItem);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task DeleteJewleryItem_Success_OkObjectResult()
        {
            long itemId = 1;

            var jewleryItem = Mock.Of<JewleryItem>();

            serviceMock.Setup(
                p => p.DeleteJewleryItem(itemId))
                    .Returns(Task.CompletedTask);
            serviceMock.Setup(
                p => p.GetJewleryItemById(itemId))
                    .ReturnsAsync(jewleryItem);

            var result = await controller.DeleteJewleryItem(itemId);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        [TestMethod()]
        public async Task DeleteJewleryItem_Fail_NotFoundResult()
        {
            long itemId = 1;

            var jewleryItem = Mock.Of<JewleryItem>();

            serviceMock.Setup(
                p => p.DeleteJewleryItem(itemId))
                    .Returns(Task.CompletedTask);
            serviceMock.Setup(
                p => p.GetJewleryItemById(itemId))
                    .ReturnsAsync(() => null);

            var result = await controller.DeleteJewleryItem(1);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        private IEnumerable<JewleryItem> GetJewleryItems()
        {
            return new List<JewleryItem>()
            {
                new JewleryItem() { Id = 1, Name = "item1", Price = 100, Type = "T1", Manufacturer = new Manufacturer() { Id = 1, Country = "srb", Name = "m1" }, ManufacturerId = 1 },
                new JewleryItem() { Id = 2, Name = "item2", Price = 102, Type = "T2", Manufacturer = new Manufacturer() { Id = 1, Country = "srb", Name = "m1" }, ManufacturerId = 1 },
            };
        }
    }
}