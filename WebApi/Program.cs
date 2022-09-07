using Microsoft.AspNetCore.ResponseCompression;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IShoppingListService, ShoppingListService>();
//builder.Services.AddSingleton<IShoppingListService>(x => new ShoppingListService(x.GetService<ShoppingListFaker>(), 55));
builder.Services.AddTransient<ShoppingListFaker>();

builder.Services.AddSingleton<IShoppingListItemService, ShoppingListItemService>();
builder.Services.AddTransient<ShoppingListItemFaker>();


builder.Services.AddSingleton<ICrudService<User>>(x => new CrudService<User>(x.GetService<UserFaker>(), 10));
builder.Services.AddTransient<UserFaker>();


builder.Services.AddResponseCompression(x =>
{
    x.EnableForHttps = true;

    x.Providers.Clear();
    //x.Providers.Add<GzipCompressionProvider>();
    x.Providers.Add<BrotliCompressionProvider>();

});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
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
}

app.UseResponseCompression();
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
