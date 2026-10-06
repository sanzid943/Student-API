using StudentAPI.Services;
using StudentAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// controllers
builder.Services.AddControllers();

// database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// dependency injection
builder.Services.AddScoped<IStudentService, StudentService>();


// swagger/ openAPI

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app= builder.Build();

// swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// custom middleware
app.UseMiddleware<LoggingMiddleware>();

//CORS
app.UseCors("FrontendPolicy");

// HTTPs
app.UseHttpsRedirection();

// controller
app.MapControllers();

app.Run();


