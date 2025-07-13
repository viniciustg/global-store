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
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _controller = new CompanyController(_companyServiceMock.Object);
        }

        [Fact]
        public async Task GivenCompaniesExist_WhenGetAllIsCalled_ThenReturnsOkWithList()
        {
            // Arrange
            var companies = new List<Company>
            {
                new() { Id = Guid.NewGuid(), Name = "Company A" },
                new() { Id = Guid.NewGuid(), Name = "Company B" }
            };

            _companyServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(companies);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<IEnumerable<CompanyResponse>>();
        }

        [Fact]
        public async Task GivenExistingCompany_WhenGetByIdIsCalled_ThenReturnsOkWithCompany()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var company = new Company { Id = companyId, Name = "Test Company" };

            _companyServiceMock.Setup(s => s.GetByIdAsync(companyId)).ReturnsAsync(company);

            // Act
            var result = await _controller.GetById(companyId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<CompanyResponse>();
        }

        [Fact]
        public async Task GivenNonExistingCompany_WhenGetByIdIsCalled_ThenReturnsNotFound()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            _companyServiceMock.Setup(s => s.GetByIdAsync(companyId)).ReturnsAsync((Company?)null);

            // Act
            var result = await _controller.GetById(companyId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GivenValidRequest_WhenCreateIsCalled_ThenReturnsCreatedAtAction()
        {
            // Arrange
            var request = new CompanyRequest { Name = "New Company" };
            var created = new Company { Id = Guid.NewGuid(), Name = request.Name };

            _companyServiceMock.Setup(s => s.AddAsync(It.IsAny<Company>()))
                               .ReturnsAsync(created);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.Value.Should().BeAssignableTo<CompanyResponse>();
        }

        [Fact]
        public async Task GivenValidRequest_WhenUpdateIsCalled_ThenReturnsOkWithUpdatedCompany()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var request = new CompanyRequest { Name = "Updated Name" };
            var updated = new Company { Id = companyId, Name = request.Name };

            _companyServiceMock.Setup(s => s.UpdateAsync(It.IsAny<Company>()))
                               .ReturnsAsync(updated);

            // Act
            var result = await _controller.Update(companyId, request);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeAssignableTo<CompanyResponse>();
        }

        [Fact]
        public async Task GivenExistingCompanyId_WhenDeleteIsCalled_ThenReturnsNoContent()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            _companyServiceMock.Setup(s => s.DeleteAsync(companyId))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(companyId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
