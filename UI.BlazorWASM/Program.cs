using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UI.BlazorWASM.Services;
using Core.Generators;
using Core.Converters;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.ViewModels;
using UI.BlazorWASM.Hints;

namespace UI.BlazorWASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IEmptyGridGenerator, EmptyGridGenerator>();
            builder.Services.AddSingleton<HodokuGridConverter, HodokuGridConverter>();
            builder.Services.AddSingleton<IGridGenerator, RESTGridGenerator>();
            builder.Services.AddSingleton<ICellColorProvider, CellColorProvider>();
            builder.Services.AddSingleton<ISudokuProvider, SudokuProvider>();
            builder.Services.AddSingleton<IGridHistoryManager, GridHistoryManager>();
            builder.Services.AddSingleton<IFilterProvider, FilterProvider>();
            builder.Services.AddSingleton<IClickableActionProvider, ClickableActionProvider>();
            builder.Services.AddSingleton<NumpadMenuBuilder, NumpadMenuBuilder>();
            builder.Services.AddSingleton<NumpadMenuProvider, NumpadMenuProvider>();
            builder.Services.AddSingleton<HotkeyProvider, HotkeyProvider>();
            builder.Services.AddSingleton<CommandProvider, CommandProvider>();
            builder.Services.AddSingleton<IGameTimerProvider, GameTimerProvider>();
            builder.Services.AddSingleton<IGameStateChecker, GameStateChecker>();
            builder.Services.AddSingleton<IGridConverter, HodokuGridConverter>();
            builder.Services.AddSingleton<ISudokuGenerator, RESTSudokuGenerator>();
            builder.Services.AddSingleton<ModalProvider, ModalProvider>();
            builder.Services.AddSingleton<HintProvider, HintProvider>();
            builder.Services.AddCors();
            var app = builder.Build();
           
            await app.RunAsync();
        }
    }
}
