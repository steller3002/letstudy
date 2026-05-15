using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Letstudy.Core;
using Letstudy.Models;
using Letstudy.Requests;
using Letstudy.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject1;

public class AuthTests : BaseIntegrationTest
{
    public AuthTests(LetstudyWebApplicationFactory factory) : base(factory) { }

    [Fact(DisplayName = "Регистрация ученика")]
    public async Task Register_Student_SavesToDbAndReturnOk()
    {
        // Arrange
        const string name = "Иван";
        const string surname = "Иванов";
        const string email = "ivanov@gmail.com";
        const string password = "password123";
        const UserRole role = UserRole.Student;
        
        var request = new RegisterRequest(
            name,
            surname, 
            email, 
            password, 
            password, 
            role);
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        result.Should().NotBeNull();
        result.UserId.Should().NotBeEmpty();

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userInDb = await db.Users.FindAsync(result.UserId);
        
        userInDb.Should().NotBeNull();
        userInDb.Should().BeOfType<Student>();
        userInDb.Name.Should().Be(name);
        userInDb.Surname.Should().Be(surname);
        userInDb.Email.Should().Be(email);
        userInDb.Role.Should().Be(role);
    }
}