using BackendPractice.Beginner.TodoListAPI.Request;
using BackendPractice.Beginner.TodoListAPI.Data;
using BackendPractice.Beginner.TodoListAPI.Models;
using BackendPractice.Beginner.TodoListAPI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Beginner.TodoListAPI.Controllers;

[ApiController]
[Route("User")]
public class UserController(AppDbContext Dbcontext, JwtTokenProvider TokenProvider) : Controller
{
    [HttpPost("register")]
    public IActionResult register(CreateUserRequest CreateUserRequest)
    {
        User user = Dbcontext.Users.FirstOrDefault(u => u.Email == CreateUserRequest.Email);
        
        if(user is not null)
            return Conflict("User Already Exists");
        
        User newUser = Models.User.FromRequest(CreateUserRequest, PasswordHelper.Hash(CreateUserRequest.Password));
        Dbcontext.Users.Add(newUser);
        Dbcontext.SaveChanges();
        
        TokenRequest tokenRequest = new TokenRequest
        {
            Email = CreateUserRequest.Email, 
            Id =  newUser.Id,
            Name = newUser.Name
        };
        var token = TokenProvider.GenerateToken(tokenRequest);
        return Ok(new { token });
    }
    
    [HttpPost("login")]
    public IActionResult login(LoginRequest LoginRequest)
    {
        User user = Dbcontext.Users.FirstOrDefault(u => u.Email == LoginRequest.Email );
        
        
        if (user is null || !PasswordHelper.Verify(LoginRequest.Password, user.Password))
            return Unauthorized("Invalid Credentials");
        
        TokenRequest tokenRequest = new TokenRequest
        {
            Email = LoginRequest.Email, 
            Id = user.Id,
            Name = user.Name
        };
        return Ok(TokenProvider.GenerateToken(tokenRequest));
    }
}