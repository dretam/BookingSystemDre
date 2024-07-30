using BookingSystem.DataAccess.Models;
using BookingSystem.Provider;
using BookingSystem.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<BookingCodeService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<TransactionRoomResService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<BookingSystemContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BookingCode}/{action=Index}/{id?}");

app.Run();
