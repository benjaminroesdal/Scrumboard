using Blazored.LocalStorage;
using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Scrumboard;
using Scrumboard.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => 
    new HttpClient
    {
    });
builder.Services.AddScoped(sp =>
{
    var http = sp.GetService<HttpClient>();
    var toasService = sp.GetService<IToastService>();
    return new BoardApiService(http, toasService);
});
builder.Services.AddMudServices();

await builder.Build().RunAsync();
