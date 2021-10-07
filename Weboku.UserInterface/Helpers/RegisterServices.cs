using AKSoftware.Localization.MultiLanguages;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Weboku.UserInterface.Commands;
using Weboku.UserInterface.Component.NumpadMenu;
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


        public static void RegisterProviders(this IServiceCollection services)
        {
            services.AddScoped<TooltipProvider>();
            services.AddScoped<CommandProvider>();
            services.AddScoped<NumpadMenuProvider>();
            services.AddScoped<NumpadMenuBuilder>();
            services.AddScoped<HotkeyProvider>();
            services.AddScoped<PreserveStateProvider>();
        }
    }
}