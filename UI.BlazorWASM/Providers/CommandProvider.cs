using Core.Converters;
using Core.Generators;
using UI.BlazorWASM.Commands;
using UI.BlazorWASM.Managers;

namespace UI.BlazorWASM.Providers
{
    public class CommandProvider
    {
        private readonly ISudokuGenerator _sudokuGenerator;
        private readonly IGridHistoryManager _gridHistoryManager;
        private readonly GameTimerProvider _gameTimerProvider;
        private readonly FilterProvider _filterProvider;
        private readonly CellColorProvider _cellColorProvider;
        private readonly IClickableActionProvider _clickableActionProvider;
        private readonly ModalProvider _modalProvider;
        private readonly IGridProvider _gridProvider;
        private readonly HodokuGridConverter _hodokuGridConverter;
        private readonly SudokuProvider _sudokuProvider;
        private readonly SettingsProvider _settingsProvider;

        public CommandProvider(
            ISudokuGenerator sudokuGenerator,
            IGridHistoryManager gridHistoryManager,
            GameTimerProvider gameTimerProvider,
            FilterProvider filterProvider,
            CellColorProvider cellColorProvider,
            IClickableActionProvider clickableActionProvider,
            ModalProvider modalProvider,
            IGridProvider gridProvider,
            HodokuGridConverter hodokuGridConverter,
            SudokuProvider sudokuProvider,
            SettingsProvider settingsProvider)
        {
            _sudokuGenerator = sudokuGenerator;
            _gridHistoryManager = gridHistoryManager;
            _gameTimerProvider = gameTimerProvider;
            _filterProvider = filterProvider;
            _cellColorProvider = cellColorProvider;
            _clickableActionProvider = clickableActionProvider;
            _modalProvider = modalProvider;
            _gridProvider = gridProvider;
            _hodokuGridConverter = hodokuGridConverter;
            _sudokuProvider = sudokuProvider;
            _settingsProvider = settingsProvider;
        }
        public ICommand SelectValue(int value)
        {
            return new SelectValueCommand(value, _filterProvider, _clickableActionProvider);
        }

        public ICommand StartNewGameV2(string difficulty)
        {
            return new StartNewGameCommand(difficulty, _sudokuGenerator, _gridProvider, _modalProvider, _cellColorProvider, _gridHistoryManager, _gameTimerProvider, _hodokuGridConverter, _sudokuProvider);
        }

        public ICommand SetLanguage(string name)
        {
            return new SetLanguageCommand(name, _settingsProvider);
        }
    }
}
