
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Data.Models;
using TodoList.Extensions;
using TodoList.Interfaces;
using TodoList.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Conntect DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services
    .AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add Services
builder.Services.AddScoped<ITodos, TodosService>();
builder.Services.AddScoped<IUser, UserService>();

builder.Services.AddCustomJwtAuth(builder.Configuration);

builder.Services.AddScoped<IAuth, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.Run();
