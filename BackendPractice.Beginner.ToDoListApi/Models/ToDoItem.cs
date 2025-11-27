using BackendPractice.Beginner.TodoListAPI.Request;

namespace BackendPractice.Beginner.TodoListAPI.Models;

public class ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ToDoItem()
    {
    }

    public ToDoItem(CreateToDoRequest toDoRequest, int userId)
    {
        Title = toDoRequest.Title;
        Description = toDoRequest.Description;
        UserId = userId;
    }

    public void Update(UpdateToDoItemRequest UpdateToDoItemRequest)
    {
        Title = UpdateToDoItemRequest.Title;
        Description = UpdateToDoItemRequest.Description;
    }
}