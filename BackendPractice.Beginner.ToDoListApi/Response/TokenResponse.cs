namespace BackendPractice.Beginner.TodoListAPI;

public class TokenResponse
{
    public string AceessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresIn { get; set; }
}