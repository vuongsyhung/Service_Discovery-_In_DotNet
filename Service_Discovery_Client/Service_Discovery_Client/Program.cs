using Microsoft.Extensions.DependencyInjection; 
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddServiceDiscovery();
builder.Services.AddHttpClient(
    "Service_Discovery_Server_Api",
    static client =>
{
    client.BaseAddress = new("http://Service_Discovery_Server_Api");
}).AddServiceDiscovery();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


