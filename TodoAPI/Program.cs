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


app.Run();
