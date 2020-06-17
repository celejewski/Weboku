using Core.Converters;
using Core.Generators;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<Displayer>();
            services.AddSingleton<Executor>();
            services.AddSingleton<Informer>();
            services.AddSingleton<HodokuParser, HodokuParser>();
            services.AddSingleton<HintsProvider, HintsProvider>();
            services.AddSingleton<ShowHintModalCommand>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddSingleton<FindAllCandidatesCommand>();
            services.AddSingleton<RestartGameCommand>();
            services.AddSingleton<SelectPairsFilterCommand>();
            services.AddSingleton<SelectCleanerAction>();
            services.AddSingleton<ClearColorsCommand>();
            services.AddSingleton<RedoCommand>();
            services.AddSingleton<UndoCommand>();
            services.AddSingleton<ShowNewGameModalCommand>();
            services.AddSingleton<ShowHowToPlayModalCommand>();
            services.AddSingleton<ShowMainMenuModalCommand>();
            services.AddSingleton<CloseModalCommand>();
            services.AddSingleton<SelectStandardActionCommand>();
            services.AddSingleton<SelectEraserActionCommand>();
            services.AddSingleton<SelectColorActionCommand>();
            services.AddSingleton<ClearCandidatesCommand>();
            services.AddSingleton<ShowShareModalCommand>();
            services.AddSingleton<ShowPasteModalCommand>();
            services.AddSingleton<SelectColorActionCommand>();
            services.AddSingleton<StartNewGameFromPastedCommand>();
        }

        public static void RegisterProviders(this IServiceCollection services)
        {
            services.AddSingleton<IEmptyGridGenerator, EmptyGridGenerator>();
            services.AddSingleton<HodokuGridConverter>();
            services.AddSingleton<IGridGenerator, RESTGridGenerator>();
            services.AddSingleton<CellColorProvider>();
            services.AddSingleton<IGridHistoryManager, GridHistoryManager>();
            services.AddSingleton<FilterProvider>();
            services.AddSingleton<IClickableActionProvider, ClickableActionProvider>();
            services.AddSingleton<NumpadMenuBuilder>();
            services.AddSingleton<NumpadMenuProvider>();
            services.AddSingleton<HotkeyProvider>();
            services.AddSingleton<CommandProvider>();
            services.AddSingleton<GameTimerProvider>();
            services.AddSingleton<GameStateChecker>();
            services.AddSingleton<IGridConverter, HodokuGridConverter>();
            services.AddSingleton<ISudokuGenerator, RESTSudokuGenerator>();
            services.AddSingleton<ModalProvider>();
            services.AddSingleton<ClickableActionFactory>();
            services.AddSingleton<ShareProvider>();
            services.AddSingleton<PasteProvider>();
            services.AddSingleton<Base64GridConverter>();
            services.AddSingleton<ChainGridConverter>();
            services.AddSingleton<SudokuProvider>();
            services.AddSingleton<IGridProvider, GridProvider>();
            services.AddSingleton<CandidatesMarkProvider>();
            services.AddSingleton<RESTGridGenerator>();
            services.AddSingleton<MarkInputProvider>();
        }
    }
}
