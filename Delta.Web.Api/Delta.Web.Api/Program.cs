using Delta.Web.Api;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.AddApplicationServices(configuration);
services.AddControllers();
services.AddCors();
services.AddEndpointsApiExplorer();
services.AddIdentityServices(configuration);
services.AddSwaggerGen();
var app = builder.Build();
await app.SeedDataAsync();
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.ConfigureCors(configuration);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync(); 