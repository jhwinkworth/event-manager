using EventStore.Models;
using EventStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EventDb>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events API", Description = "Events Manager", Version = "v1" });
});

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          builder.WithOrigins(
            "http://example.com", "*");
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API V1");
    });

    app.UseHttpsRedirection();
}

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "Hello World!");

app.MapGet("/events", async(EventDb db) => await db.Events.ToListAsync());

app.MapPost("/events", async (EventDb db, Event e) => {
    await db.Events.AddAsync(e);
    await db.SaveChangesAsync();
    return Results.Created($"/events/{e.Id}", e);
});

app.Run();
