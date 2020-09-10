using Core;
using UI.BlazorWASM.Commands;

namespace UI.BlazorWASM.Providers
{
    public class CommandProvider
    {
        private readonly FilterProvider _filterProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly SettingsProvider _settingsProvider;
        private readonly ModalProvider _modalProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly GridHistoryProvider _gridHistoryProvider;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly DomainFacade _domainFacade;

        public CommandProvider(
            FilterProvider filterProvider,
            IClickableActionProvider clickableActionProvider,
            SettingsProvider settingsProvider,
            ModalProvider modalProvider,
            CellColorProvider cellColorProvider,
            GridHistoryProvider gridHistoryProvider,
            GameTimerProvider gameTimerProvider,
            DomainFacade domainFacade

            )
        {
            _filterProvider = filterProvider;
            _clickableActionProvider = clickableActionProvider;
            _settingsProvider = settingsProvider;
            _modalProvider = modalProvider;
            _cellColorProvider = cellColorProvider;
            _gridHistoryProvider = gridHistoryProvider;
            _gameTimerProvider = gameTimerProvider;
            _domainFacade = domainFacade;
        }
        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _filterProvider, _clickableActionProvider);
        }

        public ICommand StartNewGameV2(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _modalProvider, _cellColorProvider, _gridHistoryProvider, _gameTimerProvider, _domainFacade);
        }

        public ICommand SetLanguage(string name)
        {
            return new SetLanguageCommand(name, _settingsProvider);
        }
    }
}
