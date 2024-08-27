using Infrastructure;
using Microsoft.Extensions.FileProviders;
using WebAPI;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.

builder.Services.AddInfratstructure(builder.Configuration);
builder.Services.AddWebAPIService(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin 
          .AllowCredentials());
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.MapHealthChecks("/healthchecks");

app.UseHttpsRedirection();

app.UseStaticFiles();
//Console.WriteLine(Path.Combine(builder.Environment.ContentRootPath, "ExternalFiles"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
