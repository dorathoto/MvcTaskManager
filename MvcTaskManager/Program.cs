using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Context;
using MvcTaskManager.Entities;
using MvcTaskManager.Infra;
using MvcTaskManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//.AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddIdentity<Usuario, Funcao>(options =>{}).AddEntityFrameworkStores<AppDbContext>()
//               .AddDefaultTokenProviders();

builder.Services.AddIdentity<Usuario, Funcao>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
             .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
