using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
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
