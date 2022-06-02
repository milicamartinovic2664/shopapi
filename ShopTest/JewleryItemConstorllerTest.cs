using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ShopTest() 
        {
        }

        [TestMethod()]
        public async Task JewleryItemGetAllSuccess()
        {
            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(p => p.GetJewleryItems()).ReturnsAsync(GetJewleryItems);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.GetJewleryItems();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;

            Assert.IsInstanceOfType(list?.Value, typeof(IEnumerable<JewleryItem>));

            var listJewleryItems = list?.Value as IEnumerable<JewleryItem>;

            Assert.AreEqual(2, listJewleryItems?.Count() ?? 0);
            
        }

        [TestMethod()]
        public async Task JewleryItemGetAllNotFound()
        {
            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(p => p.GetJewleryItems()).ReturnsAsync(Enumerable.Empty<JewleryItem>);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.GetJewleryItems();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

            var resultValue = result.Result as NotFoundResult;

            Assert.AreEqual(404, resultValue?.StatusCode ?? 0);
        }

        [TestMethod()]
        public async Task JewleryItemGetByIdSuccess()
        {
            long jewleryItemId = 1;

            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.GetJewleryItemById(jewleryItemId))
                    .ReturnsAsync(
                        GetJewleryItemById(jewleryItemId));

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.GetJewleryItem(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var resultValue = result.Result as OkObjectResult;

            Assert.IsInstanceOfType(resultValue?.Value, typeof(JewleryItem));

            var item = resultValue?.Value as JewleryItem;

            Assert.AreEqual(jewleryItemId, item?.Id ?? 0);
        }

        [TestMethod()]
        public async Task JewleryItemGetByIdNotFound()
        {
            long jewleryItemId = 3;
            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.GetJewleryItemById(jewleryItemId))
                    .ReturnsAsync(() => null);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.GetJewleryItem(jewleryItemId);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

            var resultValue = result.Result as NotFoundResult;

            Assert.AreEqual(404, resultValue?.StatusCode ?? 0);
        }

        [TestMethod()]
        public async Task PostJewleryItemSuccess()
        {
            var jewleryItem = GetJewleryItemById(1);

            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.CreateJewleryItem(jewleryItem))
                    .Returns(Task.CompletedTask);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.PostJewleryItem(jewleryItem);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod()]
        public async Task PutJewleryItemSuccess()
        {
            long itemId = 1;

            var jewleryItem = GetJewleryItemById(itemId);

            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.UpdateJewleryItem(itemId, jewleryItem))
                    .ReturnsAsync(jewleryItem);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.PutJewleryItem(itemId, jewleryItem);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod()]
        public async Task PutJewleryItemFail()
        {
            long itemId = 1;

            var jewleryItem = GetJewleryItemById(itemId);

            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.UpdateJewleryItem(itemId, jewleryItem))
                    .ReturnsAsync(jewleryItem);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.PutJewleryItem(2, jewleryItem);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task DeleteJewleryItemSuccess()
        {
            long itemId = 1;

            var jewleryItem = GetJewleryItemById(itemId);

            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.DeleteJewleryItem(itemId))
                    .Returns(Task.CompletedTask);
            serviceMock.Setup(
                p => p.GetJewleryItemById(itemId))
                    .ReturnsAsync(jewleryItem);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.DeleteJewleryItem(1);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        [TestMethod()]
        public async Task DeleteJewleryItemNotFound()
        {
            long itemId = 1;

            var jewleryItem = GetJewleryItemById(itemId);

            var serviceMock = new Mock<IJewleryItemService>();

            serviceMock.Setup(
                p => p.DeleteJewleryItem(itemId))
                    .Returns(Task.CompletedTask);
            serviceMock.Setup(
                p => p.GetJewleryItemById(itemId))
                    .ReturnsAsync(() => null);

            var controller = new JewleryItemsController(serviceMock.Object);

            var result = await controller.DeleteJewleryItem(1);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        private JewleryItem GetJewleryItemById(long id)
            => GetJewleryItems().First(item => item.Id == id);

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