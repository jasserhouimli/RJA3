using RJA3.Modules.Items.FoundItems;
using RJA3.Modules.Items.LostItems;
using RJA3.Modules.Items.ItemsMatcher;
using RJA3.Modules.Auth;
using RJA3.Modules.Users;
using RJA3.Shared.Events;
using Scalar.AspNetCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddLostItemServices(builder.Configuration);
builder.Services.AddFoundItemServices(builder.Configuration);
builder.Services.AddItemMatcherServices();
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddUserServices(builder.Configuration);

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
UserModule.SubscribeToEvents(eventBus);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();

    app.MapSwagger();
    app.UseSwaggerUI();
}







app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var api = app.MapGroup($"/api/{builder.Configuration["apiSettings:api_version"]}").WithTags("RJA3 API V1");

var lostItems = api.MapGroup("/").WithTags("LostItems");
lostItems.MapLostItemEndpoints();

var foundItems = api.MapGroup("/").WithTags("FoundItems");
foundItems.MapFoundItemEndpoints();

var itemsMatcher = api.MapGroup("/").WithTags("ItemsMatcher");
itemsMatcher.MapItemMatcherEndpoints();

var auth = api.MapGroup("/").WithTags("Authentication");
auth.MapAuthEndpoints();

var user = api.MapGroup("/").WithTags("User");
user.MapUserEndpoints();

app.Run();
