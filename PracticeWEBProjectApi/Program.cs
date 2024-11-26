using Microsoft.EntityFrameworkCore;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using PracticeWEBProjectApi.REPOSITORY;
using PracticeWEBProjectApi.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<PracticeDbContext>(options =>
  //  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStrings")));


// Register your repository and services
builder.Services.AddSingleton<DBContext>();
builder.Services.AddScoped<IRegistration, RegistrationService>();
builder.Services.AddScoped<ILogin, LoginService>();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
