using FluentAssertions;
using GlobalStore.Application.Services;
using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces.Repositories;
using GlobalStore.Domain.Interfaces.Services;
using Moq;

namespace GlobalStore.Tests.Services
{
    public class StoreServiceTests
    {
        private readonly IStoreService _service;
        private readonly Mock<IStoreRepository> _repositoryMock;

        public StoreServiceTests()
        {
            _repositoryMock = new Mock<IStoreRepository>();
            _service = new StoreService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GivenValidCompanyId_WhenGetStoresAsync_ThenReturnsStoresList()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var stores = new List<Store>
            {
                new() { Id = Guid.NewGuid(), Name = "Store A", CompanyId = companyId },
                new() { Id = Guid.NewGuid(), Name = "Store B", CompanyId = companyId }
            };

            _repositoryMock.Setup(r => r.GetAllAsync(companyId))
                           .ReturnsAsync(stores);

            // Act
            var result = await _service.GetStoresAsync(companyId);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(s => s.Name == "Store A");
            result.Should().Contain(s => s.Name == "Store B");
        }

        [Fact]
        public async Task GivenValidStore_WhenCreateStoreAsync_ThenReturnsCreatedStore()
        {
            // Arrange
            var store = new Store { Id = Guid.NewGuid(), Name = "New Store", CompanyId = Guid.NewGuid() };

            _repositoryMock.Setup(r => r.AddAsync(store))
                           .ReturnsAsync(store);

            // Act
            var result = await _service.CreateAsync(store);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("New Store");
        }

        [Fact]
        public async Task GivenValidIds_WhenGetStoreAsync_ThenReturnsStore()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var storeId = Guid.NewGuid();
            var store = new Store { Id = storeId, Name = "Store", CompanyId = companyId };

            _repositoryMock.Setup(r => r.GetByIdAsync(companyId, storeId))
                           .ReturnsAsync(store);

            // Act
            var result = await _service.GetStoreAsync(companyId, storeId);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Store");
        }

        [Fact]
        public async Task GivenStore_WhenUpdateStoreAsync_ThenReturnsUpdatedStore()
        {
            // Arrange
            var store = new Store { Id = Guid.NewGuid(), Name = "Store Updated", CompanyId = Guid.NewGuid() };

            _repositoryMock.Setup(r => r.UpdateAsync(store))
                           .ReturnsAsync(store);

            // Act
            var result = await _service.UpdateAsync(store);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Store Updated");
        }

        [Fact]
        public async Task GivenValidIds_WhenDeleteStoreAsync_ThenCallsRepositoryDelete()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var storeId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.DeleteAsync(companyId, storeId))
                           .Returns(Task.CompletedTask)
                           .Verifiable();

            // Act
            await _service.DeleteAsync(companyId, storeId);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(companyId, storeId), Times.Once);
        }
    }
}
