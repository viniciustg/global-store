using FluentAssertions;
using GlobalStore.Api.Controllers;
using GlobalStore.Api.Requests;
using GlobalStore.Api.Responses;
using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GlobalStore.Tests.Controllers
{
    public class StoreControllerTests
    {
        private readonly Mock<IStoreService> _storeServiceMock;
        private readonly StoreController _controller;

        public StoreControllerTests()
        {
            _storeServiceMock = new Mock<IStoreService>();
            _controller = new StoreController(_storeServiceMock.Object);
        }

        [Fact]
        public async Task GivenExistingCompany_WhenGetStoresIsCalled_ThenReturnsOkWithStores()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var stores = new List<Store>
            {
                new() { Id = Guid.NewGuid(), Name = "Store 1", Address = "Address 1", CompanyId = companyId },
                new() { Id = Guid.NewGuid(), Name = "Store 2", Address = "Address 2", CompanyId = companyId }
            };
            _storeServiceMock.Setup(s => s.GetStoresAsync(companyId)).ReturnsAsync(stores);

            // Act
            var result = await _controller.GetStores(companyId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<IEnumerable<StoreResponse>>();
        }

        [Fact]
        public async Task GivenExistingStore_WhenGetStoreIsCalled_ThenReturnsOkWithStore()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var storeId = Guid.NewGuid();
            var store = new Store { Id = storeId, Name = "Test Store", Address = "Test Address", CompanyId = companyId };

            _storeServiceMock.Setup(s => s.GetStoreAsync(companyId, storeId)).ReturnsAsync(store);

            // Act
            var result = await _controller.GetStore(companyId, storeId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<StoreResponse>();
        }

        [Fact]
        public async Task GivenNonExistentStore_WhenGetStoreIsCalled_ThenReturnsNotFound()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var storeId = Guid.NewGuid();
            _storeServiceMock.Setup(s => s.GetStoreAsync(companyId, storeId)).ReturnsAsync((Store?)null);

            // Act
            var result = await _controller.GetStore(companyId, storeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GivenValidRequest_WhenCreateStoreIsCalled_ThenReturnsCreatedResult()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var request = new StoreRequest { Name = "New Store", Address = "New Address" };
            var createdStore = new Store
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                CompanyId = companyId
            };

            _storeServiceMock.Setup(s => s.CreateAsync(It.IsAny<Store>()))
                             .ReturnsAsync(createdStore);

            // Act
            var result = await _controller.CreateStore(companyId, request);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.ActionName.Should().Be(nameof(_controller.GetStore));
            createdResult.Value.Should().BeAssignableTo<StoreResponse>();
        }

        [Fact]
        public async Task GivenValidIds_WhenDeleteStoreIsCalled_ThenReturnsNoContent()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var storeId = Guid.NewGuid();

            _storeServiceMock.Setup(s => s.DeleteAsync(companyId, storeId))
                             .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteStore(companyId, storeId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
