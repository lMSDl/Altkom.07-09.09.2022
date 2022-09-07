var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Use(async (httpContext, next) =>
{
    Console.WriteLine("Before use1");
    await next();
    Console.WriteLine("After use1");

}); app.Use(async (httpContext, next) =>
{
    Console.WriteLine("Before use2");
    await next();
    Console.WriteLine("After use2");
});

//app.UseResponseCompression();
//app.UseResponseCaching();

//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});*/

app.MapControllers();

app.Run();
