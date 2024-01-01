using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Movies.DataAccess;

var builder = WebApplication.CreateBuilder(args);

ServiceCollectionRegistration.Setup(builder.Services, builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), app.Configuration["WWW_ROOT_PATH"] ?? "../Movies.Client")),
    RequestPath = "",
    EnableDefaultFiles = true
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var error = exceptionHandlerPathFeature?.Error;
            var guid = Guid.NewGuid();
            logger.LogError(error, guid.ToString());
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"Unexpected Internal Server Error - {guid}");
        });
    });
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();