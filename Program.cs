using Microsoft.FeatureManagement;
using sqlapp.Services;

var builder = WebApplication.CreateBuilder(args);


// Get connectionString from Azure App configuration
var connectionString = "Endpoint=https://azureappconfigphanuazure.azconfig.io;Id=6K7A-la-s0:onEe354mVhE2RLGQ/QqU;Secret=056R503uKsKPEl8y0dsaUKX4Mhp9Azw53VSmQ9/GB1A=";

builder.Host.ConfigureAppConfiguration(builder =>
{
    // This line get connectionString without using FeatureFlag
    //builder.AddAzureAppConfiguration(connectionString);

    builder.AddAzureAppConfiguration(options =>
        options.Connect(connectionString).UseFeatureFlags());
});

builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddRazorPages();
// Add using service Feature Management, This allow inject the service of working with feature management in project
builder.Services.AddFeatureManagement();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
