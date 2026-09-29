using StudentAPI.Services;
using StudentAPI.Middleware;
using StudentAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

// controllers
builder.Services.AddControllers();

// auto mapper

builder.Services.AddAutoMapper(typeof(Program));

// dependency injection
builder.Services.AddScoped<IStudentService, StudentService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


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


