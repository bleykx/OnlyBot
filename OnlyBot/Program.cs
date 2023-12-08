using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OnlyBot;
using OnlyBot_Business;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_DataAccess;
using Blazored.LocalStorage;
using System;
using OnlyBot_Business.UnitOfWork;
using OnlyBot_Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
ApplicationDbContext appDbContext = serviceProvider.GetService<ApplicationDbContext>();

builder.Services.AddScoped<IBotRepository, BotRepository>();
builder.Services.AddScoped<IScriptRepository, ScriptRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IRepository<Proxy>, ProxyRepository>();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors();

var app = builder.Build();
PrepDb.PrepBot(app);

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<BotsHub>("/botshub");
app.MapHub<InventoriesHub>("/inventorieshub");
app.MapHub<ScriptHub>("/scriptshub");
app.MapHub<ProxiesHub>("/proxieshub");
app.MapHub<OrdersHub>("/ordershub");

app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();