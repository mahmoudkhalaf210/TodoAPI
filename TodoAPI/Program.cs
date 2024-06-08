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

app.MapGet("/todoitem/{id}", async (int id, TodoDB db) =>
await db.Todos.FindAsync(id)
);


app.Run();
