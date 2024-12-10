using EmployeeApiPagination.Controllers;
using Microsoft.Extensions.DependencyInjection;
using EmployeeApiPagination.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // Allow requests from specific origin (for example, your Blazor app)
        policy.WithOrigins("https://localhost:7042")  // Replace with your Blazor app's URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<DatabaseServices>(p =>
    new DatabaseServices(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply CORS middleware before authorization (so that CORS is applied first)
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
