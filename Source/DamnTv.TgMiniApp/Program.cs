var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var pathBase = "/mini-app";
app.UsePathBase(pathBase);

app.UseDefaultFiles();
app.UseStaticFiles();   

app.Run();
