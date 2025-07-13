using GlobalStore.Functions.Models;
using GlobalStore.Functions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace GlobalStore.Functions.Functions
{
    public class ProductFunctions
    {
        private readonly IProductService _service;

        public ProductFunctions(IProductService service)
        {
            _service = service;
        }

        [Function("GetProductsList")]
        public async Task<HttpResponseData> GetProductsList([HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequestData req)
        {
            var json = await _service.GetProductsJsonAsync();
            var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(json);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(products);

            return response;
        }

        [Function("GetProductById")]
        public async Task<HttpResponseData> GetById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{id:int}")] HttpRequestData req, int id)
        {
            var product = await _service.GetByIdAsync(id);
            var response = req.CreateResponse();

            if (product == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            await response.WriteAsJsonAsync(product);
            return response;
        }

        [Function("CreateProduct")]
        public async Task<HttpResponseData> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequestData req)
        {
            var product = await req.ReadFromJsonAsync<Product>();
            await _service.CreateAsync(product!);

            var response = req.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [Function("UpdateProduct")]
        public async Task<HttpResponseData> Update([HttpTrigger(AuthorizationLevel.Function, "put", Route = "products/{id:int}")] HttpRequestData req, int id)
        {
            var product = await req.ReadFromJsonAsync<Product>();
            product!.Id = id;

            await _service.UpdateAsync(product);

            var response = req.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }

        [Function("DeleteProduct")]
        public async Task<HttpResponseData> Delete([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "products/{id:int}")] HttpRequestData req, int id)
        {
            await _service.DeleteAsync(id);
            var response = req.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
    }
}
