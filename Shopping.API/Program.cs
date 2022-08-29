using Shopping.API.Configuration;
using Shopping.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddConnectionConfiguration(builder.Configuration);
builder.Services.AddCrossOriginConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddNativeDependencies();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Default");

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<CurrentUserMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();