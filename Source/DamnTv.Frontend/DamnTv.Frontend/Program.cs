using System.Globalization;

using DamnTv.Api.Client;
using DamnTv.Frontend.Client.Pages.ViewModels;
using DamnTv.Frontend.PreviewDesign.Models;
using DamnTv.Frontend.PreviewDesign.Routes;
using DamnTv.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    //.AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();


builder.Services.AddSingleton<MinimalTvApiClient>();
builder.Services.AddTransient<ISharingViewModel, SharingViewModel>();
builder.Services.AddTransient<IPreviewBuilder, SkiaPreviewBuilder>();

var app = builder.Build();

app.MapPreviewsDistribution();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

// app.MapStaticAssets(); спасибо дяд за кеширование
app.UseStaticFiles();

app.MapRazorComponents<App>()
    //.AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DamnTv.Frontend.Client._Imports).Assembly);

app.Run();
