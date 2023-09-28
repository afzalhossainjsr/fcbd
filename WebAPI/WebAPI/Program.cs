using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.FileProviders;
using WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddCors();
builder.Services.AddSignalR();


var app = builder.Build();
// Add basic setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
}
app.UseRouting(); // Add this line to enable routing for your controllers.

app.UseCors(options =>
  options.WithOrigins("http://localhost:4200", "http://localhost:4300", "http://103.222.21.142:2025")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    );

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory()))
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map your API controllers here.
    endpoints.MapHub<NotificationHubs>("/notification");
});

app.Run();

