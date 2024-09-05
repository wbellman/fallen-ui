using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FallenClient;
using FallenClient.Library.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://fallen-api.elegant.ninja/")});
builder.Services.AddScoped<FallenApi>();

await builder.Build().RunAsync();
