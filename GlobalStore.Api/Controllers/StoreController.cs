using GlobalStore.Api.Requests;
using GlobalStore.Api.Responses;
using GlobalStore.Domain.Entities;
using GlobalStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalStore.Api.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId:guid}/stores")]
    [Produces("application/json")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        /// <summary>
        /// Get all stores for a specific company.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStores(Guid companyId)
        {
            var stores = await _storeService.GetStoresAsync(companyId);
            var response = stores.Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                CompanyId = s.CompanyId
            });

            return Ok(response);
        }

        /// <summary>
        /// Get a specific store by its ID.
        /// </summary>
        [HttpGet("{storeId:guid}")]
        [ProducesResponseType(typeof(StoreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStore(Guid companyId, Guid storeId)
        {
            var store = await _storeService.GetStoreAsync(companyId, storeId);
            if (store is null) return NotFound();

            var response = new StoreResponse
            {
                Id = store.Id,
                Name = store.Name,
                Address = store.Address,
                CompanyId = store.CompanyId
            };

            return Ok(response);
        }

        /// <summary>
        /// Create a new store under a specific company.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(StoreResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStore(Guid companyId, [FromBody] StoreRequest request)
        {
            var store = new Store
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                CompanyId = companyId
            };

            var created = await _storeService.CreateAsync(store);

            var response = new StoreResponse
            {
                Id = created.Id,
                Name = created.Name,
                Address = created.Address,
                CompanyId = created.CompanyId
            };

            return CreatedAtAction(nameof(GetStore), new { companyId, storeId = response.Id }, response);
        }

        /// <summary>
        /// Update a store's information.
        /// </summary>
        [HttpPut("{storeId:guid}")]
        [ProducesResponseType(typeof(StoreResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStore(Guid companyId, Guid storeId, [FromBody] StoreRequest request)
        {
            var store = new Store
            {
                Id = storeId,
                Name = request.Name,
                Address = request.Address,
                CompanyId = companyId
            };

            var updated = await _storeService.UpdateAsync(store);

            var response = new StoreResponse
            {
                Id = updated.Id,
                Name = updated.Name,
                Address = updated.Address,
                CompanyId = updated.CompanyId
            };

            return Ok(response);
        }

        /// <summary>
        /// Delete a store by its ID.
        /// </summary>
        [HttpDelete("{storeId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStore(Guid companyId, Guid storeId)
        {
            await _storeService.DeleteAsync(companyId, storeId);
            return NoContent();
        }
    }
}
