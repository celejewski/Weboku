using AKSoftware.Localization.MultiLanguages;
using Core.Converters;
using Core.Generators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Managers;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Helpers
{
    public static class RegisterServices
    {

        public static void RegisterHints(this IServiceCollection services)
        {
            services.AddScoped<Displayer>();
            services.AddScoped<Executor>();
            services.AddScoped<Informer>();
            services.AddScoped<HodokuParser, HodokuParser>();
            services.AddScoped<HintsProvider, HintsProvider>();
            services.AddScoped<ShowHintModalCommand>();
        }

        public static void RegisterLocalization(this IServiceCollection services)
        {
            services.AddLangaugeContainer(Assembly.GetExecutingAssembly());
            services.AddScoped<SettingsProvider>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<FindAllCandidatesCommand>();
            services.AddScoped<RestartGameCommand>();
            services.AddScoped<SelectPairsFilterCommand>();
            services.AddScoped<SelectCleanerAction>();
            services.AddScoped<ClearColorsCommand>();
            services.AddScoped<RedoCommand>();
            services.AddScoped<UndoCommand>();
            services.AddScoped<ShowNewGameModalCommand>();
            services.AddScoped<ShowHowToPlayModalCommand>();
            services.AddScoped<ShowMainMenuModalCommand>();
            services.AddScoped<CloseModalCommand>();
            services.AddScoped<SelectStandardActionCommand>();
            services.AddScoped<SelectEraserActionCommand>();
            services.AddScoped<SelectColorActionCommand>();
            services.AddScoped<ClearCandidatesCommand>();
            services.AddScoped<ShowShareModalCommand>();
            services.AddScoped<ShowPasteModalCommand>();
            services.AddScoped<SelectColorActionCommand>();
            services.AddScoped<StartNewGameFromPastedCommand>();
            services.AddScoped<ShowPreviousModalCommand>();
            services.AddScoped<ShowSettingsModalCommand>();
        }

        public static void RegisterProviders(this IServiceCollection services)
        {
            services.AddScoped<IEmptyGridGenerator, EmptyGridGenerator>();
            services.AddScoped<HodokuGridConverter>();
            services.AddScoped<IGridGenerator, RESTGridGenerator>();
            services.AddScoped<CellColorProvider>();
            services.AddScoped<IGridHistoryManager, GridHistoryManager>();
            services.AddScoped<FilterProvider>();
            services.AddScoped<IClickableActionProvider, ClickableActionProvider>();
            services.AddScoped<NumpadMenuBuilder>();
            services.AddScoped<NumpadMenuProvider>();
            services.AddScoped<HotkeyProvider>();
            services.AddScoped<CommandProvider>();
            services.AddScoped<GameTimerProvider>();
            services.AddScoped<GameStateChecker>();
            services.AddScoped<IGridConverter, HodokuGridConverter>();
            services.AddScoped<ISudokuGenerator, RESTSudokuGenerator>();
            services.AddScoped<ModalProvider>();
            services.AddScoped<ClickableActionFactory>();
            services.AddScoped<ShareProvider>();
            services.AddScoped<PasteProvider>();
            services.AddScoped<Base64GridConverter>();
            services.AddScoped<ChainGridConverter>();
            services.AddScoped<SudokuProvider>();
            services.AddScoped<IGridProvider, GridProvider>();
            services.AddScoped<CandidatesMarkProvider>();
            services.AddScoped<RESTGridGenerator>();
            services.AddScoped<MarkInputProvider>();
            services.AddScoped<StorageProvider>();
            services.AddScoped<PreserveStateProvider>();
        }
    }
}
