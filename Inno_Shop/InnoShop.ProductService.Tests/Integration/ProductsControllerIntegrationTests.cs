using FluentAssertions;
using InnoShop.ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace InnoShop.ProductService.Tests.Integration
{
    public class ProductsControllerIntegrationTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly Guid _userId = Guid.NewGuid();
        private List<Product> _seededProducts = [];

        public async Task InitializeAsync()
        {
            _seededProducts = await SeedHelper.SeedProductsAsync(factory.Services, _userId, count: 3);
        }

        public async Task DisposeAsync()
        {
            await SeedHelper.CleanProductsAsync(factory.Services, _userId);
        }
        [Fact]
        public async Task GetAllProducts_ShouldReturn200_WithProducts()
        {
            var response = await _client.GetAsync("/api/products");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetProductsById_ShouldReturn200_WithProducts()
        {
            var productId = _seededProducts.First().Id;

            var response = await _client.GetAsync($"/api/products/{productId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetProductsByUserId_ShouldReturn200_WhenAuthenticated()
        {
            var auth = _client.WithAuth(_userId);

            var response = await auth.GetAsync($"/api/products/user/{_userId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetProductsByUserId_ShouldReturn401_WhenNotAuthenticated()
        {
            var unauthClient = factory.CreateClient();

            var response = await unauthClient.GetAsync($"/api/products/user/{_userId}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task CreateProduct_ShouldReturn200_WhenValidAndAuthenticated()
        {
           var auth = _client.WithAuth(_userId);

            var payload = new
            {
                name = "New Product",
                description = "Test description",
                price = 57.43m
            };

            var response = await auth.PostAsJsonAsync($"/api/products", payload);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task CreateProduct_ShouldReturn401_WhenNotAuthenticated()
        {
            var unauthClient = factory.CreateClient();

            var payload = new { name = "Product", description = "descript", price = 24m };

            var response = await unauthClient.PostAsJsonAsync($"/api/products", payload); ;

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task CreateProduct_ShouldReturn400_WhenInvalidData()
        {
            var auth= _client.WithAuth(_userId);

            var payload = new
            {
                name = "",
                price = -10.15m
            };

            var response = await auth.PostAsJsonAsync("/api/products", payload);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task UpdateProduct_ShouldReturn200_WhenOwnerUpdates()
        {
            var product = _seededProducts.First();

            var auth = _client.WithAuth(_userId);

            var payload = new
            {
                name = "Update Name",
                description = "Updated description",
                price = 23.32m
            };

            var response = await auth.PutAsJsonAsync($"/api/products/{product.Id}", payload);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task UpdateProduct_ShoulReturn403_WhenNotOwner()
        {
            var product = _seededProducts.First();

            Guid otherUserId = Guid.NewGuid();

            var auth = _client.WithAuth(otherUserId);

            var payload = new
            {
                name = "Update Name",
                description = "Updated description",
                price = 23.32m
            };

            var response = await auth.PutAsJsonAsync($"/api/products/{product.Id}", payload);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [Fact]
        public async Task UpdateProduct_ShoulReturn401_WhenNoAuthenticated()
        {
            var product = _seededProducts.First();

            var unauthClient = factory.CreateClient();

            var payload = new
            {
                name = "Update Name",
                description = "Updated description",
                price = 23.32m
            };

            var response = await unauthClient.PutAsJsonAsync($"/api/products/{product.Id}", payload);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task DeleteProduct_ShouldReturn200_WhenOwnerDeletes()
        {
            var product = _seededProducts.First();

            var auth = _client.WithAuth(_userId);

            var response = await auth.DeleteAsync($"/api/products/{product.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task DeleteProduct_ShoulReturn403_WhenNotOwner()
        {
            var product = _seededProducts.First();

            Guid otherUserId = Guid.NewGuid();

            var auth = _client.WithAuth(otherUserId);

            var response = await auth.DeleteAsync($"/api/products/{product.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
        [Fact]
        public async Task DeleteProduct_ShoulReturn401_WhenNoAuthenticated()
        {
            var product = _seededProducts.First();

            var unauthClient = factory.CreateClient();

            var response = await unauthClient.DeleteAsync($"/api/products/{product.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
