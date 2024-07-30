using BookingSystem.DataAccess.Models;
using BookingSystem.Provider;
using BookingSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BookingCodeService>();
builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<GlobalSetupService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<BookingSystemContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();