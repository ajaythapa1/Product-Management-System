using Ajay.PMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ajay.PMS.Models;
using Ajay.PMS.Irepository;
using Ajay.PMS.Repository.CRUD;
using IMS.web.Data;
using Ajay.PMS.Email;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(connectionString));

builder.Services.AddDbContext<UserDbContext>(options => 
 options.UseSqlServer(connectionString));   


builder.Services.AddIdentity<UserLogin, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();


builder.Services.AddTransient(typeof(ICrudServices<>), typeof(CrudService<>));


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedingData.InitializeAsync(services);
}

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

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
