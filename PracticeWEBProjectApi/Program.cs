using Microsoft.EntityFrameworkCore;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using PracticeWEBProjectApi.REPOSITORY;
using PracticeWEBProjectApi.DTO;
using Microsoft.Extensions.FileProviders;

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
builder.Services.AddScoped<IBlog, BlogServices>();


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

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images/BlogImages")),
    RequestPath = new PathString("/Images/BlogImages")
});

app.Run();
