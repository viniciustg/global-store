using FluentAssertions;
using GlobalStore.Application.Services;
using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces.Repositories;
using Moq;

namespace GlobalStore.Tests.Services
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _repositoryMock;
        private readonly CompanyService _service;

        public CompanyServiceTests()
        {
            _repositoryMock = new Mock<ICompanyRepository>();
            _service = new CompanyService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GivenCompaniesExist_WhenGetAllAsyncIsCalled_ThenReturnsAllCompanies()
        {
            // Arrange
            var companies = new List<Company>
        {
            new() { Id = Guid.NewGuid(), Name = "Company A" },
            new() { Id = Guid.NewGuid(), Name = "Company B" }
        };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(companies);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GivenExistingCompany_WhenGetByIdAsyncIsCalled_ThenReturnsCompany()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var company = new Company { Id = companyId, Name = "Test Company" };
            _repositoryMock.Setup(r => r.GetByIdAsync(companyId)).ReturnsAsync(company);

            // Act
            var result = await _service.GetByIdAsync(companyId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(companyId);
            result.Name.Should().Be("Test Company");
        }

        [Fact]
        public async Task GivenValidCompany_WhenAddAsyncIsCalled_ThenReturnsCreatedCompany()
        {
            // Arrange
            var company = new Company { Id = Guid.NewGuid(), Name = "New Company" };
            _repositoryMock.Setup(r => r.AddAsync(company)).ReturnsAsync(company);

            // Act
            var result = await _service.AddAsync(company);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("New Company");
        }

        [Fact]
        public async Task GivenValidCompany_WhenUpdateAsyncIsCalled_ThenReturnsUpdatedCompany()
        {
            // Arrange
            var company = new Company { Id = Guid.NewGuid(), Name = "Updated" };
            _repositoryMock.Setup(r => r.UpdateAsync(company)).ReturnsAsync(company);

            // Act
            var result = await _service.UpdateAsync(company);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Updated");
        }

        [Fact]
        public async Task GivenExistingCompanyId_WhenDeleteAsyncIsCalled_ThenRepositoryDeleteIsCalled()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.DeleteAsync(companyId))
                           .Returns(Task.CompletedTask)
                           .Verifiable();

            // Act
            await _service.DeleteAsync(companyId);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(companyId), Times.Once);
        }
    }
}
