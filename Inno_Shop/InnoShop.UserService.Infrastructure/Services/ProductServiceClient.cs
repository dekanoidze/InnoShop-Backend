using InnoShop.UserService.Application.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserService.Infrastructure.Services
{
    public class ProductServiceClient(HttpClient httpClient, IConfiguration configuration):IProductServiceClient
    {
        public async Task HideUserProductsAsync(Guid userId)
        {
            var url = $"{configuration["ProductServiceUrl"]}api/products/hide-by-user/{userId}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("X-Internal-Key", configuration["InternalApiKey"]);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to hide user products. Status: {response.StatusCode}. Body: {responseBody}");
            }
        }
        public async Task ShowUserProductsAsync(Guid userId)
        {
            var url = $"{configuration["ProductServiceUrl"]}api/products/show-by-user/{userId}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("X-Internal-Key", configuration["InternalApiKey"]);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to show user products");
        }

    }
}
