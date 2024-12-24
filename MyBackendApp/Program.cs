using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.WebHost.ConfigureKestrel(options =>
{
    
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
