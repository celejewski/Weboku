using System.Threading.Tasks;
using Weboku.UserInterface.Providers;

namespace Weboku.UserInterface.Commands
{
    public class SetLanguageCommand : ICommand
    {
        private readonly string _language;
        private readonly SettingsProvider _settingsProvider;

        public SetLanguageCommand(string language, SettingsProvider settingsProvider)
        {
            _language = language;
            _settingsProvider = settingsProvider;
        }

        public Task Execute()
        {
            _settingsProvider.SetLanguage(_language);
            return Task.CompletedTask;
        }
    }
}