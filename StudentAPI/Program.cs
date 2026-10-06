using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Mappings;
using StudentAPI.Middleware;
using StudentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// controllers
builder.Services.AddControllers();

// auto mapper

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<StudentProfile>();
});


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

// HTTPs
app.UseHttpsRedirection();

// controller
app.MapControllers();

app.Run();


