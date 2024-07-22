using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OnlyBot;
using OnlyBot_API.MiddlewareExtensions;
using OnlyBot_Business;
using OnlyBot_Business.Hubs;
using OnlyBot_Business.IRepository;
using OnlyBot_Business.SubscribeTable;
using OnlyBot_DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

builder.Services.AddScoped<IBotRepository, BotRepository>();
builder.Services.AddScoped<IScriptRepository, ScriptRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IProxyRepository, ProxyRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<BotsHub>();
builder.Services.AddSingleton<InventoriesHub>();
builder.Services.AddSingleton<ItemsHub>();
builder.Services.AddSingleton<ScriptsHub>();
builder.Services.AddSingleton<ProxiesHub>();
builder.Services.AddSingleton<OrdersHub>();

builder.Services.AddSingleton<SubscribeBotTableDependency>();
builder.Services.AddSingleton<SubscribeInventoryTableDependency>();
builder.Services.AddSingleton<SubscribeItemTableDependency>();
builder.Services.AddSingleton<SubscribeScriptTableDependency>();
builder.Services.AddSingleton<SubscribeProxyTableDependency>();
builder.Services.AddSingleton<SubscribeOrderTableDependency>();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors();

var app = builder.Build();
await PrepDb.PrepBot(app);

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
app.UseAuthorization();
app.MapControllers();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<BotsHub>("/botshub");
app.MapHub<InventoriesHub>("/inventorieshub");
app.MapHub<ItemsHub>("/itemshub");
app.MapHub<ScriptsHub>("/scriptshub");
app.MapHub<ProxiesHub>("/proxieshub");
app.MapHub<OrdersHub>("/ordershub");

app.MapFallbackToPage("/_Host");
app.UseSqlTableDependency<SubscribeBotTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeInventoryTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeItemTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeScriptTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeProxyTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeOrderTableDependency>(connectionString);


app.Run();