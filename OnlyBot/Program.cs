using Microsoft.AspNetCore.ResponseCompression;
using OnlyBot_DataAccess;
using Blazored.LocalStorage;
using OnlyBot.IServices;
using OnlyBot.Services;
using Microsoft.AspNetCore.Components.Web;
using OnlyBot;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
//builder.Services.AddSwaggerGen();
//builder.Services.AddBlazoredLocalStorage();

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
ApplicationDbContext appDbContext = serviceProvider.GetService<ApplicationDbContext>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44321/api/") });

builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IScriptService, ScriptService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IProxyService, ProxyService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors();

await builder.Build().RunAsync();