using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(/*x => x.Filters.Add(new ProducesAttribute("application/xml"))*/)
    /*.AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString;
        x.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    })*/
    .AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        x.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
        x.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        //x.SerializerSettings.DateFormatString = "yyy MMM-d _ h:mm;ss";
        //x.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
    });
    //.AddXmlDataContractSerializerFormatters()
    /*.AddXmlSerializerFormatters()*/;

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
    x.Providers.Add<GzipCompressionProvider>();
    //x.Providers.Add<BrotliCompressionProvider>();

});

//wy??czamy domy?ln?, globaln? walidacj? modlu
builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
 {
     x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
     {
         ValidateAudience = false,
         ValidateIssuer = false,
         ClockSkew = TimeSpan.FromSeconds(15),
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(AuthService.Key)
     };
 });
builder.Services.AddSingleton<AuthService>();


builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebApi", Version = "v1" }))
    .AddSwaggerGenNewtonsoftSupport();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerWebApi v1"));

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
