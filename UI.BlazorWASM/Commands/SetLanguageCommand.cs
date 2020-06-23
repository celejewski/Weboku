using System.Threading.Tasks;
using UI.BlazorWASM.Providers;

namespace UI.BlazorWASM.Commands
{
    public class SetLanguageCommand : ICommand
    {
        private readonly string _name;
        private readonly SettingsProvider _settingsProvider;

        public SetLanguageCommand(string name, SettingsProvider settingsProvider)
        {
            _name = name;
            _settingsProvider = settingsProvider;
        }
        public Task Execute()
        {
            _settingsProvider.SetLanguage(_name);
            return Task.CompletedTask;
        }
    }
}
