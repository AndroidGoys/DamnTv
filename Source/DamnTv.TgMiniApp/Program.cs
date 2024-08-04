WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UsePathBase("/mini-app");
app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
