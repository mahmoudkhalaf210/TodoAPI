var builder = WebApplication.CreateBuilder(args);

// add DI -- addService 

var app = builder.Build();

// Configure Pipeline -- UseMethod -- auth 

app.Run();
