﻿using AKSoftware.Localization.MultiLanguages;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Weboku.UserInterface.ClickableActions;
using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Component.NumpadMenu;
using Weboku.UserInterface.Hints;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Helpers
{
    public static class RegisterServices
    {
        public static void RegisterLocalization(this IServiceCollection services)
        {
            services.AddLanguageContainer(Assembly.GetExecutingAssembly());
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
            services.AddScoped<CandidateColorProvider>();
            services.AddScoped<InputMarkProvider>();
            services.AddScoped<TooltipProvider>();
            services.AddScoped<ModalProvider>();
            services.AddScoped<CommandProvider>();
            services.AddScoped<ClickableActionProvider>();
            services.AddScoped<ClickableActionFactory>();
            services.AddScoped<GameTimerProvider>();
            services.AddScoped<NumpadMenuProvider>();
            services.AddScoped<NumpadMenuBuilder>();
            services.AddScoped<HotkeyProvider>();
            services.AddScoped<PreserveStateProvider>();
            services.AddScoped<GameStateChecker>();
        }
    }
}