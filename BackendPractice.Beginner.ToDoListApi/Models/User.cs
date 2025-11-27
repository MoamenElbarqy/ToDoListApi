using BackendPractice.Beginner.TodoListAPI.Request;

namespace BackendPractice.Beginner.TodoListAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<ToDoItem> Tasks { get; set; }

    public static User FromRequest(CreateUserRequest request, string hashedPassword)
    {
        User newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = hashedPassword
        };
        return newUser;
    }
}