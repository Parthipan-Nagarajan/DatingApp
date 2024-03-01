using API;
using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "SampleCors",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

var app = builder.Build();
app.UseCors("SampleCors");
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterAppUserActions();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await SeedDatabase.SeedUsers(context);
    }
    catch(Exception ex)
    {
        var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
        logger.LogError(ex,"Something wrong while migrate and seed data");
    }
}

app.Run();
