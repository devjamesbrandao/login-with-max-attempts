using Autentication.API.Configuration;
using Autentication.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies(builder.Configuration);

builder.Services.AddSwaggerConfig();

builder.Services.AddAutenticationJWTConfig(builder.Configuration);

var app = builder.Build();

DbMigrationHelpers.MigrateAsync(app).Wait();

app.UseSwaggerConfig(app.Environment);

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAutenticationJWT();

app.Run();
