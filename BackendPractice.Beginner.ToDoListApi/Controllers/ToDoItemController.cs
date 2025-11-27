using System.Security.Claims;
using BackendPractice.Beginner.TodoListAPI.Data;
using BackendPractice.Beginner.TodoListAPI.Models;
using BackendPractice.Beginner.TodoListAPI.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Beginner.TodoListAPI.Controllers;

[ApiController]
[Route("todos")]
[Authorize]
public class ToDoItemController(AppDbContext DbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        return Ok(DbContext.ToDoItems.Where(t => t.UserId == UserId).ToList());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        ToDoItem ItemToDelete = DbContext.ToDoItems.FirstOrDefault(t => t.Id == id);
        
        if(ItemToDelete is null)
            return NotFound("Item not found");
        if(ItemToDelete.UserId != UserId)
            return Forbid("Forbidden");

        DbContext.ToDoItems.Remove(ItemToDelete);
        DbContext.SaveChanges();

        return NoContent();
        
    }

    [HttpPost]
    public IActionResult Create(CreateToDoRequest CreateToDoRequest)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        ToDoItem newToDoItem = new ToDoItem(CreateToDoRequest, userId);

        DbContext.ToDoItems.Add(newToDoItem);
        DbContext.SaveChanges();

        return Ok(newToDoItem);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateToDoItemRequest UpdateToDoItemRequest)
    {
        int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        ToDoItem ItemToUpdate = DbContext.ToDoItems.FirstOrDefault
        (
            t => t.Id == id
        );
        if(ItemToUpdate is null)
            return NotFound("Item not found");
        if(ItemToUpdate.UserId != UserId)
            return Forbid("Forbidden");
        
        ItemToUpdate.Update(UpdateToDoItemRequest);

        DbContext.SaveChanges();

        return Ok(ItemToUpdate);
    }
    
    [HttpGet("paginate")]
    public IActionResult Paginate(int page = 1, int limit = 10)
    {
        if(page < 0) page = 1;
        if(limit < 0) limit = 10;
        
        int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var ToDoItems = DbContext.ToDoItems.Where(item => item.UserId == UserId)
            .Skip(page * limit - limit).Take(limit).ToList();
        return Ok(new
        {
            data = ToDoItems.Select(i => new
            {
                i.Id,
                i.Title,
                i.Description
            }),
            page = page,
            limit = limit,
            total = DbContext.ToDoItems.Count(item => item.UserId == UserId)
        });
    }
    
}