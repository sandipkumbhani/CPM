using CPM.UI.Application.Extension;
using CPM.UI.Inftrastucture.Extension;
using Microsoft.AspNetCore.Identity;
using CPM.UI.Domain.Model;
using System;

var builder = WebApplication.CreateBuilder(args);
var globalclass = new GlobalClass();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationService();
builder.Services.AddEfcoreInfrastrucureService();
builder.Services.AddHttpClient();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
   
//}).AddRoles<IdentityRole>() //Register authorization

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    // Read a specific cookie
    var token = context.Request.Cookies["AuthToken"];

    if (token != null)
    {
        globalclass.Token = token;
    }
    await next.Invoke();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Register}/{id?}");

app.Run();
