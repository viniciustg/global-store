using GlobalStore.Api.Mappers;
using GlobalStore.Api.Requests;
using GlobalStore.Api.Responses;
using GlobalStore.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalStore.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    [Produces("application/json")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Get all companies.
        /// </summary>
        /// <returns>List of companies.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllAsync();
            var result = companies.Select(c => c.ToResponse());

            return Ok(result);
        }

        /// <summary>
        /// Get a company by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <returns>The company with the given ID.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _companyService.GetByIdAsync(id);
            
            if (company is null) 
                return NotFound();

            return Ok(company.ToResponse());
        }

        /// <summary>
        /// Create a new company.
        /// </summary>
        /// <param name="request">Company creation request.</param>
        /// <returns>The newly created company.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CompanyRequest request)
        {
            var created = await _companyService.AddAsync(request.ToEntity());

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }

        /// <summary>
        /// Update an existing company.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="request">Updated company data.</param>
        /// <returns>The updated company.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CompanyRequest request)
        {
            var updated = await _companyService.UpdateAsync(request.ToEntity(id));

            return Ok(updated.ToResponse());
        }

        /// <summary>
        /// Delete a company by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
