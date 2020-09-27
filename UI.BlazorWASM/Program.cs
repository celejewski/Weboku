using Application;
using Application.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UI.BlazorWASM.Helpers;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IStorageProvider, StorageProvider>();
            builder.Services.AddScoped<DomainFacade>();
            builder.Services.RegisterProviders();
            builder.Services.RegisterCommands();
            builder.Services.RegisterHints();
            builder.Services.AddCors();
            builder.Services.RegisterLocalization();
            var app = builder.Build();
            await app.RunAsync();
        }
    }
}
