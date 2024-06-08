using Microsoft.EntityFrameworkCore;
using TodoAPI;

var builder = WebApplication.CreateBuilder(args);

// add DI -- addService 

builder.Services.AddDbContext<TodoDB>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure Pipeline -- UseMethod -- auth 

app.Run();
