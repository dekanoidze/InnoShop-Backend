using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using InnoShop.UserService.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;

namespace InnoShop.UserService.Tests.Integration
{
    public class AuthControllerIntegrationTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient client = factory.CreateClient();

        [Fact]
        public async Task Register_ShouldReturn200_WhenValidData()
        {
            var request = new
            {
                name = "Test User",
                email = $"test_{Guid.NewGuid()}@test.com",
                password = "Password123",
                confirmPassword = "Password123"
            };

            var response = await client.PostAsJsonAsync("/api/auth/register", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Register_ShouldReturn400_WhenInvalidData()
        {
            var request = new
            {
                name = "",
                email = "notanemail",
                password = "short",
                confirmPassword = "different"
            };

            var response = await client.PostAsJsonAsync("/api/auth/register", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
