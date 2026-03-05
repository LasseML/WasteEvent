using System.Text.Json.Serialization;
using WasteEvent;
using WasteEvent.Server.WasteEventRealtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR()
    .AddJsonProtocol(options => {
    options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;

builder.Services.AddSingleton<WasteEventService>();
builder.Services.AddHostedService<WasteEventUpdater>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy
            .WithOrigins("https://localhost:60191")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.Configure<WasteEventUpdateOptions>(builder.Configuration.GetSection("WasteEventUpdateOptions"));

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<WasteEventHub>("/wasteEvent");

app.MapFallbackToFile("/index.html");

app.Run();
