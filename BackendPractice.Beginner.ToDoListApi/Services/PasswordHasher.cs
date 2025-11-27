using BackendPractice.Beginner.TodoListAPI.Models;

namespace BackendPractice.Beginner.TodoListAPI.Services;
using Microsoft.AspNetCore.Identity;

public static class PasswordHelper
{
    private static PasswordHasher<User> _hasher = new PasswordHasher<User>();

    public static string Hash(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    public static bool Verify(string password, string hashedPassword)
    {
        var result = _hasher.VerifyHashedPassword(null!, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}