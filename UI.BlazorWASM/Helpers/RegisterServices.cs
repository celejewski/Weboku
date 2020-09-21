using AKSoftware.Localization.MultiLanguages;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UI.BlazorWASM.ClickableActions;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Hints;
using UI.BlazorWASM.Providers;
using UI.BlazorWASM.ViewModels;

namespace UI.BlazorWASM.Helpers
{
    public static class RegisterServices
    {


        public static void RegisterLocalization(this IServiceCollection services)
        {
            services.AddLangaugeContainer(Assembly.GetExecutingAssembly());
            services.AddScoped<SettingsProvider>();
        }

        public static void RegisterHints(this IServiceCollection services)
        {
            services.AddScoped<Displayer>();
            services.AddScoped<Informer>();
            services.AddScoped<HintsProvider, HintsProvider>();
            services.AddScoped<ShowHintModalCommand>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<FindAllCandidatesCommand>();
            services.AddScoped<RestartGameCommand>();
            services.AddScoped<SelectPairsFilterCommand>();
            services.AddScoped<SelectActionEraserCommand>();
            services.AddScoped<ClearColorsCommand>();
            services.AddScoped<RedoCommand>();
            services.AddScoped<UndoCommand>();
            services.AddScoped<ShowNewGameModalCommand>();
            services.AddScoped<ShowHowToPlayModalCommand>();
            services.AddScoped<ShowMainMenuModalCommand>();
            services.AddScoped<CloseModalCommand>();
            services.AddScoped<SelectActionMarkerCommand>();
            services.AddScoped<SelectActionPencilCommand>();
            services.AddScoped<SelectActionBrushCommand>();
            services.AddScoped<ClearCandidatesCommand>();
            services.AddScoped<ShowShareModalCommand>();
            services.AddScoped<ShowPasteModalCommand>();
            services.AddScoped<SelectActionBrushCommand>();
            services.AddScoped<StartNewGameFromPastedCommand>();
            services.AddScoped<ShowPreviousModalCommand>();
            services.AddScoped<ShowSettingsModalCommand>();
            services.AddScoped<ShowCustomSudokuCommand>();
            services.AddScoped<StartNewCustomSudokuCommand>();
            services.AddScoped<StartGameCommand>();
        }

        public static void RegisterProviders(this IServiceCollection services)
        {
            services.AddScoped<CellColorProvider>();
            services.AddScoped<CandidatesMarkProvider>();
            services.AddScoped<MarkInputProvider>();
            services.AddScoped<TooltipProvider>();
            services.AddScoped<ModalProvider>();

            services.AddScoped<CommandProvider>();
            services.AddScoped<PasteProvider>();
            services.AddScoped<FilterProvider>();
            services.AddScoped<IClickableActionProvider, ClickableActionProvider>();
            services.AddScoped<ClickableActionFactory>();

            services.AddScoped<GameTimerProvider>();

            services.AddScoped<NumpadMenuProvider>();
            services.AddScoped<NumpadMenuBuilder>();

            services.AddScoped<HotkeyProvider>();

            services.AddScoped<StorageProvider>();
            services.AddScoped<PreserveStateProvider>();

            //services.AddScoped<SudokuProvider>();

            services.AddScoped<GameStateChecker>();
            //services.AddScoped<ShareProvider>();
            //services.AddScoped<IGridProvider, GridProvider>();
        }
    }
}
