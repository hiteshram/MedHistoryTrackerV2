using MedHistoryTrackerV2.Authentication;
using MedHistoryTrackerV2.Data;
using MedHistoryTrackerV2.Repository.Abstractions;
using MedHistoryTrackerV2.Repository;
using MedHistoryTrackerV2.Services.Abstractions;
using MedHistoryTrackerV2.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<WebsiteAuthenticator>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<WebsiteAuthenticator>());

builder.Services.AddTransient<IMedicineService, MedicineService>();
builder.Services.AddTransient<IMedicineRepository, MedicineRepositoryAccess>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
