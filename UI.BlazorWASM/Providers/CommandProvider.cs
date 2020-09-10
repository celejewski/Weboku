using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Providers
{
    public class CommandProvider
    {
        private readonly FilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly SettingsProvider _settingsProvider;

        public CommandProvider(
            FilterProvider filterProvider,
            IClickableActionProvider clickableActionProvider,
            SettingsProvider settingsProvider)
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _settingsProvider = settingsProvider;
        }
        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _filterProvider, _clickableActionProvider);
        }

        public ICommand StartNewGameV2(string difficulty)
        {
            return new StartNewGameCommand();
        }

        public ICommand SetLanguage(string name)
        {
            return new SetLanguageCommand(name, _settingsProvider);
        }
    }
}
