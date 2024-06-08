using Microsoft.EntityFrameworkCore;
using TodoAPI;

var builder = WebApplication.CreateBuilder(args);

// add DI -- addService 

builder.Services.AddDbContext<TodoDB>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure Pipeline -- UseMethod -- auth 
app.MapGet("/todoitems", async (TodoDB db) =>
    await db.Todos.ToListAsync()
);

app.MapGet("/todoitems/{id}", async (int id, TodoDB db) =>
await db.Todos.FindAsync(id)
);

app.MapPost("/todoitems", async (TodoItem todo, TodoDB db) =>
{ 
        db.Todos.Add(todo);
       await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.Id}", todo);
});


app.MapPut("/todoitems", async (int id, TodoItem newTodo, TodoDB db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();
    todo.Name = newTodo.Name;
    todo.IsComplete = newTodo.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDB db) => {
    if (await db.Todos.FindAsync(id) is TodoItem todo) { 
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();
