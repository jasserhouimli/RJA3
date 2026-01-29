using RJA3.Modules.LostItems;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLostItemServices(builder.Configuration);

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

app.Run();
