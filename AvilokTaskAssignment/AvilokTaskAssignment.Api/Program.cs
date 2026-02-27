using AutoMapper;
using AvilokTaskAssignment.Api.AutoMapper;
using AvilokTaskAssignment.Api.Managers;
using AvilokTaskAssignment.Data;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using AvilokTaskAssignment.Api.Seed;

var builder = WebApplication.CreateBuilder(args);

#region Database

var connectionString = builder.Configuration.GetConnectionString("LocalAvilokTasksConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
        

#endregion

#region Identity

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequiredLength = 8;
    option.Password.RequireUppercase = true;
    option.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;

    options.LoginPath = "/api/auth/login";
    options.AccessDeniedPath = "/api/auth/denied";

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

#endregion

#region DI

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<AuthManager>();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMappingProfile>();
});

#endregion

#region Controllers

builder.Services.AddControllers();

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion


var app = builder.Build();

#region Set admin

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

    await IdentitySeeder.SeedAsync(userManager, roleManager);
}

#endregion

#region Middleware pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion


app.MapGet("/", () => "Hello World!");

app.Run();
