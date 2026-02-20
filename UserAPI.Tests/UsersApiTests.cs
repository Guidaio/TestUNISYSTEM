using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UserAPI.Infrastructure.Data;
using UserAPI.Core.Entities;

namespace UserAPI.Tests;

public class UsersApiTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly IServiceProvider _services;

    public UsersApiTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _services = factory.Services;
    }

    [Fact]
    public async Task GetUsers_ReturnsPaginatedMetadata()
    {
        await SeedUsersAsync(3);

        var response = await _client.GetAsync("/api/v1/users?page=1&pageSize=2");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var payload = await response.Content.ReadFromJsonAsync<PagedResponse<User>>();

        payload.Should().NotBeNull();
        payload!.Items.Count.Should().Be(2);
        payload.TotalCount.Should().Be(3);
        payload.TotalPages.Should().Be(2);
        payload.Page.Should().Be(1);
        payload.PageSize.Should().Be(2);
    }

    [Fact]
    public async Task GetUserById_ReturnsUser()
    {
        var userId = await SeedUsersAsync(1);

        var response = await _client.GetAsync($"/api/v1/users/{userId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var user = await response.Content.ReadFromJsonAsync<User>();

        user.Should().NotBeNull();
        user!.Id.Should().Be(userId);
    }

    private async Task<Guid> SeedUsersAsync(int count)
    {
        using var scope = _services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Users.RemoveRange(context.Users);
        await context.SaveChangesAsync();

        var users = new List<User>();
        for (var i = 0; i < count; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = $"User {i + 1}",
                Email = $"user{i + 1}@example.com",
                CreatedAt = DateTime.UtcNow.AddMinutes(-i)
            });
        }

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        return users[0].Id;
    }

    private sealed class PagedResponse<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
