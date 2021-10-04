using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Weboku.Application;
using Weboku.Application.Interfaces;
using Weboku.UserInterface.Helpers;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IStorageProvider, StorageProvider>();
            builder.Services.AddScoped(serviceProvider =>
            {
                var navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
                var storageProvider = serviceProvider.GetRequiredService<IStorageProvider>();
                var domainFacade = new DomainFacade(storageProvider, navigationManager.BaseUri);
                return domainFacade;
            });
            builder.Services.RegisterProviders();
            builder.Services.RegisterHints();
            builder.Services.AddCors();
            builder.Services.RegisterLocalization();
            var app = builder.Build();
            await app.RunAsync();
        }
    }
}