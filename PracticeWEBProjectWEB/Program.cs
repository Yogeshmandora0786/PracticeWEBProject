using PracticeWEBProject.Repository;
using PracticeWEBProject.Dto;
using PracticeWEBProject.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configure ApiSettings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register services
builder.Services.AddControllersWithViews();

// Register the LoginRepository for dependency injection
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddHttpClient<LoginRepository>();

// Register the RegisterRepository for dependency injection
builder.Services.AddScoped<RegisterRepository>();
builder.Services.AddHttpClient<RegisterRepository>();

// Register the RegisterController for dependency injection
builder.Services.AddScoped<RegisterController>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

// Add a route for the registration page
app.MapControllerRoute(
    name: "register",
    pattern: "{controller=Register}/{action=Register}/{id?}");

// Run the application
app.Run();
