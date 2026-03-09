using FluentAssertions;
using InnoShop.ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.ProductService.Tests.Integration
{
    public class InternalProductsControllerIntegrationTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly Guid _userId = Guid.NewGuid();
        private List<Product> _seededProducts = [];
        private const string ValidInternalKey = "InnoShopInternalKey123!";

        public async Task InitializeAsync()
        {
            _seededProducts = await SeedHelper.SeedProductsAsync(factory.Services, _userId, count: 3);
        }

        public async Task DisposeAsync()
        {
            await SeedHelper.CleanProductsAsync(factory.Services, _userId);
        }

        [Fact]
        public async Task HideUserProducts_ShouldReturn200_WhenValidApiKey()
        {
          _client.DefaultRequestHeaders.Add("X-Internal-Key", ValidInternalKey);

            var response = await _client.PostAsync($"/api/products/hide-by-user/{_userId}", null);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task HideUserProducts_ShouldReturn401_WhenInvalidApiKey()
        {
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Internal-Key", "NotCorrectKey");

            var response = await client.PostAsync($"/api/products/hide-by-user/{_userId}", null);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task HideUserProducts_ShouldReturn401_WhenNoApiKey()
        {
            var response = await _client.PostAsync($"/api/products/hide-by-user/{_userId}", null);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task ShowUserProducts_ShouldReturn200_WhenValidApiKey()
        {
            _client.DefaultRequestHeaders.Add("X-Internal-Key", ValidInternalKey);

            var response = await _client.PostAsync($"/api/products/show-by-user/{_userId}", null);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task ShowUserProducts_ShouldReturn401_WhenInvalidApiKey()
        {
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Internal-Key", "NotCorrectKey");

            var response = await client.PostAsync($"/api/products/show-by-user/{_userId}", null);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task ShowUserProducts_ShouldReturn401_WhenNoApiKey()
        {
            var response = await _client.PostAsync($"/api/products/show-by-user/{_userId}", null);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
