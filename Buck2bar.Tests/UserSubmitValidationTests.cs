using System.ComponentModel.DataAnnotations;
using Buck2bar.Models;

namespace Buck2bar.Tests;

public class UserSubmitValidationTests
{
    [Fact]
    public void SubmitPayload_WithValidUser_PassesValidation()
    {
        var user = new User
        {
            Name = "Ada Lovelace",
            Email = "ada@example.com",
            Role = "Admin",
            IsActive = true
        };

        var results = Validate(user);

        Assert.Empty(results);
    }

    [Theory]
    [InlineData("", "ada@example.com", "Admin", "The Name field is required.")]
    [InlineData("Ada Lovelace", "", "Admin", "The Email field is required.")]
    [InlineData("Ada Lovelace", "not-an-email", "Admin", "The Email field is not a valid e-mail address.")]
    [InlineData("Ada Lovelace", "ada@example.com", "", "The Role field is required.")]
    public void SubmitPayload_WithInvalidRequiredFields_FailsValidation(
        string name,
        string email,
        string role,
        string expectedError)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            Role = role,
            IsActive = true
        };

        var results = Validate(user);

        Assert.Contains(results, result => result.ErrorMessage == expectedError);
    }

    [Fact]
    public void SubmitPayload_WithNameOverMaximumLength_FailsValidation()
    {
        var user = new User
        {
            Name = new string('A', 101),
            Email = "ada@example.com",
            Role = "Admin",
            IsActive = true
        };

        var results = Validate(user);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(User.Name)));
    }

    private static List<ValidationResult> Validate(User user)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(user);

        Validator.TryValidateObject(user, context, results, validateAllProperties: true);

        return results;
    }
}