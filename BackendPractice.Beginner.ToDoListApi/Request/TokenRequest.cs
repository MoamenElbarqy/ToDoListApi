namespace BackendPractice.Beginner.TodoListAPI.Services;

public class TokenRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public TokenRequest() { }
    public TokenRequest(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}   