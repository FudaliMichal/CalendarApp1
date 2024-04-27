using System.Configuration;
using CalendarApp1.Components;
using CalendarApp1.Data;
using CalendarApp1.Models;
using CalendarApp1.Services;
using Microsoft.EntityFrameworkCore;

// Add BlazorBootstrap service
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBlazorBootstrap();

// Add Calendar services
builder.Services.AddTransient<CalendarDbService>();

// Add Weather Api 
builder.Services.AddTransient<WeatherForecastService>();

// Add database
builder.Services.AddDbContext<CalendarDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();