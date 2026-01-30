using RJA3.Modules.FoundItems;
using RJA3.Modules.LostItems;
using RJA3.Modules.ItemsMatcher;
using Scalar.AspNetCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
builder.Services.AddLostItemServices(builder.Configuration);
builder.Services.AddFoundItemServices(builder.Configuration);
builder.Services.AddItemMatcherServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}





app.UseHttpsRedirection();

var api = app.MapGroup($"/api/{builder.Configuration["apiSettings:api_version"]}").WithTags("RJA3 API V1");

var lostItems = api.MapGroup("/").WithTags("LostItems");
lostItems.MapLostItemEndpoints();

var foundItems = api.MapGroup("/").WithTags("FoundItems");
foundItems.MapFoundItemEndpoints();

var itemsMatcher = api.MapGroup("/").WithTags("ItemsMatcher");
itemsMatcher.MapItemMatcherEndpoints();


app.Run();
